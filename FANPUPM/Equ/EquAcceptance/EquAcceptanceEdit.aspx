<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquAcceptanceEdit.aspx.cs" Inherits="Equ_EquAcceptance_EquAcceptanceEdit" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>设备验收</title><link rel="stylesheet" type="text/css" href="../../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnAddDetail').hide();
            Val.validate('form1', 'btnSave');
            $('#btnCancel').click(btnCancelClick);
            setDisabled();
            gvwDetail();
            // 显示被截取的信息
            jw.tooltip();
        })
        // 取消按钮事件
        function btnCancelClick() {
            winclose('EquAcceptanceEdit', 'EquAcceptanceList.aspx', false);
        }
        // 设置readonly
        function setDisabled() {
            $('#txtPurchaseCode').attr('readonly', 'readonly');
            $('#txtAcceptor').attr('readonly', 'readonly');
            $('#txtAcceptDate').attr('readonly', 'readonly');
            $('#btnDelete').attr('disabled', 'disabled');
        }
        // 选择固定资产采购单
        function selectEquPurchase() {
            var url = '/StockManage/Storage/SinglePurchaseSelect.aspx?' + new Date().getTime();
            var purchase = { width: 1000, height: 550, url: url, title: '选择固定资产采购单' };
            top.ui.callback = function (purchase) {
                var code = (purchase.code).replace('["', '');
                code = code.replace('"]', '');
                $('#txtPurchaseCode').val(code);
                $('#hfldPurchaseCode').val(code);
                $('#btnAddDetail').click();
            };
            top.ui.openWin(purchase);
        }
        // 选择人员 
        function selectUser(codeInput, nameInput) {
            jw.selectOneUser({ codeinput: codeInput, nameinput: nameInput });
        }
        // 选择设备类型
        function selectEquType(txtEquType) {
            var hfldId = txtEquType.id.replace('txt', 'hfld');
            var url = '/Equ/Equipment/SelectEquipmentType.aspx?' + new Date().getTime();
            var equTypeInfo = { width: 1000, height: 550, url: url, title: '选择设备类别' };
            top.ui.callback = function (equTypeInfo) {
                $('#' + hfldId).val(equTypeInfo.id);
                $('#' + txtEquType.id).val(equTypeInfo.name);
            };
            top.ui.openWin(equTypeInfo);
        }
        // 选择供应商
        function pickCorp(txtCorp) {
            var txtID = txtCorp.id.replace('txt', 'hfld');
            jw.selectOneCorp({ idinput: txtID, nameinput: txtCorp.id });
        }
        // 设备验收明细
        function gvwDetail() {
            var jwTable = new JustWinTable('gvwDetail');
            setButton(jwTable, '', '', '', 'hfldDetailID');
            // 单击行
            jwTable.registClickTrListener(function () {
                $('#btnDelete').removeAttr('disabled');
            });
            // 单选
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
            // 全选
            jwTable.registClickAllCHKLitener(function () {
                if (this.checked) {
                    $('#btnDelete').removeAttr('disabled');
                }
                else {
                    $('#btnDelete').attr('disabled', 'disabled');
                }
            });
            replaceEmptyTable('emptyDetail', 'gvwDetail');
        }
        // 验证整数
        function intValidator(id) {
            var value = $('#' + id).val();
            var reg = new RegExp('^[1-9]*[1-9][0-9]*$');
            if (!reg.test(value)) {
                $('#' + id).val('0');
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
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="设备验收" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="divContent2">
        <table class="tableContent2" style="" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    固定资产采购单编号
                </td>
                <td class="txt mustInput" style="padding-right: 3px">
                    <span class="spanSelect" style="width: 100%;">
                        <asp:TextBox type="text" ID="txtPurchaseCode" CssClass="{required:true, messages:{required:'固定资产采购单编号不能为空'}}" Style="width: 90%; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px" runat="server"></asp:TextBox>
                        <img src="../../../images/icon.bmp" style="float: right;" alt="固定资产采购单编号" id="img1"
                            onclick="selectEquPurchase();" />
                    </span>
                </td>
                <td class="word">
                    验收人
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtAcceptor" Style="width: 90%; height: 15px; border: none;
                            line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择验收人" onclick="selectUser('hfldAcceptor','txtAcceptor');" src="../../images/icon.bmp"
                            style="float: right;" />
                        <asp:HiddenField ID="hfldAcceptor" runat="server" />
                    </span>
                </td>
            </tr>
            <tr>
                <td class="word">
                    验收时间
                </td>
                <td class="txt mustInput">
                    <asp:TextBox type="text" ID="txtAcceptDate" Width="100%" CssClass="{required:true, messages:{required:'验收时间不能为空'}}" onclick="WdatePicker()" runat="server"></asp:TextBox>
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
                        <asp:Button ID="btnAddDetail" OnClick="btnAddDetail_Click" runat="server" />
                        <asp:Button ID="btnDelete" Text="删除" OnClientClick="return confirm('您确定要删除吗?')" OnClick="btnDelete_Click" runat="server" />
                    </div>
                </td>
            </tr>
            <tr style="vertical-align: top">
                <td style="text-align: right">
                    <div>
                        <asp:GridView ID="gvwDetail" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwDetail_RowDataBound" DataKeyNames="Id" runat="server">
<EmptyDataTemplate>
                                <table id="emptyDetail" class="tab">
                                    <tr class="header">
                                        <th scope="col" style="width: 20px;">
                                            <asp:CheckBox ID="chkSelectAll" runat="server" />
                                        </th>
                                        <th scope="col" style="width: 25px;">
                                            序号
                                        </th>
                                        <th scope="col">
                                            设备名称
                                        </th>
                                        <th scope="col">
                                            设备类型
                                        </th>
                                        <th scope="col">
                                            规格型号
                                        </th>
                                        <th scope="col">
                                            生产厂家
                                        </th>
                                        <th scope="col">
                                            供应商
                                        </th>
                                        <th scope="col">
                                            数量
                                        </th>
                                        <th scope="col">
                                            单价
                                        </th>
                                        <th scope="col">
                                            技术参数
                                        </th>
                                        <th scope="col">
                                            随机资料
                                        </th>
                                        <th scope="col">
                                            备注
                                        </th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" runat="server" />
                                    </HeaderTemplate><ItemTemplate>
                                        <asp:CheckBox ID="chkSelectSingle" runat="server" />
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                        
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="设备名称"><ItemTemplate>
                                        <span class="tooltip" title=''>
                                            
                                        </span>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="设备类型"><ItemTemplate>
                                        <asp:TextBox ID="txtEquType" Width="90px" ondblclick="selectEquType(this)" ReadOnly="true" Style="background-color: #ffffc0;" CssClass="{required:true, messages:{required:'设备类型不能为空'}}" Text='<%# System.Convert.ToString(Eval("EquipmentType.Name"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                        <asp:HiddenField ID="hfldEquType" Value='<%# System.Convert.ToString(Eval("TypeId"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格型号"><ItemTemplate>
                                        
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="生产厂家">
<ItemTemplate>
                                        <asp:TextBox ID="txtManufacturer" Width="60px" ReadOnly="true" ondblclick="pickCorp(this)" Style="background-color: #ffffc0;" Text='<%# System.Convert.ToString(base.GetCorpName(Eval("ManufacturerId")), System.Globalization.CultureInfo.CurrentCulture) %>' No='<%# System.Convert.ToString(Container.DataItemIndex, System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                        <asp:HiddenField ID="hfldManufacturer" Value='<%# System.Convert.ToString(Eval("ManufacturerId"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="供应商">
<ItemTemplate>
                                        <asp:TextBox ID="txtCorp" Width="90px" ReadOnly="true" ondblclick="pickCorp(this)" Style="background-color: #ffffc0;" Text='<%# System.Convert.ToString(base.GetCorpName(Eval("SupplierId")), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                        <asp:HiddenField ID="hfldCorp" Value='<%# System.Convert.ToString(Eval("SupplierId"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="数量">
<ItemTemplate>
                                        <asp:TextBox ID="txtQty" Style="text-align: right;" Width="60px" onblur="intValidator(this.id);" Text='<%# System.Convert.ToString(Eval("Qty").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' No='<%# System.Convert.ToString(Container.DataItemIndex, System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单价">
<ItemTemplate>
                                        <asp:TextBox ID="txtUnitPrice" Style="text-align: right;" Width="60px" CssClass="decimal_input" Text='<%# System.Convert.ToString(System.Convert.ToDecimal(Eval("UnitPrice")).ToString("0.000"), System.Globalization.CultureInfo.CurrentCulture) %>' No='<%# System.Convert.ToString(Container.DataItemIndex, System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="技术参数"><ItemTemplate>
                                        <asp:TextBox ID="txtParameter" Style="text-align: right;" Width="60px" Text='<%# System.Convert.ToString(Eval("TechnicalParameter"), System.Globalization.CultureInfo.CurrentCulture) %>' No='<%# System.Convert.ToString(Container.DataItemIndex, System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="随机资料"><ItemTemplate>
                                        <asp:TextBox ID="txtInfo" Style="text-align: right;" Width="60px" Text='<%# System.Convert.ToString(Eval("Info"), System.Globalization.CultureInfo.CurrentCulture) %>' No='<%# System.Convert.ToString(Container.DataItemIndex, System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
                                        <asp:TextBox ID="txtNote" Style="text-align: right;" Width="60px" Text='<%# System.Convert.ToString(Eval("Note"), System.Globalization.CultureInfo.CurrentCulture) %>' No='<%# System.Convert.ToString(Container.DataItemIndex, System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
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
    
    <asp:HiddenField ID="hfldDetailID" runat="server" />
    
    <asp:HiddenField ID="hfldPurchaseCode" runat="server" />
    </form>
</body>
</html>
