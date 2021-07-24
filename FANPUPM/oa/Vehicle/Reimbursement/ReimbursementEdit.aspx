<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReimbursementEdit.aspx.cs" Inherits="oa_Vehicle_Reimbursement_ReimbursementEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>司机报销单</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../../Script/jw.js"></script>
    <script type="text/javascript" src="../../../Script/jwJson.js"></script>
    <script type="text/javascript" src="../../../Script/wf.js"></script>
	<script type="text/javascript" src="../../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../../Script/DecimalInput.js"></script>
    <script type="text/javascript">
        //选择人员
        function selectUser() {
            jw.selectOneUser({ nameinput: 'txtPeople', codeinput: 'hdnPeople' });
        }

        //选择车辆
        function selectVehicle() {
            top.ui.callback = function (obj) {
                $('#hdnVehicleGuid').val(obj.code);
                $('#txtVehicleCode').val(obj.name);
            }
            top.ui.openWin({ title: '选择车辆', url: '/oa/Vehicle/DivSelectVehicle.aspx' });
        }

        //表单验证
        function valForm() {
            if (document.getElementById("hdnVehicleGuid").value == "" && document.getElementById("txtVehicleCode").value == "") {
                alert("系统提示:\n\n请选择车辆");
                return false;
            }
            if (document.getElementById("txtPeople").value == "") {
                alert("系统提示:\n\n请选择报销人");
                return false;
            }
            return true;
        }
    </script>
</head>
<body scroll="auto">
    <form id="form1" runat="server">
    <div class="divHeader2">
        <table class="tableHeader">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="divContent2" style="margin-top: 5px;">
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    单据号
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtcode" Enabled="false" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    车牌号码
                </td>
                <td class="txt" style="padding-right: 3px">
                    <span id="spanPrj" class="spanSelect" style="width: 99%; background-color: #FEFEF4;">
                        <input type="text" readonly="readonly" style="width: 89%; background-color: #FEFEF4;
                            height: 15px; border: none; line-height: 16px; margin: 1px  2px" id="txtVehicleCode" runat="server" />

                        <img src="~/images/icon.bmp" style="float: right;" alt="选择车牌号码" id="imgPrj" onclick="selectVehicle()" runat="server" />

                    </span>
                    <input id="hdnVehicleGuid" type="hidden" name="hdnVehicleGuid" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="word">
                    报销人
                </td>
                <td class="txt" style="padding-right: 3px">
                    <span id="span1" class="spanSelect" style="width: 99%; background-color: #FEFEF4;">
                        <input type="text" style="width: 89%; background-color: #FEFEF4; height: 15px; border: none;
                            line-height: 16px; margin: 1px 2px" id="txtPeople" runat="server" />

                        <img id="Img1" src="~/images/icon.bmp" style="float: right;" onclick="selectUser();" alt="选择报销人" runat="server" />

                        <input id="hdnPeople" type="hidden" name="hdnPeople" runat="server" />

                    </span>
                </td>
                <td class="word">
                    报销日期
                </td>
                <td class="txt">
                    <JWControl:DateBox ID="txtDate" runat="server"></JWControl:DateBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    目的地
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtDestination" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    过路费
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtTolls" CssClass="easyui-validatebox easyui-numberbox" required="required" data-options="min:0,max:9999999999,precision:3,groupSeparator:','" runat="server"></asp:TextBox>
                  	<span id="spanCashWords"></span>
			    </td>
            </tr>
            <tr>
                <td class="word">
                    油费
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtRepairs" CssClass="easyui-validatebox easyui-numberbox" required="required" data-options="min:0,max:9999999999,precision:3,groupSeparator:','" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    修理费
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtFuelcosts" CssClass="easyui-validatebox easyui-numberbox" required="required" data-options="min:0,max:9999999999,precision:3,groupSeparator:','" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    保养费
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtMaintenancecosts" CssClass="easyui-validatebox easyui-numberbox" required="required" data-options="min:0,max:9999999999,precision:3,groupSeparator:','" runat="server"></asp:TextBox>
                </td>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td align="right">
                    备注
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClientClick="return valForm()" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" onclick="winclose('AddSendNote','SendNoteList.aspx',false)" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divFram" title="" style="display: none">
        <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <div id="divFramPrj" title="" style="display: none">
        <iframe id="ifFramPrj" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <input type="hidden" id="hdnGuid" runat="server" />

    </form>
</body>
</html>
