<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileLibrary.aspx.cs" Inherits="oa_FileManage_FileLibrary" %>

<html>
<head runat="server"><title></title><meta http-equiv="content-type" content="text/html; charset=utf-8" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
    <script language="javascript" type="text/javascript">
    function ClickRow(LibraryCode)
	{
		document.getElementById('btnEdit').disabled = false;
		document.getElementById('btnDel').disabled = false;
		document.getElementById('btnPopedomSet').disabled = false;
		document.getElementById('HdnLibraryCode').value = LibraryCode;
	}
	function OpenWin(op)
	{
	    var LibraryCode = document.getElementById('HdnLibraryCode').value;
		var url= "FileLibraryEdit.aspx?t=" + op + "&lc=" + LibraryCode;
		var ref = window.showModalDialog(url,window,"dialogHeight:180px;dialogWidth:350px;center:1;help:0;status:0;");
		if(ref)
		{
		    return true;
		}
		return false;
	}
	function OpenPopedomSet()
	{
	    var human = new Array();
	    human[0] = "";
		var url= "../../CommonWindow/PickSinglePerson.aspx";
		var ref = window.showModalDialog(url,human,"dialogHeight:380px;dialogWidth:320px;center:1;help:0;status:0;");
		if(human[0] != "")
		{
		   document.getElementById('HdnPopedomPerson').value = human[0];
		   return true;
		}
		return false;
	}
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal" style="table-layout:auto">
            <tr>
                <td height="20px" class="td-title">
                    档案室管理
                </td>
            </tr>
            <tr>
                <td height="20px" class="td-toolsbar">
                    <asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />
                    <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
                    <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
                    <asp:Button ID="btnPopedomSet" Enabled="false" Text="权限设置" Width="80px" OnClick="btnPopedomSet_Click" runat="server" />
                    <input id="HdnLibraryCode" style="width: 20px" type="hidden" runat="server" />

                    <input id="HdnPopedomPerson" style="width: 20px" type="hidden" runat="server" />

                 </td>
            </tr>
            <tr>
                <td valign="top">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                    <asp:GridView CssClass="grid" ID="GVBook" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" PageSize="20" BorderWidth="0px" CellPadding="0" DataSourceID="SQLDataSource" OnRowDataBound="GVBook_RowDataBound" runat="server">
<EmptyDataTemplate>
                            <table id="Table1" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                                style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                                width: 100%; border-collapse: collapse; border-right-width: 0px">
                                <tr align="center" class="grid_head">
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 40px">
                                        序号</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        档案室名称</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        管理员名称</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        备注</th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField HeaderText="序号" HtmlEncode="false" /><asp:BoundField DataField="LibraryName" HeaderText="档案室名称" HtmlEncode="false" /><asp:BoundField HeaderText="管理员名称" HtmlEncode="false" /><asp:BoundField DataField="Content" HeaderText="备注" HtmlEncode="false" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                        <asp:SqlDataSource ID="SQLDataSource" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [OA_File_Library] ORDER BY [LibraryCode]" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
