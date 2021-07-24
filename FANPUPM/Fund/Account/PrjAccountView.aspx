<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjAccountView.aspx.cs" Inherits="AccountView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title></title>

    <script type="text/javascript" src="/Script/jquery.js"></script>

    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>

    <script src="../../Script/jw.js" type="text/javascript"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
    <style type="text/css" media="print">
        .noprint
        {
            display: none;
        }
    </style>

    <script language="javascript" type="text/javascript">
    // <!CDATA[
        $(function() {
            //$("td").attr("style", "white-space:nowrap;");
            //setAnnxReadOnly('flAnnx');
            $("#showaudit1_chkAudit").click(function() {
                var flag = $(this).attr("checked");
                if (flag) {
                    $("#trAudit").removeClass("noprint");
                } else {
                    $("#trAudit").addClass("noprint");
                }
            });
            //$(".viewTable tr td").attr("style", "white-space:nowrap;");
        });
        //取消
        function btnCancel_onclick() {
            top.frameWorkArea.window.desktop.getActive().close();
        }        
    // ]]>
    </script>

    <style type="text/css">
        .style1
        {
            text-align: center;
        }
        .style2
        {
            width: 162px;
        }
    </style>
</head>
<body id="print">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top;">
        <tr>
            <td class="divHeader">
                账户查看
                <div style="height: 20px; width: 100%; left: 0px; top: 0px; text-align: right; position: absolute;">
                    &nbsp;&nbsp;
                    <input type="button" style="float: right;" class="noprint" onclick="winPrint()"
                        value=" 打 印 " />&nbsp;&nbsp;
                </div>
            </td>
        </tr>
        <tr style="height: 1px;">
            <td>
                <table id="bllProducer" cellpadding="0" cellspacing="0" style="border-style: none;"  class="viewTable">
                    <tr>
                        <td style="border-style: none; border: 0px;">
                            制单人:&nbsp;&nbsp;<asp:Label ID="lblPrintPeople" runat="server"></asp:Label>
                        </td>
                        <td style="border-style: none; text-align: right; border: 0px;">
                            打印日期:&nbsp;&nbsp;<asp:Label ID="lblPrintDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <table border="0px" cellpadding="0" cellspacing="0" class="viewTable">
                    <tr>
                        <td class="descTd">
                            账户编号
                        </td>
                        <td class="style2">
                            <asp:Literal ID="txtaccountNum" runat="server"></asp:Literal>
                        </td>
                        <td class="descTd">
                            账户名称
                        </td>
                        <td class="elemTd">
                            <asp:Literal ID="txtacountName" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            项目
                        </td>
                        <td colspan="3" class="txt">
                            <asp:Literal ID="lblPrj" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            账户可用余额
                        </td>
                        <td class="style2">
                            <asp:Literal ID="keyongyue" runat="server"></asp:Literal>
                        </td>
                        <td class="descTd">
                            账户余额
                        </td>
                        <td>
                            <asp:Literal ID="zhanghuyue" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" colspan="4">
                            账户资金来源(+)
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            启动资金
                        </td>
                        <td class="style2">
                            <asp:Literal ID="txtinitialFund" runat="server"></asp:Literal>
                        </td>
                        <td class="descTd">
                            其他收入
                        </td>
                        <td>
                            <asp:Literal ID="qitashouru" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            合同汇款入账
                        </td>
                        <td class="style2">
                            <asp:Literal ID="ruzhang" runat="server"></asp:Literal>
                        </td>
                        <td class="descTd">
                            项目借款
                        </td>
                        <td>
                            <asp:Literal ID="jiekuan" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" colspan="4">
                            账户资金支出（-）
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            归还本金
                        </td>
                        <td class="style2">
                            <asp:Literal ID="benjin" runat="server"></asp:Literal>
                        </td>
                        <td class="descTd">
                            归还利息及其他
                        </td>
                        <td>
                            <asp:Literal ID="lixijiqita" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            应支合同额
                        </td>
                        <td class="style2">
                            <asp:Literal ID="YZhetonge" runat="server"></asp:Literal>
                        </td>
                        <td class="descTd">
                            应支间接费用
                        </td>
                        <td>
                            <asp:Literal ID="YZjianjiefeiyong" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" colspan="4">
                            应支未支的（+）</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            未支合同额
                        </td>
                        <td class="style2">
                            <asp:Literal ID="WZhetonge" runat="server"></asp:Literal>
                        </td>
                        <td class="descTd">
                            未支间接费用
                        </td>
                        <td>
                            <asp:Literal ID="WZjianjiefeiyong" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            附件
                        </td>
                        <td class="txt" id="upload" colspan="3" runat="server">
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            备注
                        </td>
                        <td colspan="3" class="txt">
                            <asp:Literal ID="txtRemark" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
