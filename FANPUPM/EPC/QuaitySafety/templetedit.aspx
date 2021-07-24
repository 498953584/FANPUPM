<%@ Page Language="C#" AutoEventWireup="true" CodeFile="templetedit.aspx.cs" Inherits="TempletEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>质量台账</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

    <base target="_self">
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script language="javascript" type="text/javascript">
        function changeTemplete() {
            var fileName = $('#FileModel').val();
            if (fileName.length != 0) {
                $('#btnConfirm').removeAttr('disabled', 'disabled');
            } else {
                $('#btnConfirm').attr('disabled', 'disabled');
            }
        }
        // 取消
        function btnCancelClick() {
            top.ui.closeTab();
        } 
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <div class="divContent2">
        <table width="100%">
            <tr align="center">
                <td class="divHeader">
                    <asp:Label ID="LbName" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="TbFileUpload" class="tableContent2" cellspacing="0" cellpadding="5px"
            width="100%" border="0">
            <tr>
                <td class="word">
                    模板名称:
                </td>
                <td class="txt">
                    <asp:TextBox ID="tbModelName" Width="320px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" width="30%">
                    上传模板文件:
                </td>
                <td class="txt">
                    <input id="FileModel" onchange="changeTemplete();" style="width: 320px; height: 21px" type="file" size="35" name="File1" runat="server" />

                    <asp:HyperLink ID="hlFile" Visible="false" Target="_search" runat="server">HyperLink</asp:HyperLink>
                </td>
            </tr>
        </table>
        <div class="divFooter2">
            <table class="tableFooter2">
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnConfirm" Text="确 定" disabled="disabled" OnClick="btnConfirm_Click" runat="server" />&nbsp;<input type="button" value="取 消" onclick="btnCancelClick();">
                        <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
