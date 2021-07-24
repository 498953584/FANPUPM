<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IncomeContract.aspx.cs" Inherits="ContractManage_UserControl_IncomeContract" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>选择收入合同</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript">
        addEvent(window,'load',function(){
            registerBtnSaveEvent();
            registerBtnCancelEvent();
            
            var contractTable = new JustWinTable('gvwContract');
            contractTable.registClickTrListener(function(){
                var btnSave = document.getElementById('btnSave');
                btnSave.setAttribute('contractId',this.id)
                btnSave.setAttribute('contractName',this.lastChild.lastChild.nodeValue);
            });
            
            contractTable.registDbClickListener(function(){
                var controlId = window.opener.document.getElementById(getRequestParam('ContractId'));
                var controlName = window.opener.document.getElementById(getRequestParam('ContractName'));
                controlId.value = this.id;
                controlName.value = this.lastChild.lastChild.nodeValue;
                window.close();
            });
        });
        
        function registerBtnSaveEvent(){
            var btnSave = document.getElementById('btnSave'); 
            addEvent(btnSave,'click',function(){
                var controlId = window.opener.document.getElementById(getRequestParam('ContractId'));
                var controlName = window.opener.document.getElementById(getRequestParam('ContractName'));
                controlId.value = this.getAttribute('contractId');
                controlName.value = this.getAttribute('contractName');
                window.close();
            });
        }
        
        function registerBtnCancelEvent(){
            var btnCancel = document.getElementById('btnCancel');
            addEvent(btnCancel,'click',function(){
                window.close();
            });
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table class="tab">
            <tr>
                <td style="vertical-align: top;">
                    <asp:GridView ID="gvwContract" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwContract_RowDataBound" DataKeyNames="ContractID" runat="server"><Columns><asp:BoundField DataField="ContractCode" HeaderText="合同编号" /><asp:BoundField DataField="ContractName" HeaderText="合同名称" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </td>
            </tr>
            <tr class="bottonrow">
                <td style="text-align: right;">
                    <input type="button" id="btnSave" value="保存" />
                    <input type="button" id="btnCancel" value="取消" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
