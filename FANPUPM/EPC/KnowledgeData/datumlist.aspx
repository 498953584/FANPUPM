<%@ Page Language="C#" AutoEventWireup="true" CodeFile="datumlist.aspx.cs" Inherits="DatumList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>DatumList</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>
		<script language="javascript">
		function clickRow(obj,flag)
		{
			//alert(obj2);
			document.getElementById('hdnCode').value = obj;
			if(flag!="True")
			{
			    document.getElementById('btnEdit').disabled = false;
			    document.getElementById('btnDel').disabled = false;
			}
			else
			{
			    document.getElementById('btnEdit').disabled = true;
			    document.getElementById('btnDel').disabled = true;
			}
			document.getElementById('btnSee').disabled = false;
			
			
		}
		function OpType(obj,PrjCode)
		{
			var TypeId    = document.getElementById('hdnTypeId').value;			
			var DatumCode   = document.getElementById('hdnCode').value;
			var url;
			var refresh = false;
			var type = obj
			//if(type =="SEE")
			//{
			switch(type)
			{
			case "SEE":
				url = "DatumAdd.aspx?DatumCode="+DatumCode+"&optype=SEE&PrjCode="+PrjCode;
				break;
			case "EDIT":
				url = "DatumAdd.aspx?DatumCode="+DatumCode+"&optype=EDIT&PrjCode="+PrjCode;
				break;
			case "ADD":
				url = "DatumAdd.aspx?optype=ADD&TypeId="+TypeId+"&PrjCode="+PrjCode;
				break;
			} 
			refresh = window.showModalDialog(url,window,'dialogHeight:460px;dialogWidth:650px;center:1;help:0;status:0;');	
			if (refresh==true)
			{  	   
				return true;
			}
			else
			{
				return false;
			}
		}
		 
		</script>
	</head>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE class="table-none" id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%"
				border="1">
				<TR>
					<TD class="td-title">资 料 列 表</TD>
				</TR>
				<TR>
					<TD class="td-toolsbar" align="right">&nbsp; <input id="hdnCode" type="hidden" name="hdnCode" runat="server" />
<input id="AffirmState" type="hidden" runat="server" />

						<input id="hdnTypeId" type="hidden" name="hdnTypeId" runat="server" />
&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="BtnAdd" CssClass="button-normal" Text="新  增" OnClick="BtnAdd_Click" runat="server" />&nbsp;<asp:Button ID="BtnEdit" CssClass="button-normal" Text="编  辑" Enabled="false" OnClick="BtnEdit_Click" runat="server" />&nbsp;<asp:Button ID="BtnDel" CssClass="button-normal" Text="删  除" Enabled="false" OnClick="BtnDel_Click" runat="server" />
						<asp:Button ID="BtnSee" CssClass="button-normal" Text="查  看" Enabled="false" runat="server" /></TD>
				</TR>
				<TR>
					<td vAlign="top" colSpan="2" height="100%">
						<div class="gridBox">
						<asp:DataGrid ID="GrdDatum" CssClass="grid" DataKeyField="KnowledgeCode" AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="20" OnPageIndexChanged="DataGrid1_PageIndexChanged" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号"></asp:TemplateColumn><asp:HyperLinkColumn DataTextField="DatumName" HeaderText="标题"></asp:HyperLinkColumn><asp:BoundColumn HeaderText="作 者"></asp:BoundColumn><asp:BoundColumn DataField="AddDate" HeaderText="添加日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="UpdateDate" HeaderText="更改日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn Visible="false" DataField="Author"></asp:BoundColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid></div>
						<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></td>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
