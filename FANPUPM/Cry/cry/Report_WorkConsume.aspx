<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report_WorkConsume.aspx.cs" Inherits="Report_WorkConsume" %>



<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Report_WorkConsume</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<body class="body_clear">
    <form id="Form1" method="post" runat="server">
        <table>
            <tr class="td-toolsbar">
                <td >
                    <asp:Button ID="btnexecl" CssClass="button-normal" Text="导出->Excel" Width="80px" OnClick="btnexecl_Click" runat="server" />
                    <asp:Button ID="btnWord" CssClass="button-normal" Text="导出->Word" Width="80px" OnClick="btnWord_Click" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <table class="table-normal" id="Table2" cellspacing="0" cellpadding="0" width="100%" runat="server"><tr class="td-title" runat="server"><td runat="server">
                                <font face="宋体">工程消耗对比表</font></td></tr><tr runat="server"><td runat="server">
                                <asp:DataGrid ID="dg_ManPower" PageSize="6" AllowCustomPaging="true" AutoGenerateColumns="false" CssClass="grid" BorderColor="Silver" Width="100%" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle Height="20px"></HeaderStyle><Columns><asp:BoundColumn DataField="TaskCode" HeaderText="分项工程编号"></asp:BoundColumn><asp:BoundColumn DataField="TaskName" HeaderText="分项工程名称"></asp:BoundColumn><asp:BoundColumn DataField="ResourceName" HeaderText="材料名称"></asp:BoundColumn><asp:BoundColumn DataField="Specification" HeaderText="规格"></asp:BoundColumn><asp:BoundColumn DataField="UnitName" HeaderText="单位"></asp:BoundColumn><asp:BoundColumn DataField="BugetNumber" HeaderText="预算量"></asp:BoundColumn><asp:BoundColumn DataField="FactNumber" HeaderText="实际量"></asp:BoundColumn><asp:BoundColumn DataField="LastNumber" HeaderText="节超"></asp:BoundColumn></Columns></asp:DataGrid>
                            </td></tr></table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
