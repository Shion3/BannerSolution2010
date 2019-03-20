using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;

namespace BannerSolution.ControlTemplates.BannerSolution
{
    public partial class MessageControl : UserControl
    {
        public double Percent { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public string StatusBlock { get; set; }

        public string ProgressColor { get; set; }
        public string LastStatus { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    if (File.Exists(Constant.configPath))
                    {
                        XmlDocument xml = new XmlDocument();
                        xml.Load(Constant.configPath);
                        XmlNode root = xml.SelectSingleNode("root");
                        string urlNode = root.SelectSingleNode("Site").Attributes[0].InnerText;
                        using (SPSite site = new SPSite(urlNode))
                        {
                            using (SPWeb web = site.RootWeb)
                            {
                                SPList descriptionList = web.Lists[Constant.BannerDescriptionTitle];
                                SPList assignmentList = web.Lists[Constant.BannerAssignmentTitle];

                                ProgressColor = root.SelectSingleNode("ProgressColor").Attributes[0].InnerText;
                                string currentSite = SPContext.Current.Site.RootWeb.Url;
                                List<string> statusList = GetStatusList(root, assignmentList, currentSite);
                                SPQuery q = new SPQuery();
                                q.Query = "<Where><And><Eq><FieldRef Name='" + Constant.ACSSite + "'></FieldRef><Value Type='Text'>" + currentSite + "</Value></Eq><Eq><FieldRef Name='" + Constant.ACSISArchived + "'></FieldRef><Value Type='Bool'>True</Value></Eq></And></Where>";
                                SPListItemCollection items = assignmentList.GetItems(q);
                                if (items.Count != 0)
                                {
                                    SPListItem item = items[0];
                                    Description = GetMessage(item, descriptionList);
                                    RetrieveCurrentStatus(statusList, item);
                                }
                                else
                                {
                                    Description = string.Empty;
                                    APPSLogger.Logger(Microsoft.SharePoint.Administration.TraceSeverity.Verbose, string.Format("Can't find item in {0}. Site url:{1}", Constant.BannerAssignmentTitle, currentSite));
                                }
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                APPSLogger.Logger(Microsoft.SharePoint.Administration.TraceSeverity.High, string.Format("ACSSOLUTION0011 Exception: {0}", ex.ToString()));
            }
        }

        private void RetrieveCurrentStatus(List<string> statusList, SPListItem item)
        {
            State = item[Constant.ACSBannerName].ToString();
            int stateInx = statusList.IndexOf(State);
            Percent = Convert.ToDouble(stateInx) / Convert.ToDouble(statusList.Count - 1) * 100;
        }
        private List<string> GetStatusList(XmlNode root, SPList list, string siteUrl)
        {
            List<string> statusList = new List<string>();
            List<string> Status = new List<string>();
            foreach (XmlNode node in root.SelectSingleNode("Status").ChildNodes)
            {
                string str = node.InnerText;
                Status.Add(str);
            }
            RenderStatus(Status);
            return Status;
        }
        private void RenderStatus(List<string> statusList)
        {
            StatusBlock = "";
            var style = string.Format("width:{0}%;", 100 / (statusList.Count - 1));
            foreach (string str in statusList)
            {
                if (statusList.IndexOf(str) == statusList.Count - 1)
                {
                    LastStatus = str;
                    break;
                }
                string stateBlock = string.Format("<td class='ACSProgressTitle' style='{0}'><span>{1}</span></td>", style, str);
                StatusBlock = StatusBlock + stateBlock;
            }
        }

        private string GetMessage(SPListItem item, SPList list)
        {
            try
            {
                SPQuery query = new SPQuery();
                if (item[Constant.ACSBannerName] == null)
                {
                    return string.Empty;
                }
                query.Query = "<Where><Eq><FieldRef Name='" + Constant.ACSName + "'></FieldRef><Value Type='Text'>" + item[Constant.ACSBannerName] + "</Value></Eq></Where>";
                SPListItemCollection items = list.GetItems(query);
                if (items.Count != 0)
                {
                    SPListItem result = items[0];
                    string arg1 = item[Constant.ACSArg1] != null ? item[Constant.ACSArg1].ToString() : string.Empty;
                    string arg2 = item[Constant.ACSArg2] != null ? item[Constant.ACSArg2].ToString() : string.Empty;
                    string arg3 = item[Constant.ACSArg3] != null ? item[Constant.ACSArg3].ToString() : string.Empty;
                    string value = result[Constant.ACSText].ToString().Replace("[Arg1]", arg1).Replace("[Arg2]", arg2).Replace("[Arg3]", arg3);
                    return value;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                APPSLogger.Logger(Microsoft.SharePoint.Administration.TraceSeverity.High, string.Format("ACSSOLUTION0012 Exception: {0}", ex.ToString()));
                return string.Empty;
            }
        }
    }
}
