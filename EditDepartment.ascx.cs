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
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.Web;

using DotNetNuke;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Common;
using DotNetNuke.Entities.Users;
using DotNetNuke.UI.Skins;
using DotNetNuke.UI.Skins.Controls;
using DBH.ModuleGenerator.Components;

namespace DBH.ModuleGenerator
{
    /// -----------------------------------------------------------------------------
    /// <summary>   
    /// The Edit class is used to manage content
    /// 
    /// Typically your edit control would be used to create new content, or edit existing content within your module.
    /// The ControlKey for this control is "Edit", and is defined in the manifest (.dnn) file.
    /// 
    /// Because the control inherits from PortalModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class EditDepartment : PortalModuleBase
    {
        #region Private Members

        private string strXMLfile = Null.NullString;
        private string ParentPage = "";

        #endregion

        #region BoundFields with Paging and Sorting

        protected void gvDepartment_PreRender(object sender, EventArgs e)
        {
            GridView gv = (GridView)sender;

            if ((gv.ShowHeader == true && gv.Rows.Count > 0) || (gv.ShowHeaderWhenEmpty == true))
            {
                //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
                gv.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            if (gv.ShowFooter == true && gv.Rows.Count > 0)
            {
                //Force GridView to use <tfoot> instead of <tbody> - 11/03/2013 - MCR.
                gv.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        #endregion

        #region Gidview Procedures

        protected void BindGrid()
        {
            DataSet ds = new DataSet();
            ds.ReadXml(strXMLfile);
            gvDepartment.DataSource = ds;
            gvDepartment.DataBind();
        }

        protected void gvDepartment_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            ////Retrieve the table from the session object.
            //DataTable dt = (DataTable)Session["TaskTable"];

            ////Update the values.
            //GridViewRow row = gvDepartment.Rows[e.RowIndex];
            //dt.Rows[row.DataItemIndex]["text"] = ((TextBox)(row.Cells[1].Controls[0])).Text;
            //dt.Rows[row.DataItemIndex]["value"] = ((TextBox)(row.Cells[2].Controls[0])).Text;
            //dt.Rows[row.DataItemIndex]["level"] = ((CheckBox)(row.Cells[3].Controls[0])).Checked;

            ////Reset the edit index.
            //gvDepartment.EditIndex = -1;

            int i = gvDepartment.Rows[e.RowIndex].DataItemIndex;
            GridViewRow row = gvDepartment.Rows[e.RowIndex];
            string strDepartment = ((TextBox)(row.Cells[2].Controls[0])).Text;
            string strOwnerFolder = ((TextBox)(row.Cells[3].Controls[0])).Text;
            string strRootNamespace = ((TextBox)(row.Cells[4].Controls[0])).Text;
            string strOwnerName = ((TextBox)(row.Cells[5].Controls[0])).Text;
            string strOwnerOrganization = ((TextBox)(row.Cells[6].Controls[0])).Text;
            string strOwnerWebsite = ((TextBox)(row.Cells[7].Controls[0])).Text;
            string strOwnerEmail = ((TextBox)(row.Cells[8].Controls[0])).Text;
            string strIconFile = ((TextBox)(row.Cells[9].Controls[0])).Text;

            //Reset the edit index.
            gvDepartment.EditIndex = -1;

            //Bind data to the GridView control.
            BindGrid();

            DataSet oDs = (DataSet)gvDepartment.DataSource;

            oDs.Tables[0].Rows[i]["Department"] = strDepartment;
            oDs.Tables[0].Rows[i]["OwnerFolder"] = strOwnerFolder;
            oDs.Tables[0].Rows[i]["RootNamespace"] = strRootNamespace;
            oDs.Tables[0].Rows[i]["OwnerName"] = strOwnerName;
            oDs.Tables[0].Rows[i]["OwnerOrganization"] = strOwnerOrganization;
            oDs.Tables[0].Rows[i]["OwnerWebsite"] = strOwnerWebsite;
            oDs.Tables[0].Rows[i]["OwnerEmail"] = strOwnerEmail;
            oDs.Tables[0].Rows[i]["IconFile"] = strIconFile;
            
            oDs.WriteXml(strXMLfile);

            //Bind data to the GridView control.
            BindGrid();

        }

        protected void gvDepartment_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            gvDepartment.EditIndex = -1;

            //Bind data to the GridView control.
            BindGrid();
        }

        protected void gvDepartment_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            //Bind data to the GridView control.
            BindGrid();

            DataSet oDs = (DataSet) gvDepartment.DataSource;
            oDs.Tables[0].Rows[gvDepartment.Rows[e.RowIndex].DataItemIndex].Delete();
            oDs.WriteXml(strXMLfile);

            //Bind data to the GridView control.
            BindGrid();
        }

        protected void gvDepartment_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvDepartment.EditIndex = e.NewEditIndex;

            //Bind data to the GridView control.
            BindGrid();
        }

        #endregion

        #region "Optional Command Interfaces"

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// cmdAddNewRecord_Click redirect to add new record dialog when the add new record button is clicked
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        protected void cmdAddNewRecord_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDepartment.Text))
                {
                    BindGrid();
                    DataSet oDs = (DataSet)gvDepartment.DataSource;
                    DataRow oDr = oDs.Tables[0].NewRow();
                    oDr["Department"] = txtDepartment.Text;
                    oDr["OwnerFolder"] = txtOwnerFolder.Text;
                    oDr["RootNamespace"] = txtRootNamespace.Text;
                    oDr["OwnerName"] = txtOwnerName.Text;
                    oDr["OwnerOrganization"] = txtOwnerOrganization.Text;
                    oDr["OwnerWebsite"] = txtOwnerWebsite.Text;
                    oDr["OwnerEmail"] = txtOwnerEmail.Text;
                    oDr["IconFile"] = txtIconFile.Text;
                    
                    oDs.Tables[0].Rows.Add(oDr);
                    oDs.WriteXml(strXMLfile);
                    BindGrid();
                }
                else
                {
                    Skin.AddModuleMessage(this, Localization.GetString("Department.ErrorMessage", LocalResourceFile), ModuleMessage.ModuleMessageType.YellowWarning);
                }

                // return to main page
                //Response.Redirect(Globals.NavigateURL(AddNewRecord, "COLUMN_NAME=" + ddlCOLUMN_NAME.SelectedValue, "mid=" + ModuleId.ToString()));
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion

        #region "Events"

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// OnInit runs when this program start is clicked
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

            //If user not in Access Roles or Super User, then we will redirect to Access Denied page
            if (!UserInfo.IsSuperUser)
            {
                Skin.AddModuleMessage(this, Localization.GetString("SuperUser.ErrorMessage", LocalResourceFile), ModuleMessage.ModuleMessageType.YellowWarning);
                //cmdCreate.Visible = false;
                return;
            }

            try
            {
                // Populate navigation Bar
                FeatureController.PopulateMenu(ModuleId, UserInfo, this.Request, NavigationBar);

                // setup the strXMLFile for global access
                ModuleController modCtrl = new ModuleController();
                strXMLfile = @HttpContext.Current.Server.MapPath("~/DesktopModules/") + modCtrl.GetModule(ModuleId).DesktopModule.FolderName + "\\DepartmentList.xml";

                if (!IsPostBack)
                {
                    //Bind data to the GridView control.
                    BindGrid();
                }
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
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
                Actions.Add(GetNextActionID(), Localization.GetString("EditLookupMnu", this.LocalResourceFile), "EditLookupMnu", "", "", this.EditUrl("EditLookupMnu"), false, SecurityAccessLevel.Edit, true, false);

                return Actions;
            }
        }

        #endregion

    }
}
