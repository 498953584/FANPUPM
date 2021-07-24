<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WFOvertimeCountDetails.aspx.cs" Inherits="ModuleSet_Workflow_WFOvertimeCountDetails" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>流程超时情况</title></head>
<body class="body_clear" scroll="no">
    <form id="form1" runat="server">
    <div>
      <table id="Table1" cellspacing="0" cellpadding="0" width="100%" style="height:100%" align="center" border="0" class="table-normal">
         <tr>               
            <td class="td-title">超时明细</td>
        </tr>
        <tr>
            <td>
            <div id="dvDeptBox" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
                <asp:GridView ID="GVInstance" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlInstance" Width="100%" OnRowDataBound="GVInstance_RowDataBound" DataKeyNames="NoteID" runat="server">
<EmptyDataTemplate>
                        <table id="Table2" border="1" cellspacing="0" class="grid" rules="all" style="width: 100%;
                            border-collapse: collapse">
                            <tr class="grid_head">
                                <th scope="col">
                                    节点说明</th>
                                <th scope="col">
                                    审核人</th>
                                <th scope="col">
                                    流程状态</th>
                                <th scope="col">
                                    到达时间</th>
                                <th scope="col">
                                    审核时间</th>
                                <th scope="col">
                                    是否超时</th>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><Columns><asp:BoundField DataField="NodeName" HeaderText="节点说明" SortExpression="NodeName" /><asp:BoundField DataField="Operator" HeaderText="审核人" SortExpression="Operator" /><asp:BoundField DataField="Status" HeaderText="流程状态" SortExpression="Sing" /><asp:BoundField DataField="During" HeaderText="失效时限" SortExpression="Sing" /><asp:BoundField DataField="ArriveTime" HeaderText="到达时间" SortExpression="ArriveTime" /><asp:BoundField DataField="AuditDate" HeaderText="审核时间" SortExpression="AuditDate" /><asp:BoundField HeaderText="是否超时" InsertVisible="false" ReadOnly="true" SortExpression="NoteID" /></Columns><HeaderStyle CssClass="grid_head"></HeaderStyle></asp:GridView>
                </div>
                <asp:SqlDataSource ID="SqlInstance" SelectCommand="SELECT [NodeName], [Operator], [Sing], [AuditDate], [NoteID], [ID], [NodeID], [AuditResult], [TheOrder], [OutOfTime], [IsInsertedFrontNode], [During], [ArriveTime], [IsAllPass], [IsSendMsg], [AuditInfo],(case when sing = -1 then '未到达' when sing=0 then '到达未审核' when sing='1' then '已审核' when sing='2' then '未处理已通过' else '已超时' end) as Status FROM [WF_Instance] WHERE ([ID] = @ID)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="ID" QueryStringField="id" Type="Int32"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
