<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JurisdictionSettings.aspx.cs" Inherits="OaSysAdminUserManageJurisdictionSettings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>管辖区域设置</title>
    <link rel="stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#tree").tree({
                data: eval($("#HidJurisdiction").val()),
                checkbox: true,
                animate: true,
            });
        });

        function getChecked() {
            var nodes = $('#tree').tree('getChecked');
            var checked = [];
            for (var i = 0; i < nodes.length; i++) {
                var attr = nodes[i].attributes;
                if (attr && attr.CityId) {
                    checked.push({
                        ProvinceId: attr.ProvinceId,
                        CityId: attr.CityId,
                        AreaId: nodes[i].id
                    });
                }
            }
            $("#HidChecked").val(JSON.stringify(checked));
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="HidJurisdiction" runat="server" />
        <asp:HiddenField runat="server" ID="HidChecked" Value=""/>
        <div style="height: 580px; overflow:auto">
            <ul id="tree" class="easyui-tree"></ul>
        </div>
        <div style="width: 98%; text-align: right; margin-top: 5px; overflow:hidden">
            <asp:Button runat="server" OnClick="BtnSave_Click" OnClientClick="getChecked()"  ID="BtnSave" Text="保存" />
            <input type="button" id="BtnCancel" value="取消" onclick="top.ui.closeWin();" />
        </div>
    </form>
</body>
</html>
