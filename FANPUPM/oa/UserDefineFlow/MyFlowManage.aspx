<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyFlowManage.aspx.cs" Inherits="oa_UserDefineFlow_myflowmanage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>流程控制</title>
  
    <script type='text/javascript' language='javascript' src="../../SysFrame/jscript/JsControlMenuTool.js"></script>

    <script type="text/javascript" src="../../../Web_Client/Tree.js"></script>

    <script language="javascript" type="text/javascript">
    <!--   
    function mymap(i)
    {
     var url="";
     var title="流程监控";
     if (i==1)
     {
      url="/EPC/WorkFlow/PTAuditList.aspx";
      title="待审核的流程";
     }
     if (i==2)
     {
      url="../oa/UserDefineFlow/MyFlowInitiate.aspx";
      title="我发起的流程";
     }
     if (i==3)
     {
      url="../oa/UserDefineFlow/MyFlowEnlist.aspx";
      title="我参与的流程";
     }
     if (i==4)
     {
      url="../EPC/Workflow/RecieveMsgList.aspx";
      title="被告知的流程";
     }
     if (i==5)
     {
      url="../oa/UserDefineFlow/MyFlow.aspx";
      title="发起自定义流程";
     }
     if (i==6)
     {
      url="../EPC/Workflow/WorkFlowCountFrame.aspx";
      title="流程统计";
     }
     if (i==7)
     {
      url="../EPC/Workflow/dbrSet.aspx";
      title="代办人设置";
     }
     if (i==8)
     {
      url="../oa/UserDefineFlow/FlowClass.aspx"
      title="流程分类设置";
     }
      javascript:toolbox_oncommand(url,title);
    }
	-->
    </script>
    <style type="text/css">
.main_ico {display:block;height:95px;boder:1px solid #c50;text-decoration: none;}
.main_ico_text {display:block;margin-top:10px;text-align:center;font-size:14px;color:#0066CC; font-weight:bold;text-decoration: none;}
.norepeat { background-repeat:no-repeat;}

.main_ico1 {display:block;height:95px;boder:1px solid #c50;text-decoration: none;}
.main_ico2 {display:block;height:95px;boder:1px solid #c50;text-decoration: none;}
.main_ico3 {display:block;height:95px;boder:1px solid #c50;text-decoration: none;}
.main_ico4 {display:block;height:95px;boder:1px solid #c50;text-decoration: none;}
.main_ico5 {display:block;height:95px;boder:1px solid #c50;text-decoration: none;}
.main_ico6 {display:block;height:95px;boder:1px solid #c50;text-decoration: none;}
.main_ico7 {display:block;height:95px;boder:1px solid #c50;text-decoration: none;}
</style>
</head>
<body style="background-color:#f2f8fe">
		<form id="Form1" method="post" runat="server">
		<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td>&nbsp;</td>
    <td height="30" align="center" valign="top">&nbsp;</td>
    <td align="center" valign="top"></td>
    <td align="center" valign="top">&nbsp;</td>
    <td align="center" valign="top">&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td height="123" align="center" valign="top">
    
    <table width="117" border="0" cellspacing="0" cellpadding="0" style="cursor:pointer;">
  <tr>
    <td height="134" class="norepeat" style="text-align:left" background="images/main_ico/ico11.jpg" onmouseover="this.style.backgroundImage='url(images/main_ico/ico1.jpg)'" onmouseout="this.style.backgroundImage='url(images/main_ico/ico11.jpg)'">
    <a href="#" onclick="mymap(1)" class="main_ico">&nbsp;</a>
    <a href="#" onclick="mymap(1)" class="main_ico_text">待审核的流程</a>    </td>
    </tr>
</table>    </td>
    <td align="center" valign="top"><table width="117" border="0" cellspacing="0" cellpadding="0" style="cursor:pointer;">
      <tr>
        <td height="134" class="norepeat" background="images/main_ico/ico21.jpg" onmouseover="this.style.backgroundImage='url(images/main_ico/ico2.jpg)'" onmouseout="this.style.backgroundImage='url(images/main_ico/ico21.jpg)'">
        <a href="#" onclick="mymap(2)" class="main_ico1">&nbsp;</a> 
        <a href="#" onclick="mymap(2)" class="main_ico_text">我发起的流程</a> </td>
      </tr>
    </table></td>
    <td align="center" valign="top"><table width="117" border="0" cellspacing="0" cellpadding="0" style="cursor:pointer;">
      <tr>
        <td height="134" class="norepeat" background="images/main_ico/ico31.jpg" onmouseover="this.style.backgroundImage='url(images/main_ico/ico3.jpg)'" onmouseout="this.style.backgroundImage='url(images/main_ico/ico31.jpg)'">
        <a href="#" onclick="mymap(3)" class="main_ico2">&nbsp;</a> 
        <a href="#" onclick="mymap(3)" class="main_ico_text">我参与的流程</a> </td>
      </tr>
    </table></td>
    <td align="center" valign="top"><table width="117" border="0" cellspacing="0" cellpadding="0" style="cursor:pointer;">
      <tr>
        <td height="134" class="norepeat" background="images/main_ico/ico41.jpg" onmouseover="this.style.backgroundImage='url(images/main_ico/ico4.jpg)'" onmouseout="this.style.backgroundImage='url(images/main_ico/ico41.jpg)'">
        <a href="#" onclick="mymap(4)" class="main_ico3">&nbsp;</a> 
        <a href="#" onclick="mymap(4)" class="main_ico_text">被告知的流程</a> </td>
      </tr>
    </table></td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td height="50">&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td height="123" align="center"><table width="117" border="0" cellspacing="0" cellpadding="0" style="cursor:pointer;">
      <tr>
        <td height="134" class="norepeat"  background="images/main_ico/ico51.jpg" onmouseover="this.style.backgroundImage='url(images/main_ico/ico5.jpg)'" onmouseout="this.style.backgroundImage='url(images/main_ico/ico51.jpg)'" onclick="mymap(5)" >
            <a href="#" class="main_ico4">&nbsp;</a> <a  href="#" onclick="mymap(5)" class="main_ico_text">电子办公</a> 
        </td>
     
      </tr>
    </table></td>
    <td align="center"><table width="117" border="0" cellspacing="0" cellpadding="0" style="cursor:pointer;">
      <tr>
        <td height="134"  class="norepeat" background="images/main_ico/ico61.jpg" onmouseover="this.style.backgroundImage='url(images/main_ico/ico6.jpg)'" onmouseout="this.style.backgroundImage='url(images/main_ico/ico61.jpg)'">
        <a href="#" onclick="mymap(6)" class="main_ico5">&nbsp;</a> <a href="#" onclick="mymap(6)" class="main_ico_text">流程统计</a> </td>
      </tr>
    </table></td>
    <td align="center"><table width="117" border="0" cellspacing="0" cellpadding="0" style="cursor:pointer;">
      <tr>
        <td height="134" class="norepeat" background="images/main_ico/ico71.jpg" onmouseover="this.style.backgroundImage='url(images/main_ico/ico7.jpg)'" onmouseout="this.style.backgroundImage='url(images/main_ico/ico71.jpg)'">
        <a href="#" onclick="mymap(7)" class="main_ico6">&nbsp;</a> <a href="#" onclick="mymap(7)" class="main_ico_text">代办人设置</a> </td>
      </tr>
    </table></td>
    <td align="center"><table width="117" border="0" cellspacing="0" cellpadding="0" style="cursor:pointer;" id="tableClass" visible="false" runat="server"><tr runat="server"><td height="134" class="norepeat" background="images/main_ico/ico81.jpg" onmouseover="this.style.backgroundImage='url(images/main_ico/ico8.jpg)'" onmouseout="this.style.backgroundImage='url(images/main_ico/ico81.jpg)'" runat="server">
        <a href="#" onclick="mymap(8)" class="main_ico7">&nbsp;</a> <a href="#" onclick="mymap(8)" class="main_ico_text">流程分类设置</a> </td></tr></table></td>
    <td>&nbsp;</td>
  </tr>
</table>
    
    </form>
   
</body>
</html>
