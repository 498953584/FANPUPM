<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ArrangeEdit.aspx.cs" Inherits="ArrangeEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title>会议信息维护</title>
	<script language="javascript">
	window.name = "win";
	<!--
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
	        document.getElementById('hdnMeetingRoom').value = p[1];
		    document.getElementById('txtMeetingPlace').value = p[1];
	    }
	}
	-->
	</script>
</head>
<body scroll="no" class="body_popup">
    <form id="form1" target="win" runat="server">
    <div>
        <TABLE class="table-form" id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="1">
            <TR>
	            <td id="pageTile" class="td-head" colspan="2" height="20" runat="server">
                    会议信息登记</td>
            </TR>
            <TR>
	            <TD class="td-label" width="25%">会议名称</TD>
	            <TD><asp:TextBox ID="txtMeetingTitle" Width="80%" CssClass="text-input" TabIndex="1" MaxLength="100" runat="server"></asp:TextBox>
	                <input id="hdnMeetingTitle" style="WIDTH: 10px" type="hidden" name="hdnMeetingTitle" runat="server" />

	            </TD>
            </TR>
            <TR>
	            <TD class="td-label" width="25%">会议地点</TD>
	            <TD><asp:TextBox ID="txtMeetingPlace" Width="80%" CssClass="text-input" TabIndex="2" runat="server"></asp:TextBox>
	            <img id="img2" src="../../images/contact.gif" align="bottom" onclick="pickMeetingRoom();" runat="server" />

	            <input id="hdnMeetingRoom" style="WIDTH: 10px" type="hidden" name="hdnMeetingRoom" runat="server" />

	            </TD>
            </TR>
            <TR>
	            <TD class="td-label" width="25%">召开日期</TD>
	            <TD><JWControl:DateBox ID="txtCallDate" Width="80%" TabIndex="3" runat="server"></JWControl:DateBox>
	            </TD>
            </TR>
            <TR>
	            <TD class="td-label" width="25%">开始时间</TD>
	            <TD><asp:DropDownList ID="ddltCallTime" TabIndex="4" runat="server"><asp:ListItem Value="8" Selected="true" Text="8时" /><asp:ListItem Value="9" Text="9时" /><asp:ListItem Value="10" Text="10时" /><asp:ListItem Value="11" Text="11时" /><asp:ListItem Value="12" Text="12时" /><asp:ListItem Value="13" Text="13时" /><asp:ListItem Value="14" Text="14时" /><asp:ListItem Value="15" Text="15时" /><asp:ListItem Value="16" Text="16时" /><asp:ListItem Value="17" Text="17时" /><asp:ListItem Value="18" Text="18时" /></asp:DropDownList>
                    <asp:DropDownList ID="ddltCallMinute" TabIndex="5" runat="server"><asp:ListItem Selected="true" Value="0" Text="0分" /><asp:ListItem Value="15" Text="15分" /><asp:ListItem Value="30" Text="30分" /><asp:ListItem Value="45" Text="45分" /></asp:DropDownList>
                </TD>
            </TR>
            <tr>
	            <TD class="td-label" width="25%">备注</TD>
	            <TD><asp:TextBox ID="txtContent" CssClass="text-input" TabIndex="6" Width="80%" TextMode="MultiLine" MaxLength="250" runat="server"></asp:TextBox></TD>
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
