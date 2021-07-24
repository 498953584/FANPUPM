<%@ Page Language="C#" AutoEventWireup="true" CodeFile="viewmail.aspx.cs" Inherits="ViewMail" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>阅读邮件</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

    <style type="text/css">
        html,body {
        margin-left:0px;
        margin-top:0px;
        margin-right:0px;
        margin-bottom:0px;
        PADDING: 0px;
        width:100%;
        height:100%;
        color:#000000;
        background-color:#F2F7FD;
        }
        </style>
    <link href="../../StyleCss/PM1.css" rel="stylesheet" type="text/css" />


    <script language="javascript">
		function download(filepath,OriginalName)
	    {
	        var url = "/EPC/uploadfile/down.aspx?fileName=" + escape(OriginalName) + "&filePath=" + escape(filepath) ;
            window.location.href = url;
	    }
    </script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
        <font face="宋体">
            <table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                 <tr >
                    <td align="left" background="../../images/bg_mail_title.jpg" height="37">
                        &nbsp;&nbsp; <a href="inbox.aspx" style="color: Maroon;">返回到收件箱 </a>
                        <asp:HyperLink ID="HyperLink1" Visible="false" ForeColor="Black" runat="server">返回
                        </asp:HyperLink>
                    </td>
                     <td align="right"  background="../../images/bg_mail_title.jpg">
                                    <asp:Button ID="BtnDelN" Text="删除" CssClass="button-normal" OnClick="BtnDelN_Click" runat="server" />
                                    <asp:Button ID="BtnDelY" Text="彻底删除" Width="80px" CssClass="button-normal" OnClick="BtnDelY_Click" runat="server" />
                                    <asp:Button ID="BtnEdit" Text="编辑" Width="80px" CssClass="button-normal" OnClick="BtnEdit_Click" runat="server" />
                                    <asp:Button ID="BtnReply" Text="回复" CssClass="button-normal" OnClick="BtnReply_Click" runat="server" />
                                    <asp:Button ID="BtnTrans" Text="转发" CssClass="button-normal" OnClick="BtnTrans_Click" runat="server" />
                                </td>
                </tr>
				             <tr>
                <td style="height: 1px"  bgcolor="#95BFF2" colspan=2>
                </td>
            </tr>
                <tr>
                    <td colspan=2 align="right" valign="top" bgcolor="aliceblue">
                        <table id="Table2" width="100%" border="0" align="center" cellpadding="0" cellspacing="0"
                            class="pt9">
                            <tr>
                              <td></td>
                                <td height="10">                                </td>
                            </tr>
                            <tr>
                              <td align="center" width="33" style="font-weight: bolder">&nbsp;</td>
                                <td align="center" width="75" height="30" style="font-weight: bolder">
                                    发信人：</td>
                                <td width="763">
                                <asp:Label ID="LabSender" runat="server"></asp:Label>                                </td>
                            </tr>
                            <tr>
                              <td align="center" style="font-weight: bolder">&nbsp;</td>
                                <td align="center" height="30" style="font-weight: bolder">
                                    收信人：</td>
                                <td>
                                    <asp:Label ID="LabConsignee" runat="server"></asp:Label>                                </td>
                            </tr>
                            <tr>
                              <td align="center" style="font-weight: bolder">&nbsp;</td>
                                <td align="center" height="30" style="font-weight: bolder">
                                    抄送人：</td>
                                <td>
                                    <asp:Label ID="LbCSR" runat="server"></asp:Label>                                </td>
                            </tr>
                            <tr>
                              <td align="center" style="font-weight: bolder">&nbsp;</td>
                                <td align="center" height="30" style="font-weight: bolder">
                                    主&nbsp;&nbsp;题：</td>
                                <td>
                                    <asp:Label ID="LabTitle" runat="server"></asp:Label>                                </td>
                            </tr>
                            <tr>
                              <td align="center" style="font-weight: bolder">&nbsp;</td>
                                <td align="center" height="30" style="font-weight: bolder">
                                    日&nbsp;&nbsp;期：</td>
                                <td>
                                    <asp:Label ID="LabDateTime" runat="server"></asp:Label>                                </td>
                            </tr>
                            <tr>
                              <td align="center" style="font-weight: bolder">&nbsp;</td>
                                <td align="center" height="30" style="font-weight: bolder">
                                    附&nbsp;&nbsp;件：</td>
                                <td id="tr_fj" runat="server">
                                    <div id="annexBlock" runat="server">                                    </div>
                                    <iframe height="0" width="0" name="fileload" id="fileload"></iframe>                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td height="3">
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                          <tr>
                            <td height="26" background="../../images/viewmail_middle.jpg"></td>
                          </tr>
                          <tr>
                            <td align="center" bgcolor="#FFFFFF"><table width="96%" border="0" cellpadding="20" cellspacing="0" class="pt9">
                              <tr>
                                <td colspan="2" bgcolor="#ffffff" style="line-height: 220%;"><asp:Label ID="LblCon" Width="90%" Height="250px" runat="server"></asp:Label>
                                </td>
                              </tr>
                            </table></td>
                          </tr>
                        </table>
                        </td>
                </tr>
                <tr>
                    <td height="2">
                    </td>
                </tr>
				             <tr>
                <td style="height: 1px"  bgcolor="#95BFF2" colspan=2>
                </td>
            </tr>
                <tr>
                    <td align="left" background="../../images/bg_mail_title.jpg" height="37" colspan=2>
                        &nbsp;&nbsp; <a href="inbox.aspx" style="color: Maroon;">返回到收件箱 </a>
                        <asp:HyperLink ID="HLnkBack" Visible="false" ForeColor="Black" runat="server">返回
                        </asp:HyperLink>
                    </td>
                </tr>
            </table>
        </font>
    </form>
</body>
</html>
