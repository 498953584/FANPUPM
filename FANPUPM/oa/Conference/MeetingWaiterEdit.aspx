<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MeetingWaiterEdit.aspx.cs" Inherits="MeetingWaiterEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title>服务人员维护</title>
	<script language="javascript">
	window.name = "win";
	<!--
	function pickWaiter()
	{
	    var p = new Array();
		p[0] = "";
		p[1] = "";
		var url = "";
		url = "../../CommonWindow/consignee.aspx";
        window.showModalDialog(url,p,"dialogHeight:422px;dialogWidth:659px;center:1;help:0;status:0;");
		if (p[0]!="")
		{
			document.getElementById('hdnUserCode').value = p[0];
			document.getElementById('txtUserName').value = p[1];
			document.getElementById('hdnUserName').value = p[1];
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
                    服务人员登记</TD>
            </TR>
            <TR>
	            <TD class="td-label" width="25%">姓名</TD>
	            <TD><asp:TextBox ID="txtUserName" Width="80%" Enabled="false" CssClass="text-input" TabIndex="2" runat="server"></asp:TextBox>
	            <img id="img1" src="../../images/contact.gif" align="bottom" onclick="pickWaiter();" runat="server" />

	            <input id="hdnUserCode" style="WIDTH: 10px" type="hidden" name="hdnUserCode" runat="server" />

	            <input id="hdnUserName" style="WIDTH: 10px" type="hidden" name="hdnUserName" runat="server" />

	            </TD>
            </TR>
            <tr>
	            <TD class="td-label" width="25%">服务事项</TD>
	            <TD><asp:TextBox ID="txtContent" CssClass="text-input" TabIndex="5" Width="80%" MaxLength="100" runat="server"></asp:TextBox></TD>
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
