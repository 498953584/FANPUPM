<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndirectView.aspx.cs" Inherits="BudgetManage_Cost_IndirectView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>间接成本预算查看</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript">
        var cbsCodeIndex = 1;
        $(document).ready(function () {
            var gvTask = new JustWinTable('gvBudget');
            replaceEmptyTable('tblEmpty', 'gvBudget');
            fillSpace();
            
        });
        function fillSpace() {
            var rows = document.getElementById('gvBudget').rows;
            if (rows) {
                for (i = 2; i < rows.length; i++) {
                    rows[i].cells[cbsCodeIndex].innerText = '         ' + rows[i].cells[cbsCodeIndex].innerText;
                }
            }
                }
       
    </script>
    <style type="text/css">
        .tbl
        {
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="print" style="width: 100%; margin: 0 auto;">
        <table class="tab" style="vertical-align: top; border-collapse: collapse;">
            <tr>
                
                <td class="divHeader">
                    间接成本预算查看
                    <div style="height: 20px; width: 100%; left: 0px; top: 0px; text-align: right; position: absolute;">
                        <input type="button" style="float: right;" class="noprint" onclick="winPrint()" value=" 打 印 " />
                    </div>
                </td>
            </tr>
            <tr style="height: 1px;">
                <td>
                    <table id="Table1" cellpadding="0" cellspacing="0" style="border-style: none;" class="viewTable">
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
            <tr>
                <td>
                    <table class="viewTable" style=" margin-bottom:3px;">
                        <tr>
                            <td style="width:80px;">
                                <asp:Label ID="lblPrjOrgName" Text="项目名称" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblRelatedName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                上报人
                            </td>
                            <td>
                                <asp:Label ID="lblInputUser" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                上报日期
                            </td>
                            <td>
                                <asp:Label ID="lblInputDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvBudget" CssClass="viewTable" AutoGenerateColumns="false" DataKeyNames="CBSCode" runat="server">
<EmptyDataTemplate>
                            <table id="tblEmpty">
                                <tr class="header">
                                    <th scope="col" style="width: 25px;">
                                        序号
                                    </th>
                                    <th scope="col" style="width: 100px;">
                                        CBS编码
                                    </th>
                                    <th scope="col">
                                        名称
                                    </th>
                                    <th scope="col" style="width: 100px;">
                                        预算金额
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                    <%# (Container.DataItemIndex + 1).ToString() %>
                                </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="CBS编码" DataField="CBSCode" HeaderStyle-Width="100px" /><asp:TemplateField HeaderText="名称"><ItemTemplate>
                                    <%# Eval("CostAcc.Name") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="100px" HeaderText="预算金额">
<ItemTemplate>
                                    <%# decimal.Parse(Eval("AccountAmount").ToString()).ToString("0.000") %>
                                </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </td>
            </tr>
            <tr id="trAudit">
                <td>
                    <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="119" BusiClass="001" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
