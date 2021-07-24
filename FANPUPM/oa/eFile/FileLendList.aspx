<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileLendList.aspx.cs" Inherits="oa_eFile_FileLendList" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>借阅办理</title>
     <script language="javascript" type="text/javascript">
    function ClickRow(recordid,ls,sl,as)
    {
        document.getElementById('HdnRecoreId').value = recordid;      
        //document.getElementById('btnEdit').disabled = false;  
       // document.getElementById('btnDel').disabled = false;
       if(sl == "1")
       {
           if(ls=='1')
           {
             document.getElementById('btnBorrow').disabled = false; 
           }
           else
           {
            document.getElementById('btnBorrow').disabled = true;       
           }
           if(ls=='2')
           {
             document.getElementById('btnReturn').disabled = false; 
           }
           else
            {
             document.getElementById('btnReturn').disabled = true; 
           }
       }
       else if(sl == "2")
       {
            if(ls=='1' && as=='1')
           {
             document.getElementById('btnBorrow').disabled = false; 
           }
           else
           {
            document.getElementById('btnBorrow').disabled = true;       
           }
           if(ls=='2' && as =='1')
           {
             document.getElementById('btnReturn').disabled = false; 
           }
           else
            {
             document.getElementById('btnReturn').disabled = true; 
           }       
       }
    } 
     function OpenReturn()
    {
      var rid =     document.getElementById('HdnRecoreId').value ;
      var url = "BorrowAffirm.aspx?rid="+rid;
      return window.showModalDialog(url,window,"dialogHeight:260px;dialogWidth:350px;center:1;help:0;status:0;");		    
    }     
    </script>
</head>
<body class="body_clear">
    <form id="form1" runat="server">
    <div><table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0" class="table-normal">
            <tr>               
                <td class="td-title">
                    借阅申请记录列表</td>
            </tr>
                <tr class="td-search">
                <td>
                    按<asp:DropDownList ID="DropDownList1" runat="server"><asp:ListItem Value="1" Text="档案名称" /><asp:ListItem Value="2" Text="借阅人" /></asp:DropDownList>
                    &nbsp;<asp:TextBox ID="TxtSearchCount" runat="server"></asp:TextBox>
                    &nbsp;<asp:Button ID="BtnSearch" OnClick="BtnSearch_Click" runat="server" /></td>
            </tr>
            <tr>
                <td class="td-toolsbar" style="height: 24px">             
                    <asp:Button ID="btnBorrow" Text="借阅确认" Enabled="false" OnClick="btnBorrow_Click" runat="server" />&nbsp;
                    <asp:Button ID="btnReturn" Text="强制归还" Enabled="false" OnClick="btnReturn_Click" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                   
                    <asp:HiddenField ID="HdnRecoreId" runat="server" />
                    <asp:HiddenField ID="HdnPrjGuid" Value="00000000-0000-0000-0000-000000000000" runat="server" />
                    </td>
            </tr>
            <tr>
                <td >
                 <div id="dvDeptBox" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
                    <asp:GridView ID="GVeFileLend" AllowPaging="true" AutoGenerateColumns="false" DataSourceID="SqleFileLend" CssClass="grid" Width="100%" PageSize="20" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
                            <table class="grid" cellspacing="0" rules="all" border="1" id="GridView1" style="border-collapse:collapse;">
		                        <tr class="grid_head">
			                       <th scope="col">档案名称</th><th scope="col">密级</th><th scope="col">借阅人</th><th scope="col" style="width:70px;">借阅时间</th><th scope="col" style="width:90px;">计划归还日期</th><th scope="col">借阅状态</th><th scope="col">归还类型</th><th scope="col">备注</th>
		                        </tr>
	                        </table>
                        </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField DataField="FileTitle" HeaderText="档案名称" SortExpression="FileTitle" /><asp:BoundField DataField="SecretLevel" HeaderText="密级" SortExpression="SecretLevel" /><asp:BoundField DataField="v_xm" HeaderText="借阅人" SortExpression="BorrowMan" /><asp:BoundField DataField="LendDate" HeaderText="借阅时间" SortExpression="ReturnApplyDate" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:BoundField DataField="PlanReturnDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="计划归还日期" HtmlEncode="false" SortExpression="PlanReturnDate" /><asp:BoundField DataField="LendState" HeaderText="借阅状态" SortExpression="LendState" /><asp:BoundField HeaderText="归还类型" ReadOnly="true" SortExpression="RecordType" DataField="ReturnType" /><asp:BoundField DataField="Remark" HeaderText="备注" SortExpression="Remark" /></Columns><PagerStyle HorizontalAlign="Right"></PagerStyle></asp:GridView>
                     <asp:SqlDataSource ID="SqleFileLend" SelectCommand="SELECT [AuditState], [BorrowMan], [FileRecordID], [LendDate], [RecordID], [LendState], [ReturnApplyDate], [ReturnDate], [PlanReturnDate], [RecordType], [CorpCode], [PrjGuid], [FileTitle], [SubmitMan], [UserCode], [FileType], [FileCopy], [ClassID], [FileCode], [Remark], [SubmitDate], [SecretLevel], [SaveLimit], [RecordDate], [OriginalName], [FilePath],[ReturnType],[LendNumber] ,[v_xm] FROM [v_OA_eFileLend] WHERE 1=1" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:SessionParameter Name="BorrowMan" SessionField="yhdm" Type="String"></asp:SessionParameter></SelectParameters></asp:SqlDataSource>
                    </div>                                
                </td>
            </tr>
        </table><JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
        </div>
        </form>
        </body>
        </html>
