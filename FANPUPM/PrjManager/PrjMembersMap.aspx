<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjMembersMap.aspx.cs" Inherits="PrjManager_PrjMembersMap" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>项目成员分布</title>
	<script type="text/javascript" src="../Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="http://api.map.baidu.com/api?v=1.4"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			initialize();
		});
		function initialize() {
			var map = new BMap.Map("divMembersMap");            // 创建Map实例
			var point = new BMap.Point(116.404, 39.915);    // 创建点坐标
			map.centerAndZoom(point, 5);                     // 初始化地图,设置中心点坐标和地图级别。
			map.addControl(new BMap.NavigationControl());    // 添加平移缩放控件
			map.addControl(new BMap.ScaleControl());                    // 添加比例尺控件
			map.addControl(new BMap.OverviewMapControl());              //添加缩略地图控件
			var opts1 = { offset: new BMap.Size(60, 10) }
			map.addControl(new BMap.MapTypeControl(opts1));
			map.enableScrollWheelZoom();    //启用滚轮放大缩小，默认禁用。

			var prjInfo = document.getElementById('hfldPrjInfo').value;
			if (prjInfo != "") {
				prjInfo = eval(prjInfo);
				for (var i = 0; i < prjInfo.length; i++) {
					getCoordinates(map, prjInfo[i]);
				}
			}
		}

		// 创建标注，并添加监听
		function makeMarker(map, point, prjInfo) {
			var marker = new BMap.Marker(point);
			marker.addEventListener("mouseover", function (e) {
				disInfo(map, point, prjInfo);
			});
			marker.addEventListener("mouseout", function (e) {
				map.closeInfoWindow();
			});
			map.addOverlay(marker); // 将标注添加到地图中
		}

		//信息窗口
		function disInfo(map, point, prjInfo) {
			var infoMessage = '<table><tr><td>项目编码：</td><td>' + prjInfo.prjCode + '</td></tr>';
			infoMessage += '<tr><td>项目名称：</td><td>' + prjInfo.prjName + '</td></tr>';
			infoMessage += '<tr><td>项目状态：</td><td>' + prjInfo.prjState + '</td></tr>';
			infoMessage += '<tr><td>项目地点：</td><td>' + prjInfo.prjPrice + '</td></tr>';
			infoMessage += '<tr><td style="vertical-align:top">项目成员：</td><td>' + prjInfo.members + '</td></tr></table>';
			var infoWindow = new BMap.InfoWindow(infoMessage);    // 创建信息窗口对象
			map.openInfoWindow(infoWindow, point);      // 打开信息窗口
		}

		// 创建地址解析器实例
		function getCoordinates(map, prjInfo) {
			var myGeo = new BMap.Geocoder();
			// 将地址解析结果显示在地图上，并调整地图视野
			myGeo.getPoint(prjInfo.prjPrice, function (point) {
				if (point) {
					makeMarker(map, point, prjInfo);
				}
			}, prjInfo.city);
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div style="width: 99%; height: 555px; border: 1px solid gray;" id="divMembersMap">
		<div>
		</div>
	</div>
	<input type="hidden" id="hfldPrjInfo" runat="server" />

	</form>
</body>
</html>
