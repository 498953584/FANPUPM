<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BoardroomEdit.aspx.cs" Inherits="BoardroomEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title>会议室登记维护</title>
	<script language ="javascript">
	window.name = "win";
	<!--
	function ClickRow(FlowGuid)
	{
		document.getElementById('hdnRecordID').value = FlowGuid;
		document.getElementById('btnEdit').disabled = false;
		document.getElementById('btnDel').disabled = false;	
	}
	function pickPerson()
    {
        var p = new Array();
	    p[0] = "";
	    p[1] = "";
	    var url = "";
	    url = "../../CommonWindow/PickSinglePerson.aspx";
	    window.showModalDialog(url,p,"dialogHeight:420px;dialogWidth:430px;center:1;help:0;status:0;");
	    if (p[0]!="")
	    {
	        document.getElementById('hdnUserCode').value = p[0];
		    document.getElementById('txtUserName').value = p[1];
	    }
    }
    function funChkInt()
	{
		if (event.keyCode < 48 || event.keyCode > 57 )
			event.returnValue = false;
	}
	function clickTenderRow(index)
	{
		document.getElementById('HdnSelectedIndex').value = index;
		document.getElementById('BtnDel').disabled = false;
		document.getElementById('BtnEdit').disabled = false;
	}
	-->
	</script>
</head>
<body scroll="no" class="body_popup">
    <form id="Form1" target="win" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <div>
        <table width="100%" height="100%" border="0"  cellSpacing="0" cellPadding="0"  class="table-form" id="Table1">
            <tr height="20%"><td>
                <TABLE class="table-form" id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="1">
		            <TR>
			            <TD class="td-head" colSpan="4" height="20">
                            会议室登记</TD>
		            </TR>
		            <TR>
			            <TD class="td-label" width="15%">
                            会议室名称</TD>
			            <TD width="35%"><asp:TextBox ID="txtMeetingRoom" CssClass="text-input" TabIndex="1" MaxLength="50" runat="server"></asp:TextBox><font color="#ff0000">&nbsp;</font> 
			            <input id="hdnTitle" style="WIDTH: 10px" type="hidden" name="hdnTitle" runat="server" />

			            </TD>
			            <TD class="td-label" width="15%">所属单位</TD>
			            <td><asp:TextBox ID="txtCorpCode" CssClass="text-input" Enabled="false" TabIndex="2" runat="server"></asp:TextBox>
			            <input id="hdnCorpCode" style="WIDTH: 10px" type="hidden" name="hdnCorpCode" runat="server" />
</td>
		            </TR>
		            <TR>
		                <TD class="td-label" width="15%">位置</TD>
		                <td width="35%"><asp:TextBox ID="txtLocation" CssClass="text-input" TabIndex="3" MaxLength="100" runat="server"></asp:TextBox></td>
			            <TD class="td-label" width="15%">容纳人数</TD>
			            <td><asp:TextBox ID="txtHumanNumber" CssClass="text-input" TabIndex="4" MaxLength="4" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtHumanNumber" ErrorMessage="输入错误！" ValidationExpression="^([1-9][0-9]*)$" Width="80px" runat="server"></asp:RegularExpressionValidator></td>
		            </TR> 
		            <TR>
			            <TD class="td-label" width="15%" style="height: 25px">管理员</TD>
			            <TD width="35%" style="height: 25px"><asp:TextBox ID="txtUserName" Enabled="false" CssClass="text-input" TabIndex="5" runat="server"></asp:TextBox>
			            <img id="imgPick1" src="../../images/contact.gif" align="bottom" onclick="pickPerson();" runat="server" />
<font color="#ff0000">&nbsp;</font> 
			            <input id="hdnUserCode" style="WIDTH: 10px" type="hidden" name="hdnUserCode" runat="server" />

			            </TD>
			            <TD class="td-label" width="15%" style="height: 25px">联系方式</TD>
			            <td style="height: 25px"><asp:TextBox ID="txtRelationMode" CssClass="text-input" TabIndex="6" MaxLength="50" runat="server"></asp:TextBox></td>
		            </TR>
		            <TR>
			            <TD class="td-label" width="15%">备注</TD>
			            <TD colspan="3"><asp:TextBox ID="txtContent" CssClass="text-input" TabIndex="7" Width="80%" TextMode="MultiLine" MaxLength="100" runat="server"></asp:TextBox><font color="#ff0000">&nbsp;</font> </TD>
		            </TR>
	            </TABLE>
	            </td></tr>
            <tr><td valign="top">
               
               <TABLE class="table-normal" id="Tablew" cellSpacing="0" cellPadding="0" width="100%" height="100%" border="0">
	                <TR>
		                <TD class="td-toolsbar" colspan="2" style="height: 24px"><input id="HdnSelectedIndex" style="WIDTH: 10px" type="hidden" name="HdnSelectedIndex" runat="server" />

			                <asp:Button ID="BtnAdd" Text="新 增" CommandName="Add" OnClick="BtnAdd_Click" runat="server" />&nbsp;
			                <asp:Button ID="BtnEdit" Text="编  辑" Enabled="false" OnClick="BtnEdit_Click" runat="server" />&nbsp;
			                <asp:Button ID="BtnDel" Text="删  除" Enabled="false" OnClick="BtnDel_Click" runat="server" />&nbsp;
		                </TD>
	                </TR>
	                <TR>
		                <TD colSpan="2" valign="top">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                           <asp:DataGrid ID="dgEquipment" AutoGenerateColumns="false" CssClass="grid" Width="100%" OnItemDataBound="dgEquipment_ItemDataBound" runat="server"><AlternatingItemStyle CssClass="grid_row"></AlternatingItemStyle><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
                                            <%# Container.ItemIndex + 1 %>
                                        </ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="设备名称">
<ItemTemplate>
								            <asp:Label ID="lbEquipmentName" Text='<%# Convert.ToString(Eval( "EquipmentName")) %>' runat="server"></asp:Label>
							            </ItemTemplate>

<EditItemTemplate>
								            <asp:TextBox ID="txtEquipmentName" Width="100%" Text='<%# Convert.ToString(Eval( "EquipmentName")) %>' runat="server"></asp:TextBox>
							            </EditItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="型号">
<ItemTemplate>
								            <asp:Label ID="lbModel" Text='<%# Convert.ToString(Eval( "Model")) %>' runat="server"></asp:Label>
							            </ItemTemplate>

<EditItemTemplate>
								            <asp:TextBox ID="txtModel" Width="100%" Text='<%# Convert.ToString(Eval( "Model")) %>' runat="server"></asp:TextBox>
							            </EditItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="数量">
<ItemTemplate>
								            <asp:Label ID="lbNumber" Text='<%# Convert.ToString(Eval( "Number")) %>' runat="server"></asp:Label>
							            </ItemTemplate>

<EditItemTemplate>
								            <asp:TextBox ID="txtNumber" Width="100%" onKeyPress="funChkInt();" Text='<%# Convert.ToString(Eval( "Number")) %>' runat="server"></asp:TextBox>
							            </EditItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="备注">
<ItemTemplate>
								            <asp:Label ID="lbContent" Text='<%# Convert.ToString(Eval( "Content")) %>' runat="server"></asp:Label>
							            </ItemTemplate>

<EditItemTemplate>
								            <asp:TextBox ID="txtContent" Width="100%" Text='<%# Convert.ToString(Eval( "Content")) %>' runat="server"></asp:TextBox>
							            </EditItemTemplate>
</asp:TemplateColumn></Columns></asp:DataGrid>
                                </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="__Page" EventName="Load" runat="server" /><asp:AsyncPostBackTrigger ControlID="BtnAdd" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="BtnDel" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="BtnEdit" EventName="Click" runat="server" /></Triggers></asp:UpdatePanel>
				        </TD>
	                    </TR>
                    </TABLE>
            </td></tr>
            <TR>
	            <TD class="td-submit" height="20"><asp:Button ID="BtnSave" Text="保  存" OnClick="BtnSave_Click" runat="server" />&nbsp;
		            <INPUT id="BtnClose" onclick="javascript:window.close();" type="button" value="关  闭" name="BtnClose">
	            </TD>
            </TR>
        </table>
        <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </div>   
    </form>
</body>
</html>
