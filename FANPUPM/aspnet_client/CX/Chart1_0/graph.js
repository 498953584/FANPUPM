<?xml version="1.0" encoding="utf-8"?>
<!DOCTYPE svg PUBLIC "-//W3C//DTD SVG 20001102//EN" "http://www.w3.org/TR/2000/CR-SVG-20001102/DTD/svg-20001102.dtd">
<svg onload="Initialize(evt)" width="570" height="270" viewBox="0 0 570 270" xml:space="preserve">
	<title>曲线图</title>
	<desc>曲线图</desc>

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
	
	<script><![CDATA[
		//------------------------------------------------------------------------------------------------------------------------
		//定义全局变量
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
                             "#66FFCC");	//预定义所使用的色彩
                             
        var ChartHeight		= 180;			//Y轴高度
		var ChartWidth		= 380;			//X轴宽度
		var CircleR			= 2;			//曲线上小圆点的半径
		var CircleBorder	= "black";			//曲线上小圆点的边框颜色
		
        var CurrentColor	= 0;			//记录当前色彩数组值
        var CurrentName		= 0;			//当前曲线名称数组的索引
		var SVGDocument		= null
		var UnitSize		= 0;			//记录分组个数 (节点数)
		var isUnitSize		= true;			//是否记录分组个数
		var LineSize		= 0;			//记录线条个数
		var YArray			= new Array();	//存储 Y 轴数值
		var Elements		= new Array();	//存储节点元素
		var XText			= new Array();	//存储 X 轴单位文本
		var LineName		= new Array();	//存储每个曲线代表的名称
		var MaxX			= 0;			//X 轴最大值
		var MaxY			= 0;			//Y 轴最大值
		var YSize			= 0;			//记录图示间隔距离
		var ParentGroup		= null;			//曲线上面的节点的元素
		var Grandparent		= null;			//坐标节点元素
		var ChartXYLine		= null;			//坐标刻度节点元素
		var ChartLine		= null;			//曲线节点元素
		var ChartExplains	= null;			//图例节点元素

		//------------------------------------------------------------------------------------------------------------------------
		//初始化页面元素
		function Initialize(LoadEvent)
		{
			SVGDocument = LoadEvent.getTarget().getOwnerDocument();

			ParentGroup = SVGDocument.getElementById("loadlayer");
			Grandparent = SVGDocument.getElementById("chart");		
			ChartXYLine	= SVGDocument.getElementById("XYLine");				//刻度线
			ChartLine		= SVGDocument.getElementById("chartline");		//曲线

			ChartExplains = SVGDocument.getElementById("ChartExplain");		//图例
		}
      
		//------------------------------------------------------------------------------------------------------------------------
		//向图形中增加数据元素，并根据数据绘制图形（对外公开的接口函数）
		//		Y 			：显示在 Y 轴上的数值
		//		X			：显示在 X 轴上的文本
		//		Repress	: 该条曲线的结束位置标示
	
      
		function AddChartValue(Y, X, Repress, Name)
		{
		/*
			X = X * 1;
			Y = Y * 1;			
			if ((X < 0) || (Y < 0))
			{
				alert("存在无效数据！");
				return;
			}        */
			Y = Y * 1;			
			if  (Y < 0)
			{
				alert("存在无效数据！");
				return;
			}
			//NewItem = XArray.length;			

			YArray[YArray.length] = Y * 1;
			
			//获得 X 轴文本	
			if(isUnitSize)
			{
				UnitSize++;
				XText[XText.length] = X;
			}
			if(!Repress)
			{
				isUnitSize = false;
				LineSize++;

				LineName[LineName.length] = Name;
			}
		}
      
		//------------------------------------------------------------------------------------------------------------------------
		//鼠标悬浮节点所显示的信息
		function DisplayInfo(Text, Value)
		{
			NewItem = SVGDocument.createTextNode(Value)
			SVGDocument.getElementById("labelamount").replaceChild(NewItem, SVGDocument.getElementById("labelamount").getFirstChild())
			NewItem = SVGDocument.createTextNode(Text)
			SVGDocument.getElementById("labelitem").replaceChild(NewItem, SVGDocument.getElementById("labelitem").getFirstChild())
			if (Text + Value == "  ")
			NewItem = SVGDocument.createTextNode(" ")
			else
			NewItem = SVGDocument.createTextNode(":")
			SVGDocument.getElementById("labelcolon").replaceChild(NewItem, SVGDocument.getElementById("labelcolon").getFirstChild())
		}
      
		//------------------------------------------------------------------------------------------------------------------------
		//设定Y、X轴的单位
		function SetAxes(YText, XText) 
		{
			NewItem = SVGDocument.createTextNode(XText)
			SVGDocument.getElementById("xaxis").replaceChild(NewItem, SVGDocument.getElementById("xaxis").getFirstChild())
			NewItem = SVGDocument.createTextNode(YText)
			SVGDocument.getElementById("yaxis").replaceChild(NewItem, SVGDocument.getElementById("yaxis").getFirstChild())
		}
      
		//------------------------------------------------------------------------------------------------------------------------
		//当数据增加结束时开始划图（圆点和曲线以及XY刻度）
		function CreateEnd()
		{
			//计算 Y 轴最大值
			for (var i = 0; i < YArray.length; i++)
			{
				if(YArray[i] > MaxY)
				{
					MaxY = YArray[i];
				}
			}

			var arrIndex 	= 0;								//临时变量，记录当前YArray数组的索引
			var UnitWidth 	= ChartWidth / UnitSize;	//计算 X 轴每个单位的宽度

			for (var i = 0; i < LineSize; i++)			//在坐标中划点
			{
				//确定曲线的颜色
				Color = ColorArray[CurrentColor];		          
				CurrentColor++;
				if (CurrentColor >= ColorArray.length)
				{
					CurrentColor = 0;
				}
				
				var tempCount 	= 1;
				var PathData	= "M ";
				for(var j = 0; j < UnitSize; j++)
				{
					//划曲线上面的圆点
					NewElement = SVGDocument.createElement("circle");
					Elements[Elements.length] = NewElement;
					Elements[Elements.length - 1].setAttribute("style", "stroke:" + CircleBorder + ";fill:" + Color);
					Elements[Elements.length - 1].setAttribute("cx", UnitWidth * tempCount - UnitWidth / 2);
					Elements[Elements.length - 1].setAttribute("cy", -1 * YArray[arrIndex] / MaxY * ChartHeight);
					Elements[Elements.length - 1].setAttribute("r", CircleR);
					ParentGroup.appendChild(Elements[Elements.length - 1]);

					//计算曲线的路径
					PathData = PathData + " " + (UnitWidth * tempCount - UnitWidth / 2) + " " + (-1 * YArray[arrIndex] / MaxY * ChartHeight);

					arrIndex ++;
					tempCount++;
				}
				//根据点开始画曲线
				/**/
				LineElements = SVGDocument.createElement("path");
				LineElements.setAttribute("style", "fill:none;stroke:" + Color);
				LineElements.setAttribute("d", PathData);
				ChartLine.appendChild(LineElements);
				
				SetExplain(Color);					//显示图例

			}// end of for loop

			// 设定X、Y 轴最大值与最小值
			MaxYElement = SVGDocument.getElementById("maxY").getFirstChild();
			SVGDocument.getElementById("maxY").replaceChild(SVGDocument.createTextNode(MaxY), MaxYElement);
			SVGDocument.getElementById("min").replaceChild(SVGDocument.createTextNode(0), SVGDocument.getElementById("min").getFirstChild())

			ComputeNumber(UnitWidth);		//计算 X 、Y 轴上的刻度值，并划出相关分割线
			//SetExplain();					//显示图例
			
		}//end of CreateEnd()
				
		//------------------------------------------------------------------------------------------------------------------------
		//显示图例的图片和文本
		function SetExplain(Color)
		{
			//for( var i = 0; i < LineName.length; i++)
			//{
				//增加图形

				var vChart = SVGDocument.createElement("rect");
				vChart.setAttribute("x", "0");
				vChart.setAttribute("y", YSize);
				vChart.setAttribute("width", "15");
				vChart.setAttribute("height", "15");
				vChart.setAttribute("fill", Color);

				ChartExplains.appendChild(vChart);

				//增加文本

				var vText = SVGDocument.createElement("text");
				vText.setAttribute("x", "20");
				vText.setAttribute("y", YSize + 12);
				vText.appendChild(SVGDocument.createTextNode(LineName[CurrentName]));
				ChartExplains.appendChild(vText);

				YSize += 20;
				CurrentName ++;
			//}
		}
      
		//------------------------------------------------------------------------------------------------------------------------
		//计算 X 、Y 轴上的刻度值，并划出相关分割线
		function ComputeNumber(UnitWidth)
		{
			//处理 X 轴刻度及刻度下方的显示文本
			var LineHeight = 10;
			for( var i = 1; i < UnitSize + 1; i++)
			{
				//划 X 轴 刻度线
				if(i != UnitSize)
				{
					XLineElement = SVGDocument.createElement("line");
					XLineElement.setAttribute("x1", UnitWidth * i);
					XLineElement.setAttribute("y1", 0);
					XLineElement.setAttribute("x2", UnitWidth * i);
					XLineElement.setAttribute("y2", LineHeight);
					ParentGroup.appendChild(XLineElement);
				}

				//创建 X 轴下方单位文本 
				XTextElement = SVGDocument.createElement("text");
				XTextElement.setAttribute("x", UnitWidth * i - UnitWidth / 2 -15);
				XTextElement.setAttribute("y", 20);
				XTextElement.appendChild(SVGDocument.createTextNode(XText[i - 1]));

				ChartXYLine.appendChild(XTextElement);
			}
      	
      		//处理 Y 轴刻度及显示的值；计算Y轴每单位的数量并初始化Y轴刻度，次高位数字以5为界限判断进位
      		var unitSize	= MaxY / 5;
			var strSize		= unitSize.toString();
			var dotIndex	= strSize.indexOf(".");
			var unitLen;
			if(dotIndex != -1)
			{
				strSize	= strSize.substring(0,dotIndex);//去掉小数点后的位数
				unitLen	= dotIndex;								//长度位数
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
		}
		//------------------------------------------------------------------------------------------------------------------------
		//对父级页面公开的接口
		SvgFile.addChartValue = AddChartValue;
		SvgFile.setAxes = SetAxes;
		SvgFile.createEnd  = CreateEnd;
		//------------------------------------------------------------------------------------------------------------------------
    ]]></script>

	<g id="hiddenGraph" transform="translate(0, 0)">
		<g>
			<!--设定背景色-->
			<path style="opacity:0.15;fill:#9999CC" d="M 1570,1270 h -2570 v -2270 h 1570 V 1270 z"/>
		</g>
		<g id="chart" transform="translate(60, 220)">
			<g id="loadlayer" style="filter:url(#DropShadowFilter)">
				<!--划X、Y轴-->
				<path d="M0,0H-5 0 V 5 -170 h -5 10 -5 V 0 H 380 V 5 -5 0 H 0" style="fill:none;stroke:black"/>
				<!--划曲线-->
				<g id="chartline"/>
			</g>
			
			<!--设定XY轴刻度及文本信息-->
			<g id="XYLine"></g>
			
			<!--画出Y轴单位分割线，划出Y轴并将Y轴平均分为5分，按照目前的分割每份为34单位-->

			<line x1="-5" y1="-34" x2="380" y2="-34" stroke-width="5"  />
			<text x="-7" y="-34" style="font-weight:bold;text-anchor:end;font-size:13" id="txtSecond"></text>
			<line x1="-5" y1="-68" x2="380" y2="-68" stroke-width="5"  />
			<text x="-7" y="-68" style="font-weight:bold;text-anchor:end;font-size:13" id="txtThird"></text>
			<line x1="-5" y1="-102" x2="380" y2="-102" stroke-width="5"  />
			<text x="-7" y="-102" style="font-weight:bold;text-anchor:end;font-size:13" id="txtFourth"></text>
			<line x1="-5" y1="-136" x2="380" y2="-136" stroke-width="5"  />
			<text x="-7" y="-136" style="font-weight:bold;text-anchor:end;font-size:13" id="txtFive"></text>
			<line x1="-5" y1="-170" x2="380" y2="-170" stroke-width="5"  />	<!--上-->
			<text x="-7" y="-170" style="font-weight:bold;text-anchor:end;font-size:13" id="txtSix"></text>
		</g>
		
		<g>
			<text x="100" y="260">
				<tspan id="labelitem" style="font-weight:bold"> </tspan>
				<tspan id="labelcolon"> </tspan>
				<tspan id="labelamount"> </tspan>
			</text>
		</g>
		<g>
			<!--绘制Y轴 单位-->
			<text x="60" y="25" style="font-weight:bold;text-anchor:middle;font-size:13" id="yaxis">
			</text>
			<text x="460" y="240" style="font-weight:bold;text-anchor:middle;font-size:13" id="xaxis">
			</text>
			<!--绘制Y轴最大值和最小值-->
			<text x="52" y="225" style="font-weight:bold;text-anchor:end;font-size:13" id="min"><![CDATA[ ]]></text>
			<text x="355" y="225" style="font-weight:bold;text-anchor:middle;font-size:13" id="maxX"><![CDATA[ ]]></text>
			<text x="52" y="50" style="font-weight:bold;text-anchor:end;font-size:13" id="maxY"><![CDATA[ ]]></text>
		</g>
		
		<!--图例说明-->
	   <g id="ChartExplain" transform="translate(450, 40)"></g>
	</g>
</svg>
