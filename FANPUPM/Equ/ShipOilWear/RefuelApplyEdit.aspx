<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RefuelApplyEdit.aspx.cs" Inherits="Equ_ShipOilWear_RefuelApplyEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../Script/ShipOilWear/RefuelApplyEdit.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divContent2" class="divContent2">
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    申请加油船
                </td>
                <td class="txt mustInput">
                    <span class="spanSelect" style="width: 100%">
                        <input type="text" id="txtApplyEquCode" class="{required:true, messages:{required:'申请加油船不能为空！'}}" readonly="readonly" style="width: 88%; height: 15px; border: none;
                            line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

                        <img alt="选择申请加油船" onclick="selectEqu();" src="../../images/icon.bmp" style="float: right;" />
                    </span>
                    <asp:HiddenField ID="hfldApplyEquId" runat="server" />
                </td>
                <td class="word">
                    项目
                </td>
                <td class="txt mustInput">
                    <span class="spanSelect" style="width: 100%">
                        <input type="text" id="txtPrjName" class="{required:true, messages:{required:'项目不能为空！'}}" readonly="readonly" style="width: 88%; height: 15px; border: none;
                            line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

                        <img alt="选择项目" onclick="openProjPicker();" src="../../images/icon.bmp" style="float: right;" />
                    </span>
                    <asp:HiddenField ID="hfldPrjId" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    油耗预算
                </td>
                <td class="txt mustInput">
                    <span class="spanSelect" style="width: 100%">
                        <input type="text" id="txtBudOilWearCode" class="{required:true, messages:{required:'油耗预算不能为空！'}}" readonly="readonly" style="width: 88%; height: 15px; border: none;
                            line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

                        <img alt="选择油耗预算" onclick="selectBudOilWear();" src="../../images/icon.bmp" style="float: right;" />
                    </span>
                    <asp:HiddenField ID="hfldBudOilWearId" runat="server" />
                </td>
                <td class="word">
                    分项工程
                </td>
                <td>
                    <input type="text" id="txtTaskCode" class="readonly" readonly="readonly" width="100%" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="word">
                    至今累计加油数(吨)
                </td>
                <td>
                    <asp:TextBox ID="txtRefuelTotal" Height="15px" Width="100%" Text="0.000" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    累计完成工程量(m³)
                </td>
                <td>
                    <asp:TextBox ID="txtCompletedQuantity" Height="15px" Width="100%" Text="0.000" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    定额油耗(KG/m³)
                </td>
                <td>
                    <input type="text" id="txtQuotaOilWear" value="0.000" class="readonly" readonly="readonly" style="width: 100%; height: 15px;" runat="server" />

                </td>
                <td class="word">
                    至今应该加油数(吨)
                </td>
                <td>
                    <input type="text" id="txtShouldRefuel" value="0.000" class="readonly" readonly="readonly" style="width: 100%; height: 15px;" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="word">
                    差异
                </td>
                <td>
                    <input type="text" id="txtDifference" value="0.000" class="readonly" readonly="readonly" style="width: 100%; height: 15px;" runat="server" />

                </td>
                <td class="word">
                    现有库存量(吨)
                </td>
                <td>
                    <asp:TextBox ID="txtStockQuantity" Height="15px" Width="100%" Text="0.000" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    原因分析
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtReason" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    预计完成工程量(m³)
                </td>
                <td>
                    <asp:TextBox ID="txtBudCompleteQuantity" Height="15px" Width="100%" Text="0.000" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    本次申请数量(吨)
                </td>
                <td>
                    <asp:TextBox ID="txtApplyQuantity" Height="15px" Width="100%" Text="0.000" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    挖深(m)
                </td>
                <td>
                    <asp:TextBox ID="txtSump" Height="15px" Width="100%" Text="0.000" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    油品种类型号
                </td>
                <td>
                    <asp:TextBox ID="txtOilsType" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    委托采购
                </td>
                <td>
                    <asp:CheckBox ID="chkIsEntrustPurchase" runat="server" />
                </td>
                <td class="word">
                    申请船主
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtApplyMaster" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    申请加油日期
                </td>
                <td>
                    <asp:TextBox ID="txtApplyRefuelDate" Height="15px" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    申请加油地点
                </td>
                <td>
                    <asp:TextBox ID="txtApplyRefuelPlace" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    施工日期
                </td>
                <td>
                    <asp:TextBox ID="txtConstructionDate" Height="15px" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    累计施工时间
                </td>
                <td>
                    <asp:TextBox ID="txtTotalConstructionDates" Height="15px" Width="100%" Text="0.000" class="decimal_input" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    供油船名
                </td>
                <td>
                    <asp:TextBox ID="txtFueler" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    船东
                </td>
                <td>
                    <asp:TextBox ID="txtFuelerOwner" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    现场负责人
                </td>
                <td>
                    <span class="spanSelect" style="width: 100%;">
                        <input type="text" id="txtLocaleLeader" readonly="readonly" style="width: 87%;
                            height: 15px; border: none; line-height: 16px; margin: 1px 2px" runat="server" />

                        <img src="../../images/icon.bmp" style="float: right;" alt="选择" id="imgName" onclick="selectUser();" runat="server" />

                    </span>
                    <asp:HiddenField ID="hfldLocaleLeader" runat="server" />
                </td>
                <td class="word">
                    现场负责人电话
                </td>
                <td>
                    <asp:TextBox ID="txtLeaderPhone" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    备注
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtNotes" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
