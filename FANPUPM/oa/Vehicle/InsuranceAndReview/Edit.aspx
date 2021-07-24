<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="oa_Vehicle_InsuranceAndReview_Edit" %>
<%@ Register TagPrefix="MyUserControl" TagName="oa_vehicle_vehicleusercontrol_typecontrol_ascx" Src="~/oa/Vehicle/VehicleUserControl/TypeControl.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="oa_vehicle_vehicleusercontrol_vehilclename_ascx" Src="~/oa/Vehicle/VehicleUserControl/VehilcleName.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="oa_vehicle_vehicleusercontrol_statecontrol_ascx" Src="~/oa/Vehicle/VehicleUserControl/stateControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>交强险缴纳与年审</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="../../../StockManage/skins/sm4.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script src="/StockManage/script/Config2.js" type="text/javascript"></script>
    <script src="/StockManage/script/Validation.js" type="text/javascript"></script>
    <script src="/StockManage/script/JustWinTable.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript">
        addEvent(window, 'load', function () {
            replaceEmptyTable();
            var contractTable = new JustWinTable('gvwContract');
            if (getRequestParam('Action') == 'Query' || !getRequestParam('Action')) {
                setAllInputDisabled();
            }
            Val.validate('form1');
            registerUpdateVehicleItem();
            registerBtnCancelEvent();
        });

        function registerBtnCancelEvent() {
            var btnCancel = document.getElementById('btnCancel');
            if (getRequestParam('q') == 'alert') {
                $("#btnCancel").click(function () {
                    window.close();
                });
            } else {
                addEvent(btnCancel, 'click', function () {
                    winclose('InsuranceAndReviewEdit', 'InsuranceAndReview.aspx', false)
                });
            }
        }
        function registerUpdateVehicleItem() {
            hfldContract = document.getElementById('hfldContract');
        }

        function selectVehicle() {
            document.getElementById("divFramPrj").title = "选择车辆";
            document.getElementById("ifFramPrj").src = "/oa/Vehicle/DivSelectVehicle.aspx?Method=returnVehicle";
            selectnEvent('divFramPrj');
        }
        function returnVehicle(id, name) {
            document.getElementById('hdnVehiclesCode').value = id;
            document.getElementById('txtVehicleCode').value = name;
        }
        $(function () {
            $("#img1").removeAttr("onclick");
            // $("#txtcode").attr("disabled", "disabled");
            $("#txtVehicleCode").attr("disabled", "disabled");
            $("#gvwContract tr").click(function () {
                var selectVal = ($(this).attr("id"));
                $("#Hidden_save_SID").val(selectVal);
                var selectVal_Name = ($(this).find("td:eq(2) a").html());
            });
        });
    </script>
    <style type="text/css">
        .word
        {
            width: 18%;
            text-align: right;
        }
        .txt
        {
            width: 40%;
            text-align: left;
        }
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 60%; margin: auto;" cellspacing="0" cellpadding="5px">
            <tr>
                <td class="word">
                    编码
                </td>
                <td height="25">
                    <asp:TextBox ID="txtcode" Width="99%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    日期
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtDate" Width="99%" onclick="WdatePicker()" CssClass="{required:true, messages:{required:'日期必须输入'}}" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    车牌号码
                </td>
                <td class="txt mustInput">
                    <span id="span1" class="spanSelect" style="width: 99%;">
                        <asp:TextBox Style="width: 89%; height: 15px; border: none; line-height: 16px; margin: 1px 2px" ID="txtVehicleCode" runat="server"></asp:TextBox>
                        <img style="float: right;" src="../../../images/icon.bmp" alt="选择车辆" id="img1" onclick="selectVehicle();" />
                    </span>
                    <asp:HiddenField ID="hdnVehiclesCode" runat="server" />
                </td>
            </tr>
            <td class="word">
                类型选择
            </td>
            <td class="txt ">
                <asp:RadioButtonList ID="rado" RepeatColumns="2" runat="server"><asp:ListItem Text="交强险缴纳" Selected="true" Value="0" /><asp:ListItem Text="审车" Value="1" /></asp:RadioButtonList>
            </td>
            </tr>
        </table>
    </div>
    <div id="divFooter" class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divFramPrj" title="" style="display: none">
        <iframe id="ifFramPrj" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <asp:HiddenField ID="hideIARGUID" runat="server" />
    </form>
</body>
</html>
