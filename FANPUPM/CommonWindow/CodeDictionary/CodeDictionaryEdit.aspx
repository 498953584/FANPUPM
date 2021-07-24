<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CodeDictionaryEdit.aspx.cs" Inherits="CommonWindow_CodeDictionary_CodeDictionaryEdit" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>编码字典</title>
 <script type="text/javascript" language="javascript">
    <!--
    window.name = "win";
       -->
    </script>
    
</head>
<body class="body_clear" scroll="no">
    <form id="form1" target="win" runat="server">
    <div>
    <table class="table-normal" cellspacing="0" cellpadding="0" width="100%" border="1">
      <tr>
             <td class="td-title" colspan="2">编码字典数据维护</td>
      </tr>
	   
	   
	    <tr>
	        <td class="td-label" style="width:25%">
		        字典数据值:</td>
	        <td>
		        <asp:TextBox ID="txtDisplayValue" Width="90%" CssClass="text-normal" runat="server"></asp:TextBox>
		        </td>
	    </tr>
	     <tr> 
	        <td class="td-label" style="height: 21px" >
		        </td>
	        <td  align="left" style="height: 21px">
                <asp:CheckBox ID="CBIsValid" Text="是否有效" Checked="true" runat="server" /></td>
	    </tr>
	    
	   	<tr>
			<td  align="center" colspan="2" class="td-submit">
					<asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" />&nbsp;<input id="BtnClose" onclick="window.close();" type="button" value="关 闭"/>&nbsp;
               
                
            </td>
		</tr>
</table>
    </div>
        <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
