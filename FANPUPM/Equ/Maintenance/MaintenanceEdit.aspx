<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaintenanceEdit.aspx.cs" Inherits="Equ_Maintenance_MaintenanceEdit" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_usercontrol_selectresource_ascx" Src="~/StockManage/UserControl/SelectResource.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>编辑维护保养</title><link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnBindResource').hide();
            Val.validate('form1', 'btnSave');
            $('#btnDelete').attr('disabled', 'disabled');
            //取消按钮事件
            $('#btnCancel').click(function () {
                winclose('MaintenanceEdit', 'MaintenanceList.aspx', false);
            });
            jw.tooltip();
            //物资列表
            var jwTable = new JustWinTable('gvMaintenanceStock');
            //单击行
            jwTable.registClickTrListener(function () {
                $('#hfldStockIdChecked').val('["' + this.id + '"]');
                $('#btnDelete').removeAttr('disabled');
            });
            //单选
            jwTable.registClickSingleCHKListener(function () {
                var checkedChk = jwTable.getCheckedChk();
                var purchaseIds = '';
                if (checkedChk.length == 0) {
                    $('#btnDelete').attr('disabled', 'disabled');
                }
                else if (checkedChk.length == 1) {
                    $('#hfldStockIdChecked').val(jwTable.getCheckedChkIdJson(checkedChk));
                    var tr = getFirstParentElement(checkedChk[0].parentNode, 'TR');
                    purchaseIds = '"' + tr.purchaseId + '"';
                    $('#btnDelete').removeAttr('disabled');
                }
                else {
                    $('#hfldStockIdChecked').val(jwTable.getCheckedChkIdJson(checkedChk));
                    $('#btnDelete').removeAttr('disabled');
                    for (var i = 0; i < checkedChk.length; i++) {
                        var trs = getFirstParentElement(checkedChk[i].parentNode, 'TR');
                        purchaseIds += '"' + trs.purchaseId + '",';
                    }
                    purchaseIds = purchaseIds.substring(0, purchaseIds.length - 1);
                }
            });
            //全选
            jwTable.registClickAllCHKLitener(function () {
                if (this.checked) {
                    $('#hfldStockIdChecked').val(jwTable.getCheckedChkIdJson());
                    $('#btnDelete').removeAttr('disabled');
                    var checkedChk = jwTable.getAllChk();
                    var purchaseIds = '';
                    if (checkedChk.length > 0) {
                        if (checkedChk.length == 1) {
                            var tr = getFirstParentElement(checkedChk[0].parentNode, 'TR');
                            purchaseIds = '"' + tr.purchaseId + '"';
                        } else {
                            for (var i = 0; i < checkedChk.length; i++) {
                                var trs = getFirstParentElement(checkedChk[i].parentNode, 'TR');
                                purchaseIds += '"' + trs.purchaseId + '",';
                            }
                            purchaseIds = purchaseIds.substring(0, purchaseIds.length - 1);
                        }
                    } else {
                        purchaseIds = '';
                    }
                }
                else {
                    $('#hfldStockIdChecked').val('');
                    $('#btnDelete').attr('disabled', 'disabled');
                }
            });
            replaceEmptyTable('emptyStock', 'gvMaintenanceStock');
            calcTotal();    //计算小计
        });

        //选择设备
        function selectEqu() {
            var url = '/Equ/Equipment/SelectEquipment.aspx?' + new Date().getTime();
            var taskDivInfo = { width: 800, height: 500, url: url, title: '选择设备' };
            top.ui.callback = function (equInfo) {
                $('#hfldEquId').val(equInfo.id);
                $('#txtEquCode').val(equInfo.code);
                $('#txtEquName').val(equInfo.name)
                $('#txtSpecification').val(equInfo.specification)
            };
            top.ui.openWin(taskDivInfo);
        }
        // 选择供应商
        function pickCorp(param) {
            var txtID = param.id.replace('txt', 'hfld');
            jw.selectOneCorp({ idinput: txtID, nameinput: param.id });
        }
        //选择领用人
        function pickUser(param) {
            var txtID = param.id.replace('txt', 'hfld');
            jw.selectOneUser({ codeinput: txtID, nameinput: param.id });
        }
        // 计算小计
        function calcTotal() {
            $('#gvMaintenanceStock .num, #gvMaintenanceStock .price').blur(function () {
                var tr = getFirstParentElement(this, 'TR');
                var num = _strToDecimal($(tr).find('.num').val());
                var price = _strToDecimal($(tr).find('.price').val());
                var total = _formatDecimal(num * price);
                $(tr).find('.total').html(total);
                calcSumTotal();
            });
        }
        // 计算合计
        function calcSumTotal() {
            var sumTotal = 0.0;
            $('#gvMaintenanceStock .total').each(function () {
                sumTotal += _strToDecimal($(this).html());
            });
            $('#gvMaintenanceStock ._total_').html(_formatDecimal(sumTotal));
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divContent2" class="divContent2">
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    上报日期
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtReportDate" Width="100%" onclick="WdatePicker()" class="{required:true, messages:{required:'上报日期不能为空！'}}" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    设备编号
                </td>
                <td class="txt mustInput">
                    <span class="spanSelect" style="width: 100%">
                        <input type="text" id="txtEquCode" class="{required:true, messages:{required:'设备编号不能为空！'}}" readonly="readonly" style="width: 88%; height: 15px; border: none;
                            line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

                        <img alt="选择设备" onclick="selectEqu();" src="../../images/icon.bmp" style="float: right;" />
                    </span>
                    <asp:HiddenField ID="hfldEquId" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    设备名称
                </td>
                <td>
                    <input type="text" id="txtEquName" class="readonly" readonly="readonly" width="100%" runat="server" />

                </td>
                <td class="word">
                    规格
                </td>
                <td>
                    <input type="text" id="txtSpecification" class="readonly" readonly="readonly" width="100%" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="word">
                    预计维修费用
                </td>
                <td>
                    <asp:TextBox ID="txtBudCost" Height="15px" Width="100%" Text="0.000" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    预计维修时间(天)
                </td>
                <td>
                    <asp:TextBox ID="txtBudTime" Height="15px" Width="100%" Text="0.000" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    预计维修内容
                </td>
                <td>
                    <asp:TextBox ID="txtBudContent" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    实际维修费用
                </td>
                <td>
                    <asp:TextBox ID="txtRealityCost" Height="15px" Width="100%" Text="0.000" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    实际维修时间(天)
                </td>
                <td>
                    <asp:TextBox ID="txtRealityTime" Height="15px" Width="100%" Text="0.000" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    实际维修内容
                </td>
                <td>
                    <asp:TextBox ID="txtRealityContent" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    原因分析
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtReason" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            
        </table>
        <table class="tableContent2" cellpadding="5" cellspacing="0">
            <tr>
                <td>
                    <hr class="sp" />
                </td>
            </tr>
            <tr>
                <td>
                    <div class="headerDiv" style="text-align: left;">
                        <MyUserControl:stockmanage_usercontrol_selectresource_ascx ID="SelectResource1" Text="从材料库中选择" ButtonId="btnBindResource" TypeCode="" runat="server" />
                        <asp:Button ID="btnDelete" Text="删除" OnClientClick="return confirm('您确定要删除吗?')" OnClick="btnDelete_Click" runat="server" />
                    </div>
                </td>
            </tr>
            <tr style="vertical-align: top">
                <td style="text-align: right">
                    <div class="pagediv">
                        <asp:GridView ID="gvMaintenanceStock" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvMaintenanceStock_RowDataBound" DataKeyNames="ResourceId" runat="server">
<EmptyDataTemplate>
                                <table id="emptyStock" class="tab">
                                    <tr class="header">
                                        <th scope="col" style="width: 20px;">
                                            <asp:CheckBox ID="chkSelectAll" runat="server" />
                                        </th>
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
                                            型号
                                        </th>
                                        <th scope="col">
                                            品牌
                                        </th>
                                        <th scope="col">
                                            技术参数
                                        </th>
                                        <th scope="col">
                                            单位
                                        </th>
                                        <th scope="col">
                                            数量
                                        </th>
                                        <th scope="col">
                                            采购价格
                                        </th>
                                        <th scope="col">
                                            小计
                                        </th>
                                        <th scope="col">
                                            供应商
                                        </th>
                                        <th scope="col">
                                            领用人
                                        </th>
                                        <th scope="col">
                                            领用日期
                                        </th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" runat="server" />
                                    </HeaderTemplate><ItemTemplate>
                                        <asp:CheckBox ID="chkSelectSingle" runat="server" />
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                        
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资编号">
<ItemTemplate>
                                        
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="物资名称">
<ItemTemplate>
                                        <span class="tooltip" title=''>
                                            
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="规格">
<ItemTemplate>
                                        <span class="tooltip" title=''>
                                            
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="型号">
<ItemTemplate>
                                        <span class="tooltip" title=''>
                                            
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="品牌">
<ItemTemplate>
                                        <span class="tooltip" title=''>
                                            
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="技术参数">
<ItemTemplate>
                                        <span class="tooltip" title=''>
                                            
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单位">
<ItemTemplate>
                                        
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="数量"><ItemTemplate>
                                        <asp:TextBox ID="txtNumber" Style="text-align: right;" Width="60px" CssClass="decimal_input num" Text='<%# System.Convert.ToString((Eval("Quantity").ToString() == "") ? "0.000" : System.Convert.ToDecimal(Eval("Quantity")).ToString("0.000"), System.Globalization.CultureInfo.CurrentCulture) %>' No='<%# System.Convert.ToString(Container.DataItemIndex, System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="价格"><ItemTemplate>
                                        <asp:TextBox ID="txtSprice" Style="text-align: right;" Width="60px" CssClass="decimal_input price" Text='<%# System.Convert.ToString((Eval("Price").ToString() == "") ? "0.000" : System.Convert.ToDecimal(Eval("Price")).ToString("0.000"), System.Globalization.CultureInfo.CurrentCulture) %>' No='<%# System.Convert.ToString(Container.DataItemIndex, System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="小计" ItemStyle-CssClass="total"><ItemTemplate>
                                        
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="供应商">
<ItemTemplate>
                                        <input type="text" id="txtCorp" class="{required:true, messages:{required:'供应商不能为空'}}" ondblclick="pickCorp(this)" readonly="readonly" style="width: 90px; background-color: #ffffc0;" value='<%# System.Convert.ToString(Eval("CorpName"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />

                                        <asp:HiddenField ID="hfldCorp" Value='<%# System.Convert.ToString(Eval("corpId"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="领用人">
<ItemTemplate>
                                        <input type="text" id="txtReceivePerson" class="{required:true, messages:{required:'领用人不能为空'}}" ondblclick="pickUser(this)" readonly="readonly" style="width: 65px; background-color: #ffffc0;" value='<%# System.Convert.ToString(WebUtil.GetUserNames(Eval("ReceivePerson").ToString()), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />

                                        <asp:HiddenField ID="hfldReceivePerson" Value='<%# System.Convert.ToString(Eval("ReceivePerson"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="领用日期">
<ItemTemplate>
                                        <asp:TextBox ID="txtReceiveDate" onclick="WdatePicker()" CssClass="{required:true, messages:{required:'领用日期不能为空'}}" Style="height: 15px; width: 65px; text-align:left; background-color: #ffffc0;" Text='<%# System.Convert.ToString((Eval("ReceiveDate").ToString() == "") ? "" : System.Convert.ToDateTime(Eval("ReceiveDate")).ToString("yyyy-MM-dd"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
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
    
    <asp:Button ID="btnBindResource" OnClick="btnBindResource_Click" runat="server" />
    
    <asp:HiddenField ID="hfldStockIdChecked" runat="server" />
    <!--存储从材料库中选择物资的物资ID-->
    <asp:HiddenField ID="hfldResourceId" runat="server" />
    
    <asp:HiddenField ID="hfldMaintenanceId" runat="server" />
    </form>
</body>
</html>
