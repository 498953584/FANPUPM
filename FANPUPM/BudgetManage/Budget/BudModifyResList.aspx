<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BudModifyResList.aspx.cs" Inherits="BudgetManage_Budget_BudModifyResList" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title></title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="../../Script/Budget/BudgetPait.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnBindResource').hide();
            replaceEmptyTable('emptyTable', 'gvResource');
            var table = new JustWinTable('gvResource');
            var gvResource = new JustWinTable('gvResource');
            Val.validate('form1', 'btnSave');
            setButton2(gvResource, 'btnDelete', 'btnEdit', 'btnTemImport', 'hfldPurchaseChecked');
            setWidthHeight();
            if ($('#hfldIsAllowEditRes').val() == '0') {
                $('#btnDelete').attr('disabled', 'disabled');
                $('#btnSave').attr('disabled', 'disabled');
                $('#btnClose').attr('disabled', 'disabled');
            }
        });

        //当乘积过大是，禁止保存
        function computTotal(index, txt) {
            tb = document.getElementById('gvResource');
            var cells = tb.rows[parseFloat(index) + 1].cells;
            var quantityStr = cells[9].children[0].value;
            var priceStr = cells[10].children[0].value;
            var quantity = parseFloat(parseFloat(quantityStr.replace(/\,/g, '')));
            var price = parseFloat(parseFloat(priceStr.replace(/\,/g, '')));
            if (!isNaN(quantity) && !isNaN(price)) {
                var total = quantity * price;
                if (total > 1500000000) {
                    alert('系统提示：\n\n该资源的乘积过大!');
                    txt.value = 0.000;
                }
            }
        }

        function setWidthHeight() {
            //$("#divBudget").width($("#divTop").width() - 5);
            $("#divBudget").height($(this).height() - $("#divBottom").height() - $("#divTop").height() - 5);
        }
        function validate() {
            if (!document.getElementById('gvResource') || document.getElementById('gvResource').firstChild.childNodes.length == 1) {
                alert('系统提示：\n\n请选择资源');
                return false;
            }
        }
        function setButton2(jwTable, btnDel, btnUpdate, btnQuery, hdChkId) {
            if (!jwTable.table) return;
            if (jwTable.table.firstChild.childNodes.length == 1) {
                //table中没有数据
                return;
            }
            jwTable.registClickTrListener(function () {
                if (hdChkId != '')
                    document.getElementById(hdChkId).value = this.id;
                if (this.guid) {
                    document.getElementById(btnQuery).guid = this.guid;
                }
                var checkedChk = jwTable.getCheckedChk();
                if (checkedChk.length > 0) {
                    if ($('#hfldIsAllowEditRes').val() != '0') {
                        document.getElementById(btnDel).removeAttribute('disabled');
                    }
                } else {
                    document.getElementById(btnDel).setAttribute('disabled', 'disabled');
                }

            });
            jwTable.registClickSingleCHKListener(function () {
                var checkedChk = jwTable.getCheckedChk();
                if (checkedChk.length == 0) {
                    document.getElementById(btnDel).setAttribute('disabled', 'disabled');
                }
                else if (checkedChk.length == 1) {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson(checkedChk);
                    if ($('#hfldIsAllowEditRes').val() != '0') {
                        document.getElementById(btnDel).removeAttribute('disabled');
                    }
                }
                else {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson(checkedChk);

                    var checkedChks = jwTable.getCheckedChk();
                }
            });
            jwTable.registClickAllCHKLitener(function () {
                if (this.checked) {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson();
                    var checkedChks = jwTable.getAllChk();
                    if ($('#hfldIsAllowEditRes').val() != '0') {
                        document.getElementById(btnDel).removeAttribute('disabled');
                    }
                }
                else {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = '';
                    document.getElementById(btnDel).setAttribute('disabled', 'disabled');
                }

                if (jwTable.table.rows.length == 2 && this.checked == true) {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson();

                }
            });
            showTooltip('tooltip', 10);
        };

        // 增加资源
        function selectResource() {
            var src = '/StockManage/UserControl/SelectResource.aspx?type=2&TypeCode=0';
            top.ui._ResourceList = window;
            top.ui.callback = function (obj) {
                $('#hfldResourceId').val(obj.id);
                $('#btnBindResource').click();
            }
            top.ui.openWin({ title: '选择资源', url: src, width: 1010, height: 500 });
        }
        
    </script>
    <style type="text/css">
        .descTd
        {
            text-align: right;
            font-weight: normal;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader">
        <table style="border: 0px; width: 100%;">
            <tr id="divTop">
                <td align="right">
                    
                    <input type="button" style="width: 80px;" id="btnAdd" value="增加资源" onclick="selectResource();" />
                    <asp:Button Width="80px" ID="btnDelete" Text="删除" disabled="disabled" OnClientClick="return confirm('您确定要删除吗??')" OnClick="btnDelete_Click" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="vertical-align: top;">
                    <div class="pagediv" id="divBudget" style="height: 200px;">
                        <asp:GridView ID="gvResource" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvResource_RowDataBound" DataKeyNames="resourceId,id,price,number" runat="server">
<EmptyDataTemplate>
                                <table id="emptyTable" style="background-color: Red;" class="tab" width="100%">
                                    <tr class="header">
                                        <td style="width: 20px">
                                            <asp:CheckBox ID="cbAllBox" runat="server" />
                                        </td>
                                        <td style="width: 25px">
                                            序号
                                        </td>
                                        <td style="width: 100px">
                                            资源编号
                                        </td>
                                        <td style="width: 150px">
                                            资源名称
                                        </td>
                                        <td>
                                            规格
                                        </td>
                                        <td>
                                            型号
                                        </td>
                                        <td>
                                            品牌
                                        </td>
                                        <td>
                                            技术参数
                                        </td>
                                        <td>
                                            单位
                                        </td>
                                        <td>
                                            单价
                                        </td>
                                        <td>
                                            数量
                                        </td>
                                        <td>
                                            损耗系数
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
                                        <asp:CheckBox ID="cbAllBox" runat="server" />
                                    </HeaderTemplate>

<ItemTemplate>
                                        <asp:CheckBox ID="cbBox" ToolTip='<%# System.Convert.ToString(Eval("Id"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="资源编号" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Left"><ItemTemplate>
                                        <asp:Label ID="lblCode" Text='<%# System.Convert.ToString(Eval("resourceCode"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="资源名称">
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
</asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="10%"><ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "unit") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单价" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="90px"><ItemTemplate>
                                        <asp:TextBox Style="text-align: right;" Width="90px" Height="15px" ID="txtPrice" CssClass="decimal_input" onblur="computTotal(this.No,this)" Text='<%# System.Convert.ToString((Eval("price").ToString() == "") ? "0.000" : Eval("price"), System.Globalization.CultureInfo.CurrentCulture) %>' No='<%# System.Convert.ToString(Container.DataItemIndex, System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数量" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="90px"><ItemTemplate>
                                        <asp:TextBox Style="text-align: right;" Width="90px" Height="15px" ID="txtNumber" CssClass="decimal_input" onblur="computTotal(this.No,this)" Text='<%# System.Convert.ToString((Eval("number").ToString() == "") ? "0.000" : Eval("number"), System.Globalization.CultureInfo.CurrentCulture) %>' No='<%# System.Convert.ToString(Container.DataItemIndex, System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="损耗系数" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="90px"><ItemTemplate>
                                        <asp:TextBox Style="text-align: right;" Width="90px" Height="15px" ID="txtLossCoefficient" CssClass="decimal_input" Text='<%# System.Convert.ToString(Eval("LossCoefficient"), System.Globalization.CultureInfo.CurrentCulture) %>' No='<%# System.Convert.ToString(Container.DataItemIndex, System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 100%; height: 15%;">
                    <div id="divBottom" class="divFooter2" style="width: 99.9%">
                        <table class="tableFooter2">
                            <tr>
                                <td>
                                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_ServerClick" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    <asp:Button ID="btnBindResource" OnClick="btnBindResource_Click" runat="server" />
    <asp:HiddenField ID="hfldVersion" runat="server" />
    
    <asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
    
    <asp:HiddenField ID="hfldIsAllowEditRes" runat="server" />
    
    <asp:HiddenField ID="hfldResourceId" runat="server" />
    </form>
</body>
</html>
