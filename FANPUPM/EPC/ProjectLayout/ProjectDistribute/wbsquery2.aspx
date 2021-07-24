<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wbsquery2.aspx.cs" Inherits="CostTaskAlter" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>CostTaskAlter</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<script language="javascript">
			var objRow = null;
			
			function clickUpd()
			{				
				var tempObj = window.parent.fraTask;
				tempObj.document.getElementById('hdnType').value = 2;
				tempObj.document.getElementById('hdnWbsType').value = 1;
				tempObj.document.getElementById('hdnProjectCode').value = document.getElementById('hdnProjectCode').value;
				tempObj.document.getElementById('hdnProjectName').value = document.getElementById('hdnProjectName').value;
				//tempObj.document.getElementById('hdnPrjGuid').value = document.getElementById('hdnPrjGuid').value;
				tempObj.document.getElementById('btnSave').disabled = false;
				var taskName=objRow.cells[0].innerText;
				tempObj.document.getElementById("txtTaskName").value =taskName;
				//tempObj.document.getElementById("txtTaskName").value =objRow.cells[0].innerText;
				var taskCode = objRow.cells[1].innerText;
				tempObj.document.getElementById("lbParentCode").value=taskCode.substring(0,taskCode.length-3);
				tempObj.document.getElementById("txtTaskCode").value = taskCode.substring(taskCode.length-3,taskCode.length);
				tempObj.document.getElementById("txtParentCode").value = taskCode.substring(0,taskCode.length-3);
				tempObj.document.getElementById("txtTaskCode").readonly = false;
				InitDropdownList(objRow.cells[2].innerText,0);
				tempObj.document.getElementById("txtUnit").value = objRow.cells[3].innerText;
				tempObj.document.getElementById("txtQuantity").value = objRow.cells[4].innerText;
				tempObj.document.getElementById("DateStart").value = objRow.cells[5].innerText;
				tempObj.document.getElementById("DateEnd").value = objRow.cells[6].innerText;				
				tempObj.document.getElementById("txtUnitPrice").value = objRow.cells[7].innerText;
			}
		
			function clickRow(obj,noteId,isLeaf,level,IsAlter)
			{
				var hdnConfirm = document.getElementById('HdnIsConfirm').value;	
				objRow = obj;
				
				if(hdnConfirm == "1")
				{
					if (level==0)
					{
						document.getElementById('btnUpd').disabled = true;
						document.getElementById('btnResource').disabled = true;
					}
					else
					{
						document.getElementById('btnUpd').disabled = false;
						if((IsAlter == "True") && (isLeaf == false)) //bincat，如果是变更过的任务，则可以配置资源
						{
							document.getElementById('btnResource').disabled = false;
						}
						else
						{				
							document.getElementById('btnResource').disabled = true;
						}
						try
						{
							parent.window.document.getElementById('TxtAlterQuantity').value = obj.cells[4].innerText;
							parent.window.document.getElementById('HdnTaskCode').value = obj.cells[1].innerText;
						}
						catch(e){}
						document.getElementById('hdnNoteID').value = noteId;
						try
						{
							window.parent.fraTask.document.getElementById('hdnNoteID').value = noteId;
						}
						catch(e){}
					}
				}
			}
			
			function switchDisplay(obj,t1,t2)
			{
				/*在这之前增加你的处理代码*/
				doSwitchDisplay(obj,'tblSchedule','hdnScheduleCodeList',t1,t2,'../../../');//调用样式设置
			}
			
			function InitDropdownList(Level,type)
			{
				var ddl = window.parent.fraTask.document.getElementById("ddlTaskType");
				if(Level == "单位工程") ddl.options.selectedIndex = 0+type;
				else if(Level == "分部工程") ddl.options.selectedIndex = 1+type;
				else if(Level == "分项工程") ddl.options.selectedIndex = 2+type;
				else if(Level == "子项工程") ddl.options.selectedIndex = 3+type;
			}
			
			function clickResource()
			{
				var taskCode = null;
				try
				{
					 taskCode = objRow.cells[1].innerText;
				}
				catch(e){}
				
				var projectCode = document.getElementById('hdnProjectCode').value;
				if(taskCode == null || taskCode.length<9)
				{
					alert('请选择分项工程或子项工程!');
					return false;
				}
				else
				{
					var url = "ResourceDistribute.aspx?pc="+projectCode+"&tc="+taskCode+"&wbs=1";
					var refresh = window.showModalDialog(url,window,"dialogHeight:500px;dialogWidth:700px;center:1;help:0;status:0;");
					if(refresh==true)
					{  	   
						return true;
					}
					else
					{
						return false;
					}
				}
			}
		</script>
	</head>
	<body class="body_clear">
		<form id="Form1" method="post" runat="server">
			<table class="table-none" id="Table2" height="100%" cellSpacing="0" cellPadding="0" width="100%"
				border="0">
				<tr class="td-toolsbar">
					<td align="left"><asp:Label ID="lblProjectName" runat="server"></asp:Label></td>
					<td align="right">&nbsp;<input id="btnUpd" type="button" value="任务变更" name="btnUpd" onclick="clickUpd()" enabled="False" runat="server" />
&nbsp;
						<asp:Button ID="btnResource" Text="资源配置" CssClass="button-normal" Width="80px" Enabled="false" OnClick="btnResource_Click" runat="server" />&nbsp;
					</td>
				</tr>
				<tr>
					<td colspan="2" vAlign="top" height="100%">
						<div id="dvScheduleBox" class="gridBox"><asp:Table ID="tblSchedule" CssClass="grid" GridLines="Both" CellSpacing="0" CellPadding="0" BorderColor="#E0E0E0" BorderStyle="Solid" BorderWidth="1px" Width="100%" runat="server"><asp:TableRow HorizontalAlign="Center" CssClass="grid_head" runat="server"><asp:TableCell Text="任务名称" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="90px" Text="任务编码" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="70px" Text="类型" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="30px" Text="单位" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Text="工程量" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="70px" Text="开始时间" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="70px" Text="结束时间" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="80px" Text="单价(元)" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="50px" Text="小计(元)" Wrap="false" runat="server"></asp:TableCell></asp:TableRow></asp:Table>
						</div>
					</td>
				</tr>
			</table>
			<input id="hdnTaskCode" style="WIDTH: 10px" type="hidden" name="hdnTaskCode" runat="server" />

			<input id="hdnProjectName" style="WIDTH: 10px" type="hidden" name="hdnProjectName" runat="server" />

			<input id="hdnNoteID" style="WIDTH: 10px" type="hidden" value="0" name="hdnNoteID" runat="server" />

			<input id="HdnIsConfirm" type="hidden" size="1" name="HdnIsConfirm" runat="server" />

			<input id="hdnScheduleCodeList" style="WIDTH: 10px" type="hidden" name="hdnScheduleCodeList" runat="server" />
 <input id="hdnProjectCode" type="hidden" style="WIDTH: 10px" name="hdnProjectCode" runat="server" />

		</form>
	</body>
</HTML>
