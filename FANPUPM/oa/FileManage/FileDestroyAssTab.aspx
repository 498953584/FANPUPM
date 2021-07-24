<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileDestroyAssTab.aspx.cs" Inherits="oa_FileManage_FileDestroyAssTab" %>

<html>
<head runat="server"><title></title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
    <script language="javascript" type="text/javascript">
    function ClickRow(DestroyFileID,Flag)
	{
	}
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">
            <tr>
                <td height="20px" class="td-title">
                    申请明细记录</td>
            </tr>
            <tr>
                <td valign="top" height="100%">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                    <asp:GridView CssClass="grid" ID="GVBook" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" BorderWidth="0px" CellPadding="0" OnRowDataBound="GVBook_RowDataBound" OnPageIndexChanging="GVBook_PageIndexChanging" runat="server">
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
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField HeaderText="序号" HtmlEncode="false" /><asp:BoundField DataField="FileName" HeaderText="档案标题" HtmlEncode="false" /><asp:BoundField DataField="FileCode" HeaderText="档案编号" HtmlEncode="false" /><asp:BoundField DataField="PrjName" HeaderText="所属项目" HtmlEncode="false" /><asp:BoundField HeaderText="密级" HtmlEncode="false" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
