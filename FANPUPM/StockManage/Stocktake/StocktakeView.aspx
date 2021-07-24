<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StocktakeView.aspx.cs" Inherits="StockManage_Stocktake_StocktakeView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink_ascx" Src="~/EPC/UserControl1/FileLink.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>盘点结存</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

    <style type="text/css" media="print">
        .noprint
        {
            display: none;
        }
    </style>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="../script/Config2.js"></script>
    <script type="text/javascript" src="../script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript">
        //        addEvent(window, 'load', function() {
        //            setAnnxReadOnly('flAnnx');
        //        });
        addEvent(window, 'load', function () {
            showTooltip('tooltip', 10);
        });
    </script>
</head>
<body id="print1">
    <form id="form1" runat="server">
    <div id="print" style="max-width: 1300px;">
        <table class="tab" style="vertical-align: top; min-width: 850px;">
            <tr>
                <td class="divHeader">
                    查看盘点结存
                    <input type="button" id="btnDy" style="float: right;" class="noprint" onclick="winPrint()"
                        value=" 打 印 " />
                </td>
            </tr>
            <tr style="height: 1px;">
                <td>
                    <table cellpadding="0" cellspacing="0" class="viewTable">
                        <tr>
                            <td class="descTd">
                                盘点编码
                            </td>
                            <td class="elemTd">
                                <asp:Label ID="lblCode" Text="" runat="server"></asp:Label>
                            </td>
                            <td class="descTd">
                                盘点名称
                            </td>
                            <td class="elemTd">
                                <asp:Label ID="lblName" Text="" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="descTd">
                                盘点所属年月
                            </td>
                            <td class="elemTd">
                                <asp:Label ID="lblStocktakeDate" Text="" runat="server"></asp:Label>
                            </td>
                            <td class="descTd">
                                盘点日期
                            </td>
                            <td class="elemTd">
                                <asp:Label ID="lblInputDate" Text="" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="descTd">
                                盘点开始时间
                            </td>
                            <td>
                                <asp:Label ID="lblStateTime" runat="server"></asp:Label>
                            </td>
                            <td class="descTd">
                                盘点截止时间
                            </td>
                            <td class="elemTd">
                                <asp:Label ID="lblEndTime" Text="" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trd" runat="server"><td class="descTd" runat="server">
                                盘点人
                            </td><td class="elemTd" runat="server">
                                <asp:Label ID="lblPerson" Text="" runat="server"></asp:Label>
                            </td><td class="descTd" runat="server">
                                仓库
                            </td><td class="elemTd" runat="server">
                                <asp:Label ID="lblTName" Text="" runat="server"></asp:Label>
                            </td></tr>
                        <tr>
                            <td class="descTd">
                                备注
                            </td>
                            <td colspan="3" style="word-break: break-all;">
                                <asp:Label ID="lblExplain" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="vertical-align: top; height: 1px;">
                <td style="vertical-align: top; padding-top: 10px;">
                    <asp:GridView ID="gvwStocktake" CssClass="viewTable" AutoGenerateColumns="false" runat="server"><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="物资编号"><ItemTemplate>
                                    <div style="word-break: break-all; min-width: 50px;">
                                        <%# Eval("ResourceCode") %>
                                    </div>
                                    <asp:HiddenField ID="hlfdResourceName" Value='<%# Convert.ToString(Eval("ResourceName").ToString()) %>' runat="server" />
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资名称"><ItemTemplate>
                                    <div style="word-break: break-all; min-width: 50px;">
                                        <%# Eval("ResourceName") %>
                                    </div>
                                    <asp:HiddenField ID="hlfdResourceName" Value='<%# Convert.ToString(Eval("ResourceName").ToString()) %>' runat="server" />
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格">
<ItemTemplate>
                                    <div style="word-break: break-all;">
                                        <%# Eval("Specification") %>
                                    </div>
                                    <asp:HiddenField ID="hlfdSpecification" Value='<%# Convert.ToString(Eval("Specification").ToString()) %>' runat="server" />
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单位">
<ItemTemplate>
                                    <div style="word-break: break-all;">
                                        <asp:Label ID="lblUnit" Text='<%# Convert.ToString(Eval("Unit")) %>' runat="server"></asp:Label>
                                    </div>
                                </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="Price" HeaderText="价格" ItemStyle-HorizontalAlign="Right" /><asp:TemplateField HeaderText="供应商" HeaderStyle-Width="100px"><ItemTemplate>
                                    <div style="word-break: break-all;">
                                        <%# Eval("Supplier") %>
                                    </div>
                                    <asp:HiddenField ID="hlfdSupplier" Value='<%# Convert.ToString(Eval("Supplier").ToString()) %>' runat="server" />
                                    <asp:HiddenField ID="hlfdSupplierId" Value='<%# Convert.ToString(Eval("SupplierId").ToString()) %>' runat="server" />
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="上期结余">
<ItemTemplate>
                                    <asp:Label ID="lblLastMonthNum" Text='<%# Convert.ToString(Convert.ToDecimal(Eval("LastMonthNum")).ToString("0.000")) %>' runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="入库数量">
<ItemTemplate>
                                    <asp:Label ID="lblStorageNum" Text='<%# Convert.ToString(Convert.ToDecimal(Eval("StorageNum")).ToString("0.000")) %>' runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="甲供数量">
<ItemTemplate>
                                    <asp:Label ID="lblFirstStorageNum" Text='<%# Convert.ToString(Convert.ToDecimal(Eval("FirstStorageNum")).ToString("0.000")) %>' runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="出库数量">
<ItemTemplate>
                                    <asp:Label ID="lblOutReserveNum" Text='<%# Convert.ToString(Convert.ToDecimal(Eval("OutReserveNum")).ToString("0.000")) %>' runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="调拨入库数量">
<ItemTemplate>
                                    <asp:Label ID="lblTransferringInNum" Text='<%# Convert.ToString(Convert.ToDecimal(Eval("TransferringInNum")).ToString("0.000")) %>' runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="调拨出库数量">
<ItemTemplate>
                                    <asp:Label ID="lblTransferringOutNum" Text='<%# Convert.ToString(Convert.ToDecimal(Eval("TransferringOutNum")).ToString("0.000")) %>' runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="报损出库数量">
<ItemTemplate>
                                    <asp:Label ID="lblWastageNum" Text='<%# Convert.ToString(Convert.ToDecimal(Eval("WastageNum")).ToString("0.000")) %>' runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="退库数量">
<ItemTemplate>
                                    <asp:Label ID="lblRefundingNum" Text='<%# Convert.ToString(Convert.ToDecimal(Eval("RefundingNum")).ToString("0.000")) %>' runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="账面数量">
<ItemTemplate>
                                    <asp:Label ID="lblBookNum" Text='<%# Convert.ToString(Convert.ToDecimal(Eval("BookNum")).ToString("0.000")) %>' runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="盘点数量">
<ItemTemplate>
                                    <asp:Label ID="lblBookNum" Text='<%# Convert.ToString(Convert.ToDecimal(Eval("StocktakeNum")).ToString("0.000")) %>' runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="盘盈/盘亏">
<ItemTemplate>
                                    <%# (Convert.ToDecimal(Eval("StocktakeNum")) - Convert.ToDecimal(Eval("BookNum"))).ToString("0.000") %>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注" HeaderStyle-Width="100px"><ItemTemplate>
                                    <div style="word-break: break-all;">
                                        <%# Eval("Note") %>
                                    </div>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </td>
            </tr>
            <tr id="trAudit" style="vertical-align: top;">
                <td>
                    <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="115" BusiClass="001" runat="server" />
                </td>
            </tr>
            <tr>
            </tr>
        </table>
    </div>
    <!--项目编码-->
    <asp:HiddenField ID="hfldPid" runat="server" />
    <asp:HiddenField ID="hfldProject" runat="server" />
    <asp:HiddenField ID="hfldPurchaseCode" runat="server" />
    <asp:HiddenField ID="hfldResourceCode" runat="server" />
    </form>
</body>
</html>
