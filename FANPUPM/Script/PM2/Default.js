$(document).ready(function () {
	// 登录
	$('#btnOk').get(0).onclick = validate;
	// 重置
	$('#imgReset').click(reset);

	// 用户名称添加keyup事件
	$('#txtName').keyup(function () {
		// 如果选中集中密码
		if ($('#chkPwd').attr('checked')) {
			if (getCookie('cbName') != $('#txtName').val()) {
				$('#txtPwd').val('');
			}
		}
	});

	// 自动登录
	var id = getRequestParam('id');
	var pwd = getRequestParam('pwd');
	if (!!id && !!pwd) {
		// 如果URL中包含id和pwd, 并且没有进行自动登录过，则自动进行登录
		$('#txtName').val(id);
		$('#txtPwd').val(pwd);
		//		$('#hfldIsAutoLogin').val('1');
		$('#btnOk').click();
	}

});

// 验证
function validate() {
	if (!$('#txtName').val()) {
		jw.show('系统提示', '用户名不能为空。');
		return false;
	}
	if (!$('#txtPwd').val()) {
		jw.show('系统提示', '密码不能为空。');
		return false;
	}
}

// 重置
function reset() {
	$('#txtName').val('');
	$('#txtPwd').val('');
}

// 注册
function register() {
	window.open('http://www.fanpusoft.com');
}