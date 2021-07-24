/********************************************************/
/*作者：jw                    
/*创建日期：2005年11月10日        
/*说明：此文件应以UTF8格式保存
/********************************************************/

var daysInMonth = new Array(31, 28, 31, 30, 31, 30, 31, 31,30, 31, 30, 31);
var week = new Array("日","一","二","三","四","五","六");
var ControlName = "";
var CalenderBodyColor = "#cccccc";				//日历体的颜色
var OnMouseOverColor = "#808080";				//鼠标放到日期上的颜色
var CurrentDateColor = "#ffffff";				//当前日期的颜色

//对闰年的处理
function getDays(month, year) 
{
	if (1 == month)
		return ((0 == year % 4) && (0 != (year % 100))) || (0 == year % 400) ? 29 : 28;
	else
		return daysInMonth[month];
}

//获取指定年份、月份的第一天的星期数
function StartDay()
{
	var selectYear = document.getElementById(ControlName+"_year");
	var selectMonth = document.getElementById(ControlName+"_month");
	var year = parseInt(selectYear.options[selectYear.selectedIndex].value);
	var month = selectMonth.selectedIndex;

	var ChooseStartDate = new Date(year,month,1);
	
	return  ChooseStartDate.getDay();
}

//填充日历
function FillCalendar(UniqueId)
{
	ControlName = UniqueId;
	var startDate = StartDay();
	var tempD = 1;
	var number = 1;
	
	var maxDay = getDays(document.getElementById(UniqueId+"_month").selectedIndex, parseInt(document.getElementById(UniqueId+"_year").options[document.getElementById(UniqueId+"_year").selectedIndex].value));
	for( var i = 1; i <=42; i++ )
	{
		var nameId = UniqueId+"day"+i;
		document.getElementById(nameId).innerText = " ";
	}
	for( var i = 1; i <= 42; i++ )
	{
		var nameId = UniqueId+"day"+i;
		today = new Date();
		
		document.getElementById(nameId).style.background = CalenderBodyColor;
		//如果要写日期大于指定的日期的第一天的星期数并且小于本月最大天数，才进行日历天数填充
		if( i > startDate && tempD <= maxDay )
		{
			var obj = document.getElementById(nameId);
			obj.innerText = tempD;
			if( today.getDate() + startDate == i && today.getFullYear() == document.getElementById(UniqueId+"_year").options[document.getElementById(UniqueId+"_year").selectedIndex].value && today.getMonth() +1 == document.getElementById(UniqueId+"_month").options[document.getElementById(UniqueId+"_month").selectedIndex].value)
				obj.style.background = CurrentDateColor;
			obj.style.cursor = "hand";
			tempD++;
		}
	}
}


//创建日历框架
function CreateCalenderTable()
{
	obj = document.getElementById(ControlName+"CalenderHead");
	
	NewRow = obj.insertRow();
	for( var i = 1; i < 8; i++ )
	{
		NewCell = NewRow.insertCell();
		NewCell.innerHTML = week[i-1];
		NewCell.align="center";
		NewCell.style.background = CalenderBodyColor;
	}
	var number = 1;
	
	for( var i = 1; i < 7; i++ )
	{
		NewRow = obj.insertRow();
		for( var j = 1; j <= 7; j++ )
		{
			NewCell = NewRow.insertCell();
			NewCell.align="center";
			
			NewCell.attachEvent("onclick",OnMouseDownDay);
			NewCell.attachEvent("onmouseover",OnMouseOverDay);
			NewCell.attachEvent("onmouseout",OnMouseOutDay);
			NewCell.id = ControlName+"day"+number;
			number++;
		}
	}		
}

//创建年份列表
function CreateMonthList()
{
	obj = document.getElementById(ControlName+"_month");
	var currentMonth = new Date().getMonth();
	for( var i = 1; i <=12; i++ )
	{
		var opt = document.createElement("OPTION");
		opt.text = i;
		opt.value = i;
		obj.options.add(opt);
	}
	obj.options[currentMonth].selected = true;		
}


//创建月份列表
function CreateYearList()
{
	obj = document.getElementById(ControlName+"_year");
	var day = new Date();
	
	for( var i = day.getFullYear() - 50; i < day.getFullYear() + 10; i++ )
	{
		var opt = document.createElement("OPTION");
		opt.text = i;
		opt.value = i;
		if( i-1 == day.getFullYear() )
			opt.selected = true;
		obj.options.add(opt);
	}
}

//鼠标划过日历天数的时候发生的事件
function OnMouseOverDay()
{
	obj = window.event.srcElement;
	//日历框的颜色
	obj.style.background = OnMouseOverColor;
}

//鼠标离开日历天数的时候发生的事件
function OnMouseOutDay()
{
	obj = window.event.srcElement;
	//日历框的颜色
	obj.style.background = CalenderBodyColor;
}


//选择日期后发生的事件，
function OnMouseDownDay()
{
	obj = window.event.srcElement;
	
	//点击的日历框无日期的处理
	if( (obj.innerText) == " " )
		return;
		
	var ChooseTime = document.getElementById(ControlName+"_year").options[document.getElementById(ControlName+"_year").selectedIndex].value + "-"+document.getElementById(ControlName+"_month").options[document.getElementById(ControlName+"_month").selectedIndex].value+"-"+obj.innerText;
	HiddenCalender();
	document.getElementById(ControlName+"_tb_InputDate").value =  ChooseTime;
}

//创建日历
function CreateCalender(UniqueId)
{	
	document.getElementById(UniqueId+"Time").style.display='';
	ControlName = UniqueId;
	
	
	if( document.getElementById(ControlName+"_year").options.length == 0 )
	{
		CreateYearList();
		CreateMonthList();
		CreateCalenderTable();
		
	}
	
	objDiv = document.getElementById(ControlName+"Time");
	objIframe = document.getElementById(ControlName+"iframDiv");
	DivSetVisible(objDiv,objIframe);
	
	FillCalendar(UniqueId);
}


//隐藏日历
function HiddenCalender()
{
	document.getElementById(ControlName+"Time").style.display='none';
	document.getElementById(ControlName+"iframDiv").style.display='none';
}


//对IFRAME的处理
function DivSetVisible(objDiv,objIframe)
{
	objIframe.style.width = objDiv.offsetWidth;
	objIframe.style.height = objDiv.offsetHeight;
	objIframe.style.top = objDiv.style.top;
	objIframe.style.left = objDiv.style.left;
	objIframe.style.zIndex = objDiv.style.zIndex - 1;
	objIframe.style.scrolling = "no";
	objIframe.style.frameborder = "0";
	objIframe.style.display = "block";
}
		
		