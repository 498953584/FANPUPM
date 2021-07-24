<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BudOilWearEdit.aspx.cs" Inherits="Equ_ShipOilWear_BudOilWearEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../Script/ShipOilWear/BudOilWearEdit.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    油耗预算编号
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtOilWearCode" Width="100%" CssClass="{required:true, messages:{required:'编号不能为空！'}}" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    项目名称
                </td>
                <td class="txt mustInput">
                    <span class="spanSelect" style="width: 100%">
                        <input type="text" id="txtPrjName" class="{required:true, messages:{required:'项目名称不能为空！'}}" readonly="readonly" style="width: 88%; height: 15px; border: none;
                            line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

                        <img alt="选择项目" onclick="openProjPicker();" src="../../images/icon.bmp" style="float: right;" />
                    </span>
                    <input id="hfldPrjId" type="hidden" onpropertychange="setEmpty();" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="word">
                    分项工程
                </td>
                <td class="txt mustInput">
                    <span class="spanSelect" style="width: 100%">
                        <input type="text" id="txtTaskCode" class="{required:true, messages:{required:'分项工程不能为空！'}}" readonly="readonly" style="width: 88%; height: 15px; border: none;
                            line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

                        <img alt="选择分项工程" onclick="selectTask();" src="../../images/icon.bmp" style="float: right;" />
                    </span>
                    <asp:HiddenField ID="hfldTaskId" runat="server" />
                </td>
                <td class="word">
                    合同预算油耗(KG/m³)
                </td>
                <td>
                    <asp:TextBox ID="txtConBudOilWear" Width="100%" Height="15px" Text="0.000" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    预算挖深(m)
                </td>
                <td>
                    <asp:TextBox ID="txtSump" Height="15px" Width="100%" Text="0.000" CssClass="decimal_input" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    土质
                </td>
                <td>
                    <asp:TextBox ID="txtSoilTexture" Width="100%" Height="15px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    需求船型
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtDemandShipType" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    对外租赁
                </td>
                <td>
                    <asp:CheckBox ID="chkIsOutLease" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    预算开工日期
                </td>
                <td>
                    <asp:TextBox ID="txtStartDate" Height="15px" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    预算完工日期
                </td>
                <td>
                    <asp:TextBox ID="txtEndDate" Height="15px" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    施工地点
                </td>
                <td>
                    <asp:TextBox ID="txtConstructionPlace" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    预算油单价(元/KG)
                </td>
                <td>
                    <asp:TextBox ID="txtBudOilPrice" Height="15px" Width="100%" Text="0.000" class="decimal_input" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    定额油耗(KG/m³)
                </td>
                <td>
                    <asp:TextBox ID="txtQuotaOilWear" Height="15px" Width="100%" Text="0.000" class="decimal_input" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    预算挖泥方量(m³)
                </td>
                <td>
                    <asp:TextBox ID="txtBudQutity" Height="15px" Width="100%" Text="0.000" class="decimal_input" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    预算挖泥油耗(KG/m³)
                </td>
                <td>
                    <asp:TextBox ID="txtBudOilWear" Height="15px" Width="100%" Text="0.000" class="decimal_input" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    定额油耗数量(KG)
                </td>
                <td>
                    <asp:TextBox ID="txtQuotaOilWearQuantiy" Height="15px" Width="100%" Text="0.000" class="decimal_input" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    预算油耗数量(KG)
                </td>
                <td>
                    <asp:TextBox ID="txtBudOilWearQuantity" Height="15px" Width="100%" Text="0.000" class="decimal_input" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                </td>
                <td>
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
