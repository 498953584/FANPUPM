<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Business_Data_Edit.aspx.cs" Inherits="OPM_Business_Data_Business_Data_Edit" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink3_ascx" Src="~/EPC/UserControl1/FileLink3.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link href="/StyleCss/PM4.css" rel="stylesheet" type="text/css" />
<link type="text/css" rel="Stylesheet" href="/Script/My97DatePicker/skin/WdatePicker.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script src="../../../Web_Client/TreeNew.js" type="text/javascript"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script type="text/javascript" src="/Script/wf.js"></script>
    <script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="/Script/DecimalInput.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        function chkDate() {
            if ($('#txtBCode').val() == '') {
                top.ui.alert("图纸计划编号必须输入");
                return false;
            }
            if ($('#txtBName').val() == '') {
                top.ui.alert("图纸计划名称必须输入");
                return false;
            }
            if ($('#txtBeginDate').val()== '') {
                top.ui.alert("设计时间必须输入");
                return false;
            }
            if ($('#txtEndDate').val() == '') {
                top.ui.alert("计划完成时间必须输入");
                return false;
            }
            if ($('#txtDutyUser').val()== '') {
                top.ui.alert("责任人必须输入");
                return false;
            }
            var beginDate = document.getElementById('txtBeginDate').value;
            var endDate = document.getElementById('txtEndDate').value;

            beginDate = beginDate.replace('/', '-').replace('/', '-');

            d1Arr = beginDate.split('-');
            d2Arr = endDate.split('-');

            v1 = new Date(d1Arr[0], d1Arr[1], d1Arr[2]);
            v2 = new Date(d2Arr[0], d2Arr[1], d2Arr[2]);

            if (v2 != "") {
                if (v1 > v2) {
                    top.ui.alert("计划完成时间不能小于设计时间！");
                    return false;
                }
            }

            var txtBNameLength = getWordsLength(document.getElementById('txtBName').value.toString());
            var txtDutyUserLength = getWordsLength(document.getElementById('txtDutyUser').value.toString());
            var txtBCodeLength = getWordsLength(document.getElementById('txtBCode').value.toString());
            var txtDesignerLength = getWordsLength(document.getElementById('txtDesigner').value.toString());
            if (txtBNameLength > 50) {
                top.ui.alert("图纸计划名称不能超过25个汉字！");
                return false;
            }
            if (txtDutyUserLength > 50) {
                top.ui.alert("请输入有效个字符，责任人不能超过25个汉字！");
                return false;
            }
            if (txtBCodeLength > 50) {
                top.ui.alert("请输入有效个字符，图纸计划编号不能超过25个汉字！");
                return false;
            }
            if (txtDesignerLength > 50) {
                top.ui.alert("请输入有效个字符，设计者不能超过25个汉字！");
                return false;
            }
            return true;
        }
        //选择人员
        function selectUser() {
            jw.selectOneUser({ nameinput: 'txtDesigner', codeinput: 'hdfusercode' });
        }
        //获得字符串实际长度,aa表示将汉字替换为两个英文字符
        function getWordsLength(str) {
            return str.replace(/[^\x00-\xff]/g, "aa").length;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 89%; text-align: center;">
        <table class="tableContent2" cellpadding="5px" cellspacing="0" width="80%">
            <tr>
                <td class="word">
                    图纸计划编号
                </td>
                <td class="txt mustInput">
                    <asp:TextBox CssClass="{required:true, messages:{required:'图纸计划编号必须输入'}}" ID="txtBCode" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    图纸计划名称
                </td>
                <td class="elemTd mustInput">
                    <asp:TextBox CssClass="{required:true, messages:{required:'图纸计划名称必须输入'}}" ID="txtBName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    设计时间
                </td>
                <td class="elemTd mustInput">
                    <asp:TextBox ID="txtBeginDate" onclick="WdatePicker()" CssClass="{required:true, messages:{required:'设计时间必须输入'}}" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    计划完成时间
                </td>
                <td class="elemTd mustInput">
                     <asp:TextBox ID="txtEndDate" onclick="WdatePicker()" CssClass="{required:true, messages:{required:'计划完成时间必须输入'}}" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    设计人
                </td>
                <td class="">
                    <span class="spanSelect" style="width: 100%;">
                        <input id="txtDesigner" type="text" style="width: 90%; height: 15px; border: none;
                            line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

                        <img src="/images/icon.bmp" style="float: right;" alt="选择人员" id="imgPerson" onclick="selectUser();" />
                    </span>
                    <asp:HiddenField ID="hdfusercode" runat="server" />
                </td>
                <td class="word">
                    责任人
                </td>
                <td class="elemTd mustInput">
                    <asp:TextBox CssClass="{required:true, messages:{required:'责任人必须输入'}}" ID="txtDutyUser" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    附件
                </td>
                <td class="txt">
                    <MyUserControl:epc_usercontrol1_filelink3_ascx ID="FileLink1" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                </td>
                <td colspan="3">
                </td>
            </tr>
            <tr style="display: none">
                <td class="word">
                    未完原因
                </td>
                <td colspan="3">
                    <textarea id="txtCause" style="width: 100%; height: 75px;" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td class="word">
                    备注
                </td>
                <td colspan="3">
                    <textarea id="txtRemark" style="width: 100%; height: 75px;" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td class="word">
                    创建人
                </td>
                <td class="elemTd mustInput">
                    <asp:TextBox CssClass="txtCss" contenteditable="false" ID="txtAddUser" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    创建时间
                </td>
                <td class="elemTd mustInput">
                    <asp:TextBox CssClass="txtCss" contenteditable="false" ID="txtAddTime" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div id="divFooter" class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClientClick="return chkDate()" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" onclick="top.ui.closeTab();" runat="server" />

                </td>
            </tr>
        </table>
    </div>
    <div id="divFram" title="" style="display: none">
        <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <asp:HiddenField ID="hdnUID" runat="server" />
    </form>
</body>
</html>
