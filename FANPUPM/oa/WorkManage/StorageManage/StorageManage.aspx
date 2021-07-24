<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageManage.aspx.cs" Inherits="oa_WorkManage_StorageManage_StorageManage" %>

<html>
<head id="Head1" runat="server"><title></title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
    <script language="javascript" type="text/javascript">
    function ClickRow(LibraryCode,i_ChildNum)
	{
		document.getElementById('btnEdit').disabled = false;
		if(parseInt(i_ChildNum) > 0)
		{
		    document.getElementById('btnDel').disabled = true;
		}
		else
		{
		    document.getElementById('btnDel').disabled = false;
		}
		document.getElementById('HdnLibraryCode').value = LibraryCode;
	}
	function OpenWin(op)
	{
	    var LibraryCode = "0";
	    if(op == 'upd')
	    {
	        LibraryCode = document.getElementById('HdnLibraryCode').value;
	    }
		var url= "StorageManageEdit.aspx?t=" + op + "&lc=" + LibraryCode;
		var ref = window.showModalDialog(url,window,"dialogHeight:180px;dialogWidth:350px;center:1;help:0;status:0;");
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
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal" style="table-layout:auto">
            <tr>
                <td height="20px" class="td-title">
                        仓库管理
                </td>
            </tr>
            <tr>
                <td height="20px" class="td-toolsbar">
                    <asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />
                    <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
                    <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
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
                                        分子机构名称</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        仓库名称</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        备注</th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField HeaderText="序号" HtmlEncode="false" /><asp:BoundField DataField="CorpName" HeaderText="分子机构名称" HtmlEncode="false" /><asp:BoundField DataField="DepotName" HeaderText="仓库名称" HtmlEncode="false" /><asp:BoundField DataField="Remark" HeaderText="备注" HtmlEncode="false" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                        <asp:SqlDataSource ID="SQLDataSource" ProviderName="System.Data.SqlClient" SelectCommand="SELECT a.DepotID,a.CorpCode,a.DepotName,a.Remark,a.IsValid,b.CorpName FROM OA_OfficeRes_Depot a left join pt_d_CorpCode b on a.CorpCode=b.CorpCode where a.IsValid='1' order by DepotID asc" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
