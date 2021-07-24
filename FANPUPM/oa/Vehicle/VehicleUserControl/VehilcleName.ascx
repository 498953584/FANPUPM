<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VehilcleName.ascx.cs" Inherits="oa_Vehicle_VehicleUserControl_VehilcleName" %>


<script type="text/javascript">
    addLoadEvent(function() {
        if ($('span[id$=spanVehicleName]').width() < 150) {
            $('input[id$=txtVehicleType]').width($('span[id$=spanVehicleName]').width() - 27);
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
    function selectVehicleName(element) {
        var VehicleName_Control = element.id;
        if (element.nodeName == 'IMG') {
            VehicleName_Control = element.previousSibling.previousSibling.id;
        }
        var typeIdControl = VehicleName_Control.replace('txtVehicleName', 'hfldVehicleName');
        document.getElementById('ifrmselectVehicleName').src = '/oa/Vehicle/VehicleUserControl/VehilcleName.aspx?VehicleName_Control=' + VehicleName_Control + '&TypeIdControl=' + typeIdControl;
        $('#divselectVehicleName').dialog({ width: 750, height: 450, modal: true });
        return false;
        winopen('/oa/Vehicle/VehicleUserControl/VehilcleName.aspx?VehicleName_Control=' + VehicleName_Control + '&TypeIdControl=' + typeIdControl)
    }
    function addLoadEvent(func) {
        var oldonload = window.onload;
        if (typeof (oldonload) != 'function') {
            window.onload = func;
        }
        else {
            oldonload;
            func();
        }
    }
</script>

<span id="spanVehicleName" class="spanSelect" runat="server">
    <asp:TextBox ID="txtVehicleName" Style="height: 15px; border: none; line-height: 16px; width: 89%;
        margin: 1px 2px;" CssClass="{required:true, messages:{required:'车辆户主必须输入'}}" runat="server"></asp:TextBox>
    <img style="float:right;" src="../../../images/icon.bmp" id="img" alt="请选择车辆户主" onclick="selectVehicleName(this);" runat="server" />

</span>
<div id="divselectVehicleName" title="请选择车辆户主" style="display: none">
    <iframe id="ifrmselectVehicleName" frameborder="0" width="100%" height="100%"></iframe>
</div>
<asp:HiddenField ID="hfldVehicleName" runat="server" />
