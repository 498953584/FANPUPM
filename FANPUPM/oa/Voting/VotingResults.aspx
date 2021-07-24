<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VotingResults.aspx.cs" Inherits="oa_Voting_VotingResults" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"><title>投票管理</title>
    
    <script language="javascript" type="text/javascript">
    <!--
    function ClickRow(RecordCode,RecordID)
	{
	        
		    document.getElementById('btnEdit').disabled = false;
		    document.getElementById('btnDel').disabled = false;
            document.getElementById('HdnRecordCode').value = RecordCode;
		    document.getElementById('HdnRecordID').value=RecordID;
	}
	function OpenWin(op)
	{
	    var url="";
	    var RecordCode = document.getElementById('HdnRecordCode').value;
	    var RecordID =document.getElementById('HdnRecordID').value; 
	    var Rid=document.getElementById('Hdnrid').value;
        if(op=='add')
        {
            url="VotingOptionEdit.aspx?Op=add&ac="+Rid+"&rid=0";
        }
        if(op=='edit')
        {
            url="VotingOptionEdit.aspx?Op=edit&ac="+RecordCode+"&rid="+RecordID;
        }
     
        return window.showModalDialog(url,window,'resizable:yes;dialogHeight:200px;dialogWidth:400px;center:1;help:0;status:0;');
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
                <td height="20px" class="td-toolsbar">
            
                    <asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />
                    <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
                    <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
                     <input id="Hdnrid" type="hidden" style="width:1px" runat="server" />

                    <input id="HdnRecordCode" type="hidden" style="width:20px" runat="server" />

                    <input id="HdnRecordID" type="hidden" style="width:1px" runat="server" />

                 <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
                     </td>
                    
            </tr>
            <tr>
                <td valign="top" height="90%">
                    <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
               
                    <asp:GridView CssClass="grid" ID="GVVoting" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" BorderWidth="0px" CellPadding="0" OnRowDataBound="GVVoting_RowDataBound" OnPageIndexChanging="GVVoting_PageIndexChanging" runat="server"><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField HeaderText="序号" /><asp:BoundField HeaderText="选\u3000项" DataField="Options" /><asp:BoundField HeaderText="投票数" HtmlEncode="false" DataField="Poll" /><asp:BoundField DataField="ppercent" HeaderText="百分比" HtmlEncode="false" /></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                    
                         </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnEdit" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnDel" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="__Page" EventName="Load" runat="server" /></Triggers></asp:UpdatePanel>
                     
                    </div>
                </td>
            </tr>
            
        </table>
    </form>
</body>
</html>
