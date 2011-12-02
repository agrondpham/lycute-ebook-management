using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.Animation;
using System.Diagnostics;
using System.Windows.Threading;
using LycuteEbookManagement._3DWall;
using System.Collections.ObjectModel;
using Agrond.DataAccess;
using Agrond.ObjectLib;
using Agrond.SearchEngin;

namespace LycuteEbookManagement.Ebook
{
    public delegate void ModelSelectedEventHandler(object sender, ModelSelectedEventArgs e);
    /// <summary>
    /// Interaction logic for _3DWall.xaml
    /// </summary>
    public partial class _3DWall : UserControl
    {
        #region Data
        public bool Focused { get; set; }
        private Dictionary<ModelUIElement3D, Book> modelToImageLookUp = new Dictionary<ModelUIElement3D, Book>();
        public Book CurrentObject { get; private set; }
        private const double MODEL_OFFSET = 1.05;
        private DispatcherTimer angleAnimationTimer = new DispatcherTimer();
        private DispatcherTimer loadTimer = new DispatcherTimer();
        MainWindow m;
        //private SearchTypes currentSearchtype = SearchTypes.FlickrLatest;
        #endregion

        #region Events


        //The actual event routing
        public static readonly RoutedEvent ModelSelectedEvent =
            EventManager.RegisterRoutedEvent(
            " ModelSelected", RoutingStrategy.Bubble,
            typeof(ModelSelectedEventHandler),
            typeof(_3DWall));

        //add remove handlers
        public event ModelSelectedEventHandler ModelSelected
        {
            add { AddHandler(ModelSelectedEvent, value); }
            remove { RemoveHandler(ModelSelectedEvent, value); }
        }
        #endregion

        public _3DWall()
        {
            InitializeComponent();

            IsSearchAreaShown = false;
            this.Loaded += ucslideImages3DViewPort_Loaded;
            loadTimer.Interval = TimeSpan.FromSeconds(2);
            loadTimer.IsEnabled = false;
            loadTimer.Tick += loadTimer_Tick;

            this.Loaded += new RoutedEventHandler(loadParent);
        }
        /// <summary>
        /// Load Parent control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadParent(object sender, RoutedEventArgs e)
        {
            m = (MainWindow)Window.GetWindow(this);
        }
        /// <summary>
        /// load Conduct function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucslideImages3DViewPort_Loaded(object sender, RoutedEventArgs e)
        {
            Conduct();
        }
        private void loadTimer_Tick(object sender, EventArgs e)
        {

            loadingWait1.Visibility = Visibility.Hidden;
            controlsArea.Visibility = Visibility.Visible;
            IsAnimating = false;
            IsVisible = true;
            loadTimer.IsEnabled = false;
        }
        private void Conduct()
        {
            //currentSearchtype = newSearchType;
            loadTimer.IsEnabled = true;
            controlsArea.Visibility = Visibility.Collapsed;
            
            loadingWait1.Visibility = Visibility.Visible;
            IsAnimating = true;
            IsVisible = false;

            //Load the images async, but assume that the loadTimer time will
            //be enough to cover how long it will take to fetch and display
            //all the images

            var data = GetQueryResults();
            //Create 3D models for the images 
            CreateModelsForImages(data);
        }
        /// <summary>
        /// Get data from datasource
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<Book> GetQueryResults()
        {
            BookLib bokLib = new BookLib();
            return bokLib.Search(SearchLib.Search(""));
        }
        private void CreateModelsForImages(ObservableCollection<Book> pBookObj)
        {
            int bookNum = 0;
            IsVisible = false;
            container.Children.Clear();
            modelToImageLookUp.Clear();

            for (int rows = 0; rows < Wall3dHelper.ROWS; rows++)
            {
                for (int col = 0; col < Wall3dHelper.COLUMNS; col++)
                {
                    if (bookNum < pBookObj.Count)
                    {
                        container.Children.Add(CreateModel(pBookObj[bookNum], rows, col));
                        bookNum++;
                    }
                    else { break; }
                }
            }
        }
        private ModelUIElement3D CreateModel(Book pBookObj, int row, int col)
        {
            //Get a VisualBrush for the Url
            VisualBrush vBrush = GetVisualBrush(pBookObj.bok_ImageURl);

            //Create the model
            ModelUIElement3D model3D = new ModelUIElement3D
            {
                Model = new GeometryModel3D
                {
                    Geometry = new MeshGeometry3D
                    {
                        TriangleIndices = new Int32Collection(
                            new int[] { 0, 1, 2, 2, 3, 0 }),
                        TextureCoordinates = new PointCollection(
                            new Point[] 
                            { 
                                new Point(0, 1), 
                                new Point(1, 1), 
                                new Point(1, 0), 
                                new Point(0, 0) 
                            }),
                        Positions = new Point3DCollection(
                            new Point3D[] 
                            { 
                                new Point3D(-0.35, -0.5, 0), 
                                new Point3D(0.35, -0.5, 0), 
                                new Point3D(0.35, 0.5, 0), 
                                new Point3D(-0.35, 0.5, 0) 
                            })
                    },
                    Material = new DiffuseMaterial
                    {
                        Brush = vBrush
                    },
                    BackMaterial = new DiffuseMaterial
                    {
                        Brush = Brushes.Black
                    },
                    Transform = CreateGroup(row, col)
                }
            };
            //hook up mouse events, and add to lookup and return the ModelUIElement3D
            model3D.MouseEnter += ModelUIElement3D_MouseEnter;
            model3D.MouseDown += model3D_MouseDown;
            modelToImageLookUp.Add(model3D, pBookObj);
            return model3D;
        }
        private void model3D_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CurrentObject = modelToImageLookUp[sender as ModelUIElement3D];

            //raise our custom CustomClickWithCustomArgs event
            LycuteEbookManagement.Ebook.Detail._book = CurrentObject;
            m.loadMain(new Ebook.Detail());


        }
        private void ModelUIElement3D_MouseEnter(object sender, MouseEventArgs e)
        {
//do some thig when mouse moveover
        }

        private VisualBrush GetVisualBrush(string url)
        {
            Border bord = new Border();
            bord.Width = 15;
            bord.Height = 15;
            bord.CornerRadius = new CornerRadius(0);
            bord.BorderThickness = new Thickness(0.5);
            bord.BorderBrush = Brushes.WhiteSmoke;
            try
            {
                //Interaction.GetBehaviors() interaction = new ReflectionBehavior();
                Image img = new Image
                {
                    Source = new BitmapImage(new Uri(@url, UriKind.RelativeOrAbsolute)),
                    Stretch = Stretch.Fill,
                    Margin = new Thickness(0),
                    //interac

                };

                bord.Child = img;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message, "ERROR");
            }

            VisualBrush vBrush = new VisualBrush(bord);
            return vBrush;
        }
        private void Animate(int col)
        {
            double move = col * -MODEL_OFFSET;

            Storyboard storyboard = new Storyboard();
            ParallelTimeline timeline = new ParallelTimeline();
            timeline.BeginTime = TimeSpan.FromSeconds(0);
            timeline.Duration = TimeSpan.FromSeconds(2);
            //do move
            DoubleAnimation daMove = new DoubleAnimation(move, new Duration(TimeSpan.FromSeconds(2)));
            daMove.DecelerationRatio = 1.0;
            Storyboard.SetTargetName(daMove, "contTrans");
            Storyboard.SetTargetProperty(daMove, new PropertyPath(TranslateTransform3D.OffsetXProperty));

            //do angle
            double angle = col > Wall3dHelper.COLUMNS / 2 ? -15 : 15;
            DoubleAnimation daAngle = new DoubleAnimation(angle, new Duration(TimeSpan.FromSeconds(0.8)));
            Storyboard.SetTargetName(daAngle, "contAngle");
            Storyboard.SetTargetProperty(daAngle, new PropertyPath(AxisAngleRotation3D.AngleProperty));

            DoubleAnimation daAngle2 = new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(1)));
            daAngle2.BeginTime = daAngle.Duration.TimeSpan;
            Storyboard.SetTargetName(daAngle2, "contAngle");
            Storyboard.SetTargetProperty(daAngle2, new PropertyPath(AxisAngleRotation3D.AngleProperty));

            timeline.Children.Add(daMove);
            timeline.Children.Add(daAngle);
            timeline.Children.Add(daAngle2);

            storyboard.Children.Add(timeline);

            storyboard.Begin(this);
        }

        private Transform3DGroup CreateGroup(int row, int col)
        {
            Transform3DGroup group = new Transform3DGroup();
            group.Children.Add(new TranslateTransform3D
            {
                OffsetX = col * MODEL_OFFSET,
                OffsetY = row * -MODEL_OFFSET,
                OffsetZ = 0.0
            });
            group.Children.Add(new RotateTransform3D
            {
                Rotation = new AxisAngleRotation3D
                {
                    Angle = 0,
                    Axis = new Vector3D(1, 0, 0)
                }
            });
            group.Children.Add(new ScaleTransform3D
            {
                ScaleX = 1.0,
                ScaleY = 1.0,
                ScaleZ = 1.0
            });
            return group;
        }

        #region Activities
        private void slideImages_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Animate((int)Math.Round(slideImages.Value));
        }
        private void slideZoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            CameraPosition = e.NewValue;
        }
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            double value = Math.Max(0, e.Delta / 10);//divide the value by 10 so that it is more smooth
            value = Math.Min(e.Delta, Wall3dHelper.COLUMNSTOSHOW);
            //slideZoom.Value = value;
        }
        #endregion
        #region Public Properties

        /// <summary>
        /// True if there is current an Animation in progress
        /// </summary>
        public bool IsAnimating { get; private set; }


        /// <summary>
        /// True if there teh search area is shown
        /// </summary>
        public bool IsSearchAreaShown { get; private set; }


        /// <summary>
        /// Shows or hides the ViewPort3D
        /// </summary>
        public new bool IsVisible
        {
            set
            {
                if (value)
                    vp.Visibility = Visibility.Visible;
                else
                    vp.Visibility = Visibility.Hidden;
            }
        }


        /// <summary>
        /// The new Camera Z position. When set aninmates the camera position to the new
        /// Z position. This is a simulated Zoom
        /// </summary>
        public double CameraPosition
        {
            set
            {
                Point3D newPosition = new Point3D(camera.Position.X, camera.Position.Y, value);
                Point3DAnimation daZoom = new Point3DAnimation(newPosition, new Duration(TimeSpan.FromSeconds(1)));
                camera.BeginAnimation(PerspectiveCamera.PositionProperty, daZoom);
            }
        }


        #endregion
    }
}
