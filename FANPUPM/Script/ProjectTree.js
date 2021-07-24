
// 绑定项目树
// 名称约定：
//		dropYear			年份列表
//		prj_tree			承载项目树的
//		hfldProjectJson		存放一级项目数据的隐藏控件
//		hfldSubUrl			存放子页面url的隐藏控件
//		ifram				子页面的iframe
//		hfldStateType		存放项目类型范围
$(document).ready(function () {
	bindTree(); 		// 绑定项目树
	selectOnePrj(); 	// 默认选中第一个项目

	function bindTree() {
		var year = $('#dropYear option:selected').val();
		var data = eval($('#hfldProjectJson').val());
		var url = $('#hfldSubUrl').val();

		var $tree = $('#prj_tree');

		$('#prj_tree').tree({
			data: data,
			onClick: function (node) {
				if (node.state) {
					// 如果是非叶子节点,这打开下层节点,按杨总要求修改（2013-12-25会议）
					var cnode = $tree.tree('getSelected');
					$tree.tree('toggle', cnode.target);
				}

				if (url.indexOf('PrjGuid') > -1) {
					// 物资管理中的参数名称是PrjGuid
					url = jw.modifyParam({ url: url, name: 'PrjGuid', value: node.id });
				} else if (url.indexOf('PrjCode') > -1) {
					url = jw.modifyParam({ url: url, name: 'PrjCode', value: node.id });
				} else {
					url = jw.modifyParam({ url: url, name: 'prjId', value: node.id });
				}

				url = jw.modifyParam({ url: url, name: 'year', value: year });
				if (node.id.length == 36 || (year == 'zzjg' && node.id != '1')) {
					// 项目或者不是根节点的组织机构
					if (node.text.indexOf('无权限') > -1) {
						top.ui.alert('您没有这个项目的权限');
						return false;
					}
					$('#ifram').attr('src', url);
				}
			},
			onBeforeExpand: function (node) {
				// node
				var childLeng = $('#prj_tree').tree('getChildren', node.target).length;
				if (childLeng == 0) {           // 如果没有子节点，则异步加载子节点
					var children = getChildrenJson(node.id);
					$('#prj_tree').tree('append', {
						parent: node.target,
						data: children
					});
				}
			}
		});
	}

	// 返回子节点的JSON对象
	function getChildrenJson(id) {
		var year = $('#dropYear option:selected').val();
		var stateType = $('#hfldStateType').val();
		var showType = '0'; 					// 按父子项目显示
		if (id.length != 36) showType = '1'; 	// 按项目状态显示
		var time = new Date().getTime();
		var pcode = $('#txtPjr').val() || ''; 		// 查询条件
		if (pcode != '') pcode = encodeURI(pcode);
		var url = jw.format('/PrjManager/Handler/GetSubProject.ashx?showType={0}&id={1}&year={2}&stateType={3}&time={4}&pcode={5}',
							showType, id, year, stateType, time, pcode);
		var json;
		$.ajax({
			type: 'GET',
			url: url,
			async: false,
			success: function (data) {
				json = data;
			},
			error: function () {
				alert('error');
			}
		});
		return eval(json);
	}

	// 默认选中第一个项目
	function selectOnePrj() {
		var year = $('#dropYear option:selected').val();
		if (year == 'zzjg') return false;

		if ($('#prj_tree li:eq(0) div').length == 1) {
			$('#prj_tree li:eq(0) .tree-title').click();
		}
		$('#prj_tree li:eq(0) div:eq(1) .tree-title').click();
	}
});



