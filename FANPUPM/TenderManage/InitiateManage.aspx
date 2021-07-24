<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="InitiateManage.aspx.cs" Inherits="TenderManage_InitiateManage" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>启动管理</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link Href="/Script/jquery.treeview/jquery.treeview.css" />

    <script type="text/javascript" src="../Script/jquery.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/ecmascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="/Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../Script/jw.js"></script>
    <script type="text/javascript" src="../Script/jwJson.js"></script>
    <script type="text/javascript" src="../Script/wf.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('gvDataInfo');
            // 添加验证
            $('#brnQuery')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
            replaceEmptyTable('emptyTable', 'gvDataInfo');
            setWfButtonState2(jwTable, 'WF1');
            showTooltip('tooltip', 25);
            $('#txtPrjDutyPersonName').attr('readOnly', 'readOnly');

            //单击行事件
            jwTable.registClickTrListener(function () {
                //项目状态
                var prjState = $(this).attr('state');
                $('#hfldSelectedPrjState').val(prjState);
                var FlowState = $(this).attr('FlowState');
                if (prjState == '18') {
                    $('#WF1_BusinessCode').attr('value', '138');
                } else {
                    $('#WF1_BusinessCode').attr('value', '135');
                }
                if (FlowState == '-1' || FlowState == '-3') {
                    $('#btnPass').removeAttr('disabled');
                    $('#btnNoPass').removeAttr('disabled');
                    $('#btnGiveUp').removeAttr('disabled');
                    if (prjState = jw.ProjectParameter.Approval) {
                        $('#btnStartWF').attr('disabled', 'disabled');
                    } else {
                        $('#btnStartWF').attr('disabled', 'disabled');
                    }
                } else {
                    $('#btnPass').attr('disabled', 'disabled');
                    $('#btnNoPass').attr('disabled', 'disabled');
                    $('#btnGiveUp').attr('disabled', 'disabled');
                }

                $('#hfldPrjId').val(this.getAttribute('id'));
                $('#hfldOldState').val(this.getAttribute('OldState'));
            });
            // 处理项目状态为信息立项提交审核按钮不能用
            $('#gvDataInfo tr').live('click', function () {
                if ($(this).attr('state') == '2') {
                    $('#btnStartWF').attr('disabled', 'disabled');
                } else {
                    var flowState = $(this).attr('FlowState');
                    if (flowState == '-1' || flowState == '-3') {
                        $('#btnStartWF').removeAttr('disabled');
                    } else {
                        $('#btnStartWF').attr('disabled', 'disabled');
                    }
                }
                var prjState = $(this).attr('state');
                if (prjState == jw.ProjectParameter.Approval) {
                    $('#btnEdit').removeAttr('disabled');
                } else {
                    $('#btnEdit').attr('disabled', 'disabled');
                }
                //                if ($('#hfldUserCode').val() == '00000000') {
                //                    $('#btnEdit').removeAttr('disabled');
                //                }
            });
            //单击放弃事件
            btnGiveUpClick();
            btnStartClick();
            registerUpdateTenderEvent();
        });

        function registerUpdateTenderEvent() {
            var btnUpdate = document.getElementById('btnEdit');
            var hfldPrjId = document.getElementById('hfldPrjId');
            addEvent(btnUpdate, 'click', function () {
                parent.desktop.InfoAdd = window;
                var url = "/TenderManage/InfoAdd.aspx?Action=Update&purl=InitiateManage.aspx&PrjId=" + hfldPrjId.value;
                toolbox_oncommand(url, "编辑项目信息");
            });
        }

        //放弃按钮事件
        function btnGiveUpClick() {
            $('#btnGiveUp').bind("click", function () {
                $('#hfldPrjState').val(jw.ProjectParameter.GiveUpState);
                top.ui._GiveUpInfo = window;
                var url = '/TenderManage/GiveUpInfo.aspx?' + new Date().getTime() + '&prjId=' + $('#hfldPrjId').val();
                top.ui.openWin({ title: '放弃管理', url: url });
                //                EditAjax();
                //                $('#div4').dialog({
                //                    open: function () {
                //                        $(this).parent().appendTo("form:first");
                //                        setFilelUpPathGiveUp();
                //                    },
                //                    width: 600,
                //                    height: 310,
                //                    modal: true
                //                });
            });
        }

        function EditAjax() {
            var url = '/TenderManage/Handler/GetPrjInfoZTB.ashx?' + new Date().getTime() + '&prjId=' + $('#hfldPrjId').val() + '&state=' + $('#hfldOldState').val();
            $.ajax({
                type: "GET",
                dataType: "json",
                async: false,
                url: url,
                success: function (data) {
                    if (data != null) {
                        $('#txtGiveUpTime').val(data.GiveUpTime);
                        $('#hfldOperator').val(data.Operator);
                        $('#txtOperator').val(data.OperatorName);
                        $('#txtGiveUPReason').val(data.GiveUpReason);
                        $('#txtNote').val(data.GiveUpNote);
                    }
                    else {
                        var dateTime = window["jw"].dateToStr(new Date(), "-");
                        $('#txtGiveUpTime').val(dateTime);
                    }
                },
                error: function (errorMsg) {
                    alert(errorMsg);
                }
            });
        }

        //点击报名事件
        function btnStartClick() {
            // 报名通过
            $('#btnPass').bind('click', function () {
                $('#hfldPrjState').val(jw.ProjectParameter.Initiate);
                top.ui._initiatePass = window;
                var url = '/TenderManage/initiatePass.aspx?prjId=' + $('#hfldPrjId').val();
                top.ui.openWin({ title: '报名通过基本信息', url: url, width: 600, height: 400 });
                //                $('#div3').append($('#btnSaveData'));
                //                $.ajax({
                //                    type: "GET",
                //                    async: false,
                //                    url: '/TenderManage/Handler/GetTenderInfo.ashx?' + new Date().getTime() + '&guid=' + $('#hfldPrjId').val() + '&part=3&userCode=' + $('#hfldUserCode').val(),
                //                    success: function (datas) {
                //                        if ($('#hfldSelectedPrjState').val() == $('#hfldPrjState').val() && $('#hfldPrjState').val() != jw.ProjectParameter.Approval) {
                //                            var data = datas[0];
                //                            $('#txtApplyDate').val(data.ProjApplyDate);
                //                            $('#txtProjStartDate').val(data.ProjStartDate);
                //                            $('#txtStartRemark').val(data.ProjStartRemark);
                //                        } else {
                //                            $('#txtApplyDate').val('');
                //                            $('#txtProjStartDate').val('');
                //                            $('#txtStartRemark').val('');
                //                        }
                //                    }
                //                });
                //                $('#div3').dialog({
                //                    open: function () {
                //                        $(this).parent().appendTo("form:first");
                //                        setFilelUpPath();
                //                    },
                //                    width: 600,
                //                    height: 310,
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
            // 报名不通过资料
            $('#btnNoPass').bind('click', function () {
                // 报名不通过
                top.ui._InitiateFail = window;
                $('#hfldPrjState').val(jw.ProjectParameter.InitiateFail);
                var url = '/TenderManage/InitiateFail.aspx?prjId=' + $('#hfldPrjId').val();
                top.ui.openWin({ title: '报名不通过基本信息', url: url, width: 600, height: 400 });
                //                $('#div5').append($('#btnSaveData'));
                //                $.ajax({
                //                    type: "GET",
                //                    async: false,
                //                    url: '/TenderManage/Handler/GetTenderInfo.ashx?' + new Date().getTime() + '&guid=' + $('#hfldPrjId').val() + '&part=19&userCode=' + $('#hfldUserCode').val(),
                //                    success: function (datas) {
                //                        if ($('#hfldSelectedPrjState').val() == $('#hfldPrjState').val() && $('#hfldPrjState').val() != jw.ProjectParameter.Approval) {
                //                            var data = datas[0];
                //                            $('#txtApplyDate1').val(data.ProjApplyDate);
                //                            $('#txtProjStartDate1').val(data.ProjStartDate);
                //                            $('#txtStartRemark1').val(data.ProjStartRemark);
                //                        } else {
                //                            $('#txtApplyDate1').val('');
                //                            $('#txtProjStartDate1').val('');
                //                            $('#txtStartRemark1').val('');
                //                        }
                //                    }
                //                });

                //                $('#div5').dialog({
                //                    open: function () {
                //                        $(this).parent().appendTo("form:first");
                //                        setFilelUpPathFail();
                //                    },
                //                    width: 600,
                //                    height: 310,
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

        //设置上传附件路径
        function setFilelUpPath() {
            var recordCode = $('#hfldPrjId').val() + '_3';
            updateRecordCode('FileStart', recordCode);
        }
        //设置上传附件路径
        function setFilelUpPathFail() {
            var recordCode = $('#hfldPrjId').val() + '_19';
            updateRecordCode('FileStartFail', recordCode);
        }

        //选择人员
        function selectUser(id, name) {
            jw.selectOneUser({ nameinput: name, codeinput: id });
        }
        //甲方名称
        function pickCorp(param) {
            jw.selectOneCorp({ nameinput: 'txtOwner', idinput: 'hfldOwner' });
        }
        function Query(path) {
            parent.desktop.InfoAdd = window;
            toolbox_oncommand(path, "信息立项申请");
        }
        function divClose() {
            $('#btnCancel').click(function () {
                $('#div4').dialog('close');
            })
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
                                    <asp:TextBox ID="txtOwner" Style="width: 97px; height: 15px; border: none; line-height: 16px;
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
                                <input type="button" id="btnEdit" value="编辑" disabled="disabled" />
                                <input type="button" id="btnPass" value="报名通过" style="width: 70px;" disabled="disabled" />
                                <input type="button" id="btnNoPass" value="报名不通过" style="width: 80px;" disabled="disabled" />
                                <input type="button" id="btnGiveUp" value="放弃" disabled="disabled" />
                                <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="135" BusiClass="001" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <asp:GridView ID="gvDataInfo" CssClass="gvdata" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="gvDataInfo_RowDataBound" DataKeyNames="PrjGuid,PrjState,OldState,InitiateFlowState,GiveUpFlowState" runat="server">
<EmptyDataTemplate>
                                            <table id="emptyTable">
                                                <tr class="header">
                                                    <th scope="col" style="width: 20px;">
                                                    </th>
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
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="20px">
<ItemTemplate>
                                                    <%# (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="项目状态" DataField="StateText" /><asp:BoundField HeaderText="项目跟踪人" DataField="Person" /><asp:BoundField HeaderText="项目编号" DataField="PrjCode" /><asp:TemplateField HeaderText="项目名称" HeaderStyle-Width="150px"><ItemTemplate>
                                                    <asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" CssClass="tooltip" ToolTip='<%# System.Convert.ToString(Eval("PrjName").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server">
                        <span class="link" onclick="viewopen('/TenderManage/InfoView.aspx?ic=<%# Eval("PrjGuid") %>')">
                        <%# StringUtility.GetStr(Eval("PrjName").ToString(), 25) %>
                       </span></asp:HyperLink>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="建设单位" HeaderStyle-Width="150px"><ItemTemplate>
                                                    <span class="tooltip" title='<%# Eval("WorkUnitName").ToString() %>'>
                                                        <%# StringUtility.GetStr(Eval("WorkUnitName").ToString(), 25) %>
                                                    </span>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程造价" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                    <%# Eval("PrjCost") %></ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="工程工期" DataField="Duration" /><asp:TemplateField HeaderText="立项申请日期"><ItemTemplate>
                                                    <%# Common2.GetTime(Eval("InputDate")) %>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
                                                    <%# (Eval("PrjState").ToString() == "18") ? Common2.GetState(Eval("GiveUpFlowState").ToString()) : Common2.GetState(Eval("InitiateFlowState").ToString()) %>
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
    
    <asp:HiddenField ID="hfldOldState" runat="server" />
    <div id="div1" title="选择人员" style="display: none">
        <iframe id="Iframe1" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    
    <asp:HiddenField ID="hfldSelectedPrjState" runat="server" />
    <asp:HiddenField ID="hdReturnVal" runat="server" />
    <asp:HiddenField ID="hfldUserCode" runat="server" />
    
    <asp:HiddenField ID="hfldPrjState" runat="server" />
    
    </form>
</body>
</html>
