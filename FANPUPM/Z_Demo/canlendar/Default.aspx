<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Z_Demo_canlendar_js_Default" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link href="css/dailog.css" rel="stylesheet" type="text/css" />
<link href="css/calendar.css" rel="stylesheet" type="text/css" />
<link href="css/dp.css" rel="stylesheet" type="text/css" />
<link href="css/alert.css" rel="stylesheet" type="text/css" />
<link href="css/main.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="js/common.js"></script>
	<script type="text/javascript" src="js/lib/blackbird.js"></script>
	<script type="text/javascript" src="js/plugin/datepicker_lang_zh_Cn.js"></script>
	<script type="text/javascript" src="js/plugin/jquery.datepicker.js"></script>
	<script type="text/javascript" src="js/plugin/jquery.alert.js"></script>
	<script type="text/javascript" src="js/plugin/jquery.ifrmdailog.js"></script>
	<script type="text/javascript" src="js/plugin/xgcalendar_lang_zh_CN.js"></script>
	<script type="text/javascript" src="js/plugin/xgcalendar.js?v=1.2.0.4"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var op = {
				view: "week", //默认视图，这里是周视图
				theme: 1, //默认的主题风格
				autoload: true, //是否在页面加载完毕后自动获取当前视图时间的数据
				showday: new Date(), //当前视图的显示时间
				EditCmdhandler: edit, //点击的响应事件
				//DeleteCmdhandler:dcal, //删除的响应事件
				ViewCmdhandler: view,    //查看的响应事件
				onWeekOrMonthToDay: wtd, //当when weekview or month switch to dayview 
				onBeforeRequestData: cal_beforerequest,
				onAfterRequestData: cal_afterrequest,
				onRequestDataError: cal_onerror,
				url: "../../CalService.svc/GetCalData",  //url for get event data by ajax request(post)
				quickAddUrl: "/calendar/add",   //url for quick add event data by ajax request(post)
				quickUpdateUrl: "/calendar/update",   //url for quick update event data by ajax request(post)
				quickDeleteUrl: "/calendar/delete"  //url for quick delete event data by ajax request(post)
			};
			var _MH = document.documentElement.clientHeight; //获取页面高度，不同的文档类型需要不同的计算方法，注意示例中使用的doctype 用这个就搞定了
			op.height = _MH - 70; //container height;
			op.eventItems = []; //default event data;
			$("#xgcalendarp").bcalendar(op);



			function cal_beforerequest(type) {
				var t = "Loading data...";
				switch (type) {
					case 1:
						t = "Loading data...";
						break;
					case 2:
					case 3:
					case 4:
						t = "The request is being processed ...";
						break;
				}
				$("#errorpannel").hide();
				$("#loadingpannel").html(t).show();
			}
			function cal_afterrequest(type) {
				switch (type) {
					case 1:
						$("#loadingpannel").hide();
						break;
					case 2:
					case 3:
					case 4:
						$("#loadingpannel").html("Success!");
						window.setTimeout(function () { $("#loadingpannel").hide(); }, 2000);
						break;
				}

			}
			function cal_onerror(type, data) {
				$("#errorpannel").show();
			}
			function edit(data) {
				var eurl = "edit.php?id={0}&start={2}&end={3}&isallday={4}&title={1}";
				if (data) {
					var url = StrFormat(eurl, data);
					OpenModelWindow(url, { width: 600, height: 400, caption: "Manage  The Calendar", onclose: function () {
						$("#xgcalendarp").reload();
					}
					});
				}
			}
			function view(data) {
				var str = "";
				$.each(data, function (i, item) {
					str += "[" + i + "]: " + item + "\n";
				});
				alert(str);
			}
			function Delete(data, callback) {

				$.alerts.okButton = "Ok";
				$.alerts.cancelButton = "Cancel";
				hiConfirm("Are You Sure to Delete this Event", 'Confirm', function (r) { r && callback(0); });
			}
			function wtd(p) {
				if (p && p.datestrshow) {
					$("#txtdatetimeshow").text(p.datestrshow);
				}
				$("#caltoolbar div.fcurrent").each(function () {
					$(this).removeClass("fcurrent");
				})
				$("#showdaybtn").addClass("fcurrent");
			}

			//显示日视图
			$("#showdaybtn").click(function (e) {
				//document.location.href="#day";
				$("#caltoolbar div.fcurrent").each(function () {
					$(this).removeClass("fcurrent");
				})
				$(this).addClass("fcurrent");
				var p = $("#xgcalendarp").BCalSwtichview("day").BcalGetOp();
				if (p && p.datestrshow) {
					$("#txtdatetimeshow").text(p.datestrshow);
				}
			});
			//显示周视图
			$("#showweekbtn").click(function (e) {
				//document.location.href="#week";
				$("#caltoolbar div.fcurrent").each(function () {
					$(this).removeClass("fcurrent");
				})
				$(this).addClass("fcurrent");
				var p = $("#xgcalendarp").BCalSwtichview("week").BcalGetOp();
				if (p && p.datestrshow) {
					$("#txtdatetimeshow").text(p.datestrshow);
				}

			});
			//显示月视图
			$("#showmonthbtn").click(function (e) {
				//				alert(333);
				//				document.location.href = "#month";
				$("#caltoolbar div.fcurrent").each(function () {
					$(this).removeClass("fcurrent");
				})
				$(this).addClass("fcurrent");
				var p = $("#xgcalendarp").BCalSwtichview("month").BcalGetOp();
				if (p && p.datestrshow) {
					$("#txtdatetimeshow").text(p.datestrshow);
				}
			});

			$("#showreflashbtn").click(function (e) {
				$("#xgcalendarp").BCalReload();
			});


			//点击回到今天
			$("#showtodaybtn").click(function (e) {
				var p = $("#xgcalendarp").BCalGoToday().BcalGetOp();
				if (p && p.datestrshow) {
					$("#txtdatetimeshow").text(p.datestrshow);
				}


			});
			//上一个
			$("#sfprevbtn").click(function (e) {
				var p = $("#xgcalendarp").BCalPrev().BcalGetOp();
				if (p && p.datestrshow) {
					$("#txtdatetimeshow").text(p.datestrshow);
				}

			});
			//下一个
			$("#sfnextbtn").click(function (e) {
				var p = $("#xgcalendarp").BCalNext().BcalGetOp();
				if (p && p.datestrshow) {
					$("#txtdatetimeshow").text(p.datestrshow);
				}
			});


		}); 
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div style="">
		<div id="calhead" style="padding-left: 1px; padding-right: 1px;">
			<div class="cHead">
				<div class="ftitle">
					My Calendar</div>
				<div id="loadingpannel" class="ptogtitle loadicon" style="display: none;">
					Loading data...</div>
				<div id="errorpannel" class="ptogtitle loaderror" style="display: none;">
					Sorry, could not load your data, please try again later</div>
			</div>
			<div id="caltoolbar" class="ctoolbar">
				<div id="faddbtn" class="fbutton">
					<div>
						<span title='Click to Create New Event' class="addcal">New Event </span>
					</div>
				</div>
				<div class="btnseparator">
				</div>
				<div id="showtodaybtn" class="fbutton">
					<div>
						<span title='Click to back to today ' class="showtoday">Today</span></div>
				</div>
				<div class="btnseparator">
				</div>
				<div id="showdaybtn" class="fbutton">
					<div>
						<span title='Day' class="showdayview">Daaaaaaaaay</span></div>
				</div>
				<div id="showweekbtn" class="fbutton fcurrent">
					<div>
						<span title='Week' class="showweekview">Week</span></div>
				</div>
				<div id="showmonthbtn" class="fbutton">
					<div>
						<span title='Month' class="showmonthview">Month</span></div>
				</div>
				<div class="btnseparator">
				</div>
				<div id="showreflashbtn" class="fbutton">
					<div>
						<span title='Refresh view' class="showdayflash">Refresh</span></div>
				</div>
				<div class="btnseparator">
				</div>
				<div id="sfprevbtn" title="Prev" class="fbutton">
					<span class="fprev"></span>
				</div>
				<div id="sfnextbtn" title="Next" class="fbutton">
					<span class="fnext"></span>
				</div>
				<div class="fshowdatep fbutton">
					<div>
						<input type="hidden" name="txtshow" id="hdtxtshow" />
						<span id="txtdatetimeshow">Loading</span>
					</div>
				</div>
				<div class="clear">
				</div>
			</div>
		</div>
		<div style="padding: 1px;">
			<div>
				<div class="t1 chromeColor">
					&nbsp;</div>
				<div class="t2 chromeColor">
					&nbsp;</div>
				<div id="Div13" class="calmain printborder">
					<div id="xgcalendarp" style="overflow-y: visible;">
					</div>
				</div>
				<div class="t2 chromeColor">
					&nbsp;</div>
				<div class="t1 chromeColor">
					&nbsp;
				</div>
			</div>
		</div>
	</div>
	</form>
</body>
</html>
