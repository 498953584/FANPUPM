<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pickdept.aspx.cs" Inherits="PickDept" %>
<%@ Register TagPrefix="ie" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<%@ Import Namespace="Microsoft.Web.UI.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>选择部门...</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
		<script language="JavaScript">
		<!--
			String.prototype.Trim = function()
			{
				return this.replace(/(^\s*)|(\s*$)/g, "");
			}
			
			function  initTree()
			{
				TVDept.onexpand = function()
				{
					if (this.clickedNodeIndex != null)
					{
						if (TVDept.getTreeNode(this.clickedNodeIndex).getAttribute("NodeData")!="1")
						{
							this.queueEvent('onexpand', this.clickedNodeIndex);
							window.setTimeout('__doPostBack(\'TVDept\',\'\')', 0, 'JavaScript')
						}
					}
				}
				
				TVDept.onclick = function()
				{
					if (this.clickedNodeIndex != null)
					{
						document.getElementById('BtnOK').disabled = false; 
					}
				}
			}
			
			function confirmDept()
			{
				var deptID		= TVDept.getTreeNode(TVDept.selectedNodeIndex).getAttribute("ID");
				var deptName	= TVDept.getTreeNode(TVDept.selectedNodeIndex).getAttribute("Text");
				var dept = window.dialogArguments;
				dept[0] = deptID;
				dept[1] = deptName.Trim();
				window.returnvalue= dept;
				window.close();
			}
			
			function selectDept(deptID,deptName)
			{
				var deptID		= TVDept.getTreeNode(TVDept.selectedNodeIndex).getAttribute("ID");
				var deptName	= TVDept.getTreeNode(TVDept.selectedNodeIndex).getAttribute("Text");
				var dept = window.dialogArguments;
				dept[0] = deptID;
				dept[1] = deptName.Trim();
				window.returnvalue= dept;
				window.close();
			}
			
		// -->
		</script>
	</head>
	<body class="body_frame" scroll="no" onload="initTree();">
		<form id="Form1" method="post" runat="server">
			<INPUT type="hidden" name="__EVENTTARGET"> <INPUT type="hidden" name="__EVENTARGUMENT">
			<SCRIPT language="javascript" type="text/javascript">
		<!--
			function __doPostBack(eventTarget, eventArgument) {
				var theform;
				if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
					theform = document.Form1;
				}
				else {
					theform = document.forms["Form1"];
				}
				theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
				theform.__EVENTARGUMENT.value = eventArgument;
				theform.submit();
			}
		// -->
			</SCRIPT>
			<TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1"
				style="BORDER-COLLAPSE: collapse">
				<TR>
					<TD>
						<DIV id="dvDeptBox" class="gridBox">
							<ie:TreeView ID="TVDept" ExpandLevel="2" runat="server"></ie:TreeView></DIV>
					</TD>
				</TR>
				<tr>
					<td height="30" align="right">
						<table width="60%" height="100%">
							<tr align="right" valign="middle">
								<td><input id="BtnOK" type="button" value="确  定" disabled onclick="confirmDept();" class="button-normal">
								<input id="BtnCancel" type="button" value="取  消" onclick="window.close();" class="button-normal">
                                    &nbsp;&nbsp;
                                </td>
							</tr>
						</table>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
