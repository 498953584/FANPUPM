<%@ Page Language="C#" AutoEventWireup="true" CodeFile="help.aspx.cs" Inherits="helpEditFrm" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>项目管理系统帮助</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

    <script language="javascript">
//    document.onmousedown=click   
//      function   click()     
//      {if (event.button!=1)   
//        {return;alert('无权查看') 
//        }
//      } 
      function selrow(obj)
		{
                 try
			    {
			     document.getElementById(obj).click();
                }
                catch(e){}
		}
//		function url()
//		{
//		  alert('<%=Request["url"] %>');
//		}
		
   </script> 
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" scroll=no   >

    <form id="form1" runat="server">
    <TABLE id="Table2" class="table-normal" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0" >
		<tr style=" height:20" id="trtop" runat="server"><td colspan="2" runat="server">
		        <table width="100%" border="0" cellpadding="0" cellspacing="0" background="../Help/help_img/top_bg.jpg">
                  <tr>
                    <td width="6%" height="73px">&nbsp;</td>
                    <td width="76%" valign="middle" style="padding-bottom:15px;"><img src="../Help/help_img/help_logo.jpg" width="267" height="30" /></td>
                    <td width="18%" align="right"><img src="../Help/help_img/top_right.jpg" width="154" height="73" /></td>
                  </tr>
                </table>
		</td></tr>
		<TR>
			<td height="100%" vAlign="top" align="left" style="width: 13% ;background-color: #E9F3FC;" >
				<DIV class="gridBox" align="left" style="background-color: #E9F3FC;" >
                    <asp:TreeView ID="TrvHelp" ExpandDepth="0" showtooltip="False" selectexpands="True" Height="100%" Width="220px" ForeColor="Black" ImageSet="Faq" runat="server"><SelectedNodeStyle BorderColor="Transparent" ForeColor="Red" /></asp:TreeView>
				</DIV>
			</td>
			<TD>
				<iframe name="ifrMain" id="ifrMain" width="100%" height="100%" scrolling="yes" src="/Help/helpsel.aspx?id=&mc=" runat="server">
				</iframe>
			</TD>
		</TR>
	</TABLE>
    </form>
</body>
</html>
