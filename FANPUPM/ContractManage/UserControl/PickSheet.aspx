<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PickSheet.aspx.cs" Inherits="ContractManage_UserControl_PickSheet" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>关联单据</title>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jwJson.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwSheet = new JustWinTable("tableSheet");

            jwSheet.registClickTrListener(function () {
                document.getElementById('hfldSheets').value = this.id;
                document.getElementById('btnSave').removeAttribute('disabled');
            });
            jwSheet.registClickAllCHKLitener(function () {
                if (jwSheet.isCheckedAll()) {
                    document.getElementById('hfldSheets').value =
                        jwSheet.getCheckedChkIdJson();
                    document.getElementById('btnSave').removeAttribute('disabled');
                } else {
                    document.getElementById('hfldSheets').value = '';
                    document.getElementById('btnSave').setAttribute('disabled', 'disabled');
                }
            });
            jwSheet.registClickSingleCHKListener(function () {
                if (jwSheet.getCheckedChk().length > 0) {
                    document.getElementById('hfldSheets').value =
                        jwSheet.getCheckedChkIdJson(jwSheet.getCheckedChk());
                    document.getElementById('btnSave').removeAttribute('disabled');
                } else {
                    document.getElementById('hfldSheets').value = '';
                    document.getElementById('btnSave').setAttribute('disabled', 'disabled');
                }
            });

            jwSheet.registDbClickListener(save);
        });

        function save() {
            var sheetIds = document.getElementById('hfldSheets').value;
            var sheetNames = '';
            $.ajax({
                type: 'GET',
                async: false,
                url: '/ContractManage/Handler/GetSheetNames.ashx?' + new Date().getTime() + '&SheetIds=' + sheetIds,
                success: function (data) {
                    if (data != '0') {
                        var objSheetNames = eval(data);
                        for (var i = 0; i < objSheetNames.length; i++) {
                            sheetNames += objSheetNames[i] + ',';
                        }
                    }
                }
            });

            if (typeof top.ui.callback == 'function') {
                top.ui.callback({ id: sheetIds, name: sheetNames });
                top.ui.callback = null;
            }
            top.ui.closeWin();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="pagediv" style="windth: 80%; height: 90%;">
        <asp:Repeater ID="rptSheet" runat="server">
<HeaderTemplate>
                <table id="tableSheet" class="gvdata" cellspacing="0" rules="all" border="1" style="border-collapse: collapse;">
                    <tr class="header" style="color: #727faa;">
                        <th style="width: 20px;">
                            <input type="checkbox" id="chkAll" />
                        </th>
                        <th style="width: 25px">
                            序号
                        </th>
                        <th>
                            名称
                        </th>
                        <th>
                            备注
                        </th>
                    </tr>
            </HeaderTemplate>

<ItemTemplate>
                <tr class="rowa" id='<%# Eval("ID") %>'>
                    <td>
                        <input type="checkbox" id="chk" />
                    </td>
                    <td>
                        <%# Container.ItemIndex + 1 %>
                    </td>
                    <td title='<%# Eval("ItemName") %>'>
                        <%# StringUtility.GetStr(Eval("ItemName"), 15) %>
                    </td>
                    <td title='<%# Eval("Remark") %>'>
                        <%# StringUtility.GetStr(Eval("Remark"), 25) %>
                    </td>
                </tr>
            </ItemTemplate>

<AlternatingItemTemplate>
                <tr class="rowb" id='<%# Eval("ID") %>'>
                    <td>
                        <input type="checkbox" id="chk" />
                    </td>
                    <td>
                        <%# Container.ItemIndex + 1 %>
                    </td>
                    <td title='<%# Eval("ItemName") %>'>
                        <%# StringUtility.GetStr(Eval("ItemName"), 15) %>
                    </td>
                    <td title='<%# Eval("Remark") %>'>
                        <%# StringUtility.GetStr(Eval("Remark"), 25) %>
                    </td>
                </tr>
            </AlternatingItemTemplate>

<FooterTemplate>
                </table>
            </FooterTemplate>
</asp:Repeater>
    </div>
    <hr />
    <div style="text-align: right;">
        <input type="button" id="btnSave" value="保存" disabled="disabled" onclick="save()" />
        <input type="button" id="btnCancel" value="取消" onclick="top.ui.closeWin();" />
    </div>
    <asp:HiddenField ID="hfldSheets" runat="server" />
    </form>
</body>
</html>
