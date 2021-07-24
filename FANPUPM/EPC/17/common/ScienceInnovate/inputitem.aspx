<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inputitem.aspx.cs" Inherits="InputItem" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>J</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script src="../../../Web_Client/Tree.js"></script>
		<script language="javascript">
		function ClickRow(obj,tbName,ChildID)
		{
			//doClick(obj,tbName);
			document.getElementById("hidChildID").value = ChildID;
		}
		function doCheck()
		{
			var childId = document.getElementById("hidChildID").value;
			if(childId == "")
				return false;
			else
				return true;
		}
		function checkNum(obj)
			{
				if (!(new RegExp(/^(\d+\.+\d)?(\d+$)/).test(obj.value)))
				{
					alert("应输入数值！");
					obj.select();
					obj.focus();
					return false;
				}
				else
				{
					return true;
				}
			}
		function SumAll(obj)
		{
			var iCount = document.getElementById("hidgridRowCount").value;
			var iSum = 0;
			if(checkNum(obj))
			{
				for(var i=2;i<=parseInt(iCount)+1;i++)
				{
					if(document.getElementById("dgList__ctl"+i+"_txtDevotionMoney") != null)
						iSum += parseInt(document.getElementById("dgList__ctl"+i+"_txtDevotionMoney").value);
				}
				document.getElementById("txtSum").value = iSum;
			}
		}
		</script>
	</head>
	<body class="body-normal" bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE class="table-normal" id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%">
				<TR>
					<TD class="td-toolsbar"><input id="hidType" type="hidden" name="Hidden2" runat="server" />

						<input id="hidMainId" type="hidden" name="Hidden1" runat="server" />

						<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl><asp:Button ID="btnSave" CssClass="button-normal" Text="保  存" OnClick="btnSave_Click" runat="server" />&nbsp;
						<asp:Button ID="btnDel" CssClass="button-normal" Text="删  除" OnClick="btnDel_Click" runat="server" /></TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%"><asp:DataGrid ID="dgList" CssClass="grid" AutoGenerateColumns="false" DataKeyField="ChildMainId" Width="100%" runat="server"><ItemStyle Height="20px"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn HeaderText="序号" ItemStyle-Width="50px"></asp:BoundColumn><asp:BoundColumn DataField="SubjectName" HeaderText="名称" ItemStyle-Width="180px"></asp:BoundColumn><asp:TemplateColumn HeaderText="投入费用（元）"><ItemTemplate>
											<asp:TextBox CssClass="text-normal" Width="100%" ID="txtDevotionMoney" Text='<%# Convert.ToString(Eval("DevotionMoney")) %>' runat="server"></asp:TextBox>
										</ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="备注" ItemStyle-Width="35%"><ItemTemplate>
											<asp:TextBox CssClass="text-normal" Width="100%" ID="txtRemark" Text='<%# Convert.ToString(Eval("Remark")) %>' runat="server"></asp:TextBox>
											<input type="hidden" id="firstNum" value='<%# Convert.ToString(Eval("FirstNum")) %>' runat="server" />

											<input type="hidden" id="secNum" name="Hidden1" value='<%# Convert.ToString(Eval("secNum")) %>' runat="server" />

											<input type="hidden" id="SubjectId" value='<%# Convert.ToString(Eval("SubjectId")) %>' runat="server" />

										</ItemTemplate></asp:TemplateColumn></Columns></asp:DataGrid></div>
					</TD>
				</TR>
				<tr>
					<td height="10">
						<table id="tbSumAll" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td id="sumAll" align="center" width="250">合计<input id="hidChildID" type="hidden" name="Hidden1" runat="server" />
</td>
								<td><input id="hidgridRowCount" type="hidden" name="Hidden1" runat="server" />
<INPUT class="text-normal" id="txtSum" readOnly type="text"></td>
							</tr>
						</table>
					</td>
				</tr>
			</TABLE>
		</form>
		<script language="javascript">
			var iCount = document.getElementById("hidgridRowCount").value;
			var iSum = 0;
			for(var i=2;i<=parseInt(iCount)+1;i++)
			{
				if(document.getElementById("dgList__ctl"+i+"_txtDevotionMoney") != null)
					iSum += parseInt(document.getElementById("dgList__ctl"+i+"_txtDevotionMoney").value);
			}
			document.getElementById("txtSum").value = iSum;
			function disPlayTable()
			{
				document.getElementById("tbSumAll").style.display=none;
			}
		</script>
	</body>
</HTML>
