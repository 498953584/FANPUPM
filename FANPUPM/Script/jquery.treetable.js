(function ($) {
	var $imgLminus = $('<img style="vertical-align:middle; "; src="/images/tree/lminus.gif" />');
	var $imgTminus = $('<img style="vertical-align:middle; "; src="/images/tree/tminus.gif" />');
	var $imgWhite = $('<img style="vertical-align:middle;"; src="/images/tree/white.gif" />');
	var $imgI = $('<img style="vertical-align:middle;"; src="/images/tree/i.gif" />');
	var $imgT = $('<img style="vertical-align:middle;"; src="/images/tree/t.gif" />');
	var $imgL = $('<img style="vertical-align:middle;"; src="/images/tree/l.gif" />');
	var $imgLplus = $('<img style="vertical-align:middle;"; src="/images/tree/lplus.gif" />');
	var $imgTplus = $('<img style="vertical-align:middle;"; src="/images/tree/tplus.gif" />');

	// 默认选择的任务是目标成本WBS
	var handlerUrl = '/BudgetManage/Handler/GetChildTask.ashx';
	var containsChk = false;            // 页面是否有CheckBox
	var isDisplayRed = false;           // 是否进行红色显示变更的WBS
	var isDisplayResource = false;      // 是否能点击查看任务下的资源分配
	var isDisplayDate = true;           // 是否显示WBS的日期
	var isSelectTask = false;
	var isDisplayPeriod = false;
	var isDisplayDesc = true;
	var isDisplayMainMaterial = true;
	var isDisplaySubMaterial = true;
	var isDisplayLabor = true;
	var isDIsplayTotal = true;

	jQuery.fn.extend({
		'treetable': function (options, value) {
			if (arguments.length == 1 && typeof options == 'object') {
				handlerUrl = options.handlerUrl || '/BudgetManage/Handler/GetChildTask.ashx';
				containsChk = options.containsChk || false;                 // 是否显示CheckBox
				isDisplayRed = options.isDisplayRed || false;               // 是否红色显示
				isDisplayResource = options.isDisplayResource || false;     // 是否能点击查看资源
				isDisplayDate = options.isDisplayDate || true;             // 是否显示日期
				isSelectTask = options.isSelectTask || false;               // 是否选择页面
				isDisplayPeriod = options.isDisplayPeriod || true;         // 是否显示工期
				isDisplayDesc = options.isDisplayDesc || false; 			// 项目特征描述

				isDisplayMainMaterial = options.isDisplayMainMaterial || false; 			// 主材
				isDisplaySubMaterial = options.isDisplaySubMaterial || false; 			// 辅材
				isDisplayLabor = options.isDisplayLabor || false; 			// 人工
				isDIsplayTotal = options.isDIsplayTotal || true; 			// 合计

			} else {
				containsChk = options;
				if (typeof value != undefined && (value == 'ConstractTask' || value == 'ConstractList')) {
					// 合同预算
					handlerUrl = '/BudgetManage/Handler/GetChildConstractTask.ashx';
					if (value == 'ConstractTask')
						isDisplayResource = true;
					isDisplayDate = true;
					isDisplayPeriod = true;
					isDisplayDesc = true;
				}
				else if (typeof value != 'undefined' && value == 'TempleateItems') {
					// 模板维护
					handlerUrl = '/BudgetManage/Handler/GetChildTemplateItem.ashx';
					isDisplayDate = false;
					isDisplayMainMaterial = false;
					isDisplaySubMaterial = false;
					isDisplayLabor = false;
				}
				else if (typeof value != 'undefined' && value == 'SelectTemplate') {
					// 模板导入
					handlerUrl = '/BudgetManage/Handler/GetChildTemplateItem.ashx';
					isDisplayDate = false;
					isSelectTask = true;
				}
				else if (typeof value != 'undefined' && value == "ProgressTask") {
					// 实际进度上报 （选择任务节点）
					handlerUrl = '/ProgressManagement/Handler/GetChildrenTask.ashx';
				}
				else if (typeof value != 'undefined' && value == 'ColorRed') {
					isDisplayRed = true;
					isDisplayPeriod = true;
					// 区分变更不显示资源而目标成本查询进行显示资源
					if (!containsChk)
						isDisplayResource = true;
				}
				if (typeof value != 'undefined' && value == 'DisplayResource') {
					// 资源配置
					isDisplayResource = true;
					isDisplayPeriod = true;
					isDisplayMainMaterial = false;
					isDisplaySubMaterial = false;
					isDisplayLabor = false;
				}

				if (typeof value != 'undefined' && value == 'SelectTask') {
					// 选择目标成本
					isSelectTask = true;
					isDisplayDate = false;
				}

				if (typeof value != 'undefined' && value == 'SelectTaskByRpt') {
					// 施工报量选择目标成本
					isSelectTask = true;
					isDisplayDate = false;
					isDisplayDesc = false;
				}
				
				if (typeof value != 'undefined' && value == 'BudTask') {
					// 目标成本
					isDisplayDate = true;
					isDisplayPeriod = true;
					isDisplayDesc = true;

				}

				// 目标成本没有 主材 辅材 人工
				if (handlerUrl.indexOf('GetChildTask.ashx') > -1) {
					isDisplayMainMaterial = false;
					isDisplaySubMaterial = false;
					isDisplayLabor = false;
				}
			}

			var table = this;
			initTable(table);
			// 单击行
			table.delegate('tr:gt(0)', 'click', function (e) {
				$(table).find(':checkbox').attr('checked', false);
				revertTable(table);
				this.className = 'select_row';
				$(this).find(':checkbox').attr('checked', true);
				keepTargetState();
				// 取消全选
				$(table).find(':checkbox:eq(0)').attr('checked', false);
			});

			// 复选框
			table.delegate(':checkbox:gt(0)', 'click', function (e) {
				var tr = $(this).parents('tr').get(0);
				if (!this.checked) {
					tr.className = $(this).attr('oldClass');
					//$(tr).removeClass().addClass($(this).attr('oldClass'));
					// 取消全选
					$(table).find(':checkbox:eq(0)').attr('checked', false);
				} else {
					tr.className = 'select_row';
					var chkLength = $(table).find(':checkbox:gt(0)').length;
					var chkCheckedLength = $(table).find(':checkbox:checked:gt(0)').length;
					if (chkCheckedLength + 1 == chkLength) {
						$(table).find(':checkbox:eq(0)').attr('checked', true);
					}
				}
				e.stopPropagation();
			});

			// 全选按钮
			table.delegate(':checkbox:eq(0)', 'click', function (e) {
				if (this.checked) {
					$(table).find(':checkbox:gt(0)').attr('checked', true);
					$(table).find('tr:gt(0)').attr('class', 'select_row');
				} else {
					revertTable(table);
					$(table).find(':checkbox:gt(0)').attr('checked', false);
				}
				e.stopPropagation();
			});

			table.find('tr:gt(0)').each(function () {
				if (!containsChk) {
					$td = $(this).find('td:eq(1)');
				} else {
					$td = $(this).find('td:eq(2)');
				}
				if ($(this).attr('layer') == '1') {
					if (IsExistNextsibling(this)) {
						// 存在下一个兄弟节点
						if ($(this).attr('subCount') == 0) {
							// 叶子节点
							$td.prepend($imgT.clone());
						} else {
							// 非叶子节点
							$td.prepend($imgTminus.clone());
						}
					} else {
						// 不存在下个兄弟节点
						if ($(this).attr('subCount') == 0) {
							// 叶子节点  
							$td.prepend($imgL.clone());

						} else {
							// 非叶子节点
							$td.prepend($imgLminus.clone());
						}
					}
				} else if ($(this).attr('layer') == '2') {
					if (IsExistNextsibling(this)) {
						// 存在下个兄弟节点
						if ($(this).attr('subCount') == 0) {
							// 叶子节点
							$td.prepend($imgT.clone());
						} else {
							// 非叶子节点
							$td.prepend($imgTplus.clone());
						}
					} else {
						// 不存在下个兄弟节点
						if ($(this).attr('subCount') == 0) {
							// 叶子节点
							$td.prepend($imgL.clone());
						} else {
							// 非叶子节点
							$td.prepend($imgLplus.clone());
						}
					}
					if (IsExistParentsibling(this)) {
						// 下面存在父节点的兄弟节点
						$td.prepend($imgI.clone());
					} else {
						// 下面不存在父节点的兄弟节点
						$td.prepend($imgWhite.clone());
					}
				}
			});

			// 鼠标事件
			table.find('tr:gt(0)').each(function (index) {
				var $tr = $(this);
				$tr.find('img[src*="tminus.gif"]').live('click', function (event) {
					$(getChildren($tr[0])).hide();
					this.src = '/images/tree/tplus.gif';
				});

				$tr.find('img[src*="lminus.gif"]').live('click', function (event) {
					$(getChildren($tr[0])).hide();
					this.src = '/images/tree/lplus.gif';
				});

				$tr.find('img[src*="lplus.gif"]').live('click', function (event) {
					addChildren($tr[0])
					this.src = '/images/tree/lminus.gif';
				});

				$tr.find('img[src*="tplus.gif"]').live('click', function (event) {
					addChildren($tr[0]);
					this.src = '/images/tree/tminus.gif';
				});
			});
		}
	});

	function initTable(table) {
		$(table).find('tr:gt(0)').each(function () {
			//this.oldClass = this.className;
			$(this).attr('oldClass', this.className);
		});
	}

	function revertTable(table) {
		$(table).find('tr:gt(0)').each(function () {
			//this.className = this.oldClass;
			this.className = $(this).attr('oldClass');
		});
	}

	// 是否存在下一个兄弟节点
	function IsExistNextsibling(tr) {
		var tr_layer = $(tr).attr('layer');
		var $nextTrAll = $(tr).nextAll();
		for (var i = 0; i < $nextTrAll.length; i++) {
			var this_layer = $nextTrAll.eq(i).attr('layer');
			if (parseInt(this_layer) < parseInt(tr_layer))
				return false;
			if (this_layer == tr_layer)
				return true;
		}
		return false;
	}

	// 下面是否存在父节点的兄弟节点
	function IsExistParentsibling(tr) {
		var $nextTrAll = $(tr).nextAll();
		var parentLayer = parseInt($(tr).attr('layer')) - 1;
		for (var i = 0; i < $nextTrAll.length; i++) {
			if ($nextTrAll.eq(i).attr('layer') == parentLayer) {
				return true;
			}
		}
		return false;
	}

	// 返回当前节点的所有孩子孩子节点
	function getChildren(tr) {
		var children = new Array();
		var layer = parseInt($(tr).attr('layer'));
		var $nextAll = $(tr).nextAll();
		var isCheckPrevId = true;
		for (var i = 0; i < $nextAll.length; i++) {
			var this_layer = $nextAll.eq(i).attr('layer');
			//关闭时，对已展开的资源信息进行关闭
			if (isCheckPrevId && typeof prevId != 'undefined' && prevId != undefined && prevId == $nextAll.eq(i).attr('id')) {
				$('#' + prevId + '1').remove();
				isCheckPrevId = false;
			}
			if (parseInt(this_layer) > layer || this_layer == undefined) {
				children.push($nextAll[i]);
			} else {
				return children;
			}
		}
		return children;
	}

	window['jQuery']['getChildren'] = getChildren;
	window['jQuery']['addChildren'] = addChildren;

	// 格式小数
	function toFixed(number, n) {
		number = number || 0;
		var numFormat;
		if (number.toFixed) {
			numFormat = number.toFixed(n);
		} else {
			var f3 = Math.pow(10, n);
			numFormat = Math.round(number * f3) / f3;
		}
		return numFormat;
	}

	// 控制指标已经选择的节点 和施工报量已选择的任务
	function keepTargetState() {
		if ($('#hfldSendCheckedIds').get(0) != undefined) {
			var sendIds = $('#hfldSendCheckedIds').val();
			if (sendIds != '') {
				for (i = 0; i < $('#gvBudget tr:gt(0)').length; i++) {
					var tr = $('#gvBudget tr:gt(0)').get(i);
					if (sendIds.indexOf(tr.id) > -1) {
						$(tr).find('input[type="checkbox"]').get(0).checked = true;
					}
				}
			}
		}
	}

	// 日期处理
	function getDate(date) {
		if (date == null) {
			return '';
		}
		else {
			return date;
		}
	}

	// 截取字符串
	function getStr(str, length) {
		if (str == null || str == '') {
			return '';
		}
		else {
			if (length == 0 || str.length < length) {
				return str;
			}
			else {
				return str.substring(str, length) + '...';
			}
		}
	}

	function addChildren(tr) {
		$.blockUI();
		var children = getChildren(tr)
		if (children.length > 0 && $(tr).attr('layer') != '1') {
			$(children).show();
			$.unblockUI();
		} else {
			var prjId = '';
			if ($(tr).attr('projectId')) {
				prjId = '&projectId=' + $(tr).attr('projectId');
			}
			$.ajax({
				url: handlerUrl + '?TaskId=' + tr.id + prjId + '&Time=' + new Date().getTime(),
				async: true,
				success: function (data) {
					$.unblockUI();
					for (var j = data.length - 1; j >= 0; j--) {
						if (handlerUrl.indexOf('GetChildrenTask.ashx') != -1) {
							// 对进度任务的加载信息
							var task = data[j];
							var $trChild = $('<tr id=' + task.UID_
                                + ' layer=' + task.Layer
                                + ' subCount=' + task.SubCount
                                + ' projectId=' + task.PROJECTUID_
                                + '></tr>');
							if (containsChk) {
								$trChild.append($('<td><span title=' + task.UID_ + '><input type="checkbox" /></span></td>'));
							}
							$trChild.append($('<td align="right">' + task.No + '</td>'));
							$tdName = $('<td> <span style="vertical-align:middle; margin-right:25px;">' + task.NAME_ + '</span></td>')
							var layer = task.Layer;
							if (task.SubCount == 0) {
								// 没有子节点
								if (j == data.length - 1) {
									$tdName.prepend($imgL.clone());
								} else {
									$tdName.prepend($imgT.clone());
								}
							}
							else {
								// 有子节点
								var $img = $imgTplus.clone();
								if (j == data.length - 1) {
									$img.attr('src', '/images/tree/lplus.gif');
								}
								$img.click(function (event) {
									if (this.src.indexOf('tminus') != -1) {
										this.src = '/images/tree/tplus.gif';
										$(getChildren($(this).parent().parent().get(0))).hide();
									} else if (this.src.indexOf('tplus') != -1) {
										this.src = '/images/tree/tminus.gif';
										addChildren($(this).parent().parent().get(0));
									} else if (this.src.indexOf('lminus') != -1) {
										this.src = '/images/tree/lplus.gif';
										$(getChildren($(this).parent().parent().get(0))).hide();
									} else if (this.src.indexOf('lplus') != -1) {
										this.src = '/images/tree/lminus.gif';
										addChildren($(this).parent().parent().get(0))
									}
								});
								$tdName.prepend($img);
							}
							var len = layer - 2;
							for (var i = len; i != -1; i--) {
								if ($(tr).find('img:eq(' + i + ')').attr('src').indexOf('lminus.gif') != -1) {
									$tdName.prepend($imgWhite.clone());
								} else if ($(tr).find('img:eq(' + i + ')').attr('src').indexOf('i.gif') != -1) {
									$tdName.prepend($imgI.clone());
								} else if ($(tr).find('img:eq(' + i + ')').attr('src').indexOf('tminus.gif') != -1) {
									$tdName.prepend($imgI.clone());
								} else if ($(tr).find('img:eq(' + i + ')').attr('src').indexOf('white.gif') != -1) {
									$tdName.prepend($imgWhite.clone());
								}
							}

							$trChild.append($tdName);
							$trChild.append($('<td align="right">' + task.DURATION_ + '</td>'));
							$trChild.append($('<td>' + getDate(task.START_) + '</td>'));
							$trChild.append($('<td>' + getDate(task.FINISH_) + '</td>'));
							$trChild.append($('<td>' + getDate(task.ACTUALSTART_) + '</td>'));
							$trChild.append($('<td>' + getDate(task.ACTUALFINISH_) + '</td>'));
							$trChild.append($('<td align="right">' + task.PERCENTCOMPLETE_ + '%</td>'));
							if (j % 2 == 0) {
								$trChild.attr('class', 'rowa').attr('oldClass', 'rowa');
							} else {
								$trChild.attr('class', 'rowb').attr('oldClass', 'rowb');
							}
							$(tr).after($trChild);
						}
						else {
							// 目标成本或预算模板和中标预算的加载
							var task = data[j];
							var $trChild = $('<tr id=' + task.TaskId + ' orderNumber=' + task.OrderNumber +
                                ' layer=' + task.OrderNumber.length / 3 + ' subCount=' + task.SubCount + '></tr>');
							if (containsChk) {
								$trChild.append($('<td><span title=' + task.TaskId + '><input type="checkbox" /></span></td>'));
							}
							$trChild.append($('<td align="right">' + task.No + '</td>'));
							$tdCode = $('<td><span style="vertical-align:middle; margin-right:25px;">' + task.TaskCode + '</span></td>')
							var layer = task.OrderNumber.length / 3;
							if (task.SubCount == 0) {
								// 没有子节点
								if (j == data.length - 1) {
									$tdCode.prepend($imgL.clone());
								} else {
									$tdCode.prepend($imgT.clone());
								}
								// 添加查看节点资源图标
								if (isDisplayResource) {
									$tdCode.append($('<img title=\'详细信息\' src="/images/tree/more.gif" onclick="showInfo(\'' + task.TaskId + '\')"  border="0" style="vertical-align:middle;float:right;cursor:pointer;" />'));
								}
							}
							else {
								// 有子节点
								var $img = $imgTplus.clone();
								if (j == data.length - 1) {
									$img.attr('src', '/images/tree/lplus.gif');
								}
								$img.click(function (event) {
									if (this.src.indexOf('tminus') != -1) {
										this.src = '/images/tree/tplus.gif';
										$(getChildren($(this).parent().parent().get(0))).hide();
									} else if (this.src.indexOf('tplus') != -1) {
										this.src = '/images/tree/tminus.gif';
										addChildren($(this).parent().parent().get(0));
									} else if (this.src.indexOf('lminus') != -1) {
										this.src = '/images/tree/lplus.gif';
										$(getChildren($(this).parent().parent().get(0))).hide();
									} else if (this.src.indexOf('lplus') != -1) {
										this.src = '/images/tree/lminus.gif';
										addChildren($(this).parent().parent().get(0))
									}
								});
								$tdCode.prepend($img);
							}
							var len = layer - 2;
							for (var i = len; i != -1; i--) {
								if ($(tr).find('img:eq(' + i + ')').attr('src').indexOf('lminus.gif') != -1) {
									$tdCode.prepend($imgWhite.clone());
								} else if ($(tr).find('img:eq(' + i + ')').attr('src').indexOf('i.gif') != -1) {
									$tdCode.prepend($imgI.clone());
								} else if ($(tr).find('img:eq(' + i + ')').attr('src').indexOf('tminus.gif') != -1) {
									$tdCode.prepend($imgI.clone());
								} else if ($(tr).find('img:eq(' + i + ')').attr('src').indexOf('white.gif') != -1) {
									$tdCode.prepend($imgWhite.clone());
								}
							}

							$trChild.append($tdCode);
							$trChild.append($('<td>' + task.TaskName + '</td>'));

							// 项目特征描述
							if (isDisplayDesc)
								$trChild.append($('<td><span class="tooltip" title="' + getStr(task.FeatureDescription, 0) + '">' + getStr(task.FeatureDescription, 30) + '</span></td>'));

							var typeName = task.TypeName || '';
							$trChild.append($('<td>' + typeName + '</td>'));
							$trChild.append($('<td>' + task.Unit + '</td>'));
							if (!isSelectTask)
								$trChild.append($('<td align="right">' + toFixed(task.Quantity, 3) + '</td>'));
							if (!isSelectTask) {
								$trChild.append($('<td align="right">' + toFixed(task.UnitPrice, 3) + '</td>'));

								if (isDisplayMainMaterial)
									$trChild.append($('<td align="right">' + toFixed(task.MainMaterial, 3) + '</td>'));
								if (isDisplaySubMaterial)
									$trChild.append($('<td align="right">' + toFixed(task.SubMaterial, 3) + '</td>'));
								if (isDisplayLabor)
									$trChild.append($('<td align="right">' + toFixed(task.Labor, 3) + '</td>'));
								if (isDIsplayTotal)
								    $trChild.append($('<td align="right">' + toFixed(task.Quantity * task.UnitPrice, 3) + '</td>'));

								// 开始时间和结束时间
								if (isDisplayDate) {
									var startDate = task.StartDate == null ? '' : task.StartDate;
									var endDate = task.EndDate == null ? '' : task.EndDate;
									$trChild.append($('<td>' + startDate + '</td>'));
									$trChild.append($('<td>' + endDate + '</td>'));
								}

								// 工期
								if (isDisplayPeriod) {
									if (task.ConstructionPeriod != null) {
										$trChild.append($('<td>' + task.ConstructionPeriod + '</td>'));
									} else {
										$trChild.append($('<td>0</td>'));
									}
								}

								var substrNote = task.Note + '';
								if (task.Note.length > 15)
									substrNote = substrNote.substr(0, 15) + '…';
								$trChild.append($('<td><a id="' + task.TaskId + '00" title="' + task.Note + '" style="text-decoration: none; color: Black;">' + substrNote + '</a></td>'));
							}
							if (j % 2 == 0) {
								$trChild.attr('class', 'rowa').attr('oldClass', 'rowa');
							} else {
								$trChild.attr('class', 'rowb').attr('oldClass', 'rowb');
							}
							$(tr).after($trChild);
							try {
								showMoreNote();
							} catch (err) { }

							if (task.Modified == "1" && isDisplayRed) {
								// 预算变更红色显示
								$trChild.css('color', 'red');
								if ($('#' + task.TaskId + '00').get(0) != undefined)
									$('#' + task.TaskId + '00').css('color', 'red');
							}
						}
					}
					// 对已经选择的任务标示为选中状态
					keepTargetState();
				}
			});
		}
		//jw.tooltip();
	}
})(jQuery);
