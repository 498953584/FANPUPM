<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProgressDetailEdit.aspx.cs" Inherits="Equ_RequirePlan_ProgressDetailEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>工程进度明细</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnBindEquType').hide();
            Val.validate('form1', 'btnSave');
            $('#btnDelete').attr('disabled', 'disabled');
            //取消按钮事件
            $('#btnCancel').click(function () {
                winclose('ProgressDetailEdit', 'RequirePlanList.aspx', false);
            });
            repairStock();     //工程进度明细
            $('#btnSelectEquType').click(function () {
                var url = '/Equ/Equipment/MultiSelectEquType.aspx?' + new Date().getTime();
                var equTypeInfo = { width: 900, height: 500, url: url, title: '选择设备类别' };
                top.ui.callback = function (equTypeInfos) {
                    $('#hfldEquTypeId').val(equTypeInfos);
                    $('#btnBindEquType').click();
                };
                top.ui.openWin(equTypeInfo);
            });
        });
        //工程进度明细
        function repairStock() {
            //明细列表
            var jwTable = new JustWinTable('gvProgressDetail');
            setButton(jwTable, '', '', '', 'hfldIdChecked');
            //单击行
            jwTable.registClickTrListener(function () {
                $('#btnDelete').removeAttr('disabled');
            });
            //单选
            jwTable.registClickSingleCHKListener(function () {
                var checkedChk = jwTable.getCheckedChk();
                if (checkedChk.length == 0) {
                    $('#btnDelete').attr('disabled', 'disabled');
                }
                else if (checkedChk.length == 1) {
                    $('#btnDelete').removeAttr('disabled');
                }
                else {
                    $('#btnDelete').removeAttr('disabled');
                }
            });
            //全选
            jwTable.registClickAllCHKLitener(function () {
                if (this.checked) {
                    $('#btnDelete').removeAttr('disabled');
                }
                else {
                    $('#btnDelete').attr('disabled', 'disabled');
                }
            });
            replaceEmptyTable('emptyProgressDetail', 'gvProgressDetail');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divContent2" class="divContent2">
        <table class="tableContent2" cellpadding="5" cellspacing="0">
            <tr>
                <td>
                    <div class="headerDiv" style="text-align: left;">
                        <input type="button" id="btnSelectEquType" value="选择设备类别" style="width: 90px;" />
                        <asp:Button ID="btnBindEquType" OnClick="btnBindEquType_Click" runat="server" />
                        <asp:Button ID="btnDelete" Text="删除" OnClientClick="return confirm('您确定要删除吗?')" OnClick="btnDelete_Click" runat="server" />
                    </div>
                </td>
            </tr>
            <tr style="vertical-align: top">
                <td style="text-align: right">
                    <div class="pagediv">
                        <asp:GridView ID="gvProgressDetail" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvProgressDetail_RowDataBound" DataKeyNames="id" runat="server">
<EmptyDataTemplate>
                                <table id="emptyProgressDetail" class="tab">
                                    <tr class="header">
                                        <th scope="col" style="width: 20px;">
                                            <asp:CheckBox ID="chkSelectAll" runat="server" />
                                        </th>
                                        <th scope="col" style="width: 25px;">
                                            序号
                                        </th>
                                        <th scope="col">
                                            设备类别名称
                                        </th>
                                        <th scope="col">
                                            预计进场时间
                                        </th>
                                        <th scope="col">
                                            预计退场时间
                                        </th>
                                        <th scope="col">
                                            预计进场地点
                                        </th>
                                        <th scope="col">
                                            设备来源
                                        </th>
                                        <th scope="col">
                                            预算费用
                                        </th>
                                        <th scope="col">
                                            数量
                                        </th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" runat="server" />
                                    </HeaderTemplate><ItemTemplate>
                                        <asp:CheckBox ID="chkSelectSingle" runat="server" />
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                        
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="设备类别名称">
<ItemTemplate>
                                        
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="预计进场日期" HeaderStyle-Width="70px">
<ItemTemplate>
                                        <asp:TextBox ID="txtEnterDate" onclick="WdatePicker()" Style="height: 15px;
                                            width: 95%; text-align: left;" Text='<%# System.Convert.ToString((Eval("EnterDate") == null) ? "" : System.Convert.ToDateTime(Eval("EnterDate")).ToString("yyyy-MM-dd"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="预计出场日期" HeaderStyle-Width="70px">
<ItemTemplate>
                                        <asp:TextBox ID="txtOutDate" onclick="WdatePicker()" Style="height: 15px;
                                            width: 95%; text-align: left;" Text='<%# System.Convert.ToString((Eval("OutDate") == null) ? "" : System.Convert.ToDateTime(Eval("OutDate")).ToString("yyyy-MM-dd"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="预计进场地点">
<ItemTemplate>
                                        <asp:TextBox ID="txtEnterArea" Style="height: 15px; width: 95%; text-align: left;" Text='<%# System.Convert.ToString(Eval("EnterArea"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="设备来源">
<ItemTemplate>
                                        <asp:TextBox ID="txtEquipmentSource" Style="height: 15px; width: 95%;
                                            text-align: left;" Text='<%# System.Convert.ToString(Eval("EquipmentSource"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="预算费用" HeaderStyle-Width="80px"><ItemTemplate>
                                        <asp:TextBox ID="txtBudCost" Style="text-align: right;" Width="95%" CssClass="decimal_input" Text='<%# System.Convert.ToString((Eval("BudCost").ToString() == "") ? "0.000" : System.Convert.ToDecimal(Eval("BudCost")).ToString("0.000"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数量" HeaderStyle-Width="80px"><ItemTemplate>
                                        <asp:TextBox ID="txtQuantity" Style="text-align: right;" Width="95%" CssClass="decimal_input" Text='<%# System.Convert.ToString((Eval("Quantity").ToString() == "") ? "0.000" : System.Convert.ToDecimal(Eval("Quantity")).ToString("0.000"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" />
                </td>
            </tr>
        </table>
    </div>
    
    <asp:HiddenField ID="hfldIdChecked" runat="server" />
    <!--存储从材料库中选择物资的物资ID-->
    <asp:HiddenField ID="hfldEquTypeId" runat="server" />
    
    <asp:HiddenField ID="hfldProgressId" runat="server" />
    
    <asp:HiddenField ID="hfldAction" runat="server" />
    </form>
</body>
</html>
