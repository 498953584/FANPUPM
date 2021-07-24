<%@ Page Language="C#" AutoEventWireup="true" CodeFile="constructorganizequery.aspx.cs" Inherits="ConstructOrganizeQuery" %>
<%@ Import Namespace="cn.justwin.BLL"%>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="Head1" runat="server"><title>ConstructOrganizeQuery</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsot.com/intellisense/ie5" Name="vs_targetSchema" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2S.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/Script/wf.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            var level = document.getElementById("HdnLevels").value;
            if (level != "") {
                document.getElementById("trhide").style.display = "none";
            }
            var purchasePlanTable = new JustWinTable('DGrdConstruct');
            setButton(purchasePlanTable, 'BtnDel', 'BtnUpd', 'BtnView', 'hfldConstruct')
            setWfButtonState2(purchasePlanTable, 'WF1');
            //如果只有一页的数据,隐藏页码
            hideFirstPageNo();
        });

        function deleteConstruct() {
            if (!document.getElementById('hfldConstruct')) return false;
            if (!confirm("确定要删除吗？")) {
                return false;
            }
        }
        function openview(ic, type) {
            var Id = document.getElementById("hfldConstruct").value;
            if (type == 0) {
                var url = "/Epc/17/ppm/ScienceInnovate/ConstructOrganizeView.aspx?ic=" + ic;
            }
            if (type == 1) {
                var url = "/Epc/17/ppm/ScienceInnovate/ConstructOrganizeView.aspx?id=" + Id;
            }
            parent.parent.desktop.flowclass = window;
            toolbox_oncommand(url, "查看施工组织设计");
            //viewopen(url);
        }

        function openEdit(opType) {
            var projectCode = document.getElementById("HdnPrjCode").value;
            var prjName = document.getElementById("HdnPrjName").value;
            var Id = document.getElementById("hfldConstruct").value;
            var type = document.getElementById("HdnType").value;

            var url;
            var title;
            switch (opType) {
                case 'Add':
                    url = "/Epc/17/ppm/ScienceInnovate/ConstructOrganizeEdit.aspx?Type=Add&pc=" + projectCode + "&Id=" + Id + "&pn=" + escape(prjName);
                    title = "新增施工组织设计";
                    break;
                case 'Upd':
                    url = "/Epc/17/ppm/ScienceInnovate/ConstructOrganizeEdit.aspx?Type=Upd&Id=" + Id + "&pc=" + projectCode + "&pn=" + escape(prjName);
                    title = "修改施工组织设计";
                    break;
            }
            top.ui._constructorganizequery = window;
            toolbox_oncommand(url, title);
        }
    </script>
    <style type="text/css">
        .dgheader
        {
            background-color: #EEF2F5;
            white-space: nowrap;
            overflow: hidden;
            font-weight: normal;
            text-align: center;
            border-color: #CADEED;
            color: #727FAA;
            padding-left: 6px;
            padding-right: 6px;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr id="header">
            <td>
                施工组织
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;" width="100%">
                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            编制起始日期
                        </td>
                        <td>
                            <JWControl:DateBox ID="txtStartTime" Width="120px" runat="server"></JWControl:DateBox>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            编制结束日期
                        </td>
                        <td>
                            <JWControl:DateBox ID="txtEndTime" Width="120px" runat="server"></JWControl:DateBox>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            填报单位
                        </td>
                        <td>
                            <asp:TextBox ID="txtUnit" Width="120px" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            施组名称
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" Width="120px" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="width: 100%;" id="trhide" runat="server"><td class="divFooter" style="text-align: left; width: 100%" runat="server">
                <asp:Button ID="BtnAdd" Text="新 增" runat="server" />
                <asp:Button ID="BtnUpd" Text="编 辑" Enabled="false" OnClick="BtnUpd_Click" runat="server" />
                <asp:Button ID="BtnDel" Text="删 除" Enabled="false" OnClick="BtnDel_Click" runat="server" />
                <asp:Button ID="BtnView" Text="查 看" Enabled="false" runat="server" />
                <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="023" BusiClass="001" runat="server" />
                <input id="HdnType" style="width: 88px; height: 22px" type="hidden" size="9" name="Hidden1" runat="server" />

                <input id="HdnPrjCode" style="width: 64px; height: 22px" type="hidden" size="5" name="Hidden1" runat="server" />

                <input id="HdnPrjName" style="width: 56px; height: 22px" type="hidden" size="4" name="Hidden2" runat="server" />

                <input id="HdnLevels" style="width: 88px; height: 22px" type="hidden" size="9" name="Hidden1" runat="server" />

                <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
                <asp:Button ID="btnFiles" Enabled="false" Text="归档" OnClick="btnFiles_Click" runat="server" />
            </td></tr>
        <tr>
            <td valign="top" height="100%">
                <asp:DataGrid ID="DGrdConstruct" DataKeyField="id" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="25" OnPageIndexChanged="DGrdConstruct_PageIndexChanged" runat="server"><ItemStyle CssClass="rowa"></ItemStyle><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><HeaderStyle CssClass="dgheader"></HeaderStyle><Columns><asp:TemplateColumn HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
<HeaderTemplate>
                                <asp:CheckBox ID="chkAll" runat="server" />
                            </HeaderTemplate>

<ItemTemplate>
                                <asp:Label ID="lblid" Visible="false" Text='<%# Convert.ToString(Eval("id")) %>' runat="server"></asp:Label>
                                <asp:CheckBox ID="chk" runat="server" />
                            </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn>
<HeaderTemplate>
                                <span></span>
                            </HeaderTemplate>

<ItemTemplate>
                                <img alt="" src="" id="imgPNG" style="width: 12px; height: 12px;" runat="server" />

                            </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
                                <%# Container.ItemIndex + 1 %>
                            </ItemTemplate>

<EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </EditItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="FillUnit" HeaderText="填报单位"></asp:BoundColumn><asp:TemplateColumn HeaderText="施组名称"><ItemTemplate>
                                <span class="al" onclick="openview('<%# Eval("FlowGuid") %>',0);">
                                    <%# Eval("TCO_Name") %>
                                </span>
                            </ItemTemplate></asp:TemplateColumn><asp:BoundColumn DataField="WeavePerson" HeaderText="编制人"></asp:BoundColumn><asp:BoundColumn DataField="WeaveTime" HeaderText="编制日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="Maindescripe" HeaderText="主要描述"></asp:BoundColumn><asp:TemplateColumn HeaderText="附件" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
                                <%# GetAnnx(1720, Convert.ToString(Eval("FlowGuid"))) %>
                            </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="mark" HeaderText="归档状态" Visible="false"></asp:BoundColumn><asp:TemplateColumn HeaderText="流程状态">
<ItemTemplate>
                                <%# Common2.GetState(Eval("AuditState").ToString()) %>
                            </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="FlowGuid" Visible="false"></asp:BoundColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfldConstruct" runat="server" />
    </form>
</body>
</html>
