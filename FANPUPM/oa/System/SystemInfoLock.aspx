<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemInfoLock.aspx.cs" Inherits="oa_System_SystemInfoLock" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>制度管理</title>
     <script type="text/javascript" language="javascript">
    <!--
   window.name = "win";
    function UpFile(t)
	{
	    //t=3 制度管理
	    var RecordCode = document.getElementById('hdnRecordId').value;//编号	    
		var url = "../../CommonWindow/Annex/AnnexList.aspx?mid="+t+"&rc="+RecordCode+"&at=0&type=1";			
		window.showModalDialog(url,window,'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
	}
       -->
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="form1" target="win" runat="server">
    <div>
     <table class="table-normal" cellspacing="0" cellpadding="0" border="1" width="100%">
      <tr>
			<td class="td-head" colSpan="2" height="20">
                <asp:Label ID="LbTitle" Text="Label" runat="server">制度查看</asp:Label>
                <asp:HiddenField ID="hdnRecordId" runat="server" />
            </td>
		</tr> 
		
		<tr>
			<td class="td-label" style="height: 21px">
                制度名称</td>
            <td style="height: 21px">
                <asp:Label ID="LbSystemName" CssClass="text-input" runat="server"></asp:Label>
            </td>
		</tr>
		<tr>
			<td class="td-label">
                制定日期</td>
            <td>
                <asp:Label ID="LbSignDate" runat="server"></asp:Label></td>
		</tr>
         <tr>
             <td class="td-label">
                 制定人</td>
             <td>
                 <asp:Label ID="LbSignMan" runat="server"></asp:Label></td>
         </tr>
		<tr>
			<td align="right" class="td-label">附件：</td>
			<td >
			<input id="BtnAnnex" class="button-normal" style="WIDTH: 100px" onclick="UpFile(3)" type="button" value="查看附件..." runat="server" />

			</td>
		</tr>
		
		<tr>
			<td class="td-label">
                情况说明</td>
            <td>
                <asp:Label ID="LbRemark" runat="server"></asp:Label></td>
		</tr> 
	
      </table>
    </div>
    </form>
</body>
</html>
