// ======================================================================================================================================>
// AjaxDesktop类
// ======================================================================================================================================>
function AjaxDesktop( id )
{
    var oId = ( id ? id : null ) ;
    var _oThis = this ;
    var oFrameCon = null ;
    var oMDIFrame = null ;
    var oDashedDiv = null ;
    var oTaskBar = null ;
    var startResized = false ;
    var aChildWndList = new Array() ;
    var activeWnd = null ;
    var barLocation = "top" ;
    var showBar = true ;
    var locationData = { top:0, left:0 } ;
    var _wndCount = 0 ;

    // 客户端方法
    this.closeAll        = closeAll ;               // 关闭所有窗口
    this.minimizeAll     = minimizeAll ;            // 所有窗口最小化
    this.cascadeWindow   = cascadeWindow ;          // 根据窗口层索引，层叠所有窗口
    this.getWidth        = getWidth ;               // 返回主框架宽度
	this.setWidth        = setWidth ;               // 设定主框架宽度
	this.getHeight       = getHeight ;              // 返回主框架高度
	this.setHeight       = setHeight ;              // 设置主框架高度
	this.setSize         = setSize ;                // 设定主框架的大小
	this.getFrameWidth   = getFrameWidth ;          // 返回客户区容器宽度
	this.setFrameWidth   = setFrameWidth ;          // 设定客户区容器宽度
	this.getFrameHeight  = getFrameHeight ;         // 返回客户区容器高度
	this.setFrameHeight  = setFrameHeight ;         // 设定客户区容器高度
	this.getBarLocation  = getBarLocation ;         // 获取任务栏位置
	this.setBarLocation  = setBarLocation ;         // 设置任务栏位置，只接受top和bottom值。在添加任何子窗口后调用无效
	this.getShowBar      = getShowBar ;             // 获取是否显示任务栏
	this.setShowBar      = setShowBar ;             // 设置是否显示任务栏
	this.fire            = fire ;                   // 创建
	
	this.getTaskBar      = getTaskBar ;             // ck.返回任务栏对象
	this.getWndList      = getWndList ;             // ck.返回窗口数组对象

    // 客户端事件
    this.onClose         = null ;                   // 窗口关闭事件
	this.onMaximize      = null ;                   // 窗口最大化事件
	this.onCaptionChange = null ;                   // 窗口标题文字修改事件
	this.onActive        = null ;                   // 活动窗口变化事件
		

    // 内部工具方法
	this.newWindow       = newWindow ;              // 实例化一个窗口子类,创建TaskBar,创建它大小变换时使用的虚线框
	this.getActive       = getActive ;              // 返回当前活动的窗口
	this.setActive       = setChildActive ;         // 设定OWnd为活动窗口,如果之前有活动窗口,设定它为非活动状态,更改TaskBar的状态
	this.isOpen			 = isOpen;
// --------------------------------------------------------->
// 客户端方法
// --------------------------------------------------------->
	function isOpen(src)
	{	 
	//alert(src)
		 var ishas = -1;			 
		 for( var i = 0; i <= getChildNumber(); ++i ) 
		 {
		 		//alert(aChildWndList[i].getloc());
		 		if (aChildWndList[i]==null)continue;
		 		if (src == aChildWndList[i].getloc())
		 		{
		 				//alert(aChildWndList[i].getloc());
		 				//ishas = aChildWndList[i]._index;
		 				ishas = i;
		 		}
  	     }
  	     return ishas;
	}
		
    function closeAll()
    {
        for( var i = 0; i <= getChildNumber(); ++i ) { onChildClose( i ); }
    };

    function minimizeAll()
    {
        for( var i = 0; i <= getChildNumber(); ++i ) { if( aChildWndList[i] != null && aChildWndList[i].getState() != "minimized" ) onChildMinimize( aChildWndList[i] ); }
    };

    function cascadeWindow()
    {
        locationData = { top:0, left:0 } ;
        var aWndList = new Array() ;
        var j = 0 ;

        for( var i = 0; i <= getChildNumber(); ++i )
        {
            if( aChildWndList[i] != null )
            {
                aWndList[j] = new Array() ;
                aWndList[j][0] = i ;
                aWndList[j][1] = aChildWndList[i].getZIndex() ;
                j++ ;
            }
        }

        if ( aWndList.length <= 0 ) return ;
        var aWndListSort = aWndList.sort( function listSort( a, b ) { return a[1] - b[1] ; } ) ;

        for( var i=0; i < aWndListSort.length; ++i )
        {
            var k = aWndListSort[i][0] ;
            setNewLocation( aChildWndList[k].getWidth(), aChildWndList[k].getHeight() ) ;
            aChildWndList[k].setLocation( locationData.top, locationData.left ) ;
        }
    }

    function getWidth()
    {
        if( oFrameCon == null ) return ;
        return parseInt( oFrameCon.offsetWidth, 10 ) ;
    };

    function setWidth( iWidth )
    {
        if( oFrameCon == null ) return ;
        oFrameCon.style.width = iWidth ;
    };

    function getHeight()
    {
        if( oFrameCon == null ) return ;
        return parseInt( oFrameCon.offsetHeight, 10 ) ;
    };

    function setHeight( iHeight )
    {
        if( oFrameCon == null ) return ;
        oFrameCon.style.height = iHeight ;
    };

    function setSize( iWidth, iHeight )
    {
        setWidth( iWidth ) ;
        setHeight( iHeight ) ;
    };

    function getFrameWidth()
    {
        if( oMDIFrame == null ) return ;
        return parseInt( oMDIFrame.offsetWidth, 10 ) ;
    };

    function setFrameWidth( iWidth )
    {
        if( oMDIFrame == null ) return ;
        oMDIFrame.style.width = iWidth ;
    };

    function getFrameHeight()
    {
        if( oMDIFrame == null ) return ;
        return parseInt( oMDIFrame.offsetHeight, 10 ) ;
    };

    function setFrameHeight( iHeight )
    {
        if( oMDIFrame == null ) return ;
        oMDIFrame.style.height = iHeight ;
    };

    function getBarLocation()
    {
        return barLocation ;
    };

    function setBarLocation( location )
    {
        if( location != "top" ) location = "bottom" ;
        barLocation = location ;
    };

    function getShowBar()
    {
        return showBar ;
    };

    function setShowBar( pShowBar )
    {
        showBar = pShowBar ;
    };

    function fire()
    {
        initializtion() ;
        oTaskBar.addButton( "", WINSHOW_LIST, -100 ) ;
    };
    
    function getTaskBar()   //ck.change
    {
        return oTaskBar ;
    };
    
    function getWndList()   //ck.change
    {
        return aChildWndList;
    };
// --------------------------------------------------------->
// 内部工具方法
// --------------------------------------------------------->
    function newWindow()
    {    		
//        if( _wndCount > 6 )
//        {
//            alert( __JSCONTROLWINDOW_WND_LIMIT__ ) ;
//            return null;
//        }
        _wndCount++ ;  
        
        var oWnd = new AjaxWindow( oMDIFrame ) ;
        
        oWnd.fire() ;
        oWnd.onstartResize = startChildResize ;
        oWnd.onSetActive = setActive ;  
				
								
        if( showBar )
        {
            oWnd.onMinimize = onChildMinimize ;
            oWnd.onCaptionChange = onChildCaptionChange ;
            oWnd.onMaximize = onChildMaximize ;
            oWnd.onIcoChange = onChildIcoChange ;
            oWnd.onClose = onChildClose ;

            aChildWndList.push( oWnd ) ;
            oWnd.setZIndex( oWnd.getZIndex() + getChildNumber() ) ;
            oWnd._index = getChildNumber() ;
            oTaskBar.addButton( oWnd.getWindowIco(), oWnd.getCaption(), getChildNumber() ) ;
        }

        setActive( oWnd ) ;

        oWnd.maximize() ;
        return activeWnd ;        
    };

    function getActive()
    {
        return activeWnd ;
    };

    function setActive( oWnd )
    {
        if( oWnd == null )
        {
            activeWnd = null ;
            if( _oThis.onCaptionChange != null ) _oThis.onCaptionChange( "" ) ;
            if( _oThis.onActive != null ) _oThis.onActive() ;
            return ;
        }

        if( activeWnd == null )
        {
            activeWnd = oWnd ;
            setTrue() ;
            return ;
        }

        if( activeWnd == oWnd ) return ;

        function setTrue()
        {
            oWnd.setActive( true ) ;
            if( oWnd.getState() == "minimized" ) oWnd.windowMinimize() ;
            if( oTaskBar != null ) oTaskBar.setButtonState( oWnd._index, true ) ;
            if( _oThis.onCaptionChange != null ) _oThis.onCaptionChange( oWnd.getCaption() ) ;
            if( _oThis.onActive != null ) _oThis.onActive() ;
        }

        var oActiveWnd = activeWnd ;
        activeWnd = oWnd ;
        setTrue() ;

        if( oActiveWnd != null )
        {
            activeWnd.setZIndex( oActiveWnd.getZIndex() + 1 ) ;
            oActiveWnd.setActive( false ) ;
            if( oTaskBar != null ) oTaskBar.setButtonState( oActiveWnd._index, false ) ;
        }
    };
// --------------------------------------------------------->
// 内部工具函数
// --------------------------------------------------------->
    /**
	 * 初始化桌面对象
	 */
    function initializtion()
    {
        oFrameCon = document.getElementById( oId ) ;
        oFrameCon.className = "ajaxWindow_desktop_body" ;
        oFrameCon.attachEvent( "onresize", onFrameResize ) ;

        //Frame容器
        oMDIFrame = oFrameCon.rows[1].cells[0].childNodes(0) ;
        oMDIFrame.className = "ajaxWindow_desktop_client" ;
        
        if( showBar )
        {
            oTaskBar = new AjaxTaskbar( minimizeAll ) ;
            if( barLocation == "top" )
            {
                oTaskBar.create( oFrameCon.rows[0].cells[0].childNodes(0), oMDIFrame ) ;    //ck.change
                oFrameCon.deleteRow(2);
            }
            else
            {
                oTaskBar.create( oFrameCon.rows[2].cells[0].childNodes(0), oMDIFrame ) ;    //ck.change
                oFrameCon.deleteRow(0);
            }
            oTaskBar.onClick    = function( iIndex ) { if( aChildWndList[iIndex].getState() == "minimized" ) aChildWndList[iIndex].windowMinimize(); setActive( aChildWndList[iIndex] ); }
            oTaskBar.onDblClick = function( iIndex ) { aChildWndList[iIndex].close(); }
        }
        else
        {
            oFrameCon.deleteRow(2);
            oFrameCon.deleteRow(0);
        }

        createDashedDiv() ;
    };

    /**
	 * 设定指定索引的窗口为活动窗口
	 * @param iIndex integer 窗口索引
	 */
    function setChildActive( iIndex )
    {
        if( aChildWndList[iIndex] != null ) setActive( aChildWndList[iIndex] ) ;
    };

    /**
	 * 获得当前窗口的数目
	 * @return integer 当前窗口的数目
	 */
    function getChildNumber()
    {
        return aChildWndList.length - 1 ;
    };

    /**
	 * 创建一个窗口调整大小时显示的虚线框
	 */
    function createDashedDiv()
    {
        
        oDashedDiv = document.createElement( '<div unselectable="on">' ) ;
        oDashedDiv.className = "common_dragging_area" ;
        oDashedDiv.resizeData = {} ;
        with( oDashedDiv.style )
        {
            position = "relative" ;
            overflow = "hidden" ;
            width    = "100" ;
            height   = "100" ;
            zIndex   = "20000" ;
            display  = "none" ;
        }
        oMDIFrame.insertAdjacentElement( "beforeEnd", oDashedDiv ) ;
        oDashedDiv.getLeft     = function() { return parseInt( oDashedDiv.style.left, 10 ); } ;
        oDashedDiv.getTop      = function() { return parseInt( oDashedDiv.style.top, 10 ); } ;
        oDashedDiv.getWidth    = function() { return parseInt( oDashedDiv.offsetWidth, 10 ); } ;
        oDashedDiv.getHeight   = function() { return parseInt( oDashedDiv.offsetHeight, 10 ); } ;
        oDashedDiv.setLeft     = function( iX ) { oDashedDiv.style.left = iX; } ;
        oDashedDiv.setTop      = function( iY ) { oDashedDiv.style.top = iY; } ;
        oDashedDiv.setWidth    = function( iW ) { oDashedDiv.style.width = iW; } ;
        oDashedDiv.setHeight   = function( iH ) { oDashedDiv.style.height = iH; } ;
        oDashedDiv.setSize     = function( iW, iH ) { oDashedDiv.setWidth( iW ); oDashedDiv.setHeight( iH ); } ;
        oDashedDiv.setLocation = function( iT, iL ) { oDashedDiv.setTop( iT ); oDashedDiv.setLeft( iL ); } ;
        oDashedDiv.setBounds   = function( iX, iY, iW, iH ) { oDashedDiv.setLeft( iX ); oDashedDiv.setTop( iY ); oDashedDiv.setWidth( iW ); oDashedDiv.setHeight( iH ); } ;
    };

    /**
	 * 产生一个新的坐标,供新窗口使用，根据传入的窗口大小，确定窗口该处在的位置
	 * @param iW integer 窗口宽度
	 * @param iH integer 窗口高度
	 */
    function setNewLocation(iW,iH)
    {
        var iAW = 20, iAH = 20 ;

        if( locationData.top == 0 && locationData.left == 0 )
        {
            locationData.top  = 1 ;
            locationData.left = 1 ;
            return ;
        }

        if( locationData.top + iH + iAH > getFrameHeight() || locationData.left + iW + iAW > getFrameWidth() )
        {
            locationData.top  = 1 ;
            locationData.left = 1 ;
            return ;
        }

        locationData.top  += iAH ;
        locationData.left += iAW ;
    };

    /**
	 * 设置层与当前窗口最接近的窗口为活动窗口
	 * @param iIndex integer 窗口索引
	 */
    function setNextActiveWnd( iIndex )
    {
        var aWndList = new Array() ;
        var j =0 ;

        for( var i = 0; i <= getChildNumber(); ++i )
        {
            if( i !=iIndex && aChildWndList[i] != null && aChildWndList[i].getState() != "minimized" )
            {
                aWndList[j] = new Array() ;
                aWndList[j][0] = i ;
                aWndList[j][1] = aChildWndList[i].getZIndex() ;
                j++ ;
            }
        }

        if( aWndList.length <= 0 )
        {
            setActive( null ) ;
            return ;
        }

        var aWndListSort = aWndList.sort( function listSort( a, b ) { return b[1] - a[1]; } ) ;
        setActive( aChildWndList[aWndListSort[0][0]] ) ;
    };

    /**
	 * 处理工作区大小变化操作
	 */
    function onFrameResize()
    {
        for( var i = 0; i <= getChildNumber(); ++i )
        {
            if( aChildWndList[i] != null && aChildWndList[i].getState() == "maximized" )
            {
                aChildWndList[i].setBounds( 2, 2, getFrameWidth() - 4, getFrameHeight() - 4 ) ;
            }
        }
    };

    /**
	 * 对指定窗口做最大化操作
	 * @param oWnd object 窗口对象
	 */
    function onChildMaximize( oWnd )
    {
        oWnd.windowMaximize() ;
        if( _oThis.onMaximize != null )
        {
            if( !_oThis.onMaximize( oWnd ) )
            {
                if( oWnd.getState() == "maximized" )
                {
                    oWnd.setShowCaption( false ) ;
                }
                else
                {
                    oWnd.setShowCaption( true ) ;
                }
            }
        }
    };

    /**
	 * 处理标题文字更改事件
	 * @param iIndex integer 窗口索引
	 */
    function onChildCaptionChange( iIndex )
    {
        if( oTaskBar != null ) oTaskBar.setButtonTitle( iIndex, aChildWndList[iIndex].getCaption() ) ;
        if( _oThis.onCaptionChange != null ) _oThis.onCaptionChange( aChildWndList[iIndex].getCaption() ) ;
    };

    /**
	 * 处理窗口关闭
	 * @param iIndex integer 窗口索引
	 */
    function onChildClose( iIndex )
    {  
    		//alert(aChildWndList[iIndex].getloc());
        if( aChildWndList[iIndex] == null ) return ;
        if( _oThis.onClose != null )
        {
            var oEvent = new Object() ;
            oEvent.caption = aChildWndList[iIndex].getCaption() ;
            oEvent.index = aChildWndList[iIndex]._index ;
            if( !_oThis.onClose( oEvent ) ) return ;
        }

        if( aChildWndList[iIndex] == activeWnd ) setNextActiveWnd( iIndex ) ;

        aChildWndList[iIndex].dispose() ;
        aChildWndList[iIndex] = null ;
        delete aChildWndList[iIndex] ;
        if( oTaskBar != null ) oTaskBar.deleteButton( iIndex ) ;
	
        CollectGarbage() ;
        _wndCount-- ;
    };

    /**
	 * 处理窗口最小化
	 * @param oWnd object 窗口对象
	 */
    function onChildMinimize( oWnd )
    {
        oWnd.windowMinimize() ;

        if( activeWnd == oWnd ) setNextActiveWnd( oWnd._index ) ;
        if( oTaskBar != null ) oTaskBar.setButtonState( oWnd._index, false ) ;
    };

    /**
	 * 处理窗口更改图标事件
	 * @param oWnd object 窗口对象
	 */
    function onChildIcoChange(oWnd)
    {
        if( oTaskBar != null ) oTaskBar.setButtonIco( oWnd._index, oWnd.getWindowIco() ) ;
    };

    /**
	 * 开始处理窗口缩放操作
	 * 记录当前窗口的位置和大小,显示虚线框,给容器增加鼠标事件,用户处理鼠标拖动操作
	 */
    function startChildResize()
    {
        oDashedDiv.setBounds( activeWnd.getLeft(), activeWnd.getTop(), activeWnd.getWidth(), activeWnd.getHeight() ) ;
        oDashedDiv.style.display = "" ;
        oDashedDiv.resizeDir = activeWnd.getResizeDir() ;
        oDashedDiv.MinimumWidth = activeWnd.getMinimumWidth() ;
        oDashedDiv.MinimumHeight =activeWnd.getMinimumHeight() ;

        oDashedDiv.resizeData =
        {
            screenX:event.clientX,
            screenY:event.clientY,
            startLeft:oDashedDiv.style.pixelLeft,
            startTop:oDashedDiv.style.pixelTop,
            startWidth:oDashedDiv.offsetWidth,
            startHeight:oDashedDiv.offsetHeight
        };

        startResized = true ;
        oMDIFrame.attachEvent( "onmousemove", continueChildResize ) ;
        oMDIFrame.attachEvent( "onmouseup", endChildResize ) ;
        oMDIFrame.style.cursor = activeWnd.getResizeDir() + "-resize" ;
        oMDIFrame.setCapture() ;
    };

    /**
	 * 调整虚线框大小和位置
	 * 根据鼠标当前位置和历史位置,计算偏移量,根据值定位虚线框的大小和位置
	 */
    function continueChildResize()
    {
        var dir = oDashedDiv.resizeDir, i = 0, iW, iH, iT, iL, eX = event.clientX, eY = event.clientY ;

        if( /e/i.test( dir ) )
        {
            iW = eX - oDashedDiv.resizeData.screenX ;
            iW = oDashedDiv.resizeData.startWidth + iW ;
            iW = iW < oDashedDiv.MinimumWidth ? oDashedDiv.MinimumWidth : iW ;
            iW = iW + oDashedDiv.resizeData.startLeft > getWidth() ? getWidth() - oDashedDiv.resizeData.startLeft - 4 : iW ;
            oDashedDiv.setWidth( iW ) ;
        }
        else if( /w/i.test( dir ) )
        {
            iW = eX - oDashedDiv.resizeData.screenX ;
            iW = oDashedDiv.resizeData.startWidth - iW ;
            iW = iW < oDashedDiv.MinimumWidth ? oDashedDiv.MinimumWidth : iW ;
            iL = oDashedDiv.resizeData.startLeft - ( iW - oDashedDiv.resizeData.startWidth ) ;
            if( iL <= 0 ) { i = iL; iL = 0; }
            oDashedDiv.setWidth( iW + i ) ;
            oDashedDiv.setLeft( iL ) ;
        }

        i = 0 ;
        if( /s/i.test( dir ) )
        {
            iH = eY - oDashedDiv.resizeData.screenY ;
            iH = oDashedDiv.resizeData.startHeight + iH ;
            iH = iH < oDashedDiv.MinimumHeight ? oDashedDiv.MinimumHeight : iH ;
            iH = iH + oDashedDiv.resizeData.startTop > getHeight() ? getHeight() - oDashedDiv.resizeData.startTop - 4 : iH ;
            oDashedDiv.setHeight( iH ) ;
        }
        else if( /n/i.test( dir ) )
        {
            iH = eY - oDashedDiv.resizeData.screenY ;
            iH = oDashedDiv.resizeData.startHeight - iH ;
            iH = iH < oDashedDiv.MinimumHeight ? oDashedDiv.MinimumHeight : iH ;
            iT = oDashedDiv.resizeData.startTop - ( iH - oDashedDiv.resizeData.startHeight ) ;
            if( iT <= 0 ) { i = iT; iT = 0; }
            oDashedDiv.setHeight( iH + i ) ;
            oDashedDiv.setTop( iT ) ;
        }
    };

    /**
	 * 结束大小调整
	 * 鼠标放开,根据虚线框的大小和位置调整窗口
	 */
    function endChildResize()
    {
        startResized = false ;
        oMDIFrame.detachEvent( "onmousemove", continueChildResize ) ;
        oMDIFrame.detachEvent( "onmouseup", endChildResize ) ;
        activeWnd.setBounds( oDashedDiv.getLeft(), oDashedDiv.getTop(), oDashedDiv.getWidth(), oDashedDiv.getHeight() ) ;
        oDashedDiv.style.display = "none" ;
        oMDIFrame.style.cursor = "default" ;
        oMDIFrame.releaseCapture() ;
    };
}
// ======================================================================================================================================>
// AjaxTaskbar类
// ======================================================================================================================================>
function AjaxTaskbar( parent )
{
    var _oParentTask = parent ;
    var _oThisTask = this ;
    var aBtnList = new Array() ;

    var oBar = null ;
    var oLeft = null ;
    var oRight = null ;

    // 客户端方法
    this.addButton      = addButton ;        // 增加一个按钮
    this.deleteButton   = deleteButton ;     // 根据索引删除按钮
    this.setButtonState = setButtonState ;   // 设置按钮状态
    this.setButtonTitle = setButtonTitle ;   // 设置按钮文字
    this.setButtonIco   = setButtonIco ;     // 设置按钮图标
    
    this.getBtnList     = getBtnList ;       // ck.返回btn数组

    // 客户端事件
    this.onClick        = null ;             // 
    this.onDblClick     = null ;             // 

    // 内部工具方法
    this.create         = create ;           // 将自身创建到指定位置
// --------------------------------------------------------->
// 客户端方法
// --------------------------------------------------------->
    function addButton( sIco, sText, iIndex )
    {
        if( iIndex != -100 )
        { 
            aBtnList[iIndex] = new AjaxBarButton( sIco, sText ) ;
            aBtnList[iIndex].create( oBar ) ;
            aBtnList[iIndex].onClick = buttonClick ;
            aBtnList[iIndex].onDblClick = buttonDblClick ;
            aBtnList[iIndex]._index = iIndex ;
        }
        else
        {
            aBtnList[iIndex] = new AjaxBarButton( sIco, sText ) ;
            aBtnList[iIndex].create( oBar ) ;
            aBtnList[iIndex].onClick = _oParentTask ;
        }
        
    };

    function deleteButton( iIndex )
    {
        aBtnList[iIndex].dispose() ;
        delete aBtnList[iIndex] ;
    };

    function setButtonState( iIndex, bState )
    {
        if( bState )
        {
            aBtnList[iIndex].setBtnDown() ;
        }
        else
        {
            aBtnList[iIndex].setBtnUp() ;            
        }
    };

    function setButtonTitle( iIndex, sTitle )
    {
        aBtnList[iIndex].setTitle( sTitle ) ;
    };

    function setButtonIco( iIndex, sImgN )
    {
        aBtnList[iIndex].setIco( sImgN ) ;
    };
    
    function getBtnList() //ck.change
    {
        return aBtnList ;
    };
// --------------------------------------------------------->
// 内部工具方法
// --------------------------------------------------------->
    function create( oContainer, bodyEl )
    {
        oBar = document.createElement( '<div>' ) ;
        oBar.className = "ajaxWindow_taskbar_body" ;

        if( !_oThisTask.hcon )
        {
            oContainer.insertAdjacentElement( "beforeEnd", oBar ) ;
            _oThisTask.hcon = oContainer ;
        }
    };
    
// --------------------------------------------------------->
// 内部工具函数
// --------------------------------------------------------->
    /**
	 * 向上传递按钮点击事件
	 * @param iIndex integer 按钮索引
	 */
    function buttonClick( iIndex )
    {    		
        if( _oThisTask.onClick != null ) _oThisTask.onClick( iIndex ) ;
    };

    /**
	 * 向上传递按钮双击事件
	 * @param iIndex integer 按钮索引
	 */
    function buttonDblClick( iIndex )
    {
        if( _oThisTask.onDblClick != null ) _oThisTask.onDblClick( iIndex ) ;
    };
}
// ======================================================================================================================================>
// AjaxBarButton类
// ======================================================================================================================================>
function AjaxBarButton( sIco, sText )
{
    var _oThisButton = this ;
    var oBtn = null ;
    var oBtnLeft = null ;
    var oBtnText = null ;
    var oBtnRight = null ;
    var oBtnCloseIco = null ;
    var ofun = function() { return false; } ;

    // 创建方法
    this.create = function( oContainer )
    {
        if( !_oThisButton.hcon )
        {
            if( !oContainer.canHaveChildren )
            {
                throw "创建失败，create()方法的参数不是一个容器的id！" ;
                return ;
            }
            else
            {
                oContainer.insertAdjacentElement( "beforeEnd", oBtn ) ;
                _oThisButton.hcon = oContainer ;
            }
        }
    };

    // 销毁方法
    this.dispose = function()
    {
        oBtn.detachEvent( "onmousedown", btnClick ) ;
        oBtn.detachEvent( "ondblclick", btnDblClick ) ;
        oBtn.detachEvent( "onselectstart", ofun ) ;
        oBtn.removeNode( true ) ;
        oBtn = null ;
    };

    // 鼠标按下时的响应事件
    this.setBtnDown = function()
    {
        oBtn.className = "ajaxWindow_taskbar_button_selected" ;
        oBtnLeft.childNodes(0).src = ImagePath +"/tab_selected_left.gif"  ;
        
        oBtnText.style.backgroundImage = "url(" + ImagePath+"/tab_selected_center.gif )" ;
        oBtnCloseIco.style.backgroundImage = "url(" + ImagePath+"/tab_selected_center.gif)" ;
        oBtnRight.childNodes(0).src = ImagePath+"/tab_selected_right.gif" ;
    };

    // 鼠标释放时的响应事件
    this.setBtnUp = function()
    {
        oBtn.className = "ajaxWindow_taskbar_button" ;
        oBtnLeft.childNodes(0).src = ImagePath+"/tab_left.gif"  ;
        oBtnText.style.backgroundImage = "url(" + ImagePath+"/tab_center.gif)" ;
        oBtnCloseIco.style.backgroundImage = "url(" + ImagePath+"/tab_center.gif )" ;
        oBtnRight.childNodes(0).src = ImagePath+"/tab_right.gif"  ;
    };

    // 设置按钮标题
    this.setTitle = function( sTitle )
    {
        oBtn.title = sTitle ;
        oBtnText.innerText = sTitle ;
    };

    // 设置按钮图标
    this.setIco = function( sImgN )
    {
        if( sImgN.length <= 0 )
        {
            oImg.style.display = "none" ;
        }
        else
        {
            oImg.style.display = "" ;
            oImg.src = sImgN ;
        }
    };

    // 取得按钮对象
    this.getBtn = function() { return oBtn; } ;
    this.onClick=null ;
    this.onDblClick=null ;
    
    // 创建按钮
    oBtn = document.createElement( '<div align="center" title="' + sText + '">' ) ;
    oBtn.className = "ajaxWindow_taskbar_button" ;
    with( oBtn.style )
    {
        cursor="hand";
        display="inline";
    }

    // 创建按钮图片
    var oImg = new Image() ;
    oImg.style.width = "16" ;
    oImg.style.height = "16" ;
    
    //ck.change.del
    //oBtn.insertAdjacentElement( "afterBegin", oImg ) ;
    
    if( typeof sIco == "string" && sIco.length > 0 )
    {
        oImg.src = sIco ;
    }
    else
    {
        oImg.style.display = "none" ;
    }
    
    //ck.change.add 增加按钮左边线
    oBtnLeft = document.createElement( "div" ) ;
    with( oBtnLeft.style )
    {
        display      = "inline" ;
        overflow     = "hidden" ;
        textOverflow = "ellipsis" ;
        whiteSpace   = "nowrap" ;
    }
    oBtnLeft.innerHTML = "<img src='" + ImagePath.replace( "images", ImagePath+"/tab_selected_left.gif" ) + "' border='0' height='21' width='3'>"
    
    //ck.change.add 创建关闭图标
    oBtnCloseIco = document.createElement( "div" );
    oBtnCloseIco.style.backgroundImage = "url(" + ImagePath.replace( "images", ImagePath+"/tab_selected_center.gif" ) + ")" ;
    oBtnCloseIco.className = "ajaxWindow_taskbar_button_text" ;
    with( oBtnCloseIco.style )
    {
        height       = "21px" ;
        width        = "16px" ;
        display      = "inline" ;
        overflow     = "hidden" ;
        textOverflow = "ellipsis" ;
        whiteSpace   = "nowrap" ;
        marginTop    = "-21px" ;
        
    }
    oBtnCloseIco.innerHTML = "<img src='" + ImagePath + "/btn_close.gif' style='border:red 0px solid;height:11px;width:11px;margin-top:-2px;margin-left:3px;'>";
    oBtnCloseIco.attachEvent( "onmouseover", function(){ oBtnCloseIco.childNodes(0).style.border = "#a3a3a3 1px solid" ;} );
    oBtnCloseIco.attachEvent( "onmouseout", function(){ oBtnCloseIco.childNodes(0).style.border = "#a3a3a3 0px solid" ;} );
    oBtnCloseIco.attachEvent( "onclick", btnDblClick );    
    
    // 创建按钮文本
    oBtnText = document.createElement( "div" ) ;
    //ck.change.add 增加按钮背景
    oBtnText.style.backgroundImage = "url(" + ImagePath.replace( "images", ImagePath+"/tab_selected_center.gif" ) + ")" ;
    oBtnText.className = "ajaxWindow_taskbar_button_text" ;
    with( oBtnText.style )
    {
        height       = "21px" ;
        //width        = "100px" ;
        display      = "inline" ;
        overflow     = "hidden" ;
        textOverflow = "ellipsis" ;
        whiteSpace   = "nowrap" ;
        marginTop    = "-25px" ;
     }
    oBtnText.innerText = sText ;
    //oBtnText.innerHTML = "<table borer='0'><tr height='10'><td>"+sText+"</td><td><img src='" + ImagePath + "/btn_close.gif' style='border:red 1px solid;height:11px;width:11px;'></td></tr></table>" ;
    //oBtnText.innerHTML = sText+"<img src='" + ImagePath + "/btn_close.gif' style='border:red 1px solid;height:11px;width:11px;'>" ;
    
    //ck.change.add 增加按钮右边线
    oBtnRight = document.createElement( "div" ) ;
    with( oBtnRight.style )
    {
        display      = "inline" ;
        overflow     = "hidden" ;
        textOverflow = "ellipsis" ;
        whiteSpace   = "nowrap" ;
    }
    oBtnRight.innerHTML = "<img src='" + ImagePath.replace( "images", ImagePath+"/tab_selected_right.gif" ) + "' border='0' height='21' width='3'>"

    oBtn.attachEvent( "onmousedown", btnClick ) ;
    oBtn.attachEvent( "ondblclick", btnDblClick ) ;
    oBtn.attachEvent( "onselectstart", ofun ) ;
    oBtn.insertAdjacentElement( "beforeEnd", oBtnText ) ;
    oBtn.insertAdjacentElement( "beforeEnd", oBtnCloseIco ) ;
    oBtn.insertAdjacentElement( "beforeEnd", oBtnRight ) ;
    oBtn.insertAdjacentElement( "afterBegin", oBtnLeft ) ;
    
    function btnClick()
    {
        if( _oThisButton.onClick != null ) _oThisButton.onClick( _oThisButton._index ) ;
    }

    function btnDblClick()
    {
        if( _oThisButton.onDblClick != null ) _oThisButton.onDblClick( _oThisButton._index ) ;
    }
}
// ======================================================================================================================================>
// AjaxWindow类
// ======================================================================================================================================>
function AjaxWindow( parent )
{
    var _oThis = parent ;
    var _oThisChild = this ;

    this._index = null ;
    this._hcon = null ;

    var oWindow = null ;
    var oWindowIco = null ;
    var oWindowCaption = null ;
    var oMinimizeButton = null ;
    var oMaximizeButton = null ;
    var oCaptionTitle = null ;
    var oCloseButton = null ;
    var oContentPane = null;
    var canDrag = true ;
    var oBackFrm = null ;
    //hehai
    var loc = null;

    var oFunNoSelectA = function() { return false ; };
    var oFunNoSelectB = function() { if( event.srcElement.tagName != "INPUT" && event.srcElement.tagName != "AREA" ) return false ; };

    // 客户端方法
    this.getCaption         = getCaption ;                // 获取标题
    this.setCaption         = setCaption ;                // 设置标题
    this.getShowMinimize    = getShowMinimize ;           // 获取是否显示最小化按钮
    this.setShowMinimize    = setShowMinimize ;           // 设置是否显示最小化按钮
    this.getShowMaximize    = getShowMaximize ;           // 获取是否显示最大化按钮
    this.setShowMaximize    = setShowMaximize ;           // 设置是否显示最大化按钮
    this.getShowClose       = getShowClose ;              // 获取是否显示关闭按钮
    this.setShowClose       = setShowClose ;              // 设置是否显示关闭按钮
    this.getShowCaption     = getShowCaption ;            // 获取是否显示标题栏
    this.setShowCaption     = setShowCaption ;            // 设置是否显示标题栏
    this.getShowIco         = getShowIco ;                // 获取是否显示标题栏图标
    this.setShowIco         = setShowIco ;                // 设置是否显示标题栏图标
    this.getCanMinimize     = getCanMinimize ;            // 获取是否能够最小化
    this.setCanMinimize     = setCanMinimize ;            // 设置是否能够最小化
    this.getCanResizable    = getCanResizable ;           // 获取是否可以调整大小
    this.setCanResizable    = setCanResizable ;           // 设置是否可以调整大小
    this.getCanDragging     = getCanDragging ;            // 获取是否可以拖动
    this.setCanDragging     = setCanDragging ;            // 设置是否可以拖动
    this.getTop             = getTop ;                    // 获取垂直距离
    this.setTop             = setTop ;                    // 设置垂直距离
    this.getLeft            = getLeft ;                   // 获取水平距离
    this.setLeft            = setLeft ;                   // 设置水平距离
    this.getWidth           = getWidth ;                  // 获取宽度
    this.setWidth           = setWidth ;                  // 设置宽度
    this.getHeight          = getHeight ;                 // 获取高度
    this.setHeight          = setHeight ;                 // 设置高度
    this.setSize            = setSize ;                   // 设置大小
    this.setBounds          = setBounds ;                 // 设置窗口范围
    this.setLocation        = setLocation ;               // 设置位置
    this.getActive          = getActive ;                 // 获取窗口是否是当前窗口
    this.getState           = getState ;                  // 获取窗口状态
    this.getWindowIco       = getWindowIco ;              // 获取标题栏图标地址
    this.setWindowIco       = setWindowIco ;              // 设置标题栏图标地址
    this.minimize           = onMinimizeButtonClick ;     // 最小化窗口
    this.maximize           = onMaximizeButtonClick ;     // 最大化窗口
    this.close              = onCloseButtonClick ;        // 关闭窗口
    
    //ck.change.add
    this.onMinimizeButtonMouseOver  = onMinimizeButtonMouseOver ;    //最小化按钮鼠标覆盖/移出效果
    this.onMinimizeButtonMouseOut   = onMinimizeButtonMouseOut ;     //最小化按钮鼠标覆盖/移出效果
    this.onMaximizeButtonMouseOver  = onMaximizeButtonMouseOver ;    //最大化按钮鼠标覆盖/移出效果
    this.onMaximizeButtonMouseOut   = onMaximizeButtonMouseOut ;     //最大化按钮鼠标覆盖/移出效果
    this.onCloseButtonMouseOver     = onCloseButtonMouseOver ;       //关闭按钮鼠标覆盖/移出效果
    this.onCloseButtonMouseOut      = onCloseButtonMouseOut ;        //关闭按钮鼠标覆盖/移出效果
    
    this.fire               = fire ;                      // 创建控件
    this.add                = add ;                       // 添加一个子窗口
    this.getClientSite      = getClientSite ;             // 返回窗口的用户可用区域
    this.addHTML            = addHTML ;                   // 向窗口用户区域添加HTML代码

    // 客户端事件
    this.onSetActive        = null ;
    this.onstartResize      = null ;
    this.onMinimize         = null ;
    this.onMaximize         = null ;
    this.onClose            = null ;
    this.onCaptionChange    = null ;
    this.onIcoChange        = null ;

    // 内部工具方法
    this.windowMinimize     = windowMinimize ;            // 窗口最小化
    this.windowMaximize     = windowMaximize ;            // 窗口最大化
    this.getMinimumWidth    = getMinimumWidth ;           // 获取窗口可调整的最小宽度
    this.getMinimumHeight   = getMinimumHeight ;          // 获取窗口可调整的最小高度
    this.setState           = setState ;                  // 设置窗口状态
    this.setActive          = setActive ;                 // 设置窗口是否是当前窗口
    this.getWindow          = getWindow ;                 // 返回窗口句柄(窗体DIV元素的对象引用)
    this.getZIndex          = getZIndex ;                 // 返回窗口索引
    this.setZIndex          = setZIndex ;                 // 设置窗口索引
    this.getOriginZIndex    = getOriginZIndex ;           // 返回窗口历史索引
    this.getResizeDir       = getResizeDir ;              // 返回窗口调整大小的方向
    this.dispose            = dispose ;                   // 销毁窗口及其对象
    this.setLinkPath        = setLinkPath ;
    this.setloc 						= setloc;											// 设置地址
    this.getloc 						= getloc;	

    var imageArray = new Array() ;
    imageArray[0] = new Image() ;
    imageArray[1] = new Image() ;
    imageArray[2] = new Image() ;
    imageArray[3] = new Image() ;
    
    //ck.change.add
    imageArray[4] = new Image() ;
    imageArray[5] = new Image() ;
    imageArray[6] = new Image() ;
    imageArray[7] = new Image() ;

    imageArray[0].src = ImagePath + "/JsControlWindow_minimize.gif" ;
    imageArray[1].src = ImagePath + "/JsControlWindow_maximize.gif" ;
    imageArray[2].src = ImagePath + "/JsControlWindow_close.gif" ;
    imageArray[3].src = ImagePath + "/JsControlWindow_normal.gif" ;
    
    //ck.change.add
    imageArray[4].src = ImagePath + "/JsControlWindow_minimize1.gif" ; 
    imageArray[5].src = ImagePath + "/JsControlWindow_maximize1.gif" ;
    imageArray[6].src = ImagePath + "/JsControlWindow_close1.gif" ;
    imageArray[7].src = ImagePath + "/JsControlWindow_normal1.gif" ;
    
// --------------------------------------------------------->
// 客户端方法
// --------------------------------------------------------->
    function setLinkPath( src )
    {    
    	  setloc(src);
        oBackFrm.src = src ;  
        //alert(getloc());
    }
    
    function getloc()
    {
    		return oBackFrm.loc;
   	}
    
    function setloc(src)
    {    	
    		var pos;
    		pos = src.indexOf("reftime");
    		var temp;
    		temp = src.substring(0,pos -1);    		
    		oBackFrm.loc = temp ; 
    }
    
    function getCaption()
    {
        if( oCaptionTitle )
        {
            return oCaptionTitle.innerText ;
        }
        else
        {
            return "" ;
        }
    };

    function setCaption( sCaption )
    {
        if( oCaptionTitle )
        {
            oCaptionTitle.innerText = sCaption ;
        }
        if( _oThisChild.onCaptionChange != null )
        {
            _oThisChild.onCaptionChange( _oThisChild._index ) ;
        }
    };

    function getShowClose()
    {
        return oCloseButton.style.display == "none" ? false : true ;
    };

    function setShowClose( isShowClose )
    {
        if( isShowClose )
        {
            oCloseButton.style.display = "" ;
        }
        else
        {
            oCloseButton.style.display = "none" ;
        }

        setCaptionTitleW() ;
    };

    function getShowMaximize()
    {
        return oMaximizeButton.style.display == "none" ? false : true ;
    };

    function setShowMaximize( isShowMaximize )
    {
        if( isShowMaximize )
        {
            oMaximizeButton.style.display = "" ;
        }
        else
        {
            oMaximizeButton.style.display = "none" ;
        }

        setCaptionTitleW() ;
    };

    function getShowMinimize()
    {
        return oMinimizeButton.style.display == "none" ? false : true ;
    };

    function setShowMinimize( isShowMinimize )
    {
        if ( isShowMinimize )
        {
            oMinimizeButton.style.display = "" ;
        }
        else
        {
            oMinimizeButton.style.display = "none" ;
        }

        setCaptionTitleW() ;
    };

    function getShowCaption()
    {
        return oWindowCaption.style.display == "none" ? false : true ;
    };

    function setShowCaption( isShowCaption )
    {
        if( isShowCaption )
        {
            oWindowCaption.style.display = "" ;
        }
        else
        {
            oWindowCaption.style.display = "none" ;
        }

        setChildrenSize() ;
    };

    function getShowIco()
    {
        return oWindowIco.style.display == "none" ? false : true ;
    };

    function setShowIco( isShowIco )
    {
        if( isShowIco )
        {
            oWindowIco.style.display = "" ;
        }
        else
        {
            oWindowIco.style.display = "none" ;
        }

        setCaptionTitleW() ;
    };

    function onCloseButtonClick()
    {
        oCloseButton.className = "ajaxWindow_window_button" ;
        if( _oThisChild.onClose != null )
        {
            _oThisChild.onClose( _oThisChild._index ) ;
        }
        else
        {
            oWindow.removeNode( true ) ;
        }
    }
    
    //ck.change.add
    function onMinimizeButtonMouseOver()
    {
        oMinimizeButton.childNodes(0).src = imageArray[4].src ;
    };
          
    function onMinimizeButtonMouseOut()
    {
        oMinimizeButton.childNodes(0).src = imageArray[0].src ;
    };       
    function onMaximizeButtonMouseOver()
    {
        if( oMaximizeButton.childNodes(0).src == imageArray[3].src ) oMaximizeButton.childNodes(0).src = imageArray[7].src ;
        else oMaximizeButton.childNodes(0).src = imageArray[5].src ;
        
    };      
    function onMaximizeButtonMouseOut()
    {
        if( oMaximizeButton.childNodes(0).src == imageArray[7].src ) oMaximizeButton.childNodes(0).src = imageArray[3].src ;
        else  oMaximizeButton.childNodes(0).src = imageArray[1].src ;
    };       
    function onCloseButtonMouseOver()
    {
        oCloseButton.childNodes(0).src = imageArray[6].src ;
    };         
    function onCloseButtonMouseOut()
    {
        oCloseButton.childNodes(0).src = imageArray[2].src ;
    };          

    function onMinimizeButtonClick()
    {
        if( getCanMinimize() )
        {
            oMinimizeButton.className = "ajaxWindow_window_button" ;
            if ( _oThisChild.onMinimize != null )
            {
                _oThisChild.onMinimize( _oThisChild ) ;
            }
            else
            {
                windowMinimize() ;
            }
        }
    };

    function onMaximizeButtonClick()
    {
        if( getCanResizable() )
        {
            oMaximizeButton.className = "ajaxWindow_window_button" ;
            if ( _oThisChild.onMaximize != null )
            {
                _oThisChild.onMaximize( _oThisChild ) ;
            }
            else
            {
                windowMaximize() ;
            }
        }
    };

    function getCanMinimize()
    {
        return !oMinimizeButton.disabled ;
    };

    function setCanMinimize( canMinimize )
    {
        oMinimizeButton.disabled = !canMinimize ;
    };

    function getCanResizable()
    {
        return oWindow.resizable ;
    };

    function setCanResizable( canResizable )
    {
        oWindow.resizable = canResizable ;
        oMaximizeButton.disabled = !canResizable ;
    };

    function getCanDragging()
    {
        return canDrag ;
    };

    function setCanDragging( canDragging )
    {
        canDrag = canDragging ;
    };

    function getTop()
    {
        if( oWindow != null )
        {
            return parseInt( oWindow.style.top, 10 ) ;
        }
        else
        {
            return null ;
        }
    };

    function setTop( top )
    {
        if ( oWindow == null ) return ;
        oWindow.style.top = top ;
    };

    function getLeft()
    {
        if( oWindow != null )
        {
            return parseInt( oWindow.style.left, 10 ) ;
        }
        else
        {
            return null ;
        }
    };

    function setLeft( left )
    {
        if( oWindow == null ) return ;
        oWindow.style.left = left ;
    };

    function getWidth()
    {
        if( oWindow != null )
        {
            if( parseInt( oWindow.offsetWidth, 10 ) == 0 )
            {
                return parseInt( oWindow.style.width, 10 ) ;
            }
            else
            {
                return parseInt( oWindow.offsetWidth, 10 ) ;
            }
        }
        else
        {
            return null ;
        }
    };

    function setWidth( width )
    {
        if( oWindow == null )
        {
            return ;
        }
        oWindow.style.width = width ;
        setCaptionTitleW() ;
    };

    function getHeight()
    {
        if( oWindow != null )
        {
            if( parseInt( oWindow.offsetHeight, 10 ) == 0 )
            {
                return parseInt( oWindow.style.height, 10 ) ;
            }
            else
            {
                return parseInt( oWindow.offsetHeight, 10 ) ;
            }
        }
        else
        {
            return null ;
        }
    };

    function setHeight( height )
    {
        if( oWindow == null ) return ;
        oWindow.style.height = height ;
        setcontentPaneH() ;
    };

    function setSize( width, height )
    {
        setWidth( width ) ;
        setHeight( height ) ;
    };

    function setLocation( top, left )
    {
        setTop( top ) ;
        setLeft( left ) ;
    };

    function setBounds( left, top, width, height )
    {
        setLeft( left ) ;
        setTop( top ) ;
        setWidth( width ) ;
        setHeight( height ) ;
    };

    function getActive()
    {
        return oWindow.active ;
    };
    
    function getState()
    {
        return oWindow.state ;
    };

    function getWindowIco()
    {
        return oWindowIco.src ;
    };

    function setWindowIco( sImgN )
    {
        oWindowIco.src = sImgN ;
        
        if( !getShowIco() ) setShowIco( true ) ;
        if( _oThisChild.onIcoChange != null ) _oThisChild.onIcoChange( _oThisChild ) ;
    };

    function fire()
    {
        createWindow() ;
        _oThis.insertAdjacentElement( "beforeEnd", oWindow ) ;
    };
    
    function add( oChildren )
    {
        getClientSite().insertAdjacentElement( "beforeEnd", oChildren ) ;
    };
    
    function getClientSite()
    {
        return oContentPane ;
    };

    function addHTML( sHtmlString )
    {
        getClientSite().insertAdjacentHTML( "beforeEnd", sHtmlString ) ;
    };
// --------------------------------------------------------->
// 内部工具方法
// --------------------------------------------------------->
    function getMinimumWidth()
    {
        if( oWindow != null )
        {
            return oWindow.MinimumWidth ;
        }
        else
        {
            return null ;
        }
    };

    function getMinimumHeight()
    {
        if( oWindow != null )
        {
            return oWindow.MinimumHeight ;
        }
        else
        {
            return null ;
        }
    };

    function setState( sState )
    {
        oWindow.state = sState ;
    };
    
    function setActive( bActive )
    {
        if( bActive != getActive() )
        {
            if( bActive )
            {
                oWindow.className = "ajaxWindow_window_body_active"
                oWindowCaption.className = "ajaxWindow_window_caption_active" ;
                oCaptionTitle.className = "ajaxWindow_window_caption_active ajaxWindow_window_caption_text" ;
                oWindow.active = true ;
            }
            else
            {
                oWindow.className = "ajaxWindow_window_body" ;
                oWindowCaption.className = "ajaxWindow_window_caption" ;
                oCaptionTitle.className = "ajaxWindow_window_caption ajaxWindow_window_caption_text" ;
                oWindow.active = false ;
            }
        }
    };

    function getWindow()
    {
        if( oWindow != null )
        {
            return oWindow ;
        }
        else
        {
            return null ;
        }
    };
    
    function getOriginZIndex()
    {
        if( oWindow.zIndex != null ) return parseInt( oWindow.zIndex, 10 ) ;
    };
    
    function getZIndex()
    {
        return parseInt( oWindow.style.zIndex, 10 ) ;
    };
    
    function setZIndex( index )
    {
        oWindow.style.zIndex = index ;
        if( oWindow.zIndex == null ) oWindow.zIndex = index ;
    };

    function windowMaximize()
    {
        switch( getState() )
        {
            case "minimized":
                oMinimizeButton.innerHTML = getButtonSpan( 0 ) ;
                oWindow.maxBoundsData.left = oWindow.minBoundsData.left ;
                oWindow.maxBoundsData.top = oWindow.minBoundsData.top ;
                oWindow.maxBoundsData.width = oWindow.minBoundsData.width ;
                oWindow.maxBoundsData.height = oWindow.minBoundsData.height ;
                oMaximizeButton.innerHTML = getButtonSpan( 3 ) ;
                setBounds( 0, 0, getParentWidth(), getParentHeight() ) ;
                setState( "maximized" ) ;
                break ;

            case "normal":
                oWindow.maxBoundsData = { left:getLeft(), top:getTop(), width:getWidth(), height:getHeight() } ;
                oMaximizeButton.innerHTML = getButtonSpan( 3 ) ;
                setBounds( 2, 2, getParentWidth() - 4, getParentHeight() - 4 ) ;
                setState( "maximized" ) ;
                break ;

            case "maximized":
                setState( "normal" ) ;
                oMaximizeButton.innerHTML = getButtonSpan( 1 ) ;
                setBounds( oWindow.maxBoundsData.left, oWindow.maxBoundsData.top, oWindow.maxBoundsData.width, oWindow.maxBoundsData.height ) ;
                break ;
        }
    };
    
    function windowMinimize()
    {
        if( getState() == "minimized" )
        {
            oWindow.style.display = "block" ;
            setState( oWindow.minBoundsData.state ) ;
            oMinimizeButton.innerHTML = getButtonSpan( 0 ) ;

            if( oWindow.minBoundsData.state == "normal" )
            {
                oMaximizeButton.innerHTML = getButtonSpan( 1 ) ;
            }
            else
            {
                oMaximizeButton.innerHTML = getButtonSpan( 3 ) ;
            }

            setBounds( oWindow.minBoundsData.left, oWindow.minBoundsData.top, oWindow.minBoundsData.width, oWindow.minBoundsData.height ) ;
        }
        else
        {
            oWindow.minBoundsData = { left:getLeft(), top:getTop(), width:getWidth(), height:getHeight(), state:getState() } ;
            oMinimizeButton.innerHTML = getButtonSpan( 2 ) ;
            oMaximizeButton.innerHTML = getButtonSpan( 1 ) ;
            setState( "minimized" ) ;
            oWindow.style.display = "none" ;
        }
    };
    
    function getResizeDir()
    {
        return oWindow.resizeDir ;
    };

    function dispose()
    {
        oWindowIco.detachEvent( "ondblclick", onCloseButtonClick ) ;
        oWindowCaption.detachEvent( "onselectstart", oFunNoSelectA ) ;
        oMinimizeButton.detachEvent( "onclick", onMinimizeButtonClick ) ;
        oMaximizeButton.detachEvent( "onclick", onMaximizeButtonClick ) ;
        oCloseButton.detachEvent( "onclick", onCloseButtonClick ) ;
        oMinimizeButton.detachEvent( "onfocus", oMinimizeButton.blur ) ;
        oMaximizeButton.detachEvent( "onfocus", oMaximizeButton.blur ) ;
        oCloseButton.detachEvent( "onfocus", oCloseButton.blur ) ;
        oWindowCaption.MoveHandle.detachEvent( "onmousedown", moveStart ) ;
        oWindowCaption.MoveHandle.detachEvent( "ondblclick", onMaximizeButtonClick ) ;
        oWindow.detachEvent( "onmousemove", checkForResize ) ;
        oWindow.detachEvent( "onmousedown", onWindowClick ) ;
        oWindow.detachEvent( "onresize", setChildrenSize ) ;
        oContentPane.detachEvent( "onselectstart", oFunNoSelectB ) ;
        oContentPane.detachEvent( "onfocus", oContentPane.blur ) ;
        oWindowIco = null ;
        oWindowCaption.MoveHandle = null ;
        oWindowTitle = null ;
        oWindowCaption = null ;
        oMinimizeButton = null ;
        oMaximizeButton = null ;
        oCloseButton = null ;
        oContentPane = null ;
        oWindow.removeNode( true ) ;
        oWindow = null ;
    };
// --------------------------------------------------------->
// 内部工具函数
// --------------------------------------------------------->
    /**
	 * 返回父容器宽度
	 * @return 父容器宽度
	 */
    function getParentWidth()
    {
        if( _oThis == null ) return ;
        return parseInt( _oThis.offsetWidth, 10 ) ;
    };

    /**
	 * 返回父容器高度
	 * @return 父容器高度
	 */
    function getParentHeight()
    {
        if( _oThis == null ) return ;
        return parseInt( _oThis.offsetHeight, 10 ) ;
    };

    /**
	 * 返回一个按钮HTML文本
	 * 示例：getButtonSpan( "0" ) ;
	 *
	 * @param sSpanText string 按钮文本
	 * @return 按钮HTML文本
	 */
    function getButtonSpan( sSpanText )
    {
        return "<img style='width:21px;height:21px' src='" + imageArray[sSpanText].src + "' />" //16*16
    };

    /**
	 * 返回一个指定类型的按钮
	 * 示例：getCaptionButton( "0" ) ;
	 *
	 * @param sSpanText string 按钮类型
	 * @return 按钮对象
	 */
    function getCaptionButton( sBtnType )
    {
        var oBtn = document.createElement( "span" ) ;
        with( oBtn.style )
        {
            textAlign       = "center" ;
            width           = "21px" ;  //(16px;)ck.change.edit
            height          = "21px" ;  //(16px;)ck.change.edit
            overflow        = "hidden" ;
            padding         = "0" ;
            cursor          = "default" ;
        }

        oBtn.innerHTML = getButtonSpan( sBtnType ) ;
        oBtn.className = "ajaxWindow_window_button" ;
        oBtn.style.marginLeft = 2 ;
        
        //oBtn.attachEvent( "onmouseover", changeButtonClassName ) ;
        //oBtn.attachEvent( "onmouseout", changeButtonClassName ) ;

        return oBtn ;
    };

    /**
	 * 标题栏按钮鼠标响应事件
	 */
    function changeButtonClassName()
    {
        if( event.srcElement.parentElement.className == "ajaxWindow_window_button" )
        {
            if( getActive() )
            {
                event.srcElement.parentElement.className = "ajaxWindow_window_button_active_hover" ;
            }
            else
            {
                event.srcElement.parentElement.className = "ajaxWindow_window_button_hover" ;
            }
        }
        else
        {
            event.srcElement.parentElement.className = "ajaxWindow_window_button" ;
        }
    };

    /**
	 * 返回标题栏宽度
	 * @return 标题栏宽度
	 */
    function getTitleWidth()
    {
        if( oCaptionTitle ) return oCaptionTitle.offsetWidth ;
    };

    /**
	 * 设置标题栏宽度
	 * @param width string 宽度值
	 */
    function setTitleWidth( width )
    {
        if( oCaptionTitle ) oCaptionTitle.style.width = width ;
    };

    /**
	 * 返回标题栏高度
	 * @return 标题栏高度
	 */
    function getTitleHeight()
    {
        if( oCaptionTitle ) return oCaptionTitle.offsetHeight ;
    };

    /**
	 * 设置标题栏高度
	 * @param height string 高度值
	 */
    function setTitleHeight( height )
    {
        if( oCaptionTitle ) oCaptionTitle.style.height = height ;
    };

    /**
	 * 创建标题栏文本框
	 */
    function createCaptionTitle()
    {
        var oDiv = document.createElement( "div" ) ;

        with( oDiv.style )
        {
            oDiv.style.width        ="100%" ;
            oDiv.style.height       ="18px" ;
            oDiv.style.overflow     ="hidden" ;
            oDiv.style.textOverflow ="ellipsis" ;
            oDiv.style.whiteSpace   ="nowrap" ;

        }
        oCaptionTitle = oDiv ;
        oCaptionTitle.className = "ajaxWindow_window_caption ajaxWindow_window_caption_text" ;
    };

    /**
	 * 创建一个窗口图标
	 * @param sImgName string 图片路径和名称
	 */
    function createWindowIco( sImgName )
    {
        oWindowIco=document.createElement( "img" ) ;
        oWindowIco.style.width = 16 ;
        oWindowIco.style.height = 16 ;
        oWindowIco.style.display = "none" ;
        if ( sImgName != null && sImgName.length > 0 ) oWindowIco.src = sImgName ;
        oWindowIco.attachEvent( "ondblclick", onCloseButtonClick ) ;
    };

    /**
	 * 创建一个标题栏
	 * @param oHandleFor object 标题所属容器
	 */
    function createCaption( oHandleFor )
    {
        var oDiv = document.createElement( "div" ) ;
        oDiv.style.width = "100%" ;
        //ck.change.add
        oDiv.style.backgroundImage = "url('" + ImagePath + "/window_bg.jpg')" ;
        oDiv.style.margin = "0 0 0 0" ;
        oDiv.attachEvent( "onselectstart", oFunNoSelectA ) ;

        createWindowIco() ;
        createCaptionTitle() ;
        
        oMinimizeButton = getCaptionButton( "0" ) ;
        oMinimizeButton.attachEvent( "onclick", onMinimizeButtonClick ) ;
        oMinimizeButton.attachEvent( "onmouseover", onMinimizeButtonMouseOver ) ;
        oMinimizeButton.attachEvent( "onmouseout", onMinimizeButtonMouseOut ) ;
        
        oMaximizeButton = getCaptionButton( "1" ) ;
        oMaximizeButton.attachEvent( "onclick", onMaximizeButtonClick ) ;
        oMaximizeButton.attachEvent( "onmouseover", onMaximizeButtonMouseOver ) ;
        oMaximizeButton.attachEvent( "onmouseout", onMaximizeButtonMouseOut ) ;

        oCloseButton = getCaptionButton( "2" ) ;
        oCloseButton.attachEvent( "onclick", onCloseButtonClick ) ;
        oCloseButton.attachEvent( "onmouseover", onCloseButtonMouseOver ) ;
        oCloseButton.attachEvent( "onmouseout", onCloseButtonMouseOut ) ;
        
        var oTable = document.createElement( "table" ) ;
        oTable.style.width = "100%" ;
        oTable.style.height = "23px" ; //(18px)ck.change.edit
        oTable.border = 0 ;
        oTable.cellPadding = 0 ;
        oTable.cellSpacing = 0 ;

        var oTr1 = oTable.insertRow() ;
        var oTd1 = oTr1.insertCell() ;
        oTd1.style.paddingLeft = "2" ;
        oTd1.insertAdjacentElement( "beforeEnd", oWindowIco ) ;

        var oTd2 = oTr1.insertCell() ;
        oTd2.style.width = "100%" ;
        oTd2.style.cursor = "default" ;
        oTd2.insertAdjacentElement( "beforeEnd", oCaptionTitle ) ;
        oTd2.attachEvent( "onmousedown", moveStart ) ;
        oTd2.attachEvent( "ondblclick", onMaximizeButtonClick ) ;
        
        var oTd3 = oTr1.insertCell() ;
        oTd3.noWrap = true ;
        //oTd3.style.border = "red 1px solid";//
        oTd3.insertAdjacentElement( "beforeEnd", oMinimizeButton ) ;
        oTd3.insertAdjacentElement( "beforeEnd", oMaximizeButton ) ;
        oTd3.insertAdjacentElement( "beforeEnd", oCloseButton ) ;
        oDiv.insertAdjacentElement( "beforeEnd", oTable ) ;
        oWindowCaption = oDiv ;
        oWindowCaption.MoveHandle = oTd2 ;
        oWindowCaption.className = "ajaxWindow_window_caption" ;
    };

    /**
	 * 创建一个窗口
	 */
    function createWindow()
    { 
        oWindow = document.createElement( '<div unselectable="on">' ) ;
        oWindow.moveData = {} ;
        oWindow.resizeData = {} ;
        oWindow.maxBoundsData = {} ;
        oWindow.minBoundsData = {} ;
        oWindow.resizeDir = "" ;
        oWindow.resizable = true ;
        oWindow.active = false ;
        oWindow.state = "normal" ;
        oWindow.MinimumWidth = 100 ;
        oWindow.MinimumHeight = 26 ;
        oWindow.attachEvent( "onmousemove", checkForResize) ;
        oWindow.attachEvent( "onmousedown", onWindowClick) ;
        oWindow.attachEvent( "onresize", setChildrenSize) ;
        oWindow.className = "ajaxWindow_window_body" ;
        
        with( oWindow.style )
        {
            position        = "absolute" ;
            overflow        = "hidden" ;
            border          = "2px solid" ;
            zIndex          = "10000" ;
            top             = "2px" ;
            left            = "2px" ;
            width           = "700px" ;
            height          = "200px" ;
        }

        createCaption( oWindow ) ;
        oWindow.insertAdjacentElement( "afterBegin", oWindowCaption ) ;

        oContentPane = document.createElement( 'div' ) ;
        oContentPane.attachEvent( "onselectstart", oFunNoSelectB ) ;

        with( oContentPane.style )
        {
            position = "relative" ;
            overflow = "hidden" ;
            border   = "0px" ;
            width    = "100%" ;
        }

        oContentPane.attachEvent( "onfocus", oContentPane.blur ) ;

        oBackFrm = document.createElement("iframe") ;
        oBackFrm.style.width = "100%" ;
        oBackFrm.style.height = "100%" ;
        oBackFrm.frameBorder = "0" ;
        oContentPane.insertAdjacentElement( "afterBegin", oBackFrm ) ;

        oWindow.insertAdjacentElement( "beforeEnd", oContentPane) ;
        setCaption( "" ) ;  
    };

    /**
	 * 设置标题栏文字宽度
	 */
    function setCaptionTitleW()
    {
        var iS = parseInt( oWindow.style.width, 10 ) ;
        
        if( oWindowIco.style.display != "none" ) iS -= 16 ;
        if( oMinimizeButton.style.display != "none" ) iS -= parseInt( oMinimizeButton.style.width ) ;
        if( oMaximizeButton.style.display != "none" ) iS -= parseInt( oMaximizeButton.style.width ) ;
        if( oCloseButton.style.display != "none" ) iS -= parseInt( oCloseButton.style.width ) ;
        
        iS = ( iS-16 ) < 0 ? 0 : iS - 16 ;
        setTitleWidth( iS - 16 ) ;
    };

    /**
	 * 设置容器高度
	 */
    function setcontentPaneH()
    {
        var iH = getHeight() - (parseInt( getWindow().style.border, 10 )) * 2 ;
        iH -= parseInt( oWindowCaption.offsetHeight, 10 ) ;

        if( getShowCaption() )
        {
            iH -= parseInt( oWindowCaption.style.marginTop, 10 ) ;
            iH -= parseInt( oWindowCaption.style.marginBottom, 10 ) ;
        }

        iH -= 2 ;
        oContentPane.style.height = iH < 0 ? 0 : iH ;
    };

    /**
	 * 调整标题栏文字宽度和容器高度
	 */
    function setChildrenSize()
    {
        setCaptionTitleW() ;
        setcontentPaneH() ;
    };

    /**
	 * 窗口被点击触发的事件
	 */
    function onWindowClick()
    {
        if( _oThisChild.onSetActive ) _oThisChild.onSetActive( _oThisChild ) ;
        if( _oThisChild.onstartResize && getResizeDir() != "" && event.srcElement == getWindow() ) _oThisChild.onstartResize() ;
    };

    /**
	 * 开始窗口移动
	 */
    function moveStart()
    {
        if( !getCanDragging() ) return ;
        if( getState() == "maximized" ) return ;

        oWindowCaption.MoveHandle.attachEvent( "onmouseup", moveEnd ) ;
        oWindowCaption.MoveHandle.attachEvent( "onmousemove", continueMove ) ;

        oWindow.moveData =
        {
            screenX:event.clientX,
            screenY:event.clientY,
            startX:oWindow.style.pixelLeft,
            startY:oWindow.style.pixelTop
        };

        oWindowCaption.MoveHandle.setCapture( true ) ;
    };

    /**
	 * 停止窗口移动
	 */
    function moveEnd()
    {
        oWindowCaption.MoveHandle.detachEvent( "onmouseup", moveEnd ) ;
        oWindowCaption.MoveHandle.detachEvent( "onmousemove", continueMove ) ;
        oWindowCaption.MoveHandle.releaseCapture() ;
    };

    /**
	 * 移动窗口
	 */
    function continueMove()
    {
        var iX = oWindow.moveData.startX + event.clientX - oWindow.moveData.screenX ;
        var iY = oWindow.moveData.startY + event.clientY - oWindow.moveData.screenY ;

        iX = iX <- getTitleWidth() ? - getTitleWidth() : iX ;
        iY = iY <- getTitleHeight() ? - getTitleHeight() : iY ;
        iX = iX > getParentWidth() - 30 ? getParentWidth() - 30 : iX ;
        iY = iY > getParentHeight() - getTitleHeight() ? getParentHeight() - getTitleHeight() : iY ;
        
        setLeft( iX ) ;
        setTop( iY ) ;
        oWindow.minBoundsData.left = iX ;
        oWindow.minBoundsData.top = iY ;
    };

    /**
	 * 根据鼠标所处在窗口的位置,更改鼠标类型
	 */
    function checkForResize()
    {
        if( !oWindow.resizable || getState() == "minimized" || getState() == "maximized" )
        {
            oWindow.resizeDir = "" ;
            return ;
        }

        var resizeDir = "" ;
        var iResponseBound = 4 ;
        if( event.srcElement == oWindow )
        {
            var oy = event.offsetY ;
            if( oy <= iResponseBound )
            {
                resizeDir += "n" ;
            }
            else if( oy >= getHeight() - iResponseBound )
            {
                resizeDir += "s" ;
            }

            var ox = event.offsetX ;
            if( ox <= iResponseBound )
            {
                resizeDir += "w" ;
            }
            else if( ox >= getWidth() - iResponseBound )
            {
                resizeDir += "e" ;
            }

            if( resizeDir != "" )
            {
                oWindow.style.cursor = resizeDir + "-resize" ;
                oWindow.resizeDir = resizeDir ;
            }
            else
            {
                oWindow.style.cursor = "default" ;
                oWindow.resizeDir = "" ;
            }
        }
        else
        {
            oWindow.style.cursor = "default" ;
            oWindow.resizeDir = "" ;
        }
    };
}
