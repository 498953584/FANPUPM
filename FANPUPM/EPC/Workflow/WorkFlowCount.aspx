<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkFlowCount.aspx.cs" Inherits="ModuleSet_Workflow_WorkFlowCount" %>


<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>流程统计</title>
	<style type="text/css">
		#FileLink1_But_UpFile
		{
			width: auto;
		}
		#FileLink1_Btn_Upload
		{
			width: auto;
		}
		.temShow
		{
			height: 25px;
			width: 120px;
			border: 0px solid #aa0000;
			font-size: 12px;
			text-align: center;
			float: left;
			line-height: 25px;
			cursor: pointer;
			background-image: url('/images1/Res1.jpg');
			margin-left: 2px;
		}
		.temHide
		{
			height: 25px;
			width: 120px;
			border: 0px solid #aa0000;
			font-size: 12px;
			text-align: center;
			float: left;
			line-height: 25px;
			cursor: pointer;
			background-image: url('/images1/Res2.jpg');
			margin-left: 2px;
		}
	</style>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript">
	    addEvent(window, 'load', function () {
	        var jwTable1 = new JustWinTable('GVWorkTemp');
	        var jwTable2 = new JustWinTable("GVInstance");
	        replaceEmptyTable('emptyTable1', 'GVWorkTemp');
	        replaceEmptyTable('emptyTable2', 'GVInstance');
	        setHeight();
	        tem();
	    });
	    function setHeight() {
	        var height = document.getElementById("td-muti").clientHeight;
	    }

	    function ClickRow(temp) {
	        document.getElementById('HdnTemplateID').value = temp;
	        document.getElementById('frmPage').src = "WorkFlowCountTab.aspx?tid=" + temp;
	    }

	    //设置模板选项卡
	    function tem() {
	        var num = $("#hdTem").val();
	        //alert(num);
	        if (num == "0") {
	            //使用情况
	            $("#SpTemplate").attr("class", "temShow");
	            $("#SpNoTemplate").attr("class", "temHide");
	            $("#DivNoTemplate").hide();
	            $("#DivTemplate").show();
	        } else if (num == "1") {
	            //模板状况           
	            $("#SpTemplate").attr("class", "temHide");
	            $("#DivTemplate").hide();
	            $("#SpNoTemplate").attr("class", "temShow");
	            $("#DivNoTemplate").show();

	        }
	    }
	    //设置模板选项卡
	    function setTem(num) {
	        $("#hdTem").val(num);
	        tem();
	    }
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table style="height: 88%; width: 99%;" id="table1">
		<tr style="height: 22px">
			<td>
				<div style="margin-left: 10px; height: 22px; line-height: 22px;">
					<span id="SpNoTemplate" onclick="setTem('1')" class="temShow" style="margin-left: 0px;" runat="server">
						流程模板状况统计</span> <span id="SpTemplate" onclick="setTem('0')" class="temHide" runat="server">
							流程使用状况统计 </span>
				</div>
			</td>
		</tr>
		<tr>
			<td style="vertical-align: top; height: 100%;" id="td-muti">
				<div id="DivNoTemplate">
					<table width="100%" style="height: 100%;">
						<tr valign="top">
							<td style="border-bottom: solid 2px #cccccc">
								<asp:HiddenField ID="HdnTemplateID" runat="server" />
								<div style="overflow: auto; width: 100%;" id="divworktemp">
									<asp:GridView ID="GVWorkTemp" CssClass="gvdata" Width="100%" AllowPaging="true" AutoGenerateColumns="false" PageSize="20" OnRowDataBound="GVWorkTemp_RowDataBound" OnPageIndexChanging="GVWorkTemp_PageIndexChanging" DataKeyNames="TemplateID" runat="server">
<EmptyDataTemplate>
											<table id="emptyTable1" style="width: 100%; margin-left: 0; margin-right: 0">
												<tr class="header">
													<th align="center" scope="col" style="width: 40px">
														序号
													</th>
													<th align="center" scope="col">
														流程模板名称
													</th>
													<th align="center" scope="col">
														节点数
													</th>
													<th align="center" nowrap="nowrap" scope="col" style="width: 70px">
														流程完成时间
													</th>
													<th align="center" nowrap="nowrap" scope="col">
														当前模板使用数
													</th>
												</tr>
											</table>
										</EmptyDataTemplate>
<RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
													<%# Container.DataItemIndex + 1 %>
												</ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="TemplateName" HeaderText="流程模板名称" SortExpression="TemplateName" /><asp:BoundField HeaderText="节点数" InsertVisible="false" ReadOnly="true" SortExpression="TemplateID" DataField="number" /><asp:BoundField HeaderText="流程完成时间（小时）" InsertVisible="false" ReadOnly="true" SortExpression="TemplateID" DataField="during" /><asp:BoundField HeaderText="当前该模板使用数" InsertVisible="false" ReadOnly="true" SortExpression="TemplateID" /></Columns></asp:GridView>
								</div>
							</td>
						</tr>
						<tr>
							<td>
								<iframe id="frmPage" name="frmPage" src="about:blank" frameborder="0" width="100%" runat="server"></iframe>
							</td>
						</tr>
					</table>
				</div>
				<div style="display: none; overflow: auto; margin-left: 10px; height: 100%;" id="DivTemplate">
					<asp:GridView ID="GVInstance" AllowPaging="true" AutoGenerateColumns="false" PageSize="20" CssClass="gvdata" Height="100%" Width="100%" OnRowDataBound="GVInstance_RowDataBound" OnPageIndexChanging="GVInstance_PageIndexChanging" DataKeyNames="ID" runat="server">
<EmptyDataTemplate>
							<table id="emptyTable2" style="width: 100%; margin-left: 0; margin-right: 0">
								<tr class="header">
									<th align="center" style="width: 40px">
										序号
									</th>
									<th align="center" scope="col">
										流程名称
									</th>
									<th align="center" scope="col">
										使用模板
									</th>
									<th align="center" nowrap="nowrap" scope="col" style="width: 70px">
										发起人
									</th>
									<th align="center" nowrap="nowrap" scope="col">
										最终审核人
									</th>
								</tr>
							</table>
						</EmptyDataTemplate>
<RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
									<%# Container.DataItemIndex + 1 %>
								</ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="BusinessClassName" HeaderText="流程名称" SortExpression="BusinessCode" /><asp:BoundField DataField="TemplateName" HeaderText="使用模板" SortExpression="TemplateID" /><asp:BoundField DataField="Organiger" HeaderText="发起人" SortExpression="Organiger" /><asp:BoundField HeaderText="最终审核人" SortExpression="InstanceCode" /></Columns><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
				</div>
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="hdTem" runat="server" />
	</form>
</body>
</html>
