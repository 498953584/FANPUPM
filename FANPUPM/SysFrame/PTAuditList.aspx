<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PTAuditList.aspx.cs" Inherits="SysFrame_PTAuditList" %>



<html >
<head runat="server"><title>待办工作</title></head>
<body class="body_clear" scroll="no">
    <form id="form1" runat="server">
    <div>
     <table id="Table1" cellspacing="0" cellpadding="0" style="WIDTH:100%;HEIGHT:100%" align="center" border="0" class="table-normal">
            <tr>               
                <td class="td-title">
                    待审流程</td>
            </tr>
            <tr>
                <td>
                     <div id="dvDeptBox" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
                                      <!--代办-->
                                <asp:GridView ID="gvAuditingList" AutoGenerateColumns="false" DataSourceID="SqlAudit" CssClass="grid" AllowPaging="true" PageSize="20" OnRowDataBound="gvAuditingList_RowDataBound" DataKeyNames="NoteID" runat="server">
<EmptyDataTemplate>
                                    </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head" Font-Bold="false"></HeaderStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                        </ItemTemplate>
</asp:TemplateField><asp:ButtonField HeaderText="代办审核名称" DataTextField="BusinessClassName" /><asp:BoundField DataField="ArriveTime" HeaderText="提交审核时间" /></Columns><PagerStyle HorizontalAlign="Right"></PagerStyle></asp:GridView>

                                <asp:SqlDataSource ID="SqlAudit" SelectCommand="SELECT  a.BusinessCode, DATEDIFF(hh, b.OutOfTime, GETDATE()) AS cs,a.BusinessClass,CONVERT (varchar(10), a.StartTime, 20) as rq, (SELECT BusinessClassName FROM WF_Business_Class AS d WHERE (BusinessCode = a.BusinessCode) AND (BusinessClass = a.BusinessClass)) AS BusinessClassName, b.NoteID, b.IsAllPass, a.TemplateID, (SELECT TemplateName FROM WF_Template AS c WHERE (TemplateID = a.TemplateID)) AS TemplateName, b.NodeID, b.NodeName, (SELECT v_xm FROM PT_yhmc WHERE (v_yhdm = a.Organiger)) AS organigerName, a.StartTime, a.InstanceCode, dbo.GetBusinessName(a.BusinessCode) AS BusinessName ,b.ArriveTime ,b.During FROM WF_Instance_Main AS a INNER JOIN WF_Instance AS b ON a.ID = b.ID WHERE ((b.Sing = 0) AND (b.Operator = @operator)) OR ((b.Sing = 0) AND (dbo.GetCommissionMan(a.TemplateID, b.Operator) = @operator)) order by a.StartTime desc" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:SessionParameter Name="operator" SessionField="yhdm"></asp:SessionParameter></SelectParameters></asp:SqlDataSource>
                     </div>
                </td>
            </tr>
    </table>
    </div>
    </form>
</body>
</html>
