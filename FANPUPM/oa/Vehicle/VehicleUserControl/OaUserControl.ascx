<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OaUserControl.ascx.cs" Inherits="oa_Vehicle_VehicleUserControl_OaUserControl" %>


<script type="text/javascript">

    //选择人员
    function selectUser() {
        document.getElementById("divFramPrj").title = "选择人员";
        document.getElementById("ifFramPrj").src = "/Common/DivSelectUser.aspx?Method=returnUser";
        selectnEvent('divFramPrj');
        //选择人员返回值
    }
    function returnUser(id, name) {
        document.getElementById('hfldTypeID').value = id;
        document.getElementById('txtPurchaser').value = name;
    }
</script>
<span id="spanContractType" style="width: 100%; background-color: #FEFEF4;" class="spanSelect" runat="server">
    <asp:TextBox ID="txtPurchaser" Style="height: 15px; border: none; line-height: 16px; width: 89%;
        margin: 1px 2px;" CssClass="{required:true, messages:{required:'车辆购买人必须输入'}}" runat="server"></asp:TextBox>
    <img style="float:right;" src="../../../images/icon.bmp" id="img" alt="请选择车辆购买人" onclick="selectUser();" runat="server" />

<asp:HiddenField ID="hfldTypeID" runat="server" />
</span>
