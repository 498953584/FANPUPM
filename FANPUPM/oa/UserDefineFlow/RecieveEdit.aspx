<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecieveEdit.aspx.cs" Inherits="oa_UserDefineFlow_RecieveEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>选择被告知人</title>

    <script language="javascript" type="text/javascript">
    <!--
    window.name = "win";
      function pickPerson(op)
        {
            var p = new Array();
		    p[0] = "";
		    p[1] = "";
		    var url = "";
		    url = "/EPC/WorkFlow/SelectAllUser.aspx";		    
		    window.showModalDialog(url,p,"dialogHeight:456px;dialogWidth:600px;center:1;help:0;status:0;");
		    if (p[0]!=""){
					document.getElementById('hdnAnnouncer').value = p[0];
					document.getElementById('txtAnnouncer').value = p[1];
				}
		 }
    
       
        -->
    </script>

</head>
<body scroll="no">
    <form id="form1" target="win" runat="server">
    <div>
        <table style="height: 100%; width: 100%" cellpadding="0" cellspacing="0"  id="tablex">
          
            <tr style="height:270px">
                <td valign="top" style="width: 100%">
                    <iframe id="frmPage" name="frmPage" src="about:blank" frameborder="1" width="100%" height="270px" runat="server"></iframe>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table id="tableVersion" cellspacing="0" cellpadding="5" width="100%">
                        <tr>
                            <td width="25%" style="text-align: right">
                                被告知人
                            </td>
                            <td>
                            
                               <span class="spanSelect" style="width: 320px">
                                    <asp:TextBox ID="txtAnnouncer" Style="width: 300px; height: 15px; border: none; line-height: 16px;
                                        margin: 1px 2px;" runat="server"></asp:TextBox>
                                    <img alt="选择人员" onclick="pickPerson(2);" src="/images1/iconSelect.gif" />
                                    <input type="hidden" id="hdnAnnouncer" name="hdnAnnouncer" style="width: 10px" runat="server" />

                                </span>
                            
                            
                              
                            </td>
                        </tr>
                        <tr>
                            <td width="25%" style="text-align: right">
                                告知内容
                            </td>
                            <td>
                                <asp:TextBox ID="txtContent" Height="52px" TextMode="MultiLine" Width="90%" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" height="20">
                                <asp:Button ID="btnAdd" Text="保  存" OnClick="btnAdd_Click" runat="server" />&nbsp;
                                <input id="BtnClose" onclick="javascript:returnValue=false;window.close();" type="button"
                                    value="关  闭" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtAnnouncer" Display="None" ErrorMessage="告知人不能为空！" runat="server"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
