<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CostDiaryDetails.aspx.cs" Inherits="BudgetManage_Cost_CostDiaryDetails" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>费用报销明细表</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/jwJson.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var JwTable = new JustWinTable('gvwCostVerify');
            replaceEmptyTable('emptyCostVerify', 'gvwCostVerify');
        });
        //选择部门
        function selDept() {
            jw.selectOneDep({ idinput: 'hflDepCode', nameinput: 'txtDepmentName' });
        }
        //选择人员
        function selUser() {
            jw.selectOneUser({ idinput: 'hflUserCode', nameinput: 'txtUserName' });
        }
    </script>
</head>
<body style="height: 98%;" >
    <form id="form1" runat="server">
    <div>
        <table class="queryTable" cellpadding="3px" cellspacing="0px">
			<tr>
				<td class="descTd">
					报销日期
				</td>
				<td>
					<asp:TextBox ID="txtStartDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
				</td>
                <td>
                至
                </td>
                <td>
					<asp:TextBox ID="txtEndDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
				</td>
				<td class="descTd">
					ID号
				</td>
				<td>
					<asp:TextBox ID="txtIDNumber" runat="server"></asp:TextBox>
				</td>
				<td class="descTd">
					项目编号
				</td>
                <td>
					<asp:TextBox ID="txtPojectNum" runat="server"></asp:TextBox>
				</td>
            </tr>
          <tr>
               <td class="descTd">
               部门
                </td>
				<td>
					
                    <span class="spanSelect" style="width: 130px">
                    <input name="txtDepmentName" type="text" id="txtDepmentName" style="width: 97px;
                           height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

                     <img alt="选择签订单位" onclick="selDept();" src="../../images/icon.bmp" style="float: right;" />
                    </span>
                    <input name="hflDepCode" type="hidden" id="hflDepCode" runat="server" />

				</td>
				<td class="descTd">
			    姓名
				</td>
				<td>
					
                    <span class="spanSelect" style="width: 130px">
                     <input name="txtUserName" type="text" id="txtUserName" style="width: 97px;
                                    height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

                    <img alt="选择签订单位" onclick="selUser();" src="../../images/icon.bmp" style="float: right;" />
                </span>
                <input name="hflUserCode" type="hidden" id="hflUserCode" runat="server" />

				</td>
                <td class="descTd" style="text-indent:10px">
                审核状态
                </td>
                <td>
                    <asp:DropDownList ID="ddlFlowState" Width="140px" runat="server"><asp:ListItem Text="" Value="" Selected="true" /><asp:ListItem Text="未提交" Value="-1" /><asp:ListItem Text="审核中" Value="0" /><asp:ListItem Text="已审核" Value="1" /><asp:ListItem Text="驳回" Value="-2" /><asp:ListItem Text="重报" Value="-3" /></asp:DropDownList>
                </td>
                
		  </tr>
		</table>
		<div class="divFooter" style="text-align: left; vertical-align: middle;">
			<asp:Button ID="btnQuery" Text="查询" OnClick="btnQuery_Click" runat="server" />
            <asp:Button ID="btnExcel" Text="导出Excel" Width="80px" OnClick="btnExcel_Click" runat="server" />
		</div>
		<asp:GridView ID="gvwCostVerify" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwCostVerify_RowDataBound" runat="server">
<EmptyDataTemplate>
				<table id="emptyCostVerify" class="gvdata">
					<tr class="header">
						<th scope="col">
							序号
						</th>
						<th scope="col">
							报销日期
						</th>
						<th scope="col">
							ID号(流水号)
						</th>
						<th scope="col">
							项目编号
						</th>
						<th scope="col">
							项目名称
						</th>
						<th scope="col">
							部门
						</th>
						<th scope="col">
							姓名
						</th>
						<th scope="col">
							成本科目
						</th>
						<th scope="col">
							费用名称
						</th>
						<th scope="col">
							金额
						</th>
                        <th scope="col">
							流程状态
						</th>
                        <th scope="col">
							备注
						</th>
                        
					</tr>
				</table>
			</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
						<%# Container.DataItemIndex + (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + 1 %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="报销日期" ItemStyle-Width="72px"><ItemTemplate>
						<%# Common2.GetTime(Eval("InputDate2")) %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="ID号(流水号)" ItemStyle-Width="72px"><ItemTemplate>
					<%# Eval("IndireCode") %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目编号" ItemStyle-Width="72px"><ItemTemplate>
                         <%# Eval("PrjCode") %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称" ItemStyle-Width="72px"><ItemTemplate>
                        <%# Eval("PrjName") %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="部门" ItemStyle-Width="100px"><ItemTemplate>
					   <%# Eval("V_BMMC") %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="姓名" ItemStyle-Width="72px"><ItemTemplate>
					<%# Eval("IssuedBy") %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="成本科目" ItemStyle-Width="72px"><ItemTemplate>
					<%# CBSName(Eval("CBSCode").ToString()) %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="费用名称" ItemStyle-Width="72px"><ItemTemplate>
					  <%# Eval("Name") %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="金额" ItemStyle-Width="72px" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
					<%# Eval("Amount") %>	
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
						<%# Common2.GetState(Eval("FlowState").ToString()) %>
					</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注" ItemStyle-Width="72px"><ItemTemplate>
					<%# Eval("Note") %>
					</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
		<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
    </webdiyer:AspNetPager>
    </div>
    </form>
</body>
</html>
