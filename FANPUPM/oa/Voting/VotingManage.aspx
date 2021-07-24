<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VotingManage.aspx.cs" Inherits="oa_Voting_VotingManage" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"><title>投票管理</title>
    
    <script language="javascript" type="text/javascript">
    <!--
    function ClickRow(RecordCode,AuditState)
	{
	        if(AuditState=='0')
	        {  
		    document.getElementById('btnEdit').disabled = false;
		    document.getElementById('btnDel').disabled = false;
		    document.getElementById('btnStart').disabled = false;
		    document.getElementById('btnStop').disabled= true;
		    
		    }
		    if(AuditState=='2')
	        {  
		    document.getElementById('btnEdit').disabled = false;
		    document.getElementById('btnDel').disabled = false;
		    document.getElementById('btnStart').disabled = false;
		    document.getElementById('btnStop').disabled= true;
		    
		    }
		    if(AuditState=='1')
		    {
		        document.getElementById('btnEdit').disabled=true;
		        document.getElementById('btnDel').disabled=true;
		        document.getElementById('btnStart').disabled=true;
		        document.getElementById('btnStop').disabled=false;
		    }
		
		
		document.getElementById('HdnRecordCode').value = RecordCode;
		if(RecordCode<=0)
		{
		    frmVotingResults.location.href = "VotingResults.aspx?&ac=0";
		}
		if(RecordCode>0)
		{
		frmVotingResults.location.href = "VotingResults.aspx?&ac="+RecordCode;
		}
		
	}
	function OpenWin(op)
	{
	    var url="";
	    var RecordCode = document.getElementById('HdnRecordCode').value;
        if(op=='add')
        {
            url="VotingManageEdit.aspx?Op=add&ac=0";
        }
        if(op=='edit')
        {
            url="VotingManageEdit.aspx?Op=edit&ac="+RecordCode;
        }
       
        return window.showModalDialog(url,window,'resizable:yes;dialogHeight:300px;dialogWidth:400px;center:1;help:0;status:0;');
     }
     -->
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
        &nbsp;&nbsp;
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">
            <tr>
                <td height="20px" class="td-title">投票项目管理</td>
            </tr>
            <tr>
                <td height="20px" class="td-toolsbar">
                    <asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />
                    <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
                    <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
                    <asp:Button ID="btnStart" Text="启动" Enabled="false" OnClick="btnStart_Click" runat="server" />
                    <asp:Button ID="btnStop" Text="关闭" Enabled="false" OnClick="btnStop_Click" runat="server" />
                    <input id="HdnRecordCode" type="hidden" style="width:20px" runat="server" />

                    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl></td>
            </tr>
            <tr>
                <td valign="top" height="50%">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                    <asp:GridView CssClass="grid" ID="GVVoting" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" BorderWidth="0px" CellPadding="0" OnRowDataBound="GVVoting_RowDataBound" OnPageIndexChanging="GVVoting_PageIndexChanging" runat="server"><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField HeaderText="序号" /><asp:BoundField HeaderText="项目名称" DataField="Title" /><asp:BoundField HeaderText="范围" HtmlEncode="false" /><asp:BoundField DataField="Content" HeaderText="备注" HtmlEncode="false" /><asp:BoundField HeaderText="状态" DataField="State" HtmlEncode="false" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                        &nbsp;
                         </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnEdit" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnDel" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnStart" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnStop" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="__Page" EventName="Load" runat="server" /></Triggers></asp:UpdatePanel>
                    </div>
                </td>
            </tr>
            <tr>
                <td valign="top" height="50%">
                    <iframe id="frmVotingResults" frameborder="0" width="100%" height="100%" src="VotingResults.aspx?&ac=0" runat="server"></iframe>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
