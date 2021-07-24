<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditModifyTest.aspx.cs" Inherits="BudgetManage_Budget_EditModifyTest" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>变更详情</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnBindRes').hide();
            Val.validate('form1', 'btnSave');
            setTask();
            if ($('#hfldIsWBSRelevance').val() == '1') {
                $('#btnAllocRes').show();             
            } else {
                $('#btnAllocRes').hide();
            }
        });
        //设置任务节点
        function setTask() {
            var jwTask = new JustWinTable('gvBudget');
            jwTask.registClickTrListener(function () {
                $('#hfldEditModifyTaskId').val(this.id);
                $('#btnEditBudModify').attr('disabled', false);
                $('#btnDelBudModify').attr('disabled', false);
                $('#btnAllocRes').attr('disabled', false);
            });
            setButton(jwTask, 'btnDelBudModify', 'btnEditBudModify', 'btnAddBudModify', 'hfldEditModifyTaskId');
        }    
        // 新增预算变更
        function addBudModify() {    
            var contractId = jw.getRequestParam('ContractId');
            var url = '/ContractManage/PayoutModify/AddBudModify.aspx?type=add&prjId=' + $('#hfldPrjId').val()
				+ '&modifyId=' + $('#hfldModifyId').val();
            top.ui.callback = function (t) {
                var arr = new Array();
                var oldJson = $('#hfldAllModifyTaskJson').val();
                if (oldJson == '') {
                    arr.push(t);
                } else {
                    arr = JSON.parse(oldJson);
                    arr.push(t);
                }
                $('#hfldAllModifyTaskJson').val(JSON.stringify(arr));
                $('#btnRefresh').click();
            }
            top.ui.openWin({ title: '新增预算变更', url: url, width: 700, height: 500 });
        }

        // 编辑预算变更
        function editBudModify() {
            var mtid = $('#hfldEditModifyTaskId').val(); 	// ModifyTaskId
            if (!mtid) return false;
            var json = $('#hfldAllModifyTaskJson').val();
            if (json.length <= 2) return false;

            var arr = JSON.parse(json);
            for (var i = 0; i < arr.length; i++) {
                if (arr[i].ModifyTaskId == mtid) {
                    setCookie("modifyTaskJson", JSON.stringify(arr[i]), 1, "/");
                    break;
                }
            }

            var contractId = jw.getRequestParam('ContractId');
            var url = '/ContractManage/PayoutModify/AddBudModify.aspx?type=edit&prjId=' + $('#hfldPrjId').val()
				+ '&modifyId=' + $('#hfldModifyId').val()
				+ '&modifyTaskId=' + mtid;

            top.ui._payconmodify = window;

            top.ui.callback = function (t) {
                var arr = JSON.parse($('#hfldAllModifyTaskJson').val());
                for (var i = 0; i < arr.length; i++) {
                    if (arr[i].ModifyTaskId == t.ModifyTaskId) {
                        arr.splice(i, 1, t);
                    }
                }
                $('#hfldAllModifyTaskJson').val(JSON.stringify(arr));
                $('#btnRefresh').click();
            }

            top.ui.openWin({ title: '编辑预算变更', url: url, width: 700, height: 500 });
            $('#btnEditBudModify').attr('mtid', '')
        }
      
        // 资源配置
        function allocRes() {
            var mtid = $('#hfldEditModifyTaskId').val();
            var modifyType = $('#' + mtid).attr('modifyType')
            var spType = 'in';
            if (modifyType == '0')
                spType = 'out';
            parent.desktop.BudModifyResList = window;
            var url = '/BudgetManage/Budget/BudModifyResList.aspx?' + new Date().getTime() + '&spType=' + spType;
            url += '&modifyId=' + $('#hfldBudModifyId').val() + '&modifyTaskId=' + mtid + '&modifyPage=0';
            toolbox_oncommand(url, '选择资源');
        }

      
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divContent2" id="divModifyInfo">
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word" style="white-space: nowrap;">
                    附件
                </td>
                <td colspan="3" style="padding-right: 0px;">
                    <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="uploadModify" Class="BudModify" Name="附件" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    编号
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtModifyCode" Height="15px" Width="100%" CssClass="{required:true, messages:{required:'编号必须输入'}}" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    变更内容
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtModifyContent" Height="15px" Width="100%" CssClass="{required:true, messages:{required:'变更内容必须输入'}}" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    变更文件编号
                </td>
                <td>
                    <asp:TextBox ID="txtModifyFileCode" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    预算成本
                </td>
                <td>
                    <input type="text" id="txtBudAmount" readonly="readonly" class="readonly" value="0.00" style="height: 15px; width: 100%;" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="word">
                    报审价
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtReportAmount" Height="15px" Width="100%" CssClass="decimal_input" Text="0.000" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    核准价
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtApprovalAmount" Height="15px" Width="100%" CssClass="decimal_input" Text="0.000" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    核准日期
                </td>
                <td>
                    <asp:TextBox ID="txtApprovalDate" Height="15px" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="word">
                    备注
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtNotes" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div id="divTask">
            <table class="tableContent2" cellpadding="5px" cellspacing="0">
                <tr>
                    <td>
                        <input type="button" id="btnAddBudModify" value="新增" onclick="addBudModify();" />
                        <input type="button" id="btnEditBudModify" value="编辑" onclick="editBudModify();"
                            disabled="disabled" />
                       
                        <asp:Button Text="删除" ID="btnDel" OnClientClick="return confirm('您确定要删除吗？')" disabled="disabled" OnClick="btnDel_Click" runat="server" />
                        <input type="button" id="btnAllocRes" value="资源配置" style="width: 70px; display: none;"
                            onclick="allocRes();" disabled="disabled" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvBudget" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvBudget_RowDataBound" DataKeyNames="ModifyTaskId,TaskId,OrderNumber,ModifyType" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                        <asp:HiddenField ID="hfldModifyTaskId" Value='<%# System.Convert.ToString(Eval("ModifyTaskId"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="名称"><ItemTemplate>
                                        <asp:HiddenField ID="hfldTaskId" Value='<%# System.Convert.ToString(Eval("TaskId"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                        <asp:HiddenField ID="hfldParentId" Value='<%# System.Convert.ToString(Eval("ParentId"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                        <span style="vertical-align: middle;">
                                            <asp:Label ID="lblModifyTaskContent" flag="Quantity" Style="text-align: right;" Text='<%# System.Convert.ToString(Eval("ModifyTaskContent"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                        </span>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="编码"><ItemTemplate>
                                        <asp:Label ID="lblModifyTaskCode" flag="Quantity" Style="text-align: right;" Text='<%# System.Convert.ToString(Eval("ModifyTaskCode"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="类型" HeaderStyle-Width="50px"><ItemTemplate>
                                        <%# Common2.GetTaskTypeName(Eval("OrderNumber").ToString()) %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="50px"><ItemTemplate>
                                        <asp:Label ID="lblUnit" flag="Unit" CssClass="{required:true, messages:{required:'清单内的单位必须输入'}}" Text='<%# System.Convert.ToString(Eval("Unit"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程量" HeaderStyle-Width="70px">
<ItemTemplate>
                                        <asp:Label ID="lblQuantity" flag="Quantity" CssClass="decimal_input" Style="text-align: right;" Text='<%# System.Convert.ToString(Eval("Quantity"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="开始时间" HeaderStyle-Width="50px"><ItemTemplate>
                                        <%# Common2.GetTime(Eval("StartDate")) %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结束时间" HeaderStyle-Width="50px"><ItemTemplate>
                                        <%# Common2.GetTime(Eval("EndDate")) %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="综合单价" HeaderStyle-Width="70px">
<ItemTemplate>
                                        <asp:Label ID="lblUnitPrice" flag="UnitPrice" CssClass="decimal_input" Style="text-align: right;" Text='<%# System.Convert.ToString(Eval("UnitPrice"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="小计" HeaderStyle-Width="70px">
<ItemTemplate>
                                        <asp:Label ID="lblTotal" flag="Total2" CssClass="decimal_input" Style="text-align: right;" Text='<%# System.Convert.ToString(Eval("Total2"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="变更类型" HeaderStyle-Width="70px">
<ItemTemplate>
                                     <asp:Label ID="lblModifyType" flag="ModifyType" Style="text-align: right;" Text='<%# System.Convert.ToString((Eval("ModifyType").ToString() == "0") ? "清单外" : "清单内", System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>                                
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
                                        <asp:Label ID="hlinkShowNote" Style="text-decoration: none; color: Black;" Text='<%# System.Convert.ToString(Eval("Note").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="排序" Visible="false"><ItemTemplate>
                                        <asp:HiddenField ID="hfldOrderNumber" Value='<%# System.Convert.ToString(StringUtility.GetStr(System.Convert.ToString(Eval("OrderNumber"))), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />                                      
                                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                    <asp:Button ID="btnCancel" Text="取消" OnClick="btnCancel_Click" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    
    <asp:Button ID="btnBindRes" OnClick="btnBindRes_Click" runat="server" />
    
    <asp:HiddenField ID="hfldModifyId" runat="server" />
    
    <asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
    
    <asp:HiddenField ID="hfldPrjId" runat="server" />
    <!-- 最后添加或者变更的预算变更信息-->
    <asp:HiddenField ID="hfldAllModifyTaskJson" runat="server" />
    <!-- 要编辑的预算变更节点Id-->
    <asp:HiddenField ID="hfldEditModifyTaskId" runat="server" />
    <asp:Button ID="btnRefresh" Text="" Style="display: none;" OnClick="btnRefresh_Click" runat="server" />
    </form>
</body>
</html>
