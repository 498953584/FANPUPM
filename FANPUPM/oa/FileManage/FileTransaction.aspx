<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileTransaction.aspx.cs" Inherits="oa_FileManage_FileTransaction" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title></title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
    <script language="javascript" type="text/javascript">
    function ClickRow(RecordCode,LendState,BorrowMan)
	{
	    document.getElementById('HdnBorrowMan').value = BorrowMan;
	    document.getElementById('HdnRecordID').value = RecordCode;
	    if(LendState == "1")
	    {
	        document.getElementById('btnLend').disabled = false;//借阅确认 1 - 2
	    }
	    else
	    {
	        document.getElementById('btnLend').disabled = true;
	    }
	    if(LendState == "3")
	    {
	        document.getElementById('btnReturn').disabled = false;//归还确认 3 - 4
	    }
	    else
	    {
	        document.getElementById('btnReturn').disabled = true;
	    }
	    if(LendState == "2" || LendState == "3")
	    {
	        document.getElementById('btnHasten').disabled = false;//催  还
	    }
	    else
	    {
	        document.getElementById('btnHasten').disabled = true;
	    }
	}
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">
            <tr>
                <td height="20px" class="td-title">
                        档案借阅管理
                </td>
            </tr>
            <tr>
                <td height="20px" align="right">
                    按<asp:DropDownList ID="DDLSearch" runat="server"><asp:ListItem Selected="true" Value="0" Text="档案名称" /><asp:ListItem Value="1" Text="借阅人" /></asp:DropDownList>
                    <asp:TextBox ID="txtotherSearch" CssClass="text-normal" runat="server"></asp:TextBox>
                    <asp:Button ID="btnBookSearch" Text="借出档案检索" CssClass="search_button" Width="90px" OnClick="btnBookSearch_Click" runat="server" />
                    <input id="HdnRecordID" type="hidden" style="width:20px" runat="server" />

                    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
            </tr>
            <tr>
                <td height="20px" class="td-toolsbar">
                    <asp:Button ID="btnLend" Text="借阅确认" Enabled="false" OnClick="btnLend_Click" runat="server" />
                    <asp:Button ID="btnReturn" Text="归还确认" Enabled="false" OnClick="btnReturn_Click" runat="server" />
                    <asp:Button ID="btnHasten" Text="催  还" Enabled="false" OnClick="btnHasten_Click" runat="server" />
                    <input id="HdnBorrowMan" type="hidden" style="width:20px" runat="server" />

            </tr>
            <tr>
                <td valign="top" height="100%">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                    <asp:GridView CssClass="grid" ID="GVBook" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" BorderWidth="0px" CellPadding="0" PageSize="20" OnRowDataBound="GVBook_RowDataBound" OnPageIndexChanging="GVBook_PageIndexChanging" runat="server">
<EmptyDataTemplate>
                            <table id="Table1" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                                style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                                width: 100%; border-collapse: collapse; border-right-width: 0px">
                                <tr align="center" class="grid_head">
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        借阅人</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        图书名称</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        档案编号</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        借阅时间</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 80px">
                                        计划归还时间</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        借阅状态</th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField HeaderText="借阅人" /><asp:BoundField DataField="FileName" HeaderText="档案名称" /><asp:BoundField DataField="FileCode" HeaderText="档案编号" HtmlEncode="false" /><asp:BoundField DataField="LendDate" HeaderText="借阅时间" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:BoundField DataField="PlanReturnDate" HeaderText="计划归还时间" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:BoundField HeaderText="借阅状态" HtmlEncode="false" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
        
    </form>
</body>
</html>
