<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyLoanList.aspx.cs" Inherits="oa_FileManage_MyLoanList" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title></title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
    <script language="javascript" type="text/javascript">
    //1为未借出（借阅人提交申请）；2为已借出（管理员借阅确认，更改档案借出状态）；3为归还申请；4为已归还（管理员归还确认，更改档案借出状态）
	function ClickRow(RecordID,LendState)
	{
	    document.getElementById('HdnRecordID').value = RecordID;
	    if(LendState == "2" || LendState == "3")
	    {
	        document.getElementById('btnBack').disabled = false;
	    }
	    else
	    {
	        document.getElementById('btnBack').disabled = true;
	    }
	    if(LendState == "1")
	    {
	        document.getElementById('btnDel').disabled = false;
	    }
	    else
	    {
	        document.getElementById('btnDel').disabled = true;
	    }
	}
	function OpenWin(t)
	{
	    var url = "";
	    var RecordID = document.getElementById('HdnRecordID').value;
	    if(t == "app")
	    {
	        url = "BookRenewLendApply.aspx?rid="+RecordID;
	    }
	    if(t == "back")
	    {
	        url = "BookReturn.aspx?rid="+RecordID;
	    }
		var ref = window.showModalDialog(url,window,"dialogHeight:160px;dialogWidth:350px;center:1;help:0;status:0;");
		if(ref)
		{
		    return true;
		}
		return false;
	}
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">
            <tr>
                <td height="20px" class="td-title">
                        借阅档案列表
                </td>
            </tr>
            <tr>
                <td height="20px" align="right">
                    &nbsp;<input id="HdnRecordID" type="hidden" style="width:20px" runat="server" />

                    <asp:Button ID="btnDel" Text="删  除" Enabled="false" CssClass="button-normal" OnClick="btnDel_Click" runat="server" />
                    <asp:Button ID="btnBack" Text="归  还" Enabled="false" CssClass="button-normal" OnClick="btnBack_Click" runat="server" />
                    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
            </tr>
            <tr>
                <td valign="top" height="100%">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                    <asp:GridView CssClass="grid" ID="GVBook" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" BorderWidth="0px" CellPadding="0" PageSize="20" DataSourceID="SQLLendDataSource" OnRowDataBound="GVBook_RowDataBound" runat="server">
<EmptyDataTemplate>
                                <table id="Table1" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                                    style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                                    width: 100%; border-collapse: collapse; border-right-width: 0px">
                                    <tr align="center" class="grid_head">
                                        <th align="center" nowrap="nowrap" scope="col">
                                            图书名称</th>
                                        <th align="center" nowrap="nowrap" scope="col">
                                            项目名称</th>
                                        <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                            借阅时间</th>
                                        <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                            应归还时间</th>
                                        <th align="center" nowrap="nowrap" scope="col" style="width: 80px">
                                            实际归还时间</th>
                                        <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                            状态</th>
                                        <th align="center" nowrap="nowrap" scope="col">
                                            备注</th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings><Columns><asp:BoundField DataField="FileName" HeaderText="档案名称" /><asp:BoundField DataField="PrjName" HeaderText="项目名称" HtmlEncode="false" /><asp:BoundField DataField="LendDate" HeaderText="借阅时间" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:BoundField DataField="PlanReturnDate" HeaderText="应归还时间" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:BoundField DataField="ReturnDate" HeaderText="实际归还时间" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:BoundField HeaderText="状态" HtmlEncode="false" /><asp:BoundField DataField="Content" HeaderText="备注" HtmlEncode="false" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle></asp:GridView>
                        <asp:SqlDataSource ID="SQLLendDataSource" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [V_OA_File_Lend] WHERE ([BorrowMan] = @BorrowMan) ORDER BY [ReturnDate] DESC" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:SessionParameter Name="BorrowMan" SessionField="yhdm" Type="String"></asp:SessionParameter></SelectParameters></asp:SqlDataSource>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
