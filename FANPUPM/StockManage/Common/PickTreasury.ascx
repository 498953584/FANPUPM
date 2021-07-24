<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PickTreasury.ascx.cs" Inherits="StockManage_Purchase_PickTreasury" %>

<script type="text/javascript">
    $(document).ready(function () {
        $('input[id$=txtTreasuryName]').width($('span[id$=spanContractType]').width() - 25);
    });

    function pickTreasury(element) {
        var inputId = element.id;
        if (element.nodeName == 'IMG') {
            inputId = element.previousSibling.previousSibling.id;
        }
        var hfldId = inputId.substring(0, inputId.indexOf('_') + 1) + 'hfldTreasuryCode';
        var parameters = 'tshow=' + inputId + '&hid=' + hfldId + '&no=' + '-1';
        if (element.getAttribute('Module')) {
            parameters += ('&Module=' + element.getAttribute('Module'));
        }
        var url = "/StockManage/Allocation/SelectDepository.aspx?" + parameters;

        top.ui.callback = function (obj) {
            $('#' + hfldId).val(obj.code)
            $('#' + inputId).val(obj.name);
        }

        top.ui.openWin({ title: '选择仓库', url: url, width: 600, height: 485 });
    }

</script>
<span id="spanContractType" class="spanSelect" style="margin: 0px;
    padding: 0px;" runat="server">
    <asp:TextBox ID="txtTreasuryName" CssClass="txtreadonly" ContentEditable="false" Style="height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
    <img id="img" alt="请选择仓库" src="../../images/icon.bmp" style="float: right;" onclick="pickTreasury(this);" runat="server" />

</span>
<asp:HiddenField ID="hfldTreasuryCode" runat="server" />
<asp:HiddenField ID="hfldWidth" runat="server" />
