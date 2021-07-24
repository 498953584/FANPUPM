<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DirectTargetCostTab.aspx.cs" Inherits="DirectTargetCostTab" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>DirectTargetCostTab</title>
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
                    直接目标成本总表</td>
            </tr>
          
            <tr>
                <td valign="middle" align="center" colspan="3" height="22">
                    <table id="Tablea" height="22" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr class="report_grid_head" id="TrHeader" bgColor="Gainsboro" >
                            <td width="13%">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            <td align="right" width="63%">
                                工程名称：<asp:DropDownList ID="ddlPrjname" Width="150px" runat="server"></asp:DropDownList>
                                日期：
                                <JWControl:DateBox ID="DtbStartDate" Width="70px" runat="server"></JWControl:DateBox>&nbsp;-
                                <JWControl:DateBox ID="DtbEndDate" Width="70px" runat="server"></JWControl:DateBox>&nbsp;
                            </td>
                            <td align="left" width="24%">
                                <asp:Button ID="BtnSearch" CssClass="Btn_Search" Text="" OnClick="BtnSearch_Click" runat="server" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="20" valign=top>
                    <table id="Tablexxx" cellspacing="0" cellpadding="0" width="100%" border="0" height=20>
                        <tr class="report_grid_head" id="TrHeader" bgColor="Gainsboro" >
                            <td width="45%">
                                工程名称：<asp:Label ID="lblPrjName" runat="server"></asp:Label></td>
                            <td width="25%">
                                项目经理：</td>
                            <td width="20%">
                                日期：</td>
                            <td width="10%">
                                单位：元</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top" colspan="3">
                    <div class="gridBox">
                    <table class="table-normal" id="Table2" cellspacing="0" cellpadding="0" width="100%" runat="server"><tr runat="server"><td runat="server">
                        <asp:DataGrid ID="DGrdMaterialExpend" CssClass="grid" Width="100%" AutoGenerateColumns="false" CellPadding="0" BorderWidth="0px" BorderColor="#ACA899" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle Height="20px" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn DataField="NodeName" HeaderText="项   目"></asp:BoundColumn><asp:BoundColumn DataField="TargetCost" HeaderText="目标成本"></asp:BoundColumn><asp:BoundColumn DataField="FactCost" HeaderText="实际发生的成本"></asp:BoundColumn><asp:BoundColumn DataField="CostBias" HeaderText="差 异"></asp:BoundColumn><asp:BoundColumn DataField="BiasRemark" HeaderText="差异说明"></asp:BoundColumn></Columns></asp:DataGrid>
                        </td></tr></table>
        </DIV></TD></TR></TABLE>
    </form>
</body>
</html>
