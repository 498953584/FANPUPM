<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CTSearch.aspx.cs" Inherits="TowerStation_CTSearch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/json2.js"></script>
	<script type="text/javascript" src="../SysFrame/jscript/JsControlMenuTool.js"></script>
	<style type="text/css">
			body,p{
				padding: 0;
				margin: 0;
			}
			ul{
				list-style: none;
				display: flex;
				justify-content: space-between;
			}
			ul>li{
				display: inline;
				color: #FFF;
			}
			.center{
				text-align: center;
			}
			.table-first{
				font-weight: bold;background-color: #b7b7b7;
			}
			.col{
				border-right: solid 1px #333;
				height: 30px;
				font-size: 13px;
				line-height: 30px;
			}
			.col1{
				flex-basis: 10%;
			}
			.col2{
				flex-basis: 40%;
			}
			.col3{
				flex-basis: 25%;
			}
			.col4{
				flex-basis: 25%;
			}
			.tabGe{
				width: 100%;
			}
			.hang{
				border-top: solid 1px #333;
				
			}
			.col4 p{
				display: inline-block;
				color: #0000ff;
				padding-left: 5px;
				padding-right: 5px;
			}
			.col4 p:last-child{
				color: #ff0000;
			}
			
			.hang div:first-child{
				border-left: solid 1px #333333;
			}
			#table-sty:last-child{
				border-bottom: solid 1px #333;
			}
			.alert-bg{
				position: absolute;
				z-index: 100;
				width: 100%;
				height: 100%;
				top: 0;
				background-color: rgba(0,0,0,.5);
			}
			.flex-sty{
				display: flex;
				width: 100%;
			}
			.flex-grd{
				flex-wrap: wrap;
			}
			.col50{
				flex-basis: 50%;
			}
			.flex-center{
				justify-content: center;
			}
			.jianju{
				padding: 10px;
			}
			.jianju2{
				padding-left: 20px;
			}
		</style>

	<script>
		$(document).ready(function(){
			//alert(1);
		});
		function troubleshoot (){
			console.log("ssss");
			//var alert_bg = document.getElementById("alert_bg");
			document.getElementById("alert_bg").style.display = "block";
			return false;
		}
		function closeAlert(){
			document.getElementById("alert_bg").style.display="none";
		}

		function showInfo(oid, mode) {
            var url = '';
            var title;
            if (mode === 0) {
				title = "查看";
                url = '/TowerStation/TowerStationEdit.aspx?mode=3' + "&oid=" + oid;
            } else if (mode === 1) {
				title = '编辑';
                url = '/TowerStation/TowerStationEdit.aspx?mode=2' + "&oid=" + oid;
            } 
            toolbox_oncommand(url, title + "塔站资产");
            
		}
		function getSelectInfo() {
			var SearchInfo = "";
			var check_jjq = $("#jjq :checked");
			var para_jjq = "";
			var para_fjjq = "";
			$(check_jjq).each(function (index, item) {

				var itemvalue=$(item).attr("data-info");
				SearchInfo += "聚集区_" + itemvalue + ",";
                para_jjq += "1_" + itemvalue + ",";
			});
            var check_fjjq = $("#fjjq :checked");
            $(check_fjjq).each(function (index, item) {

                var itemvalue = $(item).attr("data-info");
				SearchInfo += "非聚集区_" + itemvalue + ",";
                para_fjjq += "0_" + itemvalue + ",";
			});
            $("#txt_search").val(SearchInfo.substr(0, SearchInfo.length - 1));
            $("#txt_jjq").val(para_jjq.substr(0, para_jjq.length - 1));
            $("#txt_fjjq").val(para_fjjq.substr(0, para_fjjq.length - 1));
            $.ajax({
                type: 'POST',
                async: true,
                url: '/TowerStation/handler/GetMapData.ashx?Action=CTSearch', //document.getElementById('dropprovince').value,
                data: { "para_jjq": para_jjq, "para_fjjq": para_fjjq},
                success: function (str) {
                    //$("#drop_gys").append("<option value=''>请选择</option>");
                    //var strTemp = JSON.parse(str);
                    //$(strTemp).each(function (index, item) {
                    //    $("#drop_gys").append("<option value='" + item.NetworkManufacturer + "'>" + item.NetworkManufacturer + "</option>");
                    //});
					$("#table-sty").html(str);
					closeAlert();
                }
            });
		}
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="">
			
			<div id="">
				
				<div id="" style="margin-top: 15px;margin-left: 15px;height: 28px;display: flex;">
					
					<input type="text" name="" id="txt_search" value="" style="margin-left: 15px;width: 318px;height: 25px;"
					placeholder="排查条件1,排查条件2,排查条件3"/><input type="button" id="" style="border: solid 1px #666666;background-color: #666666;color: #FFFFFF;
					font-size: 13px;height: 28px;width: 81px;border-radius: 3px;display: inline-block;
					text-align: center;" onclick="troubleshoot()" value="排查条件" />
					<input type="hidden" id="txt_jjq"  />
					<input type="hidden" id="txt_fjjq"  />
					
				</div>
			</div>
			<div id="" style="padding: 15px;">
				<p>冲突列表</p>
				<div id="table-sty" class="ctsearch">
					
				</div>				
			</div>
			
			
		</div>
		
		<div id="alert_bg" class="alert-bg" style="display: none;" >
			<div id="" style="width: 60%;position: absolute;top: 10%;left: 10%;background-color: rgba(242,242,242,1);
			height:80%;">
				<div id="" style="background-color:rgba(215, 215, 215, 1);height: 34px;line-height: 34px;">
					<p style="display: inline-block;padding-left: 15px;">排查条件</p>
					<img class="img " id="u316_img" src="./u316.png" onclick="closeAlert()" style="position: absolute;right: 10px;top: 5px;">
				</div>
				<div id="" style="height:calc(100% - 34px); overflow-y: scroll;position: relative;">
					<div id="" style="padding: 15px;">
						<p style="font-weight: bold;font-size: 13px;">塔站间距（聚集区）</p>
					</div>
					<div id="">
						<div id="jjq" class="flex-sty flex-grd">
							<%=HTML %>
						</div>
					</div>
					
					
					<div id="" style="padding: 15px;">
						<p style="font-weight: bold;font-size: 13px;">塔站间距（非聚集区）</p>
					</div>
					<div id="">
						<div id="fjjq" class="flex-sty flex-grd">
							<%=HTML %>
						</div>
					</div>
					
			
					<div id="" style="padding: 15px;">
						<p style="font-weight: bold;font-size: 13px;">防雷、接地技术要求</p>
					</div>
					<div id="">
						<div id="" class="flex-sty flex-grd">
							<div id="" class="flex-sty col50 flex-center">
								<div id="" class="jianju">
									<span class="jianju">暂不能获取相关数据信息</span>
								</div>
							</div>
						</div>
					</div>
					
					<div id="" style="padding: 15px;">
						<p style="font-weight: bold;font-size: 13px;">电磁辐射环境要求</p>
					</div>
					<div id="">
						<div id="" class="flex-sty flex-grd">
							<div id="" class="flex-sty col50 flex-center">
								<div id="" class="jianju">
									<span class="jianju">暂不能获取相关数据信息</span>
								</div>
							</div>
						</div>
					</div>
					
					<div id="" style="padding: 15px;">
						<p style="font-weight: bold;font-size: 13px;">电磁干扰要求</p>
					</div>
					<div id="">
						<div id="" class="flex-sty flex-grd">
							<div id="" class="flex-sty col50 flex-center">
								<div id="" class="jianju">
									<span class="jianju">暂不能获取相关数据信息</span>
								</div>
							</div>
						</div>
					</div>
					
					<div id="" style="padding: 15px;">
						<p style="font-weight: bold;font-size: 13px;">干扰隔离要求</p>
					</div>
					<div id="">
						<div id="" class="flex-sty flex-grd">
							<div id="" class="flex-sty col50 flex-center">
								<div id="" class="jianju">
									<span class="jianju">暂不能获取相关数据信息</span>
								</div>
							</div>
						</div>
					</div>
				</div>
				<div style="background-color: rgba(242,242,242,1); text-align:right;padding-bottom:5px;"><input type="button" style="border: solid 1px #666666;background-color: #666666;color: #FFFFFF;
					font-size: 13px;height: 28px;width: 81px;border-radius: 3px;display: inline-block;
					text-align: center;" onclick="getSelectInfo();" value="确认" />&nbsp;&nbsp;<input type="button" style="border: solid 1px #666666;background-color: #666666;color: #FFFFFF;
					font-size: 13px;height: 28px;width: 81px;border-radius: 3px;display: inline-block;
					text-align: center;" onclick="closeAlert()" value="取消" />&nbsp;&nbsp;</div>
			</div>
		</div>
    </form>
</body>
</html>
