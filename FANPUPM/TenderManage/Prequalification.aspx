<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="Prequalification.aspx.cs" Inherits="TenderManage_Prequalification" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>资格预审</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link Href="/Script/jquery.treeview/jquery.treeview.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="../Script/jquery.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/ecmascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="/Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../Script/jw.js"></script>
    <script type="text/javascript" src="../Script/wf.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            // 添加验证
            $('#brnQuery')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
            replaceEmptyTable('emptyTable', 'gvDataInfo');
            var jwTable = new JustWinTable('gvDataInfo');
            showTooltip('tooltip', 25);
            setWfButtonState2(jwTable, 'WF1');
            setButton(jwTable, '', '', '', 'hfldPrjId');
            var btnEdit = $('#btnEdit');
            var btnPass = $('#btnPass');
            var btnFail = $('#btnFail');
            var btnGiveUp = $('#btnGiveUp');
            jwTable.registClickTrListener(function () {
                $('#hfldPrjId').val($(this).attr('id'));
                var prjState = $(this).attr('state');
                $('#hfldPrjState').val(prjState);
                if (prjState == jw.ProjectParameter.GiveUpState) {
                    $('#WF1_BusinessCode').attr('value', '138');
                } else {
                    $('#WF1_BusinessCode').attr('value', '136');
                }
                var flowState = $(this).attr('flowState');
                if (flowState == '-1' || flowState == '-3') {
                    btnPass.removeAttr('disabled');
                    btnFail.removeAttr('disabled');
                    btnGiveUp.removeAttr('disabled');
                } else {
                    btnPass.attr('disabled', 'disabled');
                    btnFail.attr('disabled', 'disabled');
                    btnGiveUp.attr('disabled','disabled');
                }
            });

            $('#gvDataInfo tr').live('click', function () {
                var prjState = $(this).attr('state');
                if (prjState == '14'||prjState=='3') {
                    $('#btnStartWF').attr('disabled', 'disabled');
                }
            });

            btnPassClick();
            btnFailClick();
            btnGiveUpClick();
            registerUpdateTenderEvent();
        });

        //点击放弃按钮事件

        function btnGiveUpClick() {
            $('#btnGiveUp').bind('click', function () {
                top.ui._GiveUpInfo = window;
                var url = '/TenderManage/GiveUpInfo.aspx?' + new Date().getTime() + '&prjId=' + $('#hfldPrjId').val();
                top.ui.openWin({ title: '放弃管理', url: url });
                //                var url = '/TenderManage/Handler/GetPrjInfoZTB.ashx?' + new Date().getTime() + '&prjId=' + $('#hfldPrjId').val() + '&state=' + $('#hfldOldState').val();
                //                $.ajax({
                //                    type: "GET",
                //                    dataType: "json",
                //                    async: false,
                //                    url: url,
                //                    success: function (data) {
                //                        if (data != null) {
                //                            $('#txtGiveUpTime').val(data.GiveUpTime);
                //                            $('#hfldOperator').val(data.Operator);
                //                            $('#txtOperator').val(data.OperatorName);
                //                            $('#txtGiveUPReason').val(data.GiveUpReason);
                //                            $('#txtNote').val(data.GiveUpNote);
                //                        }
                //                        else {
                //                            var dateTime = window["jw"].dateToStr(new Date(), "-");
                //                            $('#txtGiveUpTime').val(dateTime);
                //                        }
                //                    },
                //                    error: function (errorMsg) {
                //                        alert(errorMsg);
                //                    }
                //                });
                //                $('#div4').dialog({
                //                    open: function () {
                //                        $(this).parent().appendTo("form:first");
                //                        setFilelUpPathGiveUp();
                //                    },
                //                    close: function () {
                //                        $('#txtGiveUpTime').val('');
                //                        $('#hfldOperator').val('');
                //                        $('#txtOperator').val('');
                //                        $('#txtGiveUPReason').val('');
                //                        $('#txtNote').val('');
                //                    },
                //                    width: 600,
                //                    height: 310,
                //                    modal: true
                //                });
            });
        }

//        //设置放弃上传附件路径
//        function setFilelUpPathGiveUp() {
//            var recordCode = $('#hfldPrjId').val() + '_18';
//            updateRecordCode('fileGiveUp', recordCode);
//        }

        function registerUpdateTenderEvent() {
            var btnUpdate = $('#btnEdit');
            var hfldPrjId = $('#hfldPrjId');
            addEvent(btnUpdate, 'click', function () {
                parent.desktop.InfoAdd = window;
                var url = "/TenderManage/InfoAdd.aspx?Action=Update&purl=Prequalification.aspx&PrjId=" + hfldPrjId.value;
                toolbox_oncommand(url, "编辑项目信息");
            });
        }
        function btnPassClick() {
            var btnPass = document.getElementById('btnPass');
            addEvent(btnPass, 'click', function () {
                top.ui._QulificationPass = window;
                var url = '/TenderManage/QulificationPass.aspx?' + new Date().getTime() + '&prjId=' + $('#hfldPrjId').val();
                top.ui.openWin({ title: '预审通过基本资料', url: url });
                //$('#hdflOldPrjState').val(jw.ProjectParameter.QualificationPass);
                //                $('#div_yspass').append($('#btnSaveData'));
                //                $.ajax({
                //                    type: "GET",
                //                    async: false,
                //                    url: '/TenderManage/Handler/GetTenderInfo.ashx?' + new Date().getTime() + '&guid=' + $('#hfldPrjId').val() + '&part=15&userCode=' + $('#hfldUserCode').val(),
                //                    success: function (datas) {
                //                        var data = datas[0];
                //                        if ($('#hfldPrjState').val() == jw.ProjectParameter.QualificationPass) {
                //                            $('#txtApplyDate').val(data.ProjApplyDate);
                //                            $('#txtApprovalDate').val(data.ProjApprovalDate);
                //                            $('#txtTenderDate').val(data.ProjTenderDate);
                //                            $('#txtRegistDeadline').val(data.ProjRegistDeadline);
                //                            $('#txtAgent').val(data.ProgAgentName);
                //                            $('#hfldAgent').val(data.ProgAgent);
                //                            //$('#txtDutyPerson').val(data.PrjDutyPersonName);
                //                            $('txtQualificationReadOne').val(data.QReadOneName);
                //                            $('#hfldQualificationReadOne').val(data.QualificationReadOne)
                //                            $('#txtPrequalificationRequire').val(data.PrequalificationRequire);
                //                            $('#txtPassDate').val(data.QualificationPassDate);
                //                            $('#txtPassReason').val(data.QualificationPassReason);
                //                            $('#txtQualificationMargin').val(data.QualificationMargin);     // 预审保证金
                //                        } else {
                //                            $('#txtApplyDate').val('');
                //                            $('#txtApprovalDate').val('');
                //                            $('#txtTenderDate').val('');
                //                            $('#txtRegistDeadline').val('');
                //                            $('#txtAgent').val('');
                //                            $('#hfldAgent').val('');
                //                            $('txtQualificationReadOne').val('');
                //                            $('#hfldQualificationReadOne').val('')
                //                            $('#txtPrequalificationRequire').val('');
                //                            $('#txtPassDate').val('');
                //                            $('#txtPassReason').val('');
                //                            $('#txtQualificationMargin').val('');     // 预审保证金
                //                        }
                //                    }
                //                });

                //                $.ajax({
                //                    type: "GET",
                //                    async: false,
                //                    url: '/TenderManage/Handler/GetTenderFile.ashx?' + new Date().getTime() + '&file=' + $('#hfldPrjId').val() + '_' + jw.ProjectParameter.Prequalification,
                //                    success: function (data) {
                //                        $('#file_yspass').html(data);
                //                    }
                //                });

                //                $('#div_yspass').dialog({
                //                    open: function () {
                //                        $(this).parent().appendTo("form:first");
                //                        setFilePassPath();
                //                    },
                //                    width: 600,
                //                    height: 450,
                //                    modal: true,
                //                    buttons: {
                //                        "保存": function () {
                //                            $('#btnSaveData').click();
                //                        },
                //                        "取消": function () {
                //                            $(this).dialog("close");
                //                        }
                //                    }
                //                });
            });
        }

        function btnFailClick() {
            var btnFail = document.getElementById('btnFail');

            addEvent(btnFail, 'click', function () {
                $('#hdflOldPrjState').val(jw.ProjectParameter.QualificationFail);
                top.ui._QulifcationFail = window;
                var url = '/TenderManage/QulifcationFail.aspx?' + new Date().getTime() + '&prjId=' + $('#hfldPrjId').val();
                top.ui.openWin({ title: '预审失败基本资料', url: url });
                //                $('#div_ysfail').append($('#btnSaveData'));
                //                $.ajax({
                //                    type: "GET",
                //                    async: false,
                //                    url: '/TenderManage/Handler/GetTenderInfo.ashx?' + new Date().getTime() + '&guid=' + $('#hfldPrjId').val() + '&part=16&userCode=' + $('#hfldUserCode').val(),
                //                    success: function (datas) {
                //                        var data = datas[0];
                //                        if ($('#hfldPrjState').val() == jw.ProjectParameter.QualificationFail) {
                //                            $('#txtApplyDate1').val(data.ProjApplyDate);
                //                            $('#txtApprovalDate1').val(data.ProjApprovalDate);
                //                            $('#txtTenderDate1').val(data.ProjTenderDate);
                //                            $('#txtRegistDeadline1').val(data.ProjRegistDeadline);
                //                            $('#txtAgent1').val(data.ProgAgentName);
                //                            $('#hfldAgent1').val(data.ProgAgent);
                //                            $('txtQualificationReadOne1').val(data.QReadOneName);
                //                            $('#hfldQualificationReadOne').val(data.QualificationReadOne);
                //                            //$('#txtDutyPerson1').val(data.PrjDutyPersonName);
                //                            $('#txtPrequalificationRequire1').val(data.PrequalificationRequire);
                //                            $('#txtFailDate').val(data.QualificationFailData);
                //                            $('#txtFailReason').val(data.QualificationFailReason);
                //                            $('#txtQualificationMargin1').val(data.QualificationMargin);    // 预审保证金
                //                        } else {
                //                            $('#txtApplyDate1').val('');
                //                            $('#txtApprovalDate1').val('');
                //                            $('#txtTenderDate1').val('');
                //                            $('#txtRegistDeadline1').val('');
                //                            $('#txtAgent1').val('');
                //                            $('#hfldAgent1').val('');
                //                            $('txtQualificationReadOne1').val('');
                //                            $('#hfldQualificationReadOne').val('');
                //                            $('#txtPrequalificationRequire1').val('');
                //                            $('#txtFailDate').val('');
                //                            $('#txtFailReason').val('');
                //                            $('#txtQualificationMargin1').val('');    // 预审保证金
                //                        }
                //                    }
                //                });

                //                $.ajax({
                //                    type: "GET",
                //                    async: false,
                //                    url: '/TenderManage/Handler/GetTenderFile.ashx?' + new Date().getTime() + '&file=' + $('#hfldPrjId').val() + '_' + jw.ProjectParameter.Prequalification,
                //                    success: function (data) {
                //                        $('#file_ysfail').html(data);
                //                    }
                //                });
                //                $('#div_ysfail').dialog({
                //                    open: function () {
                //                        $(this).parent().appendTo("form:first");
                //                        setFileFailPath();
                //                    },
                //                    width: 600,
                //                    height: 450,
                //                    modal: true,
                //                    buttons: {
                //                        "保存": function () {
                //                            $('#btnSaveData').click();
                //                        },
                //                        "取消": function () {
                //                            $(this).dialog("close");
                //                        }
                //                    }
                //                });
            });
        }

//        //设置上传附件路径
//        function setFilePassPath() {
//            var recordCode = $('#hfldPrjId').val() + '_' + jw.ProjectParameter.QualificationPass;
//            updateRecordCode('FilePass', recordCode);
//        }

//        //设置上传附件路径
//        function setFileFailPath() {
//            var recordCode = $('#hfldPrjId').val() + '_' + jw.ProjectParameter.QualificationFail;
//            updateRecordCode('FileFail', recordCode);
//        }

        //选择人员
        function selectUser(id, name) {
            jw.selectOneUser({ nameinput: name, codeinput: id });
        }

        //甲方名称
        function pickCorp(param) {
            jw.selectOneCorp({ idinput: 'hfldOwner', nameinput: 'txtOwner' });
        }
        function Query(path) {
            parent.desktop.InfoAdd = window;
            toolbox_oncommand(path, "信息立项申请");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <table class="queryTable" cellpadding="3px" cellspacing="0px">
                        <tr>
                            <td class="descTd" style="white-space: nowrap;">
                                项目编号
                            </td>
                            <td>
                                <asp:TextBox ID="txtPrjCode" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd" style="white-space: nowrap;">
                                项目名称
                            </td>
                            <td>
                                <asp:TextBox ID="txtPrjName" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd" style="white-space: nowrap;">
                                项目跟踪人
                            </td>
                            <td>
                                <span class="spanSelect" style="width: 122px">
                                    <asp:TextBox ID="txtName" Style="width: 95px; height: 15px; border: none;
                                        line-height: 16px; margin: 1px 2px" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
                                    <img src="../../images/icon.bmp" style="float: right;" alt="选择" id="imgName" onclick="selectUser('hfldName','txtName');" runat="server" />

                                </span>
                                <input id="hfldName" type="hidden" style="width: 1px" runat="server" />

                            </td>
                            <td class="descTd" style="white-space: nowrap;">
                                项目类型
                            </td>
                            <td>
                                <asp:DropDownList ID="dropPrjKindClass" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="descTd" style="white-space: nowrap;">
                                立项申请日期
                            </td>
                            <td>
                                <asp:TextBox ID="txtStartTime" onclick="WdatePicker()" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd" style="white-space: nowrap;">
                                至
                            </td>
                            <td>
                                <asp:TextBox ID="txtEndTime" onclick="WdatePicker()" runat="server"></asp:TextBox>
                            </td>
                            <td class="descTd" style="white-space: nowrap;">
                                建设单位
                            </td>
                            <td>
                                <span class="spanSelect" style="width: 122px;">
                                    <asp:TextBox ID="txtOwner" Style="width: 95px; height: 15px; border: none; line-height: 16px;
                                        margin: 1px 0px 1px 2px;" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
                                    <img alt="选择乙方" onclick="pickCorp();" src="../../images/icon.bmp" style="float: right;" />
                                </span>
                                <asp:HiddenField ID="hfldOwner" runat="server" />
                            </td>
                            <td class="descTd" style="white-space: nowrap;">
                                项目状态
                            </td>
                            <td>
                                <asp:DropDownList ID="dropPrjState" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                            <td style="white-space: nowrap;">
                                <asp:Button ID="brnQuery" Text="查询" OnClick="brnQuery_Click" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top">
                    <table class="tab" style="vertical-align: top;">
                        <tr>
                            <td class="divFooter" style="text-align: left">
                                <input type="button" id="btnEdit" value="编辑" disabled="disabled"  style="display:none;"/>
                                <input type="button" id="btnPass" disabled="disabled" value="预审通过" style="width: 80px" />
                                <input type="button" id="btnFail" disabled="disabled" value="预审失败" style="width: 80px" />
                                <input type="button" id="btnGiveUp" disabled="disabled" value="放弃" />
                                <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="136" BusiClass="001" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <asp:GridView ID="gvDataInfo" CssClass="gvdata" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="gvDataInfo_RowDataBound" DataKeyNames="PrjGuid,PrjState,PftFlowState,GiveUpFlowState" runat="server">
<EmptyDataTemplate>
                                            <table id="emptyTable">
                                                <tr class="header">
                                                    <th scope="col">
                                                        序号
                                                    </th>
                                                    <th scope="col">
                                                        项目状态
                                                    </th>
                                                    <th scope="col">
                                                        项目跟踪人
                                                    </th>
                                                    <th scope="col">
                                                        项目编号
                                                    </th>
                                                    <th scope="col">
                                                        项目名称
                                                    </th>
                                                    <th scope="col">
                                                        建设单位
                                                    </th>
                                                    
                                                    <th scope="col">
                                                        工程造价
                                                    </th>
                                                    <th scope="col">
                                                        工程工期
                                                    </th>
                                                    <th scope="col">
                                                        立项申请日期
                                                    </th>
                                                    <th scope="col">
                                                       流程状态
                                                    </th>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                                    <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="项目状态" DataField="StateText" /><asp:BoundField HeaderText="项目跟踪人" DataField="Person" /><asp:BoundField HeaderText="项目编号" DataField="PrjCode" /><asp:TemplateField HeaderText="项目名称" HeaderStyle-Width="150px"><ItemTemplate>
                                                    <asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" CssClass="tooltip" ToolTip='<%# System.Convert.ToString(Eval("PrjName").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server">
                        <span class="link" onclick="viewopen('/TenderManage/InfoView.aspx?ic=<%# Eval("PrjGuid") %>')">
                        <%# StringUtility.GetStr(Eval("PrjName").ToString(), 25) %>
                       </span></asp:HyperLink>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="建设单位"><ItemTemplate>
                                                    <span class="tooltip" title="<%# Eval("WorkUnitName") %>">
                                                        <%# StringUtility.GetStr(Eval("WorkUnitName").ToString(), 25) %>
                                                    </span>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程造价" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                    <%# Eval("PrjCost") %></ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="工程工期" DataField="Duration" /><asp:TemplateField HeaderText="立项申请日期"><ItemTemplate>
                                                    <%# Common2.GetTime(Eval("InputDate")) %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
                                                    <%# (Eval("PrjState").ToString() == "18") ? Common2.GetState(Eval("GiveUpFlowState").ToString()) : Common2.GetState(Eval("PftFlowState").ToString()) %>
                                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 5px">
                                <asp:Label ID="lblTotal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                                </webdiyer:AspNetPager>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="divFramPerson" title="选择人员" style="display: none">
        <iframe id="IframePerson" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
   
   
     
    

    
    <asp:HiddenField ID="hfldPrjId" runat="server" />
    
    <asp:HiddenField ID="hfldPrjState" runat="server" />
    <div id="div1" title="选择人员" style="display: none">
        <iframe id="Iframe1" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <asp:HiddenField ID="hdReturnVal" runat="server" />
    <asp:HiddenField ID="hdflOldPrjState" runat="server" />
    <asp:HiddenField ID="hfldUserCode" runat="server" />
    <link rel="Stylesheet" type="text/css" href="../Script/jquery.easyui/themes/default/easyui.css" />
    <link rel="Stylesheet" type="text/css" href="../Script/jquery.easyui/themes/icon.css" />
    </form>
</body>
</html>
