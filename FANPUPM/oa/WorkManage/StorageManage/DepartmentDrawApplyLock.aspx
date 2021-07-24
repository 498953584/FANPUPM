<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DepartmentDrawApplyLock.aspx.cs" Inherits="oa_WorkManage_DepartmentDrawApplyLock" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>部门领用申请</title></head>
<body  class="body_popup">
    <form id="form1" runat="server">
    <div>
    <table class="table-form" id="tablex" cellspacing="0" cellpadding="0" width="100%" border="1">
		<tr>
			<td class="td-head" colspan="4" height="20">
                部门领用申请
            </td>
		</tr>  
		<tr>
			<td class="td-label" width="20%">
                流程发起人</td>
			<td><asp:Label ID="LbApplyPerson" runat="server"></asp:Label>
			</td>
			<td class="td-label" width="20%">
                申请部门</td>
			<td><asp:Label ID="LbApplyDept" runat="server"></asp:Label>
			</td>
		</tr> 
		<tr>
			<td class="td-label" width="20%">
                申请日期</td>
            <td colspan="3">
                <asp:Label ID="LbApplyDate" runat="server"></asp:Label></td>
		</tr>
		<tr>
			<td class="td-label">
                情况说明</td>
            <td colspan="3" style="height:40px">
                <div style="height:100%;width:100%">
                    <asp:Label ID="LbRemark" runat="server"></asp:Label>
                </div>
                </td>
		</tr> 
		<tr>
		    <td colspan="4">
		       <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                    <asp:GridView CssClass="grid" ID="GVBook" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" BorderWidth="0px" CellPadding="0" OnRowDataBound="GVBook_RowDataBound" OnPageIndexChanging="GVBook_PageIndexChanging" runat="server">
<EmptyDataTemplate>
                            <table id="Tablex" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                                style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                                width: 100%; border-collapse: collapse; border-right-width: 0px">
                                <tr align="center" class="grid_head">
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 40px">
                                        序号</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        名称</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        分类</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        单位</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 90px">
                                        单价(元)</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 90px">
                                        数量</th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField HeaderText="序号" /><asp:BoundField DataField="ResName" HeaderText="名称" HtmlEncode="false" /><asp:BoundField HeaderText="分类" HtmlEncode="false" /><asp:BoundField DataField="Unit" HeaderText="单位" HtmlEncode="false" /><asp:BoundField DataField="PlanFee" HeaderText="单价(元)" DataFormatString="{0:f2}" HtmlEncode="false" /><asp:BoundField DataField="ApplyNumber" HeaderText="数量" DataFormatString="{0:f2}" HtmlEncode="false" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                    </div>
		    </td>
		</tr>
	</table>
    </div>
    </form>
</body>
</html>
