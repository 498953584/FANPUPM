<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WFOvertimeCount.aspx.cs" Inherits="ModuleSet_Workflow_WFOvertimeCount" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>流程超时统计</title>
       <script language="javascript" type="text/javascript">
    <!--
    function ClickRow(temp)
    {        
        document.getElementById('frmDetail').src = "WFOvertimeCountDetails.aspx?id="+temp;     
    }
    -->    
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="form1" runat="server">
    <div>
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" style="height:100%"  align="center" border="0" class="table-normal">
         <tr>               
            <td class="td-title">流程超时统计</td>
        </tr>
             <tr>
           <td class="td-toolsbar" style="height: 24px">查询：<asp:DropDownList ID="DDLCorpCode" DataSourceID="SqlCorpCode" DataTextField="CorpName" DataValueField="CorpCode" runat="server"></asp:DropDownList><asp:SqlDataSource ID="SqlCorpCode" SelectCommand="SELECT [CorpCode], [CorpName] FROM [PT_d_CorpCode] " ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
               <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" /></td>
        </tr>
        
           <tr style="height:50%" valign="top">
            <td>
                <asp:GridView ID="GVInstance" AllowPaging="true" AutoGenerateColumns="false" PageSize="12" CssClass="grid" DataSourceID="SqlInstance" Width="100%" OnRowDataBound="GVInstance_RowDataBound" DataKeyNames="ID" runat="server">
<EmptyDataTemplate>
                        <table id="Table2" border="1" cellspacing="0" class="grid" rules="all" style="width: 100%;
                            border-collapse: collapse">
                            <tr class="grid_head">
                                <th scope="col">
                                    流程名称</th>
                                <th scope="col">
                                    使用模板</th>
                                <th align="center" scope="col" style="width: 70px">
                                    发起人</th>
                                <th scope="col">
                                    最终审核人</th>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><Columns><asp:BoundField DataField="BusinessClassName" HeaderText="流程名称" SortExpression="BusinessCode" /><asp:BoundField DataField="TemplateName" HeaderText="使用模板" SortExpression="TemplateID" /><asp:BoundField DataField="Organiger" HeaderText="发起人" SortExpression="Organiger" /><asp:BoundField HeaderText="最终审核人" SortExpression="InstanceCode" /></Columns><HeaderStyle CssClass="grid_head"></HeaderStyle></asp:GridView>
                <asp:SqlDataSource ID="SqlInstance" SelectCommand="select a.*,b.* , 
(SELECT TemplateName FROM WF_Template WHERE (TemplateID = a.TemplateID)) AS TemplateName 
from WF_Instance_Main a inner join WF_Business_Class  b on  a.BusinessCode = b.BusinessCode
and a.BusinessClass = b.BusinessClass LEFT join WF_Template c ON c.[TemplateID] = a.[TemplateID]" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
           
            </td>
        </tr>
        <tr style="height:50%">
            <td>
            <iframe id="frmDetail" name="frmDetail" frameborder="0" width="100%" height="100%" src="WFOvertimeCountDetails.aspx" runat="server"></iframe>
            </td>
        </tr>
            </table>
    </div>
    </form>
</body>
</html>
