<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileDestroy.aspx.cs" Inherits="oa_FileManage_FileDestroy" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"><title></title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
    <script language="javascript" type="text/javascript">
    function ClickRow(RecordCode,AuditState,IsConfirm)
	{
	    if(AuditState == "-1")
	    {
		    document.getElementById('btnEdit').disabled = false;
		    document.getElementById('btnDel').disabled = false;
		    document.getElementById('btnStartWF').disabled = false;
		    
		}
		else
		{
		    document.getElementById('btnEdit').disabled = true;
		    document.getElementById('btnDel').disabled = true;
		    document.getElementById('btnStartWF').disabled = true;
		     
		}
		if(AuditState == "1")
	    {
	        if(IsConfirm == "0")
	        {
	            document.getElementById('btnReportLoss').disabled = false;
	        }
	        else
	        {
	            document.getElementById('btnReportLoss').disabled = true;
	        }
	    }
	   if(AuditState == '-1')
      {
         document.getElementById('btnViewWF').disabled = true; 
         document.getElementById('btnWFPrint').disabled = true;
      }
      else
      {
       document.getElementById('btnWFPrint').disabled = false;
        document.getElementById('btnViewWF').disabled = false;       
      }
      document.getElementById('btnView').disabled = false; 
		document.getElementById('HdnRecordCode').value = RecordCode;
		frmBooksStockAss.location.href = "FileDestroyAssTab.aspx?ia="+AuditState+"&ac="+RecordCode;
	}
	function OpenWin(op,LibraryCode)
	{
	    var RecordCode = "";
	    if(op == 'upd')
	    {
	        RecordCode = document.getElementById('HdnRecordCode').value;
	        document.getElementById('HdnRecordCode').value = "";
	    }
		var url= "FileDestroyEdit.aspx?t=" + op + "&lc="+LibraryCode+"&ac=" + RecordCode;
		var ref = window.showModalDialog(url,window,"dialogHeight:400px;dialogWidth:450px;center:1;help:0;status:0;");
		if(ref)
		{
		    return true;
		}
		return false;
	}
	     ///查看审核
    function OpenViewWF()
    {
        var rid =  document.getElementById('HdnRecordCode').value ;
      var url = "/ModuleSet/Workflow/AuditViewWF.aspx?ic="+rid;
      return window.showModalDialog(url,window,"dialogHeight:460px;dialogWidth:600px;center:1;help:0;status:0;");		    
    }
      //查看审核记录
    function OpenPrintWF()
    {
           var rid =  document.getElementById('HdnRecordCode').value ;
      var url = "/ModuleSet/Workflow/AuditViewPrint.aspx?ic="+rid;
      // window.open(url,"");
     return window.showModalDialog(url,window,"dialogHeight:760px;dialogWidth:800px;center:1;help:0;status:0;");		        
    }
     //查看
    function OpenLock()
    {
      var rid =  document.getElementById('HdnRecordCode').value ;
      var url = "FileDestroyLock.aspx?ic="+rid;
      // window.open(url,"");
        return window.showModalDialog(url,window,"dialogHeight:460px;dialogWidth:600px;center:1;help:0;status:0;");		        
    }
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">
            <tr>
                <td height="20px" class="td-title">档案销毁申请记录</td>
            </tr>
            <tr>
                <td height="20px" class="td-toolsbar">
                    <asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />
                    <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
                    <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
                    <asp:Button ID="btnStartWF" Text="提交审核" Enabled="false" OnClick="btnStartWF_Click" runat="server" />
                    <asp:Button ID="btnReportLoss" Text="销毁确认" Enabled="false" OnClick="btnReportLoss_Click" runat="server" />
                    <asp:Button ID="btnViewWF" Enabled="false" Text="查看审核" Width="80px" runat="server" />
                    <asp:Button ID="btnWFPrint" Text="查看审核记录" Enabled="false" Width="100px" runat="server" />
                    <asp:Button ID="BtnView" Enabled="false" Text="查 看" runat="server" />&nbsp;
                    <input id="HdnRecordCode" type="hidden" style="width:20px" runat="server" />

                    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
                    </td>
            </tr>
            <tr>
                <td valign="top" height="50%">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                    <asp:GridView CssClass="grid" ID="GVBook" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" BorderWidth="0px" CellPadding="0" DataSourceID="SQLDataSource" OnRowDataBound="GVBook_RowDataBound" runat="server">
<EmptyDataTemplate>
                            <table id="Table1" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                                style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                                width: 100%; border-collapse: collapse; border-right-width: 0px">
                                <tr align="center" class="grid_head">
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 40px">
                                        序号</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        申请人</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        申请时间</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        状态</th>
                                    <th scope="col">
                                        备注</th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField HeaderText="序号" /><asp:BoundField HeaderText="申请人" HtmlEncode="false" /><asp:BoundField DataField="RecordDate" HeaderText="申请时间" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:BoundField HeaderText="状态" HtmlEncode="false" /><asp:BoundField DataField="Remark" HeaderText="备注" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                        <asp:SqlDataSource ID="SQLDataSource" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [OA_File_DestroyMain] WHERE ([LibraryCode] = @LibraryCode) ORDER BY [UserCode]" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="LibraryCode" QueryStringField="lc" Type="String"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
                    </div>
                </td>
            </tr>
            <tr>
                <td valign="top" height="50%">
                    <iframe id="frmBooksStockAss" frameborder="0" width="100%" height="100%" src="FileDestroyAssTab.aspx?ia=-3&ac=00000000-0000-0000-0000-000000000000" runat="server"></iframe>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
