<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TemplateEdit.aspx.cs" Inherits="TemplateEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>流程模板维护</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/jquery.easyui.extension.css" />
<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#img").tooltip({ showURL: false });
            $('#BtnAdd')[0].onclick = function () {
                if (!$('#Form1').form('validate')) return false;
            }
        });
    </script>
</head>
<body scroll="no">
    <form id="Form1" method="post" runat="server">
    <input id="hdnBusinessCode" name="hdnBusinessCode" type="hidden" style="width: 10px" runat="server" />

    <input id="hdnBusinessClass" name="hdnBusinessClass" type="hidden" style="width: 10px" runat="server" />

    <input id="hdnIsComplete" name="hdnIsComplete" type="hidden" style="width: 10px" runat="server" />

    <table id="TableVersion" cellspacing="0" cellpadding="5" width="100%" border="1">
        <tr>
            <td class="divHeader" colspan="2" height="20">
                流程模版维护
            </td>
        </tr>
        <tr style="height: 25px">
            <td style="text-align: right">
                流程名称
            </td>
            <td>
                <asp:TextBox ID="txtTemplateName" Width="200px" CssClass="easyui-validatebox" data-options="required:true, validType:'validChars[50]'" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr style="height: 25px">
            <td style="text-align: right">
                流程类型
            </td>
            <td>
                <asp:CheckBox ID="ckbIsAbnormality" Text="废止" runat="server" />
                <img id="img" style="vertical-align: middle" alt="" title='设置流程废止时，流程完成时将不会启用' src="../../images/help.jpg" />
                &nbsp;&nbsp;
                <asp:CheckBox ID="ckbIsGeneral" Text="通用流程" TabIndex="3" Visible="false" runat="server" /><font color="#ff0000">&nbsp;</font>
            </td>
        </tr>
        <!--
				<tr id="trCorp" runat="server"><td class="td-label" width="25%" runat="server">子公司编码</td><td runat="server"><asp:DropDownList ID="ddltCorpCode" CssClass="text-input" Width="133px" DataTextField="v_bmmc" DataValueField="v_bmbm" TabIndex="4" runat="server"><asp:ListItem Text="---请选择子公司---" /></asp:DropDownList><font color="#ff0000">&nbsp;</font></td></tr>-->
        <tr style="height: 80px">
            <td style="text-align: right">
                备注
            </td>
            <td>
                <asp:TextBox ID="txtRemark" MaxLength="1000" Width="300px" TabIndex="6" Rows="5" TextMode="MultiLine" runat="server"></asp:TextBox><asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" ShowMessageBox="true" runat="server"></asp:ValidationSummary>
            </td>
        </tr>
        <tr style="height: 25px">
            <td colspan="2" class="tableFooter">
                <asp:Button ID="BtnAdd" Text="保存" runat="server" />
                <input id="BtnClose" onclick="top.ui.closeTab();"
                    type="button" value="关  闭" name="BtnClose" />
                <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
            </td>
        </tr>
        <tr id="trCorpCode" runat="server"><td runat="server">
                选择分子公司
            </td><td runat="server">
                <asp:DropDownList ID="DDLCorpCode" DataSourceID="SqlCorpCode" DataTextField="CorpName" DataValueField="CorpCode" runat="server"></asp:DropDownList>
                <asp:SqlDataSource ID="SqlCorpCode" SelectCommand="SELECT [CorpCode], [CorpName] FROM [PT_d_CorpCode]" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
            </td></tr>
        <tr style="display: none">
            <td>
                关联业务ID
            </td>
            <td>
                <asp:DropDownList ID="ddltRecordID" CssClass="text-input" Width="133px" DataTextField="DisplayName" DataValueField="RecordID" TabIndex="5" runat="server"><asp:ListItem Value="0" Text="---请选择关联业务---" /></asp:DropDownList>
                <font color="#ff0000">&nbsp;</font>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
