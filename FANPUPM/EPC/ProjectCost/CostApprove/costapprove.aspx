<%@ Page Language="C#" AutoEventWireup="true" CodeFile="costapprove.aspx.cs" Inherits="CostApprove" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>成本核算</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<script language="javascript">
			function ClickRow(obj)
			{
				var cellObj = obj.cells[2].all.tags("a")[0];
				eval(cellObj.href);
			}
			function CheckInputIsFloat(keyCode,e)
			{
				if((keyCode>95 && keyCode<106) || (keyCode>47 && keyCode<58) || keyCode == 8|| keyCode == 46 || keyCode == 37 || keyCode == 39 || keyCode == 9 || keyCode == 13)
				{
			    }
				else if(keyCode == 110 || keyCode==190)
				{
					 if(e.value == "" || e.value.indexOf(".") != -1)
					 {
						 return false;
					 }
				} 
				else
				{
					return false;
				}
			}
			//选择成本归类
			function CostTypeSelect(txtchecker)
			{
				var projectCode = document.getElementById('HdnProjectCode').value;
				var startDate = document.getElementById('HdnStartDate').value;
				var url= "CostTypeSelect.aspx?pc="+projectCode+"&sd="+startDate;
				var Man = new Array();
				Man[0] = "";
				Man[1] = "";
				window.showModalDialog(url,Man,"dialogHeight:475px;dialogWidth:600px;center:1;help:0;status:0;");
				if(Man[0].length > 0)
				{
					txtchecker.parentElement.parentElement.children[7].children[0].value=Man[1];
					txtchecker.parentElement.parentElement.children[7].children[1].value=Man[0];
				}
			}
			
			function checkDecimal(sourObj)
			{
				if (sourObj.value=="")
				{
					sourObj.value = 0;
				}
				if (!(new RegExp(/^\d+(\.\d+)?$/).test(sourObj.value)))
				{
					alert('数据类型不正确！');
					sourObj.focus();
					return;
				}
			}
			function OpenWindow(ProjectCode,TaskCode,TaskBillCode)
			{
				var url= "../Construct/TaskBillLook.aspx?pc="+ProjectCode+"&tbc="+TaskBillCode;
				window.showModalDialog(url,'施工日志','dialogHeight: 500px; dialogWidth: 700px; center: Yes; help: No; resizable: No; status: No;');
				return false;
			}
			
			function selectAll(obj)
			{
				var num = document.all.tags('input').length;
				var str_temp;
				//设置子模块复选框;
				for(var i=0; i<num; i++)
				{
					str_temp = document.all.tags('input')[i].id;
					if(str_temp.substr(str_temp.length-5,5) == 'cbSel')
					{
						document.all.tags('input')[i].checked = obj.checked;
					}
				}
			}
		</script>
	</head>
	<body class="body_frame" scroll="no">
		<form id="Formx" method="post" runat="server">
			<TABLE id="Tablex" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="td-toolsbar"><input id="HdnProjectCode" style="WIDTH: 10px" type="hidden" name="HdnProjectCode" runat="server" />
&nbsp;
						<input id="HdnStartDate" style="WIDTH: 10px" type="hidden" name="HdnStartDate" runat="server" />
&nbsp;
						<asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" />
						<asp:Button ID="BtnReturn" Text="返 回" OnClick="BtnReturn_Click" runat="server" /></TD>
				</TR>
				<TR>
					<TD vAlign="top" width="100%" height="50%">
						<DIV class="gridBox" id="DVConstruct"><asp:DataGrid ID="DGrdConstruct" Width="100%" AutoGenerateColumns="false" CssClass="grid" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn DataField="ConstructDate" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn Visible="false" DataField="TaskBookCode" HeaderText="任务单编码"></asp:BoundColumn><asp:BoundColumn DataField="TaskName" HeaderText="分项工程"></asp:BoundColumn><asp:ButtonColumn Text="选择" HeaderText="选择列" CommandName="Select"></asp:ButtonColumn><asp:TemplateColumn HeaderText="详细信息">
<ItemTemplate>
											<asp:LinkButton ID="LBtnDetail" runat="server">详细信息</asp:LinkButton>
										</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="TaskCode" HeaderText="任务编号"></asp:BoundColumn></Columns></asp:DataGrid></DIV>
					</TD>
				</TR>
				<tr>
					<TD vAlign="top" width="100%" height="50%">
						<DIV class="gridBox" id="DVRelationRes"><asp:DataGrid ID="DGrdRelationRes" Width="100%" AutoGenerateColumns="false" CssClass="grid" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn>
<HeaderTemplate>
											<FONT face="宋体">
												<asp:CheckBox ID="cbAll" Text="全选" runat="server" /></FONT>
										</HeaderTemplate>

<ItemTemplate>
											<asp:CheckBox ID="cbSel" runat="server" />
										</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="ResourceName" HeaderText="名称"></asp:BoundColumn><asp:BoundColumn DataField="Specification" HeaderText="规格型号"></asp:BoundColumn><asp:BoundColumn DataField="UnitName" HeaderText="单位"></asp:BoundColumn><asp:TemplateColumn HeaderText="单价(元)">
<ItemTemplate>
											<asp:TextBox ID="TxtPrice" onblur="checkDecimal(this);" Width="60px" Text='<%# Convert.ToString(Eval("UnitPrice")) %>' runat="server"></asp:TextBox>
										</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="数量">
<ItemTemplate>
											<asp:TextBox ID="TxtQuantity" onblur="checkDecimal(this);" Width="60px" Text='<%# Convert.ToString(Eval("Quantity")) %>' runat="server"></asp:TextBox>
										</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn HeaderText="类型"></asp:BoundColumn><asp:TemplateColumn HeaderText="成本归类">
<ItemTemplate>
										
											<asp:TextBox ID="TxtCostType" ondblclick="CostTypeSelect(this);" Width="70px" ToolTip="双击选择成本归类" style="background-color:#FFFFC0; cursor:hand;" runat="server"></asp:TextBox>
										
												
												<input id="HdnCostType" style="WIDTH: 10px" type="hidden" name="HdnCostType" runat="server" />

										</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn Visible="false" DataField="NoteID" HeaderText="NoteID"></asp:BoundColumn></Columns></asp:DataGrid></DIV>
					</TD>
				</tr>
			</TABLE>
			<JWControl:PersistScrollPosition ID="PScrollP" ControlToPersist="DVConstruct" runat="server"></JWControl:PersistScrollPosition>
			<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></form>
	</body>
</HTML>
