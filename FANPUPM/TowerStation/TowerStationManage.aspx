<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TowerStationManage.aspx.cs" Inherits="TowerStation_TowerStationManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>塔站资产</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/ecmascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <link type="text/css" rel="stylesheet" href="../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../Script/wf.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <style type="text/css">
        #queryTable tr td {
            white-space: nowrap;
        }
    </style>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            replaceEmptyTable('emptyShowTs', 'GvTowerStation');
            showTooltip('tooltip', 15);
            var jwtable = new JustWinTable('GvTowerStation');
            formateTable('GvTowerStation', 4);
        });

        function openTowerStation(mode, oid) {
            top.ui._TowerStationManage = window;
            if (!mode) {
                mode = 1;
            }
            var url = '/TowerStation/TowerStationEdit.aspx?mode=' + mode + "&oid=" + oid;
            var title;
            if (mode === 1) {
                title = "新增";
            } else if (mode === 2) {
                title = '编辑';
            } else {
                title = '查看';
            }
            toolbox_oncommand(url, title + "塔站资产");
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <table class="queryTable" id="queryTable" cellpadding="3px" cellspacing="0px">
                        <tr>
                            <td class="descTd">名称
                            </td>
                            <td>
                                <asp:TextBox ID="TxtName" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd">省
                            </td>
                            <td>
                                <asp:DropDownList ID="DdlProvince" runat="server" AutoPostBack="True" Width="99%" OnSelectedIndexChanged="DdlProvince_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            <td class="descTd">地区（市）
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="DdlCity" runat="server" AutoPostBack="True" Width="99%" OnSelectedIndexChanged="DdlCity_SelectedIndexChanged"></asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="DdlProvince" EventName="SelectedIndexChanged" runat="server" />
                                        <asp:AsyncPostBackTrigger ControlID="DdlProvince" EventName="SelectedIndexChanged" runat="server" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td class="descTd">区域
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="DdlArea" runat="server" Width="99%"></asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="DdlCity" EventName="SelectedIndexChanged" runat="server" />
                                        <asp:AsyncPostBackTrigger ControlID="DdlCity" EventName="SelectedIndexChanged" runat="server" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td class="descTd">地址
                            </td>
                            <td>
                                <asp:TextBox ID="TxtAddress" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd">联系人
                            </td>
                            <td>
                                <asp:TextBox ID="TxtLiaison" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="descTd">电话号码
                            </td>
                            <td>
                                <asp:TextBox ID="TxtPhone" runat="server"></asp:TextBox></td>
                            <td class="descTd">电邮地址
                            </td>
                            <td>
                                <asp:TextBox ID="TxtEmail" runat="server"></asp:TextBox></td>
                            <td class="descTd">地点获取方式
                            </td>
                            <td>
                                <asp:DropDownList ID="DdlPlaceMode" DataTextField="text" DataValueField="value" runat="server" Width="99%">
                                </asp:DropDownList>
                            </td>
                            <td class="descTd">建设状态
                            </td>
                            <td>
                                <asp:DropDownList ID="DdlBuildState" DataTextField="text" DataValueField="value" runat="server" Width="99%">
                                </asp:DropDownList>
                            </td>
                            <td class="descTd">建设时间
                            </td>
                            <td>
                                <asp:TextBox ID="TxtBuildTimeStart" onclick="WdatePicker()" runat="server"></asp:TextBox></td>
                            <td class="descTd">至
                            </td>
                            <td>
                                <asp:TextBox ID="TxtBuildTimeEnd" onclick="WdatePicker()" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:Button ID="brnQuery" Text="查询" OnClick="BtnSearch_Click" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="divFooter" style="text-align: left; padding-left: 2px;">
                    <input type="button" id="btnAdd" value="新增" onclick="openTowerStation(1,'')" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GvTowerStation" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" Width="100%" PageSize="15" OnPageIndexChanging="GvTowerStation_PageIndexChanging" DataKeyNames="TowerStationGUID" runat="server">
                        <EmptyDataTemplate>
                            <table class="gvdata" id="emptyShowTs" cellspacing="0" rules="all" border="0" cellpadding="0"
                                style="border-collapse: collapse;">
                                <tr class="header">
                                    <th>序号
                                    </th>
                                    <th>塔站名称
                                    </th>
                                    <th>地图坐标
                                    </th>
                                    <th>操作
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="塔站名称">
                                <ItemTemplate>
                                    <%# Convert.ToString(Eval("Name")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="地图坐标">
                                <ItemTemplate>
                                    <%# Eval("MapCoordinates").ToString() %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <a id="btnQuery" href="javascript:openTowerStation(3,'<%# Eval("TowerStationGUID").ToString() %>')">查看</a>
                                    &nbsp;&nbsp;&nbsp;
                                        <a id="btnEdit" href="#" onclick="openTowerStation(2,'<%# Eval("TowerStationGUID").ToString() %>')">编辑</a>
                                    &nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton ID="BtnDel" runat="server" OnClick="BtnDel_Click" CommandArgument='<%# Eval("TowerStationGUID").ToString() %>' OnClientClick="return confirm('确定删除该塔站资产吗?')">删除</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle CssClass="rowa"></RowStyle>
                        <AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle>
                        <SelectedRowStyle BackColor="Red"></SelectedRowStyle>
                        <HeaderStyle CssClass="header"></HeaderStyle>
                        <FooterStyle CssClass="footer"></FooterStyle>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
