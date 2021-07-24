<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wbsquery.aspx.cs" Inherits="ProjectGSQuery" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>预算绑定</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>
		<script language="javascript">
			var objRow = null;
			function ParCheck()
			{
				try
				{
					window.parent.document.getElementById('hdnPR').value = document.getElementById('hdnPR').value;
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
			}
			
			function clickAdd()
			{
				var tempObj = window.parent.fraTask;
				tempObj.document.getElementById('hdnType').value = 1;
				tempObj.document.getElementById('hdnWbsType').value = 1;
				tempObj.document.getElementById('hdnProjectCode').value = document.getElementById('hdnProjectCode').value;
				tempObj.document.getElementById('hdnProjectName').value = document.getElementById('hdnProjectName').value;
				tempObj.document.getElementById('hdnPrjGuid').value = document.getElementById('hdnPrjGuid').value
				
				var taskCode = objRow.cells[1].innerText;
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
				var taskCode = objRow.cells[1].innerText;
				InitDropdownList(objRow.cells[2].innerText,0);
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
				document.getElementById('hdnNoteID').value = noteId;
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
				}
			}
			
			window.onload = function(){
			    document.getElementById('hdnProjectName').value="";
			    var tbl = document.getElementById('tblSchedule');
			    var count = 0;
			    if(tbl) {
			        count = tbl.rows.length;
			    }
			    else return ;
			    for(i = 2; i<count;i++)
			    {
			        if(tbl.rows[i]){
			            var rowI = tbl.rows[i];
			            if(rowI.cells[0]){
			                if(rowI.cells[0].firstChild){
			                    if(rowI.cells[0].firstChild.attachEvent){
			                        rowI.cells[0].firstChild.attachEvent('onclick',getTaskCode);
			                    }
			                    else{
			                        rowI.cells[0].firstChild.addEventListener('click',getTaskCode,false);
			                    }
			                }
			            }
			        }
			    }
			}
			
			function getTaskCode(){
			    document.getElementById('hdnProjectName').value="";
			   	var tbl = document.getElementById('tblSchedule');
			   	//var cont=document.getElementById('hdnProjectName').value;
			    var count = 0;
			    if(tbl) {
			        count = tbl.rows.length;
			    }
			    else return ;
			    for(i = 2; i<count;i++)
			    {
			        if(tbl.rows[i]){
			            var rowI = tbl.rows[i];
			            if(rowI.cells[0]){
			                if(rowI.cells[0].firstChild){
			                    if(rowI.cells[0].firstChild.checked)
			                        document.getElementById('hdnProjectName').value=document.getElementById('hdnProjectName').value+rowI.cells[2].innerText+,;
			                }
			            }
			        }
			    }
			    //alert(document.getElementById('hdnProjectName').value);
			}
			
		</script>
	</head>
	<body class="body_clear" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table class="table-none" id="Table2" height="100%" cellSpacing="0" cellPadding="0" width="100%"
				border="0">
				<tr class="td-toolstbar">
					<td>
				        <asp:Button ID="BtnSave" CssClass="button-input" Text="保  存" OnClick="BtnSave_Click" runat="server" />
						<INPUT class="button-input" onclick="window.close();" type="button" value="取  消">&nbsp;
				    </td>
				</tr>
				<tr>
					<td vAlign="top" height="100%">
						<div class="gridBox" id="dvScheduleBox">
						<asp:Table ID="tblSchedule" CssClass="grid" Width="100%" BorderWidth="1px" BorderStyle="Solid" BorderColor="#E0E0E0" CellPadding="0" CellSpacing="0" GridLines="Both" runat="server"><asp:TableRow HorizontalAlign="Center" CssClass="grid_head" runat="server"><asp:TableCell Text="" Wrap="false" Width="25px" runat="server"></asp:TableCell><asp:TableCell Text="任务名称" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Text="任务编码" Wrap="false" runat="server"></asp:TableCell></asp:TableRow></asp:Table></div>
					</td>
				</tr>
			</table>
			<input id="hdnPrjGuid" style="WIDTH: 10px" type="hidden" name="hdnPrjGuid" runat="server" />

			<input id="hdnTaskCode" style="WIDTH: 10px" type="hidden" name="hdnTaskCode" runat="server" />

			<input id="hdnProjectName" style="WIDTH: 10px" type="hidden" name="hdnProjectName" runat="server" />

			<input id="hdnScheduleCodeList" style="WIDTH: 10px" type="hidden" name="hdnScheduleCodeList" runat="server" />
 <input id="hdnView" style="WIDTH: 10px" type="hidden" value="0" name="hdnView" runat="server" />

			<input id="hdnNoteID" style="WIDTH: 10px" type="hidden" value="0" name="hdnNoteID" runat="server" />
&nbsp;
			<input id="hdnProjectCode" style="WIDTH: 10px" type="hidden" name="hdnProjectCode" runat="server" />
<input id="hdnPR" style="WIDTH: 10px" type="hidden" size="1" runat="server" />

			<input id="HdnIsConfirm" type="hidden" size="1" runat="server" />

			    <asp:HiddenField ID="HdnTCode" runat="server" />
			</form>
	</body>
</HTML>
