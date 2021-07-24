<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeptList.aspx.cs" Inherits="DeptList" %>
<%@ Import Namespace="Microsoft.Web.UI.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head id="Head1" runat="server"><title>DeptList</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
		<script language="JavaScript">
		<!--
		    function initTree() {
		        TVDept.onexpand = function () {
		            if (this.clickedNodeIndex != null) {
		                if (TVDept.getTreeNode(this.clickedNodeIndex).getAttribute("NodeData") != "1") {
		                    this.queueEvent('onexpand', this.clickedNodeIndex);
		                    window.setTimeout('__doPostBack(\'TVDept\',\'\')', 0, 'JavaScript')
		                }
		            }
		        }

		    }
		// -->
		</script>
	</head>
	<body class="body_frame" scroll="no" onload="initTree();">
		<form id="Form1" method="post" runat="server">
			<input type="hidden" name="__EVENTTARGET"> <input type="hidden" name="__EVENTARGUMENT">
			<script language="javascript" type="text/javascript">
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
			</script>
			<table width="100%" height="100%" cellpadding="0" cellspacing="0">
				<tr>
					<td>
						<div id="dvDeptBox" class="gridBox">
							<iewc:TreeView ID="TVDept" runat="server"></iewc:TreeView>
						</div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
