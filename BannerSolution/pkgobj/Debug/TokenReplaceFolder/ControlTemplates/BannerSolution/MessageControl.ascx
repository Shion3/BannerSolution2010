<%@ Assembly Name="appssp2010bannersolution, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a74ab03868ccbf9e" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageControl.ascx.cs" Inherits="BannerSolution.ControlTemplates.BannerSolution.MessageControl" %>

<script src="/_layouts/BannerSolution/JavaScript/jquery-3.3.1.min.js"></script>

<style type="text/css">
    html.ms-dialog body #ACSBannerBlockContainer {
        display: none;
    }

    #ACSBannerBlockContainer .ACSTableClass {
        border: none;
        margin-bottom: 0;
    }

    #ACSBannerBlockContainer .ACSLastStatus {
        width: 150px;
        font-size: <%=ProgressTitleFontSize %>;
        line-height: normal;
        position: absolute;
        right: -150px;
        bottom: 22px;
        color: <%=ProgressTitleColor %>;
        font-family: <%=ProgressTitleFontFamily %>;
    }

    #ACSBannerBlockContainer td.ACSProgressTitle {
        line-height: normal;
        padding: 5px 0 0 0;
        vertical-align: bottom;
        border: 0px;
    }

    #ACSBannerBlockContainer .ACSProgressTitle span {
        font-size: <%=ProgressTitleFontSize %>;
        display: block;
        width: 70%;
    }

    #ACSBannerBlockContainer .progress {
        overflow: hidden;
        height: 20px;
        margin-bottom: 20px;
        background-color: #f5f5f5;
        border-radius: 4px;
        box-shadow: inset 0 1px 2px rgba(0,0,0,.1);
    }

        #ACSBannerBlockContainer .progress-bar.active, #ACSBannerBlockContainer .progress.active .progress-bar {
            -webkit-animation: progress-bar-stripes 2s linear infinite;
            -o-animation: progress-bar-stripes 2s linear infinite;
            animation: progress-bar-stripes 2s linear infinite;
            float: left;
            width: 0;
            height: 100%;
            font-size: 12px;
            line-height: 20px;
            color: #fff;
            text-align: center;
            background-color: #337ab7;
            -webkit-box-shadow: inset 0 -1px 0 rgba(0,0,0,.15);
            box-shadow: inset 0 -1px 0 rgba(0,0,0,.15);
            -webkit-transition: width .6s ease;
            -o-transition: width .6s ease;
            transition: width .6s ease;
            background-image: -webkit-linear-gradient(45deg,rgba(255,255,255,.15) 25%,transparent 25%,transparent 50%,rgba(255,255,255,.15) 50%,rgba(255,255,255,.15) 75%,transparent 75%,transparent);
            background-image: -o-linear-gradient(45deg,rgba(255,255,255,.15) 25%,transparent 25%,transparent 50%,rgba(255,255,255,.15) 50%,rgba(255,255,255,.15) 75%,transparent 75%,transparent);
            background-image: linear-gradient(45deg,rgba(255,255,255,.15) 25%,transparent 25%,transparent 50%,rgba(255,255,255,.15) 50%,rgba(255,255,255,.15) 75%,transparent 75%,transparent);
            -webkit-background-size: 40px 40px;
            background-size: 40px 40px;
        }

    @-webkit-keyframes progress-bar-stripes {
        from {
            background-position: 40px 0;
        }

        to {
            background-position: 0 0;
        }
    }

    @keyframes progress-bar-stripes {
        from {
            background-position: 40px 0;
        }

        to {
            background-position: 0 0;
        }
    }
</style>
<div id="ACSBannerBlockContainer" style="<%=ShowDAndProgressBar %>">
    <div style="width: 70%; margin: 0 auto; min-width: 740px; position: relative; <%=EnableProgressBar %>">
        <table class='table ACSTableClass'>
            <tbody>
                <tr style="color: <%=ProgressTitleColor %>; font-family: <%=ProgressTitleFontFamily %>">
                    <%=StatusBlock %>
                </tr>
            </tbody>
        </table>
        <div class="ACSLastStatus">
            <%=LastStatus %>
        </div>
        <div class="progress progress-striped active">
            <div class="progress-bar" role="progressbar" style="background-color: <%=ProgressColor %>; width: <%=Percent %>%;">
            </div>
        </div>
    </div>
    <div style="font-size: 16px; width: 70%; font-weight: bold; color: red; text-align: center; margin: 10px auto"><%=Description%></div>
</div>
