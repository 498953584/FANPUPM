<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PickFrontNode.aspx.cs" Inherits="PickFrontNode" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>选择前置节点...</title>
    
    <script type="text/JavaScript">
        function confirmFrontNode() {
            var frontList = Form1.LbCur;
            var i = frontList.length;
            var j = 0;
            var text = "";
            var value = "";
            var datavalue = "";
            var frontNode = "";
            var xyPos = "";
            for (j = 0; j < i; j++) {
                text += frontList.options[j].text + ",";
                datavalue += frontList.options[j].value + ",";
                value = frontList.options[j].value;
                var index = value.lastIndexOf(";");
                frontNode += value.substr(index + 1) + ",";
                xyPos += value.substring(0, index) + ",";
            }
            var front = window.dialogArguments;
            front[0] = frontNode.substring(0, frontNode.length - 1);
            front[1] = text.substring(0, text.length - 1);
            front[2] = datavalue;
            alert(front[0] + '\n' + front[1] + '\n' + front[2]);
            return false;
            window.returnValue = front;
            window.close();
        }
    </script>
</head>
<body class="body_frame" scroll="no">
    <form id="Form1" method="post" runat="server">
    <div>
        <table id="Table1" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="td-head" height="18" colspan="3">
                    前置节点选择
                </td>
            </tr>
            <tr>
                <td class="td-head" width="40%" align="center">
                    <asp:Label ID="LblAll" Text="备选前置节点" runat="server"></asp:Label>
                </td>
                <td class="td-head">
                </td>
                <td class="td-head" width="40%" align="center">
                    <asp:Label ID="LblCur" Text="已选前置节点" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td rowspan="3">
                    <asp:ListBox ID="LbAll" Width="100%" Height="100%" Rows="10" runat="server"></asp:ListBox>
                </td>
                <td>
                </td>
                <td rowspan="3">
                    <asp:ListBox ID="LbCur" Width="100%" Height="100%" Rows="10" runat="server"></asp:ListBox>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <table id="Table3" height="60" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td align="center" height="18">
                                <asp:Button ID="BtnAdd" Text="-->>" class="button-normal" OnClick="BtnAdd_Click" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" height="18">
                                <asp:Button ID="BtnDel" Text="<<--" class="button-normal" OnClick="BtnDel_Click" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right" height="30" colspan="3">
                    <table width="60%" height="100%">
                        <tr align="center" valign="middle">
                            <td>
                                <input id="BtnOK" type="button" value="确  定" onclick="confirmFrontNode();" class="button-normal">
                            </td>
                            <td>
                                <input id="BtnCancel" type="button" value="取  消" onclick="window.close();" class="button-normal">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
