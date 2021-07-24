<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConditionSet.aspx.cs" Inherits="ConditionSet" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>节点条件编辑</title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self"></base>
</head>
<body scroll="no" class="body_popup">
		<form id="Form1" method="post" runat="server">
			<TABLE class="table-form" id="TableVersion" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1">
				<TR>
					<TD class="td-head" colSpan="4" height="20">条件设置维护</TD></TR>
			    <tr>
			        <td colspan="2" width="100%" height="30%"><div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
			            <asp:DataGrid ID="gvCondition" AutoGenerateColumns="false" Height="1px" CssClass="grid" Width="534px" OnSelectedIndexChanged="gvCondition_SelectedIndexChanged" OnItemCommand="gvCondition_ItemCommand" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn DataField="NodeID" HeaderText="节点编号"></asp:BoundColumn><asp:BoundColumn DataField="OrderNumber" HeaderText="顺序号"></asp:BoundColumn><asp:BoundColumn DataField="AndOr" HeaderText="条件关系"></asp:BoundColumn><asp:BoundColumn DataField="ConditionField" HeaderText="条件字段"></asp:BoundColumn><asp:BoundColumn DataField="FieldType" HeaderText="字段类型"></asp:BoundColumn><asp:BoundColumn DataField="ConditionType" HeaderText="条件类型"></asp:BoundColumn><asp:BoundColumn DataField="ConditionValue" HeaderText="条件值"></asp:BoundColumn></Columns></asp:DataGrid></div></td>
			    </tr>
				<tr>
					<td class="td-label" width="25%">顺序号</td>
					<td><asp:TextBox ID="txtOrder" CssClass="text-input" Enabled="false" BorderStyle="None" Text="" TabIndex="1" runat="server"></asp:TextBox></td></tr>
                <tr>
                    <td class="td-label" width="25%">条件关系</td>
                    <td><asp:RadioButton ID="RbNo" Text="无" GroupName="group1" Checked="true" TabIndex="2" runat="server" />
                        <asp:RadioButton ID="RbAnd" Text="与" GroupName="group1" TabIndex="3" runat="server" />
                        <asp:RadioButton ID="RbOr" Text="或" GroupName="group1" TabIndex="4" runat="server" />
                        </td>
                </tr>
				<TR>
					<TD class="td-label" width="25%">条件字段</TD>
					<TD><asp:DropDownList ID="ddltCondition" CssClass="text-input" TabIndex="5" Width="138px" OnSelectedIndexChanged="ddltCondition_SelectedIndexChanged" runat="server"><asp:ListItem Value="0" Text="---请选择条件字段---" /></asp:DropDownList>
                        <input id="hdnFieldType" style="width: 12px" type="hidden" visible="true" runat="server" />
</TD></TR>
				<tr>
				    <td class="td-label" width="25%">条件类型</td>
				    <td><asp:DropDownList ID="ddltConditionType" CssClass="text-input" TabIndex="9" Width="138px" runat="server"><asp:ListItem Value="0" Text="---请选择条件类型---" /><asp:ListItem Value="1" Text="小于等于" /><asp:ListItem Value="2" Text="小于" /><asp:ListItem Value="3" Text="等于" /><asp:ListItem Value="4" Text="大于" /><asp:ListItem Value="5" Text="大于等于" /></asp:DropDownList></td>
				</tr>
				<TR>
					<TD class="td-label" width="25%">条件值</TD>
					<TD><asp:TextBox ID="txtConditionValue" CssClass="text-input" TabIndex="10" runat="server"></asp:TextBox>
					    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtConditionValue" ErrorMessage="数据类型不正确！" ValidationExpression="^[0-9]*[1-9][0-9]*$|^\-[1-9][0-9]*$" runat="server"></asp:RegularExpressionValidator></TD></TR>
				<TR>
				    <TD class="td-submit" colSpan="4" height="20"><asp:Button ID="BtnNew" Text="新  增" OnClick="BtnNew_Click" runat="server" />&nbsp;
					<asp:Button ID="BtnAdd" Text="保  存" OnClick="BtnAdd_Click" runat="server" />&nbsp;
						<INPUT id="BtnClose" onclick="javascript:window.returnValue=false;window.close();" type="button" value="关  闭" name="BtnClose">
					</TD></TR></TABLE><JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl></form>

	</body>
</html>
