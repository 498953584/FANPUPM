<%@ Page Language="C#" AutoEventWireup="true" CodeFile="calendar.aspx.cs" Inherits="calendar" %>

<HTML>
	<head runat="server"><title>日历</title>
		<STYLE TYPE="text/css">
.normal{FONT-FAMILY: "宋体"; FONT-SIZE: 9pt;BACKGROUND: #ffffff}
.today {FONT-FAMILY: "宋体"; FONT-SIZE: 9pt;BACKGROUND: #cccccc}
.select {FONT-FAMILY: "宋体"; FONT-SIZE: 9pt;BACKGROUND: #6699cc}
.satday{FONT-FAMILY: "宋体"; FONT-SIZE: 9pt;color:green}
.sunday{FONT-FAMILY: "宋体"; FONT-SIZE: 9pt;color:red}
.days {FONT-FAMILY: "宋体"; FONT-SIZE: 9pt;font-weight:bold}
</STYLE>
		<SCRIPT LANGUAGE="JavaScript">
//中文月份,如果想显示英文月份，修改下面的注释
/*var months = new Array("January?, "February?, "March",
"April", "May", "June", "July", "August", "September",
"October", "November", "December");*/
var months = new Array("一月", "二月", "三月",
"四月", "五月", "六月", "七月", "八月", "九月",
"十月", "十一月", "十二月");
var daysInMonth = new Array(31, 28, 31, 30, 31, 30, 31, 31,
30, 31, 30, 31);
//中文周 如果想显示 英文的，修改下面的注释
/*var days = new Array("Sunday", "Monday", "Tuesday",
"Wednesday", "Thursday", "Friday", "Saturday");*/
var days = new Array("日,一", "二", "三,四", "五", "六");


function getDays(month, year) {
//下面的这段代码是判断当前是否是闰年的
if (1 == month)
return ((0 == year % 4) && (0 != (year % 100))) ||
(0 == year % 400) ? 29 : 28;
else
return daysInMonth[month];
}


function getToday() {
//得到今天的年,月,日
this.now = new Date();
this.year = this.now.getFullYear();
this.month = this.now.getMonth();
this.day = this.now.getDate();
}


today = new getToday();


function newCalendar() {
today = new getToday();
var parseYear = parseInt(document.all.year[document.all.year.selectedIndex].text);
var newCal = new Date(parseYear,document.all.month.selectedIndex, 1);
var day = -1;
var startDay = newCal.getDay();
var daily = 0;
if ((today.year == newCal.getFullYear()) &&(today.month == newCal.getMonth()))
day = today.day;
var tableCal = document.all.calendar.tBodies.dayList;
var intDaysInMonth =getDays(newCal.getMonth(), newCal.getFullYear());
for (var intWeek = 0; intWeek < tableCal.rows.length;intWeek++)
for (var intDay = 0;intDay < tableCal.rows[intWeek].cells.length;intDay++)
{
var cell = tableCal.rows[intWeek].cells[intDay];
if ((intDay == startDay) && (0 == daily))
daily = 1;
if(day==daily)
//今天，调用今天的Class
cell.className = "today";
else if(intDay==6)
//周六
cell.className = "sunday";
else if (intDay==0)
//周日
cell.className ="satday";
else
//平常
cell.className="normal";
if ((daily > 0) && (daily <= intDaysInMonth))
{ 
cell.innerText = daily;
daily++;
}
else
cell.innerText = "";
}
}



function Cancel() {
window.returnValue=-1;
window.close();
}
function qd()
{
window.returnValue=document.all.year.value + "-" + document.all.month.value+"-"+"1";
window.close();
}

function setDateFocus()
{

newCalendar();
event.srcElement.className = "select";
event.srcElement.title='点击选中';
}
function getDate() {
var sDate;
//这段代码处理鼠标点击的情况
if ("TD" == event.srcElement.tagName)
if ("" != event.srcElement.innerText)
{
var ParentWin=window.dialogArguments;	//此处生成生成一个具体实例
ParentWin.returnYear=document.all.year.value;
ParentWin.returnMonth=document.all.month.value;
ParentWin.returnDay=event.srcElement.innerText;

//window.returnValue=returnYear+returnMonth+returnDay;
window.close();
}
}
function mouse_over(obj)
{
   obj.style.backgroundColor="#FFFDCB";
}

function mouse_out(obj)
{
   obj.style.backgroundColor="";
}
		</SCRIPT>
	</head>
	<BODY ONLOAD="newCalendar()" scroll="no">
<table width="240" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td><img src="img/nl1.gif" height="32"></td>
  </tr>
</table>
<table width="340" border="0" cellspacing="0" cellpadding="0">
  <tr> 
    <td width="43" valign="top" background="img/nlld.gif"><img src="img/rl_r2_c1.gif" width="43" height="33"></td>
    <td rowspan="2" height="50"> 
      <div align="center" colspan="4" height="200px">
        <table id="calendar" width="100%" border="0" cellspacing="1" cellpadding="0" height="100" bgcolor="#3399CC">
          <tr height="30"> 
            <td colspan="7" align="CENTER" bgcolor="#3399CC"> 
              <select id="year" onChange="newCalendar()" name="select">
                <script language="JavaScript">
							for (var intLoop = today.year-59; intLoop < (today.year + 10);
							intLoop++)
							document.write("<OPTION VALUE= " + intLoop + " " +
							(today.year == intLoop ?
							"Selected" : "") + ">" +
							intLoop);
						</script>
              </select>年
              <select id="month" onChange="newCalendar()" name="select2">
                <script language="JavaScript">
for (var intLoop = 0; intLoop < months.length;intLoop++)
document.write("<OPTION VALUE= " + (intLoop + 1) + " " +(today.month == intLoop ?"Selected" : "") + ">" +months[intLoop]);
						</script>
              </select>
            </td>
          </tr>
          <tr height="5px" bgcolor="#ffffff" border="0">
			<td style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none">
			</td>
			<td style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none">
			</td>
			<td style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none">
			</td>
			<td style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none">
			</td>
			<td style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none">
			</td>
			<td style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none">
			</td>
			<td style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none">
			</td>	
          </tr>
          </tbody>
          <tr class="days" align="CENTER" cellspacing="1" cellpadding="1"> 
            <script language="JavaScript">
document.write("<TD class=satday>" + days[0] + "</TD>");
for (var intLoop = 1; intLoop < days.length-1;
intLoop++) 
document.write("<TD >" + days[intLoop] + "</TD>");
document.write("<TD class=sunday>" + days[intLoop] + "</TD>");
				</script>
          </tr>
          
          <tbody width="100%" border="0" cellspacing="1" cellpadding="0" height="100" id="dayList" align="CENTER"  onMouseOver="setDateFocus()" onClick="getDate()"> 
          <script language="JavaScript">
for (var intWeeks = 0; intWeeks < 6; intWeeks++) {
document.write("<TR bgcolor='#F3F3F3' align='center' style='cursor:hand'>");
for (var intDays = 0; intDays < days.length;intDays++)
document.write("<TD width='32'height='15' onMouseOver='mouse_over(this)' onMouseOut='mouse_out(this)'></TD>");
document.write("</TR>");
}
				</script>
          </tbody> 
        </table>
      </div>
    </td>
    <td width="21" valign="top" background="img/nlrd.gif"><img src="img/rl_r2_c3.gif" width="21" height="20"></td>
  </tr>
  <tr>
    <td width="43" background="img/nlld.gif" valign="bottom"><img src="img/nll2.gif" width="43" height="29"></td>
    <td width="21" background="img/nlrd.gif" valign="bottom"><img src="img/nlr2.gif" width="21" height="15"></td>
  </tr>
</table>
<img src="img/nlta1.gif" height="18"> 
</BODY>
</HTML>
<SCRIPT LANGUAGE="JavaScript">
<!--
//定义窗体大小
//window.dialogHeight="180px";
//window.dialogWidth="270px";
//-->
</SCRIPT>
