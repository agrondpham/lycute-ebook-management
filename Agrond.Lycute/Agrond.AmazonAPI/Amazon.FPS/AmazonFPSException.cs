/******************************************************************************* 
 *  Copyright 2008-2010 Amazon Technologies, Inc.
 *  Licensed under the Apache License, Version 2.0 (the "License"); 
 *  
 *  You may not use this file except in compliance with the License. 
 *  You may obtain a copy of the License at: http://aws.amazon.com/apache2.0
 *  This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
 *  CONDITIONS OF ANY KIND, either express or implied. See the License for the 
 *  specific language governing permissions and limitations under the License.
 * ***************************************************************************** 
 *    __  _    _  ___ 
 *   (  )( \/\/ )/ __)
 *   /__\ \    / \__ \
 *  (_)(_) \/\/  (___/
 * 
 *  Amazon FPS CSharp Library
 * 
 */

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Net;

namespace Amazon.FPS
{
    /// <summary>
    /// Amazon FPS  Exception provides details of errors 
    /// returned by Amazon FPS  service
    /// </summary>
    public class AmazonFPSException : Exception 
    {
    
        private String message = null;
        
        /// <summary>
        /// Constructs AmazonFPSException with message
        /// </summary>
        /// <param name="message">Overview of error</param>
        public AmazonFPSException(String message) {
            this.message = message;
        }

        /// <summary>
        /// Gets error message
        /// </summary>
        public override String Message
        {
            get { return this.message; }
        }
    }
}
