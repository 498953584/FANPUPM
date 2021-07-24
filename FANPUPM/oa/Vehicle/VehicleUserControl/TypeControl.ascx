<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TypeControl.ascx.cs" Inherits="oa_Vehicle_VehicleUserControl_TypeControl" %>

<script type="text/javascript">
    $(document).ready(function () {
        // 设置只读
        $('#txtVehicleType').attr('readonly', 'readonly');
        if ($('span[id$=spanContractType]').width() < 150) {
            $('input[id$=txtVehicleType]').width($('span[id$=spanContractType]').width() - 27);
        }
        var inputs = document.getElementsByTagName('INPUT');
        var inputWidth;
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].id.indexOf('txtVehicleType') >= 0) {
                inputs[i].setAttribute('readOnly', 'readOnly');
                inputWidth = inputs[i].style.width;
            }
        }
    });

    function selectVehicleType(element) {

        var typeNameControl = element.id;
        if (element.nodeName == 'IMG') {
            typeNameControl = element.previousSibling.previousSibling.id;
        }
        var typeIdControl = typeNameControl.replace('txtVehicleType', 'hfldVehicleTypeID');

        var url = '/oa/Vehicle/VehicleUserControl/VehicleType.aspx';

        top.ui.callback = function (t) {
            $('#' + typeNameControl).val(t.name);
            $('#' + typeIdControl).val(t.code);
        }
        top.ui.openWin({ title: '选择车辆类型', url: url, width: 350, height: 350 });
    }

</script>
<span id="spanContractType" class="spanSelect" runat="server">
    <asp:TextBox ID="txtVehicleType" Style="height: 15px; border: none; line-height: 16px;
        width: 89%; margin: 1px 2px;" CssClass="{required:true, messages:{required:'车辆类型必须输入'}}" runat="server"></asp:TextBox>
    <img style="float: right;" src="../../../images/icon.bmp" id="img" alt="请选择车辆类型" onclick="selectVehicleType(this);" runat="server" />

</span>
<div id="divSelectVehicleType" title="选择车辆类型" style="display: none">
    <iframe id="ifrmSelectVehicleType" frameborder="0" width="100%" height="100%"></iframe>
</div>
<asp:HiddenField ID="hfldVehicleTypeID" runat="server" />
