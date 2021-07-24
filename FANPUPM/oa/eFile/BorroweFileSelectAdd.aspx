<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BorroweFileSelectAdd.aspx.cs" Inherits="oa_eFile_BorroweFileSelectAdd" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>选择档案</title></head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <div>
     <table style="height:100%;width:100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">            
            <tr>
                <td valign="top" style="width:25%" rowspan="2" >  
                    <table border="0" style="height:100%;width:100%">
                        <tr>
                            <td valign="top">
                                <asp:DropDownList ID="DropDownList1" runat="server"><asp:ListItem Value="0" Text="项目名称" /><asp:ListItem Value="1" Text="项目名称" /></asp:DropDownList>
                                <asp:TreeView ID="TVClassList" ShowLines="true" runat="server"></asp:TreeView>
                            </td>
                        </tr>
                    </table>
                </td>
                
                <td style="height:50%;width:75%">
                
                    <table border="0" style="height:100%;width:100%" cellpadding="0" cellspacing="0"class="table-normal">
                        <tr><td class="td-title" colspan="2" style="height: 19px">
                            <asp:Label ID="LbTilteName" runat="server"></asp:Label>                           
                        </td></tr>
                        <tr><td>                            
                            <asp:SqlDataSource ID="SqleFileInfo" SelectCommand="SELECT [RecordID], [RecordType], [CorpCode], [PrjGuid], [FileCode], [Remark], [SubmitDate], [SubmitMan], [FileTitle], [ClassID], [UserCode], [RecordDate], [SaveLimit], [SecretLevel], [OriginalName], [FilePath], [FileCopy], [FileType] FROM [OA_eFile_Info] WHERE ([ClassID] IS NOT NULL)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                                    <asp:GridView ID="GVeFileInfo" AllowPaging="true" AutoGenerateColumns="false" DataSourceID="SqleFileInfo" PageSize="8" Width="100%" DataKeyNames="RecordID" runat="server"><Columns><asp:TemplateField><ItemTemplate>
                                                    <asp:CheckBox ID="CBeFileRecordID" runat="server" />
                                                </ItemTemplate></asp:TemplateField><asp:BoundField DataField="FileCode" HeaderText="档案编号" SortExpression="FileCode" /><asp:BoundField DataField="FileTitle" HeaderText="档案名称" SortExpression="FileTitle" /><asp:BoundField DataField="SubmitDate" HeaderText="SubmitDate" SortExpression="SubmitDate" /><asp:BoundField DataField="SubmitMan" HeaderText="SubmitMan" SortExpression="SubmitMan" /><asp:BoundField DataField="Remark" HeaderText="备 注" SortExpression="Remark" /></Columns></asp:GridView>
                                </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="TVClassList" EventName="SelectedNodeChanged" runat="server" /></Triggers></asp:UpdatePanel>
                          
                        
                     </td></tr>
                     <tr><td class="td-submit" align="center">
                         <asp:Button ID="BtnSelAdd" Text="新 增" OnClick="BtnSelAdd_Click" runat="server" /></td></tr>
                    </table>
                </td>
            </tr>
            
            
            <tr>
                <td style="height:50%;width:75%">
                    <table border="0" style="height:100%;width:100%" cellpadding="0" cellspacing="0"class="table-normal">
                        <tr><td class="td-toolsbar" >
                            <asp:Button ID="BtnConfirm" Text="确  定" runat="server" />
                            &nbsp;
                            <asp:Button ID="BtnDel" Text="删  除" runat="server" /></td></tr>
                        <tr><td>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
<ContentTemplate>
                                    <asp:GridView ID="GVSelecteFile" AutoGenerateColumns="false" DataSourceID="SqlSelectFile" DataKeyNames="RecordID" runat="server"><Columns><asp:BoundField DataField="FileCode" HeaderText="文件编号" SortExpression="FileCode" /><asp:BoundField DataField="FileTitle" HeaderText="文件名称" SortExpression="FileTitle" /><asp:TemplateField><ItemTemplate>
                                                    <JWControl:DateBox ID="DateBox1" runat="server"></JWControl:DateBox>
                                                </ItemTemplate></asp:TemplateField></Columns></asp:GridView>
                                    <asp:SqlDataSource ID="SqlSelectFile" SelectCommand="SELECT * FROM [OA_eFile_Info]" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
                                </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="BtnSelAdd" EventName="Click" runat="server" /></Triggers></asp:UpdatePanel>
                         
                     </td></tr>
                    </table>
                </td>
           </tr>
        </table>           
    </div>
    </form>
</body>
</html>
