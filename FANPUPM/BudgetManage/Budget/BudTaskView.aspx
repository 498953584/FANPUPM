<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BudTaskView.aspx.cs" Inherits="BudgetManage_Budget_BudTaskView" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link rel="stylesheet" type="text/css" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/jquery.treetable.js"></script>
    <script type="text/javascript" src="../../Script/jquery.blockUI.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#gvBudget').treetable(false, 'ColorRed');
            showTooltip('tooltip', 10);
            if ($('#hfldIsWBSRelevance').val() == '1') {
                $('#trQueryRes').hide();
            } else {
                $('#trQueryRes').show();
            }
        });

        //添加行进行显示资源信息
        var prevId;
        function showInfo(id) {
            var tab_Resource = null;
            $.ajax({
                type: 'GET',
                async: false,
                url: '/BudgetManage/Handler/ReturnResource.ashx?' + new Date().getTime() + '&taskId=' + id + '&type=check',
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
        //资源列表
        function showResList() {
            parent.parent.desktop.ResourceList = window;
            var url = '/BudgetManage/Budget/ResourceList.aspx?' + new Date().getTime() + '&prjId=' + $('#hfldPrjId').val() + '&isAllowEditRes=0';
            toolbox_oncommand(url, "查看资源");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="1" class="gvdata" style="border-collapse: collapse;" cellspacing="0">
            <tr class="header">
                <td align="center" style="width: 25%;">
                    项目名称
                </td>
                <td align="center" style="width: 20%;">
                    版本号
                </td>
                <td align="center" style="width: 20%;">
                    申请人
                </td>
                <td align="center" style="width: 30%;">
                    备注
                </td>
            </tr>
            <tr class="rowa">
                <td align="center">
                    <asp:Label ID="lblPrjName" runat="server"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lblVersionCode" runat="server"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lblUserCode" runat="server"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lblNote" runat="server"></asp:Label>
                </td>
            </tr>
            <tr id="trQueryRes">
                <td colspan="4">
                    <span class="link" onclick="showResList();">查看资源</span>
                </td>
            </tr>
        </table>
        <div id="divBudget" class="pagediv">
            <asp:GridView ID="gvBudget" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvBudget_RowDataBound" DataKeyNames="TaskId,SubCount,OrderNumber,Modified" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                            <%# Eval("No") %>
                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="清单编码"><ItemTemplate>
                            <%# Eval("TaskCode") %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称"><ItemTemplate>
                            <span style="vertical-align: middle;">
                                <%# Eval("TaskName") %>
                            </span>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目特征描述" HeaderStyle-Width="100px"><ItemTemplate>
                            <span class="tooltip" title='<%# Eval("FeatureDescription").ToString() %>'>
                                <%# StringUtility.GetStr(Eval("FeatureDescription"), 30) %>
                            </span>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="类型" HeaderStyle-Width="50px"><ItemTemplate>
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
                        </ItemTemplate></asp:TemplateField><asp:BoundField DataField="ConstructionPeriod" HeaderText="工期(天)" HeaderStyle-Width="50px" /><asp:TemplateField HeaderText="备注"><ItemTemplate>
                            <span class="tooltip" title='<%# Eval("Note").ToString() %>'>
                                <%# StringUtility.GetStr(Eval("Note").ToString(), 10) %>
                            </span>
                        </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
        </div>
    </div>
    <asp:HiddenField ID="hfldPrjId" runat="server" />
    
    <asp:HiddenField ID="hfldCheckedIds" runat="server" />
    
    <asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
    </form>
</body>
</html>
