<%@ Page Language="C#" AutoEventWireup="true" CodeFile="basicManage.aspx.cs" Inherits="AccountManage_acc_Basic_basicManage" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>账户基本设置</title>

    <script src="../../StockManage/script/Config2.js" type="text/javascript"></script>

    <script src="../../StockManage/script/JustWinTable.js" type="text/javascript"></script>

    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>

    <script type="text/javascript" src="/Script/jquery.js"></script>

    <script type="text/javascript" language="javascript">

        function rdbIsRateClick() {
           document.getElementById("rateTr").style.display = "";
          

        }
        function rdbNoRateClick() {
            document.getElementById("txtborrowRate").value = "";
            document.getElementById("rateTr").style.display = "none";


        }
    </script>

    <style type="text/css">
        .td1
        {
            text-align: right;
        }
        .style1
        {
            color: #FF0000;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divContent">
        <table border="1" style="border-collapse: collapse; width: 100%">
            <tr>
                <td class="divHeader" colspan="2">
                    账户基本设置
                </td>
            </tr>
            <tr>
                <td class="td1">
                    账户分配：
                </td>
                <td align="left">
                    <input id="rdbPrj" type="radio" value="按项目" name="allot" runat="server" />
<asp:Label ID="Label1" Text="按项目" runat="server"></asp:Label>
                    <input id="rdbCon" type="radio" value="按合同" name="allot" runat="server" />
<asp:Label ID="Label2" Text="按合同" runat="server"></asp:Label>
                &nbsp; [<span class="style1">&nbsp; 警告：账户分配后，请勿切换分配方式，否则将可能导致灾难性后果！</span><b>]</b></td>
            </tr>
            <tr>
                <td class="td1">
                    启动资金：
                </td>
                <td align="left">
                    按【<asp:RadioButton ID="rdbPrjMoney" Text="预算额" GroupName="Money" runat="server" />
                    <asp:RadioButton ID="rdbCouMoney" Text="合同额" GroupName="Money" runat="server" />
                    】的<asp:TextBox ID="txtfundRatio" Width="10%" runat="server"></asp:TextBox><asp:Label ID="labRatio" Text="%" runat="server"></asp:Label>
                  <asp:RangeValidator ID="RangeValidator1" ControlToValidate="txtfundRatio" ErrorMessage="请输入数字" Type="Double" runat="server"></asp:RangeValidator>
                      
                </td>
                
            </tr>
            <tr>
                <td class="td1">
                    贷款利息：
                </td>
                <td align="left">
                    <input id="rdbNoRate" type="radio" value="0" name="rate" onclick="rdbNoRateClick()" runat="server" />
<span>无</span>
                    <input id="rdbIsRate" type="radio" value="1" name="rate" onclick="rdbIsRateClick()" runat="server" />
<span>有</span>
                    <div id="rateTr" runat="server">
                        <asp:TextBox ID="txtborrowRate" Width="10%" runat="server"></asp:TextBox>%
                         <asp:RangeValidator ID="RangeValidator2" ControlToValidate="txtborrowRate" ErrorMessage="请输入数字" Type="Double" runat="server"></asp:RangeValidator>
                        </div>
                </td>
            </tr>
            <tr>
                <td class="td1">
                    账户权限：
                </td>
                <td align="left">
                    <input id="rdbAuthPrj" type="radio" value="是" name="authority" runat="server" />
<span>继承项目权限</span>
                    <input id="rdbAuthHand" type="radio" value="否" name="authority" runat="server" />
<span>自定义权限</span>
                </td>
            </tr>
            <tr>
                <td style="height: 100%; text-align: right;" colspan="2" class="divFooter">
                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                    <asp:Button ID="btnCancel" Text="取消" OnClick="btnCancel_Click" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
