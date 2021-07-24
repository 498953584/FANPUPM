<%@ Page Language="C#" AutoEventWireup="true" CodeFile="progressplanpermiss.aspx.cs" Inherits="ProgressPlanPermiss" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>进步计划审核</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

    <base target="_self">
</head>
<body class="body_popup">
    <form id="Form1" method="post" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr style="height: 20px;">
                <td class="divHeader">
                    进步计划审核
                </td>
            </tr>
        </table>
    </div>
    <table id="Table1" class="tableContent2" cellpadding="5px" cellspacing="0">
        <tr height="30">
            <td class="word">
                审核结果
            </td>
            <td class="txt">
                <asp:DropDownList ID="ddlAuditState" runat="server"></asp:DropDownList>
            </td>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td class="word">
                审核人
            </td>
            <td class="txt">
                <asp:TextBox ID="txtPermissionPeople" runat="server"></asp:TextBox>
            </td>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td class="word">
                审核意见
            </td>
            <td colspan="3" class="txt">
                <asp:TextBox ID="txtPermissionView" TextMode="MultiLine" Height="100px" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr class="tableFooter2">
                <td>
                    <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
                    <asp:Button ID="btnSave" Text="保  存" OnClick="btnSave_Click" runat="server" />&nbsp;<input
                        type="button" onclick="window.returnValue=true;top.ui.closeTab();" value="退  出"
                        class="button-normal">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
