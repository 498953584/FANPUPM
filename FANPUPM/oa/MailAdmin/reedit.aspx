<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="reedit.aspx.cs" Inherits="ReEdit" %>
<%@ Register TagPrefix="MyUserControl" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>ReEdit</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

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


    <script language="JavaScript" type="text/JavaScript">
			function showConsignee()
			{
				document.Form1.TBoxConsignee.value = "";
				var url = "Consignee.aspx";
				var human = new Array();
				window.showModalDialog(url,human,"dialogHeight:422px;dialogWidth:659px;center:1;help:0;status:0;");
				for (var i=0;i<human.length;i++)
				{
					document.Form1.TBoxConsignee.value += human[i] + ",";
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

</head>
<body>
    <form id="Form1" method="post" runat="server">
        <font face="宋体">
            <table id="Table2" width="100%" border="0" align="center" cellpadding="0" cellspacing="0"
                class="pt9" bgcolor="AliceBlue">
                <tr>
                    <td align="right">
                        <asp:Label ID="LabWarn" runat="server"></asp:Label></td>
                </tr>
            </table>
            <table id="Table1" width="100%" border="0" align="center" cellpadding="0" cellspacing="0"
                class="pt9" bgcolor="AliceBlue">
                <tr>
                    <td width="12%" height="25" align="right">
                        收件人:&nbsp;&nbsp;</td>
                    <td>
                        <asp:TextBox ID="TBoxConsignee" ReadOnly="true" Columns="60" runat="server"></asp:TextBox>
                        <img id="ImgshowAn" src="../../images/corp.gif" onclick="showConsignee()" style="cursor: hand" align="absmiddle" title="选择收件人" runat="server" />

                        <asp:HiddenField ID="fcnum" Value="1" runat="server" />
                        </td>
                </tr>
                <tr>
                    <td width="12%" height="25" align="right">
                        抄送:&nbsp;&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtCopy" Columns="60" ReadOnly="true" CssClass="zhengwen_input_text_no100" runat="server"></asp:TextBox>
                        <img src="../../images/corp.gif" onclick="showCopy()" style="margin-left: -1px;
                            cursor: hand" align="absmiddle" title="选择抄送人" runat="server" />

                        <asp:HiddenField ID="HdnCopy" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td width="12%" height="25" align="right">
                        密送:&nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtSecret" Columns="60" ReadOnly="true" CssClass="zhengwen_input_text_no100" runat="server"></asp:TextBox>
                        <img src="../../images/corp.gif" onclick="showSecret()" style="margin-left: -1px;
                            cursor: hand" align="absmiddle" title="选择密送人" runat="server" />

                        <asp:HiddenField ID="HdnSecret" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td height="25" align="right">
                        主&nbsp;&nbsp;题:&nbsp;&nbsp;</td>
                    <td>
                        <asp:TextBox ID="TBoxTitle" Columns="60" runat="server"></asp:TextBox><input id="HdnDraft" style="width: 1px; height: 1px" type="hidden" name="HdnAnnex" runat="server" />

                    </td>
                </tr>
                <tr>
                    <td height="25" align="right">
                        附&nbsp;&nbsp;件:&nbsp;&nbsp;</td>
                    <td>
                        <asp:TextBox ID="TBoxAnnex" Columns="60" ReadOnly="true" runat="server"></asp:TextBox>&nbsp;&nbsp;<asp:Image ID="Image1" ImageUrl="~/images/icon_att0b3dfa.gif" runat="server" />
                       
                        <asp:LinkButton ID="lbzs" runat="server">增删附件</asp:LinkButton></td>
                </tr>
                <tr>
                    <td colspan="2" align="left" width="100%">
                        <MyUserControl:FreeTextBox ID="TBoxContent" BackColor="#E0E0E0" GutterBackColor="#E0E0E0" ToolbarBackColor="#81A9E2" HelperFilesPath="/CommonWindow/FreeTextBox/" HelperFilesParameters="/CommonWindow/FreeTextBox/" ImageGalleryPath="UploadFiles/mail/img" Height="400px" ToolbarType="Office2003" Width="680px" runat="server" />
                        
                    </td>
                </tr>
                <tr>
                    <td style="height: 1px" bgcolor="#95BFF2" colspan=2>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left" background="../../images/bg_mail_title.jpg">
                        <table cellspacing="0" cellpadding="0" align="center" width="100%" border="0" height="37px">
                            <tr align="center">
                                <td>
                                    <asp:Button ID="BtnSend" Text="发  送" OnClick="BtnSend_Click" runat="server" /></td>
                                <td>
                                    <asp:Button ID="BtnToDraft" Text="保存到草稿箱" Width="80px" OnClick="BtnToDraft_Click" runat="server" /></td>
                                <td>
                                    <asp:CheckBox ID="CBoxSend" Text="保存到发件箱" runat="server" /></td>
                                <td>
                                    <asp:CheckBox ID="CBoxSMS" Text="手机短信督办" runat="server" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </font>
    </form>
</body>
</html>
