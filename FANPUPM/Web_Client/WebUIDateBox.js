//IE9下错误  2012-03-02
try {
		//================================================代码版本信息==================================================
		var dateBoxScript_Ver = "001";
		//==================================================== 参数设定部分 =======================================================
		var _VersionInfo="test";	//版本信息
		document.writeln('<IFRAME bgcolor=\"#000000\" id=\"ifraCalendarLayer\" frameborder=\"0\" width=\"200\" height=\"160\" scrolling="no" style=\"position:absolute;z-index:9998;display:none\"></IFRAME>');
		
		var MonHead = new Array(12);    		   //定义阳历中每个月的最大天数
			MonHead[0] = 31;
			MonHead[1] = 28;
			MonHead[2] = 31;
			MonHead[3] = 30;
			MonHead[4] = 31;
			MonHead[5] = 30;
			MonHead[6] = 31;
			MonHead[7] = 31;
			MonHead[8] = 30;
			MonHead[9] = 31;
			MonHead[10] = 30;
			MonHead[11] = 31;
			
		var date = new Date();
		var theYear=date.getFullYear(); //定义年的变量的初始值
		var theMonth=date.getMonth()+1; //定义月的变量的初始值
		var calendarAllDay=new Array(39); //定义写日期的数组
		
		var outObject;
		var outDate="";		//存放对象的日期
		var odatelayer;		//存放日历对象
		
		function showCalendar(obj)
		{
			var strCalendarStyle = obj.getAttribute("calendarstyle");
			var strButtonStyle = obj.getAttribute("buttonstyle");
			var strWeekStyle = obj.getAttribute("weekstyle");
			var strActiveCellStyle = obj.getAttribute("activecellstyle");
			var strInActiveCellStyle = obj.getAttribute("inactivecellstyle");
			var strTodayStyle = obj.getAttribute("todaystyle");
			var strTodayCellStyle = obj.getAttribute("todaycellstyle");
			var strSelectedCellStyle = obj.getAttribute("selectedcellstyle");
			var strMouseOverCellStyle = obj.getAttribute("mouseovercellstyle");
			var strFrame;		//存放日历层的HTML代码
			strFrame='<STYLE>';
			strFrame+='.CalendarStyle{' + strCalendarStyle + '}';
			strFrame+='INPUT.BUTTON{' + strButtonStyle + '}';
			strFrame+='TD{FONT-SIZE: 9pt;font-family:宋体;}';
			strFrame+='.WeekStyle{' + strWeekStyle + '}';
			strFrame+='.ActiveCellStyle{' + strActiveCellStyle + '}';
			strFrame+='.InActiveCellStyle{' + strInActiveCellStyle + '}';
			strFrame+='.SelectedCellStyle{' + strSelectedCellStyle + '}';
			strFrame+='.TodayCellStyle{' + strTodayCellStyle + '}';
			strFrame+='.MouseOverStyle{' + strMouseOverCellStyle + '}';
			strFrame+='.TodayStyle{' + strTodayStyle + '}';
			strFrame+='</STYLE>';
			strFrame+='<div style=\"z-index:9999;position:absolute;left:0;top:0;\" onselectstart=\"return false;\">';
			strFrame+='<span id=\"tmpSelectYearLayer\" style=\"z-index:9999;position:absolute;top:3;left:26;display: none\"></span>';
			strFrame+='<span id=\"tmpSelectMonthLayer\" style=\"z-index:9999;position:absolute;top:3;left:70;display:none\"></span>';
			strFrame+='<TABLE border=\"0\" cellspacing=\"1\" cellpadding=\"0\" width=\"200\" height=\"155\" class=\"CalendarStyle\">';
			strFrame+='<TR><td height=\"22\" colspan=\"7\" valign="middle">';
			strFrame+='<TABLE border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" height=\"20\" id=\"tbCalendarHead\">';
			strFrame+='<TR><TD width=\"20\" align=\"center\"><INPUT type=\"button\" class=\"button\" style=\"cursor:hand;font-size: 12px;height: 20px\" value=\"<<\" title=\"向前翻 1 年\" onclick=\"parent.setPrevYear()\" onfocus=\"this.blur()\" id=\"btCalendarPreYear\" name=\"btCalenderPreYear\">';
			strFrame+='</TD><TD width=\"16\" align=\"center\"><INPUT class=\"button\" title=\"向前翻 1 月\" type=\"button\" value=\"<\" style=\"cursor:hand;font-size: 12px;height: 20px\" onclick=\"parent.setPrevMonth()\" onfocus=\"this.blur()\" id=\"btCalendarPreMonth\" name=\"btCalendarPreMonth\">';
			strFrame+='</TD><TD width=\"58\" align=\"center\" style=\"font-size:12px;cursor:default\" onmouseover=\"style.backgroundColor=\'#618BC5\'\" onmouseout=\"style.backgroundColor=\'#FFFFFF\'\" onclick=\"parent.tmpSelectYearInnerHTML(this.innerText.substring(0,4))\" title=\"点击这里选择年份\">';
			strFrame+='<SPAN id=\"spCalendarYearHead\"></SPAN>';
			strFrame+='</TD><TD width=\"48\" align=\"center\" style=\"font-size:12px;cursor:default\" onmouseover=\"style.backgroundColor=\'#618BC5\'\" onmouseout=\"style.backgroundColor=\'#FFFFFF\'\" onclick=\"parent.tmpSelectMonthInnerHTML(this.innerText.length==3?this.innerText.substring(0,1):this.innerText.substring(0,2))\" title=\"点击这里选择月份\">';
			strFrame+='<SPAN id=\"spCalendarMonthHead\"></SPAN>';
			strFrame+='</TD><TD width=\"16\" align=\"center\">';
			strFrame+='<INPUT type=\"button\" class=\"button\" value=\">\" style=\"cursor:hand;font-size: 12px;height: 20px\" onclick=\"parent.setNextMonth()\" onfocus=\"this.blur()\" title=\"向后翻 1 月\" class=\"button\" id=\"btCalendarNextMonth\" name=\"btCalendarNextMonth\">';
			strFrame+='</TD><TD width=\"20\" align=\"center\"><INPUT type=\"button\" class=\"button\" style=\"cursor:hand;font-size: 12px;height: 20px\" value=\">>\" title=\"向后翻 1 年\" onclick=\"parent.setNextYear()\" onfocus=\"this.blur()\" id=\"btCalendarNextYear\" name=\"btCalendarNextYear\">';
			strFrame+='</TD><TD width=\"22\" align=\"center\"><INPUT type=\"button\" class=\"button\" style=\"cursor: hand;font-size: 12px;height: 20px;width: 20px;\" value=\"X\" title=\"关闭\" onclick=\"parent.hideCalendar()\"></TD></TR></TABLE>';
			strFrame+='</td></tr>';
			strFrame+='<tr valign=\"middle\" align=\"center\" class=\"WeekStyle\"><td width=\"28\" height=\"18\"><b>日</b></td>';
			strFrame+='<td width=\"28\"><b>一</b></td><td width=\"28\"><b>二</b></td>';
			strFrame+='<td width=\"28\"><b>三</b></td><td width=\"28\"><b>四</b></td>';
			strFrame+='<td width=\"28\"><b>五</b></td><td width=\"30\"><b>六</b></td></tr>';
			strFrame+='</tr>';
			var n=0; for (j=0;j<5;j++){ strFrame+= ' <tr align=\"center\">'; for (i=0;i<7;i++){
			strFrame+='<td height=\"18\" id=\"calendarDay'+n+'\" style="font-size:12px" onclick=\"parent.clickCalendarDay(this.innerText,0);\"></td>';n++;}
			strFrame+='</tr>';}
			strFrame+='<tr align=\"center\">';
			for (i=35;i<39;i++)strFrame+='<td class=\"CalendarCell\" height=\"18\" id=\"calendarDay'+i+'\" style="font-size:12px" onclick="parent.clickCalendarDay(this.innerText,0)"></td>';
			strFrame+='<td colspan=\"3\" align=\"center\" class=\"TodayStyle\" onclick=\"parent.clickToday();\"';
			var date = new Date();
			var thisYear = date.getFullYear();
			var thisMonth = date.getMonth()+1;
			var thisDay = date.getDate();
			var today =  thisYear + '年' + thisMonth + '月' + thisDay + '日';
			strFrame+=' title="' + today + '">' + today + '</td></tr>';
			strFrame+='</table></td></div>';

			window.frames.ifraCalendarLayer.document.writeln(strFrame);
			window.frames.ifraCalendarLayer.document.close();
		}
		
		 //主调函数
		function getCalendar(senderObj)
		{
			if (arguments.length != 1)
			{
				alert("对不起！传入本控件的参数不正确！");
				return;
			}
			
			showCalendar(senderObj);
			odatelayer =window.frames.ifraCalendarLayer.document.all;
			var dads  = document.all.ifraCalendarLayer.style;
			var th = senderObj;
			var ttop  = senderObj.offsetTop;     //senderObj控件的定位点高
			var thei  = senderObj.clientHeight;  //senderObj控件本身的高
			var tleft = senderObj.offsetLeft;    //senderObj控件的定位点宽
			var ttyp  = senderObj.type;          //senderObj控件的类型
			while (senderObj = senderObj.offsetParent)
			{
				ttop+=senderObj.offsetTop;
				//alert(ttop);
				tleft+=senderObj.offsetLeft;
			}
///到此，获取了控件的绝对位置		

			if((tleft + 200)>document.body.clientWidth)
			{
				tleft = document.body.clientWidth-200;
			}
			if((ttop + 180) > document.body.clientHeight)
			{
				ttop = document.body.clientHeight -180;
			}
			dads.top  = (ttyp=="image")? ttop+thei : ttop+thei+4;
			dads.left = tleft;
			outObject = th;
			//根据当前输入框的日期显示日历的年月
			var reg = /^(\d+)-(\d{1,2})-(\d{1,2})$/; 
			var r = outObject.value.match(reg);
			if(r!=null)
			{
				r[2]=r[2]-1; 
				var d= new Date(r[1], r[2],r[3]); 
				if(d.getFullYear()==r[1] && d.getMonth()==r[2] && d.getDate()==r[3])
				{
					outDate=d;		//保存外部传入的日期
				}
				else outDate="";
					resetCalendarDay(r[1],r[2]+1);
			}
			else
			{
				outDate="";
				resetCalendarDay(new Date().getFullYear(), new Date().getMonth() + 1);
			}
			dads.display = '';
			event.returnValue=false;
		}
		
		 //任意点击时关闭该控件	//ie6的情况可以由下面的切换焦点处理代替
		//按Esc键关闭，切换焦点关闭
//		//往 head 中写入当前的年与月
		function setCalendarHeader(yy,mm)
		{
			odatelayer.spCalendarYearHead.innerText  = yy + " 年";
			odatelayer.spCalendarMonthHead.innerText = mm + " 月";
		}
		 //年份的下拉框
		function tmpSelectYearInnerHTML(strYear)
		{
			if (strYear.match(/\D/)!=null)
			{
				alert("年份输入参数不是数字！");
				return;
			}
			var m = (strYear) ? strYear : new Date().getFullYear();
			if (m < 1000 || m > 9999)
			{
				alert("年份值不在 1000 到 9999 之间！");
				return;
			}
			var n = m - 20;
			if (n < 1000) n = 1000;
			if (n + 26 > 9999) n = 9974;
			var s = "&nbsp;&nbsp;&nbsp;<select name=tmpSelectYear style='font-size: 12px' "
				s += "onblur='document.all.tmpSelectYearLayer.style.display=\"none\"' "
				s += "onchange='document.all.tmpSelectYearLayer.style.display=\"none\";"
				s += "parent.theYear = this.value; parent.resetCalendarDay(parent.theYear,parent.theMonth)'>\r\n";
			var selectInnerHTML = s;
			for (var i = n; i < n + 125; i++)
			{
				if (i == m)
				{selectInnerHTML += "<option value='" + i + "' selected>" + i + "年" + "</option>\r\n";}
				else {selectInnerHTML += "<option value='" + i + "'>" + i + "年" + "</option>\r\n";}
			}
			selectInnerHTML += "</select>";
			odatelayer.tmpSelectYearLayer.style.display="";
			odatelayer.tmpSelectYearLayer.innerHTML = selectInnerHTML;
			odatelayer.tmpSelectYear.focus();
		}
		 //月份的下拉框
		function tmpSelectMonthInnerHTML(strMonth)
		{
			if (strMonth.match(/\D/)!=null)
			{
				alert("月份输入参数不是数字！");
				return;
			}
			var m = (strMonth) ? strMonth : new Date().getMonth() + 1;
			var s = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<select name=tmpSelectMonth style='font-size: 12px' "
				s += "onblur='document.all.tmpSelectMonthLayer.style.display=\"none\"' "
				s += "onchange='document.all.tmpSelectMonthLayer.style.display=\"none\";"
				s += "parent.theMonth = this.value; parent.resetCalendarDay(parent.theYear,parent.theMonth)'>\r\n";
			var selectInnerHTML = s;
			for (var i = 1; i < 13; i++)
			{
				if (i == m)
				{selectInnerHTML += "<option value='"+i+"' selected>"+i+"月"+"</option>\r\n";}
				else {selectInnerHTML += "<option value='"+i+"'>"+i+"月"+"</option>\r\n";}
			}
			selectInnerHTML += "</select>";
			odatelayer.tmpSelectMonthLayer.style.display="";
			odatelayer.tmpSelectMonthLayer.innerHTML = selectInnerHTML;
			odatelayer.tmpSelectMonth.focus();
		}
		//这个层的关闭
		function hideCalendar()
		{
			document.all.ifraCalendarLayer.style.display="none";
		}
		//判断是否闰平年
		function isPinYear(year)
		{
			if (0==year%4&&((year%100!=0)||(year%400==0)))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		//闰年二月为29天
		function getDayOfMonth(year,month)
		{
			var c=MonHead[month-1];
			if((month==2)&&isPinYear(year))
			{
				c++;
			}
			return c;
		}
		//求某天的星期几
		function getDayOfWeek(day,month,year)
		{
			var dt=new Date(year,month-1,day).getDay()/7;
			return dt;
		}
		//往前翻 Year
		function setPrevYear()
		{
			if(theYear > 999 && theYear <10000){theYear--;}
			else{alert("年份超出范围（1000-9999）！");}
			resetCalendarDay(theYear,theMonth);
		}
		//往后翻 Year
		function setNextYear()
		{
			if(theYear > 999 && theYear <10000){theYear++;}
			else{alert("年份超出范围（1000-9999）！");}
			resetCalendarDay(theYear,theMonth);
		}
		function clickToday()
		{
			var today;
			var tempString;
			theYear = new Date().getFullYear();
			theMonth = new Date().getMonth()+1;
			theday=new Date().getDate();
			//resetCalendarDay(theYear,calendarTheMonth);
			if(outObject){
				//outObject.value=theYear + "-" + ((theMonth.toString().length>1)?theMonth:("0" + theMonth)) + "-" + ((theday.toString().length>1)?theday:"0" + theday);
				outObject.value=theYear + "-" + theMonth + "-" + theday.toString();
			}
			outObject.setActive();
			setTimeout(hideCalendar,50);
			outObject.blur();
			//hideCalendar();
		}
		//往前翻月份
		function setPrevMonth()
		{
			if(theMonth>1){theMonth--}else{theYear--;theMonth=12;}
			resetCalendarDay(theYear,theMonth);
		}
		//往后翻月份
		function setNextMonth()
		{
			if(theMonth==12){theYear++;theMonth=1}else{theMonth++}
			resetCalendarDay(theYear,theMonth);
		}
		//主要的写程序**********
		function resetCalendarDay(yy,mm)
		{
			setCalendarHeader(yy,mm);
			//设置当前年月的公共变量为传入值
			theYear=yy;
			theMonth=mm;
			  
			for (var i = 0; i < 39; i++)
			{
				calendarAllDay[i]="";
			}  //将显示框的内容全部清空
			var day1 = 1,day2=1,firstday = new Date(yy,mm-1,1).getDay();  //某月第一天的星期几
			for (i=0;i<firstday;i++)calendarAllDay[i]=getDayOfMonth(mm==1?yy-1:yy,mm==1?12:mm-1)-firstday+i+1	//上个月的最后几天
			for (i = firstday; day1 < getDayOfMonth(yy,mm)+1; i++)
			{
				calendarAllDay[i]=day1;day1++;
			}
			for (i=firstday+getDayOfMonth(yy,mm);i<39;i++)
			{
				calendarAllDay[i]=day2;day2++
			}
			for (i = 0; i < 39; i++)
			{ var da = eval("odatelayer.calendarDay"+i)     //书写新的一个月的日期星期排列
				if (calendarAllDay[i]!="")
				{ 
					da.Class = "ActiveCellStyle";
					if(i<firstday)		//上个月的部分
					{
						da.innerHTML=calendarAllDay[i];//"<font style=' color: #B5C5D2;'>" + calendarAllDay[i] + "</font>";
						da.title=(mm==1?12:mm-1) +"月" + calendarAllDay[i] + "日";
						da.onclick=Function("clickCalendarDay(this.innerText,-1)");
						
						if(!outDate)
							da.className = ((mm==1?yy-1:yy) == new Date().getFullYear() && 
								(mm==1?12:mm-1) == new Date().getMonth()+1 && calendarAllDay[i] == new Date().getDate()) ?
								"ActiveCellStyle":"InActiveCellStyle";
						else
						{
							da.className =((mm==1?yy-1:yy)==outDate.getFullYear() && (mm==1?12:mm-1)== outDate.getMonth() + 1 && 
							calendarAllDay[i]==outDate.getDate())? "SelectedCellStyle" : // 选中日期颜色
							(((mm==1?yy-1:yy) == new Date().getFullYear() && (mm==1?12:mm-1) == new Date().getMonth()+1 && 
							calendarAllDay[i] == new Date().getDate()) ? "ActiveCellStyle":"InActiveCellStyle"); // 当前系统时间颜色
						}
						da.onmouseover=Function("this.className='MouseOverStyle';");
						da.onmouseout=Function("this.className='InActiveCellStyle';");
					}
					else if (i>=firstday+getDayOfMonth(yy,mm))		//下个月的部分
					{
						da.innerHTML= calendarAllDay[i];
						da.title=(mm==12?1:mm+1) +"月" + calendarAllDay[i] + "日";
						da.onclick=Function("clickCalendarDay(this.innerText,1)");
						if(!outDate)
							da.className = ((mm==12?yy+1:yy) == new Date().getFullYear() && 
								(mm==12?1:mm+1) == new Date().getMonth()+1 && calendarAllDay[i] == new Date().getDate()) ?
								"ActiveCellStyle":"InActiveCellStyle";
						else
						{
							da.className =((mm==12?yy+1:yy)==outDate.getFullYear() && (mm==12?1:mm+1)== outDate.getMonth() + 1 && 
							calendarAllDay[i]==outDate.getDate())? "SelectedCellStyle" : // 选中日期颜色
							(((mm==12?yy+1:yy) == new Date().getFullYear() && (mm==12?1:mm+1) == new Date().getMonth()+1 && 
							calendarAllDay[i] == new Date().getDate()) ? "ActiveCellStyle":"InActiveCellStyle"); // 当前系统时间
							//将选中的日期显示为凹下去
							//if((mm==12?yy+1:yy)==outDate.getFullYear() && (mm==12?1:mm+1)== outDate.getMonth() + 1 && 
							//calendarAllDay[i]==outDate.getDate())
							//{
							////	da.borderColorLight="#E4E3F2";
							//	da.borderColorDark="#E4E3F2";  // 	选择日期边框颜色
							//}
						}
						da.onmouseover=Function("this.className='MouseOverStyle';");
						da.onmouseout=Function("this.className='InActiveCellStyle';");
					}
					else		//本月的部分
					{
						da.innerHTML= calendarAllDay[i];
						da.title=mm +"月" + calendarAllDay[i] + "日";
						da.onclick=Function("clickCalendarDay(this.innerText,0)");		//给td赋予onclick事件的处理
						//如果是当前选择的日期，则显示亮蓝色的背景；如果是当前日期，则显示暗黄色背景
						if(!outDate)
						{
							da.className = (yy == new Date().getFullYear() && mm == new Date().getMonth()+1 && calendarAllDay[i] == new Date().getDate())?"TodayCellStyle":"ActiveCellStyle";
						}
						else
						{
							da.className = (yy == outDate.getFullYear() && mm== outDate.getMonth() + 1 && calendarAllDay[i]==outDate.getDate())?"SelectedCellStyle":((yy == new Date().getFullYear() && mm == new Date().getMonth()+1 && calendarAllDay[i] == new Date().getDate())?"TodayCellStyle":"ActiveCellStyle"); // 前一个当前系统时间,后一个是本月时间低色
						}
						da.onmouseover=Function("this.className='MouseOverStyle';");
						da.onmouseout=Function("this.className='ActiveCellStyle';");
					}
				}
				else
				{
					da.innerHTML="";
					da.className = "InActiveCellStyle";
					da.style.cursor="default";
				}
			}
		}
		//点击显示框选取日期，主输入函数*************
		function clickCalendarDay(n,ex)
		{
			var yy=theYear;
			var mm = parseInt(theMonth)+ex;	//ex表示偏移量，用于选择上个月份和下个月份的日期
			//判断月份，并进行对应的处理
			if(mm<1)
			{
				yy--;
				mm=12+mm;
			}
			else if(mm>12)
			{
				yy++;
				mm=mm-12;
			}
				
			//if (mm < 10)
			//{
			//	mm = "0" + mm;
			//}
			if (outObject)
			{	//outObject.value=""; 
				if (!n)
				{
					return;
				}
				//if ( n < 10)
				//{
				//	n = "0" + n;
				//}
				outObject.value= yy + "-" + mm + "-" + n ;
				//注：在这里你可以输出改成你想要的格式
				outObject.setActive();
				setTimeout(hideCalendar,50);
				outObject.blur();
				//hideCalendar(); 
			}
			else
			{
				hideCalendar();
				alert("您所要输出的控件对象并不存在！");
			}
		}
} catch(e) {
    
}