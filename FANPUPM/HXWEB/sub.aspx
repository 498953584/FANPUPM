<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sub.aspx.cs" Inherits="Default2" SmartNavigation="true" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"><title>华夏幸福基业</title>
    <script language="javascript">
    function showPic(pic)
    {
        document.getElementById("img1").src = pic;
    }
    
    function winOpen(url)
    {
        window.open(url);
        //var vWidth = screen.availWidth-10;
        //var vHeight = screen.availHeight;
    　　//window.open (url, '', "top=0, left=0, toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=no, status=no,width=" + vWidth + ",height=" + vHeight);
    }
    </script>
   
<link href="/site.css" rel="stylesheet" type="text/css" />
</head>
<body id="body_sub"  topmargin="0" buttommargin="0" leftmargin="0" rightmargin="0">
    <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <form id="form1" runat="server">
  <table width="1000" border="0" align="center" cellpadding="0" cellspacing="0">
    <tr> 
      <td height="80" style="border-bottom: #ff0000 1px solid;"><img src="img/03-top.gif" width="1000" height="80" usemap="#Map" border="0">      </td>
    </tr>
  </table>
<table width="930" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="1"></td>
  </tr>
</table>
  <table width="1000" border="0" align="center" cellpadding="0" cellspacing="0">
    <tr> 
      <td width="125">&nbsp;</td>
      <td>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr> 
            <td colspan="3" height="33"><strong><a href="/index.aspx" target="_self" class="main_news_text">华夏幸福基业股份有限公司</a> 
              <asp:Label ID="lblNavigator" runat="server"></asp:Label></strong></td>
          </tr>
          <tr> 
            <td width="151" valign="top"> 
              <table width="151" border="0" cellspacing="0" cellpadding="0">
                <tr> 
                  <td colspan="3"><img src="img/03-banerTop.gif" width="151" height="10"></td>
                </tr>
                <tr> 
                  <td width="1" background="img/03-Border.gif"></td>
                  <td width="149" align="center"> <asp:GridView ID="GridView1" AutoGenerateColumns="false" Width="90%" GridLines="None" OnRowDataBound="GridView1_RowDataBound" runat="server"><Columns><asp:TemplateField HeaderText="图片">
<ItemTemplate> 
                    <asp:Label ID="Label1" Text="Label" runat="server"></asp:Label> 
                    <asp:Image ID="Img2" runat="server" /> <asp:Label ID="Label2" Text="Label" Visible="false" runat="server"></asp:Label> 
                    <asp:HyperLink ID="HyperLink1" runat="server">HyperLink</asp:HyperLink> 
                    </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="c_xwlxdm" HeaderText="编号" Visible="false" /><asp:BoundField DataField="c_xwlxmc" HeaderText="名称" Visible="false" /></Columns></asp:GridView> </td>
                  <td width="1" background="img/03-Border.gif"></td>
                </tr>
                <tr> 
                  <td colspan="3"><img src="img/03-banerButtom.gif" width="151" height="10"></td>
                </tr>
              </table>
            </td>
			<td width="20"></td>
            <td valign="top"> 
              <table width="100%" border="0" cellspacing="0" cellpadding="0">
			   <tr><td heigh="8" style="border-top: activeborder 1px solid;">&nbsp;</td></tr>
                <tr>
                  <td><img src="/HXBBS/images/forumbox_head.gif" width="580" height="70"></td>
                </tr>
              </table>
              <!--放置新闻正文、或者新闻列表-->
              <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr> 
                  <td colspan="4" align="left"> 
                    <!--新闻正文-->
                    <asp:Label ID="Lbxwnr" runat="server"></asp:Label> 
                      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                    <asp:GridView ID="gvNewList" AllowPaging="true" AutoGenerateColumns="false" ShowHeader="false" Width="100%" GridLines="None" OnPageIndexChanging="gvNewList_PageIndexChanging" OnRowDataBound="gvNewList_RowDataBound" runat="server"><Columns><asp:ButtonField CommandName="Select" DataTextField="v_xwbt" /><asp:BoundField DataField="dtm_fbsj" HeaderText="dtm_fbsj" SortExpression="dtm_fbsj" /></Columns><PagerStyle HorizontalAlign="Center"></PagerStyle><RowStyle Height="25px"></RowStyle></asp:GridView> 
                          </ContentTemplate>
</asp:UpdatePanel>
                    <!--新闻列表-->
                      &nbsp;&nbsp; </td>
                </tr>
              </table>
              <br>
              <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr> 
                  <td colspan="4" align="center">&nbsp;</td>
                </tr>
                <tr> 
                  <td colspan="4" align="center" bgcolor="#DDDDDD" height="1px;"></td>
                </tr>
                <tr> 
                  <td colspan="4" height="8"></td>
                </tr>
                <tr> 
                  <td colspan="4" style="height: 10px"></td>
                </tr>
              </table>
            </td>
          </tr>
          <tr> 
            <td colspan="3">
              <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border-top: activeborder 1px solid;">
                <tr> 
                  <td  align="left" style="color:#848484;font-size:9pt;line-height:180%;padding-left:20px;font-family:'宋体'">
                    <div align="center">版权所有 华夏幸福基业股份有限公司 <br />
                      Copyright 2008 CHINA FORTUNE LAND DEVELOPMENT CO.,Ltd</div>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
        </table>
      </td>
      <td width="125">&nbsp;</td>
    </tr>
  </table>
<table width="930" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="1"></td>
  </tr>
</table>
</form>
<map name="Map">
  <area shape="rect" coords="422,44,506,84" href="sub.aspx?type=01&code=010101">
  <area shape="rect" coords="525,44,604,84" href="sub.aspx?type=02&code=02">
  <area shape="rect" coords="625,44,703,84" href="sub.aspx?type=03&code=0301">
  <area shape="rect" coords="720,44,796,84" href="sub.aspx?type=04&code=0401">
  <area shape="rect" coords="811,44,885,84" href="sub.aspx?type=05&code=050101">
<area shape="rect" coords="117,4,347,77" href="/index.aspx"></map>
</body>
</html>
