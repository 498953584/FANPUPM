/***********************************************************************
名称：CXWOCWebToolBox
功能描述: 实例化一个CXWOCMDIWnd，生成一个MDIFrame,通过ADD方法添加MDI窗口,
          实现多文档模式.
创建日期: 2003-12-23
作者: 鲁小帅
==========================================================
--------------------------修改记录-------------------------------
修改人:	            修改时间:         
修改原因:
-----------------------------------------------------------------
==========================================================
备注：
***********************************************************************/

function CXWOCMDIWnd() {
    var _oThis=this;
    this._className="WOCMDIWnd";
    
    var oFrameCon=null;
    var oTaskUp=null;
    var oTaskDown=null;
    var oMDIFrame=null;                //窗口容器
	var oDashedDiv=null;               //窗口大小调整时显示的虚线框
	var oTaskBar=null;                 //任务栏
    var aChildWndList=new Array();     //子窗口数组
    var activeWnd=null;                //当前活动窗口    
    var startResized=false;            //窗口是否开始大小调整
    
    var frameBgColor="window";
    var barBgColor="menu";
    var barBtnDownBgColor="threedhighlight";
    var dashedBorderColor="threedshadow";
    var captionButtonColor="ButtonFace";
    var activeCaptionColor="activecaption";
    var activeCaptionTextColor="captiontext";
    var inactiveCaptionColor="inactivecaption";
    var inactiveCaptionTextColor="inactivecaptiontext";
    var windowBgColor="ThreeDFace";
    var sFontSize="12px";
	var sFontFamily="宋体";
    var locationData={top:0,left:0};
    
	this.create=create;                
	this.add=add;
	this.closeAll=closeAll;
	this.minimizeAll=minimizeAll;
	this.cascadeWindow=cascadeWindow;
	this.getWidth=getWidth;
	this.setWidth=setWidth;
	this.getHeight=getHeight;
	this.setHeight=setHeight;
	this.setSize=setSize;
	
	this.getFrameWidth=getFrameWidth;
	this.setFrameWidth=setFrameWidth;
	this.getFrameHeight=getFrameHeight;
	this.setFrameHeight=getFrameHeight;
	
	this.getBgColor=getBgColor;
	this.setBgColor=setBgColor;	
	this.setBgImg=setBgImg;
	
	this.getActive=getActive;
	this.setActive=setChildActive;
	
    /***********************************************************************
	事件名称：onCommand()
	事件功能：窗口关闭事件
	创建日期: 2003-11-25
    作者: 鲁小帅
	==========================================================
	-----------------------修改记录-----------------------------------
	修改人:	修改时间: 
	修改原因:
	-----------------------------------------------------------------
	==========================================================
	备注：
	***********************************************************************/ 
	this.onClose=null;
    /***********************************************************************
	事件名称：onMaximize()
	事件功能：窗口最大化事件
	创建日期: 2003-11-25
    作者: 鲁小帅
	==========================================================
	-----------------------修改记录-----------------------------------
	修改人:	修改时间: 
	修改原因:
	-----------------------------------------------------------------
	==========================================================
	备注：
	***********************************************************************/ 
	this.onMaximize=null;
    /***********************************************************************
	事件名称：onCaptionChange()
	事件功能：窗口标题文字修改事件
	创建日期: 2003-11-25
    作者: 鲁小帅
	==========================================================
	-----------------------修改记录-----------------------------------
	修改人:	修改时间: 
	修改原因:
	-----------------------------------------------------------------
	==========================================================
	备注：
	***********************************************************************/ 
	this.onCaptionChange=null;	
	
	/***********************************************************************
	事件名称：onActive()
	事件功能：活动窗口变化事件
	创建日期: 2004-01-15
    作者: 鲁小帅
	==========================================================
	-----------------------修改记录-----------------------------------
	修改人:	修改时间: 
	修改原因:
	-----------------------------------------------------------------
	==========================================================
	备注：
	***********************************************************************/ 
	this.onActive=null;	

    /***********************************************************************
    函数或过程名称: initializtion() {
    输入参数: 无
    输出参数: 无
    功能描述: 初始化CXWOCMDIWnd.
    流程处理: 生成MDIFrame的主框架,主框架为一个三行一列的表格,上、下表格为TaskBar
              的容器,宽度设定成1,中间表格存放窗口容器,高度设定100%,这样设置程序就
              不需要再控制容器的大小，它会随IE窗口的变化而变化。
    全局变量: 
    调用方法: initializtion()
    调用样例: initializtion()
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

	function initializtion() {
        //创建主框架表格
	    oFrameCon=document.createElement('<table cellspacing="0" cellpadding="0">');
	    oFrameCon.attachEvent("onresize",onFrameResize);
	    with (oFrameCon.style) {
	        backgroundColor=frameBgColor;
	        position="relative";
            //初始宽,高都为100%,可通过setSize设置
	        width="100%";
	        height="100%"; 
	        zIndex="0";
	        border="0";
	    }
        //TaskBar上部容器
	    var oTr=oFrameCon.insertRow();
	    oTaskUp=oTr.insertCell();	    
	    oTaskUp.style.width="100%";
	    oTaskUp.style.height="1px";	    
	    //oTaskUp.style.backgroundColor=frameBgColor;	    
	    
        //Frame容器
	    oTr=oFrameCon.insertRow();
	    var oTd=oTr.insertCell();	    
	    oMDIFrame=document.createElement('<div unselectable="on">');
		with (oMDIFrame.style) {
		    position="relative";
		    overflow="hidden";
	        backgroundColor=frameBgColor;
	        //border="2px inset";
	        width="100%";
	        height="100%";
	        zIndex="0";
		}
		oTd.insertAdjacentElement("beforeEnd",oMDIFrame);
	    
        //TaskBar下部容器
	    oTr=oFrameCon.insertRow();
	    oTaskDown=oTr.insertCell();
	    oTaskDown.style.width="100%";
	    oTaskDown.style.height="1px";
	    //oTaskDown.style.backgroundColor=frameBgColor;	    
	}    

    /***********************************************************************
    函数或过程名称: create(oContainer)
    输入参数: oContainer object 组件要创建到的容器
    输出参数: 无
    功能描述: 将组件创建到一个容器
    流程处理: 如果组件没有被创建过,且传入的对象为一个容器,将组件创建到里面.
    全局变量: 
    调用方法: create(oContainer)
    调用样例: create(document.body)
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

    function create(oContainer) {
        //如果没有被创建过
        if (!(_oThis.hcon)) {
            //如果对象不是一个容器
            if (!oContainer.canHaveChildren) {				
                throw "创建失败，create()方法的参数不是一个容器的id！";
            	return;
            } else {				
                oContainer.insertAdjacentElement("beforeEnd",oFrameCon);
                _oThis.hcon = oContainer;                
            }
        }
    }
    
    /***********************************************************************
    函数或过程名称: getBgColor()
    输入参数: 无
    输出参数: string 窗口容器背景色
    功能描述: 
    流程处理: 
    全局变量: 
    调用方法: getBgColor()
    调用样例: getBgColor()
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

    function getBgColor() {
        return oFrameCon.style.backgroundColor;
    }

    /***********************************************************************
    函数或过程名称: setBgColor(sColor)
    输入参数: sColor string 颜色值
    输出参数: 无
    功能描述: 设定窗口容器的背景色
    流程处理: 
    全局变量: 
    调用方法: setBgColor(sColor)
    调用样例: setBgColor("#ffffff")
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

    function setBgColor(sColor) {
        //如果参数是string类型,而且长度大于0
        if (typeof sColor=="string" && sColor.length>0) {
            //主框架背景色,由于上下部有1px的空白,同时设定背景色防止出现杂色边
            oFrameCon.style.backgroundColor=sColor;
            //窗口容器背景色
            oMDIFrame.style.backgroundColor=sColor;
        }
    }    
    
    /***********************************************************************
    函数或过程名称: setBgImg(sImg,sMode)
    输入参数: sImg string 图片文件名
              sMode string 图片放置方式,定义如下:
              centered  居中
              tiled     平铺
              TL        上左
              TC        上中
              TR        上右
              CL        中左
              CC        居中
              CR        中右
              BL        下左
              BC        下中
              BR        下右
    输出参数: 无
    功能描述: 设定窗口容器的背景图片和方式
    流程处理: 根据放置方式代码确定图片要放置的位置
    全局变量: 
    调用方法: setBgImg(sImg,sMode)
    调用样例: setBgImg("backimg.jpg","centered")
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

    function setBgImg(sImg,sMode) {
        //没有传入合法的图片文件,直接返回
        if (typeof sImg=="string" && sImg.length>0) {
            //添加图片到临时字符串
            var sTemp="url("+sImg+")";
            if (sMode) {
                //把模式串转换成小写
                sMode=sMode.toLowerCase();
                switch (sMode) {   
                    case "centered": //居中
                        sTemp+=" no-repeat center center";
                        break;   
                    case "tiled": //平铺
                        sTemp+=" repeat";
                        break;
                    default ://设定方位
                        //如果长度小于1,直接不处理返回
                        if (sMode.length<1) return;
                        //设定不平铺
                        sTemp+=" no-repeat";
                        //模式定义 T 上 C 中 B 下
                        var sP=sMode.substr(0,1);
                        switch (sP) {
                            case "t":
                                sTemp+=" top";
                                break;
                            case "c":
                                sTemp+=" center";
                                break;
                            case "b":
                                sTemp+=" bottom";
                                break;
                        }
                        //模式定义 L 左 C 中 R 右
                        var sP=sMode.substr(1,1);
                        switch (sP) {
                            case "l":
                                sTemp+=" left";
                                break;
                            case "c":
                                sTemp+=" center";
                                break;
                            case "r":
                                sTemp+=" right";
                                break;
                        }
                        break;
                }
            }
            oMDIFrame.style.background=sTemp;
        }
    }

    /***********************************************************************
    函数或过程名称: getActive()
    输入参数: 无
    输出参数: 返回当前活动的窗口
    功能描述: 
    流程处理: 
    全局变量: 
    调用方法: getActive()
    调用样例: getActive()
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/
    
    function getActive() {
        return activeWnd;
    }
    
    /***********************************************************************
    函数或过程名称: setActive(oWnd)
    输入参数: oWnd object 需要置为活动窗口的对象
    输出参数: 无
    功能描述: 设置一个窗口为活动窗口
    流程处理: 设定OWnd为活动窗口,如果之前有活动窗口,设定它为非活动状态,
			  更改TaskBar的状态
    全局变量: 
    调用方法: setActive(oWnd)
    调用样例: setActive(oWnd)
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

    function setActive(oWnd) {
        if (oWnd==null) {
            activeWnd=null;
            if (_oThis.onCaptionChange!=null) _oThis.onCaptionChange("");
            if (_oThis.onActive!=null) _oThis.onActive();
            return;
        }
		//如果没有活动的窗口
        if (activeWnd==null) {                  
            activeWnd=oWnd; //标记活动窗口
            setTrue();  //设置当前窗口为活动窗口
            return;
        }
        if (activeWnd==oWnd) return;
		//设置活动窗口方法
        function setTrue() {
            oWnd.setActive(true);
            if (oWnd.getState()=="minimized") oWnd.windowMinimize();
            if (oTaskBar!=null) oTaskBar.setButtonState(oWnd._index,true);
			//标记活动窗口后,触发onCaptionChange事件
            if (_oThis.onCaptionChange!=null) _oThis.onCaptionChange(oWnd.getCaption());
            if (_oThis.onActive!=null) _oThis.onActive();
        }
        
		//设置为活动状态            
        var oActiveWnd=activeWnd;
        activeWnd=oWnd;
        setTrue();
		//如果之前有活动的窗口,设置它为非活动状态
        if (oActiveWnd!=null) {
            activeWnd.setZIndex(oActiveWnd.getZIndex()+1);
            oActiveWnd.setActive(false);
		    //如果TaskBar存在,设定它的状态
            if (oTaskBar!=null) oTaskBar.setButtonState(oActiveWnd._index,false);
        }
    }
    
    /***********************************************************************
    函数或过程名称: setChildActive(iIndex)
    输入参数: iIndex integer 窗口索引
    输出参数: 无
    功能描述: 设定指定索引的窗口为活动窗口
    流程处理: 通过窗口对象数组确定窗口对象,调用setActive方法实现
    全局变量: 
    调用方法: setChildActive(iIndex)
    调用样例: setChildActive(1)
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

    function setChildActive(iIndex) {
		//如果指定索引的窗口存在,调用setActive方法
        if (aChildWndList[iIndex]!=null) setActive(aChildWndList[iIndex]);
    }
    
    /***********************************************************************
    函数或过程名称: getChildNumber()
    输入参数: 无
    输出参数: integer 当前窗口的数目
    功能描述: 获得当前窗口的数目
    流程处理: 通过窗口对象数组的长度,得到窗口数
    全局变量: 
    调用方法: getChildNumber()
    调用样例: getChildNumber()
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：当有窗口关闭过的时候,获得的数目不等于实际窗口数目,此方法主要用于
		  确定窗口层索引
    ***********************************************************************/

    function getChildNumber() {
        return aChildWndList.length-1;
    }

    /***********************************************************************
    函数或过程名称: createDashedDiv()
    输入参数: 无
    输出参数: 无
    功能描述: 创建一个窗口调整大小时显示的虚线框
    流程处理: 
    全局变量: 
    调用方法: createDashedDiv()
    调用样例: createDashedDiv()
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

    function createDashedDiv() {
		//创建一个DIV
        oDashedDiv=document.createElement('<div unselectable="on">');
        oDashedDiv.resizeData={};
		with (oDashedDiv.style) {
			position="relative";
			overflow="hidden";
			border="3px dashed "+dashedBorderColor; //虚线边框
			width="100";
			height="100";
			zIndex="20000";
			display="none";
		}
		oMDIFrame.insertAdjacentElement("beforeEnd",oDashedDiv);
		//获得自己到容器左边的距离	
        oDashedDiv.getLeft=function() {            
            return parseInt(oDashedDiv.style.left,10);            
        };
		
		//获得自己到容器上边的距离
        oDashedDiv.getTop=function() {
            return parseInt(oDashedDiv.style.top,10);
        };
		
		//获得自己的宽度
        oDashedDiv.getWidth=function() {
            return parseInt(oDashedDiv.offsetWidth,10);
        };
		
		//获得自己的高度
        oDashedDiv.getHeight=function() {
            return parseInt(oDashedDiv.offsetHeight,10);
        };
		
		//设置自己到容器左边的距离
        oDashedDiv.setLeft=function(iX) {
            oDashedDiv.style.left=iX;
        };    
        
		//设置自己到容器上边的距离
        oDashedDiv.setTop=function(iY) {
            oDashedDiv.style.top=iY;
        };
		
		//设置自己的宽度
        oDashedDiv.setWidth=function(iW) {
            oDashedDiv.style.width=iW;
        };
		
		//设置自己的高度
        oDashedDiv.setHeight=function(iH) {
            oDashedDiv.style.height=iH;
        };
        
		//设置自己的大小
        oDashedDiv.setSize=function(iW,iH) {
            oDashedDiv.setWidth(iW);
            oDashedDiv.setHeight(iH);
        };
        
		//设置自己的位置
		oDashedDiv.setLocation=function(iT,iL) {
		    oDashedDiv.setTop(iT);
		    oDashedDiv.setLeft(iL);
		};
		
		//设置自己的范围
        oDashedDiv.setBounds=function(iX,iY,iW,iH) {
            oDashedDiv.setLeft(iX);
            oDashedDiv.setTop(iY);
            oDashedDiv.setWidth(iW);
            oDashedDiv.setHeight(iH);
        };
    }
    
    /***********************************************************************
    函数或过程名称: setNewLocation(iW,iH)
    输入参数: iW integer 窗口宽度 iH integer 窗口高度
    输出参数: 无
    功能描述: 产生一个新的坐标,供新窗口使用
    流程处理: 根据传入的窗口大小,确定窗口该处在的位置
    全局变量: 
    调用方法: setNewLocation(iW,iH)
    调用样例: setNewLocation(200,300)
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

    function setNewLocation(iW,iH) {
        var iAW=20;//偏移量
        var iAH=20;
		//如果是第一次调用
        if (locationData.top==0 && locationData.left==0) {
            locationData.top=1;
            locationData.left=1;
            return;
        }
		//判断当前坐标加上窗口大小是否超出容器
        if (locationData.top+iH+iAH>getFrameHeight() || locationData.left+iW+iAW>getFrameWidth()) {
            locationData.top=1; 
            locationData.left=1;
            return;
        }
		//坐标加上偏移量
        locationData.top+=iAH; 
        locationData.left+=iAW;
    }
    
    /***********************************************************************
    函数或过程名称: add(sCaption)
    输入参数: sCaption string 窗口标题
    输出参数: object 窗口对象
    功能描述: 增加一个新的窗口
    流程处理: 实例化一个窗口子类,设定它的属性,如果TaskBar未创建,创建它,如果
			  大小变换时使用的虚线框未创建,创建它
    全局变量: 
    调用方法: add(sCaption)
    调用样例: add("new window")
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	鲁小帅         修改时间: 2004-02-10
    修改原因:为TaskBar的按钮添加onDblClick事件，双击关闭对应窗口
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

   	function add(sCaption) {
		//创建一个新窗口
	    var oWnd=new WOCChildWnd(sCaption);
	    oWnd.create(oMDIFrame);
	    
		//设定处理窗口事件的处理函数
	    oWnd.onSetActive=setActive;
	    oWnd.onstartResize=startChildResize;
	    oWnd.onMinimize=onChildMinimize;
	    oWnd.onClose=onChildClose;
	    oWnd.onCaptionChange=onChildCaptionChange;
	    oWnd.onMaximize=onChildMaximize;
	    oWnd.onIcoChange=onChildIcoChange;
	    
	    //将窗口加入到窗口对象数组	    
        aChildWndList.push(oWnd); 
		//设定窗口的层索引
        oWnd.setZIndex(oWnd.getZIndex()+getChildNumber());
		//设定窗口的索引
        oWnd._index=getChildNumber();
        
        //setNewLocation(oWnd.getWidth(),oWnd.getHeight());
        
		//如果虚线框未创建,创建它
		if (oDashedDiv==null) createDashedDiv();
		//如果TaskBar未创建,创建它
		if (oTaskUp.childNodes.length==0 && oTaskDown.childNodes.length==0) {		    
		    oTaskBar=new WOCTaskBar();
		    oTaskBar.create(oTaskDown);
		    oTaskBar.onClick=function(iIndex) {
		        if (aChildWndList[iIndex].getState()=="minimized") aChildWndList[iIndex].windowMinimize();
		        setActive(aChildWndList[iIndex]);		        
		    };
		    oTaskBar.onDblClick=function(iIndex) {
		        aChildWndList[iIndex].close();
		    }
		}
		//在TaskBar中添加一个和窗口对应的按钮
		oTaskBar.addButton(oWnd.getWindowIco(),oWnd.getCaption(),getChildNumber());
		setActive(oWnd); //设定当前窗口为活动窗口
        return activeWnd; //返回这个窗口对象
	}
	
    /***********************************************************************
    函数或过程名称: closeAll()
    输入参数: 无
    输出参数: 无
    功能描述: 关闭所有窗口
    流程处理: 根据窗口数组，关闭所有窗口
    全局变量: 
    调用方法: closeAll()
    调用样例: closeAll()
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

	function closeAll() {
	    for (var i=0;i<=getChildNumber();++i) {
            //关闭窗口
	        onChildClose(i);
	    }
	}
	
    /***********************************************************************
    函数或过程名称: minimizeAll()
    输入参数: 无
    输出参数: 无
    功能描述: 最小化所有窗口
    流程处理: 
    全局变量: 
    调用方法: minimizeAll()
    调用样例: minimizeAll()
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

	function minimizeAll() {
	    for (var i=0;i<=getChildNumber();++i) {
            //如果窗口存在，最小化            
	        if (aChildWndList[i]!=null && aChildWndList[i].getState()!="minimized") onChildMinimize(aChildWndList[i]);
	    }
	}
	
    /***********************************************************************
    函数或过程名称: cascadeWindow()
    输入参数: 无
    输出参数: 无
    功能描述: 层叠窗口
    流程处理: 根据窗口层索引，层叠所有窗口
    全局变量: 
    调用方法: cascadeWindow()
    调用样例: cascadeWindow()
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

	function cascadeWindow() {
	    locationData={top:0,left:0};
	    var aWndList=new Array();
	    var j=0;
        //得到包含窗口索引和层索引的二维数组
	    for (var i=0;i<=getChildNumber();++i) {
	        if (aChildWndList[i]!=null) {	            
	            aWndList[j]=new Array();
	            aWndList[j][0]=i;
	            aWndList[j][1]=aChildWndList[i].getZIndex();
	            j++;
	        }
	    }
	    if (aWndList.length<=0) return; //没有窗口，返回
        //对数组排序
	    var aWndListSort=aWndList.sort(function listSort(a,b){return a[1]-b[1]});
	    
        //按排序结果设定窗口位置
	    for (var i=0;i<aWndListSort.length;++i) {
	        var k=aWndListSort[i][0];
	        setNewLocation(aChildWndList[k].getWidth(),aChildWndList[k].getHeight());
	        aChildWndList[k].setLocation(locationData.top,locationData.left);	        
	    }
	}
	
	/***********************************************************************
    函数或过程名称: setNextActiveWnd(iIndex)
    输入参数: iIindex integer 窗口索引
    输出参数: 无
    功能描述: 设置层与当前窗口最接近的窗口为活动窗口
    流程处理: 按照窗口层索引对除当前窗口外的所有窗口进行排序，得到和当前窗口
              层索引最接近的窗口，并设置为活动窗口
    全局变量: 
    调用方法: setNextActiveWnd(iIndex)
    调用样例: setNextActiveWnd(1)
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

    function setNextActiveWnd(iIndex) {
	    var aWndList=new Array();
	    var j=0;
        //得到包含窗口索引和层索引的二维数组
	    for (var i=0;i<=getChildNumber();++i) {
	        if (i!=iIndex && aChildWndList[i]!=null && aChildWndList[i].getState()!="minimized") {	            
	            aWndList[j]=new Array();
	            aWndList[j][0]=i;
	            aWndList[j][1]=aChildWndList[i].getZIndex();
	            j++;
	        }
	    }
        //如果没有窗口，直接返回,触发onCaptionChange事件        
	    if (aWndList.length<=0) {
	        setActive(null);	        
	        return;
	    }
        //对窗口排序
	    var aWndListSort=aWndList.sort(function listSort(a,b){return b[1]-a[1]});
        //设定层索引最大的窗口为活动窗口
	    setActive(aChildWndList[aWndListSort[0][0]]);
	}
	
	/***********************************************************************
    函数或过程名称: onFrameResize()
    输入参数: 无
    输出参数: 无
    功能描述: 处理Frame大小变化操作
    流程处理: 查询所有最大化窗口,调整到合适新的Frame大小
    全局变量: 
    调用方法: onFrameResize()
    调用样例: onFrameResize()
    创建日期: 2004-1-13
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/
    
	function onFrameResize() {
	    for (var i=0;i<=getChildNumber();++i) {
	        if (aChildWndList[i]!=null && aChildWndList[i].getState()=="maximized") {
	            aChildWndList[i].setBounds(0,0,getFrameWidth()+0,getFrameHeight()+0);
	        }
	    }
	}
	
	/***********************************************************************
    函数或过程名称: onChildMaximize(oWnd)
    输入参数: oWnd object 窗口对象
    输出参数: 无
    功能描述: 对指定窗口做最大化操作
    流程处理: 触发onMaximize事件,如果用户返回false,将标题栏隐藏
    全局变量: 
    调用方法: onChildMaximize(oWnd)
    调用样例: onChildMaximize(oWnd)
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

    function onChildMaximize(oWnd) {        
	    oWnd.windowMaximize();
	    if (_oThis.onMaximize!=null) {	        
	        if (!_oThis.onMaximize(oWnd)) {
	            if (oWnd.getState()=="maximized") {
	                oWnd.setBorder("0px");
	                oWnd.setShowCaption(false);	                
	            }else{
	                oWnd.setBorder("2px outset");
	                oWnd.setShowCaption(true);	                
	            }
	        }
	    }
	}
	
	/***********************************************************************
    函数或过程名称: onChildCaptionChange(iIndex)
    输入参数: iIndex integer 窗口索引
    输出参数: 无
    功能描述: 处理标题文字更改事件
    流程处理: 修改TaskBar上按钮文字,触发onCaptionChange事件
    全局变量: 
    调用方法: onChildCaptionChange(iIndex)
    调用样例: onChildCaptionChange(1)
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

    function onChildCaptionChange(iIndex) {
	    if (oTaskBar!=null) oTaskBar.setButtonTitle(iIndex,aChildWndList[iIndex].getCaption());
	    if (_oThis.onCaptionChange!=null) _oThis.onCaptionChange(aChildWndList[iIndex].getCaption());
	}
		
	/***********************************************************************
    函数或过程名称: onChildClose(iIndex)
    输入参数: iIndex integer 窗口索引
    输出参数: 无
    功能描述: 处理窗口关闭
    流程处理: 
    全局变量: 
    调用方法: onChildClose(iIndex)
    调用样例: onChildClose(1)
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

    function onChildClose(iIndex) {
        //指定索引的窗口不存在
	    if (aChildWndList[iIndex]==null) return;
        //用户定义了onClose事件处理函数
	    if (_oThis.onClose!=null) {
	        var oEvent=new Object();
            //提供给用户oEvent对象,包括窗口标题和窗口索引两个属性
	        oEvent.caption=aChildWndList[iIndex].getCaption();
	        oEvent.index=aChildWndList[iIndex]._index;
            //如果用户返回false,窗口不关闭
	        if (!_oThis.onClose(oEvent)) return;
	    }
        //设置其他窗口为活动窗口
	    if (aChildWndList[iIndex]==activeWnd) setNextActiveWnd(iIndex);
        //清除窗口内容
	    aChildWndList[iIndex].frameClear();
	    aChildWndList[iIndex].dispose();	    
        //删除窗口
	    aChildWndList[iIndex]=null;
	    delete aChildWndList[iIndex];
        //删除与窗口对应的按钮
	    if (oTaskBar!=null) oTaskBar.deleteButton(iIndex);
        //垃圾回收
	    CollectGarbage();
	}
	
	/***********************************************************************
    函数或过程名称: onChildMinimize(oWnd)
    输入参数: oWnd object 窗口对象
    输出参数: 无
    功能描述: 处理窗口最小化
    流程处理: 
    全局变量: 
    调用方法: onChildMinimize(oWnd)
    调用样例: onChildMinimize(oWnd)
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

    function onChildMinimize(oWnd) {
        //最小化窗口
	    oWnd.windowMinimize();
        //设置其他窗口为活动窗框口
	    if (activeWnd==oWnd) setNextActiveWnd(oWnd._index);
	    if (oTaskBar!=null) oTaskBar.setButtonState(oWnd._index,false);
	}
	
	/***********************************************************************
    函数或过程名称: onChildIcoChange(oWnd)
    输入参数: oWnd object 窗口对象
    输出参数: 无
    功能描述: 处理窗口更改图标事件
    流程处理: 
    全局变量: 
    调用方法: onChildIcoChange(oWnd)
    调用样例: onChildIcoChange()
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/
	
	function onChildIcoChange(oWnd) {
	    if (oTaskBar!=null) oTaskBar.setButtonIco(oWnd._index,oWnd.getWindowIco());
	}
	
	/***********************************************************************
    函数或过程名称: startChildResize()
    输入参数: 无
    输出参数: 无
    功能描述: 开始处理窗口缩放操作
    流程处理: 记录当前窗口的位置和大小,显示虚线框,给容器增加鼠标事件,用户处理
              鼠标拖动操作
    全局变量: 
    调用方法: startChildResize()
    调用样例: startChildResize()
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

    function startChildResize() {
        //设定虚线框的大小和位置
	    oDashedDiv.setBounds(activeWnd.getLeft(),activeWnd.getTop(),activeWnd.getWidth(),activeWnd.getHeight());
        //显示
	    oDashedDiv.style.display="";
        //记录要调整的方向
	    oDashedDiv.resizeDir=activeWnd.getResizeDir();
        //记录窗口最小宽度和高度
	    oDashedDiv.MinimumWidth=activeWnd.getMinimumWidth();
	    oDashedDiv.MinimumHeight=activeWnd.getMinimumHeight();
        //记录原始状态
	    oDashedDiv.resizeData={screenX:event.clientX,screenY:event.clientY,startLeft:oDashedDiv.style.pixelLeft,startTop:oDashedDiv.style.pixelTop,startWidth:oDashedDiv.offsetWidth,startHeight:oDashedDiv.offsetHeight};
	    startResized=true;
        //增加鼠标事件
	    oMDIFrame.attachEvent("onmousemove",continueChildResize);
	    oMDIFrame.attachEvent("onmouseup",endChildResize);
	    oMDIFrame.style.cursor=activeWnd.getResizeDir()+"-resize";
	    oMDIFrame.setCapture();
	}
	
	/***********************************************************************
    函数或过程名称: continueChildResize()
    输入参数: 无
    输出参数: 无
    功能描述: 调整虚线框大小和位置
    流程处理: 根据鼠标当前位置和历史位置,计算偏移量,根据值定位虚线框的大小和位置
    全局变量: 
    调用方法: continueChildResize()
    调用样例: continueChildResize()
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

    function continueChildResize() {
	    var dir=oDashedDiv.resizeDir;
	    var i=0;
	    var iW;
	    var iH;
	    var iT;
	    var iL;
	    var eX=event.clientX;
	    var eY=event.clientY;
        //向右调整宽度
	    if(/e/i.test(dir)){
		    //width
		    iW=eX-oDashedDiv.resizeData.screenX;
		    iW=oDashedDiv.resizeData.startWidth+iW;
		    iW=iW<oDashedDiv.MinimumWidth?oDashedDiv.MinimumWidth:iW;
            //判断是否拖出容器边界
		    iW=iW+oDashedDiv.resizeData.startLeft>getWidth()?getWidth()-oDashedDiv.resizeData.startLeft-4:iW;
		    oDashedDiv.setWidth(iW);            
	    }else if(/w/i.test(dir)){ //向左调整宽度
		    //width
		    iW=eX-oDashedDiv.resizeData.screenX;
		    iW=oDashedDiv.resizeData.startWidth-iW;
		    iW=iW<oDashedDiv.MinimumWidth?oDashedDiv.MinimumWidth:iW;
		    //left
            //判断是否拖出容器边界
		    iL=oDashedDiv.resizeData.startLeft-(iW-oDashedDiv.resizeData.startWidth);
		    if (iL<=0) {
		        i=iL;
		        iL=0;
		    }
            //设置宽度
		    oDashedDiv.setWidth(iW+i);
            //设置左外边距
		    oDashedDiv.setLeft(iL);
	    }
	    i=0;		  
        //向下调整高度
	    if(/s/i.test(dir)){
		    //height
		    iH=eY-oDashedDiv.resizeData.screenY;
		    iH=oDashedDiv.resizeData.startHeight+iH;
		    iH=iH<oDashedDiv.MinimumHeight?oDashedDiv.MinimumHeight:iH;
            //判断是否拖出容器边界
		    iH=iH+oDashedDiv.resizeData.startTop>getHeight()?getHeight()-oDashedDiv.resizeData.startTop-4:iH;
		    oDashedDiv.setHeight(iH);
	    }else if(/n/i.test(dir)){ //向上调整高度
		    //height
		    iH=eY-oDashedDiv.resizeData.screenY;
		    iH=oDashedDiv.resizeData.startHeight-iH;
		    iH=iH<oDashedDiv.MinimumHeight?oDashedDiv.MinimumHeight:iH;		    
		    //top
            //判断是否拖出容器边界
		    iT=oDashedDiv.resizeData.startTop-(iH-oDashedDiv.resizeData.startHeight);
		    if (iT<=0) {
		        i=iT;
		        iT=0;
		    }
            //设定高度
		    oDashedDiv.setHeight(iH+i);
            //设定上外边距
		    oDashedDiv.setTop(iT);
	    }
	}
	
	/***********************************************************************
    函数或过程名称: endChildResize()
    输入参数: 无
    输出参数: 无
    功能描述: 结束大小调整
    流程处理: 鼠标放开,根据虚线框的大小和位置调整窗口
    全局变量: 
    调用方法: endChildResize()
    调用样例: endChildResize()
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

    function endChildResize() {
	    startResized=false;
        //清除鼠标事件
	    oMDIFrame.detachEvent("onmousemove",continueChildResize);
	    oMDIFrame.detachEvent("onmouseup",endChildResize);
        //设定窗口范围和虚线框范围相同
	    activeWnd.setBounds(oDashedDiv.getLeft(),oDashedDiv.getTop(),oDashedDiv.getWidth(),oDashedDiv.getHeight());
        //隐藏虚线框
	    oDashedDiv.style.display="none";
        //还原鼠标状态
	    oMDIFrame.style.cursor="default";
	    oMDIFrame.releaseCapture();
	}
    
  
    /***********************************************************************
    函数或过程名称: getFrameWidth()
    输入参数: 无
    输出参数: integer 宽度
    功能描述: 返回FRAME容器宽度
    流程处理: 
    全局变量: 
    调用方法: getFrameWidth()
    调用样例: getFrameWidth()
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

    function getFrameWidth() {
		if (oMDIFrame==null) return;
		return parseInt(oMDIFrame.offsetWidth,10);
	}

	/***********************************************************************
    函数或过程名称: setFrameWidth(iW)
    输入参数: iW integer 宽度
    输出参数: 无
    功能描述: 设定Frame容器宽度
    流程处理: 
    全局变量: 
    调用方法: setFrameWidth(iW)
    调用样例: setFrameWidth(500)
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

    function setFrameWidth(iW) {
		if (oMDIFrame==null) return;
		oMDIFrame.style.width=iW;
	}

	/***********************************************************************
    函数或过程名称: getFrameHeight()
    输入参数: 无
    输出参数: integer 高度
    功能描述: 返回Frame容器高度
    流程处理: 
    全局变量: 
    调用方法: getFrameHeight()
    调用样例: getFrameHeight()
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

    function getFrameHeight() {
		if (oMDIFrame==null) return;
		return parseInt(oMDIFrame.offsetHeight,10);
	}

	/***********************************************************************
    函数或过程名称: setFrameHeight(iH)
    输入参数: iH
    输出参数: 无
    功能描述: 设定Frame容器高度
    流程处理: 
    全局变量: 
    调用方法: setFrameHeight(iH)
    调用样例: setFrameHeight(500)
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

    function setFrameHeight(iH) {
		if (oMDIFrame==null) return;
		oMDIFrame.style.height=iH;
	}
	
    
	/***********************************************************************
    函数或过程名称: getWidth()
    输入参数: 无
    输出参数: integer 主框架宽度
    功能描述: 返回框架宽度
    流程处理: 
    全局变量: 
    调用方法: getWidth()
    调用样例: getWidth()
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

    function getWidth() {
		if (oFrameCon==null) return;
		return parseInt(oFrameCon.offsetWidth,10);
	}

	/***********************************************************************
    函数或过程名称: setWidth(iW)
    输入参数: iW integer 宽度
    输出参数: 无
    功能描述: 设定主框架宽度
    流程处理: 
    全局变量: 
    调用方法: setWidth(iW)
    调用样例: setWidth(500)
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

    function setWidth(iW) {
		if (oFrameCon==null) return;
		oFrameCon.style.width=iW;
	}

	/***********************************************************************
    函数或过程名称: getHeight()
    输入参数: 无
    输出参数: integer 主框架高度
    功能描述: 返回主框架高度
    流程处理: 
    全局变量: 
    调用方法: getHeight()
    调用样例: getHeight()
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

    function getHeight() {
		if (oFrameCon==null) return;
		return parseInt(oFrameCon.offsetHeight,10);
	}

	/***********************************************************************
    函数或过程名称: setHeight(iH)
    输入参数: iH integer 高度
    输出参数: 无
    功能描述: 设置主要框架高度
    流程处理: 
    全局变量: 
    调用方法: setHeight(iH)
    调用样例: setHeight(500)
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

    function setHeight(iH) {
		if (oFrameCon==null) return;
		oFrameCon.style.height=iH;
	}

	/***********************************************************************
    函数或过程名称: setSize(iW,iH)
    输入参数: iW integer 宽度 iH integer 高度
    输出参数: 无
    功能描述: 设定主框架的大小
    流程处理: 
    全局变量: 
    调用方法: setSize(iW,iH)
    调用样例: setSize(500,500)
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

    function setSize(iW,iH) {
		setWidth(iW);
		setHeight(iH);		
	}
    
    //初始化
	initializtion();
		
	/***********************************************************************
    函数或过程名称: WOCChildWnd(sCaption)
    输入参数: sCaption string 窗口标题
    输出参数: 无
    功能描述: 窗口子类,用于生成窗口界面和事件接口
    流程处理: 
    全局变量: 
    调用方法: WOCChildWnd(sCaption)
    调用样例: new WOCChildWnd("new window")
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

    function WOCChildWnd(sCaption) {
        this.hcon=null;
        this._className="WOCChildWnd";
        this._index=null;
        var _oThisChild=this;
        
        var oWindow=null;                   //窗口
        var oWindowIco=null;				//窗口图标
        var oWindowCaption=null;			//窗口标题栏
        var oMinimizeButton=null;			//最小化按钮
        var oMaximizeButton=null;           //最大化按钮
        var oCloseButton=null;				//关闭按钮
        var oCaptionTitle=null;				//标题栏文字
        var oContentPane=null;				//窗口提供的容器
        var oClientSiteFrame=null;          
        
        var sImgPath="images/CXWOCWindow/";
        
        var oFunNoSelectA=function(){return false;};
        var oFunNoSelectB=function(){if(event.srcElement.tagName!="INPUT" && event.srcElement.tagName!="AREA") return false;};
        
        //方法,事件列表,含义说明见各个部分代码上部注释
        this.create=create;
        this.createFrame=createFrame;
        this.dispose=dispose;
        this.setCaption=setCaption;
        this.getCaption=getCaption;        
    
        this.getWindow=getWindow;
	    this.getWindowIco=getWindowIco;
	    this.setWindowIco=setWindowIco;	    
	    this.getActive=getActive;
	    this.setActive=setActive;
	    this.getZIndex=getZIndex;
	    this.getOriginZIndex=getOriginZIndex;
	    this.setZIndex=setZIndex;
	    
	    this.getShowMinimize=getShowMinimize;
	    this.setShowMinimize=setShowMinimize;
	    this.getShowMaximize=getShowMaximize;
	    this.setShowMaximize=setShowMaximize;
	    this.getShowClose=getShowClose;
	    this.setShowClose=setShowClose;
	    this.getShowIco=getShowIco;
	    this.setShowIco=setShowIco;
	    this.getShowCaption=getShowCaption;
	    this.setShowCaption=setShowCaption;
	    
	    this.getCanMinimize=getCanMinimize;
	    this.setCanMinimize=setCanMinimize;	    
	    this.getResizable=getResizable;
	    this.setResizable=setResizable;
	    
	    this.getResizeDir=getResizeDir;
	    
		this.getState=getState;  //返回当前窗口的状态normal maximized minimized
		
		this.getMinimumWidth=getMinimumWidth;
		this.getMinimumHeight=getMinimumHeight;
	    this.getTop=getTop;
        this.getLeft=getLeft;
        this.getWidth=getWidth;
        this.getHeight=getHeight;
	    this.setTop=setTop;
        this.setLeft=setLeft;
        this.setWidth=setWidth;
        this.setHeight=setHeight;
	    this.setSize=setSize;
		this.setLocation=setLocation;		    
        this.setBounds=setBounds;
        this.getBorderWidth=getBorderWidth;
        this.setBorder=setBorder;
        this.setBgColor=setBgColor;
        
        
	    this.getClientSite=getClientSite;
	    this.addHTML=addHTML;
	    this.add=add;
	    this.setUrl=setUrl;
	    this.getUrl=getUrl;
        
        this.setMinimize=onMinimizeButtonClick;
        this.setMaximize=onMaximizeButtonClick;
        this.close=onCloseButtonClick;
        this.refresh=refresh;
        
        this.windowMinimize=windowMinimize;
        this.windowMaximize=windowMaximize;
        
        this.frameClear=frameClear;
        
	    this.onSetActive=null;       //获得焦点事件
	    this.onstartResize=null;     //大小变化事件
	    this.onMinimize=null;        //最小化事件 
	    this.onMaximize=null;        //最大化事件
	    this.onClose=null;           //关闭事件
	    this.onCaptionChange=null;   //更改标题文字事件
	    this.onIcoChange=null;
	    
   
        /***********************************************************************
        函数或过程名称: getButtonSpan(sSpanText)
        输入参数: sSpanText string 按钮文本
        输出参数: 无
        功能描述: 返回一个按钮文本
        流程处理: 
        全局变量: 
        调用方法: getButtonSpan(sSpanText)
        调用样例: getButtonSpan("0")
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
    
        function getButtonSpan(sSpanText) {
            return "<span style='position:relative;top:-1px'>" + sSpanText +"</span>";
        }
      
        /***********************************************************************
        函数或过程名称: getCaptionButton(sBtnType)
        输入参数: sBrnType string 按钮类型
        输出参数: object 按钮对象
        功能描述: 返回一个指定类型的按钮
        流程处理: 
        全局变量: 
        调用方法: getCaptionButton(sBtnType)
        调用样例: getCaptionButton("0")
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function getCaptionButton(sBtnType) {
            //创建一个BUTTON
            var oBtn=document.createElement("button");
            oBtn.attachEvent("onfocus",oBtn.blur);            
            with(oBtn.style) {                
                fontFamily="Marlett";
	            fontWeight="normal";
	            fontSize="10px";
	            textAlign="center";
	            width="16px";
	            height="14px";
	            overflow="hidden";
	            padding="0";
	            backgroundColor=captionButtonColor;
        	    cursor="default";
            }            
            //设置内容
            oBtn.innerHTML=getButtonSpan(sBtnType);
            return oBtn;        
        }	
		

        /***********************************************************************
        函数或过程名称: createCaptionTitle()
        输入参数: 无
        输出参数: 无
        功能描述: 创建标题栏文本框
        流程处理: 
        全局变量: 
        调用方法: createCaptionTitle()
        调用样例: createCaptionTitle()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function createCaptionTitle() {
            var oDiv=null;
            //创建文本框
            oDiv=document.createElement("div");
            with (oDiv.style) {
	            oDiv.style.width="100%";
	            oDiv.style.height="18px";
	            oDiv.style.overflow="hidden";
	            oDiv.style.textOverflow="ellipsis";
	            oDiv.style.fontWeight="bold";
	            oDiv.style.whiteSpace="nowrap";
	            oDiv.style.padding="2px";
	            oDiv.style.font="Caption";
	            oDiv.style.color=activeCaptionTextColor;
	        }            
    	    oCaptionTitle=oDiv;	    
        }
        
        //返回标题栏文本宽度
    	function getTitleWidth() {
	        if (oCaptionTitle) return oCaptionTitle.offsetWidth;
	    }
	        
        //返回标题栏文本高度
	    function getTitleHeight() {
	        if (oCaptionTitle) return oCaptionTitle.offsetHeight;
	    }
	       
        //设置标题栏文本宽度
	    function setTitleWidth(iW) {
	        if (oCaptionTitle) oCaptionTitle.style.width=iW;
	    }
	        
        //设置标题栏文本高度
	    function setTitleHeight(iH) {
	        if (oCaptionTitle) oCaptionTitle.style.height=iH;
	    }   	    
    
        /***********************************************************************
        函数或过程名称: createWindowIco(sImgName)
        输入参数: sImgName string 图片路径和名称
        输出参数: 无
        功能描述: 创建一个窗口图标
        流程处理: 
        全局变量: 
        调用方法: createWindowIco(sImgName)
        调用样例: createWindowIco("ico16.gif")
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function createWindowIco(sImgName) {
            //创建一个IMG对象
            oWindowIco=document.createElement("img");
            //设定强制高度和宽度
            oWindowIco.style.width=16;
            oWindowIco.style.height=16;
            oWindowIco.style.display="none";
            //图片路径名称
	        if (sImgName!=null && sImgName.length>0) oWindowIco.src=sImgName;
            //双击事件
	        oWindowIco.attachEvent("ondblclick",onCloseButtonClick);	        
        }
        
        /***********************************************************************
        函数或过程名称: getWindowIco()
        输入参数: 无
        输出参数: string 窗口图标
        功能描述: 返回窗口图标的文件名
        流程处理: 
        全局变量: 
        调用方法: getWindowIco()
        调用样例: getWindowIco()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function getWindowIco() {
	            return oWindowIco.src;
	    }
	    
	    /***********************************************************************
        函数或过程名称: setWindowIco(sImgN)
        输入参数: sImgN string 图标文件名
        输出参数: 无
        功能描述: 设置窗口图标
        流程处理: 
        全局变量: 
        调用方法: setWindowIco(sImgN)
        调用样例: setWindowIco("ICO16.gif")
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setWindowIco(sImgN) {
	            oWindowIco.src=sImgN;
	            if (!getShowIco()) setShowIco(true);
	            if (_oThisChild.onIcoChange!=null) _oThisChild.onIcoChange(_oThisChild);
	    }
	    
        /***********************************************************************
        函数或过程名称: createCaption(oHandleFor)
        输入参数: oHandLeFor object 标题所属容器
        输出参数: 无
        功能描述: 创建一个标题栏
        流程处理: 
        全局变量: 
        调用方法: createCaption(oHandleFor)
        调用样例: createCaption(oWindow)
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function createCaption(oHandleFor) {
            //创建标题栏主框架
            var oDiv=document.createElement("div");
	        oDiv.style.width="100%";	    
	        oDiv.style.backgroundColor=activeCaptionColor;
	        oDiv.style.margin="2 2 2 2";	    	    
            //禁止选择
	        oDiv.attachEvent("onselectstart",oFunNoSelectA);
	        
            //创建窗口图标
	        createWindowIco();	    
            //创建标题文字
	        createCaptionTitle();	    
	        
            //创建最小化按钮
	        oMinimizeButton=getCaptionButton("0");
            //创建最大化按钮
            oMaximizeButton=getCaptionButton("1");     
            //创建关闭按钮
            oCloseButton=getCaptionButton("r");
            oCloseButton.style.marginLeft=2;
	        oCloseButton.style.marginRight=2;
			
            //按钮添加事件
			oMinimizeButton.attachEvent("onclick",onMinimizeButtonClick);
			oMaximizeButton.attachEvent("onclick",onMaximizeButtonClick);
			oCloseButton.attachEvent("onclick",onCloseButtonClick);
	        
            //创建一个一行三列的表格,第一列存放图标,第二列存放标题文字,第三列存放按钮
    	    var oTable=document.createElement("table");
	        oTable.style.width="100%";
    	    oTable.style.height="18px";
	        oTable.style.fontSize=sFontSize;	    
	        oTable.border=0;
	        oTable.cellPadding=0;
	        oTable.cellSpacing=0;
	        var oTr1=oTable.insertRow();
	        var oTd1=oTr1.insertCell();
	        oTd1.style.paddingLeft="2";
            //添加图标到第一列
	        oTd1.insertAdjacentElement("beforeEnd",oWindowIco);
	        var oTd2=oTr1.insertCell();
	        oTd2.style.width="100%";
	        //oTd2.style.verticalAlign="baseline";
	        oTd2.style.cursor="default";
            //添加标题文字到第二列
	        oTd2.insertAdjacentElement("beforeEnd",oCaptionTitle);
            //添加事件
	        oTd2.attachEvent("onmousedown",moveStart);	        
			oTd2.attachEvent("ondblclick",onMaximizeButtonClick);
			//
	        var oTd3=oTr1.insertCell();	        
	        oTd3.noWrap=true;
            //添加按钮到第三列
	        oTd3.insertAdjacentElement("beforeEnd",oMinimizeButton);
            oTd3.insertAdjacentElement("beforeEnd",oMaximizeButton);
            oTd3.insertAdjacentElement("beforeEnd",oCloseButton);
            oDiv.insertAdjacentElement("beforeEnd",oTable);
   	        oWindowCaption=oDiv;
   	        oWindowCaption.MoveHandle=oTd2;
        }
    
        /***********************************************************************
        函数或过程名称: getCaption()
        输入参数: 无
        输出参数: string 返回标题栏文字
        功能描述: 
        流程处理: 
        全局变量: 
        调用方法: getCaption()
        调用样例: getCaption()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function getCaption() {
            if(oCaptionTitle)
                return oCaptionTitle.innerText;
            else
                return "";
        }
    
        /***********************************************************************
        函数或过程名称: setCaption(sCaption)
        输入参数: sCaption string 标题栏文字
        输出参数: 无
        功能描述: 设定标题栏文字
        流程处理: 
        全局变量: 
        调用方法: setCaption(sCaption)
        调用样例: setCaption("new window")
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setCaption(sCaption) {
            if (oCaptionTitle) oCaptionTitle.innerText=sCaption;
            //触发onCaptionChange事件
            if (_oThisChild.onCaptionChange!=null) _oThisChild.onCaptionChange(_oThisChild._index);
        }    
    
        /***********************************************************************
        函数或过程名称: getClientSite()
        输入参数: 无
        输出参数: object 用户使用区域
        功能描述: 返回窗口的用户可用区域
        流程处理: 
        全局变量: 
        调用方法: getClientSite()
        调用样例: getClientSite()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function getClientSite() {            
            return oContentPane;
        }
        
        /***********************************************************************
        函数或过程名称: addHTML(sH)
        输入参数: sH string HTML代码
        输出参数: 无
        功能描述: 向窗口用户区域添加HTML代码
        流程处理: 
        全局变量: 
        调用方法: addHTML(sH)
        调用样例: addHTML("<div></div>")
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function addHTML(sH) {            
            getClientSite().insertAdjacentHTML("beforeEnd",sH);
        }
        
        /***********************************************************************
        函数或过程名称: add(oChildren)
        输入参数: oChildren object 对象
        输出参数: 无
        功能描述: 向窗口用户可用区域添加对象
        流程处理: 
        全局变量: 
        调用方法: add(oChildren)
        调用样例: add(oDiv)
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function add(oChildren) {
            getClientSite().insertAdjacentElement("beforeEnd",oChildren);
        }
        
        /***********************************************************************
        函数或过程名称: getUrl()
        输入参数: 无
        输出参数: string 页面的地址
        功能描述: 返回窗口当前页面的地址
        流程处理: 
        全局变量: 
        调用方法: getUrl()
        调用样例: getUrl()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function getUrl() {
            if (oClientSiteFrame!=null) return oClientSiteFrame.src;            
        }
        
        /***********************************************************************
        函数或过程名称: setUrl(sUrl)
        输入参数: sUrl string 页面地址
        输出参数: 无
        功能描述: 设定窗口当前页面的地址
        流程处理: 
        全局变量: 
        调用方法: setUrl(sUrl)
        调用样例: setUrl("../index.asp")
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setUrl(sUrl) {            
            if (oClientSiteFrame!=null) oClientSiteFrame.src=sUrl;
        }
        
        /***********************************************************************
        函数或过程名称: refresh()
        输入参数: 无
        输出参数: 无
        功能描述: 刷新窗口
        流程处理: 
        全局变量: 
        调用方法: refresh()
        调用样例: refresh()
        创建日期: 2004-2-11
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function refresh() {            
            if (oClientSiteFrame!=null) {
                var oFWnd=oClientSiteFrame.contentWindow;
                //oFWnd.location.reload(true);
                oFWnd.location.replace(getUrl());
                //setUrl(getUrl());
            }
        }
        
        /***********************************************************************
        函数或过程名称: getBorderWidth()
        输入参数: 无
        输出参数: integer 窗口边框的宽度
        功能描述: 返回窗口的边框宽度
        流程处理: 
        全局变量: 
        调用方法: getBorderWidth()
        调用样例: getBorderWidth()
        创建日期: 2004-02-19
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function getBorderWidth() {
            return parseInt(getWindow().style.border,10);
        }
        
        /***********************************************************************
        函数或过程名称: setBorder(sWidth,sStyle,sColor)
        输入参数: sWidth string 边框宽度 medium,thin,thick,width
                  sStyle string 边框式样 none,dotted,dashed,solid,double,groove,ridge,inset,outset
                  sColor string 边框颜色
        输出参数: 无
        功能描述: 设置窗口的边框
        流程处理: 
        全局变量: 
        调用方法: setBorder()
        调用样例: setBorder("2px outset red")
        创建日期: 2004-02-19
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setBorder(sWidth,sStyle,sColor) {
            var sBorder=""+sWidth;
            if (sStyle) sBorder+=" "+sStyle;
            if (sColor) sBorder+=" "+sColor;            
            getWindow().style.border=sBorder;
        }
        
        /***********************************************************************
        函数或过程名称: setBgColor(sColor)
        输入参数: sColor string 窗口背景色
        输出参数: 无
        功能描述: 设置窗口的边框宽度
        流程处理: 
        全局变量: 
        调用方法: setBgColor()
        调用样例: setBgColor("#080808")
        创建日期: 2004-02-19
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setBgColor(sColor) {
            oContentPane.style.backgroundColor=sColor;
        }
        
        /***********************************************************************
        函数或过程名称: getResizeDir()
        输入参数: 无
        输出参数: string 窗口调整大小的方向
        功能描述: 返回窗口调整大小的方向
        流程处理: 
        全局变量: 
        调用方法: getResizeDir()
        调用样例: getResizeDir()
        创建日期: 2003-12-23
        作者: 鲁小帅
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function getResizeDir() {
            return oWindow.resizeDir;
        }
    
        /***********************************************************************
        函数或过程名称: createWindow(sCaption)
        输入参数: sCaption string 窗口标题
        输出参数: 无
        功能描述: 创建一个窗口
        流程处理: 
        全局变量: 
        调用方法: createWindow(sCaption)
        调用样例: createWindow("new window")
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function createWindow(sCaption) {
            //窗口主框架为一个DIV
            oWindow=document.createElement('<div unselectable="on">');
            oWindow.moveData={};      //移动历史记录
            oWindow.resizeData={};    //缩放历史记录
            oWindow.maxBoundsData={}; //最大化恢复记录
            oWindow.minBoundsData={}; //最小化恢复记录
            oWindow.resizable=true;
            oWindow.active=false;
			oWindow.state="normal";
			oWindow.resizeDir="";
            oWindow.MinimumWidth=120; //最小宽度
            oWindow.MinimumHeight=26; //最小高度
            //添加鼠标事件
            oWindow.attachEvent("onmousemove",checkForResize);
            oWindow.attachEvent("onmousedown",onWindowClick);
            oWindow.attachEvent("onresize",setChildrenSize);
            //oWindow.attachEvent("onblur",setActive);
            //设置窗口的式样
            with(oWindow.style) {
                position="absolute";
	            overflow="hidden";
	            backgroundColor=windowBgColor;
	            fontSize=sFontSize;
	            border="2px outset";	            
	            top=0;
	            left=0;
	            width="400px";
	            height="200px";
	            zIndex="10000";
            }
            var oBackFrm=document.createElement("iframe");
            oBackFrm.src="JavaScript:false";
            with(oBackFrm.style) {
                position="absolute";
                visibility="inherit";
                top="0px";
                left="0px";
                width="100%";
                height="100%";
                zIndex=-1;
                filter='progid:DXImageTransform.Microsoft.Alpha(style=0,opacity=0)';
            }
            oWindow.insertAdjacentElement("afterBegin",oBackFrm);
            //创建标题栏
            createCaption(oWindow);
            //将标题栏添加到窗口中
            oWindow.insertAdjacentElement("afterBegin",oWindowCaption);
            //创建用户可用区域
            oContentPane=document.createElement('div');
            //禁止用户选择
            oContentPane.attachEvent("onselectstart",oFunNoSelectB);
            //用户可用区域式样设置
            with(oContentPane.style) {
                //border="1px solid #ff0000";
                position="relative";
	            overflow="hidden";
	            fontSize=sFontSize;
	            border="0px";
	            width="100%";
	            margin="2 2 2 2";
            }
            oContentPane.attachEvent("onfocus",oContentPane.blur);
            oWindow.insertAdjacentElement("beforeEnd",oContentPane);                    
            //设置标题栏文字
            setCaption(sCaption);        
        }
        
        /***********************************************************************
        函数或过程名称: createFrame(sUrl)
        输入参数: sUrl string 页面地址
        输出参数: 无
        功能描述: 在用户可用区域创建一个IFRAME,并设定初始页面
        流程处理: 
        全局变量: 
        调用方法: createFrame(sUrl)
        调用样例: createFrame("../index.asp")
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function createFrame(sUrl) {
            if (oClientSiteFrame==null) {
                //创建iFrame
                oClientSiteFrame=document.createElement("<iframe frameborder=\"0\" onresize=\"event.cancelBubble=true;\">");
                oClientSiteFrame.attachEvent("onfocus",onWindowClick);                
                with(oClientSiteFrame.style) {
	                width="100%";
	                height="100%";
                }
                //如果有地址参数,设置初始化页面
                if (typeof sUrl=="string" && sUrl.length>0) oClientSiteFrame.src=sUrl;
                //添加到容器中
                oContentPane.insertAdjacentElement("beforeEnd",oClientSiteFrame);                
            }
        }
    
        /***********************************************************************
        函数或过程名称: create(oContainer)
        输入参数: oContainer object 要创建到的容器
        输出参数: 无
        功能描述: 将自身创建到指定位置
        流程处理: 
        全局变量: 
        调用方法: create(oContainer)
        调用样例: create(oContainer)
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function create(oContainer) {
            //没有创建过
		    if (!_oThisChild.hcon) {
                //对象不是个容器
    			if (!oContainer.canHaveChildren) {				
                    throw "创建失败，create()方法的参数不是一个容器的id！";
		    		return;
			    } else {
                    oContainer.insertAdjacentElement("beforeEnd",oWindow);                
                    _oThisChild.hcon = oContainer;
			    }
		    }		    
        }
        
        /***********************************************************************
        函数或过程名称: frameClear()
        输入参数: 无
        输出参数: 无
        功能描述:清除IFrame的页面 
        流程处理: 
        全局变量: 
        调用方法: frameClear()
        调用样例: frameClear()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function frameClear() {
            if (oClientSiteFrame!=null) {
                oClientSiteFrame.src="about:blank";
            }
        }
        
	    /***********************************************************************
        函数或过程名称: getWindow()
        输入参数: 无
        输出参数: object 窗口
        功能描述: 返回窗口句柄
        流程处理: 
        全局变量: 
        调用方法: getWindow()
        调用样例: getWindow()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function getWindow() {
		    if (oWindow!=null)
    			return oWindow;
	    	else
		    	return null;
	    }
    
        /***********************************************************************
        函数或过程名称: getMinimumWidth()
        输入参数: 无
        输出参数: integer 宽度
        功能描述: 返回窗口的最小宽度限制
        流程处理: 
        全局变量: 
        调用方法: getMinimumWidth()
        调用样例: getMinimumWidth()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function getMinimumWidth() {
            if (oWindow!=null)
                return oWindow.MinimumWidth;
            else
                return null;
        }
    
        /***********************************************************************
        函数或过程名称: getMinimumHeight()
        输入参数: 无
        输出参数: integer 高度
        功能描述: 返回窗口的最小高度限制
        流程处理: 
        全局变量: 
        调用方法: getMinimumHeight()
        调用样例: getMinimumHeight()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function getMinimumHeight() {
            if (oWindow!=null)
                return oWindow.MinimumHeight;
            else
                return null;
        }
        
		/***********************************************************************
        函数或过程名称: getState()
        输入参数: 无
        输出参数: string normal maximized minimized
        功能描述: 返回窗口状态
        流程处理: 
        全局变量: 
        调用方法: getState()
        调用样例: getState()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function getState() {
			return oWindow.state;
		}

		/***********************************************************************
        函数或过程名称: setState(sState)
        输入参数: sState string normal maximized minimized
        输出参数: 无
        功能描述: 设置窗口状态
        流程处理: 
        全局变量: 
        调用方法: setState(sState)
        调用样例: setState("normal")
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setState(sState) {
			oWindow.state=sState;
		}

        /***********************************************************************
        函数或过程名称: onCloseButtonClick()
        输入参数: 无
        输出参数: 无
        功能描述: 处理关闭按钮点击事件
        流程处理: 如果上级容器定义了事件接收,直接传递,没有的话删除自身
        全局变量: 
        调用方法: onCloseButtonClick()
        调用样例: onCloseButtonClick()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function onCloseButtonClick() {
            if (_oThisChild.onClose!=null) 
                _oThisChild.onClose(_oThisChild._index); //向上传递
            else
	            oWindow.removeNode(true); //删除自身
        }
        
        /***********************************************************************
        函数或过程名称: windowMinimize()
        输入参数: 无
        输出参数: 无
        功能描述: 窗口最小化操作
        流程处理: 
        全局变量: 
        调用方法: windowMinimize()
        调用样例: windowMinimize()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function windowMinimize() {
            //如果是最小话状态
            if(getState()=="minimized") {
                oWindow.style.display="block"; //显示窗口
			    setState(oWindow.minBoundsData.state); //恢复状态值
			    oMinimizeButton.innerHTML=getButtonSpan(0); //恢复按钮
                //如果立即记录是普通模式,更改最大化按钮状态
			    if (oWindow.minBoundsData.state=="normal") 
			        oMaximizeButton.innerHTML=getButtonSpan(1);
			    else
			        oMaximizeButton.innerHTML=getButtonSpan(2);
                //设置窗口范围为恢复记录值
			    setBounds(oWindow.minBoundsData.left,oWindow.minBoundsData.top,oWindow.minBoundsData.width,oWindow.minBoundsData.height);			        
			}else{
                //记录窗口当前范围到恢复记录
			    oWindow.minBoundsData={left:getLeft(),top:getTop(),width:getWidth(),height:getHeight(),state:getState()};				    
                //设置按钮状态
			    oMinimizeButton.innerHTML=getButtonSpan(2);
			    oMaximizeButton.innerHTML=getButtonSpan(1);
			    setState("minimized");
			    //setBounds(oWindow.BoundsData.left,oWindow.BoundsData.top,oWindow.BoundsData.width,oWindow.BoundsData.height);
			    //setSize(getMinimumWidth(),getMinimumHeight());
                //隐藏窗口
			    oWindow.style.display="none";
			}
        }
        
        /***********************************************************************
        函数或过程名称: onMinimizeButtonClick()
        输入参数: 无
        输出参数: 无
        功能描述: 处理最小化按钮点击事件
        流程处理: 如果窗口允许最小化,上级容器定义了事件接收,向上传递,否则自己处理
        全局变量: 
        调用方法: onMinimizeButtonClick()
        调用样例: onMinimizeButtonClick()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function onMinimizeButtonClick() {
            //允许最小化
	        if(getCanMinimize()) {
	            if (_oThisChild.onMinimize!=null) {
	                _oThisChild.onMinimize(_oThisChild); //向上传递
	            }else{
				    windowMinimize(); //自行处理最小化
				}
			}
        }
        
        /***********************************************************************
        函数或过程名称: windowMaximize()
        输入参数: 无
        输出参数: 无
        功能描述: 窗口最大化操作
        流程处理: 
        全局变量: 
        调用方法: windowMaximize()
        调用样例: windowMaximize()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function windowMaximize() {
            //判断当前窗口状态
            switch (getState()) {
	            case "minimized": //最小化
                    //设定按钮状态
	                oMinimizeButton.innerHTML=getButtonSpan(0);
                    //最大化恢复记录值使用最小化恢复记录值
	                oWindow.maxBoundsData.left=oWindow.minBoundsData.left;
	                oWindow.maxBoundsData.top=oWindow.minBoundsData.top;
	                oWindow.maxBoundsData.width=oWindow.minBoundsData.width;
	                oWindow.maxBoundsData.height=oWindow.minBoundsData.height;
                    //设定最大化按钮
					oMaximizeButton.innerHTML=getButtonSpan(2);
                    //设定窗口范围
					//setBounds(-4,-4,_oThis.getFrameWidth()+8,_oThis.getFrameHeight()+8);
					setBounds(0,0,_oThis.getFrameWidth(),_oThis.getFrameHeight());
					setState("maximized");
					break;
	            case "normal":	  //普通模式
                    //记录当前窗口状态为恢复记录值
	                oWindow.maxBoundsData={left:getLeft(),top:getTop(),width:getWidth(),height:getHeight()};
                    //设定最大化按钮
				    oMaximizeButton.innerHTML=getButtonSpan(2);					    
                    //设定窗口范围
					//setBounds(-4,-4,_oThis.getFrameWidth()+8,_oThis.getFrameHeight()+8);
					setBounds(0,0,_oThis.getFrameWidth(),_oThis.getFrameHeight());
					setState("maximized");
	                break;
	            case "maximized": //最大化
	                setState("normal");
                    //设定按钮状态
				    oMaximizeButton.innerHTML=getButtonSpan(1);				    
                    //用恢复记录值恢复窗口状态                    
					setBounds(oWindow.maxBoundsData.left,oWindow.maxBoundsData.top,oWindow.maxBoundsData.width,oWindow.maxBoundsData.height);					
	                break;
	        }
        }
        
        /***********************************************************************
        函数或过程名称: onMaximizeButtonClick()
        输入参数: 无
        输出参数: 无
        功能描述: 处理最大化按钮点击事件
        流程处理: 
        全局变量: 
        调用方法: onMaximizeButtonClick()
        调用样例: onMaximizeButtonClick()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function onMaximizeButtonClick() {
	        if(getResizable()){
	            if (_oThisChild.onMaximize!=null) {
	                _oThisChild.onMaximize(_oThisChild); //向上级传递消息
	            }else{
				    windowMaximize(); //最大化操作
				}
	        }
        }
                
        /***********************************************************************
        函数或过程名称: getShowCaption()
        输入参数: 无
        输出参数: 布尔 
        功能描述: 返回标题栏显示状态
        流程处理: 
        全局变量: 
        调用方法: getShowCaption()
        调用样例: getShowCaption()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function getShowCaption() {
            return oWindowCaption.style.display=="none"?false:true;
        }
        
        /***********************************************************************
        函数或过程名称:setShowCaption(bC)
        输入参数: bC 布尔 
        输出参数: 无
        功能描述: 设置标题栏是否显示
        流程处理: 
        全局变量: 
        调用方法: setShowCaption(bC)
        调用样例: setShowCaption(true)
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setShowCaption(bC) {
            if (bC) 
                oWindowCaption.style.display="";                
            else
                oWindowCaption.style.display="none";
            setChildrenSize();
        }
        
        /***********************************************************************
        函数或过程名称:getShowIco() 
        输入参数: 无
        输出参数: 布尔
        功能描述: 返回图标显示状态
        流程处理: 
        全局变量: 
        调用方法: getShowIco()
        调用样例: getShowIco()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function getShowIco() {
            return oWindowIco.style.display=="none"?false:true;
        }
        
        /***********************************************************************
        函数或过程名称: setShowIco(bC)
        输入参数: bC 布尔
        输出参数: 无
        功能描述: 设置图标显示状态
        流程处理: 
        全局变量: 
        调用方法: setShowIco(bC)
        调用样例: setShowIco(bC)
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setShowIco(bC) {
            if (bC)
                oWindowIco.style.display="";
            else
                oWindowIco.style.display="none";
            setCaptionTitleW(); //设置标题文字宽度
        }
        
        /***********************************************************************
        函数或过程名称: getShowClose()
        输入参数: 无
        输出参数: 布尔
        功能描述: 返回关闭按钮显示状态
        流程处理: 
        全局变量: 
        调用方法: getShowClose()
        调用样例: getShowClose()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function getShowClose() {
            return oCloseButton.style.display=="none"?false:true;
        }
        
        /***********************************************************************
        函数或过程名称: setShowClose(bC)
        输入参数: bC buer
        输出参数: 无
        功能描述: 设置关闭按钮是否显示
        流程处理: 
        全局变量: 
        调用方法: setShowClose(bC)
        调用样例: setShowClose(bC)
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setShowClose(bC) {
            if (bC) 
                oCloseButton.style.display="";
            else
                oCloseButton.style.display="none";
            setCaptionTitleW();
        }
        
        /***********************************************************************
        函数或过程名称: getShowMaximize()
        输入参数: 无
        输出参数: 布尔
        功能描述: 返回最大化按钮显示状态
        流程处理: 
        全局变量: 
        调用方法: getShowMaximize()
        调用样例: getShowMaximize()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function getShowMaximize() {
            return oMaximizeButton.style.display=="none"?false:true;
        }
        
        /***********************************************************************
        函数或过程名称: setShowMaximize(bC)
        输入参数: bC 布尔
        输出参数: 无
        功能描述: 设定最大化按钮是否显示
        流程处理: 
        全局变量: 
        调用方法: setShowMaximize(bC)
        调用样例: setShowMaximize(bC)
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setShowMaximize(bC) {
            if (bC)
                oMaximizeButton.style.display="";
            else
                oMaximizeButton.style.display="none";
            setCaptionTitleW();
        }
        
        /***********************************************************************
        函数或过程名称: getShowMinimize()
        输入参数: 无
        输出参数: 布尔
        功能描述: 返回最小化按钮显示状态
        流程处理: 
        全局变量: 
        调用方法: getShowMinimize()
        调用样例: getShowMinimize()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function getShowMinimize() {
            return oMinimizeButton.style.display=="none"?false:true;
        }
        
        /***********************************************************************
        函数或过程名称: setShowMinimize(bC)
        输入参数: bC 布尔
        输出参数: 无
        功能描述: 设定最小化按钮是否显示
        流程处理: 
        全局变量: 
        调用方法: setShowMinimize(bC)
        调用样例: setShowMinimize(bC)
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setShowMinimize(bC) {        
            if (bC)
                oMinimizeButton.style.display="";
            else 
                oMinimizeButton.style.display="none";
            setCaptionTitleW();
        }
        
        /***********************************************************************
        函数或过程名称: getCanMinimize()
        输入参数: 无
        输出参数: 布尔
        功能描述: 返回最小化按钮可用状态
        流程处理: 
        全局变量: 
        调用方法: getCanMinimize()
        调用样例: getCanMinimize()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function getCanMinimize() {
            return !oMinimizeButton.disabled;
        }
        
        /***********************************************************************
        函数或过程名称: setCanMinimize(bC)
        输入参数: bC 布尔
        输出参数: 无
        功能描述: 设定窗口是否可以最小化
        流程处理: 
        全局变量: 
        调用方法: setCanMinimize(bC)
        调用样例: setCanMinimize(false)
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setCanMinimize(bC) {
            oMinimizeButton.disabled=!bC;
        }
        
        /***********************************************************************
        函数或过程名称: getResizable() 
        输入参数: 无
        输出参数: 布尔
        功能描述: 返回窗口是否可调整大小
        流程处理: 
        全局变量: 
        调用方法: getResizable() 
        调用样例: getResizable() 
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function getResizable() {
            return oWindow.resizable;
        }
        
        /***********************************************************************
        函数或过程名称: setResizable(bR)
        输入参数: bR 布尔 
        输出参数: 无
        功能描述: 设定窗口是否允许调整大小
        流程处理: 
        全局变量: 
        调用方法: setResizable(bR)
        调用样例: setResizable(false)
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setResizable(bR) {
            oWindow.resizable=bR;
            oMaximizeButton.disabled=!bR;            
        }
        
        /***********************************************************************
        函数或过程名称: setCaptionTitleW()
        输入参数: 无
        输出参数: 无
        功能描述: 设置标题栏文字宽度
        流程处理: 
        全局变量: 
        调用方法: setCaptionTitleW()
        调用样例: setCaptionTitleW()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setCaptionTitleW() {
            //总宽度标题栏
            var iS=parseInt(oWindow.style.width,10);        
            //减去图片,图标,得到实际宽度
            if (oWindowIco.style.display!="none") iS-=16;
            if (oMinimizeButton.style.display!="none") iS-=parseInt(oMinimizeButton.style.width);
            if (oMaximizeButton.style.display!="none") iS-=parseInt(oMaximizeButton.style.width);
            if (oCloseButton.style.display!="none") iS-=parseInt(oCloseButton.style.width);
            iS=(iS-16)<0?0:iS-16;
            setTitleWidth(iS-16);
        }
        
        /***********************************************************************
        函数或过程名称: setcontentPaneH() 
        输入参数: 无
        输出参数: 无
        功能描述: 设置容器高度
        流程处理: 
        全局变量: 
        调用方法: setcontentPaneH() 
        调用样例: setcontentPaneH() 
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setcontentPaneH() {
            var iH=getHeight()-getBorderWidth()*2;            
            iH-=parseInt(oWindowCaption.offsetHeight,10);
            if (getShowCaption()) {
                iH-=parseInt(oWindowCaption.style.marginTop,10);
                iH-=parseInt(oWindowCaption.style.marginBottom,10);
            }
            //iH-=parseInt(oContentPane.style.marginTop,10);
            //iH-=parseInt(oContentPane.style.marginBottom,10);
            iH-=2;//2 窗口边框宽度和空白
            oContentPane.style.height=iH<0?0:iH;            
        }
        
        /***********************************************************************
        函数或过程名称: setChildrenSize()
        输入参数: 无
        输出参数: 无
        功能描述: 调整标题栏文字宽度和容器高度
        流程处理: 
        全局变量: 
        调用方法: setChildrenSize()
        调用样例: setChildrenSize()
        创建日期: 2003-12-23
        作者: 鲁小帅
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setChildrenSize() {
            setCaptionTitleW();            
            setcontentPaneH();
        }
    
        /***********************************************************************
        函数或过程名称: getLeft()
        输入参数: 无
        输出参数: integer 
        功能描述: 返回窗口左边界
        流程处理: 
        全局变量: 
        调用方法: getLeft()
        调用样例: getLeft()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function getLeft() {
            if (oWindow!=null) 
                return parseInt(oWindow.style.left,10);
            else
                return null;
        }
    
        /***********************************************************************
        函数或过程名称: getTop()
        输入参数: 无
        输出参数: integer
        功能描述: 返回窗口上边界
        流程处理: 
        全局变量: 
        调用方法: getTop()
        调用样例: getTop()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function getTop() {
            if (oWindow!=null) 
                return parseInt(oWindow.style.top,10);
            else
                return null;
        }
    
        /***********************************************************************
        函数或过程名称: getWidth()
        输入参数: 无
        输出参数: integer
        功能描述: 返回窗口宽度
        流程处理: 
        全局变量: 
        调用方法: getWidth()
        调用样例: getWidth()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function getWidth() {
            if (oWindow!=null)
                if (parseInt(oWindow.offsetWidth,10)==0) 
                    return parseInt(oWindow.style.width,10);
                else
                    return parseInt(oWindow.offsetWidth,10);    
            else
                return null;
        }
    
        /***********************************************************************
        函数或过程名称: getHeight()
        输入参数: 无
        输出参数: integer
        功能描述: 返回窗口高度
        流程处理: 
        全局变量: 
        调用方法: getHeight()
        调用样例: getHeight()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function getHeight() {
            if (oWindow!=null) 
                if (parseInt(oWindow.offsetHeight,10)==0) 
                    return parseInt(oWindow.style.height,10);
                else
                    return parseInt(oWindow.offsetHeight,10);
            else
                return null;
        }
    
        /***********************************************************************
        函数或过程名称: setLeft(iX)
        输入参数: iX integer
        输出参数: 无
        功能描述: 
        流程处理: 
        全局变量: 
        调用方法: setLeft(iX)
        调用样例: setLeft(10)
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setLeft(iX) {
            if (oWindow==null) return;
            oWindow.style.left=iX;
        }    
        
        /***********************************************************************
        函数或过程名称: setTop(iY)
        输入参数: iY integer
        输出参数: 无
        功能描述: 
        流程处理: 
        全局变量: 
        调用方法: setTop(iY)
        调用样例: setTop(10)
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setTop(iY) {
            if (oWindow==null) return;
            oWindow.style.top=iY;
        }
    
        /***********************************************************************
        函数或过程名称: setWidth(iW)
        输入参数: iW integer 宽度
        输出参数: 无
        功能描述: 设置窗口宽度
        流程处理: 
        全局变量: 
        调用方法: setWidth(iW)
        调用样例: setWidth(200)
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setWidth(iW) {
            if (oWindow==null) return;
            oWindow.style.width=iW;
            setCaptionTitleW();
        }
    
        /***********************************************************************
        函数或过程名称: setHeight(iH)
        输入参数: iH integer 高度
        输出参数: 无
        功能描述: 设置窗口高度
        流程处理: 
        全局变量: 
        调用方法: setHeight(iH)
        调用样例: setHeight(300)
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setHeight(iH) {
            if (oWindow==null) return;
            oWindow.style.height=iH;
            setcontentPaneH();
        }
        
        /***********************************************************************
        函数或过程名称: setSize(iW,iH)
        输入参数: iW integer 宽度 iH integer 高度
        输出参数: 无
        功能描述: 设置窗口大小
        流程处理: 
        全局变量: 
        调用方法: setSize(iW,iH)
        调用样例: setSize(200,300)
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setSize(iW,iH) {
            setWidth(iW);
            setHeight(iH);
        }
        
		/***********************************************************************
        函数或过程名称: setLocation(iT,iL)
        输入参数: iT integer ,iL integer 
        输出参数: 无
        功能描述: 设置窗口位置
        流程处理: 
        全局变量: 
        调用方法: setLocation(iT,iL)
        调用样例: setLocation(10,10)
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setLocation(iT,iL) {
		    setTop(iT);
		    setLeft(iL);
		}
		    
        /***********************************************************************
        函数或过程名称: setBounds(iX,iY,iW,iH)
        输入参数: iX integer iY integer iW integer iH integer
        输出参数: 无
        功能描述: 设置窗口范围
        流程处理: 
        全局变量: 
        调用方法: setBounds(iX,iY,iW,iH)
        调用样例: setBounds(10,10,200,300)
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setBounds(iX,iY,iW,iH) {
            setLeft(iX);
            setTop(iY);
            setWidth(iW);
            setHeight(iH);
        }
    
        /***********************************************************************
        函数或过程名称: getActive()
        输入参数: 无
        输出参数: 布尔 窗口活动状态
        功能描述: 返回窗口活动状态
        流程处理: 
        全局变量: 
        调用方法: getActive()
        调用样例: getActive()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function getActive() {
            return oWindow.active;
        }
        
        /***********************************************************************
        函数或过程名称: onWindowClick()
        输入参数: 无
        输出参数: 无
        功能描述: 窗口被点击,触发事件
        流程处理: 
        全局变量: 
        调用方法: onWindowClick()
        调用样例: onWindowClick()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function onWindowClick() {            
            if (_oThisChild.onSetActive) _oThisChild.onSetActive(_oThisChild);
            if (_oThisChild.onstartResize && getResizeDir()!="" && event.srcElement==getWindow()) _oThisChild.onstartResize();
        }
        
        /***********************************************************************
        函数或过程名称: setActive(bActive)
        输入参数: bActive true 活动状态 false非活动状态
        输出参数: 无
        功能描述: 设置窗口活动状态
        流程处理: 
        全局变量: 
        调用方法: setActive(bActive)
        调用样例: setActive(true)
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setActive(bActive) {            
            if (bActive!=getActive()) {
                //设置窗口样式
                if (bActive) {
                    oWindowCaption.style.backgroundColor=activeCaptionColor;
                    oCaptionTitle.style.color=activeCaptionTextColor;
                    oWindow.active=true;
                }else{
                    oWindowCaption.style.backgroundColor=inactiveCaptionColor;
                    oCaptionTitle.style.color=inactiveCaptionTextColor;
                    oWindow.active=false;
                }
            }
            //如果图标没有显示,重新load
            //if (oWindowIco.fileSize==-1) setWindowIco(getWindowIco());
        }
        
        /***********************************************************************
        函数或过程名称: getOriginZIndex()
        输入参数: 无
        输出参数: integer 窗口历史索引
        功能描述: 返回窗口历史索引
        流程处理: 
        全局变量: 
        调用方法: getOriginZIndex()
        调用样例: getOriginZIndex()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function getOriginZIndex() {
            if (oWindow.zIndex!=null) return parseInt(oWindow.zIndex,10);
        }

        /***********************************************************************
        函数或过程名称: getZIndex()
        输入参数: 无
        输出参数: integer 层索引
        功能描述: 
        流程处理: 
        全局变量: 
        调用方法: getZIndex()
        调用样例: getZIndex()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function getZIndex() {
            return parseInt(oWindow.style.zIndex,10);
        }
        
        /***********************************************************************
        函数或过程名称: setZIndex(iI)
        输入参数: iI integer 窗口层索引
        输出参数: 无
        功能描述: 设置窗口层索引
        流程处理: 
        全局变量: 
        调用方法: setZIndex(iI)
        调用样例: setZIndex(200)
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setZIndex(iI) {
            oWindow.style.zIndex=iI;
            if (oWindow.zIndex==null) oWindow.zIndex=iI;
        }
        
        /***********************************************************************
        函数或过程名称: moveStart()
        输入参数: 无
        输出参数: 无
        功能描述: 开始开始窗口移动
        流程处理: 
        全局变量: 
        调用方法: moveStart()
        调用样例: moveStart()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function moveStart() {
            //窗口为最大化,直接返回            
            if (getState()=="maximized") return;
            oWindowCaption.MoveHandle.attachEvent("onmouseup",moveEnd);
	        oWindowCaption.MoveHandle.attachEvent("onmousemove",continueMove);
            //记录当前窗口状态,位置和大小
            oWindow.moveData={screenX:event.clientX,screenY:event.clientY,startX:oWindow.style.pixelLeft,startY:oWindow.style.pixelTop};             
            oWindowCaption.MoveHandle.setCapture(true);            
        }
        
         /***********************************************************************
        函数或过程名称: moveEnd()
        输入参数: 无
        输出参数: 无
        功能描述: 停止窗口移动
        流程处理: 
        全局变量: 
        调用方法: moveEnd()
        调用样例: moveEnd()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function moveEnd() {
            oWindowCaption.MoveHandle.detachEvent("onmouseup",moveEnd);
	        oWindowCaption.MoveHandle.detachEvent("onmousemove",continueMove);
            oWindowCaption.MoveHandle.releaseCapture();            
        }
        
        /***********************************************************************
        函数或过程名称: continueMove()
        输入参数: 无
        输出参数: 无
        功能描述: 移动窗口
        流程处理: 
        全局变量: 
        调用方法: continueMove()
        调用样例: continueMove()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function continueMove() {
            //计算偏移量
            var iX=oWindow.moveData.startX+event.clientX-oWindow.moveData.screenX;
            var iY=oWindow.moveData.startY+event.clientY-oWindow.moveData.screenY;
            //计算是否超出容器边界
            iX=iX<-getTitleWidth()?-getTitleWidth():iX;
            iY=iY<-getTitleHeight()?-getTitleHeight():iY;
            iX=iX>_oThis.getFrameWidth()-30?_oThis.getFrameWidth()-30:iX;            
            iY=iY>_oThis.getFrameHeight()-getTitleHeight()?_oThis.getFrameHeight()-getTitleHeight():iY;
            //设定窗口位置
            setLeft(iX);
            setTop(iY);
            oWindow.minBoundsData.left=iX;  //设置最小化时的还原参数
            oWindow.minBoundsData.top=iY;
        }
    
        /***********************************************************************
        函数或过程名称: checkForResize()
        输入参数: 无
        输出参数: 无
        功能描述: 更改鼠标样式
        流程处理: 根据鼠标所处在窗口的位置,更改鼠标类型
        全局变量: 
        调用方法: checkForResize()
        调用样例: checkForResize()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        function checkForResize() {
            //如果窗口不允许调整大小,直接返回
	        if(!oWindow.resizable || getState()=="minimized" || getState()=="maximized") {
	            oWindow.resizeDir="";
	            return;
	        }	        
	        var resizeDir="";
	        var iResponseBound=4; //作用范围
	        if(event.srcElement==oWindow){
                //上下
    	        var oy=event.offsetY;
	    		if(oy<=iResponseBound)
		    	    resizeDir+="n";
			    else if(oy>=getHeight()-iResponseBound)			    
			        resizeDir+="s";			
			    //左右
    			var ox=event.offsetX;
	    		if(ox<=iResponseBound)
		    	    resizeDir+="w";
			    else if(ox>=getWidth()-iResponseBound)
    			    resizeDir+="e";
		        
                //标志不为空
	            if(resizeDir!=""){
                    //设置鼠标指针样式
	                oWindow.style.cursor=resizeDir+"-resize";
					oWindow.resizeDir=resizeDir;
	            }else {
	                oWindow.style.cursor="default";
					oWindow.resizeDir="";
				}
	        }else{ 
	            oWindow.style.cursor="default";
				oWindow.resizeDir="";
			}
        }
        /*
        function checkForResize() {
            //如果窗口不允许调整大小,直接返回
	        if(!oWindow.resizable ||getState()=="minimized") {
	            oWindow.resizeDir="";
	            return;
	        }	        
	        var resizeDir="";
	        var iResponseBound=4; //作用范围
	        if(event.srcElement==oWindow){
                //上下
    	        var oy=event.y;
	    		if(oy<=getTop()+iResponseBound)
		    	    resizeDir+="n";
			    else if(oy>=getTop()+getHeight()-iResponseBound)			    
			        resizeDir+="s";			
			    //左右
    			var ox=event.x;
	    		if(ox<=getLeft()+iResponseBound)
		    	    resizeDir+="w";
			    else if(ox>=getLeft()+getWidth()-iResponseBound)
    			    resizeDir+="e";
		        
                //标志不为空
	            if(resizeDir!=""){
                    //设置鼠标指针样式
	                oWindow.style.cursor=resizeDir+"-resize";
					oWindow.resizeDir=resizeDir;
	            }else {
	                oWindow.style.cursor="default";
					oWindow.resizeDir="";
				}
	        }else{ 
	            oWindow.style.cursor="default";
				oWindow.resizeDir="";
			}
        }
        */
        function dispose() {
            oWindowIco.detachEvent("ondblclick",onCloseButtonClick);
            oWindowCaption.detachEvent("onselectstart",oFunNoSelectA);
            oMinimizeButton.detachEvent("onclick",onMinimizeButtonClick);
			oMaximizeButton.detachEvent("onclick",onMaximizeButtonClick);
			oCloseButton.detachEvent("onclick",onCloseButtonClick);
			oMinimizeButton.detachEvent("onfocus",oMinimizeButton.blur);
			oMaximizeButton.detachEvent("onfocus",oMaximizeButton.blur);
			oCloseButton.detachEvent("onfocus",oCloseButton.blur);
            oWindowCaption.MoveHandle.detachEvent("onmousedown",moveStart);
            oWindowCaption.MoveHandle.detachEvent("ondblclick",onMaximizeButtonClick);
            oWindow.detachEvent("onmousemove",checkForResize); 
            oWindow.detachEvent("onmousedown",onWindowClick);
            oWindow.detachEvent("onresize",setChildrenSize);
            oContentPane.detachEvent("onselectstart",oFunNoSelectB);
            oContentPane.detachEvent("onfocus",oContentPane.blur);
            if (oClientSiteFrame!=null) oClientSiteFrame.detachEvent("onfocus",onWindowClick);            
            oWindowIco=null;
            oWindowCaption.MoveHandle=null;
            oWindowTitle=null;
            oWindowCaption=null;
            oMinimizeButton=null;
            oMaximizeButton=null;
            oCloseButton=null;
            oContentPane=null;
            if (oClientSiteFrame!=null) oClientSiteFrame=null;
            oWindow.removeNode(true);
            oWindow=null;
        }
    
        createWindow(sCaption);
    }
    
	/***********************************************************************
    函数或过程名称: WOCTaskBar()
    输入参数: 无
    输出参数: 无
    功能描述: TaskBar
    流程处理: 
    全局变量: 
    调用方法: WOCTaskBar()
    调用样例: new WOCTaskBar()
    创建日期: 2003-12-23
    作者: 鲁小帅    
    ==========================================================
    -----------------------修改记录-----------------------------------
    修改人:	         修改时间: 
    修改原因:
    -----------------------------------------------------------------
    ==========================================================
    备注：
    ***********************************************************************/

    function WOCTaskBar() {
	    var _oThisTask=this;
	    this._className="WOCTaskBar";
	    var oBar=null;
	    var aBtnList=new Array();
	    
	    this.create=create;
	    this.addButton=addButton;
	    this.deleteButton=deleteButton;
	    this.setButtonState=setButtonState;
	    this.setButtonTitle=setButtonTitle;
	    this.setButtonIco=setButtonIco;
	    
	    this.onClick=null;
	    this.onDblClick=null;
	    
        /***********************************************************************
        函数或过程名称: createBar()
        输入参数: 无
        输出参数: 无
        功能描述: 创建一个按钮容器
        流程处理: 
        全局变量: 
        调用方法: createBar()
        调用样例: createBar()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function createBar() {
            oBar=document.createElement('<div onselectstart="return false;">');
            with (oBar.style) {
                backgroundColor=barBgColor;
                width="100%";
                height="27";
                borderTop="2 outset";    
            }
        }
        
        /***********************************************************************
        函数或过程名称: create(oContainer)
        输入参数: oContainer object 要创建到的容器
        输出参数: 无
        功能描述: 将自身创建到指定位置
        流程处理: 
        全局变量: 
        调用方法: create(oContainer)
        调用样例: create(oTaskBarDown)
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function create(oContainer) {
	        if (!_oThisTask.hcon) {
    	    	if (!oContainer.canHaveChildren) {				
                    throw "创建失败，create()方法的参数不是一个容器的id！";
		        	return;
			    }
			    else 
			    {                    
                    oContainer.insertAdjacentElement("beforeEnd",oBar);                
                    _oThisTask.hcon = oContainer;
			    }
		    }		    
        }
        
        /***********************************************************************
        函数或过程名称: buttonClick(iIndex)
        输入参数: iIndex integer 按钮索引
        输出参数: 无
        功能描述: 向上传递按钮点击事件
        流程处理: 
        全局变量: 
        调用方法: buttonClick(iIndex)
        调用样例: buttonClick(1)
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function buttonClick(iIndex) {
            if (_oThisTask.onClick!=null) 
                _oThisTask.onClick(iIndex);
            //else
                //alert(iIndex);
        }
        
        /***********************************************************************
        函数或过程名称: buttonDblClick(iIndex)
        输入参数: iIndex integer 按钮索引
        输出参数: 无
        功能描述: 向上传递按钮双击事件
        流程处理: 
        全局变量: 
        调用方法: buttonDblClick(iIndex)
        调用样例: buttonDblClick(1)
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function buttonDblClick(iIndex) {
            if (_oThisTask.onDblClick!=null) 
                _oThisTask.onDblClick(iIndex);
            //else
                //alert(iIndex);
        }        
        
        /***********************************************************************
        函数或过程名称: setButtonTitle(iIndex,sTitle)
        输入参数: iIndex integer 索引,sTitle string 文字
        输出参数: 无
        功能描述: 设置按钮文字
        流程处理: 
        全局变量: 
        调用方法: setButtonTitle(iIndex,sTitle)
        调用样例: setButtonTitle(1,"new window")
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setButtonTitle(iIndex,sTitle) {
            aBtnList[iIndex].setTitle(sTitle);
        }
        
        /***********************************************************************
        函数或过程名称: setButtonIco(iIndex,sImgN)
        输入参数: iIndex integer 索引,sImgN string 图片名称        
        输出参数: 无
        功能描述: 设置按钮图标
        流程处理: 
        全局变量: 
        调用方法: setButtonIco(iIndex,sImgN)
        调用样例: setButtonIco(1,"16.gif")
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setButtonIco(iIndex,sImgN) {
            aBtnList[iIndex].setIco(sImgN);
        }

        /***********************************************************************
        函数或过程名称: setButtonState(iIndex,bState)
        输入参数: iIndex integer 索引 bState 布尔 按钮状态 
        输出参数: 无
        功能描述: 设置按钮状态
        流程处理: 
        全局变量: 
        调用方法: setButtonState(iIndex,bState)
        调用样例: setButtonState(1,false)
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setButtonState(iIndex,bState) {            
            if (bState) {
                aBtnList[iIndex].setBtnDown();
            }else{
                aBtnList[iIndex].setBtnUp();
            }
        }
        
        /***********************************************************************
        函数或过程名称: addButton(sIco,sText,iIndex)
        输入参数: sIco string 图片,sText string 文字,iIndex integer 索引
        输出参数: 无
        功能描述: 增加一个按钮
        流程处理: 
        全局变量: 
        调用方法: addButton(sIco,sText,iIndex)
        调用样例: addButton(sIco,sText,2)
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function addButton(sIco,sText,iIndex) {
            aBtnList[iIndex]=new WOCBarButton(sIco,sText);
            aBtnList[iIndex].create(oBar);
            aBtnList[iIndex].onClick=buttonClick;
            aBtnList[iIndex].onDblClick=buttonDblClick;
            aBtnList[iIndex]._index=iIndex;
        }
        
        /***********************************************************************
        函数或过程名称: deleteButton(iIndex)
        输入参数: iIndex integer 按钮索引
        输出参数: 无
        功能描述: 根据索引删除按钮
        流程处理: 
        全局变量: 
        调用方法: deleteButton(iIndex)
        调用样例: deleteButton(1)
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function deleteButton(iIndex) {
            aBtnList[iIndex].dispose();            
            delete aBtnList[iIndex];
        }
        
        /***********************************************************************
        函数或过程名称: WOCBarButton(sIco,sText)
        输入参数: sIco string 图片文件 sText string 显示文字
        输出参数: 无
        功能描述: 创建一个按钮
        流程处理: 
        全局变量: 
        调用方法: WOCBarButton(sIco,sText)
        调用样例: obtn=new WOCBarButton(sIco,sText)
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function WOCBarButton(sIco,sText) {
            var _oThisButton=this;
	        this._className="WOCBarButton";
	        var oBtn=null;
	        var oBtnText=null;
	    
    	    this.create=create;
    	    this.dispose=dispose;
    	    this.setBtnDown=btnMouseDown;
    	    this.setBtnUp=btnMouseUp;
    	    this.setTitle=setTitle;
    	    this.setIco=setIco;
    	    this.getBtn=function() {
    	        return oBtn;
    	    };
    	    var ofun=function(){return false;};
    	    
    	    this.onClick=null;
    	    this.onDblClick=null;
    	    
    	    function dispose() {
                oBtn.detachEvent("onmousedown",btnClick);
                oBtn.detachEvent("ondblclick",btnDblClick);
                oBtn.detachEvent("onselectstart",ofun);                     
                oBtn.removeNode(true);
                oBtn=null;
            }
            
	        //创建按钮
            oBtn=document.createElement('<div align="center" title="'+sText+'">');            
            with (oBtn.style) {            
                backgroundColor=barBtnDownBgColor;
                marginRight="1px";
                border="1 inset ";
                paddingTop="3px";
                cursor="hand";
                display="inline";
                fontSize=sFontSize;
                width="82";
                height="22";                                               
            }
            //如果有图片,创建图片
            var oImg=new Image();
            oImg.style.width="16";
            oImg.style.height="16";
            oImg.style.marginLeft="2";
            oBtn.insertAdjacentElement("afterBegin",oImg);
            if (typeof sIco=="string" && sIco.length>0) {
                oImg.src=sIco;                
            }else{                
                oImg.style.display="none";
            }
            //添加文字
            oBtnText=document.createElement("div");
            with (oBtnText.style) {                
                marginLeft="2px";
                paddingTop="2px";
                display="inline";
                overflow="hidden";
	            textOverflow="ellipsis";
	            whiteSpace="nowrap";
	            width="60";
                height="20";                
            }
            oBtnText.innerText=sText;
            oBtn.attachEvent("onmousedown",btnClick);
            oBtn.attachEvent("ondblclick",btnDblClick);
            oBtn.attachEvent("onselectstart",ofun);             
            oBtn.insertAdjacentElement("beforeEnd",oBtnText);
            
        /***********************************************************************
        函数或过程名称: setTitle(sTitle)
        输入参数: sTitle string 按钮标题
        输出参数: 无
        功能描述: 更改按钮标题
        流程处理: 
        全局变量: 
        调用方法: setTitle(sTitle)
        调用样例: setTitle("new window")
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setTitle(sTitle) {
                oBtn.title=sTitle;
                oBtnText.innerText=sTitle;
        }
        
        /***********************************************************************
        函数或过程名称: setIco(sImgN)
        输入参数: sTitle string 按钮图标
        输出参数: 无
        功能描述: 更改按钮图标
        流程处理: 
        全局变量: 
        调用方法: setIco(sImgN)
        调用样例: setIco("16.gif")
        创建日期: 2004-1-10
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function setIco(sImgN) {            
            if (sImgN.length<=0) 
                oImg.style.display="none";
            else{
                oImg.style.display="";
                oImg.src=sImgN;
            }
        }
            
        /***********************************************************************
        函数或过程名称: btnClick()
        输入参数: 无
        输出参数: 无
        功能描述: 按钮点击
        流程处理: 
        全局变量: 
        调用方法: btnClick()
        调用样例: btnClick()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function btnClick() {
                if (_oThisButton.onClick!=null) _oThisButton.onClick(_oThisButton._index);
        }
        
        /***********************************************************************
        函数或过程名称: btnDblClick()
        输入参数: 无
        输出参数: 无
        功能描述: 按钮双击
        流程处理: 
        全局变量: 
        调用方法: btnDblClick()
        调用样例: btnDblClick()
        创建日期: 2004-2-10
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function btnDblClick() {
                if (_oThisButton.onDblClick!=null) _oThisButton.onDblClick(_oThisButton._index);
        }
            
        /***********************************************************************
        函数或过程名称: btnMouseDown()
        输入参数: 无
        输出参数: 无
        功能描述: 设置按钮为按下状态,如果图片没有显示,重新LOAD
        流程处理: 
        全局变量: 
        调用方法: btnMouseDown()
        调用样例: btnMouseDown()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function btnMouseDown() {
                with(oBtn.style) {
                    backgroundColor=barBtnDownBgColor;
                    border="1 inset";
                }
                //if (oImg!=null && oImg.fileSize==-1) oImg.src=oImg.src;
            }
            
        /***********************************************************************
        函数或过程名称: btnMouseUp()
        输入参数: 无
        输出参数: 无
        功能描述: 设置按钮为抬起状态
        流程处理: 
        全局变量: 
        调用方法: btnMouseUp()
        调用样例: btnMouseUp()
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function btnMouseUp() {
                with(oBtn.style) {
                    backgroundColor=barBgColor;
                    border="1 outset";
                }
            }            
        
        /***********************************************************************
        函数或过程名称: create(oContainer)
        输入参数: oContainer object 要创建到的容器
        输出参数: 无
        功能描述: 将自身创建到指定位置
        流程处理: 
        全局变量: 
        调用方法: create(oContainer)
        调用样例: create(oTaskBarDown)
        创建日期: 2003-12-23
        作者: 鲁小帅    
        ==========================================================
        -----------------------修改记录-----------------------------------
        修改人:	         修改时间: 
        修改原因:
        -----------------------------------------------------------------
        ==========================================================
        备注：
        ***********************************************************************/
        
        function create(oContainer) {
	            if (!_oThisButton.hcon) {
    	        	if (!oContainer.canHaveChildren) {				
                        throw "创建失败，create()方法的参数不是一个容器的id！";
		            	return;
			        }
			        else 
			        {                           
                        oContainer.insertAdjacentElement("beforeEnd",oBtn);                
                        _oThisButton.hcon = oContainer;
			        }
		        }		    
            }
        }       
        
        createBar();

	}	
	/*------------------------------------------
	WOCTaskBar end
	------------------------------------------*/
}