<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="criterionedit.aspx.cs" Inherits="CriterionEdit" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_quaitysafety_usercontrol_fileinfo_ascx" Src="~/EPC/QuaitySafety/UserControl/fileinfo.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_quaitysafety_usercontrol_criterion_ascx" Src="~/EPC/QuaitySafety/UserControl/criterion.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>
        <%=CriterionEdit._title %></title>
    <script src="../../Script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Script/Watermark/jquery_Watermark.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var mark = document.getElementById("hdnmark").value;
            if (mark == 1) {
                GetWaterMarked(window, '/images/yinzh.jpg', 'this');
            }
        });

        function Getdept() {
            jw.selectOneDep({ idinput: 'hdnDept', nameinput: 'TxtPublisher' });
        }

        // 选择记录类型
        function GetClass() {

            var type = $('#hdnDacumClass').val();
            var pid = jw.getRequestParam('ClassID');

            var url = '/EPC/QuaitySafety/selectDatumClass.aspx?type=' + type + '&pid=' + pid;


            top.ui.callback = function (o) {
                $('#hdnClass').val(o.id);
                $('#txtClass').val(o.name);
            }
            top.ui.openWin({ title: '选择类型', url: url, width: 400, height: 600 });

        }

        function checklen(e, maxlength) {
            if (e.value.length > maxlength) {
                alert('输入长度不能大于' + maxlength);
                e.focus();
                return false;
            }
        }
        var textval = $("#TxtCriterionName").val();
        $(function () {
            $("#DDTClass").val($("#hidenClass").val());
            if (jw.getRequestParam("Type") == "See") {
                $("#IMG2").hide();
                $("#IMG1").hide();
                //                $("#TxtCriterionName").val(textval);
                $("#Textbox1").attr("disabled", "disabled");
                $("#FileLink1_But_UpFile").attr("disabled", "disabled");
            } else if (jw.getRequestParam("Type") == "Update") {
                $("#txtClass").attr("readonly", "readonly");
                $("#TxtPublisher").attr("readonly", "readonly");
            }
            else if (jw.getRequestParam("Type") == "Add") {
                $("#txtClass").attr("readonly", "readonly");
                $("#TxtPublisher").attr("readonly", "readonly");
            }
            var temFl = $("#HiddenField1").val();
            if (temFl == "3") {
                $("#c1").show();
                $("#c2").show();
            } else {
                $("#c1").hide();
                $("#c2").hide();
            }
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
        });

        function checkFormSubmit() {
            var tf = true;
            return tf;
        }
        // 取消
        function btnCancelClick() {
            top.ui.closeTab();
        } 
    </script>
</head>
<body>
    <form id="Form2" method="post" onsubmit="return checkFormSubmit();" runat="server">
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="hdnmark" runat="server" />
    <div class="divContent2">
        <table width="100%">
            <tr>
                <td class="divHeader">
                    <asp:Label ID="LblTitle" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="tableContent2" id="Table2" cellspacing="0" cellpadding="5px" width="100%"
            border="0">
            <tr>
                <td class="word" style="width: 129px">
                    规范名称：
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="TxtCriterionName" Width="250px" Columns="21" MaxLength="80" runat="server"></asp:TextBox>
                </td>
                <td class="word" style="width: 129px">
                    发布单位：
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="TxtPublisher" Width="250px" runat="server"></asp:TextBox>
                    <img id="IMG1" src="/images/contact.gif" onclick="Getdept();" style="cursor: hand" align="absmiddle" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="word" style="width: 129px">
                    记录类型：
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtClass" Width="250px" runat="server"></asp:TextBox>
                    <img alt="" src="/images/contact.gif" onclick="GetClass();" id="IMG2" style="cursor: hand" align="absmiddle" runat="server" />

                </td>
                <td class="word" style="width: 129px">
                    发 布 人：
                </td>
                <td class="txt">
                    <asp:Label ID="PublisherYhmc" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="word">
                    附件：
                </td>
                <td class="txt" colspan="3">
                    <MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word" style="width: 129px; vertical-align: top;">
                    备&nbsp;&nbsp;&nbsp;&nbsp;注：
                </td>
                <td class="txt" colspan="3">
                    <asp:TextBox ID="Textbox1" Width="100%" CssClass="" Rows="5" TextMode="MultiLine" Height="216px" MaxLength="400" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="divFooter2">
            <table class="tableFooter2">
                <tr>
                    <td class="td-submit" colspan="4">
                        <asp:Button ID="Btn_Save" Text="确 定" OnClick="Btn_Save_Click" runat="server" />&nbsp;
                        <input class="button-normal" id="Button1" type="button" value="取 消" name="Submit3" onclick="btnCancelClick();" runat="server" />
&nbsp;
                        <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
                        <input id="hdnDacumClass" type="hidden" name="hdnDacumClass" runat="server" />

                        <input id="hdnClass" type="hidden" name="hdnClass" runat="server" />

                    </td>
                </tr>
            </table>
        </div>
    </div>
    <input type="hidden" id="hdnDept" runat="server" />

    </form>
</body>
</html>
