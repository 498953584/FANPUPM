<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TemplateEdit.aspx.cs" Inherits="TemplateEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title>会议模板登记维护</title>
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
                    会议模板登记</TD>
            </TR>
            <TR>
	            <TD class="td-label" width="25%">模板名称</TD>
	            <TD><asp:TextBox ID="txtTemplateName" Width="80%" CssClass="text-input" TabIndex="1" runat="server"></asp:TextBox>
	            <input id="hdnTemplateName" style="WIDTH: 10px" type="hidden" name="hdnTemplateName" runat="server" />

	            </TD>
            </TR>
            <tr>
	            <TD class="td-label" width="25%">备注</TD>
	            <TD><asp:TextBox ID="txtContent" CssClass="text-input" TabIndex="7" Width="80%" TextMode="MultiLine" runat="server"></asp:TextBox></TD>
            </TR>
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
