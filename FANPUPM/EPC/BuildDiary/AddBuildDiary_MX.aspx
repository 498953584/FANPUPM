<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddBuildDiary_MX.aspx.cs" Inherits="OPM_BuildDiary_AddBuildDiary_MX" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title></title>
    <script type="text/javascript">

        //取消
        function btnCancel_onclick() {
            top.ui.closeTab();
        }
        //表单验证
        function valForm() {
            var num = document.getElementById('txtWorkersCount').value;

            if (document.getElementById("txtTaskName").value == "") {
                top.ui.alert("任务名称必须输入！");
                document.getElementById("txtTaskName").focus();
                return false;
            }
            if (num == "") {
                top.ui.alert("工作人数不能为空！");
                document.getElementById('txtWorkersCount').focus();
                return false;
            }
            else if (isNaN(num)) {
                top.ui.alert("必须输入数字！");
                document.getElementById('txtWorkersCount').select();
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="height: 89%; text-align: center;">
            <table class="tableContent2" cellpadding="5px" cellspacing="0" width="80%">
                <tr>
                    <td class="word">
                        任务代码
                    </td>
                    <td class="txt mustInput">
                        <asp:TextBox CssClass="txtCss" ID="txtTaskCode" runat="server"></asp:TextBox>
                    </td>
                    <td class="word">
                        任务名称
                    </td>
                    <td class="txt mustInput">
                        <asp:TextBox CssClass="txtCss" BackColor="#FEFEF4" ID="txtTaskName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                </tr>
                <tr>
                    <td class="word">
                        工作班组
                    </td>
                    <td class="elemTd">
                        <asp:TextBox CssClass="txtCss" ID="txtWorkGroup" runat="server"></asp:TextBox>
                    </td>
                    <td class="word">
                        工作人数
                    </td>
                    <td class="elemTd">
                        <asp:TextBox CssClass="txtCss" BackColor="#FEFEF4" ID="txtWorkersCount" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="word">
                        施工部位
                    </td>
                    <td colspan="3">
                        <textarea id="txtBuildPosition" style="width: 100%; height: 75px;" runat="server"></textarea>
                    </td>
                </tr>
                <tr>
                    <td class="word">
                        进度情况
                    </td>
                    <td colspan="3">
                        <textarea id="txtJdqk" style="width: 100%; height: 75px;" runat="server"></textarea>
                    </td>
                </tr>
                <tr>
                    <td class="word">
                        备注
                    </td>
                    <td colspan="3">
                        <textarea id="txtRemark" style="width: 100%; height: 75px;" runat="server"></textarea>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divFooter" class="divFooter2">
            <table class="tableFooter2">
                <tr>
                    <td>
                        <asp:Button ID="btnSave" Text="保存" OnClientClick="return valForm()" OnClick="btnSave_Click" runat="server" />
                        <input type="button" id="btnCancel" value="取消" onclick="return btnCancel_onclick()" runat="server" />

                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
