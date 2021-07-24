<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewApply.aspx.cs" Inherits="ViewApply" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>会议室安排</title>
    <script language="javascript">
    window.name = "win";
    <!--
    
    -->
    </script>
</head>
<body class="body_popup" scroll="no">
    <form id="form1" target="win" runat="server">
        <TABLE class="table-form" id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="1">
            <TR>
	            <TD class="td-head" colSpan="2" height="20">
                    会议室申请信息</TD>
            </TR>
            <TR>
                <TR>
                    <TD class="td-label" width="25%">主题</TD>
                    <td><asp:TextBox ID="txtTitle" CssClass="text-input" Enabled="false" TabIndex="3" runat="server"></asp:TextBox></td>
                </TR>
	            <TD class="td-label" width="25%">
                    所用会议室</TD>
	            <TD><asp:TextBox ID="txtMeetingRoom" CssClass="text-input" Enabled="false" TabIndex="1" runat="server"></asp:TextBox>
	            <input id="btnView" type="button" class="button-normal" value="查看会议室状态" onclick="javascript:window.showModalDialog('ViewBoardroomState.aspx',window,'dialogHeight:630px;dialogWidth:730px;center:1;help:0;status:0;');" name="btnView" style="width: 100px" runat="server" />

	            <img id="img1" src="../../images/contact.gif" align="bottom" style="display :none" runat="server" />
<font color="#ff0000">&nbsp;</font> 
	            <asp:HiddenField ID="hdnMeetinRoomID" runat="server" />
	            </TD>
	        </TR>
	        <tr>
	            <TD class="td-label" width="25%">召开日期</TD>
	            <td><JWControl:DateBox ID="dbUserDate" Enabled="false" runat="server"></JWControl:DateBox></td>
            </TR> 
            <TR>
	            <TD class="td-label" width="25%" style="height: 25px">开始时间</TD>
	            <TD style="height: 25px">
                    <asp:DropDownList ID="ddltBeginHour" TabIndex="4" Enabled="false" runat="server"><asp:ListItem Value="8" Selected="true" Text="8时" /><asp:ListItem Value="9" Text="9时" /><asp:ListItem Value="10" Text="10时" /><asp:ListItem Value="11" Text="11时" /><asp:ListItem Value="12" Text="12时" /><asp:ListItem Value="13" Text="13时" /><asp:ListItem Value="14" Text="14时" /><asp:ListItem Value="15" Text="15时" /><asp:ListItem Value="16" Text="16时" /><asp:ListItem Value="17" Text="17时" /><asp:ListItem Value="18" Text="18时" /></asp:DropDownList>
                    <asp:DropDownList ID="ddltBeginMinute" TabIndex="5" Enabled="false" runat="server"><asp:ListItem Selected="true" Value="0" Text="0分" /><asp:ListItem Value="15" Text="15分" /><asp:ListItem Value="30" Text="30分" /><asp:ListItem Value="45" Text="45分" /></asp:DropDownList></TD>
             </TR>
	        <tr>
	            <TD class="td-label" width="25%" style="height: 25px">结束时间</TD>
	            <td style="height: 25px"><asp:DropDownList ID="ddltEndHour" TabIndex="6" Enabled="false" runat="server"><asp:ListItem Selected="true" Value="8" Text="8时" /><asp:ListItem Value="9" Text="9时" /><asp:ListItem Value="10" Text="10时" /><asp:ListItem Value="11" Text="11时" /><asp:ListItem Value="12" Text="12时" /><asp:ListItem Value="13" Text="13时" /><asp:ListItem Value="14" Text="14时" /><asp:ListItem Value="15" Text="15时" /><asp:ListItem Value="16" Text="16时" /><asp:ListItem Value="17" Text="17时" /><asp:ListItem Value="18" Text="18时" /></asp:DropDownList>
                    <asp:DropDownList ID="ddltEndMinute" TabIndex="7" Enabled="false" runat="server"><asp:ListItem Selected="true" Value="0" Text="0分" /><asp:ListItem Value="15" Text="15分" /><asp:ListItem Value="30" Text="30分" /><asp:ListItem Value="45" Text="45分" /></asp:DropDownList></td>
            </TR>
            <tr>
	            <TD class="td-label" width="25%">参与人数</TD>
	            <td><asp:TextBox ID="txtHumanNumber" CssClass="text-input" Enabled="false" TabIndex="2" runat="server"></asp:TextBox>
	            <input id="hdnHumanNumber" style="WIDTH: 10px" type="hidden" name="hdnHumanNumber" runat="server" />
</td>
            </TR>
            <TR>
	            <TD class="td-label" width="25%">备注</TD>
	            <TD><asp:TextBox ID="txtContent" CssClass="text-input" TabIndex="8" Width="80%" TextMode="MultiLine" Enabled="false" runat="server"></asp:TextBox><font color="#ff0000">&nbsp;</font> </TD>
            </TR>
            <TR>
                <TD colspan="2" class="td-submit" height="20"><asp:Button ID="BtnSave" Text="确  定" OnClick="BtnSave_Click" runat="server" />&nbsp;
                    <INPUT id="BtnClose" onclick="javascript:window.returnValue='0';window.close();" type="button" value="关  闭" name="BtnClose">
                    <asp:HiddenField ID="hdnRecordId" runat="server" /><asp:HiddenField ID="hdnEquipmentID" runat="server" />
                </TD>
            </TR>
        </TABLE>
        <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
