<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageControl.ascx.cs" Inherits="BannerSolution.ControlTemplates.BannerSolution.MessageControl" %>

<link rel="stylesheet" href="/_layouts/BannerSolution/Bootstrap/bootstrap.min.css">
<script src="/_layouts/BannerSolution/JavaScript/jquery-3.3.1.min.js" />
<script src="/_layouts/BannerSolution/Bootstrap/bootstrap.min.js"></script>

<style type="text/css">
    html.ms-dialog body .ACSBannerBlockContainer {
        display: none;
    }

    .ACSTableClass {
        border: none;
        margin-bottom: 0;
    }

    .ACSLastStatus {
        width: 87px;
        font-size: 12px;
        line-height: normal;
        position: absolute;
        right: -87px;
        bottom: 20px;
    }
</style>
<div class="ACSBannerBlockContainer">
    <div style="width: 70%; margin: 0 auto; min-width: 740px; position: relative;">
        <table class='table ACSTableClass'>
            <tbody>
                <tr>
                    <%=StatusBlock %>
                </tr>
            </tbody>
        </table>
        <div class="ACSLastStatus"><%=LastStatus %></div>
        <div class="progress progress-striped active">
            <div class="progress-bar" role="progressbar" aria-valuenow="<%=Percent %>"
                aria-valuemin="0" aria-valuemax="100" style="background-color: <%=ProgressColor %>; width: <%=Percent %>%;">
            </div>
        </div>
    </div>
    <div>
        <%=Description %>
    </div>
</div>
