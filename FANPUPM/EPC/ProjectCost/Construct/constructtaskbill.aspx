<%@ Page Language="C#" AutoEventWireup="true" CodeFile="constructtaskbill.aspx.cs" Inherits="ConstructTaskBill" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_uprojectlist_ascx" Src="~/EPC/UserControl1/uprojectlist.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>ItemDistribute</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<script language="javascript">
			function clickRow(ProjectCode,TaskCode,IsHaveChild,WorkLayer)
			{
				if ((WorkLayer==3 && IsHaveChild==false) || WorkLayer==4)
				{
					InfoList.location ="TaskBillList.aspx?pc="+ProjectCode+"&tc="+TaskCode+"&t=1";				
				}
				else
				{
					InfoList.location ="TaskBillList.aspx?pc="+ProjectCode+"&tc="+TaskCode+"&t=1";
				}
			}
			function switchDisplay(obj,t1,t2)
			{
				/*在这之前增加你的处理代码*/
				doSwitchDisplay(obj,'UCTaskTreeTableInThree_tblTask','UCTaskTreeTableInThree_HdnTaskCodeList',t1,t2,'../../../');//调用样式设置
				//window.alert(UCTaskTreeTableInThree_tblTask);
			}
		</script>
	</head>
	<body class="body_frame" bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0" onload="selrow3('UCProjectList_tvProjectt1')">
		<form id="Formx" method="post" runat="server">
			<table class="table-none" id="Table3" height="100%" cellSpacing="0" cellPadding="0" width="100%"
							border="1">
							<tr>
								<td vAlign="top" width="17%" height="100%">
									<div style="width:230px;height:100%;overflow:auto;">
										<MyUserControl:epc_usercontrol1_uprojectlist_ascx ID="UCProjectList" runat="server" /></div>
								</td>
								<td vAlign="top" width="83%"><iframe id="InfoList" name="InfoList" src="" frameBorder="0" width="100%" scrolling="no"
										height="100%"></iframe>
								</td>
							</tr>
						</table>
		</form>
	</body>
</HTML>
