<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wbsquery.aspx.cs" Inherits="WBSQuery" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>WBSQuery</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>
		<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<script language="javascript">
			var objRow = null;
			function clickAdd()
			{
				var tempObj = window.parent.fraTask;
				tempObj.document.getElementById('hdnType').value = 1;
				tempObj.document.getElementById('hdnWbsType').value = 2;
				tempObj.document.getElementById('hdnProjectCode').value = document.getElementById('hdnProjectCode').value;
				tempObj.document.getElementById('hdnProjectName').value = document.getElementById('hdnProjectName').value;
				tempObj.document.getElementById('hdnPrjGuid').value = document.getElementById('hdnPrjGuid').value;
				tempObj.document.getElementById('btnSave').disabled = false;
				var taskCode = objRow.cells[1].innerText;
				tempObj.document.getElementById("txtTaskCode").value = "";//taskcode.substring(taskcode.length-3,taskcode.length);
				tempObj.document.getElementById("txtParentCode").value = taskCode;//taskcode.substring(0,taskcode.length-3);
				tempObj.document.getElementById("txtTaskCode").readonly = false;
			}
			
			function clickUpd()
			{
				var tempObj = window.parent.fraTask;
				tempObj.document.getElementById('hdnType').value = 2;
				tempObj.document.getElementById('hdnWbsType').value = 2;
				tempObj.document.getElementById('hdnProjectCode').value = document.getElementById('hdnProjectCode').value;
				tempObj.document.getElementById('hdnProjectName').value = document.getElementById('hdnProjectName').value;
				tempObj.document.getElementById('hdnPrjGuid').value = document.getElementById('hdnPrjGuid').value;
				tempObj.document.getElementById('btnSave').disabled = false;
				tempObj.document.getElementById("txtTaskName").value =objRow.cells[0].innerText;
				var taskCode = objRow.cells[1].innerText;
				tempObj.document.getElementById("txtTaskCode").value = taskCode.substring(taskCode.length-3,taskCode.length);
				tempObj.document.getElementById("txtParentCode").value = taskCode.substring(0,taskCode.length-3);
				tempObj.document.getElementById("txtTaskCode").readonly = false;
				InitDropdownList(objRow.cells[2].innerText);
				tempObj.document.getElementById("txtUnit").value = objRow.cells[3].innerText;
				tempObj.document.getElementById("txtQuantity").value = objRow.cells[4].innerText;
				tempObj.document.getElementById("DateStart").value = objRow.cells[5].innerText;
				tempObj.document.getElementById("DateEnd").value = objRow.cells[6].innerText;				
				tempObj.document.getElementById("txtUnitPrice").value = objRow.cells[7].innerText;
			}
		
			function clickRow(obj,noteId,isLeaf,level)
			{
				//objRow = obj;
				//document.getElementById('btnDel').disabled = isLeaf;
				//document.getElementById('btnResource').disabled = isLeaf;
				//if (level==3)
				//{
				//	document.getElementById('btnAdd').disabled = true;
				//}
				//else
				//{
				//	document.getElementById('btnAdd').disabled = false;
				//}
				//if (level==0)
				//{
				//	document.getElementById('btnUpd').disabled = true;
				//}
				//else
				//{
				//	document.getElementById('btnUpd').disabled = false;
				//}
				//document.getElementById('hdnNoteID').value = noteId;
				//window.parent.fraTask.document.getElementById('hdnNoteID').value = noteId;
				
				try
					{
						if((level==3 && isLeaf==false) || level==4)
						{
							window.parent.window.document.getElementById('BtnView').disabled = false;
						}
						else
						{
							window.parent.window.document.getElementById('BtnView').disabled = true;
						}
					}
					catch(e){}
			}
			
			function clickResource()
			{
				var taskCode = objRow.cells[1].innerText;
				var projectCode = document.getElementById('hdnProjectCode').value;
				var url = "ResourceDistribute.aspx?pc="+projectCode+"&tc="+taskCode+"&wbs=1";
				window.open(url);
			}
			
			function InitDropdownList(Level)
			{
				var ddl = window.parent.fraTask.document.getElementById("ddlTaskType");
				if(Level == "��λ����") ddl.options.selectedIndex = 0;
				else if(Level == "�ֲ�����") ddl.options.selectedIndex = 1;
				else if(Level == "�����") ddl.options.selectedIndex = 2;
			}

			function switchDisplay(obj,t1,t2)
			{
				/*����֮ǰ������Ĵ������*/
				doSwitchDisplay(obj,'tblSchedule','hdnScheduleCodeList',t1,t2,'../../../');//������ʽ����
			}
		</script>
	</head>
	<body class="body_clear">
		<form id="Form1" method="post" runat="server">
			<table class="table-none" id="Table2" height="100%" cellSpacing="0" cellPadding="0" width="100%"
				border="0">
				<tr class=td-toolsbar>
					<td align=left><asp:Label ID="lblProjectName" runat="server"></asp:Label></td>
					<td align="right">&nbsp;
						<asp:Button ID="btnResource" Width="80px" CssClass="button-normal" Text="��Դ����" Visible="false" runat="server" />
						<asp:Button ID="btnDel" CssClass="button-normal" Text="ɾ  ��" Enabled="false" Visible="false" runat="server" />
							 <asp:Button ID="btnExp" CssClass="button-normal" Text="�� ��" Width="0px" OnClick="btnExp_Click1" runat="server" />
					</td>
				</tr>
				<tr>
					<td colspan="2" vAlign="top" height="100%">
						<div id="dvScheduleBox" class="gridBox"><asp:Table ID="tblSchedule" CssClass="grid" GridLines="Both" CellSpacing="0" CellPadding="0" BorderColor="#E0E0E0" BorderStyle="Solid" BorderWidth="1px" Width="100%" runat="server"><asp:TableRow HorizontalAlign="Center" CssClass="grid_head" runat="server"><asp:TableCell Text="��������" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="90px" Text="�������" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="70px" Text="����" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="30px" Text="��λ" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Text="������" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="70px" Text="��ʼʱ��" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="70px" Text="����ʱ��" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="80px" Text="����(Ԫ)" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="50px" Text="С��(Ԫ)" Wrap="false" runat="server"></asp:TableCell></asp:TableRow></asp:Table>
						</div>
					</td>
				</tr>
			</table>
			<JWControl:PersistScrollPosition ID="PersistScrollPosition1" ControlToPersist="dvScheduleBox" runat="server"></JWControl:PersistScrollPosition>&nbsp;&nbsp;
			<input id="hdnPrjGuid" style="WIDTH: 10px" type="hidden" name="hdnPrjGuid" runat="server" />

			<input id="hdnTaskCode" style="WIDTH: 10px" type="hidden" name="hdnTaskCode" runat="server" />

			<input id="hdnProjectName" style="WIDTH: 10px" type="hidden" name="hdnProjectName" runat="server" />

			<input id="hdnScheduleCodeList" style="WIDTH: 10px" type="hidden" name="hdnScheduleCodeList" runat="server" />
 <input id="hdnView" style="WIDTH: 10px" type="hidden" value="0" name="hdnView" runat="server" />

			<input id="hdnNoteID" style="WIDTH: 10px" type="hidden" value="0" name="hdnNoteID" runat="server" />
&nbsp;
			<input id="hdnProjectCode" type="hidden" style="WIDTH: 10px" name="hdnProjectCode" runat="server" />
<input id="hdnPR" style="WIDTH: 10px" type="hidden" size="1" name="hdnPR" runat="server" />

			<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
			<script language="javascript">
			try
			{
				window.parent.document.getElementById("HdnProjectCode").value = document.getElementById("hdnProjectCode").value;
			}
			catch(e){}
			try
			{
				window.parent.document.getElementById("HdnProjectName").value = document.getElementById("hdnProjectName").value;
			}
			catch(e){}
			try
			{
				window.parent.document.getElementById("BtnSchedulePlan").disabled = false;
				window.parent.document.getElementById("BtnGTT").disabled = false;
			}
			catch(e){}
			try
			{
				window.parent.document.getElementById("BtnResource").disabled = false;
			}
			catch(e){}
			try
			{
				window.parent.document.getElementById("BtnView").disabled = false;
			}
			catch(e){}
			</script>
		</form>
	</body>
</HTML>
