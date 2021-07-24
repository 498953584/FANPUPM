<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="helpupd.aspx.cs" Inherits="Help_helpupd" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Import Namespace="FredCK.FCKeditorV2"%>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="uc" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>无标题页</title>
   <script language="javascript">
//    document.onmousedown=click   
//    
//      function   click()     
//      {if   (event.button!=1)   
//        {alert('哈哈！不给你用') 
//        }
//      } 
   </script> 
</head>
<body class="body_clear">
    
    <form id="form1" runat="server">
    <div>
    <table class="table-normal" id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="1"
				height="100%" >
        <tr class="td-toolsbar" >
           <td  align="center" >
                        帮助管理</td>
           <td align="center" class=td-submit><asp:Button ID="Btn_save" CssClass="button-normal" Text="保 存" OnClick="Btn_save_Click" runat="server" />&nbsp;
					
						
			</td>
        </tr>
        <tr>
            <td style="width: 62px; " class="td-label"> 标 题:</td>
            <td > <asp:TextBox ID="Txt_mc" ReadOnly="true" CssClass="text-normal" Width="300px" runat="server"></asp:TextBox></td>
            
        </tr>
        <tr>
            <td colspan="2" vAlign="top" align="center" >
                <uc:FCKeditor ID="FCKeditor1" AutoDetectLanguage="false" DefaultLanguage="zh-cn" Height="560px" ToolbarSet="Default" runat="server"></uc:FCKeditor>
            </td>
        </tr>
        <TR>
					
				</TR>
    </table>
    </div>
        <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
