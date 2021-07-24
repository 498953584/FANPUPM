<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectInOverCostTab.aspx.cs" Inherits="ProjectInOverCostTab" %>



<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>ProjectInOverCostTab</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<body class="body_clear" scroll="yes">
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
                    竣工工程成本明细表</td>
            </tr>
            <tr style="display:none">
                <td valign="middle" align=left colspan="3" height="22">
                    <table id="Tablea" height="22" cellspacing="0" cellpadding="0" width="100%" border="0" style="display:none">
                        <tr class="report_grid_head" id="TrHeader" bgcolor="Gainsboro" >
                            <td valign="bottom" style="width: 30%; height: 22px;">
                                <asp:DropDownList ID="DDLInProcess" Width="151px" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="DDLInProcess_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                </td>
                            <td valign="bottom" style="width: 13%; height: 22px;">
                                </td>
                            <td width="60%" style="height: 22px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top" colspan="3">
                    <div class="gridBox">
                        <table class="table-normal" id="TableOUT" cellspacing="0" cellpadding="0" width="100%" runat="server"><tr runat="server"><td runat="server">
                                    <asp:DataGrid ID="DGrdMaterialExpend" Width="100%" BorderColor="#ACA899" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="false" CssClass="grid" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><Columns><asp:BoundColumn DataField="ProjectName" HeaderText="项  目"></asp:BoundColumn><asp:BoundColumn DataField="PersonBudgetCost" HeaderText="人工费/预算"></asp:BoundColumn><asp:BoundColumn DataField="PersonFactCost" HeaderText="实际"></asp:BoundColumn><asp:BoundColumn DataField="PersonEpibolyCost" HeaderText="外包人工费"></asp:BoundColumn><asp:BoundColumn DataField="MaterialBudgetCost" HeaderText="材料费用/预算"></asp:BoundColumn><asp:BoundColumn DataField="MaterialFactCost" HeaderText="实际"></asp:BoundColumn><asp:BoundColumn DataField="TurnroundMaterialBudgetCost" HeaderText="周转材料费/预算"></asp:BoundColumn><asp:BoundColumn DataField="TurnroundMaterialFactCost" HeaderText="实际"></asp:BoundColumn><asp:BoundColumn DataField="StructureBudgetCost" HeaderText="结构件/预算"></asp:BoundColumn><asp:BoundColumn DataField="StructureFactCost" HeaderText="实际"></asp:BoundColumn><asp:BoundColumn DataField="MachineBudgetCost" HeaderText="机械费/预算"></asp:BoundColumn><asp:BoundColumn DataField="MachineFactCost" HeaderText="实际"></asp:BoundColumn><asp:BoundColumn DataField="OtherDirectBudgetCost" HeaderText="其他直接费/预算"></asp:BoundColumn><asp:BoundColumn DataField="OtherDirectFactCost" HeaderText="实际"></asp:BoundColumn><asp:BoundColumn DataField="ConstructIndirectBudgetCost" HeaderText="其它间接费/预算"></asp:BoundColumn><asp:BoundColumn DataField="ConstructIndirectFactCost" HeaderText="实际"></asp:BoundColumn><asp:BoundColumn DataField="SumBudgetCost" HeaderText="合计/预算成本"></asp:BoundColumn><asp:BoundColumn DataField="SumFactCost" HeaderText="实际成本"></asp:BoundColumn><asp:BoundColumn DataField="ReduceCost" HeaderText="降低额"></asp:BoundColumn><asp:BoundColumn HeaderText="降低率"></asp:BoundColumn><asp:BoundColumn DataField="SumCurrYearBudgetCost" HeaderText="预算成本" Visible="false"></asp:BoundColumn><asp:BoundColumn DataField="SumCurrYearFactCost" HeaderText="合计数中属于本年度的/实际成本" Visible="false"></asp:BoundColumn><asp:BoundColumn DataField="SumReduceCurrYearCost" HeaderText="降低额" Visible="false"></asp:BoundColumn><asp:BoundColumn HeaderText="降低率" Visible="false"></asp:BoundColumn></Columns></asp:DataGrid>
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
