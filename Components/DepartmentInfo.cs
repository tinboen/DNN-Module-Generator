/*
' Copyright (c) 2018 Department of Beaches and Harbors
' All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;

namespace DBH.ModuleGenerator.Components
{

    ///<summary>
    /// The Location Info of where the object was created and gets displayed
    ///</summary>
    class DepartmentInfo
    {
        ///<summary>
        /// The string with the name of your object
        ///</summary>
        public string DepartmentName { get; set; }

        ///<summary>
        /// The string with the Abbreviation of your Module
        ///</summary>
        public string DepartmentValue { get; set; }

        ///<summary>
        /// The string with the Owner Folder of your Module
        ///</summary>
        public string OwnerFolder
        {
            get
            {
                if (DepartmentValue != "")
                    return DepartmentValue.Split('|')[0];
                else
                    return "DBH";
            }
        }

        ///<summary>
        /// The string with the Namespace of your Module
        ///</summary>
        public string ModuleNamespace
        {
            get
            {
                if (DepartmentValue != "")
                    return DepartmentValue.Split('|')[1];
                else
                    return "DBH";
            }
        }

        ///<summary>
        /// A string with the Owner Name of the Module
        ///</summary>
        public string OwnerName
        {
            get
            {
                if (DepartmentValue != "")
                    return DepartmentValue.Split('|')[2];
                else
                    return "Information Technology Section";
            }
        }


        ///<summary>
        /// A string with the Owner Organization of the module
        ///</summary>
        public string OwnerOrganization
        {
            get
            {
                if (DepartmentValue != "")
                    return DepartmentValue.Split('|')[3];
                else
                    return "Department of Beaches and Harbors";
            }
        }

        ///<summary>
        /// A string with the Owner Url of the module
        ///</summary>
        public string OwnerUrl
        {
            get
            {
                if (DepartmentValue != "")
                    return DepartmentValue.Split('|')[4];
                else
                    return "http://bh.lacounty.gov";
            }
        }


        ///<summary>
        /// A string with the Owner Email of the module
        ///</summary>
        public string OwnerEmail
        {
            get
            {
                if (DepartmentValue != "")
                    return DepartmentValue.Split('|')[5];
                else
                    return "dhoang@bh.lacounty.gov";
            }
        }


    }

}
