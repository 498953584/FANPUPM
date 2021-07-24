<%@ Page Language="C#" AutoEventWireup="true" CodeFile="equipaccountedit.aspx.cs" Inherits="EquipAccountEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>机械器具台账编辑</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
		    <link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />


    <script type="text/javascript" src="/Script/jquery.js"></script>

    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>

        <script src="../../../StockManage/script/Config2.js" type="text/javascript"></script>
		<script language="javascript">
			function pickEquipmentType()
			{
				var res = new Array();
				res[0] = "";
				res[1] = "";
				res[2] = "";
				res[3] = "";
				res[4] = "";
				var   url= "/EPC/Basic/Resource/ResourceFrame.aspx";
				window.showModalDialog(url,res,"dialogHeight:320px;dialogWidth:500px;center:1;help:0;status:0;");
				if (res[0]!="")
				{
					document.getElementById('hdnEquipmentType').value = res[0];
					document.getElementById('txtEquipmentType').value = res[1];
					document.getElementById('txtManCode').value = res[2];
					document.getElementById('txtEquipName').value = res[3];
					document.getElementById('txtSpec').value = res[4];
				}
			}
		   function checklen(e,maxlength)
            {
		         if(e.value.length > maxlength)
		         {
			        alert('输入长度不能大于'+maxlength);
			        e.focus();
			         return false;
		         }

            }
			function pickContract()
			{
				var res = new Array();
				res[0] = "";
				res[1] = "";
				var url= "/CommonWindow/PickCorp.aspx";
				window.showModalDialog(url,res,"dialogHeight:450px;dialogWidth:680px;center:1;help:0;status:0;");
				if (res[0]!="")
				{
					document.getElementById('HdnContractDept').value = res[0];
					document.getElementById('TxtContractDept').value = res[1];
				}
			}

			//选择项目
			function openProjPicker() {
			    document.getElementById("divFram").title = "选择项目";
			    document.getElementById("ifFram").src = "/Common/DivSelectProject.aspx?Method=returnPrj";
			    selectnEvent('divFram');
			}

			//选择项目返回值
			function returnPrj(id, name) {
			    document.getElementById('hdnProjectCode').value = id;
			    document.getElementById('txtProject').value = name;
			    document.getElementById('txtProject').focus();
			}
			function closethewindow() {
			    parent.desktop.flowclass.location = '/EPC/EquipmentManagement/Account/equipaccountlist.aspx';
			    top.frameWorkArea.window.desktop.getActive().close();
			}
		</script>
	</head>
	<body class="body_popup" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table class="table-form" id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="1">
				<tr>
					<td class="td-head" colSpan="4">机械器具台账编辑</td>
				</tr>
				<tr>
					<td class="td-label" width="20%">机械器具编号：</td>
					<td width="30%"><input type="text" class="txtneed" id="txtManCode" runat="server" />
<SPAN><IMG style="CURSOR: hand; valign: bottom" onclick="pickEquipmentType();" height="16"
								src="/images/pickRation.gif" width="16" border="0"></SPAN></td>
					<td class="td-label" width="20%">机械器具：</td>
					<td width="30%"><asp:TextBox ID="txtEquipName" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="None" ControlToValidate="txtEquipName" ErrorMessage="名称不能为空！" Width="96px" runat="server"></asp:RequiredFieldValidator></td>
				</tr>
				<tr>
					<td class="td-label">机械器具类型：</td>
					<td><input type="text" readonly="true" class="txtreadonly" style="BACKGROUND-COLOR: #ffffc0" id="txtEquipmentType" runat="server" />

						<input id="hdnEquipmentType" style="WIDTH: 10px" type="hidden" name="hdnEquipmentType" runat="server" />

						<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="None" ControlToValidate="txtEquipmentType" ErrorMessage="机械器具类型不能为空！" runat="server"></asp:RequiredFieldValidator></td>
					<td class="td-label">规格型号：</td>
					<td><asp:TextBox ID="txtSpec" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td class="td-label">精&nbsp;&nbsp; &nbsp;度：</td>
					<td><asp:TextBox ID="txtPrecision" runat="server"></asp:TextBox></td>
					<td class="td-label">出厂编号：</td>
					<td><asp:TextBox ID="txtFacCode" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td class="td-label">制造厂家：</td>
					<td><asp:TextBox ID="txtFactory" runat="server"></asp:TextBox></td>
					<td class="td-label">折旧率：
					</td>
					<td><asp:TextBox ID="txtdepreciation" Text="0" runat="server"></asp:TextBox><asp:RangeValidator ID="RangeValidator1" ErrorMessage="折旧率必须是数字!" ControlToValidate="txtdepreciation" Type="Double" MinimumValue="0" MaximumValue="100" Display="None" runat="server"></asp:RangeValidator></td>
				</tr>
				<tr>
					<td class="td-label">出厂日期：</td>
					<td><JWControl:DateBox ID="DateFactory" runat="server"></JWControl:DateBox></td>
					<td class="td-label">购置日期：</td>
					<td><JWControl:DateBox ID="DatePursh" runat="server"></JWControl:DateBox></td>
				</tr>
				<tr>
					<td class="td-label">检定周期：</td>
					<td><asp:TextBox ID="txtCheck" Text="1" runat="server"></asp:TextBox>(月)
						<asp:RangeValidator ID="RangeValidator2" Display="None" ControlToValidate="txtCheck" ErrorMessage="检定周期必须是整数！" Type="Integer" MaximumValue="65535" MinimumValue="1" runat="server"></asp:RangeValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator12" Display="None" ControlToValidate="txtCheck" ErrorMessage="检定周期不能为空！" runat="server"></asp:RequiredFieldValidator></td>
					<td class="td-label">耐用年限：</td>
					<td><asp:TextBox ID="txtSerYear" Text="10" runat="server"></asp:TextBox>(年)
						<asp:RangeValidator ID="RangeValidator3" Display="None" ControlToValidate="txtSerYear" ErrorMessage="耐用年限必须是整数！" MaximumValue="65535" MinimumValue="1" runat="server"></asp:RangeValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator13" Display="None" ControlToValidate="txtSerYear" ErrorMessage="耐用年限不能为空！" runat="server"></asp:RequiredFieldValidator></td>
				</tr>
				<tr>
					<td class="td-label">原&nbsp;&nbsp; &nbsp;值：</td>
					<td><asp:TextBox ID="txtOriginal" runat="server"></asp:TextBox>(元)
						<asp:RangeValidator ID="RangeValidator4" Display="None" ControlToValidate="txtOriginal" ErrorMessage="原值必须是数字！" Type="Double" MaximumValue="1000000000000" MinimumValue="1" runat="server"></asp:RangeValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator14" Display="None" ControlToValidate="txtOriginal" ErrorMessage="原值不能为空！" runat="server"></asp:RequiredFieldValidator></td>
					<td class="td-label">设置状态：</td>
					<td>
					<asp:DropDownList ID="drpState" runat="server"></asp:DropDownList>
						</td>
				<tr>
					<td class="td-label">所在单位：</td>
					<td colSpan=>
						<input type="text" readonly="true" class="txtreadonly" style="BACKGROUND-COLOR: #ffffc0" id="TxtContractDept" runat="server" />

						 <IMG style="CURSOR: hand; valign: bottom" onclick="pickContract();"
						  height="16" src="../../../images/pickRation.gif"
							width="16" border="0"><input id="HdnContractDept" style="WIDTH:10px" type="hidden" runat="server" />
</td>
							<td class="td-label">
							    使用项目
							</td>
							<td>
                                <input id="hdnProjectCode" type="hidden" name="hdnProjectCode" runat="server" />

								<input type="text" readonly="true" class="txtreadonly" style="BACKGROUND-COLOR: #ffffc0" id="txtProject" runat="server" />

						         <IMG style="CURSOR: hand; valign: bottom" onclick="openProjPicker();"
						          height="16" src="../../../images/pickRation.gif"
							        width="16" border="0">												        
							</td>
				</tr>
				</tr>
				<tr>
					<td class="td-label">备&nbsp;&nbsp; &nbsp;注：</td>
					<td colSpan="3"><asp:TextBox ID="txtRemark" Columns="60" Rows="4" TextMode="MultiLine" onkeyup="checklen(this,195);" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td class="td-submit" colSpan="4">
					<asp:Button ID="btnSave" Text="保 存" OnClick="btnSave_Click" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<input onclick="closethewindow()" type="button" value="取 消">
					</td>
				</tr>
			</table>
			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
			<asp:ValidationSummary ID="ValidationSummary1" Width="448px" ShowMessageBox="true" ShowSummary="false" Height="88px" runat="server"></asp:ValidationSummary>
				<div id="divFram" title="" style="display: none">
        <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
				</form>
	</body>
</HTML>
