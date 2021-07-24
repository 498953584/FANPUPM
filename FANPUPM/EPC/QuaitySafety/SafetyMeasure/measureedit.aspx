<%@ Page Language="C#" AutoEventWireup="true" CodeFile="measureedit.aspx.cs" Inherits="MeasureEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>目标编辑</title>
    <script src="/Script/jquery.js" type="text/javascript"></script>
    
    <script type="text/javascript" src="/Script/Watermark/jquery_Watermark.js"></script>
    <script language="javascript" type="text/javascript">

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
            var mark = document.getElementById("hdnmark").value;
            if (mark == 1) {
                GetWaterMarked(window, '/images/yinzh.jpg', 'this');
            }
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
                        $("#cbkmark").attr("disabled", "disabled");
                        $("#DDTClass").attr("disabled", "disabled");
                        $("#Txtremark").attr("disabled", "disabled");
                        $("#TxtScheduleCode").attr("disabled", "disabled");
                        $("#TxtMeasure").attr("disabled", "disabled");
                        $("#TxtScheduleCode").attr("style", "");
                        $("#IMG2").hide();
                        $("#BtnOK").attr("disabled", "disabled");
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

        function checkSubmitForm() {
            var tf = true;
            if ($.trim($("#TxtScheduleCode").val()) == "") {
                $("#TxtScheduleCode").focus();
                //		            var tt = $("#TxtScheduleCode");
                //		            var mes = $(tt).parent().prev(".word").text();
                //mes = mes.replace(":", "");
                alert("工程名称不能为空！");
                tf = false;
            }
            else
                if ($.trim($("#TxtMeasure").val()) == "") {
                    $("#TxtMeasure").focus();
                    //		            var tt = $("#TxtScheduleCode");
                    //		            var mes = $(tt).parent().prev(".word").text();
                    //mes = mes.replace(":", "");
                    alert("安全目标不能为空！");
                    tf = false;
                }
            return tf;
        }
        // 取消
        function btnCancelClick() {
            top.ui.closeTab();
        } 
    </script>
</head>
<body scroll="no">
    <form id="Form1" method="post" onsubmit="return checkSubmitForm();" runat="server">
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
                <td class="txt mustInput">
                    <asp:TextBox ID="TxtScheduleCode" ReadOnly="true" Width="100%" runat="server"></asp:TextBox>
                    <img id="IMG2" onclick="pickSchedule(TxtScheduleCode);" alt="" src="/images/contact.gif" style="cursor: hand" align="absmiddle" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="word">
                    安全目标：
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="TxtMeasure" Width="99%" runat="server"></asp:TextBox>
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
                        <JWControl:DropDownTree ID="DDTClass" runat="server"></JWControl:DropDownTree>
                    </div>
                </td></tr>
            <tr>
                <td class="word">
                    备&nbsp;&nbsp;&nbsp;&nbsp;注：
                </td>
                <td class="txt">
                    <asp:TextBox ID="Txtremark" Height="50px" Width="99%" TextMode="MultiLine" CssClass="textarea-input" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="divFooter2">
            <table class="tableFooter2">
                <tr>
                    <td>
                        <asp:Button ID="BtnOK" Text="保  存" OnClick="BtnOK_Click" runat="server" />
                        &nbsp;
                        <input id="Button1" onclick="btnCancelClick();" type="button" value="取 消" runat="server" />

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
