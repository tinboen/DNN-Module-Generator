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
        public string DepartmentName
        {
            get
            {
                if (DepartmentValue != "")
                    return DepartmentValue.Split('|')[0];
                else
                    return "Department of Beaches and Harbors";
            }
        }

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
                    return DepartmentValue.Split('|')[1].Replace(" ", "");
                else
                    return "DBH";
            }
        }

        ///<summary>
        /// The string with the Namespace of your Module
        ///</summary>
        public string RootNamespace
        {
            get
            {
                if (DepartmentValue != "")
                    return DepartmentValue.Split('|')[2].Replace(" ", "");
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
                    return DepartmentValue.Split('|')[3];
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
                    return DepartmentValue.Split('|')[4];
                else
                    return "Department of Beaches and Harbors";
            }
        }

        ///<summary>
        /// A string with the Owner Url of the module
        ///</summary>
        public string OwnerWebsite
        {
            get
            {
                if (DepartmentValue != "")
                    return DepartmentValue.Split('|')[5];
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
                    return DepartmentValue.Split('|')[6];
                else
                    return "dhoang@bh.lacounty.gov";
            }
        }

        ///<summary>
        /// A string with the URL of the extension icon of the department module
        ///</summary>
        public string IconFile
        {
            get
            {
                if (DepartmentValue != "")
                    return DepartmentValue.Split('|')[7];
                else
                    return "~/images/DBH_Extension.gif";
            }
        }
        
        ///<summary>
        /// The current ModuleId of where the template was stored
        ///</summary>
        public int ModuleId { get; set; }

        ///<summary>
        /// The string with the Language of your new Module
        ///</summary>
        public string Language { get; set; }

        ///<summary>
        /// The string with the Template of your new Module
        ///</summary>
        public string Template { get; set; }

        ///<summary>
        /// The string with the new Project Folder of your object, will be used as module folder
        ///</summary>
        public string ProjectFolder { get; set; }

        ///<summary>
        /// The string with the new Module name of your object
        ///</summary>
        public string ModuleName { get; set; }

        ///<summary>
        /// The string with the new Module Friendly name of your object
        ///</summary>
        public string ModuleFriendlyName { get; set; }

        ///<summary>
        /// The string with the new Module Description name of your object
        ///</summary>
        public string ModuleDescription { get; set; }

        ///<summary>
        /// The string with the new Module Abbreviation name of your object
        ///</summary>
        public string ModuleAbbreviation { get; set; }

        

        ///<summary>
        /// The Desktop Module folder
        ///</summary>
        public string ModulePath
        {
            get
            {
                DotNetNuke.Entities.Modules.ModuleController modCtrl = new DotNetNuke.Entities.Modules.ModuleController();
                if (ModuleId > 0)
                    return System.Web.HttpContext.Current.Server.MapPath("~/DesktopModules/") + modCtrl.GetModule(ModuleId).DesktopModule.FolderName.Replace("/", "\\") + "\\";
                else
                    return "";
            }
        }
        
        ///<summary>
        /// The Module Template Path of where the template was stored
        ///</summary>
        public string ModuleTemplatePath
        {
            get
            {
                return ModulePath + "Templates\\" + Language + "\\" + Template + "\\";
            }
        }

        ///<summary>
        /// The Zip Folder Path of where the Zip file was stored
        ///</summary>
        public string ZipPath
        {
            get
            {
                return ModulePath + "Templates\\Zip\\";
            }
        }

        ///<summary>
        /// The full Zip File Path of where the Zip file 
        ///</summary>
        public string ZipFilePath
        {
            get
            {
                return ModulePath + "Templates\\Zip\\" + ProjectFolder;
            }
        }

        ///<summary>
        /// The Target Folder with the new module will be created
        ///</summary>
        public string TargetFolder
        {
            get
            {
                if (ProjectFolder != "")
                    return ".\\" + OwnerFolder + "\\" + ProjectFolder;
                else
                    return "";
            }
        }

        ///<summary>
        /// The Module Generator Path with the new module will be created
        ///</summary>
        public string ModuleGeneratorPath
        {
            get
            {
                DotNetNuke.Entities.Modules.ModuleController modCtrl = new DotNetNuke.Entities.Modules.ModuleController();
                if (ProjectFolder != "")
                    return System.Web.HttpContext.Current.Server.MapPath("~/DesktopModules/") + OwnerFolder + "\\" + ProjectFolder + "\\";
                else
                    return "";
            }
        }



    }

}
