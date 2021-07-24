<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PickTreasury.aspx.cs" Inherits="StockManage_Purchase_PickTreasury" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>选择仓库</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script type="text/javascript" src="../script/Config2.js"></script>

    <script type="text/javascript" src="../script/JustWinTable.js"></script>

    <script type="text/javascript">

        addEvent(window, 'load', function() {
            addEvent(document.getElementById('btnOk'), 'click', btnOkEvent);
            var jwTable = new JustWinTable('gvwTreasury');
            jwTable.registClickTrListener(function() {
                document.getElementById('hfldTreasuryCode').value = this.id;
                document.getElementById('hfldTreasuryName').value = this.firstChild.firstChild.nodeValue;
            })
            jwTable.registDbClickListener(function() {
                var treasuryName = this.firstChild.firstChild.nodeValue;
                backParentWinow(this.id, treasuryName);
                window.close();
            });
        });

        function btnOkEvent() {
            var btnOk = document.getElementById('btnOk');
            if (!btnOk) return false;
            backParentWinow(document.getElementById('hfldTreasuryCode').value, document.getElementById('hfldTreasuryName').value);
            window.close();
        }

        function backParentWinow(treasuryCode, treasuryName) {
            var parent = window.opener.document;
            var elements = parent.getElementsByTagName('*');
            for (var i = 0; i < elements.length; i++) {
                if (elements[i].id.indexOf('txtTreasuryName') > 0) {
                    elements[i].value = treasuryName;
                }
                if (elements[i].id.indexOf('hfldTreasuryCode') > 0) {
                    elements[i].value = treasuryCode;
                }
            }
        }
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table class="tab">
        <tr style="height: 20px;">
            <td class="headerrow">
                仓库名称设置
            </td>
        </tr>
        <tr style="height: 300px">
            <td style="width: 100%; vertical-align: top">
                <div class="pagediv">
                    <asp:GridView ID="gvwTreasury" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" OnRowDataBound="gvTreasury_RowDataBound" DataKeyNames="tcode" runat="server"><Columns><asp:BoundField DataField="tname" HeaderText="仓库名称" /></Columns></asp:GridView>
                </div>
            </td>
        </tr>
        <tr style="height: 24px;">
            <td class="bottonrow" style="text-align: right; padding-right: 5px;">
                <input id="btnOk" type="button" value="确定" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfldTreasuryCode" runat="server" />
    <asp:HiddenField ID="hfldTreasuryName" runat="server" />
    </form>
</body>
</html>
