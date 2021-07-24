<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BalanceEdit.aspx.cs" Inherits="ContractManage_PayoutBalance_BalanceEdit" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>支出合同结算</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            if (getRequestParam('Action') == 'Query' || !getRequestParam('Action')) {
                setAllInputDisabled();
            }

            Val.validate('form1');
            setAuditState();
        });

        //格式化三位小数 
        function getFormat(num) {
            var numFormat;
            if (num.toFixed) {
                numFormat = num.toFixed(3);
            } else {
                var f3 = Math.pow(10, 3);
                numFormat = Math.round(num * f3) / f3;
            }
            return numFormat;
        }
        function thePurchaseMonenyChange(index) {
            tb = document.getElementById('gvwPurchaseplanStock');
            var cells = tb.rows[parseFloat(index) + 1].cells;
            if (isNaN(cells[8].children[0].value)) {
                alert("输入的结算数量不正确，请重新输入！");
                cells[8].children[0].value = cells[8].children[1].value;
                return;
            }
            // 本次结算数量
            var thisBalanceQuantity = parseFloat(cells[8].children[0].value);
            if (thisBalanceQuantity < 0) {
                alert("输入的结算数量不能小于零，请重新输入！");
                cells[8].children[0].value = cells[8].children[1].value;
                return;
            }
            // 采购数量
            var totalQuantity = parseFloat(cells[6].innerText);
            // 未入库数量
            var notArrivedQuantity = parseFloat(cells[12].innerText);
            // 已入库数量
            var arrivedQuantity = getFormat(totalQuantity) - getFormat(notArrivedQuantity);
            // 已结算数量
            var alreadyBalanceQuantity = parseFloat(cells[10].innerText);
            // 总的结算数量 = 已结算数量 + 本次结算数量
            var allBalanceQuantity = alreadyBalanceQuantity + thisBalanceQuantity;
            if (allBalanceQuantity > arrivedQuantity) {
                if (allBalanceQuantity > totalQuantity) {
                    alert("结算的数量已经大于采购的数量，请重新输入！");
                    cells[8].children[0].value = cells[8].children[1].value;
                    return;
                } else {
                    if (!confirm("结算的数量已经大于已经入库的数量，是否继续？")) {
                        cells[8].children[0].value = cells[8].children[1].value;
                        return;
                    }
                }
            }
            cells[8].children[0].value = getFormat(thisBalanceQuantity);
            cells[8].children[1].value = getFormat(thisBalanceQuantity);
            var sprice = parseFloat(cells[7].innerText);
            var thisTimeTotal = getFormat(thisBalanceQuantity) * getFormat(sprice);
            cells[9].innerText = getFormat(thisTimeTotal);

            var contractMoney = 0.00;
            for (var i = 1; i < tb.rows.length - 1; i++) {
                contractMoney += parseFloat(tb.rows[i].cells[9].innerText);
            }
            tb.rows[tb.rows.length - 1].cells[1].innerText = getFormat(contractMoney);
        }

        $(document).ready(function () {
            $('#hfldBtnMeasure').hide();
            $('#hfldBtnConBalanceItem').hide();
            $('#BtnDel').attr('disabled', true);

            // 管理扣项
            var itemTable = new JustWinTable('gvIncometItem');
            replaceEmptyTable('EmptyItem', 'gvIncometItem');
            itemTable.registClickTrListener(function () {
                $('#hfldInItemIds').val($(this).attr('id'));
                $('#btnDelItem').attr('disabled', false);
                //$('#btnEdit').attr('disabled', false);
            });
            itemTable.registClickSingleCHKListener(function () {
                var checkCHK = itemTable.getCheckedChk();
                if (checkCHK.length > 0) {
                    $('#btnDelItem').attr('disabled', false);
                    $('#hfldInItemIds').val(itemTable.getCheckedChkIdJson(checkCHK));
                } else {
                    $('#btnDelItem').attr('disabled', true);
                }
                if (checkCHK.length == '1') {
                    $('#btnEdit').attr('disabled', false);
                } else {
                    $('#btnEdit').add('disabled', true);
                }
            });
            itemTable.registClickAllCHKLitener(function () {
                var checkCHK = itemTable.getAllChk();
                $('#btnEdit').attr('disabled', true);
                if ($(this).attr('checked')) {
                    $('#btnDelItem').attr('disabled', false);
                    $('#hfldInItemIds').val(itemTable.getCheckedChkIdJson(checkCHK));
                } else {
                    $('#btnDelItem').attr('disabled', true);
                }
            });

            // 合同计量
            var rptTable = new JustWinTable('gvConract');
            replaceEmptyTable('emptyBudget', 'gvConract');
            rptTable.registClickTrListener(function () {
                $('#hfldBtnDelMeasureIds').val($(this).attr('id'));
                $('#BtnDel').attr('disabled', false);
            });
            rptTable.registClickSingleCHKListener(function () {
                var checkCHK = rptTable.getCheckedChk();
                $('#hfldBtnDelMeasureIds').val(rptTable.getCheckedChkIdJson(checkCHK));
                if (checkCHK.length > 0) {
                    $('#BtnDel').attr('disabled', false);
                } else {
                    $('#BtnDel').attr('disabled', true);
                }
            });
            rptTable.registClickAllCHKLitener(function () {
                var checkCHK = rptTable.getAllChk();
                $('#hfldBtnDelMeasureIds').val(rptTable.getCheckedChkIdJson(checkCHK));
                if ($(this).attr('checked')) {
                    $('#BtnDel').attr('disabled', false);
                } else {
                    $('#BtnDel').attr('disabled', true);
                }
            });
            var consTaskTable = new JustWinTable('gvTask');
            replaceEmptyTable('emptyConsTask', 'gvTask');
            Val.validate('form1', 'btnSave')

            var purchaseplanStockTable = new JustWinTable('gvwPurchaseplanStock');
        });
        function AddItem() {
            top.ui._AddBalanceItem = window;
            var title = '新增管理扣项';
            var url = '/ContractManage/IncometBalance/AddBalanceItem.aspx?action=Add&balanceId=' + $('#hdGuid').val();
            top.ui.callback = function () {
                $('#hfldBtnConBalanceItem').click();
            }
            top.ui.openWin({ title: title, url: url, width: 650, height: 450 });
        }

        function EditItem() {
            top.ui._AddBalanceItem = window;
            var title = '编辑管理扣项';
            var url = '/ContractManage/IncometBalance/AddBalanceItem.aspx?action=Edit&balanceId=' + $('#hdGuid').val();
            top.ui.callback = function () {
                $('#hfldBtnConBalanceItem').click();
            }
            top.ui.openWin({ title: title, url: url, width: 650, height: 450 });
        }
        //添加合同计量
        function addContractMeasure() {
            top.ui._ConInMeasureList = window;
            var url = '/ContractManage/PayoutBalance/ConPayMeasureList.aspx?contractId=' + $('#hfldContractId').val() + '&BalanceId=' + $('#hdGuid').val();
            top.ui.openWin({ title: '添加合同计量', url: url });
            top.ui.callback = function (obj) {
                if (obj.id != null) {
                    $('#hfldMeasureIds').val(obj.id);
                    $('#hfldBtnMeasure').click();
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader2">
        <table class="tableHeader">
            <tr>
                <td>
                    支出合同结算
                </td>
            </tr>
        </table>
    </div>
    <div id="divContent" class="divContent2">
        <table id="tableContent" class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td colspan="4" class="groupInfo">
                    合同基本信息
                </td>
            </tr>
            <tr>
                <td class="word">
                    合同编号
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtContractCode" ReadOnly="true" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    合同名称
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtContractName" Height="15px" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    合同类型
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtContractType" ReadOnly="true" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    合同最终金额
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtContractMoney" class="easyui-numberbox" data-options="min:-99999999999,max:99999999999,precision:3,groupSeparator:','" Height="15px" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    甲方
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtAName" ReadOnly="true" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    乙方
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtBname" ReadOnly="true" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    签订时间
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtSignedDate" Height="15px" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    签订地点
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtAddress" Height="15px" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="groupInfo">
                    结算单信息
                </td>
            </tr>
            <tr>
                <td class="word">
                    结算编号
                </td>
                <td class="txt mustInput">
                    <asp:TextBox Height="15px" Width="100%" CssClass="{required:true, messages:{required:'结算编号必须输入'}}" ID="txtBalanceCode" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    结算金额
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtBalanceMoney" Height="15px" Width="100%" CssClass=" easyui-numberbox {required:true,number:true, messages:{required:'结算金额必须输入',number:'合同金额格式错误'}}" data-options="min:-99999999999,max:99999999999,precision:3,groupSeparator:','" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    结算方式
                </td>
                <td class="txt mustInput">
                    <asp:DropDownList ID="dropBalanceMode" class="{required:true, messages:{required:'请选择结算方式'}}" Height="19px" Width="102%" runat="server"></asp:DropDownList>
                </td>
                <td class="word">
                    差额
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtDiffAmount" class="easyui-numberbox" data-options="min:-99999999999,max:99999999999,precision:3,groupSeparator:','" Height="15px" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    结算对象
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtBalanceObj" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    <asp:CheckBox ID="chkContainPending" AutoPostBack="true" OnCheckedChanged="chkContainPendint_CheckedChanged" runat="server" />
                </td>
                <td class="txt">
                    包含待审数据
                </td>
            </tr>
            <tr>
                <td class="word">
                    结算人
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtBalancePerson" Height="15px" Width="100%" CssClass="{required:true, messages:{required:'结算人必须输入'}}" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    付款方式
                </td>
                <td class="txt mustInput">
                    <asp:DropDownList ID="dropPayMode" class="{required:true, messages:{required:'请选择付款方式'}}" Height="19px" Width="102%" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="word">
                    结算日期
                </td>
                <td class="txt mustInput">
                    
                    <asp:TextBox ID="txtBalanceDate" onclick="WdatePicker()" CssClass="{required:true, messages:{required:'结算日期必须输入'}}" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    备注
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtNotes" Height="50px" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    附件
                </td>
                <td colspan="3" style="padding-right: 0px;">
                    <MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    录入人
                </td>
                <td class="txt mustInput readonly">
                    <asp:TextBox ID="txtInputPerson" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    录入时间
                </td>
                <td class="txt mustInput readonly">
                    <asp:TextBox ReadOnly="true" ID="txtInputDate" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div id="divPurchase" runat="server">
            <table class="tableContent2" cellpadding="5px" cellspacing="0">
                <tr>
                    <td class="groupInfo">
                        采购单
                    </td>
                </tr>
                <tr style="vertical-align: top">
                    <td style="width: 100%;">
                        <asp:GridView ID="gvwPurchaseplanStock" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwPurchaseplanStock_RowDataBound" DataKeyNames="ResourceCode" runat="server">
<EmptyDataTemplate>
                                <table id="emptyStock" class="tab">
                                    <tr class="header">
                                        <th scope="col" style="width: 25px;">
                                            序号
                                        </th>
                                        <th scope="col">
                                            物资编号
                                        </th>
                                        <th scope="col">
                                            物资名称
                                        </th>
                                        <th scope="col">
                                            规格
                                        </th>
                                        <th scope="col">
                                            单位
                                        </th>
                                        <th scope="col">
                                            采购数量
                                        </th>
                                        <th scope="col">
                                            采购价格
                                        </th>
                                        <th scope="col">
                                            本次结算数量
                                        </th>
                                        <th scope="col">
                                            本次结算金额
                                        </th>
                                        <th scope="col">
                                            已结算数量
                                        </th>
                                        <th scope="col">
                                            已结算金额
                                        </th>
                                        <th scope="col">
                                            应到而<br/>未到数量
                                        </th>
                                        <th scope="col">
                                            供应商
                                        </th>
                                        <th scope="col">
                                            型号
                                        </th>
                                        <th scope="col">
                                            品牌
                                        </th>
                                        <th scope="col">
                                            技术参数
                                        </th>
                                        <th scope="col">
                                            采购单号
                                        </th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资编号" HeaderStyle-Width="100px">
<ItemTemplate>
                                        <%# Eval("ResourceCode") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="物资名称">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("ResourceName").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("ResourceName").ToString(), 10) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="规格">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("Specification").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("Specification").ToString(), 10) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="50px">
<ItemTemplate>
                                        <%# Eval("Name") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="库存量" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                        <%# WebUtil.GetStockNumberByCode(Eval("ResourceCode").ToString()).ToString() %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="采购数量" HeaderStyle-Width="70px">
<ItemTemplate>
                                        <%# Convert.ToDecimal(Eval("ContractNumber")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="采购价格" HeaderStyle-Width="70px">
<ItemTemplate>
                                        <%# Convert.ToDecimal(Eval("sprice")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="本次结算数量" HeaderStyle-Width="40px"><ItemTemplate>
                                        <asp:TextBox ID="txtThisTimeArrivaledQuantity" Style="text-align: right;" Width="100%" onblur="thePurchaseMonenyChange(this.No)" Text='<%# Convert.ToString((Eval("ThisTimeArrivaledQuantity").ToString() == "") ? "0.000" : Convert.ToDecimal(Eval("ThisTimeArrivaledQuantity")).ToString("0.000")) %>' No='<%# Convert.ToString(Container.DataItemIndex) %>' runat="server"></asp:TextBox>
                                        <asp:HiddenField ID="hfldArrivaledQuantity" Value='<%# Convert.ToString((Eval("ThisTimeArrivaledQuantity").ToString() == "") ? "0.000" : Convert.ToDecimal(Eval("ThisTimeArrivaledQuantity")).ToString("0.000")) %>' runat="server" />
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="本次结算金额" HeaderStyle-Width="40px">
<ItemTemplate>
                                        <%# (Eval("ThisTimeTotal").ToString() == "") ? "0.000" : Convert.ToDecimal(Eval("ThisTimeTotal")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="已结算数量" HeaderStyle-Width="70px">
<ItemTemplate>
                                       <%# (Eval("AllAlreadyBalanceQuantity").ToString() == "") ? "0.000" : Convert.ToDecimal(Eval("AllAlreadyBalanceQuantity")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="已结算金额" HeaderStyle-Width="40px">
<ItemTemplate>
                                        <%# (Eval("AlreadyTotal").ToString() == "") ? "0.000" : Convert.ToDecimal(Eval("AlreadyTotal")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="应到而<br/>未到数量" HeaderStyle-Width="40px">
<ItemTemplate>
                                        <%# (Eval("NoArrivaledQuantity").ToString() == "") ? "0.000" : Convert.ToDecimal(Eval("NoArrivaledQuantity")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="供应商" HeaderStyle-Width="100px">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("CorpName").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("CorpName").ToString(), 10) %>
                                        </span>
                                        <asp:HiddenField ID="hfldCorp" Value='<%# Convert.ToString(Eval("corp")) %>' runat="server" />
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="型号">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("ModelNumber").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("ModelNumber").ToString(), 10) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="品牌">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("brand").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("brand").ToString(), 10) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="技术参数">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("TechnicalParameter").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("TechnicalParameter").ToString(), 10) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="采购单号" HeaderStyle-Width="50px">
<ItemTemplate>
                                        <%# Eval("pscode") %>
                                    </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="groupInfo">
                    管理扣费
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" cellpadding="1px" cellspacing="0">
                        <tr style="vertical-align: top;">
                            <td style="text-align: left;">
                                <input id="btnAddItem" type="button" value="新增" onclick="AddItem();" runat="server" />

                                <input id="btnEdit" type="button" value="编辑" disabled="true" OnServerClick="btnEdit_click" runat="server" />

                                <asp:Button Text="删除" ID="btnDelItem" OnClientClick="return confirm('您确定要删除吗？')" disabled="disabled" OnClick="btnDelItem_Click" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gvIncometItem" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvgvIncometItem_RowDataBound" DataKeyNames="Id,Type,Qty,UnitPrice" runat="server">
<EmptyDataTemplate>
                                        <table id="EmptyItem" class="tab" width="100%">
                                            <tr class="header">
                                                <td style="width: 20px">
                                                    <asp:CheckBox ID="chkAll" runat="server" />
                                                </td>
                                                <td style="width: 25px;">
                                                    序号
                                                </td>
                                                <td align="center">
                                                    扣费项目名称
                                                </td>
                                                <td align="center">
                                                    扣费数量
                                                </td>
                                                <td align="center">
                                                    单价
                                                </td>
                                                <td align="center" style="width: 50px;">
                                                    小计
                                                </td>
                                                <td>
                                                    扣费类型
                                                </td>
                                                <td align="center">
                                                    备注
                                                </td>
                                                <td align="center">
                                                    附件
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                                <asp:CheckBox ID="cbAllBox" runat="server" />
                                            </HeaderTemplate><ItemTemplate>
                                                <asp:CheckBox ID="cbBox" ToolTip='<%# Convert.ToString(Eval("Id")) %>' runat="server" />
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="扣费项目名称"><ItemTemplate>
                                                <%# Eval("Name") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数量" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                <%# Eval("Qty") %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单价" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                <%# Eval("UnitPrice") %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="小计" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="扣费类型">
<ItemTemplate>
                                                <%# getTypeName(Eval("Type").ToString()) %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
                                                <span class="tooltip" title="<%# Eval("Note") %>">
                                                    <%# StringUtility.GetStr(Eval("Note").ToString(), 25) %>
                                                </span>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件"><ItemTemplate>
                                                <%# StringUtility.FilesBind(Eval("Id").ToString(), "ContractBudBalance") %>
                                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div id='divTarget'>
            <table class="tableContent2" cellpadding="5px" cellspacing="0">
                <tr>
                    <td>
                        <input type="button" value="添加合同计量" style="width: 90px;" onclick="addContractMeasure();" />
                        <asp:Button ID="BtnDel" Width="90px" Text="删除合同计量" OnClick="delContractConsReport" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvConract" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="ConsReportId,State,PrjId" runat="server">
<EmptyDataTemplate>
                                <table id="emptyBudget" class="tab">
                                    <tr class="header">
                                        <th scope="col" style="width: 20px;">
                                            <input id="chk1" type="checkbox" />
                                        </th>
                                        <th scope="col" style="width: 25px;">
                                            序号
                                        </th>
                                        <th scope="col">
                                            录入人
                                        </th>
                                        <th scope="col">
                                            上报时间
                                        </th>
                                        <th scope="col">
                                            安全工作记录
                                        </th>
                                        <th scope="col">
                                            添加方式
                                        </th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                        <asp:CheckBox ID="cbAllBox" runat="server" />
                                    </HeaderTemplate><ItemTemplate>
                                        <asp:CheckBox ID="cbBox" ToolTip='<%# Convert.ToString(Container.DataItemIndex + 1) %>' runat="server" />
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="录入人">
<ItemTemplate>
                                        <%# Eval("InputUser") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="上报时间">
<ItemTemplate>
                                        <%# Common2.GetTime(Eval("InputDate")) %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="安全工作记录">
<ItemTemplate>
                                        <asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" CssClass="tooltip" ToolTip='<%# Convert.ToString(StringUtility.StripTagsCharArray(Eval("WorkCard").ToString())) %>' runat="server"><%# StringUtility.GetStr(StringUtility.StripTagsCharArray(Eval("WorkCard").ToString()), 25) %></asp:HyperLink>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="添加方式">
<ItemTemplate>
                                        <%# (Eval("Type").ToString() == "0") ? "手动录入" : "变更录入" %>
                                    </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="padding: 2px; width: 75px; margin-top: 5px;">
                            合同计量汇总</div>
                        <asp:GridView ID="gvTask" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvTask_RowDataBound" DataKeyNames="TaskId,ConsTaskId" runat="server">
<EmptyDataTemplate>
                                <table id="emptyConsTask" class="tab">
                                    <tr class="header">
                                        <th scope="col" style="width: 20px;">
                                            <input id="chk1" type="checkbox" />
                                        </th>
                                        <th scope="col" style="width: 25px;">
                                            序号
                                        </th>
                                        <th scope="col">
                                            任务编码
                                        </th>
                                        <th scope="col">
                                            分项工作
                                        </th>
                                        <th scope="col">
                                            单位
                                        </th>
                                        <th scope="col">
                                            累计工作量
                                        </th>
                                        <th scope="col">
                                            剩余工作量
                                        </th>
                                        <th scope="col">
                                            累计完成量
                                        </th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
                                        <asp:CheckBox ID="cbAllBox" runat="server" />
                                    </HeaderTemplate>

<ItemTemplate>
                                        <asp:CheckBox ID="cbBox" ToolTip='<%# Convert.ToString(Eval("ConsTaskId")) %>' runat="server" />
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="任务编码" ItemStyle-HorizontalAlign="Left"><HeaderTemplate>
                                        任务编码
                                    </HeaderTemplate><ItemTemplate>
                                        <%# Eval("TaskCode") %>
                                        <asp:HiddenField ID="hfldTaskId" Value='<%# Convert.ToString(Eval("TaskId")) %>' runat="server" />
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
                                        分项工作
                                    </HeaderTemplate>

<ItemTemplate>
                                        <%# Eval("TaskName") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="Quantity" HeaderText="总工作量" ItemStyle-HorizontalAlign="Right" /><asp:BoundField DataField="Unit" HeaderText="单位" ItemStyle-HorizontalAlign="Right" /><asp:BoundField DataField="SumAccountingQuantity" HeaderText="累计审核量" ItemStyle-HorizontalAlign="Right" /><asp:BoundField DataField="SurplusQuantity" HeaderText="剩余工作量" ItemStyle-HorizontalAlign="Right" /><asp:TemplateField HeaderStyle-Width="80px">
<HeaderTemplate>
                                        累计完成量
                                    </HeaderTemplate>

<ItemTemplate>
                                        <asp:TextBox ID="txtCompleteQuantity" CssClass="decimal_input" Style="text-align: right;" Width="80px" Text='<%# Convert.ToString(Eval("CompleteQuantity")) %>' No='<%# Convert.ToString(Container.DataItemIndex) %>' runat="server"></asp:TextBox>
                                        <asp:HiddenField ID="hfldCompleteQuantity" Value='<%# Convert.ToString(Eval("CompleteQuantity")) %>' runat="server" />
                                    </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="divFooter" class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" onclick="winclose('BalanceEdit', 'PayoutBalanceEdit.aspx', false)" />
                </td>
            </tr>
        </table>
    </div>
    
    <asp:HiddenField ID="hfldPrjid" runat="server" />
    
    <asp:HiddenField ID="hfldContractId" runat="server" />
    
    <asp:HiddenField ID="hfldMeasureIds" runat="server" />
    
    <asp:Button ID="hfldBtnMeasure" OnClick="hfldBtnMeasure_Click" runat="server" />
    
    <asp:HiddenField ID="hfldBtnDelMeasureIds" runat="server" />
    <asp:HiddenField ID="hdGuid" runat="server" />
    
    <asp:HiddenField ID="hfldRptIDs" runat="server" />
    
    <asp:HiddenField ID="hfldPrjKindJson" runat="server" />
    
    <asp:HiddenField ID="hfldInItemIds" runat="server" />
    <asp:Button ID="hfldBtnConBalanceItem" OnClick="hfldBtnConBalanceItem_Click" runat="server" />
    
    <asp:HiddenField ID="hfldconBalanceItem" runat="server" />
    </form>
</body>
</html>
