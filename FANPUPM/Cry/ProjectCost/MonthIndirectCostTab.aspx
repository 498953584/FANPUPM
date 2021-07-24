<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MonthIndirectCostTab.aspx.cs" Inherits="MonthIndirectCostTab" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>MonthIndirectCostTab</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<body class="body_clear" scroll="YES">
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
                    月度间接成本分析表</td>
            </tr>
           
            <tr>
                <td valign="middle" align="center" colspan="3" height="22">
                    <table id="Tablea" height="22" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr class="report_grid_head" id="TrHeader" bgcolor="Gainsboro">
                            <td width="15%">
                                &nbsp;项目名称: 
                                <asp:DropDownList ID="ddlPrjname" Width="176px" runat="server"></asp:DropDownList></td>
                            <td align="left" width="20%">
                                &nbsp;日期：
                                <JWControl:DateBox ID="DtbStartDate" Width="70px" runat="server"></JWControl:DateBox>&nbsp;-
                                <JWControl:DateBox ID="DtbEndDate" Width="70px" runat="server"></JWControl:DateBox>&nbsp;<asp:Button ID="BtnSearch" CssClass="Btn_Search" Text="" runat="server" /></td>
                            
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top" colspan="3">
                    <div class="gridBox">
                        <table class="table-normal" id="TableOUT" cellspacing="0" cellpadding="0" width="100%" runat="server"><tr runat="server"><td runat="server">
                                    <asp:DataGrid ID="DGrdMaterialExpend" CssClass="grid" Width="100%" AutoGenerateColumns="false" CellPadding="0" BorderWidth="1px" BorderColor="#ACA899" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><Columns><asp:BoundColumn DataField="NodeCode" HeaderText="间接成本编号"></asp:BoundColumn><asp:BoundColumn DataField="NodeName" HeaderText="间接成本项目"></asp:BoundColumn><asp:BoundColumn DataField="CurrMonthProductValueStat" HeaderText="产值/本月" Visible="false"></asp:BoundColumn><asp:BoundColumn DataField="JournalProductValueStat" HeaderText="累计" Visible="false"></asp:BoundColumn><asp:BoundColumn DataField="BudgetCurrMonthCost" HeaderText="预算成本/本月"></asp:BoundColumn><asp:BoundColumn DataField="BudgetCurrMonthJournalCost" HeaderText="累计"></asp:BoundColumn><asp:BoundColumn DataField="PlanCurrMonthCost" HeaderText="计划成本/本月" Visible="false"></asp:BoundColumn><asp:BoundColumn DataField="PlanCurrMonthJournalCost" HeaderText="累计" Visible="false"></asp:BoundColumn><asp:BoundColumn DataField="FactCurrMonthCost" HeaderText="实际成本/本月"></asp:BoundColumn><asp:BoundColumn DataField="FactCurrMonthJournalCost" HeaderText="累计"></asp:BoundColumn><asp:BoundColumn DataField="FactCurrMonthBias" HeaderText="实际偏差/本月"></asp:BoundColumn><asp:BoundColumn DataField="FactCurrMonthJournalBias" HeaderText="累计"></asp:BoundColumn><asp:BoundColumn DataField="TargetCurrMonthBias" HeaderText="目标偏差/本月"></asp:BoundColumn><asp:BoundColumn DataField="TargetCurrMonthJournalBias" HeaderText="累计"></asp:BoundColumn><asp:BoundColumn HeaderText="占产值的百分数(%)/本月" DataFormatString="{0:P}"></asp:BoundColumn><asp:BoundColumn HeaderText="累计" DataFormatString="{0:P}"></asp:BoundColumn></Columns></asp:DataGrid>
                                </td></tr></table>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
