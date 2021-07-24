<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyDrawFrame.aspx.cs" Inherits="oa_WorkManage_SubCompanyManage_CompanyDrawFrame" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>部门申请记录</title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
    <script language="javascript" type="text/javascript">
    function ClickRow(GuidMainID,InStorageID,ApplyDepartment)
	{
		frmInStoreroom.location.href = "CompanyDrawList.aspx?ia=1&ic="+GuidMainID+"&id="+InStorageID+"&dm="+ApplyDepartment;
	}
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">
            <tr>
                <td height="20px" class="td-title">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td valign="top" height="40%">
                            <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                    <asp:GridView CssClass="grid" ID="GVBook" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" BorderWidth="1px" CellPadding="0" OnRowDataBound="GVBook_RowDataBound" OnPageIndexChanging="GVBook_PageIndexChanging" runat="server">
<EmptyDataTemplate>
                            <table id="Tablex" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                                style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                                width: 100%; border-collapse: collapse; border-right-width: 0px">
                                <tr align="center" class="grid_head">
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 40px">
                                        序号</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        申请日期</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        申请部门</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        申请人</th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField HeaderText="序号" /><asp:BoundField DataField="ApplyDate" HeaderText="申请日期" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:BoundField HeaderText="申请部门" HtmlEncode="false" /><asp:BoundField HeaderText="申请人" HtmlEncode="false" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                    </div>
                </td>
            </tr>
            <tr>
                <td valign="top" height="60%">
                    <iframe id="frmInStoreroom" frameborder="0" width="100%" height="100%" src="CompanyDrawList.aspx?ia=0&ic=&id=&dm=0" runat="server"></iframe>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
