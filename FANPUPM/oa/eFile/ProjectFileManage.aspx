<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectFileManage.aspx.cs" Inherits="oa_eFile_ProjectFileManage" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>项目档案管理</title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self"/>
    <script language="javascript" type="text/javascript">
    function ClickRow(recordid)
    {
        document.getElementById('HdnRecoreId').value = recordid;      
        document.getElementById('btnEdit').disabled = false;  
        document.getElementById('btnDel').disabled = false;
       
       
        
    }
    function openEdit(t,classid,recordtype,sl)
    {   
       
        var rid =     document.getElementById('HdnRecoreId').value ;  
        var pj = document.getElementById('HdnPrjGuid').value;                
    	if(t=='Add')
    	{
    	    var url = 'ProjectFileManageEdit.aspx?t='+t+'&cid='+classid+'&rid=0&rt='+recordtype+'&prj='+pj+'&sl='+sl;
    	}
    	else
    	{
    	    var url = 'ProjectFileManageEdit.aspx?t='+t+'&cid='+classid+'&rid='+rid+'&rt='+recordtype+'&prj='+pj+'&sl='+sl;
    	}
		return window.showModalDialog(url,window,"dialogHeight:360px;dialogWidth:650px;center:1;help:0;status:0;");
    }       
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="form1" runat="server">
    <div><table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0" class="table-normal">
            <tr>               
                <td class="td-title">
                    <asp:Label ID="lblTitle" Text="档案列表" runat="server"></asp:Label></td>
            </tr>
                <tr class="td-search">
                <td>
                    按<asp:DropDownList ID="DropDownList1" runat="server"><asp:ListItem Value="1" Text="档案名称" /><asp:ListItem Value="2" Text="档案编号" /><asp:ListItem Value="3" Text="保存期限" /></asp:DropDownList>
                    &nbsp;<asp:TextBox ID="TxtSearchCount" runat="server"></asp:TextBox>
                    &nbsp;<asp:Button ID="BtnSearch" OnClick="BtnSearch_Click" runat="server" /></td>
            </tr>
            <tr>
                <td class="td-toolsbar" style="height: 24px">             
                    <asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />&nbsp;
                    <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />&nbsp;
                    <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />&nbsp;&nbsp;
                   
                    <asp:HiddenField ID="HdnRecoreId" runat="server" />
                    <asp:HiddenField ID="HdnPrjGuid" Value="00000000-0000-0000-0000-000000000000" runat="server" />
                    </td>
            </tr>
            <tr>
                <td >
                 <div id="dvDeptBox" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
                    <asp:GridView ID="GridView1" AllowPaging="true" AutoGenerateColumns="false" DataSourceID="sqlBulletin" CssClass="grid" Width="100%" PageSize="20" OnRowDataBound="GridView1_RowDataBound" runat="server">
<EmptyDataTemplate>
                            <table class="grid" cellspacing="0" rules="all" border="1" id="GridView1" style="border-collapse:collapse;">
		                        <tr class="grid_head">
			                        <th scope="col" style="width:70px;">  
                                        档案编号                          
                                    </th><th scope="col" style="width:70px;">
                                        文档名称</th >
                                        <th scope="col" style="width:170px;">
                                        提交人</th>
                                        <th scope="col" style="width:120px;">
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
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField DataField="FileCode" HeaderText="档案编号" SortExpression="FileCode" /><asp:BoundField DataField="FileTitle" HeaderText="文档名称" SortExpression="FileTitle" /><asp:BoundField DataField="SubmitMan" HeaderText="提交人" SortExpression="SubmitMan" /><asp:BoundField DataField="RecordDate" HeaderText="提交时间" SortExpression="RecordDate" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:BoundField DataField="SubmitDate" HeaderText="归档时间" SortExpression="SubmitDate" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:BoundField DataField="SaveLimit" HeaderText="保存期限" SortExpression="SaveLimit" /><asp:BoundField DataField="SecretLevel" HeaderText="密级" SortExpression="SecretLevel" /><asp:BoundField DataField="Remark" HeaderText="备注" SortExpression="Remark" /></Columns><PagerStyle HorizontalAlign="Right"></PagerStyle></asp:GridView>
                    </div> <asp:SqlDataSource ID="sqlBulletin" SelectCommand="SELECT [RecordID], [RecordType], [CorpCode], [FileTitle], [SubmitMan], [RecordDate], [UserCode], [ClassID], [FileCode], [Remark], [SubmitDate], [SaveLimit], [SecretLevel], [FileType], [FileCopy], [FilePath], [OriginalName] ,[PrjGuid] FROM [OA_eFile_Info] where ([PrjGuid] = @PrjGuid) and [ClassID] = @ClassID and [RecordType] = @RecordType and [ClassID] is not NULL" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="PrjGuid" QueryStringField="prj"></asp:QueryStringParameter><asp:QueryStringParameter Name="ClassID" QueryStringField="cid"></asp:QueryStringParameter><asp:QueryStringParameter Name="RecordType" QueryStringField="rt"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>                   
                </td>
            </tr>
        </table><JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    
    </div>
    </form>
</body>
</html>
