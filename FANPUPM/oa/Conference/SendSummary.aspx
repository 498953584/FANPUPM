<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendSummary.aspx.cs" Inherits="SendSummary" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title>会议纪要上传</title>
	<script language="javascript">
	window.name = "win";
	<!--
	
    -->
	</script>
</head>
<body scroll="no" class="body_popup">
    <form id="form1" target="win" runat="server">
    <div>
        <TABLE class="table-form" id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="1">
            <TR>
	            <TD class="td-head" colSpan="2" height="20">
                    会议纪要上传</TD>
            </TR>
            <TR>
	            <TD class="td-label" width="25%">纪要名称</TD>
	            <TD><asp:TextBox ID="txtSummary" Width="80%" CssClass="text-input" TabIndex="1" MaxLength="100" runat="server"></asp:TextBox>
	            </TD>
            </TR>
            <tr id="tr_add" runat="server"><td class="td-label" width="25%" runat="server">附件</td><td runat="server"><asp:FileUpload ID="txtFilePath" runat="server" /><font color="#ff0000">&nbsp;</font></td></tr>
            <tr id="tr_edit" runat="server"><td class="td-label" width="25%" runat="server">附件</td><td runat="server"><a id="annexName" href="#" onclick="annexEdit();" style="cursor : hand" runat="server" />

			    <asp:TextBox ID="txtOriginalName" CssClass="text-input" TabIndex="4" Enabled="false" Visible="false" runat="server"></asp:TextBox>
			    <asp:ImageButton ID="ImageBtn" ImageUrl="~/images/del.gif" OnClick="ImageBtn_Click" runat="server" /><font color="#ff0000">&nbsp;</font>
			    <input id="hdnFilePath" style="WIDTH: 10px" type="hidden" name="hdnFilePath" runat="server" />
</td></tr>
            <TR>
	            <TD class="td-submit" colspan="2" height="20"><asp:Button ID="BtnSave" Text="保  存" OnClick="BtnSave_Click" runat="server" />&nbsp;
		            <INPUT id="BtnClose" onclick="javascript:window.close();" type="button" value="关  闭" name="BtnClose">
	            </TD>
            </TR>
        </TABLE>
        <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </div>
    </form>
</body>
</html>
