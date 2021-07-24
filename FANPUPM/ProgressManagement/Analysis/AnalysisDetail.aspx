<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnalysisDetail.aspx.cs" Inherits="ProgressManagement_Analysis_AnalysisDetail" %>


<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../Script/ShowToolTip.js"></script>
    <style type="text/css">
        .fadingTooltip
        {
            border-right: darkgray 1px outset;
            border-top: darkgray 1px outset;
            font-size: 10pt;
            border-left: darkgray 1px outset;
            width: auto;
            color: black;
            border-bottom: darkgray 1px outset;
            height: 15px;
            background-color: lemonchiffon;
            margin: 3px 3px 3px 3px;
            padding: 3px 3px 3px 3px;
        }
    </style>
    <script type="text/javascript">
        
        function showAll() {
            parent.desktop.regionDetail = window;
            var region = document.getElementById('hfldRegion').value;
            var url = "/ProgressManagement/Analysis/AnalysisDetailAll.aspx?region=" + encodeURI(region);
            toolbox_oncommand(url, "明细查看");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" cellpadding="0px" cellspacing="0px" border="0px">
        <tr>
            <td class="divFooter" style="text-align: left">
            
               任务层级
               <asp:DropDownList ID="dropLevel" AutoPostBack="true" OnSelectedIndexChanged="dropLevel_SelectedIndexChanged" runat="server"><asp:ListItem Text="全部" Value="" /></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="center">
                <div>
                    <asp:Chart ID="Chart1" BackColor="#D3DFF0" Height="400px" BorderColor="#1A3B69" Palette="BrightPastel"
                        BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="TopBottom"
                        BorderWidth="2px" runat="server">
                        <Titles>
                            <asp:Title ShadowOffset="1" Name="headerShow" ForeColor="#1A3B69" 
                                Font="Microsoft Sans Serif, 16pt, style=Bold">
                            </asp:Title>
                        </Titles>
                        <Legends>
                            <asp:Legend Enabled="false" IsTextAutoFit="false" Name="Default" BackColor="Transparent">
                            </asp:Legend>
                        </Legends>
                        <BorderSkin SkinStyle="Emboss"/>
                        <Series>
                            <asp:Series IsValueShownAsLabel="false" ChartArea="ChartArea1" Name="Default">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1" BorderDashStyle="Solid" BackSecondaryColor="White"
                                ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                <AxisY2 Enabled="False"/>
                                <AxisX2 Enabled="False"/>
                                <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="false"
                                    WallWidth="0" IsClustered="false"/>
                                <AxisY IsLabelAutoFit="false" ArrowStyle="Triangle" Maximum="1.0"
                                    />
                                <AxisX IsLabelAutoFit="false" ArrowStyle="Triangle" Interval="1.0"
                                   />
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfldRegion" runat="server" />
    <div class="fadingTooltip" id="fadingTooltip" style="z-index: 999; visibility: hidden;
        position: absolute">
    </div>
    </form>
</body>
</html>
