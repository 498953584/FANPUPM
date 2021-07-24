
//自定义Table对象
//可以实现表格滑动，点击变色效果
function JustWinTable(tableID,overColor,selectColor){
	//Table对象
	if(!document.getElementById(tableID)) return;
	this.table = document.getElementById(tableID);
	//鼠标经过TR时的颜色
	this.overColor = overColor || '#F6FBFE';  //#F6FBFE   #F5FBFE
	//鼠标单击TR时的颜色
	this.selectColor= selectColor|| '#EAF4FD';
	//初始化方法
	this.initTable();
	//若Table含有CheckBox，则添加全选事件
	/*if(this._isContainsTr()){
	    this._registCheckBoxListener();
	    this._registSingleCheckBox();
	}*/
	
	//可拖动列宽
	//var resizeTable = new ColumnResize(this.table);
}

JustWinTable.prototype.initTable = function(){
	//为支持FireFox
	//tbody.ChildNodes.length != tbody.getElementsByTagName('TR').length;
	var trs = this.table.getElementsByTagName('TR');
	for(var i = 1; i< trs.length; i++){
		trs[i].oldColor = trs[i].style.backgroundColor;
		//把当前对象和索引传递，给TR添加参数
		//避免闭包的问题
		setTrEvent(this,i);
    }
      
	function setTrEvent(jwTable,index){
	   //当点击TR时，给Table对象添加自定义属性
	   //selectIndex表示单击TR的索引
	   //selectTR表示当前单击的TR
	   //selectOldColor表示单击前的Color
	   var table = jwTable.table;
	   var trs = table.getElementsByTagName('TR');
	   //用于全选后移除鼠标滑过事件
	   //var chk = jwTable._getFirstCheckBox(trs[0]);
	   var overColor = jwTable.overColor;
	   var selectColor = jwTable.selectColor;
	   var tr = trs[index];
        addEvent(tr,'mouseover',function(){
            //var iChk = jwTable._getFirstCheckBox(tr); 
            //if(iChk && iChk.checked) return;
	        if(table.selectIndex == index) return;
	        this.style.backgroundColor = overColor;
        });
        addEvent(tr,'mouseout',function(){
          //  var iChk = jwTable._getFirstCheckBox(tr); 
            //if(iChk && iChk.checked) return;
	        if(table.selectIndex == index) return;
	        this.style.backgroundColor = this.oldColor;
        });
        addEvent(tr,'click', function(){
            jwTable._revertTable();
	        if(table.selectIndex){
		        trs[table.selectIndex].style.backgroundColor = table.selectOldColor;
	        }
	       // var chk = jwTable._getFirstCheckBox(this);
	       // if(chk) chk.checked = true;
	        table.selectIndex = index;
	        table.selectOldColor = this.oldColor;
	        this.style.backgroundColor = selectColor;
        });
	}
}

//给对象添加表格单击事件
JustWinTable.prototype.registClickTrListener= function(clickListener){
    if(!this.table) return;
	var trs = this.table.getElementsByTagName('TR');
	for(var i = 1; i< trs.length; i++){
		//_registClickTrListener(trs,i,clickListener);
    }
    function _registClickTrListener(trs,i,clickListener){
        try
        {
        	addEvent(trs[i],'click',clickListener);
     	}catch(e)
     	{}
    }
}

//添加双击事件
JustWinTable.prototype.registDbClickListener = function(listener){
    if(!this.table) return;
	var trs = this.table.getElementsByTagName('TR');
	for(var i = 1; i< trs.length; i++){
		_registListener(trs,i,listener);
    }
    function _registListener(trs,i,listener){
        try
        {
        	addEvent(trs[i],'dblclick',listener);
     	}catch(e)
     	{}
    }
}

//注册点击全选按钮事件
JustWinTable.prototype.registClickAllCHKLitener = function(clickListener){
    if(!this.table) return;
    if(!this._isContainsTr()) return false;
    var trs = this.table.getElementsByTagName('TR');
    if(trs.length == 1) return;
	var chk = this._getFirstCheckBox(trs[0]);
	addEvent(chk,'click',clickListener);
}

//注册单个CheckBox的Click事件
JustWinTable.prototype.registClickSingleCHKListener = function(clickListener){
    if(!this.table) return;
    if(!this._isContainsTr()) return false;
    var trs = this.table.getElementsByTagName('TR');
    if(trs.length == 1) return;
    for(var i = 1; i < trs.length; i++){
        _registClickListener(this,trs,i,clickListener);
    }
    
    function _registClickListener(jwTable,trs,i,clickListener){
        var chk = jwTable._getFirstCheckBox(trs[i]);
        if(!chk) return;
        addEvent(chk,'click',clickListener);
    }
}

//是否全选
JustWinTable.prototype.isCheckedAll = function(){
    if(!this._isContainsTr()) return false;
    var trs = this.table.getElementsByTagName('TR');
	var chk = this._getFirstCheckBox(trs[0]);
	if(chk.checked){
	    return true;
	}
	return false;
}

//返回所有被选中的CHK
JustWinTable.prototype.getCheckedChk = function(){
    var chks = new Array();
    if(!this._isContainsTr()) return chks;
    var trs = this.table.getElementsByTagName('TR');
    for(var i = 1; i < trs.length; i++){
        var chk = this._getFirstCheckBox(trs[i]);
        if(!chk) continue;
        if(chk.checked){
            chks.push(chk);
        }
    }
    return chks;
}

//得到所有CHK
JustWinTable.prototype.getAllChk = function(){
    var chks = new Array();
    if(!this._isContainsTr()) return chks;
    var trs = this.table.getElementsByTagName('TR');
    for(var i = 1; i < trs.length; i++){
        var chk = this._getFirstCheckBox(trs[i]);
        if(chk){
             chks.push(chk);
        }
    }
    return chks;
}


//添加全选事件
JustWinTable.prototype._registCheckBoxListener = function(){
	var jwTable = this;
	var trs = this.table.getElementsByTagName('TR');
	var chk = jwTable._getFirstCheckBox(trs[0]);
	addEvent(chk,'click',function(){
		if(this.checked){
			_registClickCheckBoxListener(jwTable,true);
		}
		else{
			_registClickCheckBoxListener(jwTable,false);
		}
	});
	
	function _registClickCheckBoxListener(jwTable,isCheck){
		var trs = jwTable.table.getElementsByTagName('TR');
	    var selectColor = jwTable.selectColor;
		for(var i = 1; i< trs.length; i++){
			var chk = jwTable._getFirstCheckBox(trs[i]);
			if(!chk) continue;
			if(isCheck){
				//chk.checked = true;
				trs[i].style.backgroundColor = selectColor;
				}
			else{
				//chk.checked = false;
				trs[i].style.backgroundColor = trs[i].oldColor;
			}
		}
	}
}

//获取TR中的CheckBox
JustWinTable.prototype._getFirstCheckBox = function(tr){
	var inputs = tr.getElementsByTagName('INPUT');
	for(var i = 0; i < inputs.length; i++){
		if(inputs[i].getAttribute('type') == 'checkbox'){
		    return inputs[i];
		}
	}
	return null;
}

//注册单个CheckBox的事件
JustWinTable.prototype._registSingleCheckBox = function(){
    if(!this._isContainsTr()) return;
    var trs = this.table.getElementsByTagName('TR');
    for(var i = 1; i < trs.length; i++){
        //_registSingleCheckBoxListener(this,trs,i);   
    }
    
    function _registSingleCheckBoxListener(jwTable,trs,i){
//        var chk = jwTable._getFirstCheckBox(trs[i]);
//        if(!chk) return;
//        addEvent(chk,'click',function(){
//            if(chk.checked){
//                trs[i].style.backgroundColor = jwTable.selectColor;
//            }
//            else{
//                trs[i].style.backgroundColor = trs[i].oldColor;
//            }
//            stopPropagation();
//        });
    }
}



//检查是否含有TR
JustWinTable.prototype._isContainsTr = function(){
    var trs = this.table.getElementsByTagName('TR');
//    var chk = this._getFirstCheckBox(trs[0]);
//    if(chk){
//        return true;
//    }
//    else{
//        return false;
//    }
}

//重置
JustWinTable.prototype._revertTable = function(){
    var trs = this.table.getElementsByTagName('TR');
    for(var i = 0; i < trs.length; i++){
//        var chk = this._getFirstCheckBox(trs[i]);
//        if(!chk) continue;
//        chk.checked = false;
//        trs[i].style.backgroundColor = trs[i].oldColor;
//        this.table.selectIndex = null;
    }
}

//得到当前所有选中的CHK的ID的Json字符串
JustWinTable.prototype.getCheckedChkIdJson = function(checkedChk){
    //若参数为空，返回所有CHK
    checkedChk = checkedChk || this.getAllChk();
    var str = '["';
    for(var i = 0; i < checkedChk.length; i++){
        var td = getFirstParentElement(checkedChk[i],'TR');
        if(checkedChk.length == 1){
            return td.id;
        }
        str += td.id + '","';
    }
    return str.substring(0,str.length - 2) + ']';
}



//设置单个Button的状态并把选中行的id赋值给Button的target属性
JustWinTable.prototype.setBtnStateByJustwinTable = function(btnId){
    if (!this.table) return;
    if (this.table.firstChild.childNodes.length == 1) return;
    if (!document.getElementById(btnId)) return;
    var jwTable = this;
    var btn = document.getElementById(btnId);
    jwTable.registClickTrListener(function(){
        btn.removeAttribute('disabled');
        if(this.id){
            btn.target = this.id;
        }
    });
    /*this.registClickSingleCHKListener(function(){
        var checkedChk = jwTable.getCheckedChk();
        if (checkedChk.length == 1){
            btn.removeAttribute('disabled');
            var tr = getFirstParentElement(this,'TR');
            if(tr.id){
                btn.target = tr.id;
            }
        }
        else{
            btn.setAttribute('disabled','disabled');
            btn.target = '';
        }
    });*/
    /*this.registClickAllCHKLitener(function(){
        if (jwTable.table.rows.length == 2 && jwTable.checked == true){
            btn.removeAttribute('disabled');
            if(this.id){
                btn.target = this.id;
            }
        }
        else{
            btn.setAttribute('disabled','disabled');
            btn.target = '';
        }
    });*/
}

//可拖动列宽
function ColumnResize(table) {
	if (table.tagName != 'TABLE') return;

	this.id = table.id;

	// ============================================================
	// private data
	var self = this;

	var dragColumns  = table.rows[0].cells; // first row columns, used for changing of width
	if (!dragColumns) return; // return if no table exists or no one row exists

	var dragColumnNo; // current dragging column
	var dragX;        // last event X mouse coordinate

	var saveOnmouseup;   // save document onmouseup event handler
	var saveOnmousemove; // save document onmousemove event handler
	var saveBodyCursor;  // save body cursor property

	// ============================================================
	// methods

	// ============================================================
	// do changes columns widths
	// returns true if success and false otherwise
	this.changeColumnWidth = function(no, w) {
		if (!dragColumns) return false;

		if (no < 0) return false;
		if (dragColumns.length < no) return false;

		if (parseInt(dragColumns[no].style.width) <= -w) return false;
		if (dragColumns[no+1] && parseInt(dragColumns[no+1].style.width) <= w) return false;

		dragColumns[no].style.width = parseInt(dragColumns[no].style.width) + w +'px';
		if (dragColumns[no+1])
			dragColumns[no+1].style.width = parseInt(dragColumns[no+1].style.width) - w + 'px';

		return true;
	}

	// ============================================================
	// do drag column width
	this.columnDrag = function(e) {
		var e = e || window.event;
		var X = e.clientX || e.pageX;
		if (!self.changeColumnWidth(dragColumnNo, X-dragX)) {
			// stop drag!
			self.stopColumnDrag(e);
		}

		dragX = X;
		// prevent other event handling
		preventEvent(e);
		return false;
	}

	// ============================================================
	// stops column dragging
	this.stopColumnDrag = function(e) {
		var e = e || window.event;
		if (!dragColumns) return;

		// restore handlers & cursor
		document.onmouseup  = saveOnmouseup;
		document.onmousemove = saveOnmousemove;
		document.body.style.cursor = saveBodyCursor;

		// remember columns widths in cookies for server side
		var colWidth = '';
		var separator = '';
		for (var i=0; i<dragColumns.length; i++) {
			colWidth += separator + parseInt(dragColumns[i].clientWidth);
			separator = '+';
		}
		var expire = new Date();
		expire.setDate(expire.getDate() + 365); // year
		document.cookie = self.id + '-width=' + colWidth +
			'; expires=' + expire.toGMTString();

		preventEvent(e);
	}

	// ============================================================
	// init data and start dragging
	this.startColumnDrag = function(e) {
		var e = e || window.event;

		// if not first button was clicked
		//if (e.button != 0) return;

		// remember dragging object
		dragColumnNo = (e.target || e.srcElement).parentNode.parentNode.cellIndex;
		dragX = e.clientX || e.pageX;

		// set up current columns widths in their particular attributes
		// do it in two steps to avoid jumps on page!
		var colWidth = new Array();
		for (var i=0; i<dragColumns.length; i++)
			colWidth[i] = parseInt(dragColumns[i].clientWidth);
		for (var i=0; i<dragColumns.length; i++) {
			dragColumns[i].width = ""; // for sure
			dragColumns[i].style.width = colWidth[i] + "px";
		}

		saveOnmouseup       = document.onmouseup;
		document.onmouseup  = self.stopColumnDrag;

		saveBodyCursor             = document.body.style.cursor;
		document.body.style.cursor = 'w-resize';

		// fire!
		saveOnmousemove      = document.onmousemove;
		document.onmousemove = self.columnDrag;

		preventEvent(e);
	}

	// prepare table header to be draggable
	// it runs during class creation
	for (var i=0; i<dragColumns.length; i++) {
		dragColumns[i].innerHTML = "<div style='position:relative;height:100%;width:100%'>"+
			"<div style='"+
			"position:absolute;height:100%;width:5px;margin-right:-5px;"+
			"left:100%;top:0px;cursor:w-resize;z-index:10;'>"+
			"</div>"+
			dragColumns[i].innerHTML+
			"</div>";
			// BUGBUG: calculate real border width instead of 5px!!!
			dragColumns[i].firstChild.firstChild.onmousedown = this.startColumnDrag;
		}
}
