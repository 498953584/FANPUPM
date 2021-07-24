<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddBudModify.aspx.cs" Inherits="ContractManage_PayoutModify_AddBudModify" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>添加预算变更</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="Script/AddBudModify.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
        Val.validate('form1');
        })
        function F() {
            if ($('#hfldIsWBSRelevance').val() == '1') {
                calUnitPrice();
            }
            else {
                calcTotal();
            }
        }
        // 计算单价 
        function calUnitPrice() {
            var qty = parseFloat($('#txtQuantity').val().replace(/\,/g, ''));
            var total = parseFloat($('#txtTotal').val().replace(/\,/g, ''));
            if (isNaN(qty) || isNaN(total)) return false;
            if (qty != 0) {
                var uprice = total / qty;
                $('#txtUnitPrice').val(uprice);
            }         
        }

        // 保存方法，后台调用
        function save() {
            var taskObj = JSON.parse($('#hfldTaskJson').val());
            if (typeof top.ui.callback == 'function') {
                top.ui.callback(taskObj);
            }
            top.ui.closeWin();
        }
    </script>
</head>
<body onload="modifyTypeChange();">
    <form id="form1" runat="server">
    <div class="divContent2">
        <div class="divContent2" id="outBud" runat="server">
            <table class="tableContent2" cellpadding="5px" cellspacing="0">
                <tr>
                    <td class="word">
                        变更类型
                    </td>
                    <td>
                        <select id="ModifyType" onchange="modifyTypeChange();" style="width: 202px;" runat="server"><option Value="0">清单外</option><option Value="1">清单内</option></select>
                    </td>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td class="word">
                        <asp:Label ID="lbltaskName" Text="上级任务" runat="server"></asp:Label>
                    </td>
                    <td class="txt">
                        <asp:HiddenField ID="TaskId" runat="server" />
                        <span class="spanSelect mustInput" style="width: 202px">
                            <input type="text" id="txtTaskName" class="readonly {required:true, messages:{required:'任务节点必须输入'}}" readonly="readonly" style="width: 175px; height: 15px; border: none; line-height: 16px;
                                margin: 1px 0px 1px 2px;" runat="server" />

                            <img id="img"alt="选择任务" onclick="selectBudTask();" src="../../images/icon.bmp" style="float: right;" />
                        </span>
                        <asp:HiddenField ID="orderNumber" runat="server" />
                    </td>
                    <td class="word">
                        <asp:Label ID="lblTaskCode" Text="清单编号" runat="server"></asp:Label>
                    </td>
                    <td class="txt mustInput">
                        <asp:TextBox ID="txtTaskCode" CssClass="{required:true, messages:{required:'清单编号必须输入'}}" Width="200px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="word">
                        <asp:Label ID="lblModifyTaskName" Text="节点名称" runat="server"></asp:Label>
                    </td>
                    <td class="txt mustInput">
                        <asp:TextBox ID="txtModifyTaskName" CssClass="{required:true, messages:{required:'变更内容必须输入'}}" Width="200px" runat="server"></asp:TextBox>
                    </td>
                    <td class="word">
                        单位
                    </td>
                    <td class="txt mustInput">
                        <asp:TextBox ID="txtUnit" Width="200px" CssClass="{required:true, messages:{required:'变更单位必须输入'}}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="word">
                        工程量
                    </td>
                    <td>
                        <input id="txtQuantity" style="width: 200px" class="decimal_input" onblur="F();" runat="server" />

                    </td>
                    <td class="word readonly">
                        单价
                    </td>
                    <td class="txt readonly">
                        <input id="txtUnitPrice" style="width: 200px" class="decimal_input" onblur="F();" readonly="readonly" runat="server" />

                    </td>
                </tr>
                <tr>
                    <td class="word readonly">
                        小计
                    </td>
                    <td class="txt readonly">
                        <input id="txtTotal" style="width: 200px" readonly="readonly" class="decimal_input" runat="server" />

                    </td>
                    <td class="word">
                        开始时间
                    </td>
                    <td class="txt">
                        <input type="text" id="txtStartDate" style="width: 200px" onclick="WdatePicker();" onblur="validateDate();" runat="server" />

                    </td>
                </tr>
                <tr>
                    <td class="word">
                        结束时间
                    </td>
                    <td class="txt">
                        <asp:TextBox onclick="WdatePicker();" Style="width: 200px" OnBlur="validateDate();" ID="txtEndDate" runat="server"></asp:TextBox>
                    </td>
                    <td class="word">
                        工期
                    </td>
                    <td>
                        <asp:TextBox ID="txtConstructPeriod" Width="200px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="word">
                        备注
                    </td>
                    <td colspan="3" class="txt">
                        <asp:TextBox ID="txtNode" TextMode="MultiLine" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="word" id="td1">
                        附件
                    </td>
                    <td colspan="3" style="text-align: left; padding-right: 0px;" class="txt">
                        <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="fileBud" Class="BudModify" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnOk" Text="保存" OnClick="btnOk_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" onclick="top.ui.closeWin();" />
                </td>
            </tr>
        </table>
    </div>
    <!-- 保存将要进行变更节点的TaskId-->
    <asp:HiddenField ID="hfldOriginalTaskId" runat="server" />
    <!-- 保存节点的json-->
    <asp:HiddenField ID="hfldTaskJson" runat="server" />
    <!-- 保存要变更节点的json-->
    <asp:HiddenField ID="hfldEditTaskJson" runat="server" />
    <!-- ModifyTaskId-->
    <asp:HiddenField ID="hfldModifyTaskId" runat="server" />
    
    <asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
    
    <asp:HiddenField ID="hfldContractId" runat="server" />
    </form>
</body>
</html>
