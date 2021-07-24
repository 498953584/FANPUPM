<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplicationAudit.aspx.cs" Inherits="HR_Leave_ApplicationAudit" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>请假记录审核</title>

    <script language="javascript" type="text/javascript">
    <!--
    function ClickRow(recordid,as)
    {
        document.getElementById('HdnRecoreId').value = recordid;  
    
        if(as == '1')
        {
            document.getElementById('btnOk').disabled = true;
            document.getElementById('BtnView').disabled = false; 
        }
        else
        {
           document.getElementById('btnOk').disabled = false;
           //document.getElementById('btnViewWF').disabled = false; 
        }
        document.getElementById('btnView').disabled = false;    
 
    }
 
   //查看
    function OpenLock()
    {
      var rid =  document.getElementById('HdnRecoreId').value ;
      var url = "ApplicationLock.aspx?ic="+rid;
      return window.showModalDialog(url,window,"dialogHeight:240px;dialogWidth:400px;center:1;help:0;status:0;");		        
    }
    //审核
    function OpenAudit()
    {
      var rid =  document.getElementById('HdnRecoreId').value ;
      var url = "ApplicationReq.aspx?ic="+rid+"&mid=qj";
      return window.showModalDialog(url,window,"dialogHeight:230px;dialogWidth:390px;center:1;help:0;status:0;");		        
    }
    -->    
    </script>

</head>
<body class="body_clear" scroll="no">
    <form id="form1" runat="server">
        <div>
            <table id="Table1" cellspacing="0" cellpadding="0" width="100%" height="100%" align="center"
                border="0" class="table-normal">
                <tr>
                    <td class="td-title">
                        请假审核情况</td>
                </tr>
                <tr>
                    <td class="td-toolsbar" style="height: 24px">
                        <asp:Button ID="btnOk" Width="85px" Text="请假审核" ForeColor="Black" OnClick="btnOk_Click" runat="server" />
                        <asp:Button ID="BtnView" Enabled="false" Text="查 看" runat="server" />
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
                                            <th align="center" nowrap="nowrap" scope="col" style="width: 40px">
                                                序 号</th>
                                            <th scope="col">
                                                申请人</th>
                                            <th nowrap="nowrap" scope="col" style="width: 80px">
                                                请(休)假类别</th>
                                            <th nowrap="nowrap" scope="col" style="width: 70px">
                                                申请时间</th>
                                            <th nowrap="nowrap" scope="col" style="width: 80px">
                                                休假开始时间</th>
                                            <th scope="col">
                                                请假天数</th>
                                            <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                                销假时间</th>
                                            <th scope="col">
                                                请假事由</th>
                                            <th scope="col">
                                                流程状态</th>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序 号"></asp:TemplateField><asp:BoundField DataField="UserCode" HeaderText="申请人" SortExpression="UserCode" /><asp:BoundField DataField="LeaveType" HeaderText="请(休)假类别" SortExpression="LeaveType" /><asp:BoundField DataField="RecordDate" HeaderText="申请时间" SortExpression="RecordDate" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:BoundField DataField="BeginDate" HeaderText="休假开始时间" SortExpression="AuditState" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:BoundField DataField="Days" HeaderText="请假天数" SortExpression="LeaveDays" /><asp:BoundField DataField="BackDate" HeaderText="销假时间" SortExpression="BackDate" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:BoundField DataField="Reason" HeaderText="请假事由" SortExpression="Reason" /><asp:BoundField DataField="AuditState" HeaderText="流程状态" SortExpression="AuditState" /><asp:BoundField DataField="auditPerson" HeaderText="审核人" /><asp:BoundField DataField="remark" HeaderText="审核意见" /></Columns><HeaderStyle CssClass="grid_head"></HeaderStyle><RowStyle CssClass="grid_row"></RowStyle></asp:GridView>
                            <asp:SqlDataSource ID="SqlApplication" SelectCommand="SELECT [RecordID], [AuditState], [UserCode], [RecordDate], [LeaveType], [LeaveDays], [BackDate], [IsApply], [Reason], [Days], [BeginDate], [IsConfirm], [ConfirmUser],[remark],[auditPerson]  FROM [HR_Leave_Application] WHERE [AuditState]<>1 ORDER BY [AuditState],[RecordDate] " ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:SessionParameter Name="UserCode" SessionField="yhdm" Type="String"></asp:SessionParameter></SelectParameters></asp:SqlDataSource>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
