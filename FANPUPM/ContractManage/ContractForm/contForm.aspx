<%@ Page Language="C#" AutoEventWireup="true" CodeFile="contForm.aspx.cs" Inherits="ContractManage_ContractForm_contForm" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />


    <script type="text/javascript" src="/Script/jquery.js"></script>

    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>

    <script type="text/javascript" src="/Script/jw.js"></script>

    <script type="text/javascript" src="/Script/jwJson.js"></script>

    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript" src="../../Script/wf.js"></script>
<script language ="javascript" type ="text/javascript" >
    function rowQueryA(path) {
        parent.desktop.PayoutContractEdit = window;
        toolbox_oncommand(path, "查看收入合同");
    }

    function rowQueryAIco(cid) {
        parent.desktop.AddIncometContract = window;
        var url = "/ContractManage/IncometContract/AddIncometContract.aspx?t=1&id=" + cid;
        toolbox_oncommand(url, "查看收入合同");
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr id="header">
            <td>
                支出合同<asp:HiddenField ID="hdfPrjNum" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                    <tr>
                        <td class="descTd">
                            项目名称：
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrjNum" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
                            <asp:HiddenField ID="hdfSel" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <table class="tab" style="vertical-align: top;">
                    <tr>
                        <td class="divFooter" style="text-align: left">
                        
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="pagediv">
                                
                                <asp:Repeater ID="Repeater1" OnItemDataBound="Repeater1_ItemDataBound" runat="server">
<HeaderTemplate>
                                        <table class="rowa" border="1px" style="font-size: 12px; width: 1825px; line-height :22px;" >
                                            <tr style="text-align: center;">
                                                <td rowspan="2" style="width: 25px;">
                                                    序号
                                                </td>
                                                <td rowspan="2" style="width: 100px;">
                                                    项目名称
                                                </td>
                                                <td colspan="6">
                                                    收入合同
                                                </td>
                                                <td colspan="7">
                                                    支出合同
                                                </td>
                                                <td rowspan="2" style="width: 100px;">
                                                    原合同差额
                                                </td>
                                                <td rowspan="2" style="width: 100px;">
                                                    变更后合同差额
                                                </td>
                                                <td rowspan="2" style="width: 100px;">
                                                    结算差额
                                                </td>
                                                <td rowspan="2" style="width: 100px;">
                                                    支付差额
                                                </td>
                                            </tr>
                                            <tr style="text-align: center;">
                                                <td style="width: 100px;">
                                                    合同编号
                                                </td>
                                                <td style="width: 100px;">
                                                    合同名称
                                                </td>
                                                <td style="width: 100px;">
                                                    原始金额
                                                </td>
                                                <td style="width: 100px;">
                                                    变更后金额
                                                </td>
                                                <td style="width: 100px;">
                                                    开累结算
                                                </td>
                                                <td style="width: 100px;">
                                                    开累回款
                                                </td>
                                                <td style="width: 100px;">
                                                    合同编号
                                                </td>
                                                <td style="width: 100px;">
                                                    合同名称
                                                </td>
                                                <td style="width: 100px;">
                                                    合同类型
                                                </td>
                                                <td style="width: 100px;">
                                                    原始金额
                                                </td>
                                                <td style="width: 100px;">
                                                    变更后金额
                                                </td>
                                                <td style="width: 100px;">
                                                    开累结算
                                                </td>
                                                <td style="width: 100px;">
                                                    开累支付
                                                </td>
                                            </tr>
                                    </HeaderTemplate>

<ItemTemplate>
                                        <tr>
                                            <td style =" text-align :center ; width :25px;">
                                                <asp:Label ID="labIndex" Text="1" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 100px;">
                                                <%# DataBinder.Eval(Container.DataItem, "prjName") %>
                                            </td>
                                            <td colspan="13" style="width: 1300px;">
                                                <asp:Repeater ID="Repeater3" OnItemDataBound="Repeater3_ItemDataBound" runat="server">
<ItemTemplate>
                                                        <table border ="1px" cellpadding ="0" cellspacing ="0px" style =" line-height :22px;">
                                                            <tr>
                                                                <td style=" width :97px;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "ContractCode") %>
                                                                </td>
                                                                <td style="width :98px;">
                                                                 <a href="#" class="al" onclick="rowQueryAIco('<%# Eval("contractId") %>')">
                                                                    <%# DataBinder.Eval(Container.DataItem, "ContractName") %>
                                                                    </a> 
                                                                </td>
                                                                <td style="width :100px; text-align :right ;">
                                                                    <%# Eval("ContractPrice") %>
                                                                </td>
                                                                <td style="width: 99px;text-align :right ;">
                                                                    <asp:Label ID="labEndPrice" Text="Label" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width: 99px;text-align :right ;">
                                                                    <asp:Label ID="labIncoBalance" Text="" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width:100px;text-align :right ;">
                                                                    <asp:Label ID="labIncoBack" Text="" runat="server"></asp:Label>
                                                                </td>
                                                                <td colspan="7" style="width: 700px;">
                                                                    <asp:Repeater ID="Repeater2" OnItemDataBound="Repeater2_ItemDataBound" runat="server">
<ItemTemplate>
                                                                            <table class="rowa" border ="1px" cellpadding="0" cellspacing="0" width="100%" style =" line-height :22px;">
                                                                                <tr>
                                                                                    <td style="width: 100px;">
                                                                                        <%# Eval("ContractCode") %>
                                                                                    </td>
                                                                                    <td style="width: 100px;">
                                                                                     <span class="link" onclick="rowQueryA('/ContractManage/PayoutContract/PayoutContractEdit.aspx?Action=Query&ContractId=<%# Eval("ContractID") %>')">
                                                                                        <%# Eval("ContractName") %>
                                                                                        </span> 
                                                                                    </td>
                                                                                    <td style="width: 100px;">
                                                                                        <%# Eval("TypeName") %>
                                                                                    </td>
                                                                                    <td style="width: 100px;text-align :right ;">
                                                                                        <%# Eval("ContractMoney") %>
                                                                                    </td>
                                                                                    <td style="width: 100px;text-align :right ;">
                                                                                        <%# Eval("ModifiedMoney") %>
                                                                                    </td>
                                                                                    <td style="width: 100px;text-align :right ;">
                                                                                        <asp:Label ID="labPayoutBalance" Text="Label" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 100px;text-align :right ;">
                                                                                        <asp:Label ID="labPayoutBack" Text="Label" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                             </table>
                                                                        </ItemTemplate>
</asp:Repeater>
                                                                </td>
                                                            </tr>
                                                    </ItemTemplate>

<FooterTemplate>
                                                        <tr>
                                                            <td colspan ="6"></td>
                                                            <td colspan ="7">
                                                                <asp:Repeater ID="Repeater4" OnItemDataBound="Repeater4_ItemDataBound" runat="server">
<ItemTemplate>
                                                                            <table class="rowa" border ="1px" cellpadding="0" cellspacing="0" width="100%" style =" line-height :22px;">
                                                                                <tr>
                                                                                    <td style="width: 100px;">
                                                                                        <%# Eval("ContractCode") %>
                                                                                    </td>
                                                                                    <td style="width: 100px;">
                                                                                    <span class="link" onclick="rowQueryA('/ContractManage/PayoutContract/PayoutContractEdit.aspx?Action=Query&ContractId=<%# Eval("ContractID") %>')">
                                                                                        <%# Eval("ContractName") %>
                                                                                        </span> 
                                                                                    </td>
                                                                                    <td style="width: 100px;">
                                                                                        <%# Eval("TypeName") %>
                                                                                    </td>
                                                                                    <td style="width: 100px;text-align :right ;">
                                                                                        <%# Eval("ContractMoney") %>
                                                                                    </td>
                                                                                    <td style="width: 100px;text-align :right ;">
                                                                                        <%# Eval("ModifiedMoney") %>
                                                                                    </td>
                                                                                    <td style="width: 100px;text-align :right ;">
                                                                                        <asp:Label ID="labPayoutBalance" Text="Label" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 100px;text-align :right ;">
                                                                                        <asp:Label ID="labPayoutBack" Text="Label" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                             </table>
                                                                        </ItemTemplate>
</asp:Repeater>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" style="text-align:center ;">
                                                                小计
                                                            </td>
                                                            <td style =" width :100px;text-align :right ;">
                                                                <asp:Label ID="labIncoSumPrice" Text="Label" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width: 100px;text-align :right ;">
                                                                <asp:Label ID="labIncoSumEndPrice" Text="Label" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width: 100px;text-align :right ;">
                                                                <asp:Label ID="labIncoSumBalance" Text="Label" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width: 100px;text-align :right ;">
                                                                <asp:Label ID="labIncoSumBack" Text="Label" runat="server"></asp:Label>
                                                            </td>
                                                            <td colspan="3" style="text-align: right; width :300px; text-align :center ;">
                                                                小计
                                                            </td>
                                                            <td style =" width :100px;text-align :right ;">
                                                                <asp:Label ID="labPayoutSumPrice" Text="Label" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width: 100px;text-align :right ;">
                                                                <asp:Label ID="labPayoutSumEndPrice" Text="Label" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width: 100px;text-align :right ;">
                                                                <asp:Label ID="labPayoutSumBalance" Text="Label" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width: 100px;text-align :right ;">
                                                                <asp:Label ID="labPayoutSumBack" Text="Label" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        </table>
                                                    </FooterTemplate>
</asp:Repeater>
                                            </td>
                                            <td style="width: 100px;text-align :right ;">
                                                <asp:Label ID="labPrice" Text="Label" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 100px;text-align :right ;">
                                                <asp:Label ID="labEndPrice" Text="Label" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 100px;text-align :right ;">
                                                <asp:Label ID="labBalance" Text="Label" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 100px;text-align :right ;">
                                                <asp:Label ID="labBack" Text="Label" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>

<FooterTemplate>
                                            <tr>
                                                <td></td>
                                                <td colspan ="3" style =" text-align :center ;">合计</td>
                                                <td>
                                                    <asp:Label ID="labIcPrice" Text="Label" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="labIcEndPrice" Text="Label" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="labIcBl" Text="Label" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="labIcBk" Text="Label" runat="server"></asp:Label></td>
                                                <td colspan ="3" style =" text-align :center ;">
                                                    </td>
                                                <td>
                                                    <asp:Label ID="labPoPrice" Text="Label" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="labPoEndPrice" Text="Label" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="labPoIcBl" Text="Label" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="labPoIcBk" Text="Label" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="labOddsPrice" Text="Label" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="labOddsEndPrice" Text="Label" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="labOddsBl" Text="Label" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="labOddsBk" Text="Label" runat="server"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
</asp:Repeater>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
