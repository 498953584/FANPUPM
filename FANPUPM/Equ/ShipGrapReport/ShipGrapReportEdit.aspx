<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShipGrapReportEdit.aspx.cs" Inherits="Equ_ShipProductionReport_ShipProductionReportEdit" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>抓扬式挖泥船上报</title><link type="text/css" rel="stylesheet" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link type="text/css" rel="stylesheet" href="../../Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="../../../Script/jw.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../StockManage/script/ValidateInput.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <style type="text/css">
        .divSelfFooter
        {
            height: 24px;
            text-align: right;
            background: url(/images1/divBottomBack.jpg) repeat-x;
            vertical-align: middle;
            position: relative;
            bottom: 0px;
            width: 100%;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            Val.validate('form1', 'btnSave');
            //取消按钮事件
            $('#btnCancel').click(btnCancelClick);
            setProCalculateType();
            setTimeAvailability();
            setProUnitPrice();
            $('#cbxProduction').click(setProduction);
            $('#cbxTeam').click(setTeam);
            $('#rbIsTeamInfo').click(setProUnitPrice);
            $('#ddlProCalculateType').change(setProCalculateType);
        });

        function setProduction() {
            if ($('#cbxProduction').attr("Checked")) {
                $('#trProduction').show();
                if ($('#cbxTeam').attr("Checked")) {
                    $('#teamSpan').show();
                } else {
                    $('#teamSpan').hide();
                }
            } else {
                $('#trProduction').hide();
                $('#teamSpan').hide();
            }
        }

        function setTeam() {
            if ($('#cbxTeam').attr("Checked")) {
                $('#trTeam').show();
                if ($('#cbxProduction').attr("Checked")) {
                    $('#teamSpan').show();
                } else {
                    $('#teamSpan').hide();
                }
            } else {
                $('#trTeam').hide();
                $('#teamSpan').hide();
            }
        }

        function setProUnitPrice() {
            if ($('#rbIsTeamInfo').attr("value") == "0") {
                $('#rbIsTeamInfo').attr("value", "1");
                $('#trProUnitPrice').hide();
            } else {
                $('#rbIsTeamInfo').attr("value", "0");
                $('#rbIsTeamInfo').removeAttr("Checked");
                $('#trProUnitPrice').show();
            }
        }

        function setProCalculateType() {
            switch ($('#ddlProCalculateType').val()) {
                case "1":
                    $('#trDiscountRate').css("display", "none");
                    //总扣方
                    $('.tdTotalQty').css("display", "none");
                    $('#txtTotalDeductQty').css("display", "none"); 
                    break;
                case "2":
                    $('#trDiscountRate').css("display", "none");
                    //总扣方
                    $('.tdTotalQty').css("display", "block");
                    $('#txtTotalDeductQty').css("display", "block"); 
                    break;
                case "3":
                    $('#trDiscountRate').css("display", "block");
                    //总扣方
                    $('.tdTotalQty').css("display", "none");
                    $('#txtTotalDeductQty').css("display", "none"); 
                    break;
                default:
            }
        }

        function setTimeAvailability() {
            var t1 = parseFloat($('#txtWorkDurationT1').val());
            var t2 = parseFloat($('#txtWorkRestDurationT2').val());
            var t3 = parseFloat($('#txtNotWorkRestDurationT3').val());
            if ((t1 + t2 + t3) > 0) {
                var result = (t1 / (t1 + t2 + t3)).toFixed(4);
                result = result * 100;
                $('#txtTimeAvailability').val(result.toString() + "%");
            } else {
              $('#txtTimeAvailability').val("0%");
            }
        }

        function btnCancelClick() {
            winclose('ShipGrapReportEdit', '../DayOutputMenu/DayOutputMenu.aspx?isShip=1&EquEnum=1', false)
        }

        //选择泥驳船编号
        function selectMudBargeShipCode(img) {
            var url = '/Equ/Equipment/SelectEquipment.aspx?' + new Date().getTime();
            top.ui.callback = function (equInfo) {
                $('#hfldBargeId').val(equInfo.id);
                $('#txtBargeName').val(equInfo.code);
            };
            top.ui.openWin({ title: '选择船机编号', url: url, width: 1000, height: 500 });
        }

        function addTable() {
            var table1 = $('#tbProInfo');
            var row = $("<tr></tr>");
            var td = $("<td></td>");
            td.append(createProTable());
            row.append(td);
            table1.append(row);
        }

        function createProTable() {
            var $ship = $('#plModel').clone();
            var len = $('#tbProInfo').children().children().length;
            $ship.find('*[id]').each(function () { this.id += len; }); // 初始化ID
            $ship.each(function () {
                $(this).find('td:odd').find('input').val(''); // 清空文字说明
            })
            return $ship;
        }

        function delTable() {
            if ($('#tbProInfo').children().children().length > 2) {
                $('#tbProInfo').children().children().last().remove();
            } else {
                alert("追加的挖泥船信息已删除完毕！");
            }
        }

        //选择项目
        function openProjPicker() {
            jw.selectOneProject({ idinput: 'hfldPrjId', nameinput: 'txtPrjName' });
        }

        //选择支出合同
        function openContPicker() {
            jw.selectOutCon({ idinput: 'hfldContractId', nameinput: 'hfldContractName' });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <table class="tableContent2" cellpadding="5px" cellspacing="0">
                <tr>
                    <td style="text-align:left; width:11%">
                        挖泥船名
                    </td>
                    <td style="width:35%" class="txt mustInput">
                        <asp:TextBox ID="txtEquName" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td style="text-align:left; width:15%;padding-left:30px">
                        挖泥船编码
                    </td>
                    <td style="width:35%" class="txt mustInput">
                        <asp:TextBox ID="txtEquCode" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left;">
                        设备性质
                    </td>
                    <td class="txt mustInput">
                        <asp:TextBox ID="txtEquProperty" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td style="text-align:left; padding-left:30px">
                        项目名称
                    </td>
                    <td class="txt mustInput">
                        <asp:TextBox ID="txtPrjName" CssClass="select_input {required:true, messages:{required:'项目不能为空'}}" Style="width: 100%; height: 15px;" imgclick="openProjPicker" runat="server"></asp:TextBox>
                        <asp:HiddenField ID="hfldPrjId" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left;">
                        施工日期
                    </td>
                    <td class="txt mustInput">
                        <asp:TextBox ID="txtConstructionDate" Width="100%" CssClass="{required:true, messages:{required:'施工日期不能为空'}}" onclick="WdatePicker()" runat="server"></asp:TextBox>
                    </td>
                    <td style="text-align:left;padding-left:30px">
                        运转时间（T1）
                    </td>
                    <td>
                        <asp:TextBox ID="txtWorkDurationT1" Width="100%" CssClass="decimal_input" Text="0.000" onblur="setTimeAvailability()" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left;">
                        上报日期
                    </td>
                    <td class="txt mustInput">
                        <asp:TextBox ID="txtReportDate" Width="100%" CssClass="{required:true, messages:{required:'上报日期不能为空'}}" onclick="WdatePicker()" runat="server"></asp:TextBox>
                    </td>
                    <td style="text-align:left;padding-left:30px">
                        当日耗油量（T）
                    </td>
                    <td class="txt">
                        <asp:TextBox ID="txtDayOilWear" Width="100%" CssClass="decimal_input" Text="0.000" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left;">
                        非生产性间歇时间（T3）
                    </td>
                    <td>
                        <asp:TextBox ID="txtNotWorkRestDurationT3" Width="100%" CssClass="decimal_input" Text="0.000" onblur="setTimeAvailability()" runat="server"></asp:TextBox>
                    </td>
                    <td style="text-align:left;padding-left:30px">
                        生产性间歇时间（T2）
                    </td>
                    <td>
                        <asp:TextBox ID="txtWorkRestDurationT2" Width="100%" CssClass="decimal_input" Text="0.000" onblur="setTimeAvailability()" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left;">
                        时间利用率
                    </td>
                    <td>
                        <asp:TextBox ID="txtTimeAvailability" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left;">
                        备注
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtNote" Width="100%" TextMode="MultiLine" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left;">
                        产值统计方式：
                    </td>
                    <td colspan="3">
                        <span>
                            <asp:CheckBox ID="cbxProduction" Checked="true" Text="按产量" runat="server" />
                            <asp:CheckBox ID="cbxTeam" Style="margin-left: 120px" Checked="true" Text="按台班" runat="server" />
                        </span>
                    </td>
                </tr>
                <tr id="trProduction" runat="server"><td colspan="4" style="width: 100%;" runat="server">
                        <asp:Panel ID="plProduction" GroupingText="按产量：" runat="server">
                            <table class="tableContent2" cellpadding="5px" cellspacing="0" style="margin-left:5px; width:90%">
                                <tr>
                                    <td style="text-align:left; width:15%">
                                        完成产量计算方式
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlProCalculateType" Width="200px" runat="server"></asp:DropDownList>
                                    </td>
                                    <td align="center">
                                        <span id="teamSpan">
                                            <asp:RadioButton ID="rbIsTeamInfo" value="0" Text="台班计费时产量信息" runat="server" />
                                        </span>
                                    </td>
                                </tr>
                                <tr id="trDiscountRate">
                                    <td style="text-align:left;">
                                        折扣率
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtDiscountRate" Width="200px" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="width:100%">
                                        <table id="tbProInfo" class="tableContent2" cellpadding="5px" cellspacing="0" 
                                            style="margin-left: 5px; border:1px solid black; width:100%">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="plModel" Width="95%" runat="server">
                                                        <table class="tableContent2" cellpadding="5px" cellspacing="0" style="margin-left:5px;
                                                            width: 100%">
                                                            <tr>
                                                                <td style="text-align: left; width: 13%">
                                                                    泥驳名称
                                                                </td>
                                                                <td class="txt" style="width: 30%">
                                                                    <span id="span3" class="spanSelect" style="width: 100%;">
                                                                        <asp:TextBox ID="txtBargeName0" style="width: 80%; height: 15px; border: none; line-height: 16px;margin: 1px 0px 1px 2px" runat="server"></asp:TextBox>
                                                                        <img alt="选择泥驳船" onclick="selectMudBargeShipCode();" src="../../images/icon.bmp"
                                                                            style="float: right;"/>
                                                                    </span>
                                                                    <asp:HiddenField ID="hfldBargeId0" runat="server" />
                                                                </td>
                                                                <td style="text-align: left; width: 17%; padding-left: 20px">
                                                                    舱容（m3）
                                                                </td>
                                                                <td style="width: 30%;">
                                                                    <asp:TextBox ID="txtShipCapatity" CssClass="decimal_input" Width="100%" runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left;">
                                                                    运泥驳数
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtWorkCount" CssClass="decimal_input" Width="100%" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td style="text-align: left; width: 17%; padding-left: 20px">
                                                                    <asp:Label ID="Label1" class="tdTotalQty" Text="总扣方（m3）" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTotalDeductQty" CssClass="decimal_input" Width="100%" runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left;">
                                                                    日报方量（m3）
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDayReportQty" CssClass="decimal_input" Width="100%" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td style="text-align: left; width: 17%; padding-left: 20px">
                                                                    未满载时扣方（m3）
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDeductQty" CssClass="decimal_input" Width="100%" runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr id="trProUnitPrice">
                                                                <td style="text-align: left;">
                                                                    单价
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPrice" CssClass="decimal_input" Width="100%" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td colspan="2">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center">
                                                                <img src="../images/IconTexto_WebDev_059.png" title="新增泥驳船" alt="新增泥驳船" id="imgadd"
                                                                    onclick="addTable()" style="width: 25px; cursor: hand;" />
                                                            </td>
                                                            <td align="left">
                                                                <img src="../images/IconTexto_WebDev_052.png" title="删除选中泥驳船" alt="删除选中泥驳船" id="imgdel"
                                                                    onclick="delTable()" style="width: 25px; cursor: hand;" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left;">
                                        总方量
                                    </td>
                                    <td  colspan="2">
                                        <asp:TextBox ID="txtTotalQty" Width="240px" CssClass="decimal_input" ReadOnly="true" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td></tr>
                <tr id="trTeam" runat="server"><td colspan="4" style="width: 100%; height: 50px;" runat="server">
                        <asp:Panel ID="plTeam" GroupingText="按台班：" runat="server">
                            <table class="tableContent2" cellpadding="5px" cellspacing="0" style="margin-left:5px">
                                <tr>
                                    <td style="text-align:left; width:13%">
                                        有效计费时间
                                    </td>
                                    <td style="width:35%">
                                        <asp:TextBox ID="txtWorkDuration" Width="100%" CssClass="decimal_input" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="text-align:left; width:15%; padding-left:50px">
                                        单价
                                    </td>
                                    <td style="width:35%">
                                        <asp:TextBox ID="txtUnitPrice" Width="100%" CssClass="decimal_input" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left;">
                                        相关合同
                                    </td>
                                    <td style="width:35%" class="txt">
                                        <span id="span1" class="spanSelect" style="width: 100%;">
                                            <asp:TextBox ID="hfldContractName" Style="width: 80%;
                                                height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px" runat="server"></asp:TextBox>
                                            <img alt="选择支出合同" onclick="openContPicker();" src="../../images/icon.bmp" style="float: right;" />
                                        </span>
                                        <asp:HiddenField ID="hfldContractId" runat="server" />
                                    </td>
                                    <td colspan="2">
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td></tr>
                <tr>
                    <td style="text-align:left;">
                        相关附件
                    </td>
                    <td colspan="3" style="padding-right: 0px;">
                        <MyUserControl:epc_usercontrol1_filelink2_ascx ID="flAnnx" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="divSelfFooter">
            <table class="tableFooter2">
                <tr>
                    <td>
                        <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                        <asp:Button ID="btnCancel" Text="取消" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:HiddenField ID="hfldDayId" runat="server" />
    </form>
</body>
</html>
