<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BorroweFileSelectMain.aspx.cs" Inherits="oa_eFile_BorroweFileSelectMain" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<script language="javascript" src="/Web_Client/WebUIDateBox.js" type="text/javascript"></script>
<head runat="server"><title>选择档案</title>
   
    <script type="text/javascript" language="javascript">
    <!--
   // window.name = "win";
    
   --> 
    </script> 
</head>
<body class="body_clear">  
    <form id="formx" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
       <table style="height:100%;width:100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">            
            <tr>
                
                <td style="height:50%;">
                
                    <table border="0" style="height:100%;width:100%" cellpadding="0" cellspacing="0"class="table-normal">
                        <tr><td class="td-title" colspan="2" style="height: 22px">
                            <asp:Label ID="LbTilteName" runat="server"></asp:Label>
                            
                        </td></tr>
                        <tr><td>
                            <div id="dvDeptBox" class="div-scroll" style="width: 100%; height: 100%">
                                <asp:GridView ID="GVeFileInfo" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="sqlBulletin" PageSize="8" Width="100%" OnRowDataBound="GVeFileInfo_RowDataBound" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
                                        <table id="GridView1" border="1" cellspacing="0" class="grid" rules="all" style="border-collapse: collapse;">
                                            <tr class="grid_head">
                                                <th style="width: 30px;">  </th>
                                                <th scope="col" style="width: 70px;">
                                                    档案编号
                                                </th>
                                                <th scope="col" style="width: 70px;">
                                                    文档名称</th>
                                                <th scope="col" style="width: 70px;">
                                                    提交人</th>
                                                <th scope="col" style="width: 120px;">
                                                    提交时间</th>
                                                <th scope="col" style="width: 70px">
                                                    归档时间</th>
                                                <th scope="col" style="width: 70px">
                                                    保存期限</th>
                                                <th scope="col" style="width: 70px">
                                                    密级</th>
                                                <th scope="col" style="width: 120px">
                                                    备注</th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
<Columns><asp:TemplateField><ItemTemplate>
                                                <asp:CheckBox ID="CBeFile" runat="server" />
                                            </ItemTemplate></asp:TemplateField><asp:BoundField DataField="FileCode" HeaderText="档案编号" SortExpression="FileCode" /><asp:BoundField DataField="FileTitle" HeaderText="文档名称" SortExpression="FileTitle" /><asp:BoundField DataField="SubmitMan" HeaderText="提交人" SortExpression="SubmitMan" /><asp:BoundField DataField="RecordDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="提交时间" HtmlEncode="false" SortExpression="RecordDate" /><asp:BoundField DataField="SubmitDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="归档时间" HtmlEncode="false" SortExpression="SubmitDate" /><asp:BoundField DataField="SaveLimit" HeaderText="保存期限" SortExpression="SaveLimit" /><asp:BoundField DataField="SecretLevel" HeaderText="密级" SortExpression="SecretLevel" /><asp:BoundField DataField="Remark" HeaderText="备注" SortExpression="Remark" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Right"></PagerStyle><HeaderStyle CssClass="grid_head"></HeaderStyle></asp:GridView>                              
                            </div>
                            <asp:SqlDataSource ID="sqlBulletin" SelectCommand="SELECT RecordID, RecordType, CorpCode, FileTitle, SubmitMan, RecordDate, UserCode, ClassID, FileCode, Remark, SubmitDate, SaveLimit, SecretLevel, FileType, FileCopy, FilePath, OriginalName, PrjGuid FROM OA_eFile_Info WHERE (ClassID IS NOT NULL) and (SecretLevel<>3) and ClassID = @ClassID" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="ClassID" QueryStringField="cd"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
                        
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
                            <asp:Button ID="BtnConfirm" Text="确  定" OnClick="BtnConfirm_Click" runat="server" />
                            &nbsp;
                            <asp:Button ID="BtnDel" Text="删  除" OnClick="BtnDel_Click" runat="server" /></td></tr>
                        <tr><td>
                         <div id="Div1" class="div-scroll" style="width: 100%; height: 100%">
                            <asp:UpdatePanel ID="UpdatePanel2" Mode="Conditional" runat="server">
<ContentTemplate>
                                <asp:GridView ID="GVSelecteFile" AutoGenerateColumns="false" CssClass="grid" PageSize="1" Width="100%" OnRowDataBound="GVSelecteFile_RowDataBound" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
                                        <table id="Table1" border="1" cellspacing="0" class="grid" rules="all" style="border-collapse: collapse">
                                            <tr class="grid_head">
                                             <th style="width: 30px;">  </th>
                                                <th scope="col" style="width: 70px">
                                                    档案编号
                                                </th>
                                                <th scope="col" style="width: 70px">
                                                    文档名称</th>
                                                <th scope="col" style="width: 70px">
                                                    提交人</th>
                                                <th scope="col" style="width: 70px">
                                                    提交时间</th>
                                                <th scope="col" style="width: 70px">
                                                    归档时间</th>
                                                <th scope="col" style="width: 70px">
                                                    保存期限</th>
                                                <th scope="col" style="width: 70px">
                                                    密级</th>
                                                <th scope="col" style="width: 120px">
                                                    计划归还时间</th>
                                                <th scope="col" style="width: 120px">
                                                    备注</th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
<Columns><asp:TemplateField><ItemTemplate>
                                                <asp:CheckBox ID="CBRecordID" Checked="true" runat="server" />
                                            </ItemTemplate></asp:TemplateField><asp:BoundField DataField="FileCode" HeaderText="文档编号" /><asp:BoundField DataField="FileTitle" HeaderText="文档名称" /><asp:BoundField DataField="SubmitMan" HeaderText="提交人" /><asp:BoundField DataField="SubmitDate" HeaderText="提交时间" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:BoundField DataField="RecordDate" HeaderText="归档时间" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:BoundField DataField="SaveLimit" HeaderText="保存期限" /><asp:BoundField DataField="SecretLevel" HeaderText="密级" /><asp:TemplateField HeaderText="计划归还时间"><ItemTemplate>
                                                <JWControl:DateBox ID="DBPlanReturnDate" Width="70px" runat="server"></JWControl:DateBox>
                                                
                                            </ItemTemplate></asp:TemplateField><asp:BoundField DataField="Remark" HeaderText="备 注" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Right"></PagerStyle><HeaderStyle CssClass="grid_head"></HeaderStyle></asp:GridView>
                                 </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="BtnSelAdd" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="BtnDel" EventName="Click" runat="server" /></Triggers></asp:UpdatePanel>
                            </div>
                     </td></tr>
                    </table>
                </td>
           </tr>
        </table>           
        <asp:SqlDataSource ID="SqlProjectList" SelectCommand="SELECT [ProjectId], [Guid], [ProjectCode], [SystemCode], [ProjectName], [AuditState], [State], [ParentProjectId], [IsSubProject], [ParameterClass], [ProjectManager], [Comment], [RecordDate], [UserCode], [RecordPhase] FROM [PM_projects] WHERE (([IsSubProject] = @IsSubProject) AND ([State] = @State) AND ([RecordPhase] > @RecordPhase) AND ([AuditState] = @AuditState))" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:Parameter DefaultValue="0" Name="IsSubProject" Type="Byte" /><asp:Parameter DefaultValue="1" Name="State" Type="Byte" /><asp:Parameter DefaultValue="1" Name="RecordPhase" Type="Byte" /><asp:Parameter DefaultValue="1" Name="AuditState" Type="Int32" /></SelectParameters></asp:SqlDataSource>
    </form>
</body>
</html>
