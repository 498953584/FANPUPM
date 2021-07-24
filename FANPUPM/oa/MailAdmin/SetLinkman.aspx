<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetLinkman.aspx.cs" Inherits="oa_MailAdmin_SetLinkman" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>设置联系人</title>
    <script language="javascript" type="text/javascript">
    function ClickRow(userCode,linkMan)
    {
        document.getElementById('btnDel').disabled = false;
        document.getElementById('hdnLinkMan').value = linkMan;
    }
    
    function showConsignee()
	{
		var url = "/CommonWindow/Consignee.aspx";
		var human = new Array();
		window.showModalDialog(url,human,"dialogHeight:422px;dialogWidth:659px;center:1;help:0;status:0;");

		for (var i=0;i<human.length;i++)
		{
			document.getElementById('hdnSelLinkMan').value += human[i]+,;
		}
	}
    </script>
    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script src="../../StockManage/Script/Config2.js" type="text/javascript"></script>

    <script src="../../StockManage/Script/JustWinTable.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onload = function()
        {
            var aa = new JustWinTable('GridView1');
        }
    </script>
</head>
<body class="body_frame" scroll="yes">
    <form id="form1" runat="server">
    <asp:HiddenField ID="hidGroup" runat="server" />
    <div>
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0" class="table-normal">
            <tr>
                <td class="td-title">
                    常用联系人</td>
            </tr>
            <tr>
                <td class="td-toolsbar" style="height: 24px">
                    &nbsp;<asp:DropDownList ID="ddlGroup" AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" runat="server"></asp:DropDownList>
                    <asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />
                    <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
                    <asp:HiddenField ID="hdnLinkMan" runat="server" />
                    <asp:HiddenField ID="hdnSelLinkMan" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlDataSource1" PageSize="20" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="UserCode,v_yhdm" runat="server">
<EmptyDataTemplate>
                            <table id="Table2" border="1" cellspacing="0" class="grid" rules="all" style="border-collapse: collapse">
                                <tr class="grid_head">
                                    <th scope="col" style="width: 40px">
                                        序号</th>
                                    <th scope="col" style="width: 90px">
                                        姓名</th>
                                    <th scope="col" style="width: 160px">
                                       组名</th>
                                    <th scope="col">
                                        部门名称</th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<PagerSettings FirstPageText="第一页" LastPageText="最后一页"></PagerSettings><RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>  
                                </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="v_xm" HeaderText="姓名" SortExpression="v_xm" /><asp:BoundField DataField="groupName" HeaderText="组名" SortExpression="groupName" /><asp:BoundField DataField="V_BMMC" HeaderText="部门名称" SortExpression="V_BMMC" /></Columns><PagerStyle HorizontalAlign="Right"></PagerStyle></asp:GridView>
                </td>
            </tr>
        </table>
    
    </div>
        <asp:SqlDataSource ID="SqlDataSource1" SelectCommand="SELECT [v_xm], [V_BMMC], [UserCode], [v_yhdm],[groupName] FROM [v_dzyj_linkman] where UserCode = @UserCode" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:SessionParameter Name="UserCode" SessionField="yhdm"></asp:SessionParameter></SelectParameters></asp:SqlDataSource>
    </form>
</body>
</html>
