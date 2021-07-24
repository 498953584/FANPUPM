<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IEPInfoView.aspx.cs" Inherits="AccountManage_IEPInfo_IEPInfoView" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <base target="_self" />
    <link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />

    <script src="../../StyleCss/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/jwJson.js"></script>
    <script type="text/javascript" src="../../Script/wf.js"></script>
    <script type="text/javascript">
        //选择合同
        function selectContract() {
            var myRadios = document.getElementById("rdbTypeIn");
            if (myRadios.checked == true) {
                jw.selectInCon({ func: function (con) {
                    returnContract(con.id, con.name)
                }
                })
            }
            else {
                document.getElementById("ifrmSelectContract").src = "../../ContractManage/UserControl/PayoutContract.aspx?ContractId=hfldContract&ContractName=txtContract";
                selectnEvent('divSelectContract');
            }

        }

        //弹出的DIV选中合同后执行
        function returnContract(id, name) {
            document.getElementById('hfldContract').value = id;
            document.getElementById('txtContract').value = name;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader2">
        <table class="tableHeader">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="收支计划详情" runat="server"></asp:Label>
                    <asp:HiddenField ID="hdfID" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div class="divContent2">
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    计划编号
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtIEPcode" Enabled="false" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    计划名称
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtIEPname" Enabled="false" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    详情单编号
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtInfoCode" Enabled="false" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    收支类型
                </td>
                <td class="txt">
                    <asp:RadioButton ID="rdbTypeIn" Text="收入" GroupName="type" Enabled="false" runat="server" /><asp:RadioButton ID="rdbTypeOut" Text="支出" GroupName="type" Enabled="false" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    合同名称
                </td>
                <td class="txt mustInput">
                    <span class="spanSelect" style="width: 100%;">
                        <asp:TextBox ID="txtContract" Width="90%" Style="height: 15px; border: none; line-height: 16px;
                            margin: 1px 2px" Enabled="false" runat="server"></asp:TextBox>
                        <img src="/images1/iconSelect.gif" alt="选择合同" id="img1" onclick="selectContract();" />
                    </span>
                    <asp:HiddenField ID="hfldContract" runat="server" />
                </td>
                <td class="word">
                    金额
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtMoney" Enabled="false" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    备注
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtRemark" TextMode="MultiLine" Height="40px" Enabled="false" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    制单人
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtUser" Enabled="false" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    制单日期
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtData" CssClass="Wdate" onfocus="WdatePicker({el:'txtData',dateFmt:'yyyy-MM-dd'})" Enabled="false" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <input type="button" id="btnCancel" value="取消" onclick="winclose('IEPInfoEdit','IEPInfoList.aspx',false)" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divSelectContract" title="选择合同" style="display: none">
        <iframe id="ifrmSelectContract" frameborder="0" width="100%" height="100%" src="PurchaseplanList.aspx">
        </iframe>
    </div>
    </form>
</body>
</html>
