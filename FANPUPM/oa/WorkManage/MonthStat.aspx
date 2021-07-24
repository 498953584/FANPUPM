<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MonthStat.aspx.cs" Inherits="oa_WorkManage_MonthStat" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title></title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
    <script language="javascript" type="text/javascript">
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">
            <tr>
                <td height="20px" class="td-title">月度统计
                </td>
            </tr>
            <tr>
                <td height="20px" class="td-search">
                    <asp:DropDownList ID="DDLYear" runat="server"></asp:DropDownList>年<asp:DropDownList ID="DDLMonth" runat="server"></asp:DropDownList>月\r
                    <asp:Button ID="btnSearch" Text="" OnClick="btnSearch_Click" runat="server" /></td>
            </tr>
            <tr>
                <td valign="top" height="100%">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                    <asp:GridView CssClass="grid" ID="GVBook" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" BorderWidth="0px" CellPadding="0" DataSourceID="SqlDS" OnRowDataBound="GVBook_RowDataBound" runat="server">
<EmptyDataTemplate>
                            <table id="Tablex" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                                style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                                width: 100%; border-collapse: collapse; border-right-width: 0px">
                                <tr align="center" class="grid_head">
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 40px">
                                        序号</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        分子机构/部门</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 90px">
                                        领用成本</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        备注</th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField HeaderText="序号" /><asp:BoundField DataField="V_BMMC" HeaderText="分子机构/部门" HtmlEncode="false" /><asp:BoundField HeaderText="领用成本" DataFormatString="{0:f2}" HtmlEncode="false" /><asp:BoundField HeaderText="备注" HtmlEncode="false" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                        <asp:SqlDataSource ID="SqlDS" SelectCommand="select a.i_bmdm,a.v_bmbm,a.CorpCode,a.V_BMMC,b.CorpName from PT_d_bm a join PT_d_CorpCode b on a.CorpCode=b.CorpCode and a.i_jb=2" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
