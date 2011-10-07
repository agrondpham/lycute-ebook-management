using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows;

namespace Agrond._3DWallLayout
{
    public class _3DWallLayout
    {
        public static Viewport2DVisual3D CreateViewPort2D(int pAngle,Point3DCollection pPossition)//Viewport2DVisual3D viewport2D)
        {
            Viewport2DVisual3D viewport2D = new Viewport2DVisual3D();
            viewport2D.Transform = new RotateTransform3D
            {
                Rotation = new AxisAngleRotation3D
                {
                    Angle = pAngle,
                    Axis = new Vector3D(0, 1, 0)
                }
            };

            viewport2D.Geometry = CreateMeshGeometry3D(pPossition);

            viewport2D.Material = CreateMaterial();

            return viewport2D;
        }
        public static MeshGeometry3D CreateMeshGeometry3D(Point3DCollection pPossition)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions = pPossition;
                            //new Point3DCollection {       
                            //new Point3D(0,0,-10), 
                            //new Point3D(2,0,-10), 
                            //new Point3D(2,3,-10), 
                            //new Point3D(0,3,-10) 
               // };
            mesh.TriangleIndices = new Int32Collection(new int[] { 0, 1, 2, 0, 2, 3 });
            mesh.TextureCoordinates = new PointCollection(new Point[]{
                    new Point(0,1),
                    new Point(1,1),
                    new Point(1,0),
                    new Point(0,0)
                });

            return mesh;
        }
        public static Material CreateMaterial()
        {
            DiffuseMaterial material = new DiffuseMaterial { Brush = Brushes.White };
            Viewport2DVisual3D.SetIsVisualHostMaterial(material, true);
            return material;
        }
        //public 
    }
}
