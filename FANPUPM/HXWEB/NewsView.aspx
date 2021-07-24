<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsView.aspx.cs" Inherits="HXWEB_NewsView" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title></title><link href="/site.css" rel="stylesheet" type="text/css" />
</head>
<body topmargin="0" leftmargin="0" rightmargin="0">
    <form id="form1" runat="server">
    <div aling=center>
	 <table width="1000" border="0" cellspacing="0" cellpadding="0">
	<tr> 
        <td height="71" background="img/top1.gif" width="55">&nbsp;</td>
        <td height="71" background="img/top1.gif" colspan="2"><img src="img/logo.gif" width="210" height="51"></td>
        <td height="71" background="img/top1.gif" valign="top"> 
          <div align="right"><img src="img/linian.gif" width="210" height="51"></div>
        </td>
        <td height="71" background="img/top1.gif" width="55">&nbsp;</td>
      </tr>
	  </table>
    <table width="1000" border="0" cellspacing="0" cellpadding="0">
      <tr> 
        <td height="8"  width="55">&nbsp;</td>
        <td height="8"  width="155"> 
          <div align="right"></div>
        </td>
        <td height="8"  style="border-bottom: activeborder 1px solid;">&nbsp;</td>
        <td height="8"  width="155">&nbsp;</td>
        <td height="8"  width="55">&nbsp;</td>
      </tr>
    </table>
    <table width="1000" border="0" cellspacing="0" cellpadding="0">
      <tr> 
        <td rowspan="5" width="55">&nbsp;</td>
        <td colspan="3" height="30" align="center">&nbsp;</td>
        <td rowspan="5" width="55">&nbsp;</td>
      </tr>
      <tr> 
        <td colspan="3"  align="center"><font style="font-size: 12pt; font-family: 宋体, 仿宋_GB2312, 楷体_GB2312, 黑体, 华文仿宋, 华文行楷; font-weight: bold;"><asp:Label ID="lblTitle" Text="新闻标题" runat="server"></asp:Label></font></td>
      </tr>
      <tr> 
        <td colspan="3" height="55">&nbsp;</td>
      </tr>
      <tr> 
        <td colspan="3" > <asp:Label ID="Lbxwnr" runat="server"></asp:Label> </td>
      </tr>
      <tr> 
        <td colspan="3" height="10">&nbsp;</td>
      </tr>
      
    </table>
    <table width="1000" border="0" cellspacing="0" cellpadding="0">
      <tr> 
        <td height="8"  width="55">&nbsp;</td>
        <td height="8"  width="155"> 
          <div align="right"></div>
        </td>
        <td height="8"  style="border-bottom: activeborder 1px solid;">&nbsp;</td>
        <td height="8"  width="155">&nbsp;</td>
        <td height="8"  width="55">&nbsp;</td>
      </tr>
    </table>
    <table width="1000" border="0" cellspacing="0" cellpadding="0">
      <tr background="img/top3.gif"> 
        <td colspan="5" height="36"> 
          <div align="center"><img src="img/guanbi.gif" width="7" height="7"> 
            <font size="-1"><A href="#" class="bottom_a"  onClick="window.close();" ><font color="#000000">关闭窗口</font></a></font></div>
        </td>
      </tr>
    </table>
  </div>
    </form>
</body>
</html>
