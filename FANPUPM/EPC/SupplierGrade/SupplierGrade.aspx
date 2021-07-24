<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SupplierGrade.aspx.cs" Inherits="EPC_SupplierGrade_SupplierGrade" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>供应商评分系统</title>

    <script type="text/javascript" language="javascript">
    <!--
        window.name = "win";
    -->
    </script>

    <script src="../../Script/jquery.js" type="text/javascript"></script>

    <script src="../../StockManage/script/JustWinTable.js" type="text/javascript"></script>

    <script src="../../StockManage/script/Config2.js" type="text/javascript"></script>

    <script src="js/jquery.raty.min.js" type="text/javascript"></script>

    <style type="text/css">
        .td1
        {
            text-align: right;
        }
        .style1
        {
            width: 320px;
            text-align: center;
        }
        .style2
        {
            text-align: right;
            width: 180px;
        }
         .style3
        {
            text-align: right;
            width: 200px;
        }
    </style>

    <script type="text/jscript">
        $(function() {

            $('div#sx').raty({
                onClick: function(score) {

                    $("#hidsx").attr("value", score);
                    //alert($('#hidsx').val());

                }
            });
            $('div#num').raty({
                onClick: function(score) {

                    $("#hidnum").attr("value", score);
                    //alert($('#hidnum').val());
                }
            });
            $('div#zl').raty({
                onClick: function(score) {
                    $("#hidzl").attr("value", score);
                    //alert($('#hidzl').val());
                }
            });
            $('div#xh').raty({
                onClick: function(score) {
                    $("#hidxh").attr("value", score);
                    //alert($('#hidxh').val());
                }
            });
            $('div#sj').raty({
                onClick: function(score) {
                    $("#hidsj").attr("value", score);
                    //alert($('#hidsj').val());
                }
            });
            $('div#td').raty({
                onClick: function(score) {
                    $("#hidtd").attr("value", score);
                    //alert($('#hidtd').val());
                }
            });

        });

    </script>

</head>
<body>
    <form id="form1" target="win" runat="server">
    <div >
        <input id="hidSuperId" type="hidden" runat="server" />

        <input id="hidBillsId" type="hidden" runat="server" />

        <input id="hidgoodsid" type="hidden" runat="server" />

        <table border="1" style="border-collapse: collapse; width: 99%">
            <tr>
                <td colspan="3" class="divHeader">
                    供应商评分系统
                </td>
            </tr>
            <tr>
                <td class="style2">
                    发货手续齐全度&nbsp;
                </td>
                <td class="style1">
                    <div id="sx">
                    </div>
                    <input id="hidsx" type="hidden" runat="server" />

                </td>
                <td rowspan="6" align="center"  class="style3">
                    <img align="middle" alt="" src="img/tip.gif" style="text-align: center" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    数量/重量/体积相符度
                </td>
                <td class="style1">
                    <div id="num">
                    </div>
                    <input id="hidnum" type="hidden" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="style2">
                    外观质量合格度
                </td>
                <td class="style1">
                    <div id="zl">
                    </div>
                    <input id="hidzl" type="hidden" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="style2">
                    规格型号符合度
                </td>
                <td class="style1">
                    <div id="xh">
                    </div>
                    <input id="hidxh" type="hidden" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="style2">
                    送货及时性
                </td>
                <td class="style1">
                    <div id="sj">
                    </div>
                    <input id="hidsj" type="hidden" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="style2">
                    送货人员态度
                </td>
                <td class="style1">
                    <div id="td">
                    </div>
                    <input id="hidtd" type="hidden" runat="server" />

                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: right; height: 100%" class="divFooter">
                    <asp:Button ID="BtnOk" Text="确 定" OnClick="BtnOk_Click" runat="server" />
                    &nbsp;<asp:Button ID="BunCancel" Text="取 消" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
