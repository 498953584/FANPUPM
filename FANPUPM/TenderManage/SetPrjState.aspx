<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="SetPrjState.aspx.cs" Inherits="TenderManage_SetPrjState" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>投标管理-手动更改项目状态 </title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../Script/jquery.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js" charset="gb2312"></script>
    <script type="text/javascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../Script/jw.js"></script>
    <script type="text/javascript" src="../Script/wf.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // 添加验证
            $('#btnQuery')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
            setWidthHeight();
            var jwTable = new JustWinTable('gvDataInfo');
            setButton2(jwTable, 'hfldCheckedIds');
            setWfButtonState2(jwTable, 'WF1');
            bindClick();
            //绑定删除按钮事件
            registerDeleteTenderEvent();
            replaceEmptyTable('emptyTable', 'gvDataInfo');
            showTooltip('tooltip', 25);
            $('#txtQualificationReadOne').attr('readOnly', 'readOnly');

            jwTable.registClickTrListener(function () {
                $('#hfldPrjId').val($(this).attr('id'));
            });
            // 项目有变更记录事事件处理
            $('#gvDataInfo tr').live('click', function () {
                if ($(this).attr('flowState') == '0') {
                    $('#btnChange').attr('disabled', 'disabled');
                }
                else {
                    $('#btnChange').removeAttr('disabled');
                }
                // 项目状态为放弃时不能进行状态变更
                if ($(this).attr('PrjState') == '18') {
                    $('#btnChange').attr('disabled', 'disabled');
                }
                if ($(this).attr('changed') != "1") {
                    $('#btnStartWF').attr('disabled', 'disabled');
                }
                else {
                    if ($(this).attr('flowState') == '-1') {
                        $('#btnStartWF').removeAttr('disabled');
                    }
                    $('#btnStateChgShow').removeAttr('disabled');
                }
            });
        });

        // 状态变更
        function changeState() {
            if ($('#hfldPrjId').val() != '') {
                top.ui._prjStateAdjust = window;
                var url = '/TenderManage/prjStateAdjust.aspx?prjId=' + $('#hfldPrjId').val();
                top.ui.openWin({ title: '状态变更', url: url, width: 493, height: 339 });
            }
            else {
                top.ui.alert('请先选择项目！');
            }
        }

        // 查看状态变成信息
        function changeStateShow() {
            if ($('#hfldPrjId').val() != '') {
                var url = '/TenderManage/PrjinfoChangeFlowQuery.aspx?prjId=' + $('#hfldPrjId').val();
                top.ui.openWin({ title: '状态变更查看', url: url });
            } else {
                top.ui.alert('请先选择项目！');
            }
        }

        function setWidthHeight() {
            $('#divContent').height($('#divBody').height() - $('#tbHeader').height() - $('#tbQuery').height() - 5);
        }

        function setButton2(jwTable, hdChkId) {
            if (!jwTable.table) return;
            if (jwTable.table.firstChild.childNodes.length == 1) {
                //table中没有数据
                return;
            }
            jwTable.registClickTrListener(function () {
                if (hdChkId != '') {
                    document.getElementById(hdChkId).value = this.id;
                }
                if (document.getElementById(hdChkId).value != '') {
                    setAllBttons();
                    var selRow = $('#' + $('#hfldCheckedIds').val()).get(0);
                    var prjState = parseInt(selRow.getAttribute('state'));
                    if (prjState != jw.ProjectParameter.WinBid) {
                        document.getElementById('btnEdit').removeAttribute('disabled');
                        document.getElementById('btnDel').removeAttribute('disabled');
                    }
                    else {
                        document.getElementById('btnEdit').setAttribute('disabled', 'disabled');
                        document.getElementById('btnDel').setAttribute('disabled', 'disabled');
                    }
                }
            });
            jwTable.registClickSingleCHKListener(function () {
                var checkedChk = jwTable.getCheckedChk();
                if (checkedChk.length == 0 || checkedChk.length > 1) {
                    if (hdChkId != '') {
                        document.getElementById(hdChkId).value = "";
                    }
                    setAllBttons('disabled');
                    document.getElementById('btnEdit').setAttribute('disabled', 'disabled');
                    document.getElementById('btnDel').setAttribute('disabled', 'disabled');
                }
                else {
                    var checkedChks = jwTable.getCheckedChk();
                    if (hdChkId != '') {
                        var taskId = jwTable.getCheckedChkIdJson(checkedChk);
                        document.getElementById(hdChkId).value = taskId;
                    }
                    setAllBttons();
                    var selRow = $('#' + $('#hfldCheckedIds').val()).get(0);
                    var prjState = parseInt(selRow.getAttribute('state'));
                    if (prjState != jw.ProjectParameter.WinBid) {
                        document.getElementById('btnEdit').removeAttribute('disabled');
                        document.getElementById('btnDel').removeAttribute('disabled');
                    }
                    else {
                        document.getElementById('btnEdit').setAttribute('disabled', 'disabled');
                        document.getElementById('btnDel').setAttribute('disabled', 'disabled');
                    }
                }
            });
            jwTable.registClickAllCHKLitener(function () {
                if (this.checked) {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson();
                    var checkedChks = jwTable.getAllChk();
                    if (checkedChks.length == 1) {
                        setAllBttons();
                        var selRow = $('#' + $('#hfldCheckedIds').val()).get(0);
                        var prjState = parseInt(selRow.getAttribute('state'));
                        if (prjState != jw.ProjectParameter.WinBid) {
                            document.getElementById('btnEdit').removeAttribute('disabled');
                            document.getElementById('btnDel').removeAttribute('disabled');
                        }
                        else {
                            document.getElementById('btnEdit').setAttribute('disabled', 'disabled');
                            document.getElementById('btnDel').setAttribute('disabled', 'disabled');
                        }
                    }
                    else {
                        setAllBttons('disabled');
                        document.getElementById('btnEdit').setAttribute('disabled', 'disabled');
                        document.getElementById('btnDel').setAttribute('disabled', 'disabled');
                    }
                }
                else {
                    setAllBttons('disabled');
                    document.getElementById('btnEdit').setAttribute('disabled', 'disabled');
                    document.getElementById('btnDel').setAttribute('disabled', 'disabled');
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = '';
                }
            });
        }

        //禁用/释放
        function setAllBttons(disabled) {
            if (disabled != undefined && disabled != '') {
                $(":button[name='state']").each(function (i) {
                    $(this).attr('disabled', 'disabled');
                });
            }
            else {
                var selRow = $('#' + $('#hfldCheckedIds').val()).get(0);
                var prjState = parseInt(selRow.getAttribute('state'));
                var prjInfoState = selRow.getAttribute('prjInfoState');
                var flowState = selRow.getAttribute('flowState');
                $(":button[name='state']").each(function (i) {
                    $(this).attr('disabled', 'disabled');
                });
                if (flowState == 1) {
                    //流程
                    if (prjState > 1) {
                        arguments.callee('disabled');
                        if (prjInfoState == '') {
                            if (prjState == jw.ProjectParameter.Approval) {//信息立项
                                $('#btnStart').removeAttr('disabled');
                            }
                            else if (prjState == jw.ProjectParameter.Initiate) {//启动
                                $('#btnSetUp').removeAttr('disabled');
                                $('#btnYs').removeAttr('disabled');
                            }
                            else if (prjState == jw.ProjectParameter.Prequalification) {//资格预审
                                $('#btnStart').removeAttr('disabled');
                                $('#btnYsPass').removeAttr('disabled');
                                $('#btnYsFail').removeAttr('disabled');
                            }
                            else if (prjState == jw.ProjectParameter.QualificationPass) {//预审成功
                                $('#btnYs').removeAttr('disabled');
                                $('#btnBid').removeAttr('disabled');
                            }
                            else if (prjState == jw.ProjectParameter.QualificationFail) { //预审失败
                                $('#btnYs').removeAttr('disabled');
                            }
                            else if (prjState == jw.ProjectParameter.Bid) {//投标
                                $('#btnYsPass').removeAttr('disabled');
                                $('#btnGet').removeAttr('disabled');
                                $('#btnLost').removeAttr('disabled');
                            }
                            else if (prjState == jw.ProjectParameter.WinBid) { //中标
                                $('#btnBid').removeAttr('disabled');
                            }
                            else if (prjState == jw.ProjectParameter.OutBid) { //落标
                                $('#btnBid').removeAttr('disabled');
                            }
                        }
                    }

                }
            }
        }


        //绑定按钮点击事件
        function bindClick() {
            $(":button[name='state']").each(function (i) {
                $(this).bind('click', function () {
                    //选中项目的状态
                    var state = parseInt($('#' + $('#hfldCheckedIds').val()).attr('state'));
                    //按钮所代表的项目状态
                    var btnState = parseInt(this.getAttribute('state'));
                    //保存状态值
                    $('#hfldClinkButton').val(btnState);
                    //弹出层ID
                    var divId = 'div' + this.id;
                    $('#hfldDivId').val(divId);
                    //移到Dialog中，否则按钮事件无效
                    $('#' + divId).append($('#btnSaveData'));
                    //弹出信息进行,如果是投标，进行标签页的形式收入投标信息                    
                    if (btnState != '4') {
                        if ((btnState == 3 && state == 2)
                        || (btnState == 14 && state == 3)
                        || (btnState == 15 && state == 14)
                        || (btnState == 4 && state == 15)
                        || (btnState == 16 && state == 14)
                        || ((btnState == 5 || btnState == 6) && state == 4)) {
                            $.ajax({
                                type: "GET",
                                async: false,
                                url: '/TenderManage/Handler/GetTenderInfo.ashx?' + new Date().getTime() + '&guid=' + $('#hfldCheckedIds').val() + '&part=' + btnState + '&userCode=' + $('#hfldUserCode').val(),
                                success: function (datas) {
                                    var data = datas[0];
                                    if (btnState == jw.ProjectParameter.Initiate) {
                                        $('#txtStartDate').val(data.ProjStartDate);
                                        $('#txtPrjDutyPersonName').val(data.PrjDutyPersonName);
                                        $('#hfldPrjDutyPerson').val(data.PrjDutyPerson);
                                        $('#txtPrjManager').val(data.PrjManagerName);
                                        $('#hfldPrjManager').val(data.PrjManager);
                                        $('#txtStartRemark').val(data.ProjStartRemark);
                                    }
                                    else if (btnState == jw.ProjectParameter.Prequalification) {
                                        $('#txtApplyDate').val(data.ProjApplyDate);
                                        $('#txtApprovalDate').val(data.ProjApprovalDate);
                                        $('#txtTenderDate').val(data.ProjTenderDate);
                                        $('#txtRegistDeadline').val(data.ProjRegistDeadline);
                                        $('#hfldAgent').val(data.ProgAgent);
                                        $('#txtAgent').val(data.ProgAgentName);
                                        //$('#hfldDutyPerson').val(data.PrjDutyPerson);
                                        //$('#txtDutyPerson').val(data.PrjDutyPersonName);
                                        $('#txtPrequalificationRequire').val(data.PrequalificationRequire);
                                        $('#txtQualificationMargin').val(data.QualificationMargin);         // 预审保证金
                                        $('#hfldQualificationReadOne').val(data.QualificationReadOne);      // 预审阅知人
                                        $('#txtQualificationReadOne').val(data.QReadOneName);               // 预审阅知人名称
                                    }
                                    else if (btnState == jw.ProjectParameter.QualificationPass) {
                                        $('#txtPassDate').val(data.QualificationPassDate);
                                        $('#txtPassReason').val(data.QualificationPassReason);
                                        $('#txtApprovalDate1').val(data.ProjApprovalDate);
                                        $('#txtQualificationMargin1').val(data.QualificationMargin);    // 预审保证金
                                        $('#txtTenderDate1').val(data.ProjTenderDate);
                                        $('#txtRegistDeadline1').val(data.ProjRegistDeadline);
                                        $('#txtAgent1').val(data.ProgAgentName);
                                        $('#txtPrequalificationRequire1').val(data.PrequalificationRequire);
                                        $('#txtApplyDate1').val(data.ProjApplyDate);
                                    }
                                    else if (btnState == jw.ProjectParameter.QualificationFail) {
                                        $('#txtFailDate').val(data.QualificationFailData);
                                        $('#txtFailReason').val(data.QualificationFailReason);
                                        $('#txtApplyDate2').val(data.ProjApplyDate);
                                        $('#txtApprovalDate2').val(data.ProjApprovalDate);
                                        $('#txtTenderDate2').val(data.ProjTenderDate);
                                        $('#txtRegistDeadline2').val(data.ProjRegistDeadline);
                                        $('#txtAgent2').val(data.ProgAgentName);
                                        //$('#txtDutyPerson1').val(data.PrjDutyPersonName);
                                        $('#txtPrequalificationRequire2').val(data.PrequalificationRequire);
                                        $('#txtQualificationMargin2').val(data.QualificationMargin);    // 预审保证金
                                    }
                                    else if (btnState == jw.ProjectParameter.Bid) {
                                        $('#txtTenderBeginDate').val(data.ProjTenderBeginDate);
                                        $('#txtTenderCeilingPrice').val(data.TenderCeilingPrice);
                                        $('#txtTenderUnit').val(data.TenderUnit);
                                        $('#txtTenderQuote').val(data.TenderQuote);
                                        $('#txtTenderAppraiseMethod').val(data.TenderAppraiseMethod);
                                        $('#txtTenderAverage').val(data.TenderAverage);
                                        $('#txtTenderAnswerDate').val(data.ProjTenderAnswerDate);
                                        $('#txtTenderEarnestMoney').val(data.ProjTenderEarnestMoney);
                                        if (data.ProjTenderPayWay != null) {
                                            var index = data.ProjTenderPayWay;
                                            $('#RblTenderPayWay_' + index).get(0).checked = true;
                                        }
                                        $('#txtTenderPrjManager').val(data.PrjManagerName);
                                        $('#hfldTenderPrjManager').val(data.PrjManager);
                                        $('#txtTenderCostContent').val(data.ProjTenderCostContent);
                                        $('#txtTenderContent').val(data.ProjTenderContent);
                                        $('#txtTenderRemark').val(data.ProjTenderRemark);

                                    }
                                    else if (btnState == jw.ProjectParameter.WinBid) {
                                        $('#txtSuccessBidDate').val(data.SuccessBidDate);
                                        $('#txtSuccessBidPrice').val(data.SuccessBidPrice);
                                        $('#txtSuccessBidRemark').val(data.SuccessBidRemark);
                                    }
                                    else if (btnState == jw.ProjectParameter.OutBid) {
                                        $('#txtOutBidDate').val(data.OutBidDate);
                                        if (data.OutBidIsReturn != null) {
                                            var index = data.OutBidIsReturn ? "1" : "0";
                                            $('#RblOutBidIsReturn_' + index).get(0).checked = true;
                                        }
                                        $('#txtOutBidRemark').val(data.OutBidRemark);
                                    }
                                }
                            });
                            if (btnState == jw.ProjectParameter.QualificationPass) {
                                $.ajax({
                                    type: "GET",
                                    async: false,
                                    url: '/TenderManage/Handler/GetTenderFile.ashx?' + new Date().getTime() + '&file=' + $('#hfldPrjId').val() + '_' + jw.ProjectParameter.Prequalification,
                                    success: function (data) {
                                        $('#file_yspass').html(data);
                                    }
                                });
                            }
                            if (btnState == jw.ProjectParameter.QualificationFail) {
                                $.ajax({
                                    type: "GET",
                                    async: false,
                                    url: '/TenderManage/Handler/GetTenderFile.ashx?' + new Date().getTime() + '&file=' + $('#hfldPrjId').val() + '_' + jw.ProjectParameter.Prequalification,
                                    success: function (data) {
                                        $('#file_ysfail').html(data);
                                    }
                                });
                            }
                            $('#' + divId).dialog({
                                open: function () {
                                    $(this).parent().appendTo("form:first");
                                    setFilelUpPath(btnState);
                                },
                                width: 610,
                                height: 300,
                                modal: true,
                                title: this.value,
                                buttons: {
                                    "保存": function () {
                                        saveData(divId, btnState);
                                    },
                                    "取消": function () {
                                        $(this).dialog("close");
                                    }
                                }
                            });
                        }
                        else {
                            $('#btnSaveData').click();
                        }
                    }
                    else { //直接保存
                        if (btnState == '4') {
                            openTabPage(btnState);
                        }
                        else {
                            $('#btnSaveData').click();
                        }
                    }
                });
            });
        }

        //保存数据
        function saveData(divId, btnState) {
            if (btnState == jw.ProjectParameter.Initiate && $('#txtStartDate').val() == '') {
                alert('系统提示:\n\n启动日期必须输入');
                $('#txtStartDate').focus();
                return;
            }
            else if (btnState == jw.ProjectParameter.Bid && $('#txtTenderBeginDate').val() == '') {
                alert('系统提示:\n\n启动日期必须输入');
                $('#txtTenderBeginDate').focus();
                return;
            }
            else if (btnState == jw.ProjectParameter.Prequalification && $('#txtApprovalDate').val() == '') {
                alert('系统提示:\n\n预审日期必须输入');
                $('#txtApprovalDate').focus();
                return;
            }
            $('#btnSaveData').click();
        }
        //        //设置上传附件路径
        //        function setFilelUpPath(btnState) {
        //            var recordCode = $('#hfldCheckedIds').val() + '_' + btnState;
        //            updateRecordCode('FileUpload_' + btnState, recordCode);
        //        }

        //编辑按钮事件
        function edit() {
            parent.desktop.InfoAdd = window;
            var url = "/TenderManage/InfoAdd.aspx?Action=Update&purl=SetPrjState.aspx&PrjId=" + document.getElementById('hfldCheckedIds').value;
            toolbox_oncommand(url, "编辑项目信息");
        }
        function registerDeleteTenderEvent() {
            var btnDelete = document.getElementById('btnDel');
            btnDelete.onclick = function () {
                if (!window.confirm('系统提示：\n\n确定要删除吗？')) {
                    return false;
                }
            }
        }

        //选择人员返回值
        function returnUser(id, name) {
            var val = document.getElementById("hdReturnVal").value.split(',');
            document.getElementById(val[0]).value = id;
            document.getElementById(val[1]).value = name;

        }
        //选择人员
        function selectUser(id, name) {
            document.getElementById("hdReturnVal").value = id + "," + name;
            var url = "/Common/DivSelectUser.aspx?type=tender&Method=returnUser";
            document.getElementById("IframePerson").src = url;
            selectnEvent('divFramPerson');
        }
        //选择建设单位
        function pickCorp() {
            jw.selectOneCorp({ nameinput: 'txtBuildUnit' });
        }
        //投标
        function openTabPage(btnState) {
            //选中项目的状态
            var state = parseInt($('#' + $('#hfldCheckedIds').val()).attr('state'));
            if (btnState > state || state == 15) {
                parent.desktop.BidSet = window;
                var id = document.getElementById('hfldCheckedIds').value;
                var url = "/TenderManage/BidSet.aspx?purl=SetPrjState.aspx&id=" + id;
                toolbox_oncommand(url, "投标基本资料");
            } else {
                $('#btnSaveData').click();
            }
        }
    </script>
    <style type="text/css">
        .adjunct
        {
            white-space: nowrap;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divBody" style="height: 100%; overflow: hidden;">
        <div>
            <table class="queryTable" id="tbQuery" cellpadding="3px" cellspacing="0px">
                <tr>
                    <td class="descTd" style="white-space: nowrap;">
                        项目编号
                    </td>
                    <td>
                        <asp:TextBox ID="txtPrjCode" Style="width: 120px;" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
                    </td>
                    <td class="descTd" style="white-space: nowrap;">
                        项目名称
                    </td>
                    <td>
                        <asp:TextBox ID="txtPrjName" Style="width: 120px;" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
                    </td>
                    <td class="descTd" style="white-space: nowrap;">
                        项目类型
                    </td>
                    <td>
                        <asp:DropDownList ID="dropPrjType" Style="min-width: 150px; width: auto;" runat="server"></asp:DropDownList>
                    </td>
                    <td class="descTd" style="white-space: nowrap;">
                        项目状态
                    </td>
                    <td>
                        <asp:DropDownList ID="dropPrjState" Width="100px" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="descTd" style="white-space: nowrap;">
                        立项申请日期
                    </td>
                    <td>
                        <asp:TextBox ID="txtStartTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                    </td>
                    <td class="descTd" style="white-space: nowrap;">
                        至
                    </td>
                    <td>
                        <asp:TextBox ID="txtEndTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                    </td>
                    <td class="descTd" style="white-space: nowrap;">
                        建设单位
                    </td>
                    <td class="descTd" >
                        <span class="spanSelect" style="width:120px">
                            <asp:TextBox ID="txtBuildUnit" Style="width: 95px; height: 15px; border: none; line-height: 15px;
                                margin: 1px 0px 1px 2px;" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
                            <img alt="选择建设单位" onclick="pickCorp();" src="../../images/icon.bmp" style="float: right;" />
                        </span>
                    </td>
                    <td class="descTd" style="white-space: nowrap;">
                        流程状态
                    </td>
                    <td>
                        <asp:DropDownList ID="dropWFState" Width="100px" runat="server"><asp:ListItem Text="" Value="" Selected="true" /><asp:ListItem Text="未提交" Value="-1" /><asp:ListItem Text="审核中" Value="0" /><asp:ListItem Text="已审核" Value="1" /><asp:ListItem Text="驳回" Value="-2" /><asp:ListItem Text="重报" Value="-3" /></asp:DropDownList>
                    </td>
                    <td style="white-space: nowrap;">
                        <asp:Button ID="btnQuery" Text="查询" OnClick="btnQuery_Click" runat="server" />
                    </td>
                </tr>
            </table>
            <table id="tbHeader" border="0" class="tab" style="width: 100%; margin-top: 0px;">
                <tr>
                    <td class="divFooter" style="text-align: left;">
                        <input type="button" value="编辑" id="btnEdit" disabled="disabled" onclick="edit()" />
                        <asp:Button ID="btnDel" Text="删除" disabled="disabled" OnClick="btnDelete_Click" runat="server" />
                        <input type="button" value="状态变更" id="btnChange" style="width: 70px;" onclick="changeState();" />
                        <input type="button" value="状态变更查看" id="btnStateChgShow" style="width: 90px;" onclick="changeStateShow();" />
                        <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="140" BusiClass="001" runat="server" />
                        <input type="hidden" id="hd" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="divContent" style="overflow: auto;">
            <asp:GridView ID="gvDataInfo" CssClass="gvdata" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="gvDataInfo_RowDataBound" DataKeyNames="PrjGuid,PrjState,PrjInfoState,ProjFlowSate,ChangeFlowSate" runat="server">
<EmptyDataTemplate>
                    <table id="emptyTable">
                        <tr>
                            <th class="header">
                                序号
                            </th>
                            <th class="header">
                                项目状态
                            </th>
                            <th class="header">
                                变更为
                            </th>
                            <th class="header">
                                项目经理
                            </th>
                            <th class="header">
                                项目编号
                            </th>
                            <th class="header">
                                项目名称
                            </th>
                            <th class="header">
                                建设单位
                            </th>
                            
                            <th class="header">
                                工程造价
                            </th>
                            <th class="header">
                                工程工期
                            </th>
                            <th class="header">
                                流程状态
                            </th>
                            <th class="header">
                                立项申请日期
                            </th>
                        </tr>
                    </table>
                </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                            <asp:CheckBox ID="cbAllBox" runat="server" />
                        </HeaderTemplate><ItemTemplate>
                            <asp:CheckBox ID="cbBox" runat="server" />
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                            <%# Eval("No") %>
                        </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="项目状态" DataField="StateText" /><asp:TemplateField HeaderText="变更为"><ItemTemplate>
                            <%# GetPrjStateName(Eval("changeState").ToString()) %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目经理"><ItemTemplate>
                            <span class="tooltip" title="<%# Eval("Person") %>">
                                <%# StringUtility.GetStr(Eval("Person").ToString(), 25) %>
                            </span>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目编号"><ItemTemplate>
                            <%# Eval("PrjCode") %>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目名称"><ItemTemplate>
                            <asp:HyperLink ID="hlinkShowName" CssClass="tooltip" Style="text-decoration: none;
                                color: Black;" ToolTip='<%# System.Convert.ToString(Eval("PrjName").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server">
                        <span class="link" onclick="viewopen('/TenderManage/InfoView.aspx?&&ic=<%# Eval("PrjGuid") %>', '项目信息查看')">
                        <%# StringUtility.GetStr(Eval("PrjName").ToString(), 25) %>
                       </span></asp:HyperLink>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="建设单位"><ItemTemplate>
                            <span class="tooltip" title="<%# Eval("WorkUnitName") %>">
                                <%# StringUtility.GetStr(Eval("WorkUnitName").ToString(), 25) %>
                            </span>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程造价" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                            <%# Eval("PrjCost") %></ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
                            <%# Common2.GetState(Eval("ChangeFlowSate").ToString()) %></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="立项申请日期"><ItemTemplate>
                            <%# Common2.GetTime(Eval("InputDate")) %>
                        </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
            <asp:Label ID="lblTotal" runat="server"></asp:Label>
            <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
            </webdiyer:AspNetPager>
        </div>
    </div>

    <div id="divFramPerson" title="选择人员" style="display: none">
        <iframe id="IframePerson" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <asp:HiddenField ID="hfldCheckedIds" runat="server" />
    <asp:HiddenField ID="hfldClinkButton" runat="server" />
    <asp:HiddenField ID="hfldDivId" runat="server" />
    <asp:HiddenField ID="hfldUserCode" runat="server" />
    <!-- 保存选中的项目Id-->
    <asp:HiddenField ID="hfldPrjId" runat="server" />
    <link rel="Stylesheet" type="text/css" href="../Script/jquery.easyui/themes/default/easyui.css" />
    <link rel="Stylesheet" type="text/css" href="../Script/jquery.easyui/themes/icon.css" />
    </form>
</body>
</html>
