//预算编制
$(document).ready(function () {
	$imgLminus = $('<img style="vertical-align:middle; "; src="/images/tree/lminus.gif" />');
	$imgTminus = $('<img style="vertical-align:middle; "; src="/images/tree/tminus.gif" />');
	$imgWhite = $('<img style="vertical-align:middle;"; src="/images/tree/white.gif" />');
	$imgI = $('<img style="vertical-align:middle;"; src="/images/tree/i.gif" />');
	$imgT = $('<img style="vertical-align:middle;"; src="/images/tree/t.gif" />');
	$imgL = $('<img style="vertical-align:middle;"; src="/images/tree/l.gif" />');

	// 如果不包含复选框，则在第2上添加树形结构
	var treeIndex = 1;
	if ($('#gvBudget :checkbox').length > 0) {
		treeIndex = 2;
	}

	// 遍历每个Layer=1的TR，调用setImg方法
	$('#gvBudget tr[layer=1]').each(function (i) {
		setImg($(this));
	});

	function setImg($tr) {
		var $children = $(getNextLayerChildren($tr));

		$td = $tr.find('td').eq(treeIndex);

		var layer = parseInt($tr.attr('layer'));

		if (layer == 1) {
			var nextSameNode = getSameLayer($tr);
			if (nextSameNode != null) {
				if ($children.length == 0) {
					$td.prepend($imgT.clone());
				} else {
					$td.prepend($imgTminus.clone());
				}
			} else {
				if ($children.length == 0) {
					$td.prepend($imgL.clone());
				} else {
					$td.prepend($imgLminus.clone());
				}
			}
		} else {
			//layer != 1
			if ($children.length == 0) {
				//当前节点为叶子节点
				if ($tr.attr('layer') == $tr.next().attr('layer')) {
					//若下一个节点的layer == 当前节点的layer
					$td.prepend($imgT.clone());
				} else {
					//若下一个节点的layer != 当前节点的layer
					$td.prepend($imgL.clone());
				}
			} else {
				//当前节点不为叶子节点
				//当前节点下与此等级相同的节点
				var nextSameNode = getSameLayer($tr);
				if (nextSameNode != null) {
					//当前节点下与此等级相同的节点的父节点
					var parentNode = getParentLayer($tr, nextSameNode);
					//父节点的一样的
					if (parentNode) {
						$td.prepend($imgLminus.clone());
					} else {

						$td.prepend($imgTminus.clone());
					}
				} else {
					$td.prepend($imgLminus.clone());
				}
			}

			for (var i = layer - 1; i > 1; i--) {
				var $parent = $(getParent($tr, i));
				var $simbling = $(getSimbing($parent));
				if ($simbling.last().attr('id') == $parent.attr('id')) {
					//若当前节点的父节点是所在层级的最后一个节点
					$td.prepend($imgWhite.clone());
				}
				else {
					$td.prepend($imgI.clone());
				}
			}
			if (getNextFirsNode($tr)) {
				$td.prepend($imgI.clone());

			} else {
				$td.prepend($imgWhite.clone());
			}
		}

		if ($children.length == 0) {
			return false;
		}

		$children.each(function (i) {
			setImg($(this));
		})
	}

	//鼠标事件
	$('#gvBudget tr:gt(0)').each(function (index) {
		var $tr = $(this);
		var $c = $(getAllChildren($tr));
		$(this).find('img[src*="lminus.gif"]').live('click', function (event) {
			this.src = '/images/tree/lplus.gif';
			$(getAllChildren($tr)).hide();
			setStateByImg();
		});

		$(this).find('img[src*="tminus.gif"]').live('click', function () {
			this.src = '/images/tree/tplus.gif';
			$(getAllChildren($tr)).hide();
			setStateByImg();
		})

		$(this).find('img[src*="lplus.gif"]').live('click', function (event) {
			this.src = '/images/tree/lminus.gif';
			$tr.nextAll().show();
			setStateByImg();
		});

		$(this).find('img[src*="tplus.gif"]').live('click', function () {
			this.src = '/images/tree/tminus.gif';
			$(getAllChildren($tr)).show();
			setStateByImg();
		})
	});
});


//获取父节点
function getParent($tr, parentLayer) {
	var orderNumber = $tr.attr('orderNumber');
	var parentOrderNumberLength = parentLayer * 3 || orderNumber.length - 3;
	var parentOrderNumber = orderNumber.substr(0, parentOrderNumberLength);
	var parentNode = $('#gvBudget tr[orderNumber=' + parentOrderNumber + ']').get(0);
	return parentNode;
}

//返回当前节点的所有下一级节点
function getNextLayerChildren($tr) {
	var children = new Array();
	var nextLayer = parseInt($tr.attr('layer')) + 1;
	var $nextAll = $tr.nextAll();
	for (var i = 0; i < $nextAll.length; i++) {
		if (parseInt($($nextAll[i]).attr('layer')) == nextLayer) {
			children.push($nextAll[i]);
		} else if (parseInt($($nextAll[i]).attr('layer')) < nextLayer) {
			return children;
		}
	}
	return children;
}

//返回当前节点的所有后代节点
function getAllChildren($tr) {
	var children = new Array();
	var layer = parseInt($tr.attr('layer'));
	var $nextAll = $tr.nextAll();
	for (var i = 0; i < $nextAll.length; i++) {
		if (parseInt($nextAll[i].layer) > layer) {
			children.push($nextAll[i]);
		} else {
			return children;
		}
	}
	return children;
}

//获取兄弟节点
function getSimbing($tr) {
	var $parent = $(getParent($tr));
	return getNextLayerChildren($parent);
}


//根据当前行的图片设置下面节点显示和隐藏状态
function setStateByImg() {
	$('img[src*="tplus.gif"]').add($('img[src*="lplus.gif"]')).each(function () {
		var $tr = $(this).parent().parent();
		$(getAllChildren($tr)).hide();
	});
}

//查询此节点下是否还有与此相同的节点
function getSameLayer($tr) {
	var layer = parseInt($tr.attr('layer'));
	var $nextAll = $tr.nextAll();
	for (var i = 0; i < $nextAll.length; i++) {
		if (parseInt($($nextAll[i]).attr('layer')) == layer) {
			return $nextAll[i];
		}
	}
	return null;
}

//查询此节点下与此相同的节点是否与此节点同属一个父节点3.7
function getParentLayer($old, $new) {
	var oldLayer = parseInt($old.attr('layer'));
	var newLayer = parseInt($($new).prev().attr('layer'));
	if (oldLayer - 1 == newLayer) {
		return true;
	} else {
		return false;
	}
}
function getNextFirsNode($tr) {
	var $nextAll = $tr.nextAll();
	for (var i = 0; i < $nextAll.length; i++) {
		if (parseInt($($nextAll[i]).attr('layer')) == "1") {
			return true;
		}
	}
	return false;
}
