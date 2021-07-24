<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContractTask.aspx.cs" Inherits="BudgetManage_Budget_ContractTask" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>中标预算导入</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/Script/jquery.cookie.js"></script>
    <script type="text/javascript" src="../../Script/wf.js"></script>
    <script type="text/javascript" src="../../Script/HideButtons.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery-budgetContractTask.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#gvBudget').treetable(true, 'ConstractTask');
            $('#gvBudget').delegate(':checkbox:gt(0)', 'click', checkSingle);
            $('#gvBudget').delegate(':checkbox:eq(0)', 'click', checkAll);
            setControlButton('hfldCheckedIds', 'btnAdd', 'btnEdit', 'btnDelTask', 'btnExcelImport', '', '', '', true);
            setHiddenId('hfldPrjId', 'hfldInputUser', 'hfldIsLocked', 'hfldEditOrAdd');

            // 无任务节点不显示汇总信息
            if ($('#gvBudget').get(0) != undefined) {
                var tb = document.getElementById('gvBudget');
                if (tb != null) {
                    var row = tb.rows[1];
                    var firsId = row.id;
                    showHuiZong(firsId);
                }
            }
            setWFButtonState($('#hfldContractTaskAuditId').val(), $('#hfldPrjId').val(), $('#hfldFlowState').val(), true);
            // 当没有数据时，禁用“提交审核”按钮
            if (document.getElementById('gvBudget') == null) {
                $('#btnStartWF').attr('disabled', 'disabled');
                $('#btnEdit').attr('disabled', 'disabled');
                $('#btnDelTask').attr('disabled', 'disabled');
            }

            // 隐藏按钮
            hideButtons('tdButtons');


            if ($('#hfldPrjId').val() == '') {
                $('#btnAdd').attr('disabled', 'disabled');
                $('#btnExcelImport').attr('disabled', 'disabled');
            } else {
                $('#btnAdd').attr('disabled', false);
                $('#btnExcelImport').attr('disabled', false);
            }

            if ($('#hfldFlowState').val() == '1' || $('#hfldFlowState').val() == '0' || $('#hfldFlowState').val() == '-2') {
                $('#btnExcelImport').attr('disabled', 'disabled');
                $('#btnAdd').attr('disabled', 'disabled');
            }
            if ($('#hfldIsLocked').val() == 'False') {
                $('#btnExcelImport').removeAttr('disabled');
            }
            // 显示被截取的信息
            //jw.tooltip();
        });

        // 显示汇总信息
        function showHuiZong(id) {
            $('#statisticsTable').html(getTable(id));
        }

        // 查看资源信息
        var prevId;
        function showInfo(id) {
            showHuiZong(id);
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
                $('#' + id).after('<tr id="' + id + '1"><td align="center" colspan="17" style="border:solid 1px #CADEED;">' + tab_Resource + '</td></tr>');
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

        // 合同预算节点人材机和总成本汇总
        function getTable(id) {
            var tab_Resource = null;
            $.ajax({
                type: 'GET',
                async: false,
                url: '/BudgetManage/Handler/ReturnConResource.ashx?' + new Date().getTime() + '&taskId=' + id + '&type=resourcetotal&prjId=' + $('#hfldPrjId').val(),
                success: function (data) {
                    tab_Resource = data;
                }
            });
            return tab_Resource;
        }
        // exce导入
        function excelImport() {
            var year = jw.getRequestParam('year');
            var isContinue = true;
            var taskId = $('#hfldCheckedIds').val();
            var count = $('#gvBudget').get(0) == undefined ? 0 : 1;
            if (taskId == '' && count != '0') {
                isContinue = confirm("系统提示：\n没有选择导入Excel的根节点，将会覆盖当前版本！\n是否继续导入Excel？");
            }
            if (isContinue) {
                top.ui._contractTask = window;
                var prjid = $("#hfldPrjId").val();
                var url = "/BudgetManage/Budget/ExcelImportContract.aspx?taskId=" + taskId + "&year=" + year + "&prjId=" + prjid + "&version=1";
                toolbox_oncommand(url, "Excel导入");
            }
        }
    </script>
    <style type="text/css">
        .descTd
        {
            text-align: right;
        }
    </style>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">
    <div id="divContent2" class="divContent2" data-options="region:'center',title:''">
        <table style="width: 100%; height: 97%; vertical-align: top;" border="0" class="tab">
            <tr id="tr_Buttons">
                <td colspan="3" class="divFooter" style="text-align: left;" id="tdButtons">
                    <input type="button" id="btnAdd" value="新增" runat="server" />

                    <input type="button" id="btnEdit" value="编辑" disabled="disabled" />
                    <input type="button" id="btnExcelImport" style="cursor: pointer; width: 80px;" value="Excel导入" onclick="excelImport()" runat="server" />

                    <asp:Button ID="btnGenerage" Text="自动生成" Width="80px" ToolTip="根据主项目的预算自动生成子项目的预算" CssClass="tooltip" Visible="false" OnClick="btnGenerage_Click" runat="server" />
                    <asp:Button ID="btnDelTask" Text="删除" disabled="disabled" OnClientClick="return confirm('确定要删除吗？')" OnClick="btnDelTask_Click" runat="server" />
                    <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="121" BusiClass="001" runat="server" />
                    <asp:Label ID="lblLockPrjMessage" Font-Bold="true" runat="server"></asp:Label>
                    <a id="linkADownLoad" class="link" style="margin-top: 4px; margin-right: 5px; white-space: nowrap;"
                        href="../../Download/合同预算模板.xls">合同预算模板</a>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top;" colspan="3">
                    <div id="divBudget" class="pagediv" style="overflow: hidden; vertical-align: top;
                        margin: 0px;">
                        <asp:GridView ID="gvBudget" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="TaskId,SubCount,OrderNumber" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
                                        <asp:CheckBox ID="cbAllBox" runat="server" />
                                    </HeaderTemplate>

<ItemTemplate>
                                        <asp:CheckBox ID="cbBox" runat="server" />
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                                        <%# Eval("No") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="清单编码"><ItemTemplate>
                                        <span style="vertical-align: middle; margin-right: 25px;">
                                            <%# Eval("TaskCode").ToString().Trim() %>
                                        </span>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称"><ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("TaskName").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("TaskName"), 10) %>
                                        </span>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目特征描述" HeaderStyle-Width="20px"><ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("FeatureDescription").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("FeatureDescription"), 30) %>
                                        </span>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="类型" HeaderStyle-Width="50px"><ItemTemplate>
                                        <%# Eval("TypeName") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="50px"><ItemTemplate>
                                        <%# Eval("Unit") %>
                                    </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="工程量" DataField="Quantity" DataFormatString="{0:F3}" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="70px" ItemStyle-CssClass="decimal_input" /><asp:TemplateField HeaderText="综合单价" HeaderStyle-Width="70px" ItemStyle-CssClass="decimal_input">
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
                                    </ItemTemplate></asp:TemplateField><asp:BoundField DataField="ConstructionPeriod" HeaderText="工期(天)" HeaderStyle-Width="50px" /><asp:TemplateField HeaderText="备注"><ItemTemplate>
                                        <asp:HyperLink ID="hlinkShowNote" Style="text-decoration: none; color: Black;" ToolTip='<%# System.Convert.ToString(Eval("Note").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"><%# StringUtility.GetStr(Eval("Note").ToString()) %></asp:HyperLink>
                                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div data-options="region:'south',title:'',split:true" style="height: 85px;">
        <table style="width: 100%;" border="0">
            <tr>
                <td id="statisticsTable">
                </td>
            </tr>
        </table>
    </div>
    <div id="editTask" title="新增/编辑节点" style="display: none">
        <iframe id="editTaskFrame" frameborder="0" width="100%" height="100%"></iframe>
    </div>
    
    <asp:HiddenField ID="hfldCheckedIds" runat="server" />
    
    <asp:HiddenField ID="hfldPrjId" runat="server" />
    
    <asp:HiddenField ID="hfldContractTaskAuditId" runat="server" />
    
    <asp:HiddenField ID="hfldEditOrAdd" runat="server" />
    
    <asp:HiddenField ID="hfldInputUser" runat="server" />
    
    <asp:HiddenField ID="hfldFlowState" runat="server" />
    <asp:HiddenField ID="hfldIsLocked" runat="server" />
    </form>
    <script type="text/javascript" src="../../Script/jquery.treetable.js"></script>
    <script type="text/javascript" src="../../Script/jquery.blockUI.js"></script>
</body>
</html>
