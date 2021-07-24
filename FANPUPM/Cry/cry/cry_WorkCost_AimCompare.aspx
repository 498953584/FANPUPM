<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cry_WorkCost_AimCompare.aspx.cs" Inherits="WorkCost_AimCompare" %>



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
                    <font face="宋体">目标成本差异汇总表 </font>
                </td>
            </tr>
            <tr>
                <td height="22" valign=top>
                    <table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0" height="22">
                        <tr class="report_grid_head" id="TrHeader" bgcolor="Gainsboro">
                            <td height="22">
                                <font face="宋体">单位工程名称</font>：
                                <asp:DropDownList ID="ddl_pn" Width="216px" AutoPostBack="true" DESIGNTIMEDRAGDROP="19" runat="server"></asp:DropDownList></td>
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
                    <table class="table-normal" id="Tabledc" cellspacing="0" cellpadding="0" width="100%" runat="server"><tr runat="server"><td runat="server">
                    <asp:DataGrid ID="dg_ManPower" Width="100%" PageSize="6" AllowCustomPaging="true" AutoGenerateColumns="false" CssClass="grid" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle Height="20px" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn DataField="xh" HeaderText="序号"></asp:BoundColumn><asp:BoundColumn DataField="xm" HeaderText="成本项目"></asp:BoundColumn><asp:BoundColumn DataField="sj" HeaderText="实际成本" DataFormatString="{0:0.0000}"></asp:BoundColumn><asp:BoundColumn DataField="mb" HeaderText="目标成本" DataFormatString="{0:0.0000}"></asp:BoundColumn><asp:BoundColumn DataField="cy" HeaderText="差异金额" DataFormatString="{0:0.0000}"></asp:BoundColumn><asp:BoundColumn DataField="cyl" HeaderText="差异率" DataFormatString="{0:0.00%}"></asp:BoundColumn></Columns></asp:DataGrid>
                    </td></tr></table>
                        </font></td>
            </tr>
            <tr>
                <td>
                    <font face="宋体">
                        <table id="Table2" cellspacing="1" cellpadding="1" width="100%" border="0">
                            <tr>
                                <td width="25%">
                                    企业负责人：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>
                                </td>
                                <td width="190" style="width: 190px">
                                    财会负责人：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>
                                </td>
                                <td>
                                </td>
                                <td align="right" width="20%">
                                    制表人：
                                    <asp:Label ID="Lb_userName" Font-Underline="true" runat="server">Label</asp:Label>&nbsp;
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
