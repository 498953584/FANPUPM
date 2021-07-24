<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuditEdit.aspx.cs" Inherits="AuditEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>审核</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self"></base>
		<script language ="javascript">
        function pickPerson(op)
        {
            var p = new Array();
		    p[0] = "";
		    p[1] = "";
		    var url = "";
		    url = "../../CommonWindow/PickSinglePerson.aspx";
		    window.showModalDialog(url,p,"dialogHeight:420px;dialogWidth:430px;center:1;help:0;status:0;");
		    if (p[0]!="")
		    {
		        if (op == 1)
		        {
			        document.getElementById('hdnFrontPerson').value = p[0];
			        document.getElementById('txtFrontPerson').value = p[1];
			    }
			    else if(op == 2)
			    {
			        document.getElementById('hdnAnnouncer').value = p[0];
			        document.getElementById('txtAnnouncer').value = p[1];
			    }
			    else
			    {
			        document.getElementById('hdnAfterPerson').value = p[0];
			        document.getElementById('txtAfterPerson').value = p[1];
			    }
		    }
        }
        function ckeck(op)
        {
            var nodeId = "";
            nodeId = document.getElementById("hdnNodeID").value;
            if (nodeId != "") //当前节点非前插或后插节点
            {
                if (op == 0) 
                {
                    document.getElementById('trFront').style.display = '';
	                document.getElementById('trResult').style.display = 'none';
	                document.getElementById('trAuditInfo').style.display = 'none';
	                document.getElementById('trAnnouncer').style.display = 'none';
	                document.getElementById('trContent').style.display = 'none';
	                document.getElementById('trAfter').style.display = 'none';
	                document.getElementById('trSend').style.display = 'none';
	                document.getElementById('hdnType').value = '前插';
	                document.getElementById('ddlRoleType').disabled = false;
                }
                else
                {
                    document.getElementById('trFront').style.display = 'none';
	                document.getElementById('trResult').style.display = '';
	                document.getElementById('trAuditInfo').style.display = '';
	                document.getElementById('trAnnouncer').style.display = '';
	                document.getElementById('trContent').style.display = '';
	                document.getElementById('trAfter').style.display = '';
	                document.getElementById('trSend').style.display = '';
	                document.getElementById('hdnType').value = '后插';
	                document.getElementById('ddlRoleType').disabled = true;
                }
            }
        }
        function openAudit(op)
        {
            var url = "";
            var instanceCode = document.getElementById('hdnInstanceCode').value
            if (op == 0)
            {
                url = "FlowStatus.aspx?ic="+instanceCode;
            }
            else
            {
                url = "AuditStatus.aspx?ic="+instanceCode;
            }
            window.showModalDialog(url,window,"dialogHeight:400px;dialogWidth:800px;center:1;help:0;status:0;");
        }
	    </script>
	</head>
	<body scroll="no" class="body_popup">
		<form id="Form1" method="post" runat="server">
			<TABLE class="table-form" id="TableVersion" cellSpacing="0" cellPadding="0" width="100%" border="1">
				<TR>
					<TD colSpan="4" height = "40px" class="td-toolsbar">
					    <div align="right">
					        <input type="hidden" id="hdnNodeID" name="hdnNodeID" style="width : 10px" runat="server" />

					        <input type="hidden" id="hdnType" name="hdnType" style="width : 10px" runat="server" />

					        <input type="hidden" id="hdnInstanceCode" name="hdnInstanceCode" style="width : 10px" runat="server" />

                            <input id="btnFront" type="button" value="前插审核人" onclick="ckeck(0);" runat="server" />

                            <input id="btnAfter" type="button" value="后插审核人" onclick="ckeck(1);" runat="server" />

                            <input id="btnFlowStatus" type="button" value="查看流程状态" onclick="openAudit(0);" runat="server" />

                            <input id="btnStartWFRecord" type="button" value="查看审核记录" onclick="openAudit(1);" runat="server" />

					        <asp:Button ID="btnSubmit" Text="提 交" OnClick="btnSubmit_Click" runat="server" />
                        </div></TD>
				</TR>
				<tr id="trFront" style="display:none">
				    <TD class="td-label" width="25%">选择前插人员</TD>
				    <td><asp:TextBox ID="txtFrontPerson" CssClass="td-input" Width="20%" runat="server"></asp:TextBox> 
				    <img id="imgPick1" src="../../images/contact.gif" align="bottom" onclick="pickPerson(1);" width="16" height="16" runat="server" />

				    <input type="hidden" id="hdnFrontPerson" name="hdnFrontPerson" style="width : 10px" runat="server" />
</td>
				</tr>
				<TR id = "trResult">
					<TD class="td-label" width="25%">审核结果</TD>
					<TD><font color="#ff0000"><asp:DropDownList ID="ddlRoleType" Width="20%" runat="server"><asp:ListItem Value="1" Text="通过" /><asp:ListItem Value="0" Text="驳回" /></asp:DropDownList>&nbsp;</font>
					</TD>
				</TR>
				<tr>
				    <td  class="td-label">审核要点</td>
				    <td>
                        <asp:Label ID="LbAuditMain" runat="server"></asp:Label></td>
				</tr>
				<tr id="trAuditInfo">
				    <TD class="td-label" width="25%">审核意见</TD>
				    <td>
                        <asp:TextBox ID="txtAuditInfo" Height="52px" TextMode="MultiLine" Width="90%" runat="server"></asp:TextBox></td>
				</tr>
				<tr id="trAnnouncer">
				    <TD class="td-label" width="25%">告知人</TD>
				    <td><asp:TextBox ID="txtAnnouncer" CssClass="td-input" Width="20%" runat="server"></asp:TextBox>
					    <img id="imgPick2" src="../../images/contact.gif" align="bottom" onclick="pickPerson(2);" width="16" height="16" runat="server" />

				        <input type="hidden" id="hdnAnnouncer" name="hdnAnnouncer" style="width : 10px" runat="server" />
</td>
				</tr>
				<tr id="trContent">
				    <TD class="td-label" width="25%">告知内容</TD>
				    <td>
                        <asp:TextBox ID="txtContent" Height="52px" TextMode="MultiLine" Width="90%" runat="server"></asp:TextBox></td>
				</tr>
				<TR id = "trAfter" style="display:none">
					<TD class="td-label" width="25%">
                        选择后插人员</TD>
					<TD><asp:TextBox ID="txtAfterPerson" CssClass="td-input" Width="20%" runat="server"></asp:TextBox>
					    <img id="imgPick3" src="../../images/contact.gif" align="bottom" onclick="pickPerson(3);" width="16" height="16" runat="server" />

				        <input type="hidden" id="hdnAfterPerson" name="hdnAfterPerson" style="width : 10px" runat="server" />
</TD>
				</TR>
				<TR id="trSend">
					<TD class="td-label" width="25%"></TD>
					<td><asp:CheckBox ID="ckbIsSendMsg" Text="短信督办" runat="server" />&nbsp;
					    <asp:CheckBox ID="ckbIsSendInfo" Text="发送消息" runat="server" /></td>
				</TR>
			</TABLE>
			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
		</form>
	</body>
</HTML>
