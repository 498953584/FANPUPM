<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyEdit.aspx.cs" Inherits="ApplyEdit" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"><title>会议室申请信息</title>
	<script language="javascript">
	window.name = "win";
	<!--
	function ClickRow(ApplyId)
	{
//		document.getElementById('hdnRecordId').value = ApplyId;
	}
	function pickMeetingRoom()
	{
	    var p = new Array();
	    p[0] = "";
	    p[1] = "";
	    var url = "";
	    url = "MeetingRoomSelect.aspx";
	    window.showModalDialog(url,p,"dialogHeight:430px;dialogWidth:550px;center:1;help:0;status:0;");
	    if (p[0]!="")
	    {
	        document.getElementById('hdnMeetinRoomID').value = p[0];
		    document.getElementById('txtMeetingRoom').value = p[1];
	    }
	}
	function pickEquipment()
	{
	    var url = "";
	    var meetingroomId = document.getElementById('hdnMeetinRoomID').value;
	    var applyId = document.getElementById('hdnRecordId').value;
	    url = "pickEquipment.aspx?mid=" + meetingroomId + "&aid=" + applyId;
	    var rtn = window.showModalDialog(url,window,"dialogHeight:400px;dialogWidth:500px;center:1;help:0;status:0;");
	    if (rtn == undefined)
	    {
	    }
	    else
	    {
            if (rtn != "0")
            {
                document.getElementById("hdnEquipmentID").value = rtn;
//              document.getElementById("btnRefresh").click();
            }
         }
	}
	//选择人
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
	-->
	</script>
</head>
<body class="body_popup" scroll="no">
    <form id="form1" target="win" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
        <table width="100%" height="100%" border="0"  cellSpacing="0" cellPadding="0"  class="table-form" id="Table1">
            <tr height="20%"><td>
                <table class="table-form" id="Table2" cellspacing="0" cellpadding="0" width="100%" border="1">
		            <tr>
			            <td class="td-head" colSpan="4" height="20">
                            会议室申请信息</TD>
		            </tr>
		            <tr>
			            <td class="td-label" width="15%">
                            会议室名称</TD>
			            <td width="35%"><asp:TextBox ID="txtMeetingRoom" CssClass="text-input" TabIndex="1" MaxLength="100" runat="server"></asp:TextBox>
			            <img id="img1" src="../../images/contact.gif" align="bottom" onclick="pickMeetingRoom();" runat="server" />
<font color="#ff0000">&nbsp;</font> 
			            <asp:HiddenField ID="hdnMeetinRoomID" runat="server" />
			            </td>
			            <td class="td-label" width="15%">参与人数</td>
			            <td><asp:TextBox ID="txtHumanNumber" CssClass="text-input" TabIndex="2" MaxLength="4" runat="server"></asp:TextBox>
			            <input id="hdnHumanNumber" style="WIDTH: 10px" type="hidden" name="hdnHumanNumber" runat="server" />

                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="^[1-9][0-9]*$" ControlToValidate="txtHumanNumber" ErrorMessage="输入错误！" runat="server"></asp:RegularExpressionValidator></td>
		            </tr>
		            <tr>
		                <td class="td-label" width="15%">主题</td>
		                <td width="35%"><asp:TextBox ID="txtTitle" CssClass="text-input" TabIndex="3" MaxLength="100" runat="server"></asp:TextBox></td>
			            <td class="td-label" width="15%">召开日期</td>
			            <td><JWControl:DateBox ID="dbUserDate" runat="server"></JWControl:DateBox></td>
		            </tr> 
		            <tr>
			            <td class="td-label" width="15%" style="height: 25px">开始时间</td>
			            <td width="35%" style="height: 25px">
                            <asp:DropDownList ID="ddltBeginHour" TabIndex="4" runat="server"><asp:ListItem Value="8" Selected="true" Text="8时" /><asp:ListItem Value="9" Text="9时" /><asp:ListItem Value="10" Text="10时" /><asp:ListItem Value="11" Text="11时" /><asp:ListItem Value="12" Text="12时" /><asp:ListItem Value="13" Text="13时" /><asp:ListItem Value="14" Text="14时" /><asp:ListItem Value="15" Text="15时" /><asp:ListItem Value="16" Text="16时" /><asp:ListItem Value="17" Text="17时" /><asp:ListItem Value="18" Text="18时" /></asp:DropDownList>
                            <asp:DropDownList ID="ddltBeginMinute" TabIndex="5" runat="server"><asp:ListItem Selected="true" Value="0" Text="0分" /><asp:ListItem Value="15" Text="15分" /><asp:ListItem Value="30" Text="30分" /><asp:ListItem Value="45" Text="45分" /></asp:DropDownList></TD>
			            <td class="td-label" width="15%" style="height: 25px">结束时间</td>
			            <td style="height: 25px"><asp:DropDownList ID="ddltEndHour" TabIndex="6" runat="server"><asp:ListItem Selected="true" Value="8" Text="8时" /><asp:ListItem Value="9" Text="9时" /><asp:ListItem Value="10" Text="10时" /><asp:ListItem Value="11" Text="11时" /><asp:ListItem Value="12" Text="12时" /><asp:ListItem Value="13" Text="13时" /><asp:ListItem Value="14" Text="14时" /><asp:ListItem Value="15" Text="15时" /><asp:ListItem Value="16" Text="16时" /><asp:ListItem Value="17" Text="17时" /><asp:ListItem Value="18" Text="18时" /></asp:DropDownList>
                            <asp:DropDownList ID="ddltEndMinute" TabIndex="7" runat="server"><asp:ListItem Selected="true" Value="0" Text="0分" /><asp:ListItem Value="15" Text="15分" /><asp:ListItem Value="30" Text="30分" /><asp:ListItem Value="45" Text="45分" /></asp:DropDownList></td>
		            </TR>
		              <tr>
			            <td class="td-label" width="15%" style="height: 25px">申请人</td>
			            <td width="35%" style="height: 25px" colspan="3"><asp:TextBox ID="txtUserName" Enabled="false" CssClass="text-input" TabIndex="5" runat="server"></asp:TextBox>
			            <img id="imgPick1" src="../../images/contact.gif" align="bottom" onclick="pickPerson();" runat="server" />
<font color="#ff0000">&nbsp;</font> 
			            <input id="hdnUserCode" style="WIDTH: 10px" type="hidden" name="hdnUserCode" runat="server" />

			            </td>
			           
		            </tr>
		            <tr>
			            <td class="td-label" width="15%"  tabindex = "8">备注</td>
			            <td colspan="3"><asp:TextBox ID="txtContent" CssClass="text-input" TabIndex="7" Width="80%" TextMode="MultiLine" MaxLength="100" runat="server"></asp:TextBox><font color="#ff0000">&nbsp;</font> </td>
		            </tr>
	            </table>
	            </td></tr>
            <tr><td valign="top">
               
               <table class="table-normal" id="Tablew" cellSpacing="0" cellPadding="0" width="100%" height="100%" border="0">
	                <TR>
		                <TD class="td-toolsbar" colspan="2" style="height: 24px"><input id="HdnSelectedIndex" style="WIDTH: 10px" type="hidden" name="HdnSelectedIndex" runat="server" />

			                <asp:Button ID="BtnSelect" Text="选择设备" OnClick="BtnSelect_Click" runat="server" />&nbsp;
		                </TD>
	                </TR>
	                <TR>
		                <TD colSpan="2" valign="top">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                                <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                        <asp:GridView ID="gvEquipment" AllowPaging="true" AutoGenerateColumns="false" DataSourceID="SqlEquipment" CssClass="grid" Width="100%" OnRowDataBound="gvEquipment_RowDataBound" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
                            <table class="grid" cellspacing="0" rules="all" border="1" id="gvEquipment" style="border-collapse:collapse;">
		                        <tr class="grid_head">
			                        <th scope="col" style="width:10%;">序号</th><th scope="col">设备名称</th><th scope="col" style="width:20%;">型号</th><th scope="col" style="width:15%;">数量</th><th scope="col" style="width:25%;">备注</th>
		                        </tr>
	                        </table>
                        </EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="设备名称">
<ItemTemplate>
                                    <asp:Label ID="lbEquipmentName" Text='<%# Convert.ToString(Eval("EquipmentName")) %>' runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="型号">
<ItemTemplate>
                                    <asp:Label ID="lbModel" Text='<%# Convert.ToString(Eval("Model")) %>' runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="数量">
<ItemTemplate>
                                    <asp:Label ID="lbNumber" Text='<%# Convert.ToString(Eval("Number")) %>' runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注">
<ItemTemplate>
                                    <asp:Label ID="lbContent" Text='<%# Convert.ToString(Eval("Content")) %>' runat="server"></asp:Label>
                                </ItemTemplate>
</asp:TemplateField></Columns><PagerStyle HorizontalAlign="Right"></PagerStyle></asp:GridView>
                    </div>
                    <asp:SqlDataSource ID="SqlEquipment" ConnectionString='<%$ ConnectionStrings:Sql %>' SelectCommand='<%# Convert.ToString(base.GetSelectCommandText()) %>' runat="server"><SelectParameters><asp:ControlParameter ControlID="hdnRecordId" DefaultValue="0" Name="ApplyRecordID" PropertyName="Value" Type="Int32" runat="server" /></SelectParameters></asp:SqlDataSource>   
                                </ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="BtnSelect" EventName="Click" runat="server" /><asp:AsyncPostBackTrigger ControlID="btnRefresh" EventName="Click" runat="server" /></Triggers></asp:UpdatePanel>
				        </TD>
	                    </TR>
                    </TABLE>
            </td></tr>
            <TR>
	            <TD class="td-submit" height="20"><asp:Button ID="BtnSave" Text="保  存" OnClick="BtnSave_Click" runat="server" />&nbsp;
		            <INPUT id="BtnClose" onclick="javascript:window.returnValue='0';window.close();" type="button" value="关  闭" name="BtnClose">
		            <asp:Button ID="btnRefresh" Text="刷 新" style="display : none" OnClick="btnRefresh_Click" runat="server" />
		            <asp:HiddenField ID="hdnRecordId" runat="server" /><asp:HiddenField ID="hdnEquipmentID" runat="server" />
	            </TD>
            </TR>
        </table>
        <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
