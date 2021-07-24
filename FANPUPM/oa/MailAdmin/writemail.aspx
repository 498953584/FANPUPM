<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="writemail.aspx.cs" Inherits="WriteMail" %>
<%@ Import Namespace="AjaxControlToolkit"%>
<%@ Import Namespace="FreeTextBoxControls"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc" %>
<%@ Register TagPrefix="MyUserControl" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>WriteMail</title><link href="../../StyleCss/site.css" rel="stylesheet" type="text/css" />


    <script type="text/JavaScript">

		//收件人
			function showConsignee() 
			{
				document.Form1.TBoxConsignee.value = "";
				document.getElementById('hf').value = "";
				var url = "Consignee.aspx";
				var human = new Array(); 
				window.showModalDialog(url,human,"dialogHeight:422px;dialogWidth:659px;center:1;help:0;status:0;");
				for (var i=0;i<human.length;i++)
				{
					//document.Form1.HdnConsignee.value += human[0][i] + "!";
					document.getElementById("TBoxConsignee").value += human[i] + ",";
					document.getElementById('hf').value += human[i] + ",";
				}
			}
			//抄送
			function showCopy() 
			{
				document.Form1.txtCopy.value = ""; 
				document.getElementById('HdnCopy').value = "";   
				var url = "Consignee2.aspx";
				var human = new Array();
				window.showModalDialog(url, human, "dialogHeight:422px;dialogWidth:659px;center:1;help:0;status:0;");
				for (var i=0;i<human.length;i++)
				{
					//document.Form1.HdnConsignee.value += human[0][i] + "!";
					document.getElementById("txtCopy").value += human[i] + ",";
					document.getElementById('HdnCopy').value += human[i] + ",";
				}
			}
			//密送
			function showSecret()
			{
				document.Form1.txtSecret.value = "";
				document.getElementById('HdnSecret').value = "";
				var url = "Consignee3.aspx";
				var human = new Array();
				window.showModalDialog(url, human, "dialogHeight:422px;dialogWidth:659px;center:1;help:0;status:0;");
				for (var i=0;i<human.length;i++)
				{
					//document.Form1.HdnConsignee.value += human[0][i] + "!";
					document.getElementById("txtSecret").value += human[i] + ",";
					document.getElementById('HdnSecret').value += human[i] + ",";
				}
			}
			
			function showAnnex()
			{
				document.Form1.TBoxAnnex.value = "";
				var url = "broker.aspx?go=1";
				var annex = new Array();
				window.showModalDialog(url,annex,"dialogHeight:225px;dialogWidth:425px;center:1;help:0;status:0;");
				for (var i=0;i<annex.length;i++)
				{
					document.Form1.TBoxAnnex.value += annex[i] + ",";
				}
			}
			//zyg
			function getWhereForc(obj)
			{
			    document.getElementById("fcnum").value =obj;
			    //alert(obj);
			    if(obj=="1")
			    {
			     document.getElementById("TBoxConsignee").style.backgroundColor='#FFFFE0';
			     document.getElementById("txtCopy").style.backgroundColor='';
			     document.getElementById("txtSecret").style.backgroundColor='';
			    }
			    if(obj=="2")
			    {
			     document.getElementById("txtCopy").style.backgroundColor='#FFFFE0';
			     document.getElementById("txtSecret").style.backgroundColor='';
			     document.getElementById("TBoxConsignee").style.backgroundColor='';
			    }
			    if(obj=="3")
			    {
			     document.getElementById("txtSecret").style.backgroundColor='#FFFFE0';
			     document.getElementById("TBoxConsignee").style.backgroundColor='';
			     document.getElementById("txtCopy").style.backgroundColor='';
			    }    
		
			}
    </script>

    <style type="text/css">
        html, body
        {
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
            padding: 0px;
            width: 100%;
            height: 100%;
            color: #000000;
        }
    </style>
</head>
<body class="body_frame">
    <form id="Form1" name="Form1" method="post" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <!--原表格左部wxf-->
                <table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0"
                    class="table-normal" bgcolor="#F2F7FD">
                    <tr>
                        <td colspan="2" height="15px">
                        </td>
                    </tr>
                    <tr>
                        <td width="108" height="25" align="center">
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 收件人:
                        </td>
                        <td width="490" align="left">
                            <asp:TextBox ID="TBoxConsignee" Columns="60" ReadOnly="true" CssClass="zhengwen_input_text_no100" runat="server"></asp:TextBox><img src="../../images/corp.gif" onclick="showConsignee()" style="margin-left: -1px;
                                cursor: hand" align="absmiddle" title="选择收件人" runat="server" />
&nbsp;
                            <asp:HiddenField ID="hf" runat="server" />
                            <asp:HiddenField ID="fcnum" Value="1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td height="25" align="center">
                            抄&nbsp;&nbsp;送:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCopy" Columns="60" ReadOnly="true" CssClass="zhengwen_input_text_no100" runat="server"></asp:TextBox><img id="Img1" src="../../images/corp.gif" onclick="showCopy()" style="margin-left: -1px;
                                cursor: hand" align="absmiddle" title="选择抄送人" runat="server" />

                            <asp:HiddenField ID="HdnCopy" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td height="25" align="center">
                            密&nbsp;&nbsp;送:
                        </td>
                        <td>
                            <asp:TextBox ID="txtSecret" Columns="60" ReadOnly="true" CssClass="zhengwen_input_text_no100" runat="server"></asp:TextBox><img src="../../images/corp.gif" onclick="showSecret()" style="margin-left: -1px;
                                cursor: hand" align="absmiddle" title="选择密送人" runat="server" />

                            <asp:HiddenField ID="HdnSecret" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td height="25" align="center">
                            主&nbsp;&nbsp;题:
                        </td>
                        <td>
                            <asp:TextBox ID="TBoxTitle" Columns="60" CssClass="zhengwen_input_text_no100" runat="server"></asp:TextBox><input id="HdnAnnex" style="width: 1px; height: 1px" type="hidden" name="HdnAnnex" runat="server" />

                        </td>
                    </tr>
                    <tr>
                        <td height="25" align="center">
                            附&nbsp;&nbsp;件:
                        </td>
                        <td>
                            <asp:TextBox ID="TBoxAnnex" ReadOnly="true" Columns="60" CssClass="zhengwen_input_text_no100" runat="server"></asp:TextBox>
                            <img Src="../../images/icon_att0b3dfa.gif" Align="absmiddle" runat="server" />

                            <asp:LinkButton ID="LinkButton1" Style="cursor: hand" runat="server">增删附件</asp:LinkButton>
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="height: 1px" bgcolor="#95BFF2" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left" background="../../images/bg_mail_title.jpg">
                            <table cellspacing="0" cellpadding="0" align="center" width="100%" border="0" height="37px">
                                <tr align="center">
                                    <td>
                                        <asp:Button ID="btnsend1" Text="发  送" CssClass="button-normal" OnClick="btnsend1_Click" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnsave1" Text="保存到草稿箱" CssClass="button-normal" Width="90px" OnClick="btnsave1_Click" runat="server" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chfj" Text="保存到发件箱" Checked="true" runat="server" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chmb" Text="手机短信督办" Height="16px" runat="server" />
                                    </td>
                                    <td style="width: 133px">
                                        <asp:CheckBox ID="chtr" Text="待办消息" Height="16px" Checked="true" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 1px" bgcolor="#95BFF2" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 2px" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%" colspan="2" align="left">
                            <MyUserControl:FreeTextBox ID="TBoxContent" BackColor="#E0E0E0" GutterBackColor="#E0E0E0" ToolbarBackColor="#81A9E2" HelperFilesPath="/CommonWindow/FreeTextBox/" HelperFilesParameters="/CommonWindow/FreeTextBox/" ImageGalleryPath="UploadFiles/mail/img" Height="420px" ToolbarType="Office2003" Width="100%" runat="server" />
                            
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 2px" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 1px" bgcolor="#95BFF2" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left" background="../../images/bg_mail_title.jpg">
                            <table cellspacing="0" cellpadding="0" align="center" width="100%" border="0" height="37px">
                                <tr align="center">
                                    <td>
                                        <asp:Button ID="BtnSend" Text="发  送" CssClass="button-normal" OnClick="BtnSend_Click" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Button ID="BtnToDraft" Text="保存到草稿箱" CssClass="button-normal" Width="90px" OnClick="BtnToDraft_Click" runat="server" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="CBoxSend" Text="保存到发件箱" runat="server" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="CBoxSMS" Text="手机短信督办" Height="16px" runat="server" />
                                    </td>
                                    <td style="width: 133px">
                                        <asp:CheckBox ID="CBRTX" Text="待办消息" Height="16px" Checked="true" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <asp:Panel ID="Panel1" Height="100%" Width="100px" BorderColor="#9FAFBB" BorderStyle="Double" BorderWidth="0px" runat="server">
                    <uc:TabContainer ID="TabContainer1" ActiveTabIndex="0" Width="120px" BorderStyle="None" ScrollBars="Vertical" Font-Names="华文宋体" runat="server">
                        <uc:TabPanel ID="tp1" HeaderText="常用联系人" runat="server">
<ContentTemplate>
                                <asp:TreeView ID="TreeView1" BorderColor="InactiveCaption" ForeColor="Black" Height="100%" ShowLines="true" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" runat="server"></asp:TreeView>
                            
                        </ContentTemplate>
</uc:TabPanel>
                        <uc:TabPanel ID="tp2" HeaderText="好友录" runat="server">
<ContentTemplate>
                                <asp:TreeView ID="TreeView2" BorderColor="InactiveCaption" ForeColor="Black" Height="100%" ShowLines="true" OnSelectedNodeChanged="TreeView2_SelectedNodeChanged" runat="server"></asp:TreeView>

                            
                        </ContentTemplate>
</uc:TabPanel>
                    </uc:TabContainer>
                 
                </asp:Panel>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
<style type="text/css">
.ajax__tab_xp .ajax__tab_tab {
height: 21px;
}
</style>
