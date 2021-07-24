<%@ Page Language="C#" AutoEventWireup="true" CodeFile="technologystandardedit.aspx.cs" Inherits="TechnologyStandardEdit" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>技术标准台账</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

    <base target="_self"></base>
    <script type="text/javascript" src="../../../../Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/Script/Watermark/jquery_Watermark.js"></script>
    <script language="javascript">
        function btnChecked() {
            var cbkmark = document.getElementById("cbkmark"); //获取元素
            if (cbkmark.status == false)//判断是否选中
                document.getElementById("TrGDType").style.display = "none"; //没有选中，隐藏元素
            else
                document.getElementById("TrGDType").style.display = "block"; //选中，显示元素
        }
        window.onload = function () {
            var mark = document.getElementById("hdnmark").value;
            var type = document.getElementById("hdnType").value;
            if (type == "view" && mark == 1) {
                GetWaterMarked(window, '/images/yinzh.jpg', 'this');
            }
        }
    </script>
</head>
<body scroll="no">
    <form id="Form1" method="post" runat="server">
    <font face="宋体">
        <div class="divContent2">
            <table width="100%">
                <tr>
                    <td class="divHeader">
                        <asp:Label ID="lb_change" Font-Bold="true" runat="server"></asp:Label>技术标准台账<input id="HdnTypeOld" style="width: 80px; height: 22px" type="hidden" size="8" name="Hidden1" runat="server" />

                    </td>
                </tr>
            </table>
            <table class="tableContent2" id="Table1" cellspacing="0" cellpadding="5px" width="100%"
                border="0">
                <tr>
                    <td class="word" style="white-space: nowrap;">
                        技术标准代号：
                    </td>
                    <td colspan="3" class="txt">
                        <asp:TextBox ID="TxtStandCode" MaxLength="50" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="技术标准代号不能为空！" ControlToValidate="TxtStandCode" Display="None" runat="server"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="word">
                        年号：
                    </td>
                    <td colspan="3" class="txt">
                        <asp:TextBox ID="DatePublic" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="word" style="white-space: nowrap;">
                        标准名称：
                    </td>
                    <td colspan="3" class="txt">
                        <asp:TextBox ID="TxtStandName" MaxLength="50" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="标准名称不能为空！" ControlToValidate="TxtStandName" Display="None" runat="server"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="word" style="white-space: nowrap;">
                        标准分类：
                    </td>
                    <td colspan="3" class="txt">
                        <asp:Label ID="lbPlanSort" runat="server"></asp:Label><input id="hidIsSave" type="hidden" value="true" name="Hidden1" runat="server" />

                    </td>
                </tr>
                <tr>
                    <td class="word">
                        做为归档资料：
                    </td>
                    <td class="txt" colspan="3">
                        <input id="cbkmark" type="checkbox" onclick="btnChecked();" runat="server" />

                    </td>
                </tr>
                <tr id="TrGDType" style="display: none;" runat="server"><td class="word" runat="server">
                        文档类别：
                    </td><td class="txt" colspan="3" runat="server">
                        <JWControl:DropDownTree ID="DDTClass" runat="server"></JWControl:DropDownTree>
                        <input id="HdnProjectCode" style="width: 10px" type="hidden" name="HdnProjectCode" runat="server" />

                        <input id="HdnDocCode" style="width: 10px" type="hidden" name="HdnDocCode" runat="server" />

                    </td></tr>
                <tr>
                    <td class="word" style="vertical-align: top; white-space: nowrap;">
                        附件：
                    </td>
                    <td colspan="3" class="txt">
                        <MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" />
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="word" style="white-space: nowrap;">
                        备注：
                    </td>
                    <td colspan="3" class="txt">
                        <asp:TextBox ID="TxtRemark" TextMode="MultiLine" Rows="5" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <div class="divFooter2">
                <table class="tableFooter2">
                    <tr>
                        <td class="td-submit" colspan="4">
                            <asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" />&nbsp;
                            <JWControl:JavaScriptControl ID="Js" runat="server"></JWControl:JavaScriptControl>
                            <input onclick="javascript:top.ui.closeTab();" type="button" value="取 消" id="BunClose" runat="server" />

                            <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary>
                        </td>
                    </tr>
                </table>
                <input type="hidden" id="hdnmark" runat="server" />

                <input type="hidden" id="hdnType" runat="server" />

            </div>
        </div>
    </font>
    <asp:HiddenField ID="hdnTechGuid" runat="server" />
    </form>
</body>
</html>
