<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportPhoto.aspx.cs" Inherits="BudgetManage_Construct_ReportPhoto" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>拍照监控</title>
	<style type="text/css">
		#container
		{
			width: 100%;
			height: 100%;
			float: left;
		}
		#project
		{
			width: 100%;
			height: 100%;
			float: left;
		}
		#budget
		{
			width: 300px;
			float: left;
			border-left: solid 2px #CADEED;
		}
		.photo .photo
		{
			float: left;
			margin-bottom: 12px;
			width: 185px;
		}
		.photo A
		{
			display: block;
			font-size: 0.85em;
			background: url(../../Script/lightbox/images/ImageBack.png) #fff no-repeat 0px 0px;
			color: #002c79;
			text-decoration: none;
		}
		.photo A:visited
		{
			background-position: -371px 0px;
			color: #7e7e7e;
		}
		.photo A:hover
		{
			background-position: -185px 0px;
			color: #c24f00;
		}
		.photo SPAN.desc
		{
			display: block;
			text-align: center;
		}
		.photo IMG
		{
			display: inline;
			margin: 11px 11px 11px 11px;
			width: 163px;
			height: 105px;
		}
		.photo
		{
			text-align: center;
		}
		.photo .photos-3
		{
			margin: auto;
			width: 100%;
		}
	</style>
	<link type="text/css" rel="Stylesheet" href="../../Script/lightbox/jquery.lightbox-0.5.css" />

	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../Script/lightbox/jquery.lightbox-0.5.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			$('#btnDisplayBud').hide();
			setWidthHeight();
			clickTree('trvwBudget', 'hfldTaskId');
			$('#gallery a').lightBox({
				overlayBgColor: '#555',
				imageLoading: '../../Script/lightbox/images/lightbox-ico-loading.gif',
				imageBtnPrev: '../../Script/lightbox/images/lightbox-btn-prev.png',
				imageBtnNext: '../../Script/lightbox/images/lightbox-btn-next.png',
				imageBtnClose: '../../Script/lightbox/images/lightbox-btn-close.jpg',
				txtImage: '图片',
				txtOf: '共',
				maxWidth: 430,
				maxHeight: 300
			});
		});
		function setWidthHeight() {
			$('#divBudTask').height($(this).height() - $('#divProject').height() - 3);
		}
		//选择项目
		function openProjPicker() {
			jw.selectOneProject({
				idinput: 'hdnProjectCode',
				nameinput: 'txtProject',
				func: function () {
					$('#btnDisplayBud').click();
				}
			});
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div id="container">
		<table style="width: 100%; height: 99%;">
			<tr>
				<td id="td_Left" style="width: 194px; vertical-align: top; height: 100%;">
					<div id="divProject" style="width: 194px;">
						<table>
							<tr>
								<td>
									项目
								</td>
								<td>
									<span class="spanSelect" style="width: 122px;">
										<input type="text" id="txtProject" style="width: 97px; height: 15px;
											border: none; line-height: 16px; margin: 1px 0px 1px 2px;" readonly="readonly" runat="server" />

										<img alt="选择项目" onclick="openProjPicker();" src="../../images/icon.bmp" style="float: right;" />
									</span>
									<asp:HiddenField ID="hdnProjectCode" runat="server" />
								</td>
							</tr>
						</table>
					</div>
					<div id="divBudTask" style="overflow: auto; width: 194px;">
						<asp:TreeView ID="trvwBudget" ShowLines="true" OnSelectedNodeChanged="trvwBudget_SelectedNodeChanged" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
					</div>
				</td>
				<td style="vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
					<div class="photo">
						<div class="photos-3">
							<div id="gallery">
								<ul>
									<%=this.imaStr %>
								</ul>
							</div>
						</div>
					</div>
				</td>
			</tr>
		</table>
	</div>
	<asp:Button ID="btnDisplayBud" OnClick="btnDisplayBud_Click" runat="server" />
	<asp:HiddenField ID="hfldTaskId" runat="server" />
	</form>
</body>
</html>
