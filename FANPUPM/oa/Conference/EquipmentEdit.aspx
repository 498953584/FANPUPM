<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquipmentEdit.aspx.cs" Inherits="EquipmentEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title>会议设备维护</title>
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
                    会议设备登记</TD>
            </TR>
            <TR>
	            <TD class="td-label" width="20%">设备名称</TD>
	            <TD><asp:TextBox ID="txtEquipmentName" Width="80%" CssClass="text-input" TabIndex="2" MaxLength="50" runat="server"></asp:TextBox>
	            </TD>
            </TR>
            <tr>
	            <TD class="td-label" width="20%">设备数量</TD>
	            <TD><asp:TextBox ID="txtNumber" CssClass="text-input" TabIndex="5" Width="80%" MaxLength="4" runat="server"></asp:TextBox>
	            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtNumber" ErrorMessage="输入错误!" ValidationExpression="^([1-9][0-9]*)$" runat="server"></asp:RegularExpressionValidator></TD>
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
