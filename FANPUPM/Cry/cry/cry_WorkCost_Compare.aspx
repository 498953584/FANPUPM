<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cry_WorkCost_Compare.aspx.cs" Inherits="WorkCost_Compare" %>



<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>cry</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
</head>
<body class="body_clear" scroll="no">
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
                    <font face="宋体">其它成本差异分析表 </font>
                </td>
            </tr>
            <tr>
                <td height="22">
                    <table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr class="report_grid_head" id="TrHeader" bgcolor="Gainsboro">
                            <td height="22">
                                <font face="宋体">单位工程名称</font>：
                                <asp:DropDownList ID="ddl_pn" AutoPostBack="true" Width="224px" runat="server"></asp:DropDownList></td>
                            <td align="right" height="22">
                                <asp:DropDownList ID="ddl_year" AutoPostBack="true" Visible="false" runat="server"></asp:DropDownList></td>
                            <td align="left" height="22">
                                <asp:DropDownList ID="ddl_month" AutoPostBack="true" Visible="false" runat="server"></asp:DropDownList></td>
                            <td align="right" height="22">
                                &nbsp;单位：万元</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <font face="宋体">
                        <table class="table-normal" id="Table5" cellspacing="0" cellpadding="0" width="100%" runat="server"><tr runat="server"><td runat="server">
                                <asp:DataGrid ID="dg_ManPower" Width="100%" CssClass="grid" AutoGenerateColumns="false" AllowCustomPaging="true" PageSize="6" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle Height="20px" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn DataField="xh" HeaderText="序号"></asp:BoundColumn><asp:BoundColumn DataField="xm" HeaderText="项目"></asp:BoundColumn><asp:BoundColumn DataField="sj" HeaderText="实际" DataFormatString="{0:0.0000}"></asp:BoundColumn><asp:BoundColumn DataField="mb" HeaderText="目标" DataFormatString="{0:0.0000}"></asp:BoundColumn><asp:BoundColumn DataField="cy" HeaderText="差异" DataFormatString="{0:0.0000}"></asp:BoundColumn><asp:BoundColumn DataField="bz" HeaderText="备注"></asp:BoundColumn></Columns></asp:DataGrid>
                                </td></tr></table>
                        </font></td>
            </tr>
            <tr>
                <td>
                    <font face="宋体">
                        <table id="Table2" cellspacing="1" cellpadding="1" width="100%" border="0">
                            <tr>
                                <td width="30%">
                                    企业负责人：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>
                                </td>
                                <td width="30%">
                                    财会负责人：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>
                                </td>
                                <td>
                                </td>
                                <td align="right" width="20%">
                                    制表人：
                                    <asp:Label ID="Lb_userName" Font-Underline="true" runat="server">Label</asp:Label>&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </font>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
