<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VotingInfoList.aspx.cs" Inherits="oa_Voting_VotingInfoList" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"><title>投票管理</title>
   
    <script language="javascript" type="text/javascript">
    <!--
    function ClickRow(RecordCode,AuditState)
	{
	        if(AuditState=='0')
	        {  
		    document.getElementById('btnAdd').disabled = true;
		    
		    
		    }
		    if(AuditState=='1')
		    {
		        document.getElementById('btnAdd').disabled=false;
		        
		    }
		
		
		document.getElementById('HdnRecordCode').value = RecordCode;
		
		
		
	}
	function OpenWin(op)
	{
	    var url="";
	    var RecordCode = document.getElementById('HdnRecordCode').value;
        if(op=='add')
        {
            url="VotingOnLine.aspx?Op=add&ac="+RecordCode;
        }
        
        return window.showModalDialog(url,window,'resizable:yes;dialogHeight:300px;dialogWidth:400px;center:1;help:0;status:0;');
     }
     -->
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
     <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
        
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">
            <tr>
                <td height="20px" class="td-title">投票项目</td>
            </tr>
            <tr>
                <td height="20px" class="td-toolsbar">
                    <asp:Button ID="btnAdd" Text="投\u3000票" Enabled="false" OnClick="btnAdd_Click" runat="server" />
                    <input id="HdnRecordCode" type="hidden" style="width:20px" runat="server" />

                    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl></td>
            </tr>
            <tr>
                <td valign="top" height="90%">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                
                    <asp:GridView CssClass="grid" ID="GVVoting" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" BorderWidth="0px" CellPadding="0" OnRowDataBound="GVVoting_RowDataBound" OnPageIndexChanging="GVVoting_PageIndexChanging" runat="server"><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField HeaderText="序号" /><asp:BoundField HeaderText="项目名称" DataField="Title" /><asp:BoundField HeaderText="范围" HtmlEncode="false" /><asp:BoundField DataField="Content" HeaderText="备注" HtmlEncode="false" /><asp:BoundField HeaderText="状态" DataField="State" HtmlEncode="false" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                        &nbsp;
                       </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" runat="server" /></Triggers></asp:UpdatePanel>
                    </div>
                </td>
            </tr>
            
        </table>
    </form>
</body>
</html>
