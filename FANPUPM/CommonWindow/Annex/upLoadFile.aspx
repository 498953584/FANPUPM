<%@ Page Language="C#" AutoEventWireup="true" CodeFile="upLoadFile.aspx.cs" Inherits="upLoadFile" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>上传附件</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtFileCode').keydown(function (event) {
                var browser = navigator.appName
                if (browser != 'Microsoft Internet Explorer') {
                    if (event.which >= 48 && event.which <= 105)
                        return false;
                }
            });
            $('#txtRemark').keydown(function (event) {
                var browser = navigator.appName
                if (browser != 'Microsoft Internet Explorer') {
                    if (event.which >= 48 && event.which <= 105)
                        return false;
                }
            });
        });
    </script>
    <meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

    <base target="_self">
    <script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>
</head>
<body class="body_popup" scroll="no" ms_positioning="FlowLayout">
    <form id="Formx" method="post" runat="server">
    <table class="table-normal" id="Tablex" height="100%" cellspacing="0" cellpadding="0"
        width="100%" border="1">
        <tr>
            <td class="td-title" colspan="2" height="20">
                上传附件
            </td>
        </tr>
        <tr>
            <td class="td-label" align="right" width="18%">
                编号：
            </td>
            <td>
                <asp:TextBox ID="txtFileCode" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-label" align="right" width="18%">
                文件：
            </td>
            <td>
                <input id="fileAnnex" type="file" name="File1" style="width: 320px" runat="server" />

            </td>
        </tr>
        <tr>
            <td class="td-label" valign="top" align="right">
                备注：
            </td>
            <td>
                <asp:TextBox ID="txtRemark" TextMode="MultiLine" Rows="2" Columns="50" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="top" align="right" colspan="2" height="28">
                <table id="Table2" height="100%" cellspacing="0" cellpadding="0" width="60%" border="0">
                    <tr align="center">
                        <td style="width: 349px" colspan="2" class="td-submit">
                            <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
                            <asp:Button ID="btnAdd" CssClass="button-normal" Text="确 定" runat="server" />
                            <input class="button-normal" id="btnClose" onclick="window.close();" type="button"
                                value="取 消" name="btnClose">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
