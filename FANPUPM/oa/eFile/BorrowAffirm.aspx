<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BorrowAffirm.aspx.cs" Inherits="oa_eFile_BorrowAffirm" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>无标题页</title>
<script language="javascript" type="text/javascript">
    window.name = "win";
    </script>
</head>
<body scroll="no">
    <form id="form1" target="win" runat="server">
           <div>
     <table class="table-normal" cellspacing="0" cellpadding="0" width="100%" border="1">
           <tr>
             <td class="td-title" colspan="2">借阅确认</td>
      </tr>	
             <tr>
	        <td class="td-label">
		        借阅时间
	        </td>
	        <td >
                <JWControl:DateBox ID="DBBorrowDate" runat="server"></JWControl:DateBox></td>
                 </tr>
	        <tr>
                <td class="td-label">
		        计划归时间
	        </td>
	       
	        <td>
                <JWControl:DateBox ID="DBPlanReturn" runat="server"></JWControl:DateBox></td>
	     
	    </tr>
	    <tr>
					<td  align="center" colspan="2" class="td-submit"><asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" />&nbsp;<input id="BtnClose" onclick="window.close();" type="button" value="关 闭"/>&nbsp;
                        </td>
				</tr>
        </table>    
        <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>       
    </div>
    </form>
</body>
</html>
