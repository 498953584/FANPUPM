<%@ Page Language="C#" AutoEventWireup="true" CodeFile="measuredatatab.aspx.cs" Inherits="MeasureDataTab" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>MeasureDataTab</title>
		<script src="../../../../Script/jquery.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(function() {
                var lev = request("Levels");
                $("#dd").attr("src", "MeasureDataQuery.aspx?BigSort=1&SmallSort=3&Levels=" + lev);
                $("#dd1").attr("src", "MeasureDataQuery.aspx?BigSort=1&SmallSort=1&Levels=" + lev);
                $("#dd2").attr("src", "MeasureDataQuery.aspx?BigSort=1&SmallSort=2&Levels=" + lev);
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
	          font-size : 13px;
           	font-weight: bold;
          }
		</style>
	</head>
	<body class="body_Frame" scroll="no">
		<form id="Form1" method="post" runat="server">
			<TABLE class="table-normal" id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%"
				border="0">
				<TR style="display:none;">
					<TD class="divHeader">
                        <asp:Label ID="lblTitle" Text="Label" runat="server"></asp:Label>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 15px" vAlign="top">
						<iewc:TabStrip ID="tabSchedule" TargetID="mpResource" SepDefaultStyle="border-top:#c6dae9 solid 0px;border-left:#c6dae9 solid 0px;border-right:#c6dae9 solid 0px;border-bottom:#c6dae9 solid 1px;" TabDefaultStyle="background-image:url(../../../images/2_2.gif);background-color:#f2f8fd;border-left:#c6dae9 solid 1px;border-top:#c6dae9 solid 1px;border-right:#c6dae9 solid 1px;border-bottom:#c6dae9 solid 1px;font-family:verdana;font-size:12px;color:#000000;width:100;height:20;text-align:center;" TabHoverStyle="background-color:#dfefff;" TabSelectedStyle="background-image:url(../../../images/2_1.gif);color:#000000;border-left:#c6dae9 solid 1px;border-top:#c6dae9 solid 1px;border-right:#c6dae9 solid 1px;border-bottom:#c6dae9 solid 0px;width:100;height:20;font-size:12px;text-align:center;" Width="320px" runat="server"><Items><iewc:TabSeparator runat="server"></iewc:TabSeparator><iewc:Tab Text="业主控制网资料" runat="server"></iewc:Tab><iewc:TabSeparator runat="server"></iewc:TabSeparator><iewc:Tab Text="测量控件网资料" runat="server"></iewc:Tab><iewc:TabSeparator runat="server"></iewc:TabSeparator><iewc:Tab Text="测量放线资料" runat="server"></iewc:Tab></Items></iewc:TabStrip></TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<iewc:MultiPage ID="mpResource" runat="server"><iewc:PageView BorderWidth="1px" BorderColor="White" BorderStyle="Solid" runat="server">
								<IFRAME width="100%" height="100%" frameborder="0" id="dd1" runat="server">
								</IFRAME>
							</iewc:PageView><iewc:PageView BorderWidth="1px" BorderColor="White" BorderStyle="Solid" runat="server">
								<IFRAME width="100%" height="100%" frameborder="0" id="dd2" runat="server">
								</IFRAME>
							</iewc:PageView><iewc:PageView BorderWidth="1px" BorderColor="White" BorderStyle="Solid" runat="server">
								<IFRAME width="100%" id="dd" frameborder="0" height="100%" runat="server"></IFRAME>
							</iewc:PageView></iewc:MultiPage></TD>
				</TR>
			</TABLE>
			&nbsp;
		</form>
	</body>
</HTML>
