<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClassList.aspx.cs" Inherits="oa_BooksManage_ClassList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
<head runat="server"><title></title>
    <script language="javascript" type="text/javascript">
    function ClickRow(ClassID,ClassCode,ChildNum)
	{
	    //alert(ClassID);
		document.getElementById('btnEdit').disabled = false;
		if(parseInt(ChildNum) > 0)
		{
		    document.getElementById('btnDel').disabled = true;
		}
		else
		{
		    document.getElementById('btnDel').disabled = false;
		}
		document.getElementById('HdnID').value = ClassID;
		document.getElementById('HdnClassCode').value = ClassCode;
	}
	function OpenWin(op,ClassTypeCode,CatalogName)
	{
	    var ClassCode = document.getElementById('HdnClassCode').value;
		var url= "ClassEdit.aspx?t=" + op + "&ctc=" + ClassTypeCode + "&cc="+ ClassCode +"&cn="+escape(CatalogName);
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
                    <asp:Label ID="Label1" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td height="20px" class="td-toolsbar">
                    <asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />
                    <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
                    <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
                    <input id="HdnID" type="hidden" style="width:20px" runat="server" />

                    <input id="HdnClassCode" type="hidden" style="width:20px" runat="server" />

                    <input id="HdnClassTypeCode" type="hidden" style="width:20px" runat="server" />
</td>
            </tr>
            <tr>
                <td valign="top">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                    <asp:GridView CssClass="grid" ID="GridView1" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" PageSize="20" BorderWidth="0px" CellPadding="0" DataSourceID="SQLDataSource" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="ClassTypeCode,CorpCode,ClassCode" runat="server">
<EmptyDataTemplate>
                            <table id="Table1" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                                style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                                width: 100%; border-collapse: collapse; border-right-width: 0px">
                                <tr align="center" class="grid_head">
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 40px">
                                        序号</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        分类编号</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        分类名称</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        备注</th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField HeaderText="序号" HtmlEncode="false" /><asp:BoundField DataField="ClassCode" HeaderText="分类编号" HtmlEncode="false" /><asp:BoundField HeaderText="分类名称" HtmlEncode="false" /><asp:BoundField DataField="Remark" HeaderText="备注" HtmlEncode="false" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                        <asp:SqlDataSource ID="SQLDataSource" ProviderName="System.Data.SqlClient" SelectCommand="SELECT ClassID,ClassTypeCode,CorpCode,ClassCode,ParentClassCode,
(replicate('^',(len(ClassCode)-1)/3*4)+ClassName) as ClassName,
dbo.GetClassChildNum(ClassTypeCode,ClassCode) as ChildNum,
Remark,IsValid FROM [PT_MultiClasses] 
WHERE ([ClassTypeCode] =@ClassTypeCode)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="ClassTypeCode" QueryStringField="ctc" Type="String"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
