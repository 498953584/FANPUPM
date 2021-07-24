<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquipmentEdit.aspx.cs" Inherits="Equ_Equipment_EquipmentEdit" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>设备台账</title><link type="text/css" rel="stylesheet" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link type="text/css" rel="stylesheet" href="../../Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Validation.js" charset="gb2312"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <style type="text/css">
        .EquStyle1
        {text-align:left;
         padding-left:30px;
         }
        .EquStyle2
        {text-align:left;
         padding-left:20px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            // 绑定项目类型
            bindOtherShipInfo();
            // 添加项目类型
            $('#btnAdd').click(addOtherShipInfo);
        }
        )

        function addOtherShipInfo() {
            $('.equShipInfo').last().after(createShipInfo());              // 添加到页面
        }

        function createShipInfo() {
            var $ship = $('#trOtherShipInfo').clone();
            var len = $('.equShipInfo').length;
            $ship.find('*[id]').each(function () { this.id += '_' + len; }); // 初始化ID
            $ship.find('td:even').empty();          // 清空文字说明
            $ship.find('input').val('');     // 初始化
            // 删除按钮
            $ship.find(':button').val('删除').click(function () {
                $(this).parent().parent().remove();
            });
            return $ship;
        }

        function beforeClickCheck() {
            if ($('#txtEquipmentCode').val() == "") {
                top.ui.alert("设备编号不能为空！");
                $('#txtEquipmentCode').focus();
                return false;
            }
            if ($('#txtEquName').val() == "") {
                top.ui.alert("设备名称不能为空！");
                $('#txtEquName').focus();
                return false;
            }
            if ($('#txtEquTypeName').val() == "") {
                top.ui.alert("设备类别不能为空！");
                $('#txtEquTypeName').focus();
                return false;
            }
            SetOtherShipsInfo();
        }

        function SetOtherShipsInfo() {
            var shipInfo = "";
            var shipsInfo = "";
            $('.equShipInfo').each(function () {
                shipInfo = $(this).find("input[id^='txtOtherShipInfo']").first().val();
                if (shipInfo != ""){
                    if (shipsInfo != "") {
                        shipsInfo += "|";
                    }
                    shipsInfo += shipInfo;
                }
            });
            $('#hfldOtherShipsInfo').val(shipsInfo);
            return true;
        }

        // 绑定船舶设备的其他参数信息
        function bindOtherShipInfo() {
            var shipInfos = $('#hfldOtherShipsInfo').val();
            if (!shipInfos) return false;

            var shipArr = shipInfos.split("|");
            for (var i = 0; i < shipArr.length; i++) {
                if (i == 0) {
                    $('#txtOtherShipInfo').val(shipArr[i]);
                } else {
                    var $kind = createShipInfo();
                    $kind.find('input:eq(0)').val(shipArr[i]);
                    $('.equShipInfo').last().after($kind);
                }
            }
        }

        //选择设备类别
        function SetEquType() {
            var url = '/Equ/Equipment/SelectEquipmentType.aspx?' + new Date().getTime();
            var equTypeInfo = { width: 1000, height: 550, url: url, title: '选择设备类别' };
            top.ui.callback = function (equTypeInfo) {
                $('#hfldEquTypeId').val(equTypeInfo.id);
                $('#txtEquTypeName').val(equTypeInfo.name);
                $('#hdButton').click();
            };
            top.ui.openWin(equTypeInfo);
        }

        //设定折旧率信息
        function SetRate() {
            var rate = $('#txtDepreciationRate').val();
            var index = rate.search(/%/);
            if (index != -1) {
                if (index != (rate.length - 1)) {
                    top.ui.alert("折旧率的格式不正，请重新输入！")
                    $('#txtDepreciationRate').val("0.000%");
                    return;
                }
                var finalRate = rate.replace(/%/g, "");
                checkRate(finalRate);
            } else {
                if (checkRate(rate)) {
                    var finalRate = rate.toString() + "%";
                    $('#txtDepreciationRate').val(finalRate);
                }
            }
        }

        function checkRate(rate) {
            if (rate > 100) {
                top.ui.alert("折旧率不能大于100%，请重新输入！");
                $('#txtDepreciationRate').val("0.000%");
                return false;
            }
            if (rate < 0) {
                top.ui.alert("折旧率不能小于零，请重新输入！");
                $('#txtDepreciationRate').val("0.000%");
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="tbEquiment" class="tableContent2" cellpadding="4px" cellspacing="0">
            <tr>
                <td class="EquStyle1">
                    设备编号
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtEquipmentCode" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="EquStyle2">
                    设备名称
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtEquName" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="EquStyle1">
                    设备类别
                </td>
                <td class="txt mustInput">
                    <span id="span3" class="spanSelect" style="width: 100%;">
                        <input type="text" style="width: 90%; height: 15px; border: none; line-height: 16px;
                            margin: 1px 0px 1px 2px" readonly="readonly" id="txtEquTypeName" runat="server" />

                        <img alt="选择设备类别" onclick="SetEquType();" src="../../images/icon.bmp" style="float: right;" />
                    </span>
                    <asp:HiddenField ID="hfldEquTypeId" runat="server" />
                </td>
                <td class="EquStyle2">
                    设备性质
                </td>
                <td>
                   <asp:DropDownList ID="ddlProperty" Width="100%" runat="server"></asp:DropDownList> 
                </td>
            </tr>
            <tr>
                <td class="EquStyle1">
                    设备原值
                </td>
                <td>
                    <asp:TextBox ID="txtPurchasePrice" Height="15px" Width="100%" Text="0.000" class="decimal_input" runat="server"></asp:TextBox>
                </td>
                <td class="EquStyle2">
                    折旧率
                </td>
                <td>
                    <asp:TextBox ID="txtDepreciationRate" Height="15px" Width="100%" Text="0.000%" onblur="SetRate()" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="EquStyle1">
                    购置日期
                </td>
                <td>
                    <asp:TextBox ID="txtPurchaseDate" Height="15px" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td class="EquStyle2">
                    耐用年限
                </td>
                <td>
                    <asp:TextBox ID="txtDurableYear" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                 <td class="EquStyle1">
                    出厂编号
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtFactoryNumber" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="EquStyle2">
                    设备状态
                </td>
                <td>
                    <asp:DropDownList ID="ddlState" Width="100%" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="EquStyle1">
                    供应商
                </td>
                <td>
                    <span id="span2" class="spanSelect" style="width: 100%;">
                        <input type="text" style="width: 90%; height: 15px; border: none;
                            line-height: 16px; margin: 1px 0px 1px 2px" readonly="readonly" id="txtCorpName" runat="server" />

                        <img alt="选择供应商" onclick="SelectCorp();" src="../../images/icon.bmp" style="float: right;" />
                    </span>
                    <asp:HiddenField ID="hfldCorpId" runat="server" />
                </td>
                <td class="EquStyle2">
                    出厂日期
                </td>
                <td>
                    <asp:TextBox ID="txtFactoryDate" Height="15px" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>     
                <td class="EquStyle1">
                    检定周期
                </td>
                <td>
                    <asp:TextBox ID="txtPeriodicVertification" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>     
                <td class="EquStyle2">
                    发票号
                </td>
                <td>
                    <asp:TextBox ID="txtReceiptNo" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>      
            </tr>
            <tr id="trShip" runat="server"><td colspan="4" runat="server">
                    <div style="border:1px solid gray; width:97%; margin-left:30px">
                        <table style="width:99.5%" cellpadding="4px" cellspacing="0">
                            <tr>
                                <td style="text-align:left; padding-left:5px" colspan="4">
                                    技术参数：
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:left; padding-left:30px;width:12%">
                                    船长
                                </td>
                                <td style="width:40%">
                                    <asp:TextBox ID="txtShipLength" Height="15px" Width="100%" runat="server"></asp:TextBox>
                                </td>
                                <td style="text-align:left; padding-left:30px;width:8%">
                                    船宽
                                </td>
                                <td style="width:40%">
                                    <asp:TextBox ID="txtShipWidth" Height="15px" Width="100%" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="EquStyle1">
                                    舱容
                                </td>
                                <td>
                                    <asp:TextBox ID="txtShipCapacity" Height="15px" Width="100%" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trOtherShipInfo" class="equShipInfo" runat="server"><td class="EquStyle1" runat="server">
                                    其它技术参数
                                </td><td colspan="3" runat="server">
                                    <asp:TextBox ID="txtOtherShipInfo" Height="15px" Width="80%" runat="server"></asp:TextBox>
                                    <input id="btnAdd" type="button" value="添加" style=" vertical-align:top" />
                                </td></tr>
                        </table>
                    </div>
                </td></tr>
            <tr>
                <td class="EquStyle1" colspan="4">
                    重要证书有效期：
                </td>
            </tr>
            <tr>     
                <td class="EquStyle1">
                    中间检验
                </td>
                <td>
                    <asp:TextBox ID="txtMiddleInspectDate" Height="15px" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>     
                <td class="EquStyle2">
                    年度检验
                </td>
                <td>
                    <asp:TextBox ID="txtYearInspectDate" Height="15px" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>      
            </tr>
            <tr>     
                <td class="EquStyle1">
                    其它重要证书
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtOtherCredentials" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>    
            </tr>
            <tr>
                <td class="EquStyle1">
                    附件
                </td>
                <td colspan="3" style="padding-right: 0px;">
                    <MyUserControl:epc_usercontrol1_filelink2_ascx ID="flAnnx" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="EquStyle1">
                    备注
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtNotes" TextMode="MultiLine" Height="40px" Rows="3" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="divFooter2">
            <table class="tableFooter2">
                <tr>
                    <td>
                        <asp:Button ID="btnSave" Text="保存" OnClientClick="return beforeClickCheck();" OnClick="btnSave_Click" runat="server" />
                        <asp:Button ID="btnCancel" Text="取消" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    <asp:Button ID="hdButton" style="display:none" OnClick="hdButton_Click" runat="server" />
    <asp:HiddenField ID="hdfEquId" runat="server" />
    <asp:HiddenField ID="hfldOtherShipsInfo" runat="server" />
    </form>
</body>
</html>
