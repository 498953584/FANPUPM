<%@ Page Language="C#" AutoEventWireup="true" CodeFile="measuredataedit.aspx.cs" Inherits="MeasureDataEdit" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>编辑/查看页面</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

    <base target="_self" />
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/Script/Watermark/jquery_Watermark.js"></script>
    <script type="text/javascript" src="../../../../StockManage/script/Config2S.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script language="javascript">
        window.onload = function () {
            var mark = document.getElementById("hdnmark").value;
            if (mark == 1) {
                GetWaterMarked(window, '/images/yinzh.jpg', 'this');
            }
            //如果是审核页面,隐藏关闭按钮
            var parentLocation = window.parent.location.href;
            if (parentLocation.indexOf('AuditFrame.aspx') > 0) {
                $('#divBtns').attr('style', 'display:none');
            }
        }
        function checklen(e, maxlength) {
            if (e.value.length > maxlength) {
                alert('输入长度不能大于' + maxlength);
                e.focus();
                return false;
            }
        }
        function checkSubmitForm() {
            return begiFrom();
        }
        function begiFrom() {
            var tf = true;
            if (this.type == 'submit') {
                if ($.trim($("#TxtStandCode").val()) == "") {
                    $("#TxtStandCode").focus();
                    alert("编号不能为空！");
                    tf = false;
                }
            }
            return tf;
        }

        //关闭查看页面
        function closeCheckWindow() {
            try {
                top.ui.closeTab();
            } catch (e) {
                window.close();
            }
        }

        //选择人员
        function selectUser(id, name) {
            document.getElementById("hdReturnVal").value = id + "," + name;
            var url = "/Common/DivSelectUser.aspx?Method=returnUser";
            document.getElementById("IframePerson").src = url;
            selectnEvent('divFramPerson');
        }
        //选择人员返回值
        function returnUser(id, name) {
            var val = document.getElementById("hdReturnVal").value.split(',');
            document.getElementById(val[0]).value = id;
            document.getElementById(val[1]).value = name;
        }
    </script>
</head>
<body scroll="no">
    <form id="Form1" method="post" onsubmit="return checkSubmitForm();" runat="server">
    <font face="宋体">
        <div class="divContent2">
            <table width="100%">
                <tr>
                    <td class="divHeader">
                        <asp:Label ID="lb_change" Font-Bold="false" runat="server">Label</asp:Label><asp:Label ID="LblTitle" runat="server">Label</asp:Label>
                    </td>
                </tr>
            </table>
            <table class="tableContent2" id="Table1" cellspacing="0" cellpadding="5px" width="100%"
                border="0">
                <tr>
                    <td class="word" style="white-space: nowrap;">
                        附件：
                    </td>
                    <td colspan="3" class="txt">
                        <MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" />
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="word" style="white-space: nowrap;">
                        编号：
                    </td>
                    <td colspan="3" class="txt mustInput">
                        <asp:TextBox ID="TxtStandCode" MaxLength="50" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="None" ControlToValidate="TxtStandCode" ErrorMessage="编号不能为空！" runat="server"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="word" style="white-space: nowrap;">
                        <asp:Label ID="LblName" Width="114px" runat="server">Label</asp:Label>
                    </td>
                    <td colspan="3" class="txt mustInput">
                        <asp:TextBox ID="TxtStandName" MaxLength="200" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="None" ControlToValidate="TxtStandName" ErrorMessage="名称不能为空！" runat="server"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="word">
                        作为归档资料：
                    </td>
                    <td class="txt" colspan="3">
                        <asp:CheckBox ID="cbkmark" runat="server" />
                    </td>
                </tr>
                <tr id="filesClass">
                    <td class="word">
                        <div id="c1">
                            归档类别：
                        </div>
                    </td>
                    <td class="txt" colspan="3">
                        <div id="c2">
                            <asp:HiddenField ID="hidenClass" runat="server" />
                            <JWControl:DropDownTree ID="DDTClass" runat="server"></JWControl:DropDownTree>
                        </div>
                    </td>
                </tr>
                <tr id="TRName" visible="false" runat="server"><td class="word" runat="server">
                        交接人:
                    </td><td runat="server">
                        <span class="spanSelect" style="width: 122px;">
                            <asp:TextBox ID="txtJoin" Style="width: 97px; height: 15px; border: none;
                                line-height: 16px; margin: 1px 2px" runat="server"></asp:TextBox>
                            <img src="~/images/icon.bmp" style="float: right;" alt="选择" id="imgName" onclick="selectUser('hfldName','txtJoin');" runat="server" />

                        </span>
                        <input id="hfldName" type="hidden" style="width: 1px" runat="server" />

                    </td></tr>
                <tr id="TRReceive" visible="false" runat="server"><td class="word" runat="server">
                        接收人:
                    </td><td runat="server">
                        <span class="spanSelect" style="width: 122px;">
                            <asp:TextBox ID="txtReceive" Style="width: 97px; height: 15px; border: none;
                                line-height: 16px; margin: 1px 2px" runat="server"></asp:TextBox>
                            <img src="~/images/icon.bmp" style="float: right;" alt="选择" id="img1" onclick="selectUser('hfldPerson','txtReceive');" runat="server" />

                        </span>
                        <input id="hfldPerson" type="hidden" style="width: 1px" runat="server" />

                    </td></tr>
                <tr>
                    <td class="word" style="white-space: nowrap;">
                        备注：
                    </td>
                    <td colspan="3" class="txt">
                        <asp:TextBox ID="TxtRemark" Rows="5" TextMode="MultiLine" onkeyup="checklen(this,100);" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <div class="divFooter2" id="divBtns">
                <table class="tableFooter2">
                    <tr>
                        <td class="td-submit" colspan="4">
                            <asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" />&nbsp;
                            <input onclick="closeCheckWindow();" type="button" value="取 消" id="BunClose" name="BunClose" runat="server" />

                            <JWControl:JavaScriptControl ID="Js" runat="server"></JWControl:JavaScriptControl>
                            <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" ShowMessageBox="true" runat="server"></asp:ValidationSummary>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </font>
    <div>
        <asp:HiddenField ID="hdnmark" runat="server" />
    </div>
    <div id="divFramPerson" title="选择人员" style="display: none">
        <iframe id="IframePerson" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <asp:HiddenField ID="hdnTechGuid" runat="server" />
    <asp:HiddenField ID="hdReturnVal" runat="server" />
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    $(function () {
        var flge = $("#cbkmark").attr("checked");
        if (flge) {
            $("#filesClass").show();
        } else {
            $("#filesClass").hide();
        }
        $("#cbkmark").click(function () {
            var flge = $("#cbkmark").attr("checked");
            if (flge) {
                $("#filesClass").show();
            } else {
                $("#filesClass").hide();
            }
        });
        $("#DDTClass").val($("#hidenClass").val());
        var typeName = request("Type");
        if (typeName == "View") {
            $("#FileLink1_But_UpFile").attr("disabled", "disabled");
            $("#cbkmark").attr("disabled", "disabled");
            $("#DDTClass").attr("disabled", "disabled");
        } else {
            $("#FileLink1_But_UpFile").removeAttr("disabled")
        }
    });
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
