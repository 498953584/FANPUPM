<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Business_Data_ItemEdit.aspx.cs" Inherits="OPM_Business_Data_Business_Data_ItemEdit" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink3_ascx" Src="~/EPC/UserControl1/FileLink3.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link href="/StyleCss/PM4.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script src="../../../Web_Client/TreeNew.js" type="text/javascript"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script type="text/javascript" src="/Script/wf.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="/Script/DecimalInput.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

        });
        function chkDate() {
            var txtPCodeValue = Trim($('#txtPCode').val());
            var txtPNameValue = Trim($('#txtPName').val());
            var txtDesignDateValue = Trim($('#txtDesignDate').val());
            if ( txtPCodeValue== '') {
                top.ui.alert("图纸编号必须输入");
                return false;
            } else if (getWordsLength(txtPCodeValue) > 50) {
                top.ui.alert("请输入有效个字符，图纸编号不能超过25个汉字!");
                return false;
            }
            if (txtPNameValue == '') {
                top.ui.alert("图纸名称必须输入");
                return false;
            } else if (getWordsLength(txtPNameValue) > 50) {
                top.ui.alert("请输入有效个字符，图纸名称不能超过25个汉字!");
                return false;
            }
            if (txtDesignDateValue== '') {
                top.ui.alert("设计时间必须输入");
                return false;
            }
            var designDate = document.getElementById('txtDesignDate').value;
            var endDate = document.getElementById('txtEndDate').value;

            designDate = designDate.replace('/', '-').replace('/', '-');
            d1Arr = designDate.split('-');
            d2Arr = endDate.split('-');

            v1 = new Date(d1Arr[0], d1Arr[1], d1Arr[2]);
            v2 = new Date(d2Arr[0], d2Arr[1], d2Arr[2]);
            if (v2 != "") {
                if (v1 > v2) {
                    top.ui.alert("计划完成时间不能小于设计时间！");
                    return false;
                }
            }
            return true;
        }
        // 选择人员
        function selectUser() {
            jw.selectOneUser({ nameinput: 'txtDesigner', codeinput: 'hdfusercode' });
        }

        // 获得字符串实际长度,aa表示将汉字替换为两个英文字符
        function getWordsLength(str) {
            return str.replace(/[^\x00-\xff]/g, "aa").length;
        }
    </script>
</head>
<body>
    <form id="form1" style="overflow: hidden" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr style="height: 100%;">
            <td style="height: 100%; vertical-align: top;">
                <div id="divScroll" runat="server">
                    <table class="tableContent2" cellpadding="5px" cellspacing="0" width="80%">
                        <tr>
                            <td colspan="4" class="groupInfo">
                                图纸计划基本信息
                            </td>
                        </tr>
                        <tr>
                            <td class="word">
                                项目名称
                            </td>
                            <td class="txt">
                                <asp:TextBox ID="txtPrjName" ReadOnly="true" Height="15px" Width="100%" runat="server"></asp:TextBox>
                            </td>
                            <td class="word">
                                图纸计划编号
                            </td>
                            <td class="txt">
                                <asp:TextBox ID="txtBCode" ReadOnly="true" Height="15px" Width="100%" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="word">
                                图纸计划名称
                            </td>
                            <td class="txt">
                                <asp:TextBox ID="txtBName" ReadOnly="true" Height="15px" Width="100%" runat="server"></asp:TextBox>
                            </td>
                            <td class="word">
                                设计人
                            </td>
                            <td class="txt">
                                <asp:TextBox ID="txtDesigner" ReadOnly="true" Height="15px" Width="100%" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="word">
                                计划完成时间
                            </td>
                            <td class="txt">
                                <asp:TextBox ID="txtEndDate" ReadOnly="true" Height="15px" Width="100%" runat="server"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="word">
                                备注
                            </td>
                            <td colspan="3">
                                <textarea id="txtRemark" readonly="readonly" style="width: 100%; height: 75px;" runat="server"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="groupInfo">
                                图纸信息
                            </td>
                        </tr>
                        <tr>
                            <td class="word">
                                图纸编号
                            </td>
                            <td class="txt mustInput">
                                <asp:TextBox CssClass="{required:true, messages:{required:'图纸编号必须输入'}}" ID="txtPCode" runat="server"></asp:TextBox>
                            </td>
                            <td class="word">
                                图纸名称
                            </td>
                            <td class="elemTd mustInput">
                                <asp:TextBox CssClass="{required:true, messages:{required:'图纸名称必须输入'}}" ID="txtPName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="word">
                                设计时间
                            </td>
                            <td class="elemTd mustInput">

                                    <asp:TextBox ID="txtDesignDate" onclick="WdatePicker()" CssClass="{required:true, messages:{required:'设计时间必须输入'}}" runat="server"></asp:TextBox>
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
                        <tr>
                            <td class="word">
                                备注
                            </td>
                            <td colspan="3">
                                <textarea id="txtNode" style="width: 100%; height: 75px;" runat="server"></textarea>
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
            </td>
        </tr>
        <tr>
            <td>
                <div id="Div1" class="divFooter2" runat="server">
                    <table class="tableFooter2">
                        <tr>
                            <td>
                                <asp:Button ID="btnSave" Text="保存" OnClientClick="return chkDate()" OnClick="btnSave_Click" runat="server" />
                                <input type="button" id="btnCancel" value="取消" onclick="top.ui.closeTab();" runat="server" />

                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <div id="divFram" title="选择人员" style="display: none">
        <iframe id="ifFram" frameborder="0" width="100%" height="100%"></iframe>
    </div>
    <asp:HiddenField ID="hdnUID" runat="server" />
    </form>
</body>
</html>
