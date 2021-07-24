<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileClassTree.aspx.cs" Inherits="oa_eFile_FileClassTree" %>
<%@ Register TagPrefix="MyUserControl" TagName="oa_booksmanage_ucbooksort_ascx" Src="~/oa/BooksManage/UCBookSort.ascx" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>图书分类</title></head>
<body class="body_clear" scroll="no">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <div>
         <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">
            <tr>
                <td valign="top" style="height:24px"> 
                    <asp:DropDownList ID="DDLPrjCode" Width="90%" DataSourceID="SqlProjectList" DataTextField="ProjectName" DataValueField="Guid" AutoPostBack="true" OnSelectedIndexChanged="DDLPrjCode_SelectedIndexChanged" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td valign="top">
                <div id="dvDeptBox" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                        <div id="Div1" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
                    <asp:TreeView ID="TreeView1" ShowLines="true" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" runat="server"><SelectedNodeStyle ForeColor="Red" /></asp:TreeView>
                           </div>
                        </ContentTemplate>
</asp:UpdatePanel>
                    
                    </div>
                </td>
            </tr>
        </table>      
    </div>
        <asp:SqlDataSource ID="SqlProjectList" SelectCommand="SELECT [ProjectId], [Guid], [ProjectCode], [SystemCode], [ProjectName], [AuditState], [State], [ParentProjectId], [IsSubProject], [ParameterClass], [ProjectManager], [Comment], [RecordDate], [UserCode], [RecordPhase] FROM [PM_projects] WHERE (([IsSubProject] = @IsSubProject) AND ([State] = @State) AND ([RecordPhase] > @RecordPhase) AND ([AuditState] = @AuditState))" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:Parameter DefaultValue="0" Name="IsSubProject" Type="Byte" /><asp:Parameter DefaultValue="1" Name="State" Type="Byte" /><asp:Parameter DefaultValue="1" Name="RecordPhase" Type="Byte" /><asp:Parameter DefaultValue="1" Name="AuditState" Type="Int32" /></SelectParameters></asp:SqlDataSource>
    </form>
</body>
</html>
