<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaintenanceView.aspx.cs" Inherits="Equ_Maintenance_MaintenanceView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>维护保养上报查看</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            replaceEmptyTable('emptyMaintenanceStock', 'gvMaintenanceStock')
        });
    </script>
</head>
<body id="print1">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top;">
        <tr style="height: 1px;">
            <td class="divHeader">
                维护保养上报查看
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
                <table class="viewTable" cellpadding="0px" cellspacing="0">
                    <tr>
                        <td class="descTd">
                            上报日起
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblReportDate" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            设备编号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblEquCode" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            设备名称
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblEquName" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            规格
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblSpecification" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            预计维修费用
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblBudCost" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            预计维修时间(天)
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblBudTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            预计维修内容
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblBudContent" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            实际维修费用
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblRealityCost" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            实际维修时间(天)
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblRealityTime" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            实际维修内容
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblRealityContent" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            原因分析
                        </td>
                        <td colspan="3" style="word-break: break-all;">
                            <asp:Label ID="lblReason" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style=" height:1px;">
            <td>
                <table style="width: 100%; height: 50%; margin-top: 10px;" border="0">
                    <tr style="vertical-align: top;">
                        <td>
                            <asp:GridView ID="gvMaintenanceStock" Width="100%" AutoGenerateColumns="false" CssClass="viewTable" DataKeyNames="ResourceId" runat="server">
<EmptyDataTemplate>
                                    <table class="tab" id="emptyMaintenanceStock">
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
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="20px"><ItemTemplate>
                                            
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资编号" HeaderStyle-Width="50px"><ItemTemplate>
                                            <div style="word-break: break-all;">
                                                
                                            </div>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资名称" HeaderStyle-Width="80px">
<ItemTemplate>
                                            <div style="word-break: break-all;">
                                                
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="规格">
<ItemTemplate>
                                            <div style="word-break: break-all;">
                                                
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="型号">
<ItemTemplate>
                                            <div style="word-break: break-all;">
                                                
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="品牌">
<ItemTemplate>
                                            <div style="word-break: break-all;">
                                                
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="技术参数">
<ItemTemplate>
                                            <div style="word-break: break-all;">
                                                
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="50px"><ItemTemplate>
                                            <div style="word-break: break-all;">
                                                
                                            </div>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数量" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                            
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="价格" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                            
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="小计" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                            
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="供应商">
<ItemTemplate>
                                            <div style="word-break: break-all;">
                                                
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="领用人">
<ItemTemplate>
                                            <div style="word-break: break-all;">
                                                
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="领用日期" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
                                            <div style="text-align: center; word-break: break-all;">
                                                
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trAudit" style="vertical-align: top;">
            <td>
                <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiClass="001" BusiCode="145" runat="server" />
            </td>
        </tr>
        <tr>
        </tr>
    </table>
    </form>
</body>
</html>
