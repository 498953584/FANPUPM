<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquipmentPurchaseView.aspx.cs" Inherits="Equ_Purchase_EquipmentPurchaseView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>固定资产采购单</title>
    <style type="text/css" media="print">
        .noprint
        {
            display: none;
        }
    </style>
    <script type="text/javascript" src="../../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../script/Config2.js"></script>
    <script type="text/javascript" src="../../script/JustWinTable.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            replaceEmptyTable('emptyMaterialStock', 'gvwMaterialStock')
        });
    </script>
</head>
<body id="print1">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top; min-width: 800px;">
        <tr>
            <td class="divHeader">
                固定资产采购单
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
                            编制人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="txtPerson" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            附件
                        </td>
                        <td class="elemTd" colspan="3">
                            <asp:Label ID="lblAccessory" Text="" runat="server"></asp:Label>
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
                            <asp:GridView ID="gvwMaterialStock" Width="100%" AutoGenerateColumns="false" CssClass="viewTable" DataKeyNames="scode" runat="server">
<EmptyDataTemplate>
                                    <table class="tab" id="emptyMaterialStock">
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
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="库存量" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                            <%# WebUtil.GetStockNumberByCode(Eval("scode").ToString()).ToString() %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数量" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                            <%# Eval("number") %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="采购价格" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                            <%# Eval("sprice") %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="小计" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                            <%# Convert.ToDecimal((Eval("Total") == DBNull.Value) ? "0" : Eval("Total")).ToString("0.000") %>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="供应商">
<ItemTemplate>
                                            <div style="word-break: break-all;">
                                                <%# Eval("CorpName") %>
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="到货日期" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
                                            
                                            <div style="text-align: center; word-break: break-all;">
                                                <%# (Eval("arrivalDate") == DBNull.Value) ? "" : Convert.ToDateTime(Eval("arrivalDate")).ToString("yyyy-MM-dd") %>
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                        </td>
                    </tr>
                    
                    <tr id="trAudit" style="vertical-align: top;">
                        <td>
                            <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="141" BusiClass="001" runat="server" />
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
