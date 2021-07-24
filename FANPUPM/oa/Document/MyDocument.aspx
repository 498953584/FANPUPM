<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyDocument.aspx.cs" Inherits="MyDocument" EnableEventValidation="false" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title>我的公文</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<script language="javascript" type="text/javascript">
	function doClickRow(fileID,AuditState)
	{
	    if (Number(AuditState) < 0)
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
		document.getElementById('hdnFileID').value=fileID;
		if(AuditState == "-1")
		{
		    document.getElementById('btnViewWF').disabled = true; 
		      document.getElementById('btnWFPrint').disabled = true;
		}
		else
		{
		    document.getElementById('btnViewWF').disabled = false; 
		      document.getElementById('btnWFPrint').disabled = false;
		}
		document.getElementById('btnView').disabled = false; 
		
	}
	function openMyDocEdit(op,corpCode,userCode)
	{				
		var fileID="";
		if (op==2)
		{
			fileID = document.getElementById('hdnFileID').value;
		}	
		var url= "MyDocumentEdit.aspx?t=" + op + "&code=" + corpCode + "&fid="+ fileID + "&ucd="+userCode;
		return window.showModalDialog(url,window,"dialogHeight:290px;dialogWidth:550px;center:1;help:0;status:0;");
	}
	function OpenViewWF()
    {
        var rid =  document.getElementById('hdnFileID').value ;
      var url = "/ModuleSet/Workflow/AuditViewWF.aspx?ic="+rid;
      return window.showModalDialog(url,window,"dialogHeight:460px;dialogWidth:600px;center:1;help:0;status:0;");		    
    }
     //查看审核记录
    function OpenPrintWF()
    {
           var rid =  document.getElementById('hdnFileID').value ;
      var url = "/ModuleSet/Workflow/AuditViewPrint.aspx?ic="+rid;
      // window.open(url,"");
     return window.showModalDialog(url,window,"dialogHeight:760px;dialogWidth:800px;center:1;help:0;status:0;");		        
    }
  //查看
    function OpenLock()
    {
      var rid =  document.getElementById('hdnFileID').value ;
      var url = "MyDocumentLock.aspx?ic="+rid;
      // window.open(url,"");
        return window.showModalDialog(url,window,"dialogHeight:460px;dialogWidth:600px;center:1;help:0;status:0;");		        
    }
	</script>
</head>
<body class="body_frame" scroll="no" >
    <form id="form1" runat="server">
       <table width="100%"  height="100%" border="0" id="table1" class="table-normal">
    <tr>
        <TD class="td-title" align="center" height="28">我的公文列表</TD>
    </tr>
    <tr id="btnTr" height="24" runat="server"><td class="td-toolsbar" colspan="3" runat="server">
		    <input type="hidden" id="hdnUserCode" name="hdnUserCode" style="WIDTH: 10px" runat="server" />

		    <input type="hidden" id="hdnUserName" name="hdnUserName" style="WIDTH: 10px" runat="server" />

			<input type="hidden" id="hdnFileID" name="hdnFileID" style="WIDTH: 10px" runat="server" />

			<asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />
			<asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
			<asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
			<asp:Button ID="btnStartWF" Text="提交审核" Enabled="false" OnClick="btnStartWF_Click" runat="server" />
			<asp:Button ID="btnViewWF" Enabled="false" Text="查看审核" Width="80px" runat="server" />
			<asp:Button ID="btnWFPrint" Text="查看审核记录" Enabled="false" Width="100px" runat="server" />
            <asp:Button ID="BtnView" Enabled="false" Text="查 看" runat="server" />&nbsp;	
			<asp:Button ID="btnRefresh" Text="刷 新" style="display:none" OnClick="btnRefresh_Click" runat="server" /></td></tr>
    <tr>
        <TD vAlign="top">
			<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
				<asp:DataGrid ID="dgFileList" CssClass="grid" DataKeyField="FileID" AutoGenerateColumns="false" Width="100%" AllowCustomPaging="true" OnItemDataBound="dgFileList_ItemDataBound" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
								<%# Container.ItemIndex + 1 %>
							</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="Title" HeaderText="公文标题"></asp:BoundColumn><asp:BoundColumn DataField="UserName" HeaderText="起草人"></asp:BoundColumn><asp:BoundColumn DataField="RecordDate" HeaderText="起草日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="State" HeaderText="状态"></asp:BoundColumn><asp:BoundColumn DataField="AuditState" HeaderText="" Visible="false"></asp:BoundColumn><asp:TemplateColumn HeaderText="审核事项"><ItemTemplate>
								<asp:Label ID="TxtRemark" Text='<%# Convert.ToString(Eval("Remark")) %>' runat="server"></asp:Label>
							</ItemTemplate></asp:TemplateColumn></Columns><PagerStyle Visible="false"></PagerStyle></asp:DataGrid>
			</div>
		</TD>
    </tr>
    </table>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
