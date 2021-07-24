<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FunctionManager.aspx.cs" Inherits="HR_Organization_FunctionManager" %>
<%@ Register TagPrefix="MyUserControl" TagName="hr_organization_usercontrol_uctechnology_ascx" Src="~/HR/Organization/UserControl/UcTechnology.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="hr_organization_usercontrol_ucmanager_ascx" Src="~/HR/Organization/UserControl/UcManager.ascx" %>

<html>
<head id="Head1" runat="server"><title></title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/jquery.easyui.extension.css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript">
        function ClickRow(t, RecordID) {
            if (parseInt(t) == 1) {
                $('#UcManager_btnEdit').removeAttr('disabled');
                $('#UcManager_btnDel').removeAttr('disabled');
                $('#UcManager_HdnRecordID').val(RecordID);

            }
            if (parseInt(t) == 2) {
                $('#UcTechnology_btnEdit').removeAttr('disabled');
                $('#UcTechnology_btnDel').removeAttr('disabled');
                $('#UcTechnology_HdnRecordID').val(RecordID);
            }
        }
        function OpenManageWin(op) {
            var RecordID = "";
            if (op != 'add') {
                RecordID = document.getElementById('UcManager_HdnRecordID').value;
            }
            var url = "/HR/Organization/FunctionManagerEdit.aspx?t=1&op=" + op + "&rid=" + RecordID;

            top.ui.openWin({ title: '职级', url: url });
        }
        function OpenTechnologyWin(op) {
            var RecordID = "";
            if (op != 'add') {
                RecordID = document.getElementById('UcTechnology_HdnRecordID').value;
            }
            var url = "/HR/Organization/FunctionManagerEdit.aspx?t=2&op=" + op + "&rid=" + RecordID;
            top.ui.openWin({ title: '职级', url: url });
        }
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
    <table width="100%" height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex"
        style="table-layout: fixed;">
        <tr>
            <td style="">
                <div id="tt2" class="easyui-tabs" style="" fit="true">
                    <div title="职称类" style="">
                        <MyUserControl:hr_organization_usercontrol_ucmanager_ascx ID="UcManager" runat="server" />
                    </div>
                    <div title="岗位证书" style="overflow: auto;">
                        <MyUserControl:hr_organization_usercontrol_uctechnology_ascx ID="UcTechnology" runat="server" />
                    </div>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
