<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dbrSet.aspx.cs" Inherits="dbrSet" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>代办人设置</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            var jwtable = new JustWinTable('dgFlow');
            replaceEmptyTable('emptyTable', 'dgFlow');
            setHeight();
        });
        function doClickRow(userCode, templateId, nodeid) {
            var url = "dbrInfo.aspx?tid=" + templateId + "&code=" + userCode + "&nodeid=" + nodeid;
            document.getElementById('dbrInfo').src = url;
        }

        function openRoleEdit(op, businessCode, businessClass) {
            var templateID = 0;
            if (op == 2) {
                templateID = document.getElementById('hdnTemplateID').value;
            }
            var url = "TemplateEdit.aspx?t=" + op + "&ti=" + templateID + "&code=" + businessCode + "&class=" + businessClass;
            return window.showModalDialog(url, window, "dialogHeight:200px;dialogWidth:450px;center:1;help:0;status:0;");
        }

        function setHeight() {
            var height = (document.getElementById("table-flow").clientHeight - 22) / 2;
            document.getElementById("divflow").style.height = height;
            document.getElementById("dbrInfo").style.height = height;
        }
    </script>
</head>
<body>
    <form id="Form1" runat="server">
    <table style="height: 95%; vertical-align: middle;" width="100%" id="table-flow">
        <tr>
            <td style="vertical-align: top; height: 25px;">
                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                    <tr>
                        <td class="descTd" style="white-space: nowrap;">
                            流程类别
                        </td>
                        <td>
                            <asp:DropDownList ID="DDLBusinessClass" Width="145px" runat="server"></asp:DropDownList>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            模板名称
                        </td>
                        <td>
                            <asp:TextBox ID="txtTemplateName" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd" style="white-space: nowrap;">
                            节点名称
                        </td>
                        <td>
                            <asp:TextBox ID="txtNodeName" runat="server"></asp:TextBox>
                        </td>
                        <td style="white-space: nowrap;">
                            <asp:Button ID="brnQuery" Width="80px" Text="查询" OnClick="brnQuery_Click" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; height: 280px; border-bottom: solid 2px #cccccc;">
                <div style="overflow: auto; width: 100%; height: 280px" id="divflow">
                    <asp:DataGrid ID="dgFlow" CssClass="gvdata" AutoGenerateColumns="false" Width="100%" AllowPaging="false" PageSize="10" OnItemDataBound="dgFlow_ItemDataBound" OnPageIndexChanged="dgFlow_PageIndexChanged" runat="server"><ItemStyle CssClass="rowa"></ItemStyle><AlternatingItemStyle CssClass="rowb"></AlternatingItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
                                    <%# Eval("Num") %>
                                </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="模板名称"><ItemTemplate>
                                    <asp:Label ID="TxtFlowName" Width="100%" Text='<%# Eval("TemplateName") %>' runat="server"></asp:Label>
                                </ItemTemplate></asp:TemplateColumn><asp:BoundColumn HeaderText="流程名称" DataField="BusinessClassName"></asp:BoundColumn><asp:TemplateColumn HeaderText="节点名称"><ItemTemplate>
                                    <asp:Label ID="TxtNodeName" Width="100%" Text='<%# Eval("NodeName") %>' runat="server"></asp:Label>
                                </ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="审核人"><ItemTemplate>
                                    <asp:Label ID="TxtAuditor" Width="100%" Text='<%# base.GetUserName(Eval("Codes").ToString()) %>' runat="server"></asp:Label>
                                </ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="备注"><ItemTemplate>
                                    <asp:Label ID="TxtRemark" Width="100%" Text='<%# Eval("Remark") %>' runat="server"></asp:Label>
                                </ItemTemplate></asp:TemplateColumn></Columns><PagerStyle Mode="NumericPages"></PagerStyle></asp:DataGrid>
                    <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                    </webdiyer:AspNetPager>
                </div>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; padding-top: 0;">
                <iframe id="dbrInfo" name="dbrInfo" frameborder="0" width="100%" height="350px">
                </iframe>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
