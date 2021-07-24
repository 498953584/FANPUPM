<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editlinkman.aspx.cs" Inherits="EditLinkman" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>EditLinkman</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

    <script type="text/javascript" src="../../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../../Script/jw.js"></script>
    <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></head>
<body class="body_clear" scroll="no">
    <form id="Form1" method="post" runat="server">
    <font face="宋体">
        <table class="table-normal" id="Table1" cellspacing="1" cellpadding="1" width="100%" align="center" border="1" runat="server"><tr class="grid_head" runat="server"><td align="center" colspan="2" runat="server">
                    <asp:Label ID="lblTitle" runat="server">Label</asp:Label>
                </td></tr><tr height="25" runat="server"><td style="width: 127px" runat="server">
                    姓名：
                </td><td runat="server">
                    <asp:TextBox ID="tbName" MaxLength="10" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="rfvName" ControlToValidate="tbName" ErrorMessage="RequiredFieldValidator" runat="server">必填</asp:RequiredFieldValidator>
                </td></tr><tr height="25" runat="server"><td style="width: 127px" runat="server">
                    性别：
                </td><td runat="server">
                    <asp:RadioButtonList ID="rblSex" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server"><asp:ListItem Value="m" Selected="true" Text="男" /><asp:ListItem Value="f" Text="女" /></asp:RadioButtonList>
                </td></tr><tr height="25" runat="server"><td style="width: 127px" runat="server">
                    部门：
                </td><td runat="server">
                    <asp:DropDownList ID="ddlDept" AutoPostBack="true" Width="138px" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged_1" runat="server"></asp:DropDownList>
                    <input id="hdnDept" type="hidden" name="Hidden1" OnServerChange="hdnDept_ServerChange" runat="server" />

                </td></tr><tr height="25" runat="server"><td style="width: 127px" runat="server">
                    序号：
                </td><td runat="server">
                    <asp:TextBox ID="tbSerial" MaxLength="6" runat="server"></asp:TextBox><input id="hdnSerial" type="hidden" name="Hidden1" runat="server" />

                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ErrorMessage="序号必须是数字" ControlToValidate="tbSerial" ValidationExpression="\d{1,4}" runat="server"></asp:RegularExpressionValidator>
                </td></tr><tr height="25" runat="server"><td style="width: 127px" id="zhiwu" runat="server">
                    办公电话（内线）
                </td><td runat="server">
                    <asp:TextBox ID="tbDuty" MaxLength="20" runat="server"></asp:TextBox>
                </td></tr><tr height="25" runat="server"><td style="width: 127px" runat="server">
                    通讯地址：
                </td><td runat="server">
                    <asp:TextBox ID="tbAddress" MaxLength="20" runat="server"></asp:TextBox>
                </td></tr><tr height="25" runat="server"><td style="width: 127px" runat="server">
                    邮编：
                </td><td runat="server">
                    <asp:TextBox ID="tbPostalcode" MaxLength="6" runat="server"></asp:TextBox><asp:RegularExpressionValidator ID="revCode" ControlToValidate="tbPostalcode" ErrorMessage="RegularExpressionValidator" ValidationExpression="\d{6}" runat="server">6位数字</asp:RegularExpressionValidator>
                </td></tr><tr height="25" runat="server"><td style="width: 127px; height: 25px;" runat="server">
                    办公电话（外线）：
                </td><td style="height: 25px" runat="server">
                    <asp:TextBox ID="tbCorpPhone" MaxLength="20" runat="server"></asp:TextBox>
                </td></tr><tr height="25" runat="server"><td style="width: 127px" runat="server">
                    传真：
                </td><td runat="server">
                    <asp:TextBox ID="tbFax" MaxLength="20" runat="server"></asp:TextBox>
                </td></tr><tr height="25" runat="server"><td style="width: 127px" runat="server">
                    住宅电话：
                </td><td runat="server">
                    <asp:TextBox ID="tbHomePhone" MaxLength="20" runat="server"></asp:TextBox><asp:CheckBox ID="cbZdbs" Text="保密" runat="server" />
                </td></tr><tr height="25" runat="server"><td style="width: 127px" runat="server">
                    手机：
                </td><td runat="server">
                    <asp:TextBox ID="tbHandset" MaxLength="20" runat="server"></asp:TextBox><asp:CheckBox ID="cbSjbs" Text="保密" runat="server" />
                </td></tr><tr height="25" runat="server"><td style="width: 127px" runat="server">
                    E-mail:
                </td><td runat="server">
                    <asp:TextBox ID="tbEmail" MaxLength="20" runat="server"></asp:TextBox><asp:RegularExpressionValidator ID="revEmail" ControlToValidate="tbEmail" ErrorMessage="RegularExpressionValidator" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server">不合法</asp:RegularExpressionValidator>
                </td></tr><tr height="25" runat="server"><td style="width: 127px" runat="server">
                    QQ:
                </td><td runat="server">
                    <asp:TextBox ID="tbQQ" MaxLength="15" runat="server"></asp:TextBox>
                </td></tr><tr height="70" runat="server"><td style="width: 127px" runat="server">
                    备注：
                </td><td runat="server">
                    <asp:TextBox ID="tbContent" Width="272px" Height="76px" TextMode="MultiLine" MaxLength="50" runat="server"></asp:TextBox>
                </td></tr><tr height="25" runat="server"><td colspan="2" align="center" class="td-submit" runat="server">
                    <asp:Button ID="btnConfirm" Text=" 确 定 " CssClass="button-normal" OnClick="btnConfirm_Click" runat="server" />&nbsp;
                    <input id="btnClose" type="button" value=" 取 消 " class="button-normal" onclick="top.ui.closeWin();" runat="server" />
&nbsp;
                </td></tr></table>
         <asp:HiddenField ID="hfldDepId" runat="server" />
    </font>
    </form>
</body>
</html>
