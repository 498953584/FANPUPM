<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VotingManageEdit.aspx.cs" Inherits="oa_Voting_VotingManageEdit" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>增加项目信息</title>
    
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript">
        function selDept(userCode) {
            var _callback = top.ui.callback;
            var url = "/oa/common/selDept.aspx?yhdm=00000000&dept=0";
            top.ui.openWin({ title: '选择人员', url: url, width: 400, height: 600, winNo: 2 });
            top.ui.callback = function (user) {
                $('#hdnDept').val(user.code);
                $('#tbDept').val(user.name);
                top.ui.callback = _callback;
            }
            //            var p = new Array();
            //            p[0] = "";
            //            p[1] = "";
            //            var dept = document.getElementById('hdnDept').value;
            //            var url = "../common/selDept.aspx?yhdm=" + userCode + "&dept=" + dept;
            //            var str = window.showModalDialog(url, window, 'dialogHeight:600px;dialogWidth:350px;center:1;help:0;status:0;');
            //            if (str == undefined) {
            //            }
            //            else {
            //                if (str != "") {
            //                    var index = str.indexOf("*");
            //                    document.getElementById('hdnDept').value = str.substring(0, index);
            //                    document.getElementById('tbDept').value = str.substr(index + 1);
            //                    //              document.getElementById('hdnDept').value = p[0];
            //                    //		        document.getElementById('tbDept').value = p[0];
            //                    str = "";
            //                }
            //            }
        }
    </script>
</head>
<body scroll="no" class="body_popup">
    <form id="Formx" method="post" runat="server">
    <table class="table-form" id="tablex" cellspacing="0" cellpadding="0" width="100%"
        border="1">
        <tr>
            <td class="td-head" colspan="2" height="20">
                增加项目信息
            </td>
        </tr>
        <tr>
            <td class="td-label">
                项目名称
            </td>
            <td>
                <asp:TextBox ID="txtzdname" CssClass="text-input" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-label">
                备注
            </td>
            <td>
                <asp:TextBox ID="txtRemark" CssClass="text-input" Rows="3" TextMode="MultiLine" Width="100%" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-label">
                范围：
            </td>
            <td colspan="2">
                <asp:TextBox ID="tbDept" ReadOnly="true" runat="server"></asp:TextBox>
                <input type="hidden" id="hdnDept" name="hdnDept" style="width: 1px" runat="server" />
<input id="BtnSel" class="button-normal" style="width: 100px" type="button" value="选 择" runat="server" />

            </td>
        </tr>
        <tr>
            <td class="td-label">
                投票方式：
            </td>
            <td>
                <asp:RadioButtonList ID="RBL" RepeatDirection="Horizontal" CellPadding="5" CellSpacing="5" BorderStyle="None" runat="server"><asp:ListItem Value="0" Selected="true" Text="单选" /><asp:ListItem Value="1" Text="多选" /></asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="td-submit" colspan="2" height="20">
                <input type="hidden" id="hdnRecordId" name="hdnRecordId" style="width: 10px" runat="server" />

                <asp:Button ID="btnAdd" Text="保  存" OnClick="btnAdd_Click" runat="server" />&nbsp;
                <input id="BtnClose" onclick="javascript:returnValue=false;window.close();" type="button"
                    value="关  闭">
                <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
