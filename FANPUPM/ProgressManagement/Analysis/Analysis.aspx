<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Analysis.aspx.cs" Inherits="ProgressManagement_Analysis_Analysis" %>
<%@ Import Namespace="cn.justwin.BLL"%>

<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../Script/ShowToolTip.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <link rel="Stylesheet" type="text/css" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
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
        addEvent(window, 'load', function () {
            replaceEmptyTable('emptyTable', 'gvProgress');
            showTooltip('tooltip', 25);
            var jwTable = new JustWinTable('gvProgress');
        });
        function query(region) {
            parent.desktop.regionDetail = window;
            var url = "/ProgressManagement/Analysis/AnalysisDetail.aspx?region=" + encodeURI(region);
            toolbox_oncommand(url, "进度明细");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" cellpadding="0px" cellspacing="0px" border="0px">
        <tr>
            <td align="center">
                <div>
                    <asp:Chart ID="Chart1" BackColor="#D3DFF0" Height="300px" BorderColor="#1A3B69" Palette="BrightPastel"
                        BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="TopBottom"
                        BorderWidth="2px" runat="server">
                        <Titles>
                            <asp:Title ShadowOffset="1" Text="项目进度视图" ForeColor="#1A3B69" 
                                Font="Microsoft Sans Serif, 16pt, style=Bold">
                            </asp:Title>
                        </Titles>
                        <Legends>
                            <asp:Legend Enabled="false" IsTextAutoFit="false" Name="Default" BackColor="Transparent">
                            </asp:Legend>
                        </Legends>
                        <asp:BorderSkin SkinStyle="Emboss"></asp:BorderSkin>
                        <Series>
                            <asp:Series IsValueShownAsLabel="false" ChartArea="ChartArea1" Name="Default" 
                                LabelAngle="60">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1" BorderColor="Gray" BorderDashStyle="Solid" BackSecondaryColor="White"
                                ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                <AxisY2 Enabled="False"/>
                                <AxisX2 Enabled="False"/>
                                <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="false"
                                    WallWidth="0" IsClustered="false" />
                                <AxisY LineColor="Gray" IsLabelAutoFit="false" ArrowStyle="Triangle" Maximum="1.0"
                                  />
                                <AxisX LineColor="Gray" IsLabelAutoFit="false" ArrowStyle="Triangle" Interval="1.0"
                                    IntervalOffset="1.0"/>                               
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>            </td>
        </tr>
        <tr>
            <td align="center" style="vertical-align: top; ">
                <asp:GridView ID="gvProgress" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvProgress_RowDataBound" runat="server">
<EmptyDataTemplate>
                        <table id="emptyTable">
                            <tr class="header">
                                <th scope="col">
                                    序号
                                </th>
                                <th scope="col">
                                    项目名称
                                </th>
                                <th scope="col">
                                    进度
                                </th>
                                <th scope="col">
                                    工程工期
                                </th>
                                <th scope="col">
                                    工程造价（万元）
                                </th>
                                <th scope="col">
                                    项目经理
                                </th>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false">
<ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="项目名称">
<ItemTemplate>
                                <a class="link tooltip" title='<%# Eval("PrjName") %>' onclick="query('<%# Eval("PrjName") %>')">
                                    <%# StringUtility.GetStr(Eval("PrjName"), 25) %></a>
                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="进度">
<ItemTemplate>
                                <%# Eval("PERCENTCOMPLETE_").ToString() + '%' %>
                            </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="工程工期" DataField="Duration" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="工程造价（万元）" DataField="PrjCost" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="项目经理" DataField="PrjManagerName" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
            </td>
        </tr>
    </table>
    <div class="fadingTooltip" id="fadingTooltip" style="z-index: 999; visibility: hidden;
        position: absolute">
    </div>
    </form>
</body>
</html>
