<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectCorpUser.aspx.cs" Inherits="EPC_Workflow_SelectCorpUser" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>选择部门人员</title>

    <script type="text/javascript" src="/Script/jquery.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>

    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function() {
            var table = new JustWinTable('gvUser');
            replaceEmptyTable('emptyUser', 'gvUser');

            $('#btnCancel').click(function() {
                divClose(parent);
            });
        });
            </script>
</head>
<body>
    <form id="form1" runat="server">
    <table  width="100%" cellpadding="0px" cellspacing="0px"  style="height:90%;" >
        <tr>
            <td style="vertical-align:top; height:30px;">
                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            用户名称
                        </td>
                        <td>
                            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                           &nbsp; <asp:Button ID="btnQuery" Text="查询" OnClick="btnQuery_Click" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align:top;" >
                <asp:GridView ID="gvUser" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvUser_RowDataBound" DataKeyNames="v_yhdm" runat="server">
<EmptyDataTemplate>
                        <table id="emptyUser">
                            <tr class="header">
                                <th scope="col" style="width: 20px;">
                                </th>
                                <th scope="col">
                                    序号
                                </th>
                                <th scope="col">
                                    用户名称
                                </th>
                                <th scope="col">
                                    部门名称
                                </th>
                                <th scope="col">
                                    岗位
                                </th>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                <asp:CheckBox ID="cbAllBox" runat="server" />
                            </HeaderTemplate><ItemTemplate>
                                <asp:CheckBox ID="cbBox" runat="server" />
                            </ItemTemplate></asp:TemplateField><asp:BoundField DataField="Num" HeaderText="序号" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="20px" /><asp:BoundField HeaderText="用户名称" DataField="v_xm" /><asp:BoundField HeaderText="部门名称" DataField="bmmc" /><asp:BoundField HeaderText="岗位" DataField="DutyName" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                </webdiyer:AspNetPager>
            </td>
        </tr>
        <tr><td style="height:25px; text-align:right; vertical-align:bottom;"><asp:Button ID="btnSave" Text="确定" OnClick="btnSave_Click" runat="server" />
                <input id="btnCancel" type="button" value="取 消" class="button-normal" /></td> </tr>
    </table>
  
    </form>
</body>
</html>
