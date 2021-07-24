<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EMail.aspx.cs" Inherits="Z_Demo_EMail" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title></head>
<body>
    <form id="form1" runat="server">
    <div>
    
    	<asp:Button ID="Button1" Text="Send" OnClick="Button1_Click" runat="server" />
		<asp:Button ID="Button2" Text="ThreadSend" OnClick="Button2_Click" runat="server" />
		<asp:Button ID="Button3" Text="SendSync" OnClick="Button3_Click" runat="server" />
    
    </div>
    </form>
</body>
</html>
