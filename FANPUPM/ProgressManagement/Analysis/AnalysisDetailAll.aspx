<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnalysisDetailAll.aspx.cs" Inherits="ProgressManagement_Analysis_AnalysisDetailAll" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" cellpadding="0px" cellspacing="0px" border="0px">
        <tr>
            <td align="center">
                <div>
                    <asp:Chart ID="Chart1" BackColor="#D3DFF0" Height="400px" BorderColor="#1A3B69" Palette="BrightPastel"
                        BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="TopBottom"
                        BorderWidth="2px" runat="server">
                        <Titles>
                            <asp:Title ShadowOffset="3" Name="headerShow" ForeColor="#1A3B69">
                            </asp:Title>
                        </Titles>
                        <Legends>
                            <asp:Legend Enabled="false" IsTextAutoFit="false" Name="Default" BackColor="Transparent">
                            </asp:Legend>
                        </Legends>
                        <BorderSkin SkinStyle="Emboss" />
                        <Series>
                            <asp:Series IsValueShownAsLabel="false" ChartArea="ChartArea1" Name="Default">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1" BorderDashStyle="Solid" BackSecondaryColor="White"
                                ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                <AxisY2 enabled="False" />
                                <AxisX2 enabled="False" />
                                <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="false"
                                    WallWidth="0" IsClustered="false" />
                                <AxisY islabelautofit="false" arrowstyle="Triangle"
                                    maximum="1.0" />
                                <AxisX islabelautofit="false" arrowstyle="Triangle" interval="1.0"
                                     />
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                </div>
            </td>
        </tr>
    </table>
    <div class="fadingTooltip" id="fadingTooltip" style="z-index: 999; visibility: hidden;
        position: absolute">
    </div>
    </form>
</body>
</html>
