<%@ Page Language="C#" AutoEventWireup="true" CodeFile="datumclasslist.aspx.cs" Inherits="DatumClassList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>DatumClassList</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
		<script language="javascript">
		function clickRow(obj,obj2)		// obj 对应 TypeId ，obj2 对应 IsDelete，obj3 对应 flage＝永久为1
		{
			document.getElementById('hdnType').value = obj;		
		}
		function OpType(obj)
		 {
			
			var TypeId    = document.getElementById('hdnType').value;
			var ParentId  = document.getElementById('hdnParentId').value;
			
			var url;
			var refresh = false;
			var type = obj
			//if(type =="SEE")
			//{
			switch(type)
			{
			case "SEE":
				url = "ClassAdd.aspx?ID="+TypeId+"&optype=SEE";
				break;
			case "EDIT":
				url = "ClassAdd.aspx?ID="+TypeId+"&optype=EDIT";
				break;
			case "ADD":
				url = "ClassAdd.aspx?optype=ADD&ParId="+ParentId+"";
				break;
			} 
			//alert(url);
			
		    refresh = window.showModalDialog(url,window,'dialogHeight:260px;dialogWidth:450px;center:1;help:0;status:0;');	
		    if (refresh==true)
			{  	   
				return true;
			}
			else
			{
				return false;
			}
			     
		 }
		
		 function ReturnCheck(ID,Name)
			{
				var Arr = new Array();
				Arr[0] = ID;
				Arr[1] = Name;
				//alert(Arr[0]);
				window.parent.returnValue= Arr;
				window.parent.close();
			}
		</script>
	</head>
	<body leftmargin="0" scroll="no">
		<form id="Form1" method="post" runat="server">
			<TABLE class="table-none" border="1" cellpadding="0" cellspacing="0" id="Table1" width="300"
				height="100%">
				<TR>
					<TD class="td-title">
						&nbsp;类 别&nbsp;管 理</TD>
				</TR>
				<TR>
					<TD class="td-toolsbar" align="right">
						<input id="hdnTypeName" type="hidden" name="hdnTypeName" runat="server" />
 <input id="hdnParentId" type="hidden" name="hdnParentId" runat="server" />

						<input id="hdnType" type="hidden" name="hdnType" runat="server" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<INPUT class="button-normal" type="button" value="关 闭" onclick="ReturnCheck('','');">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					</TD>
				</TR>
				<TR>
					<TD colSpan="2" height="100%" valign="top">
						<div class="gridBox"><asp:DataGrid ID="DG_List" CssClass="grid" Width="100%" AutoGenerateColumns="false" DataKeyField="TypeId" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号"></asp:TemplateColumn><asp:BoundColumn DataField="TypeName" HeaderText="分类名"></asp:BoundColumn><asp:BoundColumn DataField="Remark" HeaderText="说明"></asp:BoundColumn><asp:BoundColumn Visible="false" DataField="IsVisible" HeaderText="可见性"></asp:BoundColumn><asp:BoundColumn Visible="false" DataField="IsDelete" ReadOnly="true"></asp:BoundColumn></Columns></asp:DataGrid></div>
					</TD>
				</TR>
				<TR>
					<TD class="td-page" colSpan="2">
						<JWControl:PaginationControl ID="PaginationControl1" OnPageIndexChange="PaginationControl1_PageIndexChange" runat="server"></JWControl:PaginationControl>
						<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
