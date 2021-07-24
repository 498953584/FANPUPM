<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userlist.aspx.cs" Inherits="UserList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>UserList</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>
		<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<script language="javascript">
		
			var ArrYhdm = new Array();
			var ArrYhmc = new Array();
		
			function GetArr()
			{
				
				var yhdmList = window.parent.document.getElementById('hdnUserList').value;
				var yhmcList = window.parent.document.getElementById('lbUseList').innerText;
				
				ArrYhdm = yhdmList.split(,);
				ArrYhmc = yhmcList.split(,);
				
				
				var objs = document.getElementsByTagName("input");			
				
				for(var i=0;i<objs.length;i++)
				{
					if(objs[i].type == "checkbox"  && yhdmList.indexOf(objs[i].value)>-1)
					{
						objs[i].checked = true ;				
					}
					
				}
			}
		
		</script>
	</head>
	<body class="body_frame" bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table class="table-form" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td vAlign="top" height="100%"><asp:DataGrid ID="DataGrid1" CssClass="grid" AutoGenerateColumns="false" Width="100%" DataKeyField="v_yhdm" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="是否参与项目">
<ItemTemplate>
										<input id="CheckBox1" type="checkbox" onclick="check(this);" value='<%# System.Convert.ToString(Eval( "v_yhdm"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />

									</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="v_xm" HeaderText="姓名"></asp:BoundColumn></Columns></asp:DataGrid>
						<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></td>
				</tr>
			</table>
		</form>
		<SCRIPT language="javascript">
		 GetArr();
	function check(obj)
	{
	
	var yhmc = obj.parentNode.parentNode.cells(1).innerText
		
		
		AddOrDel(obj.value,obj.checked,yhmc);
		window.parent.document.getElementById('hdnUserList').value = ArrYhdm.join();
		window.parent.document.getElementById('lbUseList').innerText = ArrYhmc.join();
	}
	
	function AddOrDel(id,yn,yhmc)
	{
		
		if(yn)
		{
			if(getIndexOf(ArrYhdm,id)==-1)
			{
			
			ArrYhdm.push(id);
			ArrYhmc.push(yhmc);
			}
		}
		else
		{
			var ii = getIndexOf(ArrYhdm,id);
			
			ArrYhdm.splice(ii,1);
			var bb = getIndexOf(ArrYhmc,yhmc)
			ArrYhmc.splice(bb,1);
			//alert(bb)
			
			
		}
		
	}
	
		function getIndexOf(arr,itm)
	{
		var idx = -1 ;
		for(var i=0;i<arr.length;i++)
		{
			if(arr[i] == itm)
			{
				idx = i;
				break;
			}
		}
		return idx;
	}
		</SCRIPT>
	</body>
</HTML>
