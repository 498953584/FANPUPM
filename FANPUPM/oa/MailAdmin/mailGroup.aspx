<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mailGroup.aspx.cs" Inherits="oa_MailAdmin_mailGroup" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>无标题页</title><link href="../../CSS/site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style ="font-size :9pt;">
            <tr>
                <td style ="width :120px;" align ="right" >组名称：</td>
                <td style ="width :200px;" align ="left" >
                    <asp:TextBox ID="txtName" Width="200px" CssClass="zhengwen_input_text_no100" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style ="width :120px;" align ="right" >组描述：</td>
                <td style ="width :200px;" align ="left" >
                    <asp:TextBox ID="txtCrac" Height="31px" TextMode="MultiLine" Width="200px" CssClass="zhengwen_input_text_no100" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan ="2" align ="right" >
                    <asp:Button ID="Button1" Text="保  存" class="button-normal" Width="58px" OnClick="Button1_Click" runat="server" />
                    &nbsp;
                    <input id="Button2" type="button" value="取  消" onclick ="window.close()" class="button-normal" />
                    </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
