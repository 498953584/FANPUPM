<%@ Page Language="C#" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Edit1" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>Edit</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

    <script src="../../../../StockManage/script/Validation.js" type="text/javascript"></script>
    <script src="../../../../Script/jquery.js" type="text/javascript"></script>
    <script language="javascript">
        $(document).ready(function () {
            Val.validate('Form1', 'btnSave');
        });
        function openannexpage(RecordCode) {
            window.showModalDialog("/CommonWindow/Annex/AnnexList.aspx?mid=1743&rc=" + RecordCode + "&at=0&type=2", "", 'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
        }
        function request(paras) {
            var url = location.href;
            var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
            var paraObj = {}
            for (i = 0; j = paraString[i]; i++) {
                paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
            }
            var returnValue = paraObj[paras.toLowerCase()];
            if (typeof (returnValue) == "undefined") {
                return "";
            } else {
                return returnValue;
            }
        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <div class="divContent2">
        <div class="divHeader">
            曝光信息</div>
        <table width="100%" border="0" class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word" style="white-space: nowrap;">
                    曝光部门
                </td>
                <td class="txt mustInput" colspan="3">
                    <asp:TextBox ID="TextBox1" MaxLength="25" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    曝光时间
                </td>
                <td class="txt" colspan="3">
                    <JWControl:DateBox ID="DateBox1" runat="server"></JWControl:DateBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    文件名称
                </td>
                <td class="txt" colspan="3">
                    <MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" />
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    曝光内容
                </td>
                <td class="txt" colspan="3">
                    <asp:TextBox ID="TextBox4" TextMode="MultiLine" Height="120px" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="divFooter2">
            <asp:Button ID="btnSave" Text="保 存" OnClick="Button1_Click" runat="server" />
            <JWControl:JavaScriptControl ID="Js" runat="server"></JWControl:JavaScriptControl>
            &nbsp;&nbsp;<input type="button" value="取 消" onclick="top.ui.closeTab();">
        </div>
    </div>
    </form>
</body>
</html>
