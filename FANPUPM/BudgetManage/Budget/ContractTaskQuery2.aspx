<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContractTaskQuery2.aspx.cs" Inherits="BudgetManage_Budget_ContractTaskQuery2" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>合同预算查询</title><link type="text/css" href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/jquery.treetable.js"></script>
    <script type="text/javascript" src="../../Script/jquery.blockUI.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.extension.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#gvBudget').treetable(false, 'ConstractTask');
        });

        // 添加行进行显示资源信息
        var prevId;
        function showInfo(id) {
            var tab_Resource = null;
            $.ajax({
                type: 'GET',
                async: false,
                url: '/BudgetManage/Handler/ReturnConResource.ashx?' + new Date().getTime() + '&taskId=' + id + '&type=resources',
                success: function (data) {
                    tab_Resource = data;
                }
            });
            var isDisplay = $('#' + id + '1').get(0);
            if (isDisplay == undefined) {
                $('#' + id).after('<tr id="' + id + '1"><td align="center" colspan="12" style="border:solid 1px #CADEED;">' + tab_Resource + '</td></tr>');
                if (prevId != undefined && prevId != id) {
                    $('#' + prevId + '1').remove();
                }
                prevId = id;

            }
            else {
                $('#' + id + '1').remove();
                isDisplay = undefined;
            }
        }
    </script>
</head>
<body style="overflow-x: scroll; width: 99%; height: 99.3%">
    <form id="form1" runat="server">
    <div id="divBudget" class="pagediv">
        <asp:GridView ID="gvBudget" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvBudget_RowDataBound" DataKeyNames="TaskId,SubCount,OrderNumber" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                        <%# Eval("No") %>
                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="清单编码"><ItemTemplate>
                        <span style="vertical-align: middle; margin-right: 25px;">
                            <%# Eval("TaskCode") %>
                        </span>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称"><ItemTemplate>
                        <span style="vertical-align: middle;">
                            <%# Eval("TaskName") %>
                        </span>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目特征描述" HeaderStyle-Width="80px"><ItemTemplate>
                        <span class="tooltip" title='<%# Eval("FeatureDescription").ToString() %>'>
                            <%# StringUtility.GetStr(Eval("FeatureDescription"), 20) %>
                        </span>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="类型" HeaderStyle-Width="50px"><ItemTemplate>
                        <%# Eval("TypeName") %>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="50px"><ItemTemplate>
                        <%# Eval("Unit") %>
                    </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="工程量" DataField="Quantity" DataFormatString="{0:F3}" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="100px" ItemStyle-CssClass="decimal_input" /><asp:TemplateField HeaderText="综合单价" HeaderStyle-Width="70px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                        <%# System.Convert.ToDecimal(Eval("UnitPrice")).ToString() %>
                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="主材" HeaderStyle-Width="70px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                        <%# System.Convert.ToDecimal(Eval("MainMaterial")).ToString() %>
                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="辅材" HeaderStyle-Width="70px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                        <%# System.Convert.ToDecimal(Eval("SubMaterial")).ToString() %>
                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="人工" HeaderStyle-Width="70px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                        <%# System.Convert.ToDecimal(Eval("Labor")).ToString() %>
                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="合价" HeaderStyle-Width="70px" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                        <%# System.Convert.ToDecimal(Eval("Total")).ToString() %>
                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="开始时间" HeaderStyle-Width="50px"><ItemTemplate>
                        <%# Common2.GetTime(Eval("StartDate")) %>
                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结束时间" HeaderStyle-Width="50px"><ItemTemplate>
                        <%# Common2.GetTime(Eval("EndDate")) %>
                    </ItemTemplate></asp:TemplateField><asp:BoundField DataField="ConstructionPeriod" HeaderText="工期(天)" /><asp:TemplateField HeaderText="备注"><ItemTemplate>
                        <asp:HyperLink ID="hlinkShowNote" Style="text-decoration: none; color: Black;" ToolTip='<%# System.Convert.ToString(Eval("Note").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"><%# StringUtility.GetStr(Eval("Note").ToString()) %></asp:HyperLink>
                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
    </div>
    </form>
</body>
</html>
