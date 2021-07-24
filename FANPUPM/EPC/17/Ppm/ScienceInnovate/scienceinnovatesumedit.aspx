<%@ Page Language="C#" AutoEventWireup="true" CodeFile="scienceinnovatesumedit.aspx.cs" Inherits="ScienceInnovateSumEdit" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>ScienceInnovateSumEdit</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

    <base target="_self"></base>
    
    <script type="text/javascript" src="/Script/Watermark/jquery_Watermark.js"></script>
    <script type="text/javascript">
        window.onload = function () {
            var mark = document.getElementById("hdnmark").value;
            var type = document.getElementById("hdnType").value;
            if (type == "view" && mark == 1) {
                GetWaterMarked(window, '/images/yinzh.jpg', 'this');
            }
            var t = request("Type");
            if (document.getElementById('btnSave') != null) {
                document.getElementById('btnSave').onclick = btnSaveClick;
            }
        }

        function btnChecked() {
            var cbkmark = document.getElementById("cbkmark"); //获取元素
            if (cbkmark.status == false)//判断是否选中
                document.getElementById("TrGDType").style.display = "none"; //没有选中，隐藏元素
            else
                document.getElementById("TrGDType").style.display = "block"; //选中，显示元素
        }

        function btnSaveClick() {
            if (!document.getElementById('txtSummaryName').value) {
                top.ui.alert('施工技术总结名称或填报单位不能为空');
                return false;
            }
        }
        function checklen(e, maxlength) {
            if (e.value.length > maxlength) {
                top.ui.alert('输入长度不能大于' + maxlength);
                e.focus();
                return false;
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
<body>
    <form id="Form1" method="post" runat="server">
    <table width="100%">
        <tr>
            <td class="divHeader">
                工程总结资料<input type="hidden" id="hidID" value="" runat="server" />

            </td>
        </tr>
    </table>
    <table class="tableContent2" id="Table1" cellspacing="0" cellpadding="5px" width="100%"
        border="0">
        <tr>
            <td class="word" style="white-space: nowrap;">
                填报单位
            </td>
            <td class="txt" colspan="3">
                <asp:TextBox ID="txtReportUnit" Width="100%" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="word" style="white-space: nowrap;">
                工程名称
            </td>
            <td class="txt" colspan="3">
                <asp:TextBox ID="txtSumPrjName" Enabled="false" Width="100%" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="word" style="white-space: nowrap;">
                施工技术总结名称
            </td>
            <td class="txt" colspan="3">
                <asp:TextBox ID="txtSummaryName" Width="100%" runat="server"></asp:TextBox>
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
            <td class="word">
                做为归档资料
            </td>
            <td class="txt" colspan="3">
                <input id="cbkmark" type="checkbox" onclick="btnChecked();" runat="server" />

            </td>
        </tr>
        <tr id="TrGDType" style="display: none;" runat="server"><td class="word" runat="server">
                文档类别
            </td><td class="txt" colspan="3" runat="server">
                <JWControl:DropDownTree ID="DDTClass" runat="server"></JWControl:DropDownTree>
                <input id="HdnProjectCode" style="width: 10px" type="hidden" name="HdnProjectCode" runat="server" />

                <input id="HdnDocCode" style="width: 10px" type="hidden" name="HdnDocCode" runat="server" />

            </td></tr>
        <tr>
            <td class="word" style="white-space: nowrap;">
                备注
            </td>
            <td colspan="3" class="txt">
                <asp:TextBox ID="TextBox5" TextMode="MultiLine" Height="100px" Width="100%" onkeyup="checklen(this,300);" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="word" style="white-space: nowrap;">
                编制人
            </td>
            <td class="txt">
                <asp:TextBox ID="txtReporter" Enabled="false" Width="100%" runat="server"></asp:TextBox>
            </td>
            <td class="word" style="white-space: nowrap;">
                编制日期
            </td>
            <td class="txt">
                <JWControl:DateBox ID="txtReportTime" Enabled="false" Width="100%" runat="server"></JWControl:DateBox>
                <JWControl:JavaScriptControl ID="js" Width="100%" runat="server"></JWControl:JavaScriptControl>
            </td>
        </tr>
    </table>
    <div id="divFooter2" class="divFooter2" runat="server">
        <table class="tableFooter2">
            <tr>
                <td id="cbtn">
                    <asp:Button ID="btnSave" Text="保 存" OnClick="btnSave_Click" runat="server" />
                    <input type="button" value="取 消" onclick="javascript:top.ui.closeTab();" id="BunClose" runat="server" />

                </td>
            </tr>
        </table>
    </div>
    <input type="hidden" id="hdnmark" runat="server" />

    <input type="hidden" id="hdnType" runat="server" />

    <asp:HiddenField ID="hdnWfGuid" runat="server" />
    </form>
</body>
</html>
