<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PayoutContract.ascx.cs" Inherits="ContractManage_UserControl_PayoutContract" %>


<script type="text/javascript">
    addLoadEvent(function(){
        var inputs = document.getElementsByTagName('INPUT');
        for(var i = 0; i < inputs.length; i++){
            if(inputs[i].id.indexOf('txtContractName') >= 0){
                inputs[i].setAttribute('readOnly','readOnly');
            }
        }
    });
    function selectContract(id){
        var contractId = id.replace('txtContractName','hfldContractId')
        winopen('/ContractManage/UserControl/PayoutContract.aspx?ContractName=' + id + '&ContractId=' + contractId + '&Random=' + new Date().getTime());
    }
    function addLoadEvent(func){
        var oldonload = window.onload;
        if(typeof(oldonload) != 'function'){
            window.onload = func;
        }
        else{
            oldonload;
            func();
        }
    }
</script>

<asp:TextBox ID="txtContractName" ondblclick="selectContract(this.id);" Style="background-color: #FFFFC0;" runat="server"></asp:TextBox>
<asp:HiddenField ID="hfldContractId" runat="server" />
