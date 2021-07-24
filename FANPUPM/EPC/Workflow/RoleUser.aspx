<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleUser.aspx.cs" Inherits="RoleUser" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head runat="server"><title>RoleUser</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            var jwTable = new JustWinTable('dgdUser');
            setHeight();
        });
        function setHeight() {
            var height = document.getElementById("td-user").clientHeight;
            document.getElementById("divUser").style.height = height;
        }
        function doClickRow(usercode) {
            document.getElementById('hdnUserID').value = usercode;
            document.getElementById('btnDel').disabled = false;
        }
        function openRoleEdit(RoleID) {
            var url = "/EPC/Workflow/RoleUserSel.aspx?ri=" + RoleID
            top.ui._roleuser = window;
            window.parent.parent.desktop.flowclass = window;
            toolbox_oncommand(url, "角色信息维护");
        }	
    </script>
</head>
<body>
    <form id="Form1" runat="server">
    <table style="height: 99%" cellspacing="0" cellpadding="0" width="100%">
        <tr>
            <td class="divHeader" style="height: 22px">
                流程群组设置
            </td>
        </tr>
        <tr>
            <td align="left" style="height: 24px">
                <input id="hdnRoleID" style="width: 10px" type="hidden" name="hdnRoleID" runat="server" />

                <input id="hdnUserID" style="width: 10px" type="hidden" name="hdnUserID" runat="server" />

                <input type="button" id="btnAdd" value="新增" runat="server" />

                <asp:Button ID="btnDel" Text="删 除" Enabled="false" runat="server" />
            </td>
        </tr>
        <tr>
            <td valign="top" id="td-user">
                <div style="overflow: auto; width: 100%;" id="divUser">
                    <asp:DataGrid ID="dgdUser" CssClass="gvdata" CellPadding="0" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="25" OnPageIndexChanged="dgdUser_PageIndexChanged" runat="server"><AlternatingItemStyle CssClass="growb"></AlternatingItemStyle><ItemStyle CssClass="rowa"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="header" VerticalAlign="Middle"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle><Columns><asp:TemplateColumn HeaderText="序号" ItemStyle-Width="20px"><ItemTemplate>
                                    
                                </ItemTemplate></asp:TemplateColumn><asp:BoundColumn DataField="usercode" HeaderText="用户名称"></asp:BoundColumn><asp:BoundColumn DataField="CorpName" HeaderText="分子机构名称"></asp:BoundColumn></Columns><PagerStyle Mode="NumericPages"></PagerStyle></asp:DataGrid></div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
