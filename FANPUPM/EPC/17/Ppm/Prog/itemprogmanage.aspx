<%@ Page Language="C#" AutoEventWireup="true" CodeFile="itemprogmanage.aspx.cs" Inherits="ItemProgManage" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>项目奖罚</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />
<meta http-equiv="Expires" content="0" />

    <!-- 清除页面缓存-->
    <base target="_self" />
    <script src="../../../../Script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Script/Watermark/jquery_Watermark.js"></script>
    <script type="text/javascript" src="../../../../StockManage/script/Config2.js"></script>
    <script src="../../../../Script/jw.js" type="text/javascript"></script>
    <script src="../../../../Script/DecimalInput.js" type="text/javascript"></script>
	<script type="text/javascript" src="../../../../Script/My97DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript">
        function btnSaveClick() {
            if (!Trim(document.getElementById('TextBox_cfdw').value)) {
                alert("系统提示：\n\n奖罚单位不能为空");
                return false;
            }
        }
        function init_Info() {
            var mark = document.getElementById("hdnmark").value;
            if (mark == 1) {
                GetWaterMarked(window, '/images/yinzh.jpg', 'this');
            }
            $("#DDTClass").val($("#hidenClass").val());
            var flag = $("#cbkmark").attr("checked");
            if (flag) {
                $("#cbkmark").parent().nextAll().show();
            } else {
                $("#cbkmark").parent().nextAll().hide();
            }
            $("#cbkmark").click(function () {
                var flag = $(this).attr("checked");
                if (flag) {
                    $(this).parent().nextAll().show();
                } else {
                    $(this).parent().nextAll().hide();
                }
            });
            if (request("Type").toUpperCase() == "View".toUpperCase()) {
                $("input[type!='button']").attr("disabled", "disabled");
                $("select").attr("disabled", "disabled");
                $("textarea").attr("disabled", "disabled");
            }
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
<body scroll="no">
    <form id="Form1" method="post" onsubmit="return btnSaveClick()" runat="server">
    <asp:HiddenField ID="hdnmark" runat="server" />
    <div class="divContent2">
        <table width="100%">
            <tr>
                <td class="divHeader">
                    项目奖罚
                </td>
            </tr>
        </table>
        <table class="tableContent2" cellspacing="0" cellpadding="5px" width="100%" border="0">
            <tr>
                <td class="word" style="white-space: nowrap">
                    奖罚单位
                </td>
                <td class="txt" colspan="3">
                    <asp:TextBox ID="TextBox_cfdw" MaxLength="25" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap">
                    被奖罚对象
                </td>
                <td class="txt" colspan="3">
                    <asp:TextBox ID="TextBox_bcfdx" MaxLength="25" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap; vertical-align: top;">
                    奖罚金额
                </td>
                <td class="txt" colspan="3">
                    <asp:TextBox ID="Textbox_cfje" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap">
                    奖罚类别
                </td>
                <td class="txt" colspan="3">
                    <asp:DropDownList ID="DropDownList_lb" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap">
                    作为归档资料
                </td>
                <td class="txt">
                    <asp:CheckBox ID="cbkmark" runat="server" />
                </td>
                <td class="word" style="white-space: nowrap">
                    归档类别
                </td>
                <td>
                    <asp:HiddenField ID="hidenClass" runat="server" />
                    <JWControl:DropDownTree ID="DDTClass" Width="99%" runat="server"></JWControl:DropDownTree>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap">
                    奖罚日期
                </td>
                <td class="txt" colspan="3">
                  
					 <asp:TextBox ID="Textbox_cfrq" onclick="WdatePicker()" runat="server"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap">
                    负责人
                </td>
                <td class="txt" colspan="3">
                    <asp:TextBox ID="Textbox_fzr" MaxLength="10" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap">
                    奖罚单开出单位
                </td>
                <td class="txt" colspan="3">
                    <asp:TextBox ID="Textbox_cfdkcdw" MaxLength="150" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap">
                    奖罚依据
                </td>
                <td class="txt" colspan="3">
                    <asp:TextBox ID="TextBox_cfyj" TextMode="MultiLine" Height="56px" MaxLength="300" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap">
                    奖罚原因
                </td>
                <td class="txt" colspan="3">
                    <asp:TextBox ID="TextBox_cfyy" Height="56px" TextMode="MultiLine" MaxLength="500" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap">
                    备注
                </td>
                <td class="txt" colspan="3">
                    <asp:TextBox ID="Textbox_bz" Height="46px" TextMode="MultiLine" MaxLength="300" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="divFooter2">
            <table class="tableFooter2">
                <tr>
                    <td>
                        <asp:Button ID="Button_save" Text="保 存" OnClick="Button_save_Click" runat="server" />&nbsp;
                        <input type="button" id="ButtonColse" value="取 消" onclick="top.ui.closeTab();" runat="server" />
&nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <JWControl:JavaScriptControl ID="JavaScriptControl1" runat="server"></JWControl:JavaScriptControl>
    </div>
    </form>
</body>
</html>
