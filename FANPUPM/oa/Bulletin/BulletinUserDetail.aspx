<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BulletinUserDetail.aspx.cs" Inherits="oa_Bulletin_BulletinUserDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>公告的人员查看页面</title>

    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>

    <script type="text/javascript">
        addEvent(window, 'load', function() {
            replaceEmptyTable('emptyTable', 'gvData');
            var jwTable = new JustWinTable('gvData');
        });

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table border="0px" cellpadding="0px" cellspacing="0px" width="100%">
        <tr>
            <td style="vertical-align: top;">
                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                    <tr>
                        <td class="descTd" style="white-space:nowrap;">
                            用户名称
                        </td>
                        <td>
                           <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd" style="white-space:nowrap;">
                            查看时间
                        </td>
                        <td>
                            <asp:TextBox ID="txtStartDate" Width="150px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            至
                        </td>
                        <td>
                            <asp:TextBox ID="txtEndDate" Width="150px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnQuery" Text="查询" OnClick="btnQuery_Click" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <asp:GridView ID="gvData" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvData_RowDataBound" runat="server">
<EmptyDataTemplate>
                        <table id="emptyTable">
                            <tr class="header">
                                <th scope="col">
                                    序号
                                </th>
                                <th scope="col">
                                    用户名称
                                </th>
                                <th scope="col">
                                    查看时间
                                </th>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" ItemStyle-Width="20px">
<ItemTemplate>
                                
                            </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="用户名称" DataField="UserName" /><asp:BoundField HeaderText="查看时间" DataField="MinDate" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                </webdiyer:AspNetPager>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
