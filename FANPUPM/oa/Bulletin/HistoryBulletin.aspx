<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HistoryBulletin.aspx.cs" Inherits="HistoryBulletin" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="com.jwsoft.pm.entpm.action"%>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>无标题页</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            showTooltip('tooltip', 25);
        });

        function ClickRow(FlowGuid, AuditState) {
            //		document.getElementById('hdnRecordID').value = FlowGuid;
        }

        function openWin(op) {
            var RID = "";
            if (op == "edit") {
                RID = document.getElementById('hdnRecordID').value;
            }
            var url = "EditBulletin.aspx?op=" + op + "&rid=" + RID;
            return window.showModalDialog(url, window, "dialogHeight:500px;dialogWidth:650px;center:1;help:0;status:0;");
        }
    </script>
</head>
<body class="body_frame" scroll="auto">
    <form id="form1" runat="server">
    <div>
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0"
            class="table-normal">
            <tr>
                <td class="td-title">
                    历史公告
                </td>
            </tr>
            <tr>
                <td style="height: 24px" class="td-search">
                    &nbsp;&nbsp;发布日期
                    <asp:TextBox ID="txtBeginDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                    至
                    <asp:TextBox ID="txtEndDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;按
                    <asp:DropDownList ID="ddlClass" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" runat="server"><asp:ListItem Value="V_TITLE" Selected="true" Text="公告标题" /><asp:ListItem Value="I_RELEASEBOUND" Text="发布范围" /><asp:ListItem Value="V_RELEASEUSER" Text="发布人" /></asp:DropDownList>
                    键值
                    <asp:TextBox ID="tbKey" Width="120px" ToolTip="输入查询关键字" runat="server"></asp:TextBox>
                    <asp:DropDownList ID="ddlnw" Style="width: 50px; display: none;" runat="server"><asp:ListItem Selected="true" Value="0" Text="内部" /><asp:ListItem Value="1" Text="外部" /></asp:DropDownList>
                    <asp:Button ID="btnSearch" CssClass="button-normal" OnClick="btnSearch_Click" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" AllowPaging="true" AutoGenerateColumns="false" PageSize="15" CssClass="grid" ToolTip="请双击查看详细信息" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="I_BULLETINID" runat="server">
<EmptyDataTemplate>
                            <table class="grid" cellspacing="0" rules="all" border="0" id="GridView1" style="border-collapse: collapse;">
                                <tr class="grid_head" style="position: static">
                                    <th scope="col" style="width: 30px;">
                                        序号
                                    </th>
                                    <th scope="col" style="width: 150px;">
                                        公告标题
                                    </th>
                                    <th scope="col" style="width: 70px;">
                                        发布范围
                                    </th>
                                    <th scope="col">
                                        分子机构名称
                                    </th>
                                    <th scope="col" style="width: 70px;">
                                        发布人
                                    </th>
                                    <th scope="col" style="width: 70px;">
                                        发布时间
                                    </th>
                                    <th scope="col" style="width: 80px;">
                                        流程状态
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="公告标题">
<ItemTemplate>
                                    <span class="tooltip" title='<%# Eval("V_TITLE") %>'><a href="BulletinLock.aspx?rid=<%# Eval("I_BULLETINID") %>&ic=<%# Eval("I_BULLETINID") %>"
                                        target="_blank">
                                        <%# StringUtility.GetStr(Eval("V_TITLE").ToString(), 25) %>
                                    </a></span>
                                </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="RELEASEBOUND" HeaderText="发布范围" SortExpression="RELEASEBOUND" /><asp:TemplateField HeaderText="分子机构名称" SortExpression="CorpCode"><ItemTemplate>
                                    <span class="tooltip" title='<%# BulletionAction.ReturnCropName(Eval("CorpCode").ToString()) %>'>
                                        <%# StringUtility.GetStr(BulletionAction.ReturnCropName(Eval("CorpCode").ToString()), 25) %>
                                    </span>
                                </ItemTemplate><EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" Text='<%# System.Convert.ToString(Eval("CorpName"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                </EditItemTemplate></asp:TemplateField><asp:BoundField DataField="V_RELEASEUSER" HeaderText="发布人" SortExpression="V_RELEASEUSER" /><asp:BoundField DataField="DTM_RELEASETIME" DataFormatString="{0:yyyy-MM-dd}" HeaderText="发布时间" HtmlEncode="false" SortExpression="DTM_RELEASETIME" /><asp:TemplateField HeaderText="流程状态">
<ItemTemplate>
                                    <%# Common2.GetState(Eval("AuditState").ToString()) %>
                                </ItemTemplate>
</asp:TemplateField></Columns><PagerStyle HorizontalAlign="Right"></PagerStyle></asp:GridView>
                </td>
            </tr>
        </table>
        <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </div>
    </form>
</body>
</html>
