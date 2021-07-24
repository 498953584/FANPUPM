<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubsectionEdit.aspx.cs" Inherits="SubsectionEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title>会议分段信息维护</title>
	<script language="javascript">
	window.name = "win" ;
	<!--
	function pickAttendMan()
	{
	    var p = new Array();
		p[0] = "";
		p[1] = "";
		var url = "";
		url = "../../CommonWindow/consignee.aspx";
        window.showModalDialog(url,p,"dialogHeight:422px;dialogWidth:659px;center:1;help:0;status:0;");
		if (p[0]!="")
		{
			document.getElementById('hdnAttendManName').value = p[0];
			document.getElementById('txtAttendManName').value = p[1];
			document.getElementById('hdnName').value = p[1];
			var str = document.getElementById('txtAttendManName').value;
			var count = getManNumber(str);
			document.getElementById('txtNumber').value = count;
			document.getElementById('hdnNumber').value = count;
		}
	}
	function getManNumber(value)
	{
        var count = 0 ;
		var str = trim(value);
	    var index = str.indexOf(,);
	    while (index > 0 ) 
	    {
	        count++;
	        str = str.substr(index+1);
	        index = str.indexOf(,);
	    }    
	    return count+1;
	}
	function trim(value)
    {   
        var res = String(value).replace(/^[\s]+|[\s]+$/g,'');   
        return res;   
    }
    -->
	</script>
</head>
<body scroll="no" class="body_popup">
    <form id="form1" target="win" runat="server">
    <div>
        <TABLE class="table-form" id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="1">
            <TR>
	            <TD class="td-head" colSpan="2" height="20">
                    会议分段信息</TD>
            </TR>
            <TR>
	            <TD class="td-label" width="25%">开始时间</TD>
	            <TD><asp:DropDownList ID="ddltCallTime" TabIndex="4" runat="server"><asp:ListItem Value="8" Selected="true" Text="8时" /><asp:ListItem Value="9" Text="9时" /><asp:ListItem Value="10" Text="10时" /><asp:ListItem Value="11" Text="11时" /><asp:ListItem Value="12" Text="12时" /><asp:ListItem Value="13" Text="13时" /><asp:ListItem Value="14" Text="14时" /><asp:ListItem Value="15" Text="15时" /><asp:ListItem Value="16" Text="16时" /><asp:ListItem Value="17" Text="17时" /><asp:ListItem Value="18" Text="18时" /></asp:DropDownList>
                    <asp:DropDownList ID="ddltCallMinute" TabIndex="5" runat="server"><asp:ListItem Selected="true" Value="0" Text="0分" /><asp:ListItem Value="15" Text="15分" /><asp:ListItem Value="30" Text="30分" /><asp:ListItem Value="45" Text="45分" /></asp:DropDownList>
                </TD>
            </TR>
            <TR>
	            <TD class="td-label" width="25%">会议议题</TD>
	            <TD><asp:TextBox ID="txtTopic" Width="80%" CssClass="text-input" TabIndex="1" MaxLength="100" runat="server"></asp:TextBox>
	            </TD>
            </TR>
            <TR>
	            <TD class="td-label" width="25%">参会人</TD>
	            <TD><asp:TextBox ID="txtAttendManName" Width="80%" Enabled="false" CssClass="text-input" TabIndex="2" runat="server"></asp:TextBox>
	            <img id="img1" src="../../images/contact.gif" align="bottom" onclick="pickAttendMan();" runat="server" />

	            <input id="hdnAttendManName" style="WIDTH: 10px" type="hidden" name="hdnAttendManName" runat="server" />

	            <input id="hdnName" style="WIDTH: 10px" type="hidden" name="hdnName" runat="server" />

	            </TD>
            </TR>
            <TR>
	            <TD class="td-label" width="25%">参会人数</TD>
	            <TD><asp:TextBox ID="txtNumber" Width="80%" Enabled="false" CssClass="text-input" TabIndex="3" MaxLength="4" runat="server"></asp:TextBox>
	            <input id="hdnNumber" style="WIDTH: 10px" type="hidden" name="hdnNumber" runat="server" />

	             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="^[1-9][0-9]*$" ControlToValidate="txtNumber" ErrorMessage="输入错误！" runat="server"></asp:RegularExpressionValidator>
	            </TD>
            </TR>
            <TR>
	            <TD class="td-label" width="25%"></TD>
	            <TD><asp:CheckBox ID="ckbIsSms" Text="是否短信发送" TabIndex="4" runat="server" /></TD>
            </TR>
            <tr>
	            <TD class="td-label" width="25%">备注</TD>
	            <TD><asp:TextBox ID="txtContent" CssClass="text-input" TabIndex="5" Width="80%" TextMode="MultiLine" MaxLength="100" runat="server"></asp:TextBox></TD>
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
