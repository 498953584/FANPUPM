<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileDestroyLock.aspx.cs" Inherits="oa_FileManage_FileDestroyLock" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>档案销毁</title></head>
<body class="body_popup">
    <form id="form1" runat="server">
    <div>
     <table class="table-form" id="tablex"  cellSpacing="0" cellPadding="0" width="100%" border="1">
		<tr>
			<td class="td-head" colSpan="4" height="20">
                管理销毁记录
            </td>
		</tr>  
		<tr>
			<td class="td-label">
                流程发启人</td>
            <td>
                &nbsp;<asp:Label ID="LbApplyPerson" Text="Label" runat="server"></asp:Label></td>
		
			<td class="td-label">
                申请时间</td>
            <td>
                <asp:Label ID="LbApplyDate" Text="Label" runat="server"></asp:Label></td>
		</tr>
		<tr>
			<td colspan="4">
                <asp:GridView ID="GVBook" AutoGenerateColumns="false" BorderWidth="0px" CellPadding="0" CssClass="grid" Width="100%" OnRowDataBound="GVBook_RowDataBound" runat="server">
<EmptyDataTemplate>
                        <table id="Table1" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                            style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                            width: 100%; border-collapse: collapse; border-right-width: 0px">
                            <tr align="center" class="grid_head">
                                <th align="center" nowrap="nowrap" scope="col" style="width: 40px">
                                    序号</th>
                                <th align="center" nowrap="nowrap" scope="col">
                                    档案标题</th>
                                <th align="center" nowrap="nowrap" scope="col" style="width: 120px">
                                    档案编号</th>
                                <th align="center" nowrap="nowrap" scope="col">
                                    所属项目</th>
                                <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                    密级</th>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
<PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings><RowStyle CssClass="grid_row"></RowStyle><Columns><asp:BoundField HeaderText="序号" HtmlEncode="false" /><asp:BoundField DataField="FileName" HeaderText="档案标题" HtmlEncode="false" /><asp:BoundField DataField="FileCode" HeaderText="档案编号" HtmlEncode="false" /><asp:BoundField DataField="PrjName" HeaderText="所属项目" HtmlEncode="false" /><asp:BoundField HeaderText="密级" HtmlEncode="false" /></Columns><PagerStyle HorizontalAlign="Center"></PagerStyle><HeaderStyle CssClass="grid_head" HorizontalAlign="Center"></HeaderStyle></asp:GridView>
			    
			</td>
		</tr> 
	</table>
    </div>
    </form>
</body>
</html>
