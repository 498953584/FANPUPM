<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VehicleState.aspx.cs" Inherits="oa_Vehicle_VehicleUserControl_state" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title></title>
    <script type="text/javascript" src="../../../Script/jquery.js"></script>
    <script src="../../../StockManage/script/Config2.js" type="text/javascript"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            var anchor = document.getElementsByTagName('A');
            for (var i = 0; i < anchor.length; i++) {
                if (anchor[i].getAttribute('title') == 'ContractType') {
                    anchor[i].onclick = function () {
                        document.getElementById('btnSave').removeAttribute('disabled');
                        cancelSelect(anchor);
                        this.style.textDecoration = 'underline';
                        var typeId = this.href.substring(this.href.lastIndexOf('\\') + 1, this.href.length - 2);
                        var typeName = this.firstChild.nodeValue;

                        document.getElementById('hfldVehicleStateID').value = typeId;
                        document.getElementById('hfldVehicleStateName').value = typeName;
                        return false;
                    }
                    anchor[i].ondblclick = save;
                }
                if (anchor[i].getAttribute('title') == 'type') {
                    anchor[i].onclick = function () {
                        document.getElementById('btnSave').setAttribute('disabled', 'disabled');
                    }
                }
            }
        });


        function nonFun() {
        }

        function cancelSelect(anchor) {
            for (var i = 0; i < anchor.length; i++) {
                if (anchor[i].getAttribute('title') == 'ContractType') {
                    anchor[i].style.textDecoration = 'none';
                }
            }
        }

        function save() {
            if (top.ui.callback != null) {
                top.ui.callback({ code: $('#hfldVehicleStateID').val(), name: $('#hfldVehicleStateName').val() });
                top.ui.callback = null;
            }
            top.ui.closeWin();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="pagediv">
        <table class="tab">
            <tr style="height: 96%">
                <td style="vertical-align: top;">
                    <asp:TreeView ID="trvwControlType" ShowLines="true" OnSelectedNodeChanged="trvwControlType_SelectedNodeChanged" runat="server"><SelectedNodeStyle Font-Underline="true" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" /></asp:TreeView>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <input id="btnSave" type="button" value="保存" disabled="disabled" onclick="save();" />
                    <input type="button" id="btnCancel" value="取消" onclick="top.ui.closeWin();" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hfldVehicleStateName" runat="server" />
        <asp:HiddenField ID="hfldVehicleStateID" runat="server" />
    </div>
    </form>
</body>
</html>
