using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using System.Xml;
using System.IO;

namespace BannerSolution.Features.Feature1
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("5012a2c4-9583-4582-9cc3-09af3135b5ec")]
    public class Feature1EventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            try
            {
                SPSite site = properties.Feature.Parent as SPSite;
                if (File.Exists(Constant.configPath))
                {
                    XmlDocument xml = new XmlDocument();
                    xml.Load(Constant.configPath);
                    XmlNode root = xml.SelectSingleNode("root");
                    string urlNode = root.SelectSingleNode("Site").Attributes[0].InnerText;

                    using (SPSite site1 = new SPSite(urlNode))
                    {
                        using (SPWeb web = site1.RootWeb)
                        {
                            if (web.ID == site.RootWeb.ID)
                            {
                                //DealWithDescriptionList(web);
                                //DealWithAssignmentList(web);
                            }
                            else
                            {
                                DealWithList(site.RootWeb, Constant.BannerDescriptionTitle);
                                DealWithList(site.RootWeb, Constant.BannerAssignmentTitle);
                            }
                        }
                    }
                }
                APPSLogger.Logger(Microsoft.SharePoint.Administration.TraceSeverity.Verbose, "Active banner solution for sp2010 successful");
            }
            catch (Exception e)
            {
                APPSLogger.Logger(Microsoft.SharePoint.Administration.TraceSeverity.High, string.Format("ACSSOLUTION0001 Exception: {0}", e.ToString()));
            }
        }

        private void DealWithDescriptionList(SPWeb web)
        {
            try
            {
                SPList list = web.Lists[Constant.BannerDescriptionTitle];
                AddColumn(list);
                AddColumnToDefaultView(list);
                DisableTitleColumn(list);
            }
            catch (Exception e)
            {
                APPSLogger.Logger(Microsoft.SharePoint.Administration.TraceSeverity.High, string.Format("ACSSOLUTION0002 Exception: {0}", e.ToString()));
            }
        }

        private void DisableTitleColumn(SPList list)
        {
            try
            {
                SPView view = list.DefaultView;

                SPField field = list.Fields.GetField("Title");
                if (field.Required)
                {
                    field.Required = false;
                    field.ShowInNewForm = false;
                    field.ShowInEditForm = false;
                    field.ShowInDisplayForm = false;
                    field.Update();
                }
            }
            catch (Exception e)
            {
                APPSLogger.Logger(Microsoft.SharePoint.Administration.TraceSeverity.High, string.Format("ACSSOLUTION0003 Exception: {0}", e.ToString()));
            }
        }

        private void DealWithAssignmentList(SPWeb web)
        {
            try
            {
                SPList list = web.Lists[Constant.BannerAssignmentTitle];
                AddColumn(list);
                AddColumnToDefaultView(list);
                DisableTitleColumn(list);
                ChangeDisplayName(list, Constant.ACSSite, "Site Collection");
            }
            catch (Exception e)
            {
                APPSLogger.Logger(Microsoft.SharePoint.Administration.TraceSeverity.High, string.Format("ACSSOLUTION0004 Exception: {0}", e.ToString()));
            }
        }

        private void ChangeDisplayName(SPList list, string internalName, string newDisplayName)
        {
            SPField field = list.Fields.GetField(internalName);
            if (field != null)
            {
                field.Title = newDisplayName;
                field.Update();
            }
        }

        private void AddColumnToDefaultView(SPList list)
        {
            try
            {
                if (list.Title.Equals(Constant.BannerDescriptionTitle))
                {
                    AddColumnToView(Constant.ACSName, list);
                    AddColumnToView(Constant.ACSText, list);
                }
                else
                {
                    AddColumnToView(Constant.ACSSite, list);
                    AddColumnToView(Constant.ACSISArchived, list);
                    AddColumnToView(Constant.ACSBannerName, list);
                    AddColumnToView(Constant.ACSArg1, list);
                    AddColumnToView(Constant.ACSArg2, list);
                    AddColumnToView(Constant.ACSArg3, list);
                }
            }
            catch (Exception e)
            {
                APPSLogger.Logger(Microsoft.SharePoint.Administration.TraceSeverity.High, string.Format("ACSSOLUTION0005 Exception: {0}", e.ToString()));
            }
        }
        private void AddColumn(SPList list)
        {
            try
            {
                if (list.Title.Equals(Constant.BannerDescriptionTitle))
                {
                    AddColumnToList(Constant.ACSName, list);
                    AddColumnToList(Constant.ACSText, list);

                }
                else
                {
                    AddColumnToList(Constant.ACSSite, list);
                    AddColumnToList(Constant.ACSISArchived, list);
                    AddColumnToList(Constant.ACSBannerName, list);
                    AddColumnToList(Constant.ACSArg1, list);
                    AddColumnToList(Constant.ACSArg2, list);
                    AddColumnToList(Constant.ACSArg3, list);
                }
            }
            catch (Exception e)
            {
                APPSLogger.Logger(Microsoft.SharePoint.Administration.TraceSeverity.High, string.Format("ACSSOLUTION0006 Exception: {0}", e.ToString()));
            }
        }
        private void AddColumnToList(string fieldName, SPList list)
        {
            SPField field = list.ParentWeb.Fields.GetField(fieldName);
            if (field != null && !list.Fields.ContainsField(field.InternalName))
            {
                list.Fields.Add(field);
            }
        }
        private void AddColumnToView(string fieldName, SPList list)
        {
            SPField field = list.Fields.GetField(fieldName);
            SPView view = list.DefaultView;
            if (field != null && !list.DefaultView.ViewFields.Exists(field.InternalName))
            {
                view.ViewFields.Add(field);
                view.Update();
            }
        }
        private void DealWithList(SPWeb web, string listTitle)
        {
            try
            {
                SPList list = web.Lists[listTitle];
                list.Delete();
            }
            catch (Exception e)
            {
                APPSLogger.Logger(Microsoft.SharePoint.Administration.TraceSeverity.High, string.Format("ACSSOLUTION0007 Exception: {0}", e.ToString()));
            }
        }
        // Uncomment the method below to handle the event raised before a feature is deactivated.

        //public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
