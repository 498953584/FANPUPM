<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wbsquery.aspx.cs" Inherits="ProjectGSQuery" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>WBS查询</title><meta http-equiv="Content-Type" content="text/html; charset=uft-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<script language="javascript">
			var objRow = null;
			function ParCheck()
			{
				try
				{
					window.parent.document.getElementById('BtnImport').disabled = false;
					window.parent.document.getElementById('btnAdd').disabled = false;
					window.parent.document.getElementById('hdnPR').value = document.getElementById('hdnPR').value;
					window.parent.document.getElementById('hdnPrjName').value = document.getElementById('lblProjectName').innerText;
					window.parent.document.getElementById('btnPodepom').disabled = false;
					window.parent.document.getElementById('hdnPrjGuid').value = document.getElementById('hdnPrjGuid').value;	
				}
				catch(e){}
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
				try
				{
					document.getElementById("btnAdd").disabled = true;
				}
				catch(e){}
			}
			
			function clickAdd()
			{
				var tempObj = window.parent.fraTask;
				tempObj.document.getElementById('hdnType').value = 1;
				tempObj.document.getElementById('hdnWbsType').value = 1;
				tempObj.document.getElementById('hdnProjectCode').value = document.getElementById('hdnProjectCode').value;
				tempObj.document.getElementById('hdnProjectName').value = document.getElementById('hdnProjectName').value;
				tempObj.document.getElementById('hdnPrjGuid').value = document.getElementById('hdnPrjGuid').value;
				
				tempObj.document.getElementById('btnSave').disabled = false;
				
				var taskCode = objRow.cells[1].innerText;
				tempObj.document.getElementById("txtTaskCode").value = "";
				tempObj.document.getElementById("txtParentCode").value = taskCode;
				tempObj.document.getElementById("lbParentCode").value = taskCode;
				tempObj.document.getElementById("txtTaskCode").readonly = false;
				InitDropdownList(objRow.cells[2].innerText,1);
				
				var res = new Array();
				res[0] =1;
				res[1] =1;
				res[2] =document.getElementById('hdnProjectCode').value;
				res[3] =document.getElementById('hdnProjectName').value;
				res[4] =document.getElementById('hdnPrjGuid').value;
				
				res[5] = objRow.cells[1].innerText;
				res[6] ="";
				res[7] =taskCode;
				res[8] =taskCode;

				
//				window.open("TaskEdit111.aspx?jw=1",'','left=150,top=150,width=600,height=180,toolbar=no,status=yes,scrollbars=yes,resizable=no');
			}
			
			function clickUpd()
			{
				var tempObj = window.parent.fraTask;
				tempObj.document.getElementById('hdnType').value = 2;
				tempObj.document.getElementById('hdnWbsType').value = 1;
				tempObj.document.getElementById('hdnProjectCode').value = document.getElementById('hdnProjectCode').value;
				tempObj.document.getElementById('hdnProjectName').value = document.getElementById('hdnProjectName').value;
				tempObj.document.getElementById('hdnPrjGuid').value = document.getElementById('hdnPrjGuid').value;
				tempObj.document.getElementById('btnSave').disabled = false;
				tempObj.document.getElementById("txtTaskName").value =objRow.cells[0].innerText;
				var taskCode = objRow.cells[1].innerText;
				tempObj.document.getElementById("txtTaskCode").value = taskCode.substring(taskCode.length-3,taskCode.length);
				tempObj.document.getElementById("txtParentCode").value = taskCode.substring(0,taskCode.length-3);
				tempObj.document.getElementById("lbParentCode").value = taskCode;
				tempObj.document.getElementById("txtTaskCode").readonly = false;
				InitDropdownList(objRow.cells[2].innerText,0);
				tempObj.document.getElementById("txtUnit").value = objRow.cells[3].innerText;
				tempObj.document.getElementById("txtQuantity").value = objRow.cells[4].innerText;
				tempObj.document.getElementById("DateStart").value = objRow.cells[5].innerText;
				tempObj.document.getElementById("DateEnd").value = objRow.cells[6].innerText;				
				tempObj.document.getElementById("txtUnitPrice").value = objRow.cells[7].innerText;
				tempObj.document.getElementById("HidRemark").value = objRow.cells[9].title;
				//alert(objRow.cells[9].title);
				
			    var res = new Array();
				res[0] =2;
				res[1] =1;
				res[2] =document.getElementById('hdnProjectCode').value;
				res[3] =document.getElementById('hdnProjectName').value;
				res[4] =document.getElementById('hdnPrjGuid').value;
				
				res[5] =objRow.cells[0].innerText;
				res[6] =objRow.cells[1].innerText;
				res[7] =taskCode.substring(taskCode.length-3,taskCode.length);
				res[8] =taskCode.substring(0,taskCode.length-3);
				res[9] = taskCode;
				
				
				res[10] =objRow.cells[3].innerText;
				res[11] =objRow.cells[4].innerText;
				res[12] =objRow.cells[5].innerText;
				res[13] =objRow.cells[6].innerText;
				res[14] =objRow.cells[7].innerText;
												
//				var url="../../../EPC/ProjectLayout/ProjectDistribute/taskedit111.aspx?&jw=2";
//				window.showModalDialog(url,res,"dialogHeight:250px;dialogWidth:550px;center:1;help:0;status:0;");	
				
			}
		
			function clickRow(obj,noteId,isLeaf,level)
			{
			  var hdnConfirm = document.getElementById('HdnIsConfirm').value;		
			  objRow = obj;
			  
			  if(hdnConfirm != "1")
			  {
				document.getElementById('btnDel').disabled = isLeaf;
				document.getElementById('btnResource').disabled = isLeaf;
				
				if (level==3)//4
				{
					document.getElementById('btnAdd').disabled = true;
				}
				else
				{
					document.getElementById('btnAdd').disabled = false;
				}
				if (level==0)
				{
					document.getElementById('btnUpd').disabled = true;
					document.getElementById('btnResource').disabled = true;
				}
				else
				{
					document.getElementById('btnUpd').disabled = false;
				}
				try
				{
					if(objRow.cells[1].innerText.length <= 1)
					{
						document.getElementById('btnDel').disabled = true;
					}
				}
				catch(e){}
				document.getElementById('hdnNoteID').value = noteId;
				try
				{
					window.parent.fraTask.document.getElementById('hdnNoteID').value = noteId;
				}
				catch(e){}
			  }
			  else
			  {
				document.getElementById('btnAdd').disabled = true;
				document.getElementById('btnUpd').disabled = true;
				document.getElementById('btnDel').disabled = true;
			  }
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
			
			function InitDropdownList(Level,type)
			{
				var ddl = window.parent.fraTask.document.getElementById("ddlTaskType");
				if(Level == "单位工程") ddl.options.selectedIndex = 0+type;
				else if(Level == "分部工程") ddl.options.selectedIndex = 1+type;
				else if(Level == "分项工程") ddl.options.selectedIndex = 2+type;
				else if(Level == "子项工程") ddl.options.selectedIndex = 3+type;
			}
			
			function clickScheduleRow(scheduleCode,NoteID,isAllowAdd,isAllowDel,isAllowUpd,isView,workLayer)
			{
				try
				{
					window.parent.document.getElementById('hdnNoteID').value = NoteID;
					var objhdnsc = window.parent.document.getElementById('hdnTaskCode');
					if(objhdnsc!=null)
						objhdnsc.value = scheduleCode;
					var objbtnadd = window.parent.document.getElementById('btnAdd');
					if(objbtnadd!=null)
						objbtnadd.disabled = !isAllowAdd;
					var objbtnDel = window.parent.document.getElementById('btnDel');
					if(objbtnDel!=null)
						objbtnDel.disabled = !isAllowDel;
					var objbtnupd = window.parent.document.getElementById('btnUpd');
					if(objbtnupd!=null)
						objbtnupd.disabled = !isAllowUpd;
					var objhdnview = window.parent.document.getElementById('hdnView');
					if (isView)
					{
						if(objbtnupd!=null)
							objbtnupd.value = "查  看";
						if(objhdnview!=null)
							objhdnview.value = "1";
					}	
					else
					{
						if(objbtnupd!=null)
						objbtnupd.value = "编  辑";
						if(objhdnview!=null)
							objhdnview.value = "0";
					}
					if (workLayer==3)
					{
						objbtnadd.disabled = false;
					}
				}
				catch(e)
				{}
			}
			
			
			function switchDisplay(obj,t1,t2)
			{
				/*在这之前增加你的处理代码*/
				doSwitchDisplay(obj,'tblSchedule','hdnScheduleCodeList',t1,t2,'../../../');//调用样式设置
			}		
			
			//2007-1-5
			function ImportData(PrjCode,t)
			{
				var url = "WbsTemplateImport.aspx?PrjCode="+PrjCode+"&t="+t;
				//alert(url);
				var refresh = window.showModalDialog(url,window,"dialogHeight:500px;dialogWidth:700px;center:1;help:0;status:0;");			
				//if(refresh==true)
				//{  	   
					return true;
				//}
				//else
				//{
				//	return false;
				//}
			}	
				
			function HiddenBtnwbs()
			{
				var Hidden3Value = "";
				try
				{
					Hidden3Value = window.parent.document.getElementById('Hidden3').value;
				}
				catch(e)
				{
					Hidden3Value = "";
				}
				finally
				{
				if(Hidden3Value == "")
				{
					window.document.getElementById('Btn_wbs').style.display = "none";
					window.document.getElementById('BtnRation').style.display = "none";
					window.document.getElementById('Btn_wbs1').style.display = "none";
				}
				}
			//<asp:HyperLink ID="hlQD" NavigateUrl="templete/清单预算.xls" style="DISPLAY:none" runat="server">清单预算模板  </asp:HyperLink>
			//<asp:HyperLink ID="hlDE" NavigateUrl="templete/定额预算模板.xls" style="DISPLAY:none" runat="server">定额预算模板  </asp:HyperLink>
			}
		</script>
	</head>
	<body class="body_clear">
		<form id="Form1" method="post" runat="server">
			<table class="table-none" id="Table2" height="100%" cellSpacing="0" cellPadding="0" width="100%"
				border="0">
				<tr class="td-toolstbar">
					<td align="left" width="0%"><asp:Label ID="lblProjectName" runat="server"></asp:Label></td>
					<td><asp:HyperLink ID="hlQD2" style="DISPLAY: none" NavigateUrl="templete/报价模板.xls" runat="server">  报价模板下载</asp:HyperLink><asp:HyperLink ID="hlQD1" style="DISPLAY: none" NavigateUrl="templete/清单预算模板.xls" runat="server">  清单预算模板下载</asp:HyperLink></td>
				</tr>
				<tr class="td-toolsbar">
					<td align="right" colSpan="2"><asp:Button ID="BtnConfirm" style="DISPLAY: none" Width="80px" Text="预算锁定" CssClass="button-normal" OnClick="BtnConfirm_Click" runat="server" /><asp:Button ID="btnResource" style="DISPLAY: none" Width="80px" Text="资源配置" CssClass="button-normal" Enabled="false" OnClick="btnResource_Click" runat="server" />&nbsp;<input class="button-normal" id="btnAdd" style="DISPLAY: none" onclick="clickAdd();" type="button" value="新  增" name="btnAdd" runat="server" />
&nbsp;<input class="button-normal" id="btnUpd" style="DISPLAY: none" disabled="true" onclick="clickUpd()" type="button" value="编  辑" name="btnUpd" runat="server" />

						<asp:Button ID="btnDel" style="DISPLAY: none" Enabled="false" Text="删  除" CssClass="button-normal" OnClick="btnDel_Click" runat="server" /><asp:Button ID="BtnRation" style="DISPLAY: none" Width="0px" Text="定额预算导入" CssClass="button-normal" OnClick="BtnRation_Click" runat="server" />&nbsp;<asp:Button ID="Btn_wbs" style="DISPLAY: none" Width="75px" Text="报价导入" CssClass="button-normal" OnClick="Btn_wbs_Click" runat="server" />&nbsp;<asp:Button ID="Btn_wbs1" style="DISPLAY: none" Width="80px" Text="清单预算导入" CssClass="button-normal" OnClick="Btn_wbs1_Click" runat="server" />
					</td>
				</tr>
				<tr>
					<td align="left" colSpan="2"><asp:DataGrid ID="DGsum" Width="100%" AutoGenerateColumns="false" ShowHeader="false" ItemStyle-Height="22px" runat="server"><HeaderStyle Wrap="false" Height="0px"></HeaderStyle><Columns><asp:BoundColumn DataField="c1" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="60px"></asp:BoundColumn><asp:BoundColumn DataField="c11" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px"></asp:BoundColumn><asp:BoundColumn DataField="c2" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="60px"></asp:BoundColumn><asp:BoundColumn DataField="c21" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px"></asp:BoundColumn><asp:BoundColumn DataField="c3" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="60px"></asp:BoundColumn><asp:BoundColumn DataField="c31" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px"></asp:BoundColumn></Columns></asp:DataGrid></td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="2" height="100%">
						<div class="gridBox" id="dvScheduleBox"><asp:Table ID="tblSchedule" CssClass="grid" Width="100%" BorderWidth="1px" BorderStyle="Solid" BorderColor="#E0E0E0" CellPadding="0" CellSpacing="0" GridLines="Both" runat="server"><asp:TableRow HorizontalAlign="Center" CssClass="grid_head" runat="server"><asp:TableCell Text="任务名称" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="90px" Text="任务编码" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="70px" Text="类型" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="30px" Text="单位" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Text="工程量" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="70px" Text="开始时间" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="70px" Text="结束时间" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="80px" Text="单价(元)" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="50px" Text="小计(元)" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="150px" Text="备注" Wrap="false" runat="server"></asp:TableCell></asp:TableRow></asp:Table></div>
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
			<input id="hdnProjectCode" style="WIDTH: 10px" type="hidden" name="hdnProjectCode" runat="server" />
<input id="hdnPR" style="WIDTH: 10px" type="hidden" size="1" runat="server" />

			<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl><input id="HdnIsConfirm" type="hidden" size="1" runat="server" />
</form>
		<script language="javascript">
	ParCheck();
		</script>
	</body>
</HTML>
