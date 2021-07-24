<%@ Page Language="C#" AutoEventWireup="true" CodeFile="oa_vehicle_main_vehiclereport_.aspx.cs" Inherits="oa_Vehicle_Main_VehicleReport_" %>
<%@ Register TagPrefix="MyUserControl" TagName="oa_vehicle_vehicleusercontrol_typecontrol_ascx" Src="~/oa/Vehicle/VehicleUserControl/TypeControl.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="oa_vehicle_vehicleusercontrol_vehilclename_ascx" Src="~/oa/Vehicle/VehicleUserControl/VehilcleName.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="oa_vehicle_vehicleusercontrol_statecontrol_ascx" Src="~/oa/Vehicle/VehicleUserControl/stateControl.ascx" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>车辆信息一览表</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
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
    <script src="/StockManage/script/Config2.js" type="text/javascript"></script>
    <script src="../../../StockManage/script/JustWinTable.js" type="text/javascript"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            // 设置只读
            $('#txtVehicleState').attr('readonly', 'readonly');
            replaceEmptyTable();
            var contractTable = new JustWinTable('gvwContract');
        });
        function replaceEmptyTable() {
            //当数据量为空时，修改样式
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

        //选择车辆
        function selectVehicle() {
            document.getElementById("divFramPrj").title = "选择车辆";
            document.getElementById("ifFramPrj").src = "/oa/Vehicle/DivSelectVehicle.aspx?Method=returnVehicle";
            selectnEvent('divFramPrj'); //选择车辆返回值
        }
        function returnVehicle(id, name) {
            document.getElementById('hdnVehiclesCode').value = id;
            document.getElementById('txtVehicleCode').value = name;
        }

        //选择车辆户主
        function selectVehicleName() {
            document.getElementById("divFramPrj").title = "选择车辆户主";
            document.getElementById("ifFramPrj").src = "/Common/DivSelectUser.aspx?Method=returnVehicleName";
            selectnEvent('divFramPrj'); //选择人员返回值
        }
        function returnVehicleName(id, name) {
            document.getElementById('hfldVehicleNameID').value = id;
            document.getElementById('txtVehicleName').value = name;
        }
        $(function () {
            $(".queryTable td").attr("style", "white-space:nowrap;");
        });
    </script>
    <style type="text/css">
        .style1
        {
            height: 19px;
            line-height: 28px;
        }
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="" cellspacing="" width="100%">
        <tr id="header">
            <td class="style1">
                车辆信息
            </td>
        </tr>
        <tr>
            <td style="width: 100%; vertical-align: top;">
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
                            <span id="spanPrj" class="spanSelect" style="width: 122px;">
                                <asp:TextBox Style="width: 96px; height: 15px; border: none; line-height: 16px; margin: 1px 2px" ID="txtVehicleCode" runat="server"></asp:TextBox>
                                <img style="float: right;" src="../../../images/icon.bmp" alt="选择车辆" id="imgPrj"
                                    onclick="selectVehicle();" />
                            </span>
                            <asp:HiddenField ID="hdnVehiclesCode" runat="server" />
                        </td>
                        <td class="descTd">
                            年检开始日期
                        </td>
                        <td>
                            <JWControl:DateBox ID="txtEndTimeNJ1" Width="120px" runat="server"></JWControl:DateBox>
                        </td>
                        <td class="descTd">
                            年检结束日期
                        </td>
                        <td>
                            <JWControl:DateBox ID="txtEndTimeNJ2" Width="120px" runat="server"></JWControl:DateBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            购买开始日期
                        </td>
                        <td>
                            <JWControl:DateBox ID="txtBeinTime" Width="116px" runat="server"></JWControl:DateBox>
                        </td>
                        <td class="descTd">
                            购买结束日期
                        </td>
                        <td>
                            <JWControl:DateBox ID="txtEndTime" Width="120px" runat="server"></JWControl:DateBox>
                        </td>
                        <td class="descTd">
                            上户开始日期
                        </td>
                        <td>
                            <JWControl:DateBox ID="txtEndTimesh0" Width="120px" runat="server"></JWControl:DateBox>
                        </td>
                        <td class="descTd">
                            上户结束日期
                        </td>
                        <td>
                            <JWControl:DateBox ID="txtEndTimesh1" Width="120px" runat="server"></JWControl:DateBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            车辆户主
                        </td>
                        <td>
                            <span id="spanVehicleName" style="width: 118px;" class="spanSelect" runat="server">
                                <asp:TextBox ID="txtVehicleName" Style="height: 15px; border: none; line-height: 16px;
                                    width: 91px; margin: 1px 2px;" CssClass="{required:true, messages:{required:'车辆户主必须输入'}}" runat="server"></asp:TextBox>
                                <img style="float: right;" src="../../../images/icon.bmp" id="img1" alt="请选择车辆户主" onclick="selectVehicleName();" runat="server" />

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
                                    width: 100px; margin: 1px 2px;" CssClass="{required:true, messages:{required:'请选择车辆状态'}}" runat="server"></asp:TextBox>
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
                            <JWControl:DateBox ID="txtEndTimeBX1" Width="120px" runat="server"></JWControl:DateBox>
                        </td>
                        <td class="descTd">
                            保险结束日期
                        </td>
                        <td>
                            <JWControl:DateBox ID="txtEndTimeBX2" Width="120px" runat="server"></JWControl:DateBox>
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
                <table class="tab" style="vertical-align: top;">
                    <tr>
                        <td>
                            <div class="pagediv">
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
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="Specification" HeaderText="车辆名称" /><asp:TemplateField HeaderText="车牌号码"><ItemTemplate>
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
                </table>
            </td>
        </tr>
    </table>
    <div id="divFram" title="" style="display: none">
        <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <div id="divFramRole" title="" style="display: none">
        <iframe id="iframeRole" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <div id="divFramPrj" title="" style="display: none">
        <iframe id="ifFramPrj" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <asp:HiddenField ID="hfldContract" runat="server" />
    </form>
</body>
</html>
<script type="text/javascript">
    addLoadEvent(function () {
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
    });
    function selectVehicleState(element) {

        var typeNameControl = element.id;
        if (element.nodeName == 'IMG') {
            typeNameControl = element.previousSibling.previousSibling.id;
        }
        var typeIdControl = typeNameControl.replace('txtVehicleState', 'hfldStateID');
        document.getElementById('ifrmSelectState').src = '/oa/Vehicle/VehicleUserControl/VehicleState.aspx?TypeNameControl=' + typeNameControl + '&TypeIdControl=' + typeIdControl;
        $('#divSelectState').dialog({ width: 350, height: 350, modal: true });
        return false;

        winopen('/oa/Vehicle/VehicleUserControl/VehicleState.aspx?TypeNameControl=' + typeNameControl + '&TypeIdControl=' + typeIdControl)
    }

    function addLoadEvent(func) {
        var oldonload = window.onload;
        if (typeof (oldonload) != 'function') {
            window.onload = func;
        }
        else {
            oldonload;
            func();
        }
    }
    
</script>
