<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectList.aspx.cs" Inherits="CommonWindow_PopedomSetup_ProjectList" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>小区名称</title></head>
<body class="body_clear" scroll="no">
    <form id="form1" runat="server">
    <div>
    <table style="height:100%;width:100%" cellpadding="0" cellspacing="0" border="1" id="tablex" class="table-normal">            
             <tr>               
                <td class="td-title">小区名称
                    </td>
            </tr>
            <tr>
                <td valign="top">
                <div id="dvDeptBox" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
                    <asp:GridView ID="GVProjectList" CssClass="grid" Width="100%" AutoGenerateColumns="false" DataSourceID="SqlProjectList" PageSize="6" OnRowDataBound="GVProjectList_RowDataBound" DataKeyNames="ProjectId" runat="server"><RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateField>
<ItemTemplate>
                                    <asp:CheckBox ID="CBSelect" runat="server" />
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号"></asp:TemplateField><asp:BoundField DataField="ProjectName" HeaderText="项目名称" SortExpression="ProjectName" /></Columns></asp:GridView>
                    <asp:SqlDataSource ID="SqlProjectList" SelectCommand="SELECT [ProjectId], [Guid], [ProjectCode], [SystemCode], [ProjectName], [AuditState], [State], [ParentProjectId], [IsSubProject], [ParameterClass], [ProjectManager], [Comment], [RecordDate], [UserCode], [RecordPhase] FROM [PM_projects] WHERE (([IsSubProject] = @IsSubProject) AND ([State] = @State) AND ([RecordPhase] > @RecordPhase) AND ([AuditState] = @AuditState))" ConnectionString="Convert.ToString(ConnectionStringsExpressionBuilder.GetConnectionString("Sql"), CultureInfo.CurrentCulture)" runat="server"><SelectParameters><asp:Parameter DefaultValue="0" Name="IsSubProject" Type="TypeCode.Byte" /><asp:Parameter DefaultValue="1" Name="State" Type="TypeCode.Byte" /><asp:Parameter DefaultValue="1" Name="RecordPhase" Type="TypeCode.Byte" /><asp:Parameter DefaultValue="1" Name="AuditState" Type="TypeCode.Int32" /></SelectParameters></asp:SqlDataSource>
                </div>                
                </td>
            </tr>
            	<tr>
			<td  align="center" colspan="2" class="td-submit">
					<asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" />
               
                
            </td>
		</tr>
      </table>
    </div>
    </form>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
</body>
</html>
