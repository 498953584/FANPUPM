<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsuranceAndReviewEdit.aspx.cs" Inherits="oa_Vehicle_InsuranceAndReview_InsuranceAndReviewEdit" %>
<%@ Register TagPrefix="MyUserControl" TagName="oa_vehicle_vehicleusercontrol_typecontrol_ascx" Src="~/oa/Vehicle/VehicleUserControl/TypeControl.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="oa_vehicle_vehicleusercontrol_vehilclename_ascx" Src="~/oa/Vehicle/VehicleUserControl/VehilcleName.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="oa_vehicle_vehicleusercontrol_statecontrol_ascx" Src="~/oa/Vehicle/VehicleUserControl/stateControl.ascx" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>交强险缴纳与年审</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script src="/StockManage/script/Config2.js" type="text/javascript"></script>
    <script src="/StockManage/script/Validation.js" type="text/javascript"></script>
    <script src="/StockManage/script/JustWinTable.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../../Script/My97DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            replaceEmptyTable();
            var contractTable = new JustWinTable('gvwContract');
            setButton(contractTable, 'Button1', 'Button2', 'Button3', 'hfldContract')
            if (getRequestParam('Action') == 'Query' || !getRequestParam('Action')) {
                setAllInputDisabled();
            }
            Val.validate('form1');
            registerUpdateVehicleItem();
            registerBtnCancelEvent();

            $("#txtVehicleCode").attr("readonly", "readonly");

            if ($('span[id$=spanContractType]').width() < 150) {
                $('input[id$=txtVehicleState]').width($('span[id$=spanContractType]').width() - 27);
            }
            var inputs = document.getElementsByTagName('INPUT');
            var inputWidth;
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].id.indexOf('txtVehicleState') >= 0) {
                    inputs[i].setAttribute('readOnly', 'readOnly');
                    inputWidth = inputs[i].style.width;
                }
            }

            $("#gvwContract tr").click(function () {
                var selectVal = ($(this).attr("id"));
                $("#Hidden_save_SID").val(selectVal);
                var selectVal_Name = ($(this).find("td:eq(2) a").html());
            });
        });

        function registerUpdateVehicleItem() {
            var hfldContract = document.getElementById('hfldContract');
        }
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


        // 选择车辆
        function selectVehicle() {
            top.ui.callback = function (obj) {
                $('#hdnVehiclesCode').val(obj.code);
                $('#txtVehicleCode').val(obj.name);
            }
            top.ui.openWin({ title: '选择车辆', url: '/oa/Vehicle/DivSelectVehicle.aspx' });
        }


        function replaceEmptyTable() {
            if (!document.getElementById('gvwContract')) return;
            if (!document.getElementById('emptyContractType')) return;
            var gvwContractType = document.getElementById('gvwContract');
            var emptyContractType = document.getElementById('emptyContractType');
            if (gvwContractType.firstChild.childNodes.length == 1) {
                gvwContractType.replaceChild(emptyContractType.firstChild, gvwContractType.firstChild);
            }
        }

        function viewopen_n(url) {
            toolbox_oncommand(url, "查看车辆信息");
        }

        function selectVehicleName() {
            selectPurchaser();
        }

        // 选择车辆户主
        function selectPurchaser() {
            jw.selectOneUser('');
            jw.selectOneUser({ nameinput: 'txtVehicleName' });
        }

        // 选择车辆状态
        function selectVehicleState(element) {
            var typeNameControl = element.id;
            if (element.nodeName == 'IMG') {
                typeNameControl = element.previousSibling.previousSibling.id;
            }
            var typeIdControl = typeNameControl.replace('txtVehicleState', 'hfldStateID');

            var url = '/oa/Vehicle/VehicleUserControl/VehicleState.aspx';
            top.ui.callback = function (obj) {
                $('#' + typeIdControl).val(obj.code);
                $('#' + typeNameControl).val(obj.name);
            }
            top.ui.openWin({ title: '选择车辆状态', url: url, width: 350, height: 350 });
        }

        function Button1_onclick() {
            alert($("#Hidden_save_SID").val());
        }
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
        .divfooterTest
        {
            height: 24px;
            text-align: right;
            background: url(/images1/divBottomBack.jpg) repeat-x;
            vertical-align: middle;
            position: absolute;
            bottom: 0px;
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 100%;">
        <div class="divHeader2">
            <table class="tableHeader2">
                <tr>
                    <td>
                        交强险缴纳与年审
                    </td>
                </tr>
            </table>
        </div>
        <div style="height: 74%;">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr id="header">
                    <td class="" style="line-height: 28px;">
                        交强险缴纳与审车
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top;">
                        <table class="queryTable" cellpadding="3px" cellspacing="0px">
                            <tr>
                                <td class="descTd">
                                    车辆类型
                                </td>
                                <td>
                                    <MyUserControl:oa_vehicle_vehicleusercontrol_typecontrol_ascx ID="TypeControl1" Width="120px" runat="server" />
                                </td>
                                <td class="descTd">
                                    车牌号码
                                </td>
                                <td>
                                    <span id="span1" class="spanSelect" style="width: 122px;">
                                        <asp:TextBox Style="width: 96px; height: 15px; border: none; line-height: 16px; margin: 1px 2px" ID="txtVehicleCode" runat="server"></asp:TextBox>
                                        <img style="float: right;" src="../../../images/icon.bmp" alt="选择车辆" id="img1" onclick="selectVehicle();" />
                                    </span>
                                    <asp:HiddenField ID="hdnVehiclesCode" runat="server" />
                                </td>
                                <td class="descTd">
                                    年检开始日期
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEndTimeNJ1" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                </td>
                                <td class="descTd">
                                    年检结束日期
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEndTimeNJ2" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="descTd">
                                    购买开始日期
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBeinTime" Width="116px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                </td>
                                <td class="descTd">
                                    购买结束日期
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEndTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                </td>
                                <td class="descTd">
                                    上户开始日期
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEndTimesh0" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                </td>
                                <td class="descTd">
                                    上户结束日期
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEndTimesh1" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="descTd">
                                    车辆户主
                                </td>
                                <td>
                                    <span id="spanVehicleName" style="width: 118px;" class="spanSelect" runat="server">
                                        <asp:TextBox ID="txtVehicleName" Style="height: 15px; border: none; line-height: 16px;
                                            width: 91px; margin: 1px 2px;" runat="server"></asp:TextBox>
                                        <img style="float: right;" src="../../../images/icon.bmp" id="img2" alt="请选择车辆户主" onclick="selectVehicleName();" runat="server" />

                                        <asp:HiddenField ID="hfldVehicleNameID" runat="server" />
                                    </span>
                                </td>
                                <td class="descTd">
                                    车辆识别码
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVehicleIdentify" Width="120px" runat="server"></asp:TextBox>
                                </td>
                                <td class="descTd">
                                    归属地
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAddress" Width="120px" runat="server"></asp:TextBox>
                                </td>
                                <td class="descTd">
                                    车辆状态
                                </td>
                                <td>
                                    <span id="spanContractType" style="width: 122px;" class="spanSelect" runat="server">
                                        <asp:TextBox ID="txtVehicleState" Style="height: 15px; border: none; line-height: 16px;
                                            width: 100px; margin: 1px 2px;" ReadOnly="true" runat="server"></asp:TextBox>
                                        <img style="float: right;" src="../../../images/icon.bmp" id="img" alt="请选择车辆状态" onclick="selectVehicleState(this);" runat="server" />

                                    </span>
                                    <div id="divSelectState" title="请选择车辆状态" style="display: none">
                                        <iframe id="ifrmSelectState" frameborder="0" width="100%" height="100%"></iframe>
                                    </div>
                                    <asp:HiddenField ID="hfldStateID" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="descTd">
                                    发动机号
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEngineCode" Width="116px" runat="server"></asp:TextBox>
                                </td>
                                <td class="descTd">
                                    保险开始日期
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEndTimeBX1" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                </td>
                                <td class="descTd">
                                    保险结束日期
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEndTimeBX2" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                </td>
                                <td class="descTd">
                                    是否为入股
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList1" runat="server"><asp:ListItem Text="--请选择--" Value="" /><asp:ListItem Text="是" Value="1" /><asp:ListItem Text="否" Value="0" /></asp:DropDownList>
                                </td>
                                <td class="descTd">
                                    <asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top;">
                        <table class="tab" style="height: 70%;">
                            <tr>
                                <td>
                                    <div class="">
                                        <asp:GridView ID="gvwContract" CssClass="gvdata" AllowPaging="true" AutoGenerateColumns="false" OnRowDataBound="gvwContract_RowDataBound" OnPageIndexChanging="gvwContract_PageIndexChanging" DataKeyNames="guid" runat="server">
<EmptyDataTemplate>
                                                <table id="emptyContractType" class="gvdata">
                                                    <tr class="header">
                                                        <th scope="col" style="width: 20px;">
                                                            <input id="chk1" type="checkbox" />
                                                        </th>
                                                        <th scope="col" style="width: 25px;">
                                                            序号
                                                        </th>
                                                        <th scope="col">
                                                            车辆名称
                                                        </th>
                                                        <th scope="col">
                                                            车牌号码
                                                        </th>
                                                        <th scope="col">
                                                            识别号码
                                                        </th>
                                                        <th scope="col">
                                                            车辆类型
                                                        </th>
                                                        <th scope="col">
                                                            购买日期
                                                        </th>
                                                        <th scope="col">
                                                            年检日期
                                                        </th>
                                                        <th scope="col">
                                                            保险日期
                                                        </th>
                                                        <th scope="col">
                                                            发票价格
                                                        </th>
                                                        <th scope="col">
                                                            入账价格
                                                        </th>
                                                        <th scope="col">
                                                            车辆状态
                                                        </th>
                                                        <th scope="col">
                                                            归属地
                                                        </th>
                                                        <th scope="col">
                                                            备注
                                                        </th>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
                                                        <asp:CheckBox ID="chkAll" runat="server" />
                                                    </HeaderTemplate><ItemTemplate>
                                                        <asp:CheckBox ID="chk" runat="server" />
                                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center" HeaderText="车辆名称">
<ItemTemplate>
                                                        <%# Eval("Specification") %>
                                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="车牌号码"><ItemTemplate>
                                                        <span class="link" onclick="viewopen_n('/oa/Vehicle/Main/Default.aspx?Action=Query&q=alert&VehicletId=<%# Eval("guid") %>')">
                                                            <%# Eval("VehicleCode") %></span>
                                                    </ItemTemplate></asp:TemplateField><asp:BoundField DataField="VehicleIdentify" HeaderText="车辆识别码" /><asp:TemplateField HeaderText="车辆类型" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
                                                        <%# getVehicleName(Eval("VehicleType").ToString()) %>
                                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="购买日期" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
                                                        <%# Common2.GetTime(Eval("PurchaseDate")) %>
                                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="年检日期" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
                                                        <%# Common2.GetTime(Eval("InspectionDate")) %>
                                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="保险日期" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
                                                        <%# Common2.GetTime(Eval("InsuranceDate")) %>
                                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="发票价格" ItemStyle-HorizontalAlign="Right">
<ItemTemplate>
                                                        <%# (Eval("Fatfare") == null) ? "" : Eval("Fatfare").ToString() %>
                                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="入账价格" ItemStyle-HorizontalAlign="Right">
<ItemTemplate>
                                                        <%# (Eval("Recordedprice") == null) ? "" : Eval("Recordedprice").ToString() %>
                                                    </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="State" HeaderText="车辆状态" ItemStyle-HorizontalAlign="Center" /><asp:BoundField DataField="Address" HeaderText="归属地" /><asp:TemplateField HeaderText="备注"><ItemTemplate>
                                                        <%# (Eval("Remark") == null) ? "" : StringUtility.GetStr(Eval("Remark"), 10) %>
                                                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="margin-top: 20px; vertical-align: bottom;">
                                        <table style="width: 80%; margin: auto;" cellspacing="0" cellpadding="5px">
                                            <tr>
                                                <td class="word">
                                                    编码
                                                </td>
                                                <td height="25" class="txt">
                                                    <asp:TextBox ID="txtcode" Width="99%" runat="server"></asp:TextBox>
                                                </td>
                                                <td class="word">
                                                    &nbsp;
                                                </td>
                                                <td class="word">
                                                    &nbsp;
                                                </td>
                                                <td class="word">
                                                    &nbsp;
                                                </td>
                                                <tr>
                                                    <td class="word">
                                                        日期
                                                    </td>
                                                    <td class="txt mustInput">
                                                        <asp:TextBox ID="txtDate" Width="99%" onclick="WdatePicker()" CssClass="{required:true, messages:{required:'日期必须输入'}}" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td class="word">
                                                        &nbsp;
                                                    </td>
                                                    <td class="word">
                                                        &nbsp;
                                                    </td>
                                                    <td class="word">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <td class="word">
                                                    类型选择
                                                </td>
                                                <td class="txt " colspan="4">
                                                    <asp:RadioButtonList ID="rado" RepeatColumns="2" runat="server"><asp:ListItem Text="交强险缴纳" Selected="true" Value="0" /><asp:ListItem Text="审车" Value="1" /></asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                        <div id="divFooter" class="divFooter">
                                            <table class="tableFooter2">
                                                <tr>
                                                    <td>
                                                        <asp:HiddenField ID="Hidden_save_SID" runat="server" />
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
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div style="display: none;">
            <input id="Button1" type="button" value="button" />
            <input id="Button2" type="button" value="button" />
            <input id="Button3" type="button" value="button" />
        </div>
    </div>
    <div id="divFram" title="" style="display: none">
        <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <div id="divFramRole" title="" style="display: none">
        <iframe id="iframeRole" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <asp:HiddenField ID="hfldContract" runat="server" />
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    $(function () {
        $("#TypeControl1_txtVehicleType").removeAttr("class");
        $("#TypeControl1_txtVehicleType").attr("style", "width:93px;height: 15px; border: none; line-height: 16px; width: 78%; margin: 1px 2px;");
    });
</script>
