<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurchaseView.aspx.cs" Inherits="StockManage_Purchase_PurchaseView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink_ascx" Src="~/EPC/UserControl1/FileLink.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>采购单</title>
    <style type="text/css" media="print">
        .noprint
        {
            display: none;
        }
    </style>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../script/Config2.js"></script>
    <script type="text/javascript" src="../script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script src="../../Script/jw.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            setAnnxReadOnly('flAnnx');
            registerChkDiffEvent();
            replaceEmptyTable('emptyPurchaseplanStock', 'gvwPurchaseplanStock')
        });
        function registerChkDiffEvent() {
            if (!document.getElementById('chkDiff')) return false;
            if (!document.getElementById('trDiff')) return false;
            addEvent(document.getElementById('chkDiff'), 'click', function () {
                if (this.checked) {
                    document.getElementById('trDiff').className = '';
                }
                else {
                    document.getElementById('trDiff').className = 'noprint';
                }
            });
        }
    </script>
</head>
<body id="print1">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top; min-width: 800px;">
        <tr>
            <td class="divHeader">
                采购单
                <input type="button" id="btnDy" style="float: right;" class="noprint" onclick="winPrint()"
                    value=" 打 印 " />
            </td>
        </tr>
        <tr style="height: 1px;">
            <td>
                <table id="bllProducer" cellpadding="0" cellspacing="0" style="border-style: none;"
                    class="viewTable">
                    <tr>
                        <td style="border-style: none;">
                            制单人:&nbsp;&nbsp;<asp:Label ID="lblBllProducer" runat="server"></asp:Label>
                        </td>
                        <td style="border-style: none; text-align: right">
                            打印日期:&nbsp;&nbsp;<asp:Label ID="lblPrintDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 1px;">
            <td style="vertical-align: top;">
                <table border="0px" cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td class="descTd">
                            采购编号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPpcode" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            编制日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="DateInTime" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            合同
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblContract" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            项目
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblProject" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            编制人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="txtPerson" Text="" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            附件
                        </td>
                        <td class="elemTd">
                            <MyUserControl:epc_usercontrol1_filelink_ascx ID="flAnnx" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            说明
                        </td>
                        <td colspan="3" style="word-break: break-all;">
                            <asp:Label ID="lblExplain" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%; height: 50%; margin-top: 10px;" border="0">
                    <tr style="vertical-align: top;">
                        <td>
                            <asp:GridView ID="gvwPurchaseplanStock" Width="100%" AutoGenerateColumns="false" CssClass="viewTable" DataKeyNames="scode" runat="server">
<EmptyDataTemplate>
                                    <table class="tab" id="emptyPurchaseplanStock">
                                        <tr class="header">
                                            <td>
                                                <asp:CheckBox ID="chkSelectAll" runat="server" />
                                            </td>
                                            <td style="width: 20px">
                                                序号
                                            </td>
                                            <td>
                                                物资编号
                                            </td>
                                            <td>
                                                物资名称
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
                                                库存量
                                            </td>
                                            <td>
                                                数量
                                            </td>
                                            <td>
                                                采购价格
                                            </td>
                                            <td>
                                                小计
                                            </td>
                                            <td>
                                                供应商
                                            </td>
                                            <td>
                                                到货日期
                                            </td>
                                            <td>
                                                预算量
                                            </td>
                                            <td>
                                                单价
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="20px"><ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资编号" HeaderStyle-Width="50px"><ItemTemplate>
                                            <div style="word-break: break-all;">
                                                <%# Eval("ResourceCode") %>
                                            </div>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资名称" HeaderStyle-Width="80px">
<ItemTemplate>
                                            <div style="word-break: break-all;">
                                                <%# Eval("ResourceName") %>
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="规格">
<ItemTemplate>
                                            <div style="word-break: break-all;">
                                                <%# Eval("Specification") %>
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="型号">
<ItemTemplate>
                                            <div style="word-break: break-all;">
                                                <%# Eval("ModelNumber") %>
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="品牌">
<ItemTemplate>
                                            <div style="word-break: break-all;">
                                                <%# Eval("brand") %>
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="技术参数">
<ItemTemplate>
                                            <div style="word-break: break-all;">
                                                <%# Eval("TechnicalParameter") %>
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="50px"><ItemTemplate>
                                            <div style="word-break: break-all;">
                                                <%# Eval("Name") %>
                                            </div>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="库存量" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                            <%# WebUtil.GetStockNumberByCode(Eval("scode").ToString()).ToString() %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数量" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                            <%# Eval("number") %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="采购价格" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                            <%# Eval("sprice") %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="小计" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                            <%# System.Convert.ToDecimal((Eval("Total") == System.DBNull.Value) ? "0" : Eval("Total")).ToString("0.000") %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="供应商">
<ItemTemplate>
                                            <div style="word-break: break-all;">
                                                <%# Eval("CorpName") %>
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="到货日期" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
                                            
                                            <div style="text-align: center; word-break: break-all;">
                                                <%# (Eval("arrivalDate") == System.DBNull.Value) ? "" : System.Convert.ToDateTime(Eval("arrivalDate")).ToString("yyyy-MM-dd") %>
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="预算量" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                            <%# Eval("ReadyNumber") %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单价" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                            <%# Eval("Price") %>
                                        </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                        </td>
                    </tr>
                    <tr id="trDiff" style="vertical-align: top;">
                        <td>
                            <div id="diffTitle" style="position: relative;" runat="server">
                                指标对比表
                                <div class="noprint" style="height: 20px; width: 100%; left: 0px; bottom: 0px; text-align: right;
                                    position: absolute; font-weight: normal;">
                                    <asp:CheckBox ID="chkDiff" Style="float: right;" Checked="true" Text="打印" runat="server" />
                                </div>
                            </div>
                            <div style="width: 100%;">
                                <asp:GridView AutoGenerateColumns="false" Width="100%" ID="gvwDiff" CssClass="viewTable" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="2%">
<ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="ResourceName" HeaderText="名称" HeaderStyle-Width="15%" /><asp:BoundField DataField="Specification" HeaderText="规格" HeaderStyle-Width="5%" /><asp:BoundField DataField="Brand" HeaderText="品牌" HeaderStyle-Width="5%" /><asp:BoundField DataField="PurPlanNumber" HeaderText="物资采购审核计划量" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="PurNumber" HeaderText="本次采购量" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="ConResPrice" HeaderText="合同预算报价" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%" ItemStyle-CssClass="decimal_input" /><asp:TemplateField HeaderText="目标成本预算价" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                                <%# System.Convert.ToDecimal(Eval("BudPrice")).ToString("0.000") %>
                                            </ItemTemplate></asp:TemplateField><asp:BoundField DataField="PuredPrice" HeaderText="以往采购价" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%" ItemStyle-CssClass="decimal_input" /><asp:BoundField DataField="PurPrice" HeaderText="本次采购价" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%" ItemStyle-CssClass="decimal_input" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr id="trAudit" style="vertical-align: top;">
                        <td>
                            <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="073" BusiClass="001" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
    
    <asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
    </form>
</body>
</html>
