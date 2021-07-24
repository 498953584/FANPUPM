<%@ Page Language="C#" AutoEventWireup="true" CodeFile="technologyfinishtab.aspx.cs" Inherits="TechnologyFinishTab" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>TechnologyFinishTab</title>
	<script src="../../../../Script/jquery.js" type="text/javascript"></script>
	<script type="text/javascript">
		$(function () {
			var lev = request("Levels");
			$("#ifsrc1").attr("src", "MeasureDataQuery.aspx?BigSort=6&SmallSort=1&Levels=" + lev);
			//$("#ifsrc2").attr("src", "MeasureDataQuery.aspx?BigSort=6&SmallSort=2&Levels=" + lev);
		});
		function request(paras) {
			var url = location.href;
			var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
			var paraObj = {}
			for (i = 0; j = paraString[i]; i++) {
				paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
			}
			var returnValue = paraObj[paras.toLowerCase()];
			if (typeof (returnValue) == "undefined") {
				return "";
			} else {
				return returnValue;
			}
		}
	</script>
	<style type="text/css">
		.divHeader
		{
			height: 20px;
			background: url(/images1/divTopBack.jpg) repeat-x;
			text-align: center;
			vertical-align: middle;
			font-size: 13px;
			font-weight: bold;
		}
	</style>
</head>
<body class="body_frame" scroll="no">
	<form id="Form1" method="post" runat="server">
	<font face="宋体">
		<table class="table-normal" id="Table1" height="100%" cellspacing="0" cellpadding="0"
			width="100%" border="2" style="vertical-align: middle;">
			<tr style="display: none;">
				<td class="divHeader">
					<asp:Label ID="lbltitle" Text="Label" runat="server"></asp:Label>
				</td>
			</tr>
			<tr>
				<td valign="top">
					<iewc:MultiPage ID="mpResource" runat="server"><iewc:PageView BorderWidth="0px" BorderColor="White" BorderStyle="Solid" runat="server">
							<iframe src="MeasureDataQuery.aspx?BigSort=6&SmallSort=1" frameborder="0" width="100%"
								height="100%" id="ifsrc1"></iframe>
						</iewc:PageView><iewc:PageView BorderWidth="0px" BorderColor="White" BorderStyle="Solid" runat="server">
							<iframe src="MeasureDataQuery.aspx?BigSort=6&SmallSort=2" frameborder="0" width="100%"
								height="100%" id="ifsrc2"></iframe>
						</iewc:PageView></iewc:MultiPage>
				</td>
			</tr>
		</table>
	</font>
	</form>
</body>
</html>
