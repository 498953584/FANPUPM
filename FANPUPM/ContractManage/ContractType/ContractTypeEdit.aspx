<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContractTypeEdit.aspx.cs" Inherits="ContractManage_ContractType_ContractTypeEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>合同类型</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var action = getRequestParam('Action');
            if (action == 'Query') {
                setAllInputDisabled();
            }
            $('#btnSave').bind('click', validate);
            $('#txtTypeShort').keyup(function () {
                $(this).val($(this).val().replace(/[^\a-\z\A-\Z]/g,''));
            });
        });
        function validate() {
            if (!Trim($('#txtTypeName').val())) {
                alert('系统提示：\n\n合同类型必须输入！');
                $('#txtTypeName').focus();
                return false;
            }
            if (!Trim($('#txtTypeShort').val())) {
                alert('系统提示：\n\n合同类型简写必须输入！');
                $('#txtTypeShort').focus();
                return false;
            }
            return true;
        }
    </script>
    <style type="text/css">
        .desTd
        {
            width: 19%;
            text-align: right;
        }
        .elemTd
        {
            width: 31%;
            text-align: left;
        }
        .elemTd input
        {
            width: 100%;
        }
        table[id=content] tr
        {
            height: 30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divContent2">
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    类型编码
                </td>
                <td class="elemTd readonly">
                    <asp:TextBox ID="txtTypeCode" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    <asp:Label ID="lblCBS" Text="类型名称" runat="server"></asp:Label>
                </td>
                <td class="txt  mustInput">
                    <!-- 隐藏成本科目 Bery 2010-03-26 本字段由项目部要求添加, 但后来忘记了需求, 故隐藏 -->
                    <asp:DropDownList ID="dropCBSCode" Visible="false" Width="100%" runat="server"></asp:DropDownList>
                    <asp:TextBox ID="txtTypeName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr style="display: none">
                <td class="word">
                    类型名称
                </td>
                <td colspan="3" style="padding-right: 3px">
                    <asp:TextBox ID="txtTypeName2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    类型简写
                </td>
                <td class="txt  mustInput">
                    <asp:TextBox ID="txtTypeShort" MaxLength="20" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    直接成本
                </td>
                <td style="padding-right: 3px">
                    <asp:DropDownList ID="ddlCBS" Width="100%" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="word">
                    备注
                </td>
                <td colspan="3" style="padding-right: 3px">
                    <asp:TextBox ID="txtNotes" TextMode="MultiLine" Rows="4" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    编制人员
                </td>
                <td class="elemTd readonly">
                    <asp:TextBox ID="txtInputPerson" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    编制日期
                </td>
                <td class="elemTd readonly">
                    <asp:TextBox ID="txtInputDate" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                    <input id="btnCancel" type="button" value="取消" onclick="top.ui.closeTab();" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
