<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Search_right.aspx.cs" Inherits="oa_System_Search_right" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>制度查询</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
		function showEditWin(op)
		{
		    var url ;
		    var cid=document.getElementById('Label1').value;
		    var rid=document.getElementById('hfRecord').value;
		    url ="SystemInfoLock.aspx?ic="+rid;    		   		    
		    return window.showModalDialog(url,window,'resizable:yes;dialogHeight:180px;dialogWidth:400px;center:1;help:0;status:0;');
		}
		function getRecordID(RecordID)
		{
		    window.document.getElementById('hfRecord').value = RecordID;
		}
		 function UpFile(t,RecordCode)
	{
	    //t=3 制度管理	       
		var url = "../../CommonWindow/Annex/AnnexList.aspx?mid="+t+"&rc="+RecordCode+"&at=0&type=1";			
		window.showModalDialog(url,window,'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
	}
		</script>
</head>
<body class="body_frame" scroll="no">
		<form id="Form1" method="post" runat="server">
			
				<table id="tablex" cellspacing="0" cellpadding="0" width="100%" height="100%" align="center" border="0">
				    <tr>
						<td colspan="2" class="td-title"><input type="hidden" id="Label1" style="width:1px" runat="server" />
制度查询</td>
					</tr>
					<tr>
						<td colspan="2" height="20px" class="td-search">
                            制度名称 <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                            <asp:Button ID="btnSearch" Text="" OnClick="btnSearch_Click" runat="server" />
                            <asp:HiddenField ID="hfRecord" runat="server" />
                        </td>
					</tr>
					<tr>
						<td colspan="2" style="width: 100%; height: 100%" valign="top">
						<div id="Box" style="width:100%;height:100%">
						<asp:DataGrid ID="DataGrid1" Width="100%" CssClass="grid" ItemStyle-CssClass="grid_row" HeaderStyle-CssClass="grid_head" AutoGenerateColumns="false" AllowPaging="true" PageSize="16" ToolTip="请双击查看详细信息" OnPageIndexChanged="DataGrid1_PageIndexChanged" OnItemDataBound="DataGrid1_ItemDataBound" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
                                        <%# Container.ItemIndex + 1 %>
                                    </ItemTemplate>

<EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="SystemName" HeaderText="制度名称"></asp:BoundColumn><asp:BoundColumn DataField="SignDate" HeaderText="制订日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="SignMan" HeaderText="制订人"></asp:BoundColumn><asp:BoundColumn HeaderText="现行制度"></asp:BoundColumn><asp:TemplateColumn HeaderText="附件">
<ItemTemplate>
                                        <asp:ImageButton ID="IBAnnex" runat="server" />
                                    </ItemTemplate>
</asp:TemplateColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid></div></td>
					</tr>
				</table>
                <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
			
		</form>
	</body>
</html>
