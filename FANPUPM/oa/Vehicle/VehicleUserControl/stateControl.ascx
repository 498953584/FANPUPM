<%@ Control Language="C#" AutoEventWireup="true" CodeFile="stateControl.ascx.cs" Inherits="oa_Vehicle_VehicleUserControl_stateControl" %>

<script type="text/javascript">
    $(document).ready(function () {
        if ($('span[id$=spanContractType]').width() < 150) {
            $('input[id$=txtVehicleState]').width($('span[id$=spanContractType]').width() - 27);
        }
        var inputs = document.getElementsByTagName('INPUT');
        var inputWidth;
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].id.indexOf('txtVehicleState') >= 0) {
                inputs[i].setAttribute('readOnly', 'readOnly');
                inputWidth = inputs[i].style.width;
            }
        }
    });

    function selectVehicleState(element) {

        var typeNameControl = element.id;
        if (element.nodeName == 'IMG') {
            typeNameControl = element.previousSibling.previousSibling.id;
        }
        var typeIdControl = typeNameControl.replace('txtVehicleState', 'hfldStateID');

        var url = '/oa/Vehicle/VehicleUserControl/VehicleState.aspx';
        top.ui.callback = function (t) {
            $('#' + typeIdControl).val(t.code);
            $('#' + typeNameControl).val(t.name);
        }
        top.ui.openWin({ title: '选择车辆状态', url: url });
    }
</script>
<span id="spanContractType" class="spanSelect" runat="server">
    <asp:TextBox ID="txtVehicleState" Style="height: 15px; border: none; line-height: 16px;
        width: 89%; margin: 1px 2px;" ReadOnly="true" CssClass="{required:true, messages:{required:'请选择车辆状态'}}" runat="server"></asp:TextBox>
    <img style="float: right;" src="../../../images/icon.bmp" id="img" alt="请选择车辆状态" onclick="selectVehicleState(this);" runat="server" />

</span>
<div id="divSelectState" title="请选择车辆状态" style="display: none">
    <iframe id="ifrmSelectState" frameborder="0" width="100%" height="100%"></iframe>
</div>
<asp:HiddenField ID="hfldStateID" runat="server" />
