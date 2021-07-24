<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="BudTaskList.aspx.cs" Inherits="BudgetManage_Budget_BudTaskList" MaintainScrollPositionOnPostBack="true" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>任务选择</title><link rel="Stylesheet" type="text/css" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/jwJson.js"></script>
    <script type="text/javascript" src="../../Script/jquery.treetable.js"></script>
    <script type="text/javascript" src="../../Script/jquery.blockUI.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            showTooltip('tooltip', 10);
            var spType = getRequestParam('spType');
            $('#gvBudget').treetable(false, 'BudTaskList');
            replaceEmptyTable('emptyBudget', 'gvBudget');
            $('#gvBudget tr:gt(0)').live('click', function () {
                var isLeaf;
                var $tr = $('<TR id=' + this.id + ' code=' + this.code + '>' + this.innerHTML + '</TR>');
                if (spType == 'in') {
                    //资源与WBS挂钩时，如果是清单内的，判断选择的是否是子节点，如果是不是子节点禁用增加按钮
                    $.ajax({
                        type: 'GET',
                        async: false,
                        url: '/BudgetManage/Handler/IsLeafNode.ashx?' + new Date().getTime() + '&Type=Task&Id=' + this.id,
                        success: function (data) {
                            isLeaf = data;
                        }
                    });
                    if ($('#hfldIsWBSRelevance').val() == '1') {
                        if (isLeaf == '0') {
                            $('#btnSave').attr('disabled', 'disabled');
                            return;
                        } else {
                            $('#btnSave').removeAttr('disabled');
                            $('#hfldCheckedIds').val(this.id);
                        }
                    } else {
                        $('#btnSave').removeAttr('disabled');
                        $('#hfldCheckedIds').val(this.id);
                    }
                }
                else {
                    $.ajax({
                        type: 'GET',
                        async: false,
                        url: '/BudgetManage/Handler/IsLeafNode.ashx?' + new Date().getTime() + '&Type=Resource&Id=' + this.id,
                        success: function (data) {
                            isLeaf = data;
                        }
                    });
                    if ($('#hfldIsWBSRelevance').val() == '1') {
                        //资源与WBS挂钩，先判断是否是叶子节点，在判断是否被合同中控制指标和施工报量引用
                        if (isLeaf == 1) {
                            if (taskIdIsUsed(this.id) == "1") {
                                $('#btnSave').removeAttr('disabled');
                                $('#hfldCheckedIds').val(this.id);
                            }
                            else {
                                $('#btnSave').attr('disabled', 'disabled');
                            }
                        } else {
                            $('#btnSave').attr('disabled', 'disabled');
                        }
                    } else {
                        //资源与WBS不挂钩，判断是否被合同中控制指标和施工报量引用
                        if (taskIdIsUsed(this.id) == "1") {
                            $('#btnSave').removeAttr('disabled');
                            $('#hfldCheckedIds').val(this.id);
                        } else {
                            $('#btnSave').attr('disabled', 'disabled');
                        }
                    }
                }
            });
            //取消
            $('#btnCancel').click(function () {
                top.ui.closeWin();
            });
            //保存
            $('#btnSave').click(save);
            // 绑定双击事件
            $('#gvBudget').delegate('tr', 'dblclick', save);
        });
        //从任务节点是否被施工报量和控制指标的引用来判断是否是叶子节点，如果被引用，不认为是叶子节点
        function taskIdIsUsed(taskId) {
            var isLeaf;
            $.ajax({
                type: 'GET',
                async: false,
                url: '/BudgetManage/Handler/IsLeafNode.ashx?' + new Date().getTime() + '&Type=isUsed&Id=' + taskId,
                success: function (data) {
                    isLeaf = data;
                }
            });
            return isLeaf;
        }
        function save() {
            var taskId = document.getElementById('hfldCheckedIds').value;
            top.ui.callback(taskId);
            top.ui.closeWin();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%;">
        <div style="width: 100%; height: 430px; overflow: auto; float: left;">
            <asp:GridView ID="gvBudget" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwResource_RowDataBound" DataKeyNames="TaskId,SubCount,OrderNumber" runat="server">
<EmptyDataTemplate>
                    <table id="emptyBudget" class="gvdata">
                        <tr class="header">
                            <th scope="col" style="width: 25px;">
                                序号
                            </th>
                            <th scope="col">
                                清单编码
                            </th>
                            <th scope="col">
                                项目名称
                            </th>
                            <th scope="col">
                                项目特征描述
                            </th>
                            <th scope="col">
                                类型
                            </th>
                            <th scope="col">
                                单位
                            </th>
                            <th scope="col">
                                工程量
                            </th>
                            <th scope="col">
                                开始时间
                            </th>
                            <th scope="col">
                                结束时间
                            </th>
                            <th scope="col">
                                综合单价
                            </th>
                            <th scope="col">
                                合价
                            </th>
                            <th scope="col">
                                备注
                            </th>
                            <td id="Th1" scope="col" visible="false" runat="server">
                                排序
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                            <%# Eval("No") %>
                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="清单编码"><ItemTemplate>
                            <span class="tooltip" title='<%# Eval("TaskCode") %>'>
                                <%# StringUtility.GetStr(Eval("TaskCode").ToString(), 20) %>
                            </span>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称"><ItemTemplate>
                            <span class="tooltip" title='<%# Eval("TaskName") %>'>
                                <%# StringUtility.GetStr(Eval("TaskName").ToString(), 20) %>
                            </span>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目特征描述" HeaderStyle-Width="100px"><ItemTemplate>
                            <span class="tooltip" title='<%# Eval("FeatureDescription").ToString() %>'>
                                <%# StringUtility.GetStr(Eval("FeatureDescription"), 30) %>
                            </span>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="类型"><ItemTemplate>
                            <%# Eval("TypeName") %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="50px"><ItemTemplate>
                            <%# Eval("Unit") %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程量" HeaderStyle-Width="70px">
<ItemTemplate>
                            <%# Eval("Quantity") %>
                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="综合单价" HeaderStyle-Width="70px">
<ItemTemplate>
                            <%# System.Convert.ToDecimal(Eval("UnitPrice")).ToString("0.000") %>
                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="合价" HeaderStyle-Width="70px">
<ItemTemplate>
                            <%# System.Convert.ToDecimal(Eval("Total")).ToString("0.000") %>
                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="开始时间" HeaderStyle-Width="50px"><ItemTemplate>
                            <%# Common2.GetTime(Eval("StartDate")) %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结束时间" HeaderStyle-Width="50px"><ItemTemplate>
                            <%# Common2.GetTime(Eval("EndDate")) %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
                            <span class="tooltip" title='<%# Eval("Note") %>'>
                                <%# StringUtility.GetStr(Eval("Note").ToString()) %>
                            </span>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="排序" Visible="false"><ItemTemplate>
                            <%# StringUtility.GetStr(System.Convert.ToString(Eval("OrderNumber"))) %>
                        </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
        </div>
        <div id="divFooter2" class="divFooter2">
            <table class="tableFooter2">
                <tr>
                    <td>
                        <input type="button" id="btnSave" disabled="disabled" value="保存" />
                        <input type="button" id="btnCancel" value="取消" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:HiddenField ID="hfldCheckedIds" runat="server" />
    <asp:HiddenField ID="hfldType" runat="server" />
    
    <asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
    </form>
</body>
</html>
