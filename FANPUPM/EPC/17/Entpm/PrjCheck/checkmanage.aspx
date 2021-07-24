<%@ Page Language="C#" AutoEventWireup="true" CodeFile="checkmanage.aspx.cs" Inherits="CheckManage" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>项目检查</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />
<meta http-equiv="Expires" content="0" />

    <!-- 清除页面缓存-->
    <base target="_self">
    <script src="/Script/jquery.js" type="text/javascript"></script>
    <script src="../../../../Script/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script language="javascript">
        $(function () {
            var _style = $("td").attr("style");
            if (_style != null) {
                $("td").attr("style", _style + " white-space:nowrap;");
            } else {
                $("td").attr("style", " white-space:nowrap;");
            }
            var temFl = $("#cbkmark").attr("checked");
            if (temFl) {
                $("#c1").show();
                $("#c2").show();
            } else {
                $("#c1").hide();
                $("#c2").hide();
            }
            $("#DDTClass").val($("#hidenClass").val());
            $("#cbkmark").click(function () {
                var flag = $(this).attr("checked");
                if (flag) {
                    $("#c1").show();
                    $("#c2").show();
                } else {
                    $("#c1").hide();
                    $("#c2").hide();
                }
            });

            var _type = request("Type");
            if (_type.toLowerCase() == "view") {
                $("#cbkmark").attr("disabled", "disabled");
                $("#DDTClass").attr("disabled", "disabled");
                $("#FileLink1_txtFile").attr("disabled", "disabled");
                $("#FileLink1_But_UpFile").attr("disabled", "disabled");
                //   $("#FileLink1_Btn_Upload").attr("disabled", "disabled");
                $("#FileLink2_txtFile").attr("disabled", "disabled");
                $("#FileLink2_But_UpFile").attr("disabled", "disabled");
                // $("#FileLink2_Btn_Upload").attr("disabled", "disabled");
            } else if (_type.toLowerCase() == "rectify") {
                $("#FileLink1_txtFile").attr("disabled", "disabled");
                $("#FileLink1_But_UpFile").attr("disabled", "disabled");
                // $("#FileLink1_Btn_Upload").attr("disabled", "disabled");
                $("#cbkmark").attr("disabled", "disabled");
                $("#DDTClass").attr("disabled", "disabled");
                $("#DateBox_sjwcsj").attr("disabled", "disabled");
            } else if (_type.toLowerCase() == "edit") {
                $("#FileLink2_txtFile").attr("disabled", "disabled");
                $("#FileLink2_But_UpFile").attr("disabled", "disabled");
                $("#FileLink2_Btn_Upload").attr("disabled", "disabled");
            } else if (_type.toLowerCase() == "certify") {
                $("#cbkmark").attr("disabled", "disabled");
                $("#DDTClass").attr("disabled", "disabled");
                $("#FileLink1_txtFile").attr("disabled", "disabled");
                $("#FileLink1_But_UpFile").attr("disabled", "disabled");
                //$("#FileLink1_Btn_Upload").attr("disabled", "disabled");
                $("#FileLink2_txtFile").attr("disabled", "disabled");
                $("#FileLink2_But_UpFile").attr("disabled", "disabled");
                //$("#FileLink2_Btn_Upload").attr("disabled", "disabled");
            }
        });
        ///根据URL参数名称 获得到所对应的值
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
    <form id="Form1" method="post" runat="server">
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <div class="divContent2">
        <table width="100%">
            <tr>
                <td class="divHeader">
                    <asp:Label ID="lbTitle" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="tableContent2" cellspacing="0" cellpadding="5px" width="100%" border="0">
            <tr>
                <td class="word" style="white-space: nowrap;">
                    检查单位
                </td>
                <td colspan="3" class="txt">
                    <asp:TextBox ID="TextBox_jcdw" MaxLength="100" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    受检单位
                </td>
                <td colspan="3" class="txt">
                    <asp:TextBox ID="TextBox_sjdw" MaxLength="150" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    作为归档资料
                </td>
                <td class="txt">
                    <asp:CheckBox ID="cbkmark" runat="server" />
                </td>
                <td class="word" style="white-space: nowrap;" id="c2">
                    归档类别
                </td>
                <td class="txt" id="c1">
                    <asp:HiddenField ID="hidenClass" runat="server" />
                    <JWControl:DropDownTree ID="DDTClass" Width="100%" runat="server"></JWControl:DropDownTree>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    检查内容
                </td>
                <td colspan="3" class="txt">
                    <asp:TextBox ID="TextBox_jcnr" CssClass="text-input" TextMode="MultiLine" Height="56px" MaxLength="250" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    检查依据
                </td>
                <td colspan="3" class="txt">
                    <asp:TextBox ID="TextBox_jcyj" Height="56px" TextMode="MultiLine" MaxLength="250" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    附件
                </td>
                <td colspan="3" class="txt">
                    <MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" />
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    检查类别
                </td>
                <td class="txt">
                    <asp:DropDownList ID="DropDownList_lb" Width="100%" runat="server"></asp:DropDownList>
                </td>
                <td class="word" style="white-space: nowrap;">
                    检查责任人
                </td>
                <td class="txt">
                    <asp:TextBox ID="TextBox_jcfzr" MaxLength="10" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    需要整改
                </td>
                <td class="txt">
                    <asp:DropDownList ID="ddlCheckResult" ForeColor="#C00000" runat="server"><asp:ListItem Value="1" Text="否" /><asp:ListItem Value="0" Text="是" /></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    检查结果
                </td>
                <td class="txt">
                    <asp:DropDownList ID="ddlCheckResults" Width="100%" runat="server"><asp:ListItem Value="1" Text="优良" /><asp:ListItem Value="2" Text="合格" /><asp:ListItem Value="0" Text="不合格" /></asp:DropDownList>
                </td>
                <td class="word" style="white-space: nowrap;">
                    验证结果
                </td>
                <td class="txt">
                    <asp:DropDownList ID="ddlCertifiResult" Width="100%" ForeColor="#C00000" runat="server"><asp:ListItem Value="0" Text="未验证" /><asp:ListItem Value="2" Text="合格" /><asp:ListItem Value="1" Text="不合格" /></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    检查日期
                </td>
                <td class="txt" colspan="3">
                   <asp:TextBox ID="DateBox_jcrq" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    要求整改完成时间
                </td>
                <td class="txt" colspan="3">
                <asp:TextBox ID="DateBox_yqwcsj" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    项目经理部计划完成时间
                </td>
                <td class="txt" colspan="3">
                    <asp:TextBox ID="DateBox_jhwcsj" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    项目经理部实际整改完成时间
                </td>
                <td class="txt" colspan="3">
                <asp:TextBox ID="DateBox_sjwcsj" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    检查结果(内容)
                </td>
                <td colspan="3" class="txt">
                    <asp:TextBox ID="Textbox_jcjg" Height="56px" TextMode="MultiLine" MaxLength="250" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    整改要求
                </td>
                <td colspan="3" class="txt">
                    <asp:TextBox ID="Textbox_zgnr" Height="56px" TextMode="MultiLine" MaxLength="250" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    整改计划（项目部填写，长度一千字）
                </td>
                <td colspan="3" class="txt">
                    <asp:TextBox ID="TextBox_xmjh" MaxLength="1000" Height="56px" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    附件
                </td>
                <td colspan="3" class="txt">
                    <MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink2" runat="server" />
                    <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    整改结果验证
                </td>
                <td colspan="3" class="txt">
                    <asp:TextBox ID="TextBox_sjjg" MaxLength="250" Height="46px" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    备注
                </td>
                <td colspan="3" class="txt">
                    <asp:TextBox ID="Textbox_bz" Height="46px" TextMode="MultiLine" MaxLength="150" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="divFooter2">
            <table class="tableFooter2">
                <tr>
                    <td>
                        <asp:Button ID="Button_save" Text="保 存" OnClick="Button_save_Click" runat="server" />&nbsp;
                        <input type="button" id="Button1" name="butcolse" value="取 消" onclick="top.ui.closeTab();" runat="server" />
&nbsp
                    </td>
                </tr>
            </table>
        </div>
        <JWControl:JavaScriptControl ID="JavaScriptControl1" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
