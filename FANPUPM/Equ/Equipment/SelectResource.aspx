<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="SelectResource.aspx.cs" Inherits="Equ_Equipment_SelectResource" MaintainScrollPositionOnPostBack="true" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>资源选择</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../Script/json2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript" src="../../Script/jwJson.js"></script>
	<script type="text/javascript" src="../Script/Equipment/SelectResource.js"></script>
</head>
<body>
	<form id="form1" runat="server">
	<div style="width: 100%; height: 25px;">
		<div style="width: 150px; float: left">
			<asp:DropDownList ID="dropResourceType" DataTextField="ResourceTypeName" DataValueField="ResourceTypeCode" AutoPostBack="true" OnSelectedIndexChanged="dropResourceType_SelectedIndexChanged" runat="server"></asp:DropDownList>
		</div>
		<div style="width: 400px; float: right">
			编码:&nbsp;&nbsp;<asp:TextBox ID="txtCode" Width="120px" runat="server"></asp:TextBox>
			名称:&nbsp;&nbsp;<asp:TextBox ID="txtName" Width="120px" runat="server"></asp:TextBox>
			<asp:Button ID="btnSelect" Text="查询" OnClick="btnSelect_Click" runat="server" />
		</div>
	</div>
	<hr />
	<div style="width: 100%; height:460px;">
		<div style="width: 150px; height: 420px; overflow: auto; float: left; border-right: solid 1px #B5CCDE;">
			<asp:TreeView ID="trvwResourceType" ExpandDepth="1" ShowLines="true" OnSelectedNodeChanged="trvwResourceType_SelectedNodeChanged" OnTreeNodePopulate="trvwResourceType_TreeNodePopulate" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
		</div>
		<div style="width: 810px; height: 400px; overflow: auto; float: left;">
			<asp:GridView ID="gvwResource" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwResource_RowDataBound" DataKeyNames="ResourceId,ResourceCode,ResourceName,Specification,SupplierId,CorpName" runat="server">
<EmptyDataTemplate>
					<table id="emptyResource" style="width: 100%; margin-left: 0px; margin-right: 0px;
						padding: 0;">
						<tr class="header" style="width: 100%;">
							<th scope="col" style="width: 25px;">
								序号
							</th>
							<th scope="col">
								编码
							</th>
							<th scope="col">
								名称
							</th>
							<th scope="col">
								规格
							</th>
							<th scope="col">
								型号
							</th>
							<th scope="col">
								单位
							</th>
							<th scope="col">
								单价
							</th>
							<th scope="col">
								品牌
							</th>
							<th scope="col">
								技术参数
							</th>
							<th scope="col">
								供应商
							</th>
						</tr>
					</table>
				</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
							
						</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="编码" DataField="ResourceCode" /><asp:TemplateField HeaderText="名称"><ItemTemplate>
							<span class="tooltip" title=''>
								
							</span>
						</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格"><ItemTemplate>
							<span class="tooltip" title=''>
								
							</span>
						</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="型号"><ItemTemplate>
							<span class="tooltip" title=''>
								
							</span>
						</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="单位" DataField="Name" /><asp:TemplateField HeaderText="单价" HeaderStyle-Width="50px">
<ItemTemplate>
							
						</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="品牌">
<ItemTemplate>
							<span class="tooltip" title=''>
								
							</span>
						</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="技术参数">
<ItemTemplate>
							<span class="tooltip" title=''>
								
							</span>
						</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="供应商">
<ItemTemplate>
							<span class="tooltip" title=''>
								
							</span>
						</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
			<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" ShowPageIndexBox="Always" NumericButtonCount="8" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
			</webdiyer:AspNetPager>
		</div>
		<div style="width: 810px; height: 25px; float: left; text-align: right; vertical-align: middle">
			<br />
			<input type="button" id="btnSave" disabled="disabled" value="保存" />
			<input type="button" id="btnCancel" value="取消" />
		</div>
	</div>
	
	<asp:HiddenField ID="hfldResource" runat="server" />
	
	<asp:HiddenField ID="hfldResourceCode" runat="server" />
	
	<asp:HiddenField ID="hfldResourceName" runat="server" />
	
	<asp:HiddenField ID="hfldSpecification" runat="server" />
	
	<asp:HiddenField ID="hfldCorpId" runat="server" />
	
	<asp:HiddenField ID="hfldCorpName" runat="server" />
	</form>
</body>
</html>
