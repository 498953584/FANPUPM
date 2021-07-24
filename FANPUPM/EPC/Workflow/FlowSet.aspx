<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FlowSet.aspx.cs" Inherits="FlowSet" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>工作流程设置</title>
    <base target="_self" />
</head>
<body class="body_frame" scroll="no">
    <form id="Form1" runat="server">
    <table id="Table3" style="height: 100%; width: 100%" cellspacing="0" cellpadding="0"
        border="1">
        <tr>
            <td valign="top" width="15%">
                <table id="tb" style="height: 100%" width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            选择<asp:DropDownList ID="DDLMKMC" AutoPostBack="true" OnSelectedIndexChanged="DDLMKMC_SelectedIndexChanged" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="height: 30%">
                        <td valign="top" height="100%">
                            <div class="gridBox" style="width: 100%; border: solid 0px red;">
                                <asp:TreeView ID="TVRole" ShowLines="true" ExpandDepth="-1" runat="server"><SelectedNodeStyle CssClass="trvw_select" /><NodeStyle ForeColor="Black" /></asp:TreeView>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="85%">
                <table id="Table1" style="height: 100%" cellspacing="0" cellpadding="0" width="100%"
                    border="1">
                    <tr style="height: 50%">
                        <td width="85%" style="height: 40%">
                            <iframe id="fraTemplate" name="fraTemplate" src="" frameborder="0" width="100%" scrolling="no"
                                height="100%"></iframe>
                        </td>
                    </tr>
                    <tr style="height: 50%">
                        <td>
                            <iframe id="fraNode" name="fraNode" src="" frameborder="0" width="100%" scrolling="no"
                                height="100%"></iframe>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
