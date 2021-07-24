<%@ Page Language="C#" AutoEventWireup="true" CodeFile="goaledit.aspx.cs" Inherits="GoalEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//Dtd HTML 4.0 transitional//EN" >
<html>
<head runat="server"><title>工程质量目标</title>
    <script src="/Script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Script/Watermark/jquery_Watermark.js"></script>
    <script language="javascript" type="text/javascript">
        window.onload = function () {
            var mark = $('#hdnmark').val(); //document.getElementById("hdnmark").value;
            if (mark == 1) {
                GetWaterMarked(window, '/images/yinzh.jpg', 'this');
            }
        }
        function clickRow(obj, selectedIndex) {
            document.getElementById('hdnSelectedIndex').value = selectedIndex;
            /*在这之前增加你的处理代码*/
            doClick(obj, 'dgdScheduleUp'); //调用样式设置
        }
        function outrow(obj) {
            /*在这之前增加你的处理代码*/
            doMouseOut(obj); //调用样式设置
        }
        function overRow(obj) {
            /*在这之前增加你的处理代码*/
            doMouseOver(obj); //调用样式设置
        }
        function pickSchedule(obj) {
            var objCode
            objCode = document.getElementById('hdnprjcode').value;
            var url;
            var pn = escape('选择');
            url = "/commonWindow/tasklist.aspx?pn=" + pn + "&pc=" + objCode;
            var Man = new Array(new Array(), new Array(), new Array());
            window.showModalDialog(url, Man, 'dialogHeight:500px;dialogWidth:640px;center:1;help:0;status:0;');
            if (Man[0] != "") {
                obj.value = Man[1];
                document.getElementById('hdnTempScheduleCode').value = Man[0];
                document.getElementById('hdnTempScheduleName').value = Man[1];
            }
        }
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
            var _pagetype = request("Type");
            if (_pagetype != "") {
                switch (_pagetype) {
                    case "Query":
                        $("#Txtremark").attr("disabled", "disabled");
                        $("#cbkmark").attr("disabled", "disabled");
                        $("#DDTClass").attr("disabled", "disabled");
                        $("#TxtScheduleCode").attr("disabled", "disabled");
                        $("#TxtGoal").attr("disabled", "disabled");
                        $("#TxtScheduleCode").attr("style", "width:184px;");
                        $("#IMG2").hide();
                        $("#BtnOK").hide();
                        break;
                }
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
        function Button1_onclick() {
            top.ui.closeTab();
        }
    </script>
</head>
<body scroll="no">
    <form id="Form1" method="post" runat="server">
    <asp:HiddenField ID="hdnmark" runat="server" />
    <div class="divContent2">
        <table width="100%">
            <tr>
                <td class="divHeader">
                    <asp:Label ID="LblHead" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="table1" width="100%" class="tableContent2" cellpadding="5px" cellspacing="0"
            border="0">
            <tr>
                <td class="word">
                    工程名称：
                </td>
                <td class="txt" colspan="3">
                    <input type="text" readonly="true" class="txtreadonly" style="background-color: #ffffc0;" id="TxtScheduleCode" runat="server" />

                    <img id="IMG2" align="absmiddle" onclick="pickSchedule(TxtScheduleCode);" alt="" src="/images/contact.gif" style="cursor: hand" runat="server" />

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="RequiredFieldValidator" ControlToValidate="TxtScheduleCode" runat="server">*(必填)</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="word">
                    质量目标：
                </td>
                <td class="txt" colspan="3">
                    <asp:TextBox ID="TxtGoal" MaxLength="200" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    作为归档资料：
                </td>
                <td class="txt">
                    <asp:CheckBox ID="cbkmark" runat="server" />
                </td>
            </tr>
            <tr id="filesClass" runat="server"><td class="word" runat="server">
                    <div id="c1">
                        归档类别：
                    </div>
                </td><td class="txt" runat="server">
                    <div id="c2">
                        <asp:HiddenField ID="hidenClass" runat="server" />
                        <JWControl:DropDownTree ID="DDTClass" Width="80%" runat="server"></JWControl:DropDownTree>
                    </div>
                </td></tr>
            <tr>
                <td class="word">
                    备&nbsp;&nbsp;&nbsp;&nbsp;注：
                </td>
                <td class="txt" colspan="3">
                    <asp:TextBox ID="Txtremark" Height="50px" TextMode="MultiLine" CssClass="textarea-input" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="divFooter2">
            <table class="tableFooter2">
                <tr>
                    <td>
                        <asp:Button ID="BtnOK" Text="保  存" OnClick="BtnOK_Click" runat="server" />
                        &nbsp;
                        <input id="Button1" type="button" value="取 消" onclick="return Button1_onclick()" runat="server" />

                        &nbsp;
                        <input id="hdnDacumClass" type="hidden" runat="server" />

                    </td>
                </tr>
            </table>
        </div>
    </div>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    <input id="hdnTempScheduleCode" style="width: 10px" type="hidden" name="hdnTempScheduleCode" runat="server" />

    <input id="hdnTempScheduleName" style="width: 10px" type="hidden" name="hdnTempScheduleName" />
    <input id="hdnScheduleCodeList" style="width: 10px" type="hidden" name="hdnScheduleCodeList" />
    <input id="hdnPn" type="hidden" name="hdnPn" runat="server" />

    <input id="hdnprjcode" type="hidden" runat="server" />

    </form>
</body>
</html>
