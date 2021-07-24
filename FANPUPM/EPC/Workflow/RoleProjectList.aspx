<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleProjectList.aspx.cs" Inherits="ModuleSet_Workflow_RoleProjectList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>项目相关角色设置</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('GVRoleProject');
            replaceEmptyTable('emptyTable', 'GVRoleProject');
        });

        function setHeight() {
            var height = window.screen.height - 370;
            document.getElementById("divroleproject").style.height = height;
        }

        function ClickRow(recordid) {
            document.getElementById('HdnRoleProjectID').value = recordid;
            document.getElementById('btnEdit').disabled = false;
            document.getElementById('btnDel').disabled = false;
        }

        //添加\编辑
        function openEdit(t, rl) {
            var rid = document.getElementById('HdnRoleProjectID').value;
            if (t == 'Add') {
                var url = '/EPC/Workflow/RoleProjectEdit.aspx?t=' + t + '&rid=0&rl=' + rl;
            }
            else {
                var url = '/EPC/Workflow/RoleProjectEdit.aspx?t=' + t + '&rid=' + rid + '&rl=' + rl;
            }
            top.ui._roleprojectlist = window;
            //            window.parent.parent.desktop._roleprojectlist = window;
            toolbox_oncommand(url, "项目相关角色信息维护");
        }
    </script>
</head>
<body scroll="no" onload="setHeight();">
    <form id="form1" runat="server">
    <div>
        <table class="tab" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td align="center" class="divHeader" height="28" style="width: 518px">
                    项目相关角色设置
                </td>
            </tr>
            <tr>
                <td style="height: 24px">
                    <input id="btnAdd" type="button" value="新增" runat="server" />

                    <input id="btnEdit" type="button" value="修改" disabled="true" runat="server" />

                    <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />&nbsp;
                    <asp:HiddenField ID="HdnRoleProjectID" runat="server" />
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div style="overflow: auto; width: 100%;" id="divroleproject">
                        <asp:GridView ID="GVRoleProject" PageSize="25" AllowPaging="true" AutoGenerateColumns="false" CssClass="gvdata" DataSourceID="SqlRoleProject" Width="100%" OnRowDataBound="GVRoleProject_RowDataBound" runat="server">
<EmptyDataTemplate>
                                <table width="100%" id="emptyTable">
                                    <tr class="header">
                                        <td>
                                            序号
                                        </td>
                                        <td>
                                            项目负责人
                                        </td>
                                        <td>
                                            所负责项目
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:BoundField HeaderText="序 号" InsertVisible="false" ReadOnly="true" SortExpression="RoleProjectID" /><asp:BoundField DataField="UserCode" HeaderText="项目负责人" SortExpression="UserCode" /><asp:BoundField DataField="ProjectCodes" HeaderText="所负责项目" SortExpression="ProjectCodes" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                        <asp:SqlDataSource ID="SqlRoleProject" SelectCommand="SELECT [RoleProjectID], [UserCode], [RoleID], [ProjectCodes] FROM [WF_RoleProject] WHERE ([RoleID] = @RoleID)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="RoleID" QueryStringField="ri" Type="Int32"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
