function doClick(obj, tbName) {
	var table = document.getElementById(tbName);
	for (var i = 0; i < table.rows.length; i++) {
		if (table.rows[i].className == 'trm_focus') {
			table.rows[i].className = 'trm_out';
		}
	}

	obj.className = 'trm_focus';

	//选择第一列的单选按�?
	try {
		var cellObj = obj.cells[0].all.tags("input")[0];
		if (cellObj.type == "radio") {
			cellObj.checked = true;
		}
		//else if(cellObj.type == "checkbox")
		//{
		//	cellObj.checked = !cellObj.checked;
		//}
	}
	catch (e)
	{ }
}

function doClear(tbName) {
	var table = document.getElementById(tbName);
	for (var i = 0; i < table.rows.length; i++) {
		if (table.rows[i].className == 'trm_focus') {
			table.rows[i].className = 'trm_out';
		}
	}
}


function doMouseOut(obj) {
	if (obj.className.toLowerCase() == 'trm_focus')
		return;
	obj.className = 'trm_out';
}

function doMouseOver(obj) {
	if (obj.className.toLowerCase() == 'trm_focus')
		return;
	obj.className = 'trm_over';
}

function doSwitchDisplay(obj, tbName, theShadow, t1, t2, imgurl) {
	var show = false;
	var table = document.getElementById(tbName);


	var coll = new Array();
	var trs = table.getElementsByTagName('TR');
	for (var i = 0; i < trs.length; i++) {
		if (trs[i].getAttribute('id') == t1) {
			coll.push(trs[i]);
		}
	}

	if (coll != null) {
		if (coll.length > 0) {
			for (i = 0; i < coll.length; i++) {
				if (coll[i].style.display == 'none') {
					coll[i].style.display = '';
				}
				else {
					coll[i].style.display = 'none';
				}
			}
		}
		//		else {
		//			if (coll.style.display == 'none') {
		//				coll.style.display = '';
		//			}
		//			else {
		//				coll.style.display = 'none';
		//			}
		//		}
	}
	if (obj.src.toLowerCase().indexOf("minus") > 0) {
		obj.src = imgurl + "/images/tree/" + t2 + "plus.gif";
		doCodeList(t1, 'd', theShadow);
		hideChild(t1, tbName, theShadow);
	}
	else {
		obj.src = imgurl + "/images/tree/" + t2 + "minus.gif";
		doCodeList(t1, 'a', theShadow);
		showChild(t1, tbName, theShadow);
	}
}

function showChild(value, tbName, theShadow) {
	var table = document.getElementById(tbName);
	var treeCode = document.getElementById(theShadow);
	var treeCodeList = treeCode.value.split('^');
	var childlist;
	for (var i = 0; i < treeCodeList.length; i++) {
		if ((treeCodeList[i].indexOf(value) == 0) && (treeCodeList[i] != value)) {
			childlist = table.all(treeCodeList[i]);
			if (childlist != null) {
				if (childlist.length) {
					for (var j = 0; j < childlist.length; j++) {
						childlist(j).style.display = '';
					}
				}
				else {
					childlist.style.display = '';
				}
			}
		}
	}
}

function hideChild(value, tbName, theShadow) {
	var table = document.getElementById(tbName);
	var treeCode = document.getElementById(theShadow);
	var treeCodeList = treeCode.value.split('^');
	var childlist;

	//alert(treeCodeList.value);
	for (var i = 0; i < treeCodeList.length; i++) {
		if ((treeCodeList[i].indexOf(value) == 0) && (treeCodeList[i] != value)) {
			childlist = table.all(treeCodeList[i]);

			if (childlist != null) {
				if (childlist.length) {
					for (var j = 0; j < childlist.length; j++) {
						childlist(j).style.display = 'none';
					}
				}
				else {
					childlist.style.display = 'none';
				}
			}
		}
	}
}

function doCodeList(value, op, theShadow) {
	var exists = false;
	var treeCode = document.getElementById(theShadow);
	var treeCodeList = treeCode.value.split('^');

	if (treeCode.value == "") {
		treeCodeList.splice(0, 1);
	}

	if (op == 'a') {
		for (var i = 0; i < treeCodeList.length; i++) {
			if (treeCodeList[i] == value) {
				exists = true;
				break;
			}
		}
		if (!exists) {
			treeCodeList.push(value);
		}
	}
	else {
		for (var i = 0; i < treeCodeList.length; i++) {
			if (treeCodeList[i] == value) {
				treeCodeList.splice(i, 1);
			}
		}
	}
	document.getElementById(theShadow).value = treeCodeList.join('^');
}

//----------------------何海 树的折叠控制 start----------------------------------------------
var j;
//设置隐藏图片
function ReplaceDemo(ss) {
	var r, re;                    // 声明变量�?		
	re = /minus/g             // 创建正则表达式模式�?
	r = ss.replace(re, 'plus');    // �?"plus" 替换 "minus"�?
	return (r);                   // 返回替换后的字符串�?
}
//设置显示图片
function ReplaceDemo2(ss) {
	var r, re;                    // 声明变量�?		
	re = /plus/g             // 创建正则表达式模式�?
	r = ss.replace(re, 'minus');    // �?"minus" 替换 "plus"�?
	return (r);                   // 返回替换后的字符串�?
}
//当前节点的子节点显示，隐�?方法	
//递归折叠树的节点
//r是图片的路径，PCode是当前父节点对象，sty是判断显示还是隐�?
function NodeWimple(r, PCode, sty) {
	myrows = document.getElementById("table").rows;
	if (myrows.length < 2) {
		return;
	}
	for (i = 2; i < myrows.length; i++)//遍历table
	{
		var parentcode = myrows[i].children("parentschedulecode", 0).innerHTML;
		if (parentcode == PCode) {
			j = i;
			if (sty > 0)//隐藏
			{
				myrows[i].style.display = 'none';
				r.src = ReplaceDemo(r.src);
				NodeWimple(r, myrows[i].children("ScheduleCode", 0).innerHTML, sty);
			}
			else//显示
			{
				myrows[i].style.display = '';
				myrows[i].children("ScheduleName", 0).innerHTML = ReplaceDemo2(myrows[i].children("ScheduleName", 0).innerHTML);
				r.src = ReplaceDemo2(r.src);
				NodeWimple(r, myrows[i].children("ScheduleCode", 0).innerHTML, sty);
			}
		}
	}
	i = j;
}
//触发当前节点的子节点隐藏和显示事�?
function SetDisplay(r, obj) {
	var bz = r.src.indexOf('minus.gif');
	NodeWimple(r, obj, bz);
}
//处理多�?
function SetNodeCheck(PCode, IsChecked) {
	myrows = document.getElementById("table").rows;
	if (myrows.length < 2) {
		return;
	}
	for (i = 2; i < myrows.length; i++)//遍历table
	{
		var parentcode = myrows[i].children("parentschedulecode", 0).innerHTML;
		if (parentcode == PCode) {
			j = i;
			myrows[i].children[4].children[0].checked = IsChecked;
			SetNodeCheck(myrows[i].children("ScheduleCode", 0).innerHTML, IsChecked);
		}
	}
	i = j;
}
//----------------------何海 树的折叠控制 end----------------------------------------------*/ 
function selrow1(obj) {
	/**************
	*hxp
	*2008-8-1
	*onload="selrow1('DG_list')"			    
	***************/

	try {
		var table = document.getElementById(obj);
		table.rows[1].click();
	}
	catch (e) { }

}
function selrow2(obj) {
	/**************
	*hxp
	*2008-8-1
	*onload="selrow1('DG_list')"			    
	***************/

	try {
		var table = document.getElementById(obj);
		table.rows[2].click();
	}
	catch (e) { }

}
function selrow3(obj) {
	/**************
	*treeview �� ��λ
	*hxp
	*2008-8-1
	*onload="selrow3('DG_list')"
	***************/
	try {
		document.getElementById(obj).click();
	}
	catch (e) { }
}			