<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IssuePayInfoMonth.aspx.cs" Inherits="HR_Salary_IssuePayInfoMonth" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>发放月份</title>
     <script type="text/javascript" language="javascript">
    <!--
    window.name = "win";

       -->
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="form1" target="win" runat="server">
    <div>
     <table class="table-normal" cellspacing="0" cellpadding="0" width="100%" border="1" id="TABLE1" language="javascript" onclick="return TABLE1_onclick()">
      <tr>
             <td class="td-head"  colspan="2">请选择发放月份</td>
      </tr>
     <tr><td colspan="2">
         <asp:RadioButtonList ID="CBLMonth" RepeatDirection="Horizontal" Width="100%" runat="server"><asp:ListItem Value="1" Selected="true" Text="一月" /><asp:ListItem Value="2" Text="二月" /><asp:ListItem Value="3" Text="三月" /><asp:ListItem Value="4" Text="四月" /><asp:ListItem Value="5" Text="五月" /><asp:ListItem Value="6" Text="六月" /><asp:ListItem Value="7" Text="七月" /><asp:ListItem Value="8" Text="八月" /><asp:ListItem Value="9" Text="九月" /><asp:ListItem Value="10" Text="十月" /><asp:ListItem Value="11" Text="十一月" /><asp:ListItem Value="12" Text="十二月" /></asp:RadioButtonList></td></tr> 
         <tr>
           <td style="width:15%" class="td-label">
               情况说明</td>
           <td>
               <asp:TextBox ID="txtRemark" Rows="4" TextMode="MultiLine" Width="80%" runat="server"></asp:TextBox></td>
          </tr>
        <tr>
			<td  align="center" class="td-submit"  colspan="2">
					<asp:Button ID="BtnSave" Text="确 定" OnClick="BtnSave_Click" runat="server" />&nbsp;&nbsp;
                &nbsp; &nbsp;
            </td>
		</tr> 
     </table> 
    
    </div>
    </form>
     <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
</body>
</html>
