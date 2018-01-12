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
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Web;
using System.Web.UI;

using DotNetNuke;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
// using DotNetNuke.UI.Utilities;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Common;
using DotNetNuke.Entities.Users;
using DotNetNuke.Entities.Controllers;
using DotNetNuke.UI.Skins.Controls;
using DotNetNuke.Services.Log.EventLog;
using DotNetNuke.UI.Skins;
using DBH.ModuleGenerator.Components;
using System.Web.UI.HtmlControls;

namespace DBH.ModuleGenerator
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from PortalModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class CreateModule : PortalModuleBase
    {

        #region Private Members

        private string EditDepartment = "EditDepartment";

        #endregion

        #region Private Methods

        /// <summary>
        /// CreateNewModule runs to create new module
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        private bool CreateNewModule()
        {
            EventLogController objEventLog = new EventLogController();

            try
            {
                DotNetNuke.Entities.Modules.ModuleController modCtrl = new ModuleController();

                //setup information
                DepartmentInfo objInfo = new DepartmentInfo();
                objInfo.DepartmentValue = ddlDepartment.SelectedValue;
                objInfo.ModuleId = ModuleId;
                objInfo.ProjectFolder = txtProjectFolder.Text.Replace(" ", "");
                objInfo.ModuleName = txtModuleName.Text;
                objInfo.Language = optLanguage.SelectedValue;
                objInfo.Template = cboTemplate.SelectedValue;
                objInfo.ModuleFriendlyName = txtModuleFriendlyName.Text;
                objInfo.ModuleDescription = txtModuleDescription.Text;
                objInfo.ModuleAbbreviation = txtModuleAbbreviation.Text;

                //before create new module folder, check if folder exists
                if (!Directory.Exists(objInfo.ModuleGeneratorPath))
                {
                    Directory.CreateDirectory(objInfo.ModuleGeneratorPath);
                }
                else
                {
                    Skin.AddModuleMessage(this, "New module is not created, because folder " + objInfo.TargetFolder + " is Existed", ModuleMessage.ModuleMessageType.RedError);
                    return false;
                }

                // Zip all folders and files
                ZipFile.CreateFromDirectory(objInfo.ModuleTemplatePath, objInfo.ZipFilePath);

                // Extract all folders and Files
                ZipFile.ExtractToDirectory(objInfo.ZipFilePath, objInfo.ModuleGeneratorPath);

                // Delete the zip file
                File.Delete(objInfo.ZipFilePath);

                //objEventLog.AddLog("Processing Template Folder", objInfo.ModuleTemplatePath, PortalSettings, -1, EventLogController.EventLogType.HOST_ALERT);

                var controlName = Null.NullString;
                var fileName = Null.NullString;
                // var modulePath = "";
                var sourceCode = Null.NullString;

                //iterate through files in template folder
                string[] fileList = Directory.GetFiles(objInfo.ModuleGeneratorPath, "*", SearchOption.AllDirectories);
                foreach (string filePath in fileList)
                {
                    //open file
                    TextReader tr = new StreamReader(filePath);
                    sourceCode = tr.ReadToEnd();
                    tr.Close();

                    //replace tokens
                    sourceCode = sourceCode.Replace("$department$", objInfo.DepartmentName);
                    sourceCode = sourceCode.Replace("$safeprojectfolder$", objInfo.ProjectFolder);
                    sourceCode = sourceCode.Replace("$safeprojectname$", objInfo.ModuleName);
                    sourceCode = sourceCode.Replace("$ownerfolder$", lblOwnerFolder.Text);
                    sourceCode = sourceCode.Replace("$rootnamespace$", objInfo.RootNamespace);
                    sourceCode = sourceCode.Replace("$ownername$", objInfo.OwnerName);
                    sourceCode = sourceCode.Replace("$ownerorganization$", objInfo.OwnerOrganization);
                    sourceCode = sourceCode.Replace("$owneremail$", objInfo.OwnerEmail);
                    sourceCode = sourceCode.Replace("$ownerwebsite$", objInfo.OwnerWebsite);
                    sourceCode = sourceCode.Replace("$iconfile$", objInfo.IconFile);
                    sourceCode = sourceCode.Replace("$year$", DateTime.Now.Year.ToString());
                    sourceCode = sourceCode.Replace("$ModuleAbbreviation$", objInfo.ModuleAbbreviation);
                    sourceCode = sourceCode.Replace("$friendlyname$", objInfo.ModuleFriendlyName);
                    sourceCode = sourceCode.Replace("$moduledescription$", objInfo.ModuleDescription);

                    //create file
                    TextWriter tw = new StreamWriter(filePath);
                    tw.WriteLine(sourceCode);
                    tw.Close();

                    try
                    {
                        // If file name contains "DnnTemplate", then we must rename the file to the new module name
                        if (filePath.Contains("DnnTemplate"))
                            File.Move(filePath, filePath.Replace("DnnTemplate", objInfo.ProjectFolder));
                    }
                    catch (Exception exc)
                    {
                        Skin.AddModuleMessage(this, filePath + Environment.NewLine + exc.ToString(), ModuleMessage.ModuleMessageType.RedError);
                    }

                }

                return true;
            }
            catch (Exception exc)
            {
                Exceptions.LogException(exc);
                Skin.AddModuleMessage(this, exc.ToString(), ModuleMessage.ModuleMessageType.RedError);
                return false;
            }

        }



        #endregion

        #region Event Handlers

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Init runs when the control is initialised
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            try
            {
                //optLanguage.SelectedIndexChanged += optLanguage_SelectedIndexChanged;
                //cboTemplate.SelectedIndexChanged += cboTemplate_SelectedIndexChanged;
                //cmdCreate.Click += cmdCreate_Click;

                //JavaScript.RequestRegistration(CommonJs.jQuery);

                //ClientResourceManager.RegisterScript(Page, "~/desktopmodules/DBH.ModuleGenerator/scripts/jquery.Manager.js");

                //DetailView.ItemDataBound += RepeaterItemDataBound;

                ////Save User Preferences
                //SavePersonalizedSettings();
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// OnLoad runs when this program start is clicked
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!UserInfo.IsSuperUser)
            {
                Skin.AddModuleMessage(this, Localization.GetString("SuperUser.ErrorMessage", LocalResourceFile), ModuleMessage.ModuleMessageType.YellowWarning);

                // Hide command button
                cmdCreate.Visible = false;

                return;
            }
            try
            {
                // Populate navigation Bar
                FeatureController.PopulateMenu(ModuleId, UserInfo, this.Request, NavigationBar);

                if (!Page.IsPostBack)
                {
                    // Populate drop down list, radio button list
                    FeatureController.PopulateDDL(ModuleId, ddlDepartment, "DepartmentList.xml");
                    FeatureController.LoadLanguages(ModuleId, optLanguage);
                    FeatureController.LoadModuleTemplates(ModuleId, cboTemplate, optLanguage, lblDescription);

                    if (Settings.ContainsKey("Department"))
                    {
                        ddlDepartment.Items.FindByText(Settings["Department"].ToString()).Selected = true;
                        ddlDepartment_SelectedIndexChanged(null, null);
                    }
                    if (Settings.ContainsKey("Language"))
                    {
                        optLanguage.SelectedIndex = optLanguage.Items.IndexOf(optLanguage.Items.FindByText(Settings["Language"].ToString()));
                        optLanguage_SelectedIndexChanged(null, null);
                    }
                    if (Settings.ContainsKey("Template"))
                    {
                        cboTemplate.SelectedIndex = cboTemplate.Items.IndexOf(cboTemplate.Items.FindByText(Settings["Template"].ToString()));
                        cboTemplate_SelectedIndexChanged(null, null);
                    }

                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }

        }

        #endregion

        #region "Optional Interfaces"

        protected void optLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FeatureController.LoadModuleTemplates(ModuleId, cboTemplate, optLanguage, lblDescription);
        }

        protected void cboTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            FeatureController.LoadReadMe(ModuleId, cboTemplate, optLanguage, lblDescription);
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlDepartment.SelectedValue != "")
                {
                    //Setup department
                    DepartmentInfo objInfo = new DepartmentInfo();
                    objInfo.DepartmentValue = ddlDepartment.SelectedValue;

                    lblOwnerFolder.Text = objInfo.OwnerFolder;
                    lblRootNamespace.Text = objInfo.RootNamespace;
                    lblOwnerName.Text = objInfo.OwnerName;
                    lblOwnerOrganization.Text = objInfo.OwnerOrganization;
                    lblOwnerWebsite.Text = objInfo.OwnerWebsite;
                    lblOwnerEmail.Text = objInfo.OwnerEmail;
                    imgIconFile.ImageUrl = objInfo.IconFile;
                    imgIconFile.AlternateText = objInfo.IconFile;
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// cmdCreate_Click runs when the Create button is clicked
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        protected void cmdCreate_Click(System.Object sender, System.EventArgs e)
        {
            if (UserInfo.IsSuperUser)
            {
                if (!String.IsNullOrEmpty(lblOwnerName.Text) && !String.IsNullOrEmpty(txtProjectFolder.Text) && cboTemplate.SelectedIndex > 0)
                {
                    if (CreateNewModule())
                    {
                        var modules = new ModuleController();
                        modules.UpdateModuleSetting(ModuleId, "Department", ddlDepartment.SelectedItem.Text);
                        modules.UpdateModuleSetting(ModuleId, "Language", optLanguage.SelectedValue);
                        modules.UpdateModuleSetting(ModuleId, "Template", cboTemplate.SelectedValue);

                        Skin.AddModuleMessage(this, Localization.GetString("ModuleGenerator.CompleteMessage", LocalResourceFile), ModuleMessage.ModuleMessageType.GreenSuccess);

                        //Response.Redirect(Globals.NavigateURL(), true);
                    }
                }
                else
                {
                    Skin.AddModuleMessage(this, Localization.GetString("InputValidation.ErrorMessage", LocalResourceFile), ModuleMessage.ModuleMessageType.YellowWarning);
                }
            }
        }

        #endregion

        #region "Optional Interfaces"

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///   ModuleActions is an interface property that returns the module actions collection for the module
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public ModuleActionCollection ModuleActions
        {
            get
            {
                // add the Edit Text action
                var Actions = new ModuleActionCollection();
                Actions.Add(GetNextActionID(), Localization.GetString("cmdEditDepartment", this.LocalResourceFile), EditDepartment, "", "", this.EditUrl("EditDepartment"), false, SecurityAccessLevel.Edit, true, false);

                return Actions;
            }
        }

        #endregion

    }
}