<%@ Page Language="C#" AutoEventWireup="true" CodeFile="drawcheckuptabpage.aspx.cs" Inherits="DrawCheckUpTabPage" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>DrawCheckUpTabPage</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

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
		</style>
	</head>
	<body class="body_frame" scroll="no">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<TABLE id="Table1" height="100%" style="margin-top:5px;" cellSpacing="0" cellPadding="0" width="100%"
					border="0">
					<TR style="display:none;">
						<TD class="divHeader">
                            <asp:Label ID="lbltitle" Text="Label" Visible="false" runat="server"></asp:Label></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 15px;" vAlign="top">
							<iewc:TabStrip ID="tabSchedule" TargetID="mpResource" SepDefaultStyle="border-top:#c6dae9 solid 0px;border-left:#c6dae9 solid 0px;border-right:#c6dae9 solid 0px;border-bottom:#c6dae9 solid 1px;" TabDefaultStyle="background-image:url(../../../images/2_2.gif);background-color:#f2f8fd;border-left:#c6dae9 solid 1px;border-top:#c6dae9 solid 1px;border-right:#c6dae9 solid 1px;border-bottom:#c6dae9 solid 1px;font-family:verdana;font-size:12px;color:#000000;width:80;height:20;text-align:center;" TabHoverStyle="background-color:#dfefff;" TabSelectedStyle="background-image:url(../../../images/2_1.gif);color:#000000;border-left:#c6dae9 solid 1px;border-top:#c6dae9 solid 1px;border-right:#c6dae9 solid 1px;border-bottom:#c6dae9 solid 0px;width:80;height:20;font-size:12px;text-align:center;" Width="180px" runat="server"><Items><iewc:TabSeparator runat="server"></iewc:TabSeparator><iewc:Tab Text="自审" runat="server"></iewc:Tab><iewc:TabSeparator runat="server"></iewc:TabSeparator><iewc:Tab Text="会审" runat="server"></iewc:Tab></Items></iewc:TabStrip></TD>
					</TR>
					<TR>
						<TD vAlign="top" Height="100%">
							<iewc:MultiPage ID="mpResource" runat="server"><iewc:PageView BorderWidth="0px" BorderColor="White" BorderStyle="Solid" runat="server">
									<IFRAME src="MeasureDataQuery.aspx?BigSort=2&SmallSort=1" frameborder="0" width="100%" height="100%">
									</IFRAME>
								</iewc:PageView><iewc:PageView BorderWidth="0px" BorderColor="White" BorderStyle="Solid" runat="server">
									<IFRAME src="MeasureDataQuery.aspx?BigSort=2&SmallSort=2" frameborder="0" width="100%" height="100%">
									</IFRAME>
								</iewc:PageView></iewc:MultiPage></TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
