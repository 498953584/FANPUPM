<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectFile.aspx.cs" Inherits="oa_FileManage_SelectFile" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>档案选择</title><meta http-equiv="content-type" content="text/html; charset=utf-8" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
    <script language="javascript" type="text/javascript">
    function ClickRow(ClassID,ClassName)
	{
	}
	function dblClickRow(RecordID,FileName,FileCode,PrjName,SecretLevel)
	{
	    var file = window.dialogArguments;
	    file[0] = RecordID;
		file[1] = FileName;
		file[2] = FileCode;
		file[3] = PrjName;
		file[4] = SecretLevel;
		window.returnvalue= file;
		window.close();
	}
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">
            <tr>
                <td valign="top">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                    <asp:GridView CssClass="grid" ID="GridView1" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" PageSize="20" BorderWidth="0px" CellPadding="0" DataSourceID="SQLDataSource" OnRowDataBound="GridView1_RowDataBound" runat="server">
<EmptyDataTemplate>
                            <table id="Table1" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                                style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                                width: 100%; border-collapse: collapse; border-right-width: 0px">
                                <tr align="center" class="grid_head">
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 40px">
                                        序号</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        档案编号</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        档案名称</th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings><Columns><asp:BoundField HeaderText="序号" HtmlEncode="false" /><asp:BoundField DataField="FileCode" HeaderText="档案编号" HtmlEncode="false" /><asp:BoundField DataField="FileName" HeaderText="档案名称" HtmlEncode="false" /></Columns></asp:GridView>
                        <asp:SqlDataSource ID="SQLDataSource" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [OA_File_Catalog] WHERE ([LibraryCode] = @LibraryCode) ORDER BY [FileAge]" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="LibraryCode" QueryStringField="lc" Type="String"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
