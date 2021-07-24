<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TwoPassSet.aspx.cs" Inherits="oa_SysAdmin_UserManage_TwoPassSet" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>安全码验证</title>
    <script language="javascript">
     window.name="win";
     
	function Enter() 
	{ 
		var keycode=event.keyCode; 
		var ss= document.getElementById("txtpass").vaule;
		if(keycode==13 && ss!="") 
		{ 
			//alert(keycode); 
			document.getElementById("Button1").click(); 
			document.getElementById("txtpass").vaule=ss;
		} 
	} 
	function Close() {
		if(document.getElementById('ttText').value=="1"){
			window.close ();
		}
		else {
			try {
				top.frameWorkArea.window.desktop.getActive().close();
			} catch (ex) {
				top._closeTab();
			}
		}
	}
          
 //设置弹出页面的table和页面一样大 zyg 08-12-16
    function setSize()  
        {              
             alert( document.body.offsetWidth);    
             <%if (base.Request["tt"] == null && base.Request["tt"].ToString() == "1")
{%>
		       document.all.tabmain.height=document.body.offsetHeight;
               document.all.tabmain.width=document.body.offsetWidth; 
			  <%}%>   			                                			
        } 

    </script>
</head>
<body class="body_frame" scroll="no" onkeydown="Enter();">
    <form id="form1" target="win" runat="server">
    <asp:HiddenField ID="ttText" runat="server" />
    <table id="tabmain" align="center" style="border: 1px solid Gray;" height="115px" width="260px" runat="server"><tr class="td-head" runat="server"><td colspan="2" align="center" runat="server">
                <asp:Label ID="labhead" Text="Label" ForeColor="Red" Font-Names="微软雅黑" runat="server"></asp:Label>
            </td></tr><tr runat="server"><td colspan="3" runat="server">
            </td></tr><tr runat="server"><td width="70px" runat="server">
                &nbsp;密码:
            </td><td width="" runat="server">
                <asp:TextBox ID="txtpass" Width="170px" TextMode="Password" ForeColor="Red" runat="server"></asp:TextBox>
            </td></tr><tr runat="server"><td colspan="3" runat="server">
            </td></tr><tr runat="server"><td colspan="2" align="right" runat="server">
                &nbsp;<input id="Button1" type="button" value="确 定" OnServerClick="Button1_ServerClick" runat="server" />

                <input type="button" value="取 消" onclick="Close()" id="btncancel">&nbsp;
            </td></tr></table>
    </form>
</body>
</html>
