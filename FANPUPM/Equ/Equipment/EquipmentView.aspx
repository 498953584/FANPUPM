<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquipmentView.aspx.cs" Inherits="Equ_Equipment_EquipmentView" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>设备台账查看</title></head>
<body id="print1">
    <form id="form1" runat="server">
    <table class="tab" style="vertical-align: top;">
        <tr style="height: 1px;">
            <td class="divHeader">
                设备台账查看
            </td>
        </tr>
        <tr style="height: 1px;">
            <td style="vertical-align: top;">
                <table class="viewTable" cellpadding="0px" cellspacing="0">
                    <tr>
                        <td class="descTd">
                            设备编号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblEquCode" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            设备名称
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblEquName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            设备类别
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblEquTypeName" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            设备性质
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblEquProperty" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            设备原值
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPurchasePrice" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            折旧率
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblDepreciationRate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            购置日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPurchaseDate" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            耐用年限
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblDurableYear" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            出厂编号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblFactoryNumber" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            设备状态
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblState" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            供应商
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblCorpName" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            出厂日期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblFactoryDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            检定周期
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblPeriodicVertification" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            购置发票号
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblReceiptNo" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr id="trShip" runat="server"><td colspan="4" runat="server">
                        <table style="width:100%; border:1px solid black" cellpadding="4px" cellspacing="0">
                            <tr>
                                <td align="left" colspan="4">
                                    技术参数：
                                </td>
                            </tr>
                            <tr>
                                <td class="descTd">
                                    船长
                                </td>
                                <td class="elemTd">
                                    <asp:Label ID="lblShipLength" runat="server"></asp:Label>
                                </td>
                                <td class="descTd">
                                    船宽
                                </td>
                                <td class="elemTd">
                                    <asp:Label ID="lblShipWidth" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="descTd">
                                    舱容
                                </td>
                                <td class="elemTd" colspan="3">
                                    <asp:Label ID="lblShipCapacity" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="descTd">
                                    其它技术参数
                                </td>
                                <td class="elemTd" colspan="3">
                                    <asp:Panel ID="plOtherShips" runat="server">
                                        <asp:Label ID="lblOtherShipInfo" runat="server"></asp:Label>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                        </td></tr>
                    <tr>
                        <td align="left" colspan="4">
                            重要证书有效期：
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            中间检验
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblMiddleInspectDate" runat="server"></asp:Label>
                        </td>
                        <td class="descTd">
                            年度检验
                        </td>
                        <td class="elemTd">
                            <asp:Label ID="lblYearInspectDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            其它重要证书
                        </td>
                        <td class="elemTd" colspan="3">
                            <asp:Label ID="lblOtherCredentials" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            附件
                        </td>
                        <td class="elemTd" colspan="3">
                            <asp:Label ID="lblAnnex" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            备注
                        </td>
                        <td class="elemTd" colspan="3">
                            <asp:Label ID="lblNotes" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
        </tr>
    </table>
    </form>
</body>
</html>
