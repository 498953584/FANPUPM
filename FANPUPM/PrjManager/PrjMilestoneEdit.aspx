<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjMilestoneEdit.aspx.cs" Inherits="PrjManager_PrjMilestoneEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <script type="text/javascript" src="../Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js" charset="gb2312"></script>
    <script type="text/javascript" src="../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../Script/jw.js"></script>
    <script type="text/javascript" src="../Script/wf.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            Val.validate('form1', 'btnSave');
            
        });

        function selectOneUser() {

            jw.selectOneUser({ codeinput: 'hlfUserCode', nameinput: 'txtUserName' });
        }

        //取消按钮事件
        function CancelClick() {
            winclose('PrjMilestoneEdit.aspx', 'PrjMilestoneList.aspx', false);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr style="height: 100%;">
                <td style="height: 100%; vertical-align: top;">
                    <div id="divScroll" runat="server">
                        <table width="100%" height="100%" cellpadding="0" cellspacing="0" id="tb">
                            <tr>
                                <td>
                                    <fieldset style="width: 85%; margin: auto; text-align: center">
                                        <legend>九个里程碑基本资料</legend>
                                        <table id="table5" class="tableContent2" style="width: 95%" cellpadding="5px" cellspacing="0">
                                            <tr>
                                                <td class="word">
                                                    项目负责人
                                                </td>
                                                <td class="txt">
                                                    <asp:TextBox ID="txtUserName" ReadOnly="true" CssClass="select_input" imgclick="selectOneUser" runat="server"></asp:TextBox>
                                                    <asp:HiddenField ID="hlfUserCode" runat="server" />
                                                </td>
                                                <td class="word">
                                                    项目储备额（千元）
                                                </td>
                                                <td class="txt mustInput">
                                                    <asp:TextBox ID="txtStoreAmont" class="decimal_input {required:true, messages:{required:'项目储备金额不能为空'}}" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    今年预测（千元）
                                                </td>
                                                <td class="txt mustInput">
                                                    <asp:TextBox ID="txtForeCast" class="decimal_input {required:true, messages:{required:'今年预测金额不能为空'}}" runat="server"></asp:TextBox>
                                                </td>
                                                <td class="word">
                                                    明年预测
                                                </td>
                                                <td class="txt mustInput">
                                                    <asp:TextBox ID="txtNextForeCast" class="decimal_input {required:true, messages:{required:'明年预测金额不能为空'}}" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    （1）初步接洽
                                                </td>
                                                <td class="txt ">
                                                    <asp:TextBox ID="txtSton1" runat="server"></asp:TextBox>
                                                </td>
                                                <td class="word">
                                                    （2）提供样品
                                                </td>
                                                <td class="txt ">
                                                    <asp:TextBox ID="txtSton2" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    （3）样品质量被接纳
                                                </td>
                                                <td class="txt ">
                                                    <asp:TextBox ID="txtSton3" runat="server"></asp:TextBox>
                                                </td>
                                                <td class="word">
                                                    （4）投标
                                                </td>
                                                <td class="txt ">
                                                    <asp:TextBox ID="txtSton4" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    （5）中标
                                                </td>
                                                <td class="txt ">
                                                    <asp:TextBox ID="txtSton5" runat="server"></asp:TextBox>
                                                </td>
                                                <td class="word">
                                                    （6）下订单
                                                </td>
                                                <td class="txt ">
                                                    <asp:TextBox ID="txtSton6" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    （7）交货
                                                </td>
                                                <td class="txt ">
                                                    <asp:TextBox ID="txtSton7" runat="server"></asp:TextBox>
                                                </td>
                                                <td class="word">
                                                    （8）销售确认
                                                </td>
                                                <td class="txt ">
                                                    <asp:TextBox ID="txtSton8" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    （9）项目失败
                                                </td>
                                                <td class="txt ">
                                                    <asp:TextBox ID="txtSton9" runat="server"></asp:TextBox>
                                                </td>
                                                <td class="word">
                                                    上报日期
                                                </td>
                                                <td class="txt">
                                                    <asp:TextBox ID="txtRptDate" ReadOnly="true" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
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
                                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                                    <input type="button" id="btnCancel" value="取消" onclick="CancelClick()" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hlfRptDate" runat="server" />
    </form>
</body>
</html>
