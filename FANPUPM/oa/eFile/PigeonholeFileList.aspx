<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PigeonholeFileList.aspx.cs" Inherits="oa_eFile_PigeonholeFileList" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>管理分类归档</title>
    <script language="javascript" type="text/javascript">
    <!--
    function ClickRow(recordid)
    {        
        document.getElementById('btnPigeonhole').disabled = false;
        document.getElementById('HdnRecoreId').value = recordid;
    }
    function openEdit()
    {  
        var rid =     document.getElementById('HdnRecoreId').value ; 
    	var url = 'PigeonholeFileEdit.aspx?rid='+rid;
		return window.showModalDialog(url,window,"dialogHeight:360px;dialogWidth:650px;center:1;help:0;status:0;");
    }   
    -->    
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="form1" runat="server">
    <div><table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0" class="table-normal">
            <tr>               
                <td class="td-title">
                    有归档记录清单</td>
            </tr>           
            <tr>
                <td class="td-toolsbar" style="height: 24px">             
                    <asp:Button ID="btnPigeonhole" Text="归档处理" Enabled="false" OnClick="btnPigeonhole_Click" runat="server" />&nbsp;&nbsp;
                   
                    <asp:HiddenField ID="HdnRecoreId" runat="server" />
                    <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
                    &nbsp;&nbsp;
                    </td>
            </tr>
            <tr>
                <td >
                 <div id="dvDeptBox" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                    <asp:GridView ID="GridView1" AllowPaging="true" AutoGenerateColumns="false" DataSourceID="sqlBulletin" CssClass="grid" Width="100%" PageSize="22" OnRowDataBound="GridView1_RowDataBound" runat="server">
<EmptyDataTemplate>
                            <table class="grid" cellspacing="0" rules="all" border="1" id="GridView1" style="border-collapse:collapse;">
		                        <tr class="grid_head">
			                        <th scope="col" style="width:60px;">  
                                        序 号                          
                                    </th><th scope="col" style="width:70px;">
                                        文档名称</th >
                                        <th scope="col" style="width:170px;">
                                        提交人</th>
                                        <th scope="col" style="width:120px;">
                                        提交时间</th>
                                    <th scope="col" style="width: 120px">
                                        备注</th>
		                        </tr>
	                        </table>
                        </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField HeaderText="序 号" /><asp:BoundField DataField="FileTitle" HeaderText="文档名称" SortExpression="FileTitle" /><asp:BoundField DataField="SubmitMan" HeaderText="提交人" SortExpression="SubmitMan" /><asp:BoundField DataField="RecordDate" HeaderText="提交时间" SortExpression="RecordDate" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:BoundField DataField="Remark" HeaderText="备注" SortExpression="Remark" /></Columns><PagerStyle HorizontalAlign="Right"></PagerStyle></asp:GridView>
                         </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="btnPigeonhole" EventName="Click" runat="server" /></Triggers></asp:UpdatePanel>
                    </div> <asp:SqlDataSource ID="sqlBulletin" SelectCommand="SELECT [RecordID], [RecordType], [CorpCode], [FileTitle], [SubmitMan], [RecordDate], [UserCode], [ClassID], [FileCode], [Remark], [SubmitDate], [SaveLimit], [SecretLevel], [FileType], [FileCopy], [FilePath], [OriginalName] ,[PrjGuid] FROM [OA_eFile_Info] WHERE [ClassID]  IS NULL" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>                   
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
