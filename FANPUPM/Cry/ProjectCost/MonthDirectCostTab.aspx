<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MonthDirectCostTab.aspx.cs" Inherits="MonthDirectCostTab" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>MonthDirectCostTab</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
</head>
<body class="body_clear" scroll="no">
    <form id="Formx" method="post" runat="server">
        <table id="Tablex" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr class="td-toolsbar">
                <td height="24">
                    <asp:Button ID="btnexecl" CssClass="button-normal" Text="导出->Excel" Width="80px" OnClick="btnexecl_Click" runat="server" />
                    <asp:Button ID="btnWord" CssClass="button-normal" Text="导出->Word" Width="80px" OnClick="btnWord_Click" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="td-title" valign="middle" align="center" colspan="3" height="25">
                    月度成本实物分析表</td>
            </tr>
            
            <tr>
                <td valign="middle" align="center" colspan="3" height="22">
                    <table id="Tablea" height="22" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr class="report_grid_head" id="TrHeader" bgcolor="Gainsboro">
                            <td width="13%">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            <td align="right" width="65%">
                                工程名称：<asp:DropDownList ID="ddlPrjname" Width="147px" runat="server"></asp:DropDownList>
                                日期：
                                <JWControl:DateBox ID="DtbStartDate" Width="70px" ReadOnly="true" runat="server"></JWControl:DateBox>&nbsp;-
                                <JWControl:DateBox ID="DtbEndDate" Width="70px" ReadOnly="true" runat="server"></JWControl:DateBox>&nbsp;&nbsp;&nbsp;</td>
                            <td align="left" width="20%">
                                <asp:Button ID="BtnSearch" Text="" CssClass="Btn_Search" runat="server" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top" colspan="3">
                    <div class="gridBox">
                        <table class="table-normal" id="TableOUT" cellspacing="0" cellpadding="0" width="100%" runat="server"><tr runat="server"><td runat="server">
                                    <asp:DataGrid ID="DGrdMaterialExpend" Width="100%" BorderColor="#ACA899" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="false" CssClass="grid" runat="server"><Columns><asp:BoundColumn DataField="TaskCode" HeaderText="分项工程编号"></asp:BoundColumn><asp:BoundColumn DataField="TaskName" HeaderText="分项工程工序名称"></asp:BoundColumn><asp:BoundColumn DataField="UnitName" HeaderText="实物单位"></asp:BoundColumn><asp:BoundColumn DataField="PlanCurrMonthQuantity" HeaderText="实物工程量/计划/本月"></asp:BoundColumn><asp:BoundColumn DataField="PlanCurrMonthJournalQuantity" HeaderText="累计"></asp:BoundColumn><asp:BoundColumn DataField="FactCurrMonthQuantity" HeaderText="实际/本月"></asp:BoundColumn><asp:BoundColumn DataField="FactCurrMonthJournalQuantity" HeaderText="累计"></asp:BoundColumn><asp:BoundColumn DataField="BudgetCurrMonthCost" HeaderText="预算成本/本月"></asp:BoundColumn><asp:BoundColumn DataField="BudgetCurrMonthJournalCost" HeaderText="累计"></asp:BoundColumn><asp:BoundColumn DataField="PlanCurrMonthCost" HeaderText="计划成本/本月" Visible="false"></asp:BoundColumn><asp:BoundColumn DataField="PlanCurrMonthJournalCost" HeaderText="累计" Visible="false"></asp:BoundColumn><asp:BoundColumn DataField="FactCurrMonthCost" HeaderText="实际成本/本月"></asp:BoundColumn><asp:BoundColumn DataField="FactCurrMonthJournalCost" HeaderText="累计"></asp:BoundColumn><asp:BoundColumn DataField="FactCurrMonthBias" HeaderText="实际偏差/本月"></asp:BoundColumn><asp:BoundColumn DataField="FactCurrMonthJournalBias" HeaderText="累计"></asp:BoundColumn><asp:BoundColumn DataField="TargetCurrMonthBias" HeaderText="目标偏差/本月"></asp:BoundColumn><asp:BoundColumn DataField="TargetCurrMonthJournalBias" HeaderText="累计"></asp:BoundColumn></Columns></asp:DataGrid>
                                </td></tr></table>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
