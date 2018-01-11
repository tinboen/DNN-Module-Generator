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
using DotNetNuke.Common;
using DotNetNuke.Entities.Controllers;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Content.Common;
using DotNetNuke.Entities.Content;
using DotNetNuke.Entities.Content.Taxonomy;
using DotNetNuke.Services.Installer.Packages;
using System;

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
    public class FeatureController : IUpgradeable
    {
        // feel free to remove any interfaces that you don't wish to use
        // (requires that you also update the .dnn manifest file)

        #region Optional Interfaces

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpgradeModule implements the IUpgradeable Interface
        /// </summary>
        /// <param name="version">The current version of the module</param>
        /// -----------------------------------------------------------------------------
        public string UpgradeModule(string Version)
        {
            ////Fix icon and add "Development" to the DesktopModule taxonomy and associate it with this module
            //if (Version == "01.00.00")
            //{
            //    var vocabularyId = -1;
            //    var termId = -1;
            //    var objTermController = DotNetNuke.Entities.Content.Common.Util.GetTermController();
            //    var objTerms = objTermController.GetTermsByVocabulary("Module_Categories");
            //    foreach (Term term in objTerms)
            //    {
            //        vocabularyId = term.VocabularyId;
            //        if (term.Name == "Development")
            //        {
            //            termId = term.TermId;
            //        }
            //    }
            //    if (termId == -1)
            //    {
            //        termId = objTermController.AddTerm(new Term(vocabularyId) { Name = "Development" });
            //    }
            //    var objTerm = objTermController.GetTerm(termId);

            //    var portalID = -1;
            //    Dictionary<string, string> HostSettings = HostController.Instance.GetSettingsDictionary();
            //    if (HostSettings.ContainsKey("HostPortalId"))
            //    {
            //        portalID = int.Parse(HostSettings["HostPortalId"]);
            //    }
            //    if (portalID != -1)
            //    {
            //        var objDesktopModule = DesktopModuleController.GetDesktopModuleByModuleName("DBH.ModuleGenerator", portalID);
            //        var objPackage = PackageController.GetPackage(objDesktopModule.PackageID);
            //        objPackage.IconFile = "~/DesktopModules/DBH/ModuleGenerator/Images/icon.png";
            //        PackageController.SavePackage(objPackage);

            //        var objContentController = DotNetNuke.Entities.Content.Common.Util.GetContentController();
            //        var objContent = objContentController.GetContentItem(objDesktopModule.ContentItemId);
            //        objTermController.AddTermToContent(objTerm, objContent);
            //    }
            //}

            return Version;
        }

        #endregion
    }
}
