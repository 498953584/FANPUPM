<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplicationReq.aspx.cs" Inherits="HR_Leave_ApplicationReq" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>审核窗口</title>

    <script type="text/javascript" language="javascript">
    <!--
    window.name = "win";
       -->
    </script>

</head>
<body class="body_clear" scroll="no">
    <form id="form1" target="win" runat="server">
        <div>
            <table class="table-normal" cellspacing="0" cellpadding="0" border="1" style="width: 386px">
                <tr>
                    <td class="td-label" style="height: 22px" >
                        审 核 人:</td>
                    <td style="width: 310px; height: 22px" >
                        &nbsp;
                        <asp:Label ID="labSHperson" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="td-label" style="height: 22px" >
                        是否同意:</td>
                    <td style="height: 22px; width: 310px;" >
                        <asp:RadioButtonList ID="RadioButtonList1" RepeatDirection="Horizontal" runat="server"><asp:ListItem Value="1" Selected="true" Text="同意" /><asp:ListItem Value="-1" Text="不同意" /></asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="td-label" style="height: 28px" >
                        审核日期:</td>
                    <td style="width: 310px; height: 28px"  >
                        &nbsp;
                        <asp:Label ID="labDate" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="td-label" style="height: 26px" >
                        审核意见:</td>
                    <td style="width: 310px; height: 26px"  >
                        <asp:TextBox ID="txtldyj" MaxLength="250" Rows="4" TextMode="MultiLine" Height="55px" Width="304px" runat="server"></asp:TextBox></td>
                   
            </table>
        </div>
        <table>
         <tr>
                        <td colspan="2" class="td-submit" style="height: 62px; width: 378px;" align=right>
                            <asp:Button ID="BtnSave" Text="提 交" OnClick="BtnSave_Click" runat="server" />&nbsp;<input
                                id="BtnClose" onclick="window.close();" type="button" value="取 消" />&nbsp; &nbsp;
                        </td>
                    </tr>
        </table>
        <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
