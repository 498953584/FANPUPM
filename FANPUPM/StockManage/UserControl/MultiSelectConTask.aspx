<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MultiSelectConTask.aspx.cs" Inherits="StockManage_UserControl_MultiSelectConTask" EnableEventValidation="false" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>选择中标预算节点</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/Script/jquery.treetable.js"></script>
    <script type="text/javascript" src="/Script/jquery.blockUI.js"></script>
    <script type="text/javascript" src="/Script/jquery-budgetpait.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnBindTask').css('display', 'none');
            $('#gvwConTask').treetable({
                handlerUrl: '/BudgetManage/Handler/GetChildConstractTask.ashx',
                containsChk: true,
                isSelectTask: true,
				isDisplayDesc: true
            });
            // 绑定双击事件
            $('#gvwConTask').delegate('tr', 'dblclick', btnOk_click);
        });


        // 确定按钮事件
        function btnOk_click() {
            var idArr = new Array();
            $('#gvwConTask input:checked').each(function () {
                var thisTr = $(this).parents('tr');
                var id = thisTr.attr('id');
                var subCount = thisTr.attr('subCount');
                if (id && subCount) {
                    idArr.push(id);
                }
            });

            // 执行回调方法
            if (top.ui.callback != null && idArr.length > 0) {
                top.ui.callback(jw.array1dToJson(idArr));
                top.ui.callback = null
            }

            top.ui.closeWin();
        }

        // 取消按钮事件
        function btnCancel_click() {
            top.ui.closeWin();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="divHeader" style="text-align: left;">
            <asp:DropDownList ID="dropTaskType" AutoPostBack="true" OnSelectedIndexChanged="dropTaskType_SelectedIndexChanged" runat="server"><asp:ListItem Value="" Text="总预算" /><asp:ListItem Value="Y" Text="年度预算" /><asp:ListItem Value="M" Text="月度预算" /></asp:DropDownList>
            <asp:DropDownList ID="dropYear" AutoPostBack="true" OnSelectedIndexChanged="dropYear_SelectedIndexChanged" runat="server"><asp:ListItem Value="" Text="选择年份" /></asp:DropDownList>
            <asp:DropDownList ID="dropMonth" AutoPostBack="true" OnSelectedIndexChanged="dropMonth_SelectedIndexChanged" runat="server"><asp:ListItem Value="" Text="选择月份" /></asp:DropDownList>
        </div>
        <div style="width: 100%; height: 375px; overflow: auto; float: left;">
            <asp:GridView ID="gvwConTask" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwConTask_RowDataBound" DataKeyNames="TaskId,OrderNumber" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                            <asp:CheckBox ID="chkAll" runat="server" />
                        </HeaderTemplate><ItemTemplate>
                            <asp:CheckBox ID="chkSingle" runat="server" />
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Right"><ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate></asp:TemplateField><asp:BoundField DataField="TaskCode" HeaderText="清单编码" /><asp:BoundField DataField="TaskName" HeaderText="项目名称" /><asp:TemplateField HeaderText="项目特征描述" HeaderStyle-Width="100px"><ItemTemplate>
                            <span class="tooltip" title='<%# Eval("FeatureDescription") %>'>
                                <%# StringUtility.GetStr(Eval("FeatureDescription"), 30) %>
                            </span>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="类型"><ItemTemplate>
                            <%# Common2.GetTaskTypeName(Eval("OrderNumber").ToString()) %>
                        </ItemTemplate></asp:TemplateField><asp:BoundField DataField="Unit" HeaderText="单位" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
        </div>
        <div style="width: 100%; height: 25px; float: left; text-align: right; vertical-align: middle">
            <input type="button" id="btnOk" value="确定" onclick="btnOk_click();" style="margin: 10px;" />
            <input type="button" id="btnCancel" value="取消" onclick="btnCancel_click();" />
        </div>
    </div>
    <asp:Button ID="btnBindTask" Text="Button" OnClick="btnBindTask_Click" runat="server" />
    </form>
</body>
</html>
