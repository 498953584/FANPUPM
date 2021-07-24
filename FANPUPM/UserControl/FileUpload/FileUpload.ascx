<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FileUpload.ascx.cs" Inherits="UserControl_FileUpload" %>

<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/jquery.easyui.extension.css" />
<link href="/UserControl/FileUpload/jquery.uploadify-v2.1.4/default.css" rel="stylesheet"
	type="text/css" />
<link href="/UserControl/FileUpload/jquery.uploadify-v2.1.4/uploadify.css" rel="stylesheet"
	type="text/css" />
<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
<script type="text/javascript" src="/UserControl/FileUpload/jquery.uploadify-v2.1.4/swfobject.js"></script>
<script type="text/javascript" src="/UserControl/FileUpload/jquery.uploadify-v2.1.4/jquery.uploadify.v2.1.4.js"></script>
<script type="text/javascript" src="/Script/jw.js"></script>
<script type="text/javascript" src="/UserControl/FileUpload/FileUpload.js"></script>
<script type="text/javascript">
	$(document).ready(function () {
		//支持一个页面多个上传控件
		$('input[id$=btnUp]').each(function (index) {
			//服务器控件生成前缀
			var uploadifyId = this.id.substring(0, this.id.length - 6);
			initialFileUpload(uploadifyId);
		});
	});
    
</script>
<div style="display: block; float: left; width: 100%;">
	<div style="text-align: left">
		<div style="text-align: left;">
			<asp:TextBox ID="txtFile" Width="60%" Height="15px" runat="server"></asp:TextBox>
			<input type="button" id="btnUp" style="background: #fff url(/images1/btnBack.jpg);
				text-align: center; vertical-align: middle; width: 75px; height: 20px; margin-left: 7px;
				margin-right: 0px;" value="新增附件" runat="server" />

			<input type="button" id="btnManage" style="background: #fff url(/images1/btnBack.jpg);
				text-align: center; vertical-align: middle; width: 75px; height: 20px; margin-left: 7px;
				margin-right: 0px;" value="管理附件" runat="server" />

		</div>
		<!--上传附件-->
		<div id="dialog" title="Basic dialog" style="display: none" runat="server">
			<div id="fileQueue" class="fileQueue" runat="server">
			</div>
			<input type="file" name="uploadify" id="uploadify" runat="server" />

			<img src="/UserControl/FileUpload/jquery.uploadify-v2.1.4/Upload.jpg" id="imgUp" alt="上传" style="cursor: pointer" runat="server" />

			<img src="/UserControl/FileUpload/jquery.uploadify-v2.1.4/Cancel.jpg" id="imgCancle" alt="取消" style="cursor: pointer" runat="server" />

			<img src="/UserControl/FileUpload/jquery.uploadify-v2.1.4/Close.jpg" id="closeDialog" alt="取消" style="cursor: pointer" runat="server" />

		</div>
		<div id="annexManage" title="管理附件" style="display: none; text-align: center">
			<table id="annexTable" class="gvdata" style="width: 100%;" runat="server"><tr class="header" runat="server"><td style="width: 60%" runat="server">
							名称
						</td><td style="width: 30%" runat="server">
							大小
						</td><td runat="server">
						</td></tr></table>
		</div>
	</div>
</div>
<asp:HiddenField ID="hfldType" runat="server" />
<asp:HiddenField ID="hfldName" runat="server" />
<asp:HiddenField ID="hfldFileType" runat="server" />
<asp:HiddenField ID="hfldFolder" runat="server" />
<asp:HiddenField ID="hfldMergerFolder" runat="server" />
<asp:HiddenField ID="hfldRecordCode" runat="server" />
<asp:HiddenField ID="hfldOnComplete" runat="server" />
<asp:HiddenField ID="hfldScript" runat="server" />
<asp:HiddenField ID="hfldOnClose" runat="server" />
<asp:HiddenField ID="hfldScriptData" runat="server" />
