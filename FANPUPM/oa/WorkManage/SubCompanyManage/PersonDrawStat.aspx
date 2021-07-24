<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PersonDrawStat.aspx.cs" Inherits="oa_WorkManage_SubCompanyManage_PersonDrawStat" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title></title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
    <script language="javascript" type="text/javascript">
    function ClickRow(ACRecordID,AuditState)
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
	   if(AuditState == '-1')
      {
         document.getElementById('btnViewWF').disabled = true;
          document.getElementById('btnWFPrint').disabled = true; 
      }
      else
      {
        document.getElementById('btnViewWF').disabled = false;  
         document.getElementById('btnWFPrint').disabled = false;     
      }
		document.getElementById('HdnACRecordID').value = ACRecordID;
		document.getElementById('btnView').disabled = false; 
		frmInStoreroom.location.href = "PersonDrawStatSubTab.aspx?ia="+AuditState+"&ac="+ACRecordID;
		
	}
	function OpenWin(op)
	{
	    var RecordCode = document.getElementById('HdnACRecordID').value;
		var url= "PersonDrawStatEdit.aspx?op=" + op + "&id=" + RecordCode;
		var ref = window.showModalDialog(url,window,"dialogHeight:300px;dialogWidth:450px;center:1;help:0;status:0;");
		if(ref)
		{
		    return true;
		}
		return false;
	}
			     ///查看审核
    function OpenViewWF()
    {
        var rid =  document.getElementById('HdnACRecordID').value ;
      var url = "/ModuleSet/Workflow/AuditViewWF.aspx?ic="+rid;
      return window.showModalDialog(url,window,"dialogHeight:460px;dialogWidth:600px;center:1;help:0;status:0;");		    
    }
           //查看审核记录
    function OpenPrintWF()
    {
           var rid =  document.getElementById('HdnACRecordID').value ;
      var url = "/ModuleSet/Workflow/AuditViewPrint.aspx?ic="+rid;
      // window.open(url,"");
     return window.showModalDialog(url,window,"dialogHeight:760px;dialogWidth:800px;center:1;help:0;status:0;");		        
    }
    
           //查看
    function OpenLock()
    {
      var rid =  document.getElementById('HdnACRecordID').value ;
      var url = "PersonDrawLock.aspx?ic="+rid;
      return window.showModalDialog(url,window,"dialogHeight:260px;dialogWidth:600px;center:1;help:0;status:0;");		        
    }
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">
            <tr>
                <td height="20px" class="td-title">汇总表<asp:ScriptManager ID="SManager" runat="server"></asp:ScriptManager>
                </td>
            </tr>
            <tr>
                <td height="20px" class="td-search">
                    <asp:TextBox ID="txtYear" runat="server"></asp:TextBox>年\r
                    <asp:Button ID="btnSearch" Text="" OnClick="btnSearch_Click" runat="server" /></td>
            </tr>
            <tr>
                <td height="20px" class="td-toolsbar">
                    <asp:UpdatePanel ID="UPanel" runat="server">
<ContentTemplate>
                    <asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />
                    <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
                    <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
                    <asp:Button ID="btnStartWF" Text="提交审核" Enabled="false" OnClick="btnStartWF_Click" runat="server" />
                    <asp:Button ID="btnViewWF" Enabled="false" Text="查看审核" Width="80px" runat="server" />                    
                    <asp:Button ID="btnWFPrint" Text="查看审核记录" Enabled="false" Width="100px" runat="server" />
                    <input id="HdnACRecordID" type="hidden" style="width:20px" runat="server" />

                    <asp:Button ID="BtnView" Enabled="false" Text="查 看" runat="server" />
                    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
                        </ContentTemplate>
</asp:UpdatePanel>
                    </td>
            </tr>
            <tr>
                <td valign="top" height="50%">
                    <asp:UpdatePanel ID="UPanelGV" runat="server">
<ContentTemplate>
                            <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                    <asp:GridView CssClass="grid" ID="GVBook" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" BorderWidth="0px" CellPadding="0" OnRowDataBound="GVBook_RowDataBound" OnPageIndexChanging="GVBook_PageIndexChanging" runat="server">
<EmptyDataTemplate>
                            <table id="Tablex" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                                style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                                width: 100%; border-collapse: collapse; border-right-width: 0px">
                                <tr align="center" class="grid_head">
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        年</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        月</th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        公司名称</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 90px">
                                        总金额(元)</th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        状态</th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField HeaderText="年" HtmlEncode="false" /><asp:BoundField HeaderText="月" HtmlEncode="false" /><asp:BoundField HeaderText="公司名称" HtmlEncode="false" /><asp:BoundField DataField="SumMoney" HeaderText="总金额(元)" DataFormatString="{0:f2}" HtmlEncode="false" /><asp:BoundField HeaderText="状态" HtmlEncode="false" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                    </div>
                        </ContentTemplate>
</asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td valign="top" height="50%">
                    <iframe id="frmInStoreroom" frameborder="0" width="100%" height="100%" src="PersonDrawStatSubTab.aspx?ia=0&ac=" runat="server"></iframe>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
