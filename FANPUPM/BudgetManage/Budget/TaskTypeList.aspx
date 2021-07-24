<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaskTypeList.aspx.cs" Inherits="BudgetManage_Budget_TaskTypeList" %>
<%@ Import Namespace="Wuqi.Webdiyer"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>类型管理</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>

    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript">
        window.onload = function() {
            var taskTypeTable = new JustWinTable('gvwTaskType');
            setButton(taskTypeTable, 'btnDel', 'btnEdit', 'btnLook', 'hfldTaskTypeId');
            addEvent(document.getElementById('btnEdit'), 'click', editTaskType);
            addEvent(document.getElementById('btnLook'), 'click', queryTaskType);
            addEvent(document.getElementById('btnAdd'), 'click', addTaskType);
            document.getElementById('btnDel').onclick = deleteTaskType;
            function editTaskType() {
                parent.desktop.taskTypeEdit = window;
                var url = '/BudgetManage/Budget/TaskTypeEdit.aspx?id=' + document.getElementById('hfldTaskTypeId').value;
                toolbox_oncommand(url, "编辑任务类型");
            }
            function addTaskType() {
                parent.desktop.taskTypeEdit = window;
                var url = '/BudgetManage/Budget/TaskTypeEdit.aspx';
                toolbox_oncommand(url, "新增任务类型");
            }
            function queryTaskType() {
                var url = 'AddIncometBalance.aspx?t=1&id=' + document.getElementById('hfldTaskTypeId').value;
                winopen(url)
            }
            function deleteTaskType() {
                if (!confirm('系统提示\n\n确定要删除吗')) {
                    return false;
                }
            }
        }       
  
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader" style="display:none">
        <table class="tableHeader">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="类型管理" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <table style="width: 100%">
        <tr>
            <td style="width: 100%; vertical-align: top;">
                <table border="0" class="tab">
                    <tr>
                        <td class="divFooter" style="text-align: left;">
                                <input type="button" id="btnAdd" value="新增" />
                                <input type="button" id="btnEdit" disabled="disabled" value="编辑" />
                                <asp:Button ID="btnDel" Text="删  除" disabled="disabled" OnClick="btnDel_Click" runat="server" />
                                <input type="button" id="btnLook" style="display: none;" disabled="disabled" value="查看" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 100%; vertical-align: top;">
                            <div class="pagediv">
                                <asp:GridView ID="gvwTaskType" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="Id" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                                <asp:CheckBox ID="cbAllBox" runat="server" />
                                            </HeaderTemplate><ItemTemplate>
                                                <asp:CheckBox ID="cbBox" ToolTip='<%# System.Convert.ToString(Eval("Id"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                <%# Container.DataItemIndex + (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + 1 %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="类型名称"><ItemTemplate>
                                                <%# Eval("Name") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="类型级别"><ItemTemplate>
                                                <%# Eval("Layer") %>
                                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                                </webdiyer:AspNetPager>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfldTaskTypeId" runat="server" />
    <asp:HiddenField ID="hdContractID" runat="server" />
    </form>
</body>
</html>
