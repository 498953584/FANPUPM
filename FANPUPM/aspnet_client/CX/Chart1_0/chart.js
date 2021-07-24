<?xml version="1.0" encoding="utf-8"?>
<!--
功能：绘制饼图和柱状图
版权：中色长信
作者：王鹏
日期：2004-10-06
-->
<!DOCTYPE svg PUBLIC "-//W3C//DTD SVG 20001102//EN" "http://www.w3.org/TR/2000/CR-SVG-20001102/DTD/svg-20001102.dtd">
<svg onload="Initialize(evt)" width="570" height="270" viewBox="0 0 570 270" xml:space="preserve">
<title>饼图和柱状图</title> 
<desc>饼图和柱状图</desc>
  <defs>
	    <filter id="DropShadowFilter" filterUnits="objectBoundingBox" x="-10%" y="-10%" width="120%" height="120%">
		      <feGaussianBlur in="SourceAlpha" stdDeviation="2" result="BlurAlpha"/>
		      <feOffset in="BlurAlpha" dx="4" dy="4" result="OffsetBlurAlpha"/>
		      <feMerge>
			        <feMergeNode in="OffsetBlurAlpha"/>
			        <feMergeNode in="SourceGraphic"/>
		      </feMerge>
	    </filter>
  </defs>
  <script language="javascript">
    <![CDATA[
    //------------------------------------------------------------------------------------------------------------------------
	  //预定义所使用的色彩
      var ColorArray = new Array("#FF0000",
							 "#00FF00",
							 "#0000FF",
							 "#FF9966", 
                             "#C2A00E", 
                             "#71A214", 
                             "#3399CC", 
                             "#CC99FF", 
                             "#FF9999", 
                             "#00CC99", 
                             "#C2CC0E", 
                             "#71CC14", 
                             "#CC68CC", 
                             "#66FFCC");
                             
      var ActiveSegment		= "Pie";  
      var CurrentColor		= 0;			//记录当前色彩数组值
      var SVGDocument		= null;
      
      var BarChartHeight	= 165;			//柱状图最高高度
      var BarChartWidth		= 380;			//柱状图总宽度
      var PieChartSize		= 180;			//饼图大小定义
      var MoveDistance		= 30;			//饼图鼠标单击后分开的距离
      
      var YSize				= 0;			//记录图示间隔距离
      var bChart			= false;		//记录是否初始化图示
      var GroupName			= "";			//分组名称
      var GroupStart		= 0;			//记录分组文字显示的位置
    
      var Values			= new Array();
      var Names				= new Array();
      var PieElements		= new Array();
      var BarElements		= new Array();
      var BarTexts			= new Array();
      var DeleteList		= new Array();
      
      var PieTotalSize		= 0;			//记录最大数值
      var BarTotalSize		= 0;			//柱子当前的数量
      var GroupBarSize		= 0;			//记录每组的柱子数量
      var tGroupBarSize		= 0;			//临时记录每组柱子数量
      var MaxSize			= 0;			//记录最大数值
      
      var AngleFactor		= Math.pow(2, .5);
      var Removed			= false;
      
      var PieStart;
      var BarStart;
	  
	  //------------------------------------------------------------------------------------------------------------------------
	  //初始化图形框架
      function Initialize(LoadEvent)
        {
          SVGDocument  = LoadEvent.getTarget().getOwnerDocument();
          ParentGroup1 = SVGDocument.getElementById("slices");
          Grandparent1 = SVGDocument.getElementById("piechart");
          
          ParentGroup2 = SVGDocument.getElementById("bars");
          Grandparent2 = SVGDocument.getElementById("barchart");
          
          Grandparent3 = SVGDocument.getElementById("ChartExplain");
        }
      
      //------------------------------------------------------------------------------------------------------------------------
      //增加图形元素

      function AddChartValue(Value, Name, Repress, Group)
        {
          Value = Value * 1;
          if (isNaN(Value))
            {
              alert("Y轴数据存在空值或者值为0,数据存在问题！");
              return;
            }          
          
          //如果是分割线则将颜色设定为白色,
          if(Value == -1)
          {
			
			Color = "#ffffff";
			bChart = true;
          }
          else
          {
			Color = ColorArray[CurrentColor];
          }
          
          CurrentColor++;
          if (CurrentColor >= ColorArray.length || Value == -1)
          {
			CurrentColor = 0;
          }
          
          
          if (!(Removed))
            {
              Grandparent1.removeChild(ParentGroup1);
              Grandparent2.removeChild(ParentGroup2);
            }
          
          DeleteList[DeleteList.length] = false;
          
          Values[Values.length] = Value * 1;
          Names[Names.length]	= SVGDocument.createElement("text");
          
          //处理饼图
          PieElements[PieElements.length] = SVGDocument.createElement("path");
          PieElements[PieElements.length - 1].setAttribute("style", "stroke:black;fill:" + Color);
          PieElements[PieElements.length - 1].setAttribute("onmouseover", "DisplayInfo('" + Name + "', '" + Value + "')");
          PieElements[PieElements.length - 1].setAttribute("onmouseout", "DisplayInfo(' ', ' ')");
          ParentGroup1.appendChild(PieElements[PieElements.length - 1]);
          
          //处理柱状图
          BarElements[BarElements.length] = SVGDocument.createElement("path");
          BarTexts[BarTexts.length] = SVGDocument.createElement("text");
          BarElements[BarElements.length - 1].setAttribute("style", "stroke:black;fill:" + Color);
          if(Value != -1)
          {
			BarElements[BarElements.length - 1].setAttribute("onmouseover", "BarTexts[" + (BarTexts.length - 1) + "].getStyle().setProperty('visibility', 'show')")
			BarElements[BarElements.length - 1].setAttribute("onmouseout",  "BarTexts[" + (BarTexts.length - 1) + "].getStyle().setProperty('visibility', 'hidden')")
          }
          ParentGroup2.appendChild(BarElements[BarElements.length - 1]);
          
          //处理柱状鼠标悬浮显示的数值
          BarTexts[BarTexts.length - 1].setAttribute("style", "text-anchor:end;font-weight:bold;font-size:13;visibility:hidden");
          BarTexts[BarTexts.length - 1].setAttribute("x", "188");
          BarTexts[BarTexts.length - 1].appendChild(SVGDocument.createTextNode(Value + " - "));
          SVGDocument.getElementById("barsidetext").appendChild(BarTexts[BarTexts.length - 1]);
          
          //处理柱状图下面的文字
          
          tGroupBarSize ++;
          
          if(Value == -1 && GroupName != "")
          {
			    Names[Names.length - 1].appendChild(SVGDocument.createTextNode(GroupName + ""));
				SVGDocument.getElementById("labels").appendChild(Names[Names.length - 1]);
				GroupBarSize = tGroupBarSize;
				
				tGroupBarSize = 0;
          }
          GroupName = Group;
          
          //开始构建图示,白色#ffffff表示间隔区域不需要显示
		  if(Color != "#ffffff" && !bChart)
		  {
				//增加图形

				var vChart = SVGDocument.createElement("rect");
				vChart.setAttribute("x", "0");
				vChart.setAttribute("y", YSize);
				vChart.setAttribute("width", "15");
				vChart.setAttribute("height", "15");
				vChart.setAttribute("fill", Color);
				
				Grandparent3.appendChild(vChart);
				
				//增加文本

				var vText = SVGDocument.createElement("text");
				vText.setAttribute("x", "20");
				vText.setAttribute("y", YSize + 12);
				vText.appendChild(SVGDocument.createTextNode(Name));
				Grandparent3.appendChild(vText);
				
				YSize += 20;
          }
          
          Refresh()
          
          if (Repress)
            Removed = true;
          else
            {
              Removed = false;
              Grandparent1.appendChild(ParentGroup1);
              Grandparent2.appendChild(ParentGroup2);

              ComputeNumber();	//计算Y轴刻度
            }
        }//end of AddChartValue()
      
      //------------------------------------------------------------------------------------------------------------------------
      //刷新图形
      function Refresh()
        {
          PieTotalSize = 0;
          BarTotalSize = 0;
          MaxSize = 0;
          
          for (var I = 0; I < Values.length; I++)
            {
              PieTotalSize += Values[I];
              BarTotalSize++;
              if (Values[I] > MaxSize)
                MaxSize = Values[I];
            }
          
          PieStart = 0
          BarStart = 0
          if (PieTotalSize > 0)
            for (var I = 0; I < Values.length; I++)
              {
                PieStart = DrawPieSegment(PieStart, Values[I] / PieTotalSize, PieElements[I], I)
                
                BarStart = DrawBarSegment(BarStart, Values[I] / MaxSize, BarElements[I], BarTexts[I], Names[I], Values[I])
              }
          
          SVGDocument.getElementById("max").replaceChild(SVGDocument.createTextNode(Math.round(MaxSize) + ""), SVGDocument.getElementById("max").getFirstChild())
          SVGDocument.getElementById("min").replaceChild(SVGDocument.createTextNode("0"), SVGDocument.getElementById("min").getFirstChild())
        }//end of Refresh()
   
   //------------------------------------------------------------------------------------------------------------------------
      //构建柱状图
      function DrawBarSegment(Start, Height, Element, Text, Label, tHeight)
      {
          XOffset3D = 6;
          YOffset3D = 5;
        
          PathData = "M" + (Start + (BarChartWidth / BarTotalSize)) + ",0";
          PathData = PathData + "h" + (BarChartWidth / BarTotalSize * -1);
          PathData = PathData + "v" + (Height * BarChartHeight * -1);
          PathData = PathData + "l" + XOffset3D + ",-" + YOffset3D;
          PathData = PathData + "h" + (BarChartWidth / BarTotalSize);
          PathData = PathData + "v" + (Height * BarChartHeight);
          PathData = PathData + "l-" + XOffset3D + "," + YOffset3D;
          PathData = PathData + "v" + (Height * BarChartHeight * -1);
          PathData = PathData + "h" + (BarChartWidth / BarTotalSize * -1);
          PathData = PathData + "h" + (BarChartWidth / BarTotalSize);
          PathData = PathData + "l" + XOffset3D + ",-" + YOffset3D;
          PathData = PathData + "l-" + XOffset3D + "," + YOffset3D;
          
          if(tHeight == -1)
          {
					PathData = "M" + (Start + (BarChartWidth / BarTotalSize)) + ",0";
					PathData = PathData + "h" + (BarChartWidth / BarTotalSize * -1);
					PathData = PathData + "v" + (0 * BarChartHeight * -1);
					PathData = PathData + "l" + XOffset3D + ",-" + YOffset3D;
					PathData = PathData + "h" + (BarChartWidth / BarTotalSize);
					PathData = PathData + "v" + (0 * BarChartHeight);
					PathData = PathData + "l-" + XOffset3D + "," + YOffset3D;
					PathData = PathData + "v" + (0 * BarChartHeight * -1);
					PathData = PathData + "h" + (BarChartWidth / BarTotalSize * -1);
					PathData = PathData + "h" + (BarChartWidth / BarTotalSize);
					PathData = PathData + "l" + XOffset3D + ",-" + YOffset3D;
					PathData = PathData + "l-" + XOffset3D + "," + YOffset3D;
					PathData = PathData + "v9";		//以0高度的柱体作为分割线
			}
			Element.setAttribute("d", PathData)
			
			//设定X轴下方显示的文字的X点坐标 
			Label.setAttribute("x", Start - (BarChartWidth / BarTotalSize) * GroupBarSize / 2);
			Label.setAttribute("y", 20);
			
			//Label.setAttribute("transform", "rotate(45)");
			//Label.setAttribute("y", Start * -1 / AngleFactor + 25);
			//Label.setAttribute("x", Start / AngleFactor);
			
			Text.setAttribute("y", 30);								//设定柱图（鼠标悬浮）显示数值的Y坐标位置
			Text.setAttribute("x", 170);								//设定柱图（鼠标悬浮）显示数值的X坐标位置
			Text.setAttribute("style", "font-size:15;visibility:hidden;");
			
			return Start + BarChartWidth / BarTotalSize;
      }
       
      //------------------------------------------------------------------------------------------------------------------------
      //创建饼图
      function DrawPieSegment(Start, Size, Element, ID)
      {
          PathData = "M0,0L"; 
          PathData = PathData + PieChartSize * Math.sin(Start * Math.PI * 2) + "," + PieChartSize * Math.cos(Start * Math.PI * 2)
          if (Size >= .5)
            PathData = PathData + "A" + PieChartSize + " " + PieChartSize + " 1 1 0 " + PieChartSize * Math.sin((Start + Size) * Math.PI * 2) + "," + PieChartSize * Math.cos((Start + Size) * Math.PI * 2)
          else
            PathData = PathData + "A" + PieChartSize + " " + PieChartSize + " 0 0 0 " + PieChartSize * Math.sin((Start + Size) * Math.PI * 2) + "," + PieChartSize * Math.cos((Start + Size) * Math.PI * 2)
          PathData = PathData + "z"
          
          Element.setAttribute("d", PathData)
          if (Start > 0)
            Element.setAttribute("onclick", "MoveSegment(evt, " + (Start + Size / 2) + ", true, " + ID + ")");
          else
          {
              DeleteList[ID] = true;
              Angle = Start + Size / 2;
            
              X = MoveDistance * Math.sin(Angle * 2 * Math.PI);
              Y = MoveDistance * Math.cos(Angle * 2 * Math.PI);
              Element.setAttribute("transform", "translate(" + X + "," + Y + ")");
              Element.setAttribute("onclick", "MoveSegment(evt, " + ((Start + Size / 2) * -1) + ", false, " + ID + ")");
          }
          
          return Start + Size;
      }
        
      //------------------------------------------------------------------------------------------------------------------------
      /*鼠标单击饼图的移动动作*/
      function MoveSegment(MouseEvent, Angle, CanBeDeleted, ID)
      {
          Element = MouseEvent.getTarget()
          if (Angle < 0)
            {
              X = 0;
              Y = 0;
            }
          else
            {
              X = MoveDistance * Math.sin(Angle * 2 * Math.PI);
              Y = MoveDistance * Math.cos(Angle * 2 * Math.PI);
            }
            
          DeleteList[ID] = CanBeDeleted
          
          Element.setAttribute("transform", "translate(" + X + "," + Y + ")");
          Element.setAttribute("onclick", "MoveSegment(evt, " + (Angle * -1) + ", " + (!CanBeDeleted) + ", " + ID + ")");
      }
      
      //------------------------------------------------------------------------------------------------------------------------
      //饼图 显示信息
      function DisplayInfo(Text, Value)
      {
          if (Text != " ")
            Percent = " (" + Math.round(Value / PieTotalSize * 10000) / 100 + "%)";
          else
            Percent = "";
          
          NewItem = SVGDocument.createTextNode(Value + Percent);
          SVGDocument.getElementById("labelamount").replaceChild(NewItem, SVGDocument.getElementById("labelamount").getFirstChild())
          NewItem = SVGDocument.createTextNode(Text + "");
          SVGDocument.getElementById("labelitem").replaceChild(NewItem, SVGDocument.getElementById("labelitem").getFirstChild())
          if (Text + Value == "  ")
            NewItem = SVGDocument.createTextNode(" ");
          else
            NewItem = SVGDocument.createTextNode(":");
          SVGDocument.getElementById("labelcolon").replaceChild(NewItem, SVGDocument.getElementById("labelcolon").getFirstChild())
      }
      
      //------------------------------------------------------------------------------------------------------------------------
      //设定标题文本
      function SetTitle(Text)
      {
          NewItem = SVGDocument.createTextNode(Text + "");
          SVGDocument.getElementById("title").replaceChild(NewItem, SVGDocument.getElementById("title").getFirstChild());
      }
      
      //------------------------------------------------------------------------------------------------------------------------
      //设定纵轴文本
      function SetAxis(Text)
      {
          NewItem = SVGDocument.createTextNode(Text + "");
          SVGDocument.getElementById("axis").replaceChild(NewItem, SVGDocument.getElementById("axis").getFirstChild());
          NewItem = SVGDocument.createTextNode(Text + "");
          SVGDocument.getElementById("subtitle").replaceChild(NewItem, SVGDocument.getElementById("subtitle").getFirstChild());
          
      }
      
      //------------------------------------------------------------------------------------------------------------------------
      //显示饼图
      function ShowPie()
        {
          ActiveSegment = "Pie";	//饼状图
         
          SVGDocument.getElementById("barchart").getStyle().setProperty("visibility", "hidden");
          //SVGDocument.getElementById("subtitle").getStyle().setProperty("visibility", "show");
          SVGDocument.getElementById("piechart").getStyle().setProperty("visibility", "show");
          SVGDocument.getElementById("pieitemlabel").getStyle().setProperty("visibility", "show");
          SVGDocument.getElementById("barsidetext").getStyle().setProperty("visibility", "hidden");
        }
      
      //------------------------------------------------------------------------------------------------------------------------
      //显示柱图
      function ShowBar()
      {
          ActiveSegment = "Bar"	//柱状图

          SVGDocument.getElementById("barchart").getStyle().setProperty("visibility", "show");
          //SVGDocument.getElementById("subtitle").getStyle().setProperty("visibility", "hidden");
          SVGDocument.getElementById("piechart").getStyle().setProperty("visibility", "hidden");
          SVGDocument.getElementById("pieitemlabel").getStyle().setProperty("visibility", "hidden");
          SVGDocument.getElementById("barsidetext").getStyle().setProperty("visibility", "show");
     	}//end of ShowBar()
      
      //------------------------------------------------------------------------------------------------------------------------  
      //计算并显示Y轴刻度
      function ComputeNumber()
      {
			//计算Y轴每单位的数量并初始化Y轴刻度，次高位数字以5为界限判断进位
			var unitSize	= MaxSize / 5;
			var strSize	= unitSize.toString();
			var dotIndex	= strSize.indexOf(".");
			var unitLen;
	        
			if(dotIndex != -1)
			{
				strSize	= strSize.substring(0,dotIndex);	//去掉小数点后的位数
				unitLen	= dotIndex;							//长度位数
			}
			else
			{
				unitLen = strSize.length;
			}
	        
			var unitA		= strSize.substring(0,1);		//最高位数字
			var unitB		= strSize.substring(1,2);		//次高位数字
			
			unitA = unitA + unitB;
			
			for(i = 0; i < unitLen - 2; i++)
			{
				unitA += "0";
			}
	        
			unitA = unitA * 1;
	        
	      /*显示Y轴刻度数值*/
			SVGDocument.getElementById("txtSecond").appendChild(SVGDocument.createTextNode(unitA * 1));
			SVGDocument.getElementById("txtThird").appendChild(SVGDocument.createTextNode(unitA * 2));
			SVGDocument.getElementById("txtFourth").appendChild(SVGDocument.createTextNode(unitA * 3));
			SVGDocument.getElementById("txtFive").appendChild(SVGDocument.createTextNode(unitA * 4));
			//SVGDocument.getElementById("txtSix").appendChild(SVGDocument.createTextNode(unitA * 5));
      }//end of ComputeNumber()
      
      //------------------------------------------------------------------------------------------------------------------------
      //设定背景颜色
      function SetBgColor(bgColor)
      {
		SVGDocument.getElementById("ChartBgColor").getStyle().setProperty("fill", bgColor);
      }
	  
	  //------------------------------------------------------------------------------------------------------------------------
      function HiddenChartExplain()
      {
		SVGDocument.getElementById("ChartExplain").getStyle().setProperty("display", "none");
      }
	  
	  //------------------------------------------------------------------------------------------------------------------------
      parent.addChartValue	= AddChartValue;
      parent.setAxis		= SetAxis;
      
      parent.showBar		= ShowBar;
      parent.showPie		= ShowPie;
      parent.setBgColor		= SetBgColor;
      parent.hiddenChartExplain = HiddenChartExplain;
    
    ]]>
  </script>


	
	<!--**********************************************************************************************************-->

	<g id="hiddenChart" transform="translate(0, 0)">
	
		<g>
			<!--设定背景色-->
			<path id="ChartBgColor" style="opacity:0.15;fill:#9999CC;" d="M 1570,1270 h -2570 v -2270 h 2570 V 1270 z"/>
		</g>
		
		<g id="barsidetext">
			<!--Y轴显示的单位文本文位置-->
			<text x="-190" y="25" style="font-weight:bold;text-anchor:middle;font-size:13;display:none;" id="axis" transform="rotate(-90)">
			</text>
			<text x="52" y="55" style="font-weight:bold;text-anchor:end;font-size:13" id="max"><!--Y轴最大值--></text>
			<text x="52" y="225" style="font-weight:bold;text-anchor:end;font-size:13" id="min"><!--Y轴最小值--></text>
	
			<!--划出Y轴并将Y轴平均分为5分，按照目前的分割每份为34单位-->
			<path d="M60,220 V 50 H 55 H 65" style="fill:none;stroke:black;filter:url(#DropShadowFilter)"/>
	        
			<!--画出Y轴单位分割线-->
			<line x1="60" y1="220" x2="55" y2="220" stroke="black"/>
	
			<line x1="55" y1="186" x2="450" y2="186" stroke-width="5"  />
			<text x="52" y="186" style="font-weight:bold;text-anchor:end;font-size:13" id="txtSecond"></text>
	
			<line x1="55" y1="152" x2="450" y2="152" stroke-width="5"  />
			<text x="52" y="152" style="font-weight:bold;text-anchor:end;font-size:13" id="txtThird"></text>
	
			<line x1="55" y1="118" x2="450" y2="118" stroke-width="5"  />
			<text x="52" y="118" style="font-weight:bold;text-anchor:end;font-size:13" id="txtFourth"></text>
	
			<line x1="55" y1="84" x2="450" y2="84" stroke-width="5"  />
			<text x="52" y="84" style="font-weight:bold;text-anchor:end;font-size:13" id="txtFive"></text>
			
			<!--上-->
			<line x1="55" y1="50" x2="450" y2="50" stroke-width="5"  />
			<text x="52" y="50" style="font-weight:bold;text-anchor:end;font-size:13" id="txtSix"></text>
		</g>
	      
	     <g id="barchart" transform="translate(60, 220)">
	       <!--划X轴直线-->
	       <path d="M0,0V 0H380 0v8" style="fill:none;stroke:black;filter:url(#DropShadowFilter)"/>
	       <g id="bars" style="filter:url(#DropShadowFilter)"></g>
	       <g id="labels"></g>
	     </g>
	       
	     <g style="filter:url(#DropShadowFilter)">
	        <g id="piechart" transform="translate(250, 130) scale(1, .5)" style="visibility:hidden">
	          	<g id="slices"></g>
	        </g>
	     </g>
	       
	     <g id="pieitemlabel" style="visibility:hidden">
	        <text x="240" y="260" id="labelitem" style="text-anchor:end;font-size:14;font-weight:bold">
	        </text>
	        <text x="242" y="260" id="labelcolon" style="text-anchor:middle;font-size:14">
	        </text>
	        <text x="245" y="260" id="labelamount" style="text-anchor:begin;font-size:14">
	        </text>
	     </g>
	    
	     <g>
	        <!--设定图形标题-->
	        <text x="20" y="20" style="font-weight:bold;text-anchor:middle;font-size:20" id="title">
	        </text>
	        
	        <text x="300" y="30" style="font-weight:bold;text-anchor:middle;font-size:14" id="subtitle">
	        </text>
	     </g>
	     
	     <!--图例说明-->
	     <g id="ChartExplain" style="display:''" transform="translate(460, 40)"></g>
	</g>
</svg>

