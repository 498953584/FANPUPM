<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnnulConfirmList.aspx.cs" Inherits="HR_Leave_AnnulConfirmList" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>销假申请记录</title>
     <script language="javascript" type="text/javascript">
    <!--
    function ClickRow(recordid)
    {
        document.getElementById('HdnRecoreId').value = recordid; 
        document.getElementById('BtnConfirm').disabled = false;
        
    }
   
    //销假确认
    function openConfirm()
    {
        var rid = document.getElementById('HdnRecoreId').value ; 
        var url = "AnnulConfirmAdd.aspx?rid="+rid;
        return window.showModalDialog(url,window,"dialogHeight:265px;dialogWidth:400px;center:1;help:0;status:0;");
    }  
    -->    
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="form1" runat="server">
    <div>
     <table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0" class="table-normal">
            <tr>               
                <td class="td-title">
                    销假申请记录</td>
            </tr>
                <tr>
                <td class="td-toolsbar" style="height: 24px">             
                   <asp:Button ID="BtnConfirm" Width="85px" Text="销假审核" Enabled="false" Visible="false" OnClick="BtnConfirm_Click" runat="server" />&nbsp;
                    <asp:HiddenField ID="HdnRecoreId" runat="server" />                    
                    </td>
            </tr>  
            <tr>
                <td>
                     <div id="dvDeptBox" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
                         <asp:GridView ID="GVConfirm" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlConfirm" Width="100%" PageSize="25" OnRowDataBound="GVConfirm_RowDataBound" OnDataBound="GVConfirm_DataBound" DataKeyNames="RecordID,Days" runat="server"><Columns><asp:TemplateField HeaderText="序 号"></asp:TemplateField><asp:BoundField DataField="UserCode" HeaderText="申请人" SortExpression="UserCode" /><asp:BoundField DataField="LeaveType" HeaderText="请(休)假类型" SortExpression="LeaveType" /><asp:BoundField DataField="BeginDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="休假开始时间" HtmlEncode="false" SortExpression="BackDate" /><asp:BoundField HeaderText="流程状态" SortExpression="IsApply" /><asp:BoundField DataField="BackDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="销假时间" HtmlEncode="false" SortExpression="BackDate" /><asp:BoundField DataField="IsConfirm" HeaderText="是否销假" SortExpression="IsApply" /></Columns><RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle></asp:GridView>
                         <asp:SqlDataSource ID="SqlConfirm" SelectCommand="SELECT [RecordID], [AuditState], [UserCode], [RecordDate], [LeaveType], [LeaveDays], [BackDate], [Reason], [IsApply], [Days], [BeginDate], [IsConfirm], [ConfirmUser] FROM [HR_Leave_Application] WHERE ([AuditState] = @AuditState)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:Parameter DefaultValue="1" Name="AuditState" Type="String" /></SelectParameters></asp:SqlDataSource>
                     </div>
                     </td>
              </tr>
            </table>
    </div>
    </form>
</body>
</html>
