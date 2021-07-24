<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BorroweFileLendAdd.aspx.cs" Inherits="oa_eFile_BorroweFileSelectAdd" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>选择档案</title>
    <script language="javascript" type="text/javascript">
    window.name = "win";
    </script>
</head>
<body scroll="no">
    <form id="form1" target="win" runat="server">
           <div>
     <table class="table-normal" cellspacing="0" cellpadding="0" width="100%" border="1">
           <tr>
             <td class="td-title" colspan="2">档案归还</td>
      </tr>	
             <tr>
	        <td class="td-label">
		        归还时间
	        </td>
	        <td >
                <JWControl:DateBox ID="DBReturnDate" runat="server"></JWControl:DateBox></td>
                 </tr>
	        <tr>
                <td class="td-label">
		        备  注
	        </td>
	       
	        <td>
		        <asp:TextBox ID="txtRemark" runat="server"></asp:TextBox>
	        </td>
	     
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
