<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectInProcessCostTab.aspx.cs" Inherits="ProjectInProcessCostTab" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>ProjectInProcessCostTab</title>
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
                    在建工程成本明细表</td>
            </tr>
           
            <tr>
                <td valign="middle" align="center" colspan="3" height="22">
                    <table id="Tablea" height="22" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr class="report_grid_head" id="TrHeader" bgcolor="Gainsboro">
                            <td valign="bottom" align=left width=7%>
                                编报单位:<asp:DropDownList ID="DDLInProcess" runat="server"></asp:DropDownList></td>
                            <td  valign="bottom" align=left width=20% style="height: 25px">
                             日期：
                                <JWControl:DateBox ID="DtbStartDate" Width="70px" runat="server"></JWControl:DateBox>&nbsp;-
                                <JWControl:DateBox ID="DtbEndDate" Width="70px" runat="server"></JWControl:DateBox>&nbsp;
                                <asp:Button ID="BtnSearch" Text="" CssClass="Btn_Search" runat="server" />&nbsp;
                                </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top" colspan="3">
                    <div class="gridBox">
                        <table class="table-normal" id="TableOUT" cellspacing="0" cellpadding="0" width="100%" runat="server"><tr runat="server"><td runat="server">
                                    <asp:DataGrid ID="DGrdMaterialExpend" Width="100%" BorderColor="#ACA899" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="false" CssClass="grid" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><Columns><asp:BoundColumn DataField="ProjectName" HeaderText="项目名称"></asp:BoundColumn><asp:BoundColumn DataField="CurrMonthBugetCost" HeaderText="本 月 数/预算成本"></asp:BoundColumn><asp:BoundColumn DataField="CurrMonthPersonCost" HeaderText="人工费"></asp:BoundColumn><asp:BoundColumn DataField="CurrMonthEpibolyCost" HeaderText="外包人工费"></asp:BoundColumn><asp:BoundColumn DataField="CurrMonthMaterialCost" HeaderText="材料费用"></asp:BoundColumn><asp:BoundColumn DataField="CurrMonthTurnroundMaterialCost" HeaderText="主材"></asp:BoundColumn><asp:BoundColumn DataField="CurrMonthStructureCost" HeaderText="辅材"></asp:BoundColumn><asp:BoundColumn DataField="CurrMonthMachineCost" HeaderText="机械费"></asp:BoundColumn><asp:BoundColumn DataField="CurrMonthOtherDirectCost" HeaderText="其他直接费"></asp:BoundColumn><asp:BoundColumn DataField="CurrMonthConstructIndirectCost" HeaderText="施工间接费"></asp:BoundColumn><asp:BoundColumn DataField="CurrMonthSublettingCost" HeaderText="分包成本" Visible="false"></asp:BoundColumn><asp:BoundColumn DataField="CurrMonthFactTotalCost" HeaderText="实际成本合计"></asp:BoundColumn><asp:BoundColumn DataField="CurrMonthReduceCost" HeaderText="降低额"></asp:BoundColumn><asp:BoundColumn DataField="CurrMonthReducePercent" HeaderText="降低率"></asp:BoundColumn><asp:BoundColumn DataField="CurrMonthProjectOtherIncome" HeaderText="工程其他收入" Visible="false"></asp:BoundColumn><asp:BoundColumn DataField="CurrYearBugetCost" HeaderText="本年度累计\预算成本"></asp:BoundColumn><asp:BoundColumn DataField="CurrYearFactCost" HeaderText="实际成本"></asp:BoundColumn><asp:BoundColumn DataField="CurrYearReduceCost" HeaderText="降低额"></asp:BoundColumn><asp:BoundColumn DataField="CurrYearReducePercent" HeaderText="降低率"></asp:BoundColumn><asp:BoundColumn DataField="CurrYearProjectOtherIncome" HeaderText="工程其他收入" Visible="false"></asp:BoundColumn><asp:BoundColumn DataField="SplitYearBugetCost" HeaderText="跨年度累计\预算成本"></asp:BoundColumn><asp:BoundColumn DataField="SplitYearFactCost" HeaderText="实际成本"></asp:BoundColumn><asp:BoundColumn DataField="SplitYearReduceCost" HeaderText="降低额"></asp:BoundColumn><asp:BoundColumn DataField="SplitYearReducePercent" HeaderText="降低率"></asp:BoundColumn><asp:BoundColumn DataField="SplitYearProjectOtherIncome" HeaderText="工程其他收入" Visible="false"></asp:BoundColumn></Columns></asp:DataGrid>
                                </td></tr></table>
                    </div>
                </td>
            </tr>
            <tr>
                <td valign="middle" align="center" colspan="3" height="22">
                    <table id="Tablev" height="22" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td width="34%" valign="bottom">
                                单位负责人:<asp:Label ID="LblPrincipal" runat="server"></asp:Label></td>
                            <td width="33%" valign="bottom">
                                成本员:<asp:Label ID="LblCostPerson" runat="server"></asp:Label></td>
                            <td width="33%" valign="bottom">
                                编报日期:<asp:Label ID="LblDate" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
