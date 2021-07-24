<object classid="clsid:BD96C556-65A3-11D0-983A-00C04FC29E33" id="dsodata" height="0" width="0"
	 VIEWASTEXT>
</object>
<object ID="RDSDataSpace" CLASSID="clsid:BD96C556-65A3-11D0-983A-00C04FC29E36" STYLE="display: none"
	 VIEWASTEXT>
</object>
<object ID="ProjLang" CLASSID="clsid:9756A817-0537-44db-99DC-F59244F8A23E" CODEBASE="pjcintl.cab#version=10,0,2002,0502"
	TYPE="application/x-oleobject" STYLE="display: none" VIEWASTEXT>
</object>
<object ID="PjAdoInfo" CLASSID="clsid:229C22C0-B5B4-414d-A00C-7669274293B8" CODEBASE="pjclient.cab#version=10,0,2002,0502"
	TYPE="application/x-oleobject" STYLE="display: none" VIEWASTEXT>
</object>
<OBJECT classid="clsid:BD96C556-65A3-11D0-983A-00C04FC29E33" ID="RDC" WIDTH="1" HEIGHT="1" STYLE="display: none" VIEWASTEXT>
</OBJECT>
<OBJECT ID="RDS" WIDTH="1" HEIGHT="1" CLASSID="CLSID:BD96C556-65A3-11D0-983A-00C04FC29E36" STYLE="display: none" VIEWASTEXT>
</OBJECT>
<%
on error resume next
dim intProjectId,strPrjName,strUserCode
intProjectId = trim(request("pc"))
strPrjName = unescape(trim(request("pn")))
strUserCode = trim(request("yhdm"))
 strconn ="Provider=SQLOLEDB.1;Persist Security Info=True;User ID=sa;Initial Catalog=pm2;Password="+trim(request("strconn"))
%>
<html>
	<head>
		<title><%=strPrjName%>----形象进度网络图　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　</title>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<meta name="GENERATOR" Content="Microsoft Visual Studio.NET 7.0">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<meta http-equiv="Expires" content="0">
		<meta http-equiv="Cache-Control" content="no-cache">
		<meta http-equiv="Pragma" content="no-cache">
	</head>
	<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0" onload="javascript:table_onload();" scroll="no">
		<FORM NAME="Form1" ID="Form1" style="margin:0px;">
			<table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="myTABLE" height="100%">
				<TBODY>
					<tr>
						<td align="center" bgcolor="#E7F3FE" height="30"><font size='4'><b><%=strPrjName%>形象进度网络图</b></font></td>
					</tr>
					<tr >
						<td  style="background-image:url(img/toolsbar.jpg)" height="28" ><img src='img/zoomInBtn.gif' onmouseover="this.style.cursor='hand'" onclick='CollapseTS_OnClick();'
								title='放大图形'>|<img src='img/zoomOutBtn.gif' onmouseover="this.style.cursor='hand'" onclick='ExpandTS_OnClick();'
								title='缩小图形'>|<img src='img/gototaskbtn.gif' onmouseover="this.style.cursor='hand'" onclick='GotoSelectedProjBtn_OnClick()'
								title='转到所选任务'>|<img src='img/e.gif' onmouseover="this.style.cursor='hand'" onclick='outexcel()' 
								title='数据导入到Excel中'>|<img src='img/p.gif' onmouseover="this.style.cursor='hand'" onclick='outproj()' title='数据导入到Project中'>|
								<img src='img/download.gif' onmouseover="this.style.cursor='hand'" onclick='downfile()' title='下载甘特图控件'></td>
					</tr>
					<tr>
						<td height="90%"><object classid='clsid:67EF6CD7-CA45-4C9C-882E-48613F134F7C' id='MSPJGrid' data='DATA:application/x-oleobject;BASE64,12zvZ0XKnEyILkhhPxNPfAADAADYEwAA2BMAAA=='
											 VIEWASTEXT>
										</object>
								</td>
					</tr>
				</TBODY>
			</table>
		</FORM>b
		<TABLE CELLPADDING="0" CELLSPACING="0" style="display:none">
			<TR STYLE="width: 100%;">
				<TD ID="idSizingCell" ONRESIZE="SizingCell_OnResize();" STYLE="height: 0px; font-size: ;">
				</TD>
			</TR>
		</TABLE>
	</body>
</html>
<script language="javascript">
document.onmousedown=click   
  function   click()     
  {if   (event.button==2)   
    {
    alert('右键不可用！') 
    }
  }   
  
var Ganttzoom=0
var pjgrid
var DF=document.all('RDS').createobject("RDSServer.DataFactory","http://<%=Request.ServerVariables("SERVER_NAME")%>"); 

//var strConnect="Provider=SQLOLEDB.1;Password=sa;Persist Security Info=True;User ID=sa;Initial Catalog=ERP080616;Data Source=ZYG\\QCSQL2005";
//var strConnect="Provider=SQLOLEDB.1;Persist Security Info=True;User ID=sa;Initial Catalog=ERP080616;Password="+'<% =strconn %>';
var strConnect='<% =strconn %>';
//alert('<% =strconn %>');


function downfile()
{
window.open('GTTsetup.CAB');
}
function table_onload()
{
	init('MSPJGrid');
	mspjset('MSPJGrid');
	
	//window.alert("noinit")
}
function addrow(form, where)
{
	var newRow = theTableBody.insertRow(where);
	newCell = newRow.insertCell();
	newCell.innerHTML = "<object classid='clsid:67EF6CD7-CA45-4C9C-882E-48613F134F7C' codebase='http://yztxp/tzpj/pjcalendar2.ocx' id='MSPJGrid' data='DATA:application/x-oleobject;BASE64,12zvZ0XKnEyILkhhPxNPfAADAADYEwAA2BMAAA==' VIEWASTEXT></object>";
	newCell.align="center";
}


function rdsconn(rdsname)
{
	strSQL="select * FROM EPM_UF_Project('<%=intProjectId%>','<%=strUserCode%>')"//所有任务
    var rs=DF.Query(strConnect, strSQL);
	return rs;
}

function rdsconn2(rdsname)
{
	strSQL="select * FROM EPM_UF_Successor('<%=intProjectId%>','<%=strUserCode%>')"//前置后续任务
//	var rs = new ActiveXObject("ADODB.Recordset"); 
//    rs.open(strSQL, conn); 
var rs=DF.Query(strConnect, strSQL);
	return rs;
}


function init(pjgridname) 
{
	theTable = (document.all) ? document.all.myTABLE : document.getElementById("myTABLE");
	theTableBody = theTable.tBodies[0];
	//pjgrid1=document.all(pjgridname);
	////pjgrid1.binding=rdsconn('RDS');						//控件邦定数据rdsconn('RDS')
	//pjgrid1.SuccessorBinding=rdsconn2('RDS');			//邦定前置任务数据 rdsconn2('RDS')   
	//pjgrid1.FieldList="#TASKID[工作代码],TASKNAME[任务名称],v_jqgz[前置任务],V_DATE1[工程量],V_DATE2[单位],I_GCGQ[工期],TASKSTART[开始时间]<4>,TASKFINISH[结束时间]<4>,TASKOUTLINELEVEL[大纲级别],TaskPercentComplete[完成百分比]<5>{75}";
	
	//pjgrid1.FieldList2="TASKOUTLINELEVEL[大纲级别]";
	//pjgrid1.GanttView = true;	
}

function mspjset(pjgridname)
{
	pjgrid=document.all(pjgridname);
	pjgrid.width='100%';								//控件宽度
	pjgrid.height='100%';								//控件高度
	pjgrid.binding=rdsconn('RDS');						//控件邦定数据rdsconn('RDS')
	pjgrid.SuccessorBinding=rdsconn2('RDS');			//邦定前置任务数据 rdsconn2('RDS')   
	pjgrid.FieldList="TaskUniqueID[标志号]{50},TASKCODE[任务编号],#TASKNAME[任务名称],QUANTITY[工程量],QUANTITYUNIT[单位],WORKDAY[工期],TASKSTART[开始时间]<4>,TASKFINISH[结束时间]<4>,TaskPercentComplete[完成百分比]{75}";
	
	pjgrid.UseProjOLEDBNamesForGantt(1);				//显示摘要图形
    pjgrid.TextDirection = 1;                           //文本方向
    pjgrid.GanttSummaryRollup = false;                  //
    pjgrid.WeekStartOn = 0;								//设置每个星期从星期几开始
	pjgrid.GanttView = true;							//设置是否显示甘特视图
	//pjgrid.Autofilter = false;						//显示表头选
	//pjgrid.Disable();									//设置控件禁用
	//pjgrid.CurrentResID = 1;
	pjgrid.GanttTimeScaleZoom = 0
	pjgrid.GotoSelectedTask()
	pjgrid.Refresh();	
	pjgrid.GetFieldName(1,1)								//控件更新
}

function CollapseTS_OnClick()
{
	pjgrid.CollapseTimeScale()
	pjgrid.Refresh(1);	
}


function ExpandTS_OnClick()
{
	pjgrid.ExpandTimeScale()
	pjgrid.Refresh(1);	
}

function GotoSelectedProjBtn_OnClick() {
	pjgrid.GotoSelectedTask();
	pjgrid.Refresh(1);
}




</script>

<script language="vbscript">
	
function outExcel()
	err.Clear()
	on error resume next
	Set objExcelDoc=CreateObject("Excel.Application") 
	'alert(err.number)	
	if err.number<>0 then
		window.alert("请确认IE设置正确，并且计算安装有Microsoft Excel软件！")
		exit function
	end if
		
	'objExcelDoc.Application.Workbooks.Open "HTTP://<%=Request.ServerVariables("SERVER_NAME")%>/EPC/ConstructSchedule/project/book.xls"
	objExcelDoc.Application.Workbooks.Open "http://localhost/EPC/ConstructSchedule/project/book.xls"
	objExcelDoc.Application.Worksheets(1).name="profile"
	'sqlMark = "select * FROM uf_GetGantTask('<%=intProjectId%>')"
	'sqlMark="select * FROM pm_wbs')"//所有任务
	'sqlMark="select * FROM uf_GetGantTask('<%=intProjectId%>','<%=strUserCode%>','<%=intPhaseType%>')"//所有任务
		sqlMark="select * FROM EPM_UF_Project('<%=intProjectId%>','<%=strUserCode%>')"//所有任务

	'alert(sqlMark)
	set rsMark = DF.Query(strConnect, sqlMark)
	dim intI 
	intI = 2
	do while not rsMark.eof 
		redim arrMark(6)
		arrMark(0)="C"+rsMark("TASKCODE")
		arrMark(1)=trim(rsMark("TASKNAME"))
		arrMark(2)=trim(rsMark("TASKSTART"))
		arrMark(3)=trim(rsMark("TASKFINISH"))
		arrMark(4)=trim(rsMark("TASKRELATION"))
		arrMark(5)=trim(rsMark("WORKDAY"))
		arrMark(6)=trim(rsMark("TaskPercentComplete"))
		set therange=objExcelDoc.Application.Worksheets("profile").range(objExcelDoc.Application.Worksheets("profile").Cells(intI, 1),objExcelDoc.Application.Worksheets("profile").Cells(intI, 7))
		therange.VALUE=arrMark
	intI = intI+1
	rsMark.movenext
	loop
	alert("生成EXCEL成功")

	objExcelDoc.Application.Visible = true
 end function
 
function gettaskcon(arr1,arr2,str)
	if str = "" or str = "," then
		gettaskcon = ""
	else
		dim tmpid,tmpcode
		for i = 0 to ubound(arr1)
			tmpid = arr1(i)
			tmpcode = arr2(i)
			'window.alert("NUM:"+str+"$FIN:"+tmpcode+"$REP:"+tmpid)
			str = replace(str,tmpcode,tmpid,1,-1,0)
			'window.alert(str)
		next
		gettaskcon = right(str,len(str)-1)
	end if
end function
	
function outproj()
	err.Clear()
	on error resume next
     Set objWordDoc =CreateObject("MSProject.Project") '创建一个Word.Document的对象
     if err.number<>0 then
		window.alert("请确认IE设置正确，并且计算安装有Microsoft Project软件！")
		exit function
	end if
	 'sqlproj = "select * FROM uf_GetProjectTask('<%=intProjectId%>')"
	 'sqlproj="select * FROM pm_wbs)"//所有任务
	 'sqlproj="select * FROM uf_GetProjectTask('<%=intProjectId%>','<%=strUserCode%>','<%=intPhaseType%>')"'//所有任务
    sqlproj="select * FROM EPM_UF_Project('<%=intProjectId%>','<%=strUserCode%>')"'//所有任务

     set rsproj = DF.Query(strConnect, sqlproj)
     dim strid,strcode,arrid,arrcode
     'do while not rsproj.eof
	'	strid = strid+"#,"+trim(rsProj("NOTEID"))+"["
	'	strcode = strcode +"#,"+trim(rsProj("TASKCODE"))+"["
	'	rsproj.movenext
	'loop
	'window.alert(strid)
	'window.alert(strcode)
	'arrid = split(strid,"#")
	'arrcode = split(strcode,"#")
	
	objWordDoc.Application.FileNew
	objWordDoc.Application.Visible = false
	objWordDoc.Application.ActiveProject.Calendar.Weekdays(1).Working = True
	objWordDoc.Application.ActiveProject.Calendar.Weekdays(7).Working = True
	rsproj.movefirst
	dim strProjCode,strProjName,dtmProjDate,dtmProjEnd,strProjClass,intProjLen,intIscir,intrtfnotes
	dim i
	i=1
	do while not rsproj.eof
		set thetask=objWordDoc.Application.ActiveProject.Tasks.Add
		thetask.uniqueid = trim(rsProj("NoteID"))
		thetask.name = trim(rsProj("TaskName"))		'任务名称
		thetask.start = trim(rsProj("TaskStart"))		'开始时间
		thetask.finish = trim(rsProj("TaskFinish"))		'结束时间
		intProjLen = trim(rsProj("WORKDAY"))
		thetask.Duration=480*intProjLen	'工期
		thetask.ISCRITICAL = trim(rsProj("IsPivotal"))		'是否关键任务
		thetask.PercentComplete= trim(rsProj("TaskPercentComplete"))	'任务完成度
		thetask.outlinelevel=rsProj("TaskOutlineLevel")
		thetask.Successors = trim(rsProj("TaskRelation"))		
		'strProjClass = gettaskcon(arrid,arrcode,","+trim(rsProj("TASKRELATION")))
		'if strProjClass<>"" then
		'		strProjClass = replace(strProjClass,"[","",1,-1,1)
		'		strProjClass = replace(strProjClass,"]","",1,-1,1)
		'end if

		'thetask.Predecessors=trim(rsProj("TaskRelation"))
		rsproj.movenext
		i = i+1
	loop
	alert("生成project成功")
	objWordDoc.Application.Visible = true
	end function
</script>
