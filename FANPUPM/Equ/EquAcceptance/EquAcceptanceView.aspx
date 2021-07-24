<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquAcceptanceView.aspx.cs" Inherits="Equ_EquAcceptance_EquAcceptanceView" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>设备验收查看</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
</head>
<body id="print1">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top; width: 800px; margin: 0 auto">
        <tr style="height: 1px;">
            <td class="divHeader">
                设备验收查看
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
            <td>
                <table class="viewTable" cellpadding="0px" cellspacing="0">
                    <tr>
                        <td class="descTd">
                            固定资产采购单编号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPurchaseCode" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            验收人
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblAcceptor" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            验收时间
                        </td>
                        <td class="elemTd" colspan="3">
                            <asp:Label ID="lblAcceptDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 1px;">
            <td>
                <table style="width: 100%; height: 50%; margin-top: 10px;" border="0">
                    <tr style="vertical-align: top;">
                        <td>
                            <asp:GridView ID="gvwDetail" AutoGenerateColumns="false" CssClass="gvdata" DataKeyNames="Id" runat="server">
<EmptyDataTemplate>
                                    <table id="emptyDetail" class="tab">
                                        <tr class="header">
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
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                            
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="设备名称"><ItemTemplate>
                                            <div style="word-break: break-all;">
                                                
                                            </div>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="设备类型"><ItemTemplate>
                                            
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格型号"><ItemTemplate>
                                            <div style="word-break: break-all;">
                                                
                                            </div>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="生产厂家">
<ItemTemplate>
                                            <div style="overflow: hidden;">
                                                
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="供应商">
<ItemTemplate>
                                            <div style="overflow: hidden;">
                                                
                                            </div>
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="数量" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                            
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单价" ItemStyle-HorizontalAlign="Right">
<ItemTemplate>
                                            
                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="技术参数"><ItemTemplate>
                                            <div style="word-break: break-all; word-wrap: break-word; width: 100px;">
                                                
                                            </div>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="随机资料"><ItemTemplate>
                                            <div style="word-break: break-all; word-wrap: break-word; width: 100px;">
                                                
                                            </div>
                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
                                            <div style="word-break: break-all; word-wrap: break-word; width: 100px;">
                                                
                                            </div>
                                        </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trAudit" style="vertical-align: top;">
            <td>
                <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="166" BusiClass="001" runat="server" />
            </td>
        </tr>
        <tr>
        </tr>
    </table>
    </form>
</body>
</html>
