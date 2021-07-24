<%@ Page Language="C#" AutoEventWireup="true" CodeFile="add.aspx.cs" Inherits="Add1" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>Add</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

    <script src="../../../../Script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../../../StockManage/script/Config2.js"></script>
    <script src="../../../../StockManage/script/Validation.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            Val.validate('Form1', 'btnSave');
            document.getElementById('btnSave').onclick = btnSaveClick;
        });
        function btnSaveClick() {
            if (!Trim(document.getElementById('TextBox1').value)) {
                top.ui.show("系统提示：\n\n曝光部门不能为空！");
                return false;
            }
        }
        function openannexpage(RecordCode) {
            window.showModalDialog("/CommonWindow/Annex/AnnexList.aspx?mid=1743&rc=" + RecordCode + "&at=0&type=2", "", 'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <div class="divContent2">
        <div class="divHeader">
            信息添加</div>
        <table width="100%" border="0" class="tableContent2" cellpadding="5px">
            <tr>
                <td class="word" style="white-space: nowrap;">
                    曝光部门
                </td>
                <td colspan="3" class="txt mustInput">
                    <asp:TextBox ID="TextBox1" MaxLength="25" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    曝光时间
                </td>
                <td colspan="3" class="txt">
                    <JWControl:DateBox ID="DateBox1" runat="server"></JWControl:DateBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    文件名称
                </td>
                <td colspan="3" class="txt">
                    <MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" Visible="true" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    曝光内容
                </td>
                <td colspan="3" class="txt">
                    <asp:TextBox ID="TextBox4" Height="120px" CausesValidation="true" TextMode="MultiLine" MaxLength="10" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="divFooter2">
            <table class="tableFooter2">
                <tr>
                    <td>
                        <asp:Button ID="btnSave" Text="保 存" OnClick="Button1_Click" runat="server" />&nbsp;&nbsp;
                        <JWControl:JavaScriptControl ID="Js" runat="server"></JWControl:JavaScriptControl>
                        <input type="button" value="取 消" onclick="javascript:top.frameWorkArea.window.desktop.getActive().close();">
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
