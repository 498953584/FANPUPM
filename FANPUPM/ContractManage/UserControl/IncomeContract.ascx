<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IncomeContract.ascx.cs" Inherits="ContractManage_UserControl_IncomeContract" %>

<link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />

<script type="text/javascript" src="/Script/jquery.js"></script>

<script type="text/javascript" src="/Script/jquery.cookie.js"></script>

<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>

<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>

<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>

<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>

<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>

<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>

<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>

<script type="text/javascript">
    function selectContract(element) {
        var id = element.id;
        if (element.nodeName == 'IMG') {
            id = element.previousSibling.previousSibling.id;
        }
        var contractId = id.replace('txtContractName', 'hfldContractId');
        var cont = new Array();
        cont[0] = '';
        cont[1] = '';
        var url = "/Common/SelectIncometCont.aspx";
        window.showModalDialog(url, cont, "dialogHeight:400px;dialogWidth:600px;center:1;help:0;status:0;");
        if (cont[0] != '') {
            document.getElementById(contractId).value = cont[0];
            document.getElementById(id).value = cont[1];
        }
        //winopen('/ContractManage/UserControl/IncomeContract.aspx?ContractName=' + id + '&ContractId=' + contractId + '&Random=' + new Date().getTime());
    }
</script>

<span id="spanIncomeContract" class="spanSelect">
    <asp:TextBox ID="txtContractName" Style="height: 15px; border: none; line-height: 16px;
        margin: 1px 2px;" runat="server"></asp:TextBox>
    <img id="img" alt="请选择收入合同" src="/images1/iconSelect.gif" onclick="selectContract(this);" runat="server" />

</span>
<div id="divSelectIncomeContract" title="选择收入合同" style="display: none">
    <iframe id="ifResouece" frameborder="0" width="100%" height="100%" src="/Common/SelectIncometCont.aspx">
    </iframe>
</div>
<asp:HiddenField ID="hfldContractId" runat="server" />
