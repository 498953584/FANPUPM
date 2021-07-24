<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PettyCashDetail2.aspx.cs" Inherits="PettyCash_PettyCashDetail2" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_audit_ascx" Src="~/UserControl/Audit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link rel="Stylesheet" type="text/css" href="/Script/Print/css/print-preview.css" media="screen" />
<link rel="Stylesheet" type="text/css" href="/Script/Print/css/print.css" media="print" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/Print/jquery.print-preview.js" charset="utf-8"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/Print/jw.print.js"></script>
    <script type="text/javascript" src="/Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <style type="text/css">
        #table1 tr td
        {
            border: 1px solid black;
            height: 18px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            //显示大写金额
            //var path = $("#hfldPath").val();
            //$("#imgOrginator").attr("src", path);
            $("#lblCapital").html(jw.amountInWords($('#lblTotalMoney').html()));
        });
    </script>
    <script type="text/javascript">
        $('img').each(function () {
            // 如果没有签名图片，就显示用户名称
            if (this.src.indexOf('UploadFiles') == -1) {
                $(this).remove();
            } else {
                $(this).prev().remove();
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:left;">
        <table style="width: 680px; margin: 0px auto;" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="lblTitle" Text="" Font-Size="20px" runat="server"></asp:Label>
                </td>
                <td>
                    <input type="button" id="btnDy" style="float: right; text-align: center" class="noprint"
                        onclick="winPrint()" value=" 打 印 " />
                </td>
            </tr>
        </table>
        <table style="text-align: left; margin: 0px auto; margin-top: 10px; width: 680px;">
            <tr>
                <td>
                    项目编号:
                    <asp:Label ID="lblProjectCode" Text="" runat="server"></asp:Label>
                </td>
                <td>
                    打印时间:
                    <asp:Label ID="lblPayMentDate" Text="" runat="server"></asp:Label>
                </td>
                <td>
                    文件ID:
                    <asp:Label ID="lblFileNumber" Text="" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="table1" style="border-collapse: collapse; width: 680px; text-indent:4px; margin: 0px auto;
            margin-top: 10px; table-layout: fixed; word-wrap: break-word;">
            <tr>
                <td>
                    部门
                </td>
                <td>
                    <asp:Label ID="lblDepartMent" Text="" runat="server"></asp:Label>
                </td>
                <td>
                    经办人
                </td>
                <td>
                    <asp:Label ID="lblOperator" Text="" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    内容
                </td>
                <td>
                    金额
                </td>
                <td colspan="2">
                    说明
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblContent" Text="" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblMoney" Text="" runat="server"></asp:Label>
                </td>
                <td colspan="2" style="text-align: left">
                    <asp:Label ID="lblExplian" Text="" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" Text="" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label2" Text="" runat="server"></asp:Label>
                </td>
                <td colspan="2" style="text-align: left">
                    <asp:Label ID="Label3" Text="" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" Text="" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label5" Text="" runat="server"></asp:Label>
                </td>
                <td colspan="2" style="text-align: left">
                    <asp:Label ID="Label6" Text="" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" Text="" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label8" Text="" runat="server"></asp:Label>
                </td>
                <td colspan="2" style="text-align: left">
                    <asp:Label ID="Label9" Text="" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label10" Text="" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label11" Text="" runat="server"></asp:Label>
                </td>
                <td colspan="2" style="text-align: left">
                    <asp:Label ID="Label12" Text="" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    金额合计(元)
                </td>
                <td style="text-align: left">
                    <asp:Label ID="lblTotalMoney" Text="" runat="server"></asp:Label>
                </td>
                <td>
                    大写金额
                </td>
                <td style="text-align: left">
                    <asp:Label ID="lblCapital" Text="" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    收付款方式
                </td>
                <td>
                    <asp:Label ID="lblPayment" Text="" runat="server"></asp:Label>
                </td>
                <td>
                    领款人
                </td>
                <td>
                    <asp:Label ID="lblDrawPeople" Text="" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    收款单位
                </td>
                <td colspan="3">
                    <asp:Label ID="lblpayee" Text="" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    开户行
                </td>
                <td colspan="3">
                    <asp:Label ID="lblBank" Text="" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    账号
                </td>
                <td colspan="3">
                    <asp:Label ID="lblAccount" Text="" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
           <td colspan="4">
           <asp:Repeater ID="rptAudit" runat="server">
<ItemTemplate>
            <div style="float:left; width:16.6%">
                <img id="imgOrginator" width="80px" height="50px" src='<%# GetNamePath(Eval("Operator")) %>' />
            </div>
            </ItemTemplate>
</asp:Repeater>
        </td>
        </tr>
        </table>
    </div>
    <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    <asp:HiddenField ID="hfldPrjId" runat="server" />
    <asp:HiddenField ID="hfldYear" runat="server" />
    <asp:HiddenField ID="hfldPrjName" runat="server" />
    <asp:HiddenField ID="hfldPath" runat="server" />
    </form>
</body>
</html>
