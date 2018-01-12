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

using System.IO;
using System.Collections.Generic;
using DotNetNuke.Entities.Modules;
using System;
using System.Web.UI.WebControls;
using System.Web;
using System.Data;
using DotNetNuke.Services.Search.Entities;
using DotNetNuke.Services.Localization;
using DotNetNuke.Common.Utilities;
using System.Web.UI.HtmlControls;
using DotNetNuke.Entities.Users;
using System.Xml;

namespace DBH.ModuleGenerator.Components
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for ModuleGenerator
    /// 
    /// The FeatureController class is defined as the BusinessController in the manifest file (.dnn)
    /// DotNetNuke will poll this class to find out which Interfaces the class implements. 
    /// 
    /// The IPortable interface is used to import/export content from a DNN module
    /// 
    /// The ISearchable interface is used by DNN to index the content of a module
    /// 
    /// The IUpgradeable interface allows module developers to execute code during the upgrade 
    /// process for a module.
    /// 
    /// Below you will find stubbed out implementations of each, uncomment and populate with your own data
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class FeatureController : ModuleSearchBase, IPortable, IUpgradeable
    {
        // feel free to remove any interfaces that you don't wish to use
        // (requires that you also update the .dnn manifest file)

        #region Private Members

        static string UserManual = "Module Generator User Manual.pdf";

        #endregion

        #region " Page Header Navigation "

        public static void PopulateMenu(int ModuleId, UserInfo objUser, HttpRequest Request, HtmlGenericControl NavigationBar)
        {
            if (!objUser.IsSuperUser)
                return;

            DotNetNuke.Entities.Modules.ModuleController modCtrl = new ModuleController();
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(@HttpContext.Current.Server.MapPath("~/DesktopModules/") + modCtrl.GetModule(ModuleId).DesktopModule.FolderName + "\\ModuleControl.xml");
            bool bFound = false;

            foreach (XmlNode node in xDocument.GetElementsByTagName("Field"))
            {
                HtmlGenericControl li = new HtmlGenericControl("li");
                if (Request.QueryString["ctl"] == node.Attributes["value"].Value || (string.IsNullOrEmpty(Request.QueryString["ctl"]) && !bFound))
                {
                    // Indicates a successful or positive action
                    li.Attributes.Add("class", "btn btn-success");
                    bFound = true;
                }
                else
                {
                    // Secondary, outline button
                    li.Attributes.Add("class", "btn btn-secondary");
                }

                NavigationBar.Controls.Add(li);

                //var currentUrl = Globals.NavigateURL(this.TabId, this.Request.QueryString["ctl"], UrlUtils.GetQSParamsForNavigateURL());

                HtmlGenericControl anchor = new HtmlGenericControl("a");
                if (string.IsNullOrEmpty(node.Attributes["value"].Value))
                {
                    anchor.Attributes.Add("href", DotNetNuke.Common.Globals.NavigateURL(node.Attributes["value"].Value));
                }
                else
                {
                    if (node.Attributes["value"].Value == "Manual")
                    {
                        String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                        String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/") + HttpContext.Current.Request.ApplicationPath;
                        // Setup link to open a new window
                        anchor.Attributes.Add("href", strUrl + "/DesktopModules/" + modCtrl.GetModule(ModuleId).DesktopModule.FolderName + "/Documentation/" + UserManual);
                        anchor.Attributes.Add("Target", "_blank");
                    }
                    else
                    {
                        anchor.Attributes.Add("href", DotNetNuke.Common.Globals.NavigateURL(node.Attributes["value"].Value, "mid=" + ModuleId.ToString()));
                    }
                }
                anchor.InnerText = node.Attributes["text"].Value;
                li.Controls.Add(anchor);
            }
        }

        #endregion

        #region Populate Dropdownlist

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Reading the names of folders and Binding the Radio Buttons
        /// Below is the method that reads the names of folders and then binds the same to the ASP.Net Radio Buttons Control
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static void LoadLanguages(int ModuleId, RadioButtonList optLanguage)
        {

            optLanguage.Items.Clear();
            DotNetNuke.Entities.Modules.ModuleController modCtrl = new ModuleController();
            string sModulePath = @HttpContext.Current.Server.MapPath("~/DesktopModules/") + modCtrl.GetModule(ModuleId).DesktopModule.FolderName + "\\";
            var moduleTemplatePath = sModulePath + "Templates";
            string[] folderList = Directory.GetDirectories(moduleTemplatePath);
            foreach (string folderPath in folderList)
            {
                optLanguage.Items.Add(new ListItem(Path.GetFileName(folderPath)));
            }
            optLanguage.SelectedIndex = 0;
        }

        public static void LoadReadMe(int ModuleId, DropDownList cboTemplate, RadioButtonList optLanguage, Label lblDescription)
        {
            DotNetNuke.Entities.Modules.ModuleController modCtrl = new ModuleController();
            string sModulePath = @HttpContext.Current.Server.MapPath("~/DesktopModules/") + modCtrl.GetModule(ModuleId).DesktopModule.FolderName + "\\";
            var readMePath = sModulePath + "Templates\\" + optLanguage.SelectedValue + "\\" + cboTemplate.SelectedItem.Value + "\\readme.txt";
            if (File.Exists(readMePath))
            {
                var readMe = Null.NullString;
                TextReader tr = new StreamReader(readMePath);
                readMe = tr.ReadToEnd();
                tr.Close();
                lblDescription.Text = readMe.Replace("\n", "<br/>");
            }
            else
            {
                lblDescription.Text = "";
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Reading the names of folders and Binding the Radio Buttons
        /// Below is the method that reads the names of folders and then binds the same to the ASP.Net Radio Buttons Control
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static void LoadModuleTemplates(int ModuleId, DropDownList cboTemplate, RadioButtonList optLanguage, Label lblDescription)
        {
            cboTemplate.Items.Clear();
            DotNetNuke.Entities.Modules.ModuleController modCtrl = new ModuleController();
            string sModulePath = @HttpContext.Current.Server.MapPath("~/DesktopModules/") + modCtrl.GetModule(ModuleId).DesktopModule.FolderName + "\\";
            var moduleTemplatePath = sModulePath + "Templates\\" + optLanguage.SelectedValue;
            string[] folderList = Directory.GetDirectories(moduleTemplatePath);
            foreach (string folderPath in folderList)
            {
                cboTemplate.Items.Add(new ListItem(Path.GetFileName(folderPath)));
            }
            cboTemplate.Items.Insert(0, new ListItem("<" + Localization.GetString("Not_Specified", Localization.SharedResourceFile) + ">", ""));
            //if (cboTemplate.Items.FindByText("Module - User Control") != null)
            //{
            //    cboTemplate.Items.FindByText("Module - User Control").Selected = true;
            //}
            //else
            //{
            //    cboTemplate.SelectedIndex = 0;
            //}
            LoadReadMe(ModuleId, cboTemplate, optLanguage, lblDescription);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Reading the XML file and Binding the DropDownList
        /// Below is the method that reads the XML file into a Dataset object and then binds the same to the ASP.Net DropDownList Control
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static void PopulateDDL(int ModuleId, DropDownList DDL, string XML_File)
        {
            DotNetNuke.Entities.Modules.ModuleController modCtrl = new ModuleController();
            string filePath = @HttpContext.Current.Server.MapPath("~/DesktopModules/") + modCtrl.GetModule(ModuleId).DesktopModule.FolderName + "\\" + XML_File;
            using (DataSet ds = new DataSet())
            {
                ds.ReadXml(filePath);
                DataTable dt = ds.Tables[0];
                DataColumn col = new DataColumn("Value", typeof(string));
                dt.Columns.Add(col);
                foreach (DataRow row in dt.Rows)
                {
                    row["Value"] = row["Department"].ToString()
+ "|" + row["OwnerFolder"].ToString()
+ "|" + row["RootNamespace"].ToString()
+ "|" + row["OwnerName"].ToString()
+ "|" + row["OwnerOrganization"].ToString()
+ "|" + row["OwnerWebsite"].ToString()
+ "|" + row["OwnerEmail"].ToString()
+ "|" + row["IconFile"].ToString();
                }
                DDL.DataSource = dt;
                DDL.DataTextField = "Department";
                DDL.DataValueField = "Value";
                DDL.DataBind();
            }
        }

        #endregion

        #region Optional Interfaces

        /// <summary>
        /// Gets the modified search documents for the DNN search engine indexer.
        /// </summary>
        /// <param name="moduleInfo">The module information.</param>
        /// <param name="beginDate">The begin date.</param>
        /// <returns></returns>
        public override IList<SearchDocument> GetModifiedSearchDocuments(ModuleInfo moduleInfo, DateTime beginDate)
        {
            var searchDocuments = new List<SearchDocument>();

            return searchDocuments;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ExportModule implements the IPortable ExportModule Interface
        /// </summary>
        /// <param name="moduleId">The Id of the module to be exported</param>
        /// -----------------------------------------------------------------------------
        public string ExportModule(int ModuleID)
        {
            throw new NotImplementedException();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ImportModule implements the IPortable ImportModule Interface
        /// </summary>
        /// <param name="moduleId">The Id of the module to be imported</param>
        /// <param name="content">The content to be imported</param>
        /// <param name="version">The version of the module to be imported</param>
        /// <param name="userId">The Id of the user performing the import</param>
        /// -----------------------------------------------------------------------------
        public void ImportModule(int ModuleID, string Content, string Version, int UserID)
        {
            throw new NotImplementedException();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpgradeModule implements the IUpgradeable Interface
        /// </summary>
        /// <param name="version">The current version of the module</param>
        /// -----------------------------------------------------------------------------
        public string UpgradeModule(string Version)
        {
            try
            {
                switch (Version)
                {
                    case "01.00.00":
                        // run your custom code here
                        return "success";
                    default:
                        return "success";
                }
            }
            catch
            {
                return "failure";
            }
        }


        #endregion

    }
}
