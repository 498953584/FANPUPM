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
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <table class="queryTable" id="queryTable" cellpadding="3px" cellspacing="0px">
                        <tr>
                            <td class="descTd">省
                            </td>
                            <td>
                                <asp:TextBox ID="TxtProvince" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd">地区（市）
                            </td>
                            <td>
                                <asp:TextBox ID="TxtCity" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd">区域
                            </td>
                            <td>
                                <asp:TextBox ID="TxtArea" runat="server"></asp:TextBox></td>
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
                                <asp:DropDownList ID="DdlPlaceMode" runat="server">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="购买" Text="购买"></asp:ListItem>
                                    <asp:ListItem Value="租赁" Text="租赁"></asp:ListItem>
                                    <asp:ListItem Value="共享" Text="共享"></asp:ListItem>
                                </asp:DropDownList>
                             <%--   <asp:RadioButton ID="PlaceMode购买" GroupName="PlaceMode" runat="server" Text="购买" />
                                <asp:RadioButton ID="PlaceMode租赁" GroupName="PlaceMode" runat="server" Text="租赁" />
                                <asp:RadioButton ID="PlaceMode共享" GroupName="PlaceMode" runat="server" Text="共享" />--%>
                            </td>
                            <td class="descTd">建设状态
                            </td>
                            <td>
                                <asp:DropDownList ID="DdlBuildState" runat="server">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="已建成" Text="已建成"></asp:ListItem>
                                    <asp:ListItem Value="建设中" Text="建设中"></asp:ListItem>
                                    <asp:ListItem Value="规划中" Text="规划中"></asp:ListItem>
                                </asp:DropDownList>
                               <%-- <asp:RadioButton ID="BuildState已建成" GroupName="BuildState" runat="server" Text="已建成" />
                                <asp:RadioButton ID="BuildState建设中" GroupName="BuildState" runat="server" Text="建设中" />
                                <asp:RadioButton ID="BuildState规划中" GroupName="BuildState" runat="server" Text="规划中" />--%>
                            </td>
                            <td colspan="2">
                                <asp:Button ID="brnQuery" Text="查询" OnClick="btnSearch_Click" runat="server" />
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
                                    <%# Convert.ToString(Eval("Province")) %>
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
                    <%--<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                    </webdiyer:AspNetPager>--%>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
