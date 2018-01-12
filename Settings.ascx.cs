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
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DBH.ModuleGenerator.Components;
using System.Web.UI.WebControls;
using System.Data;
using System.Web;

namespace DBH.ModuleGenerator
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Settings class manages Module Settings
    /// 
    /// Typically your settings control would be used to manage settings for your module.
    /// There are two types of settings, ModuleSettings, and TabModuleSettings.
    /// 
    /// ModuleSettings apply to all "copies" of a module on a site, no matter which page the module is on. 
    /// 
    /// TabModuleSettings apply only to the current module on the current page, if you copy that module to
    /// another page the settings are not transferred.
    /// 
    /// If you happen to save both TabModuleSettings and ModuleSettings, TabModuleSettings overrides ModuleSettings.
    /// 
    /// Below we have some examples of how to access these settings but you will need to uncomment to use.
    /// 
    /// Because the control inherits from ModuleGeneratorSettingsBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Settings : ModuleSettingsBase
    {
        #region Base Method Implementations

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// LoadSettings loads the settings from the Database and displays them
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void LoadSettings()
        {
            try
            {
                // Populate drop down list, radio button list
                FeatureController.PopulateDDL(ModuleId, ddlDepartment, "DepartmentList.xml");
                FeatureController.LoadLanguages(ModuleId, optLanguage);
                FeatureController.LoadModuleTemplates(ModuleId, cboTemplate, optLanguage, lblDescription);

                if (Page.IsPostBack == false)
                {
                    //Check for existing settings and use those on this page
                    if (Settings.Contains("Department"))
                        ddlDepartment.Items.FindByText(Settings["Department"].ToString()).Selected = true;

                    if (Settings.Contains("Language"))
                        optLanguage.SelectedIndex = optLanguage.Items.IndexOf(optLanguage.Items.FindByText(Settings["Language"].ToString()));

                    if (Settings.Contains("Template"))
                        cboTemplate.SelectedIndex = cboTemplate.Items.IndexOf(cboTemplate.Items.FindByText(Settings["Template"].ToString()));
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }

        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpdateSettings saves the modified settings to the Database
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void UpdateSettings()
        {
            try
            {
                var modules = new ModuleController();

                //the following are two sample Module Settings, using the text boxes that are commented out in the ASCX file.
                modules.UpdateModuleSetting(ModuleId, "Department", ddlDepartment.SelectedItem.Text);
                modules.UpdateModuleSetting(ModuleId, "Language", optLanguage.SelectedValue);
                modules.UpdateModuleSetting(ModuleId, "Template", cboTemplate.SelectedValue);

                //tab module settings
                //modules.UpdateTabModuleSetting(TabModuleId, "Department",  txtDepartment.Text);
                //modules.UpdateTabModuleSetting(TabModuleId, "Language",  txtLanguage.Text);
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion

        #region Private Methods

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Reading the XML file and Binding the DropDownList
        /// Below is the method that reads the XML file into a Dataset object and then binds the same to the ASP.Net DropDownList Control
        /// </summary>
        /// -----------------------------------------------------------------------------
        private void PopulateDDL(DropDownList DDL, string XML_File)
        {
            DotNetNuke.Entities.Modules.ModuleController modCtrl = new ModuleController();
            string filePath = @HttpContext.Current.Server.MapPath("~/DesktopModules/") + modCtrl.GetModule(ModuleId).DesktopModule.FolderName + "\\" + XML_File;
            using (DataSet ds = new DataSet())
            {
                ds.ReadXml(filePath);
                DDL.DataSource = ds;
                DDL.DataTextField = "name";
                DDL.DataValueField = "value";
                DDL.DataBind();
            }
        }

        #endregion
    }
}