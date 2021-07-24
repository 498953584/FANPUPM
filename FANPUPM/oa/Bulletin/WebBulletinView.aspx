<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebBulletinView.aspx.cs" Inherits="oa_Bulletin_WebBulletinView" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>公告信息</title><link href="/site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="gonggao">
        <h1><span><asp:Label ID="lbDate" Text="" runat="server"></asp:Label></span></h1>
        <h2><asp:Label ID="lbTitle" Text="" runat="server"></asp:Label></h2>
        <div class="txt">
            <p> &nbsp;<asp:Label ID="LblCon" runat="server"></asp:Label></p>            
              <h3>url地址：<asp:HyperLink ID="hlUrl" NavigateUrl="" runat="server"></asp:HyperLink><br />附件：<asp:Literal ID="Literal1" runat="server"></asp:Literal></h3>
        </div>
      
        <div class="close"> 
        <a href="#" onclick="javascript:window.close();">关闭窗口</a>       
        </div>
    </div>
   
    </form>
</body>
</html>
