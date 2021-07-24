<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cry_WorkUse.aspx.cs" Inherits="WorkUse" %>



<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>cry</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
</head>
<body class="body_clear" scroll="yes">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
             <tr class="td-toolsbar">
                <td height="24">
                    <asp:Button ID="btnexecl" CssClass="button-normal" Text="导出->Excel" Width="80px" OnClick="btnexecl_Click" runat="server" />
                    <asp:Button ID="btnWord" CssClass="button-normal" Text="导出->Word" Width="80px" OnClick="btnWord_Click" runat="server" />
                </td>
            </tr>           
            <tr>
                <td class="td-title">
                    <font face="宋体">分项工程消耗对比表</font></td>
            </tr>

            <tr class="report_grid_head" id="TrHeader" bgcolor="Gainsboro">
                <td>
                    <font face="宋体">单位工程名称：
                        <asp:DropDownList ID="ddl_pn" Width="200px" AutoPostBack="true" runat="server"></asp:DropDownList></font></td>
            </tr>
            <tr>
                <td>
                <table class="table-normal" id="Table2" cellspacing="0" cellpadding="0" width="100%" runat="server"><tr runat="server"><td runat="server">
                    <font face="宋体">
                        <div style="overflow: auto; width: 100%; height: 100%">
                            <asp:DataGrid ID="dg_ManPower" PageSize="6" AllowCustomPaging="true" AutoGenerateColumns="false" CssClass="grid" Width="100%" BorderColor="Silver" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle Height="20px" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn DataField="tc" HeaderText="分项工程编号"></asp:BoundColumn><asp:BoundColumn DataField="tn" HeaderText="分项工程名称"></asp:BoundColumn><asp:BoundColumn DataField="rn" HeaderText="材料名称"></asp:BoundColumn><asp:BoundColumn DataField="rs" HeaderText="规格"></asp:BoundColumn><asp:BoundColumn HeaderText="单位" DataField="UnitName"></asp:BoundColumn><asp:BoundColumn DataField="yssl" HeaderText="预算量"></asp:BoundColumn><asp:BoundColumn DataField="sjsl" HeaderText="实际量"></asp:BoundColumn><asp:BoundColumn DataField="jcsl" HeaderText="节超"></asp:BoundColumn></Columns></asp:DataGrid></div>
                    </font>
                    </td></tr></table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
