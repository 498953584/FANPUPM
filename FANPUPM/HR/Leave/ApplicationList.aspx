<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplicationList.aspx.cs" Inherits="HR_Leave_ApplicationList" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>我的请假记录</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript">
        function ClickRow(recordid, AuditState, UserName) {
            $('#HdnRecoreId').val(recordid);
            $('#WF1_FID').val(recordid);
            $('#WF1_BusinessCode').val('004');
            $('#WF1_BusinessClass').val('001');
            $('#WF1_PrjGuid').val('');
            $('#WF1_hidcontent').val(UserName + '请假审核');   //查看审核记录是显示审核内容在这里设置

            // 未提交状态
            if (AuditState == '-1') {
                $('#btnStartWF').removeAttr('disabled');            //可提交 
                $('#WF1_btnViewWF').attr('disabled', 'disabled');         //不可查看流程状态 
                $('#WF1_btnWFPrint').attr('disabled', 'disabled');         //不可查看审核记录 
            }

            if (AuditState == '-3') {
                $('#btnStartWF').removeAttr('disabled');            //可提交 
                $('#WF1_btnViewWF').removeAttr('disabled');
                $('#WF1_btnWFPrint').removeAttr('disabled');
            }

            // 以通过或者审核中驳回时
            if (AuditState == "1" || AuditState == "0" || AuditState == "-2") {
                $('#btnStartWF').attr('disabled', 'disabled');      //可提交 
                $('#WF1_btnViewWF').removeAttr('disabled');         //不可查看流程状态 
                $('#WF1_btnWFPrint').removeAttr('disabled');        //不可查看审核记录 
            }

            if (AuditState == "0") {
                $('#CancelBt').removeAttr('disabled');
            }
            else {
                $('#CancelBt').attr('disabled', 'disabled');
            }

            if (AuditState == "1" || AuditState == "0" || AuditState == "-3" || AuditState == "-2") {
                //彻底删除的可用性
                $('#BtnWFDel').removeAttr('disabled');
            }
            else {
                $('#BtnWFDel').attr('disabled', 'disabled');
            }

            if (AuditState == '-1') {
                document.getElementById('btnEdit').disabled = false;
                document.getElementById('btnDel').disabled = false;
            } else if (AuditState == '-3') {
                document.getElementById('btnEdit').disabled = false;
                document.getElementById('btnDel').disabled = true;
            }
            else {
                document.getElementById('btnEdit').disabled = true;
                document.getElementById('btnDel').disabled = true;
            }

            document.getElementById('btnView').disabled = false;

            // 提升管理员权限
            upAdminPrivilege();
        }

        // 请假申请添加\编辑
        function openEdit(t) {
            var rid = document.getElementById('HdnRecoreId').value;
            if (t == 'Add') {
                var url = '/HR/Leave/ApplicationAdd.aspx?t=' + t + '&rid=00000000-0000-0000-0000-000000000000';
            }
            else {
                var url = '/HR/Leave/ApplicationAdd.aspx?t=' + t + '&rid=' + rid;
            }
            $('#ifFram').attr('src', url);
            top.ui.callback = function () {
                document.getElementById('btnRefresh').click();
            }
            top.ui.openWin({ title: '新增请假', url: url, width: 400, height: 300 });
        }

        // 关闭弹出层
        function closeDialog() {
            top.ui.closeWin();
        }
        window['closeDialog'] = closeDialog;

        // 销假申请
        function openConfirm() {
            var rid = document.getElementById('HdnRecoreId').value;
            var url = "ApplicationConfirm.aspx?rid=" + rid;
            return window.showModalDialog(url, window, "dialogHeight:250px;dialogWidth:400px;center:1;help:0;status:0;");
        }

        // 查看
        function OpenLock() {
            var rid = document.getElementById('HdnRecoreId').value;
            //var url = "ApplicationLock.aspx?ic=" + rid;
            //return window.showModalDialog(url, window, "dialogHeight:240px;dialogWidth:400px;center:1;help:0;status:0;");
            var url = "ApplicationView.aspx?ic=" + rid;
            return window.showModalDialog(url, window, "dialogHeight:600px;dialogWidth:800px;center:1;help:0;status:0;");
        }
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="form1" runat="server">
    <div>
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" height="100%" align="center"
            border="0" class="table-normal">
            <tr>
                <td class="td-title">
                    我的请假记录
                </td>
            </tr>
            <tr>
                <td class="td-toolsbar" style="height: 24px">
                    <input type="button" value="新 增" id="btnAdd" onclick="openEdit('Add');" />
                    <input type="button" value="编 辑" id="btnEdit" onclick="openEdit('Edit');" disabled="disabled" />
                    <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
                    <asp:Button ID="BtnConfirm" Text="销假申请" Enabled="false" Style="display: none" runat="server" />&nbsp;
                    <asp:Button ID="btnView" Enabled="false" Text="查 看" runat="server" />
                    <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" runat="server" />
                    <asp:Button ID="btnRefresh" Style="display: none;" OnClick="btnRefresh_Click" runat="server" />
                    <asp:HiddenField ID="HdnRecoreId" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <div id="dvDeptBox" class="div-scroll" style="width: 100%; height: 100%">
                        <asp:GridView ID="GVApplication" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlApplication" PageSize="25" Width="100%" OnRowDataBound="GVApplication_RowDataBound" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
                                <table id="Table2" border="1" cellspacing="0" class="grid" rules="all" style="width: 100%;
                                    border-collapse: collapse">
                                    <tr class="grid_head">
                                        <th  nowrap="nowrap" scope="col" style="width: 30px">
                                            序 号
                                        </th>
                                        <th scope="col">
                                            申请人
                                        </th>
                                        <th nowrap="nowrap" scope="col" style="width: 80px">
                                            请(休)假类别
                                        </th>
                                        <th nowrap="nowrap" scope="col" style="width: 70px">
                                            申请时间
                                        </th>
                                        <th nowrap="nowrap" scope="col" style="width: 80px">
                                            休假开始时间
                                        </th>
                                        <th scope="col">
                                            请假天数
                                        </th>
                                        <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                            销假时间
                                        </th>
                                        <th scope="col">
                                            请假事由
                                        </th>
                                        <th scope="col">
                                            流程状态
                                        </th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="UserCode" HeaderText="申请人" SortExpression="UserCode" /><asp:BoundField DataField="LeaveType" HeaderText="请(休)假类别" SortExpression="LeaveType" /><asp:BoundField DataField="RecordDate" HeaderText="申请时间" SortExpression="RecordDate" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:BoundField DataField="BeginDate" HeaderText="休假开始时间" SortExpression="AuditState" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:BoundField DataField="Days" HeaderText="请假天数" SortExpression="LeaveDays" /><asp:BoundField DataField="BackDate" HeaderText="销假时间" SortExpression="BackDate" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:BoundField DataField="Reason" HeaderText="请假事由" SortExpression="Reason" /><asp:BoundField DataField="AuditState" HeaderText="流程状态" SortExpression="AuditState" /><asp:BoundField DataField="auditPerson" HeaderText="审核领导" Visible="false" /><asp:BoundField DataField="remark" HeaderText="领导意见" Visible="false" /></Columns><HeaderStyle CssClass="grid_head"></HeaderStyle><RowStyle CssClass="grid_row"></RowStyle></asp:GridView>
                        <asp:SqlDataSource ID="SqlApplication" SelectCommand="SELECT [RecordID], [AuditState], [UserCode], [RecordDate], [LeaveType], [LeaveDays], [BackDate], [IsApply], [Reason], [Days], [BeginDate], [IsConfirm], [ConfirmUser],[remark],[auditPerson] FROM [HR_Leave_Application] WHERE ([UserCode] = @UserCode) ORDER BY [RecordDate] DESC" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:SessionParameter Name="UserCode" SessionField="yhdm" Type="String"></asp:SessionParameter></SelectParameters></asp:SqlDataSource>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="divFram" title="" style="display: none">
        <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    </form>
</body>
</html>
