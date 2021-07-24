<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="AllbidManage.aspx.cs" Inherits="TenderManage_AllbidManage" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>中落标管理 </title><link type="text/css" rel="stylesheet" href="/Script/themes/base/jquery.ui.all.css" />
<link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="../Script/jquery.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/ecmascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../Script/jw.js"></script>
    <script type="text/javascript" src="../Script/wf.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // 添加验证
            $('#brnQuery')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
            replaceEmptyTable('emptyProject', 'gvwProject');
            var table = new JustWinTable('gvwProject');
            showTooltip('tooltip', 25);
            //  setButton2(table, 'hfldCheckedIds');
            setWfButtonState2(table, 'WF1');
            //giveUpClick();
            bindClick();

            $('#gvwProject tr').live('click', function () {
                if ($(this).attr('state') == '4') {
                    $('#btnStartWF').attr('disabled', 'disabled');
                }
            });

            //单击行事件
            table.registClickTrListener(function () {
                var $btnEdit = $('#btnEdit');   // 编辑
                var $btnBit = $('#btnGet');     // 中标
                var $btnLost = $('#btnLost');   // 落标
                //获取流程状态
                var bitFlowState = $(this).attr('bidFlowState');
                var oldState = $(this).attr('OldState');
                var prjState = $(this).attr('state');
                $('#hfldCheckedIds').val($(this).attr('id'));
                $('#hfldOldState').val($(this).attr('OldState'));
                // 项目状态为投标，中标和落标按钮不能用
                if (prjState == jw.ProjectParameter.Bid) {
                    $btnBit.removeAttr('disabled');
                    $btnLost.removeAttr('disabled');
                    $btnEdit.removeAttr('disabled');
                } else {
                    // 项目状态为中标或落标
                    if (bitFlowState == "-1"||bitFlowState == '-3') {
                        // 流程状态为未提交，中标和落标状态可以互换
                        $btnBit.removeAttr('disabled');
                        $btnLost.removeAttr('disabled');
                    } else {
                        $btnBit.attr('disabled','disabled');
                        $btnLost.attr('disabled','disabled');
                    }
                }
            });
        });

        // 建设单位(甲方)名称
        function pickCorp(param) {
            jw.selectOneCorp({ nameinput: 'txtName', idinput: 'hfldOwner' });
        }

        // 选择人员
        function selectUser(id, name) {
            jw.selectOneUser({ nameinput: name, codeinput: id });
        }
        // 控制按钮投标
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
                    setAllBttons('');
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
                arguments.callee('disabled');
                $(":button[name='state']").each(function (i) {
                    var selRow = $('#' + $('#hfldCheckedIds').val()).get(0);
                    var prjState = parseInt(selRow.getAttribute('state'));
                    var prjInfoState = selRow.prjInfoState;
                    if ((prjState == '4' && (this.getAttribute('state') == '5' ||
                        this.getAttribute('state') == '6' || this.getAttribute('state') == '0')) ||
                        (prjState == '5' && this.getAttribute('state') == '7' && prjInfoState == '') ||
                        (prjState == '6' && this.getAttribute('state') == '0')) {
                        $(this).removeAttr('disabled');
                    }
                });
            }
        }

//        //单击放弃按钮事件
//        function giveUpClick() {
//            $('#btnGiveUp').bind('click', function () {
//                var url = '/TenderManage/Handler/GetPrjInfoZTB.ashx?' + new Date().getTime() + '&prjId=' + $('#hfldCheckedIds').val() + '&state=' + $('#hfldOldState').val();
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
//                    }
//                });
//                $('#div4').dialog({
//                    open: function () {
//                        $(this).parent().appendTo("form:first");
//                    },
//                    close: function () {
//                        //$('#txtGiveUpTime').val('');
//                        $('#hfldOperator').val('');
//                        $('#txtOperator').val('');
//                        $('#txtGiveUPReason').val('');
//                        $('#txtNote').val('');
//                    },
//                    width: 610,
//                    height: 300,
//                    modal: true
//                });
//            });
//        }

        //状态变更标签页
        function openEditTabPage(id) {
            parent.desktop.InfoAdd = window;
            var url = "/TenderManage/InfoAdd.aspx?Action=Update&purl=AllbidManage.aspx&PrjId=" + id;
            toolbox_oncommand(url, "编辑项目信息");
        }
        //绑定按钮点击事件
        function bindClick() {
            $(":button[name='state']").each(function (i) {
                $(this).bind('click', function () {

                    //选中项目的状态
                    var state = parseInt($('#' + $('#hfldCheckedIds').val()).attr('state'));
                    //按钮所代表的项目状态
                    var btnState = parseInt(this.getAttribute('state'));
                    if (btnState == '0') {
                        openEditTabPage($('#hfldCheckedIds').val());
                        return;
                    }
                    //保存状态值
                    $('#hfldClinkButton').val(btnState);
                    //弹出层ID
                    var divId = 'div' + this.id;
                    $('#hfldDivId').val(divId);
                    //移到Dialog中，否则按钮事件无效
                    $('#' + divId).append($('#btnSaveData'));
                    //弹出信息进行保存
                    if (btnState == '5') {
                        var url = '/TenderManage/BidSuccessInfo.aspx?'+new Date().getTime()+'&PrjId=' + $('#hfldCheckedIds').val();
                        top.ui.openWin({ title: '中标信息管理', url: url,width:650, height: 500  });
                    } else if (btnState == '6') {
                        var url = '/TenderManage/BidFailInfo.aspx?'+new Date().getTime()+'&PrjId=' + $('#hfldCheckedIds').val();
                        top.ui.openWin({ title: '落标信息管理', url: url,width:650, height: 500 });
                    }
                    //                        if (btnState != '7') {
                    //                            $.ajax({
                    //                                type: "GET",
                    //                                async: false,
                    //                                url: '/TenderManage/Handler/GetTenderInfo.ashx?' + new Date().getTime() + '&guid=' + $('#hfldCheckedIds').val() + '&part=' + btnState + '&userCode=' + $('#hfldUserCode').val(),
                    //                                success: function (datas) {
                    //                                    var data = datas[0];
                    //                                    if (btnState == 5) {
                    //                                        $('#txtSuccessBidDate').val(data.SuccessBidDate);
                    //                                        var price = data.SuccessBidPrice;
                    //                                        if (price != null)
                    //                                            price = price.toFixed(3);
                    //                                        $('#txtSuccessBidPrice').val(price);
                    //                                        $('#txtSuccessBidRemark').val(data.SuccessBidRemark);
                    //                                    }
                    //                                    else {
                    //                                        $('#txtOutBidDate').val(data.OutBidDate);
                    //                                        if (data.OutBidIsReturn != null) {
                    //                                            var index = data.OutBidIsReturn ? "1" : "0";
                    //                                            $('#RblOutBidIsReturn_' + index).get(0).checked = true;
                    //                                        }
                    //                                        $('#txtOutBidRemark').val(data.OutBidRemark);
                    //                                    }
                    //                                }
                    //                            });
                    //                        $('#' + divId).dialog({

                    //                            open: function () {
                    //                                $(this).parent().appendTo("form:first");
                    //                                if (btnState == '5' || btnState == '6')
                    //                                    setFilelUpPath(btnState);
                    //                                setDate();
                    //                            },
                    //                            width: 610,
                    //                            height: 300,
                    //                            modal: true,
                    //                            title: this.value,
                    //                            buttons: {
                    //                                "保存": function () {
                    //                                    saveData(divId, btnState);
                    //                                },
                    //                                "取消": function () {
                    //                                    $(this).dialog("close");
                    //                                }
                    //                            }
                    //                        });
                    //                    }
                });
            });
        }
        //返回项目开始日期
        function setDate() {
            $.ajax({
                async: false,
                type: "GET",
                dataType: "text",
                url: "/TenderManage/Handler/GetPrjStartDate.ashx?" + new Date().getTime() + "&prjId=" + $('#hfldCheckedIds').val(),
                success: function (data) {
                    $('#txtBuildStartDate').val(data);
                }
            });

        }
        //设置上传附件路径
        function setFilelUpPath(btnState) {
            var recordCode = $('#hfldCheckedIds').val() + '_' + btnState;
            updateRecordCode('FileUpload_' + btnState, recordCode);
        }
        //保存数据
        function saveData(divId, btnState) {
            if (btnState == 3 && $('#txtStartDate').val() == '') {
                //alert('启动日期必须输入');
                $.messager.alert('系统提示', '启动日期必须输入');
                $('#txtStartDate').focus();
                return;
            }
            else if (btnState == 4 && $('#txtTenderBeginDate').val() == '') {
                //alert('开标日期必须输入');
                $.messager.alert('系统提示', '开标日期必须输入');
                $('#txtTenderBeginDate').focus();
                return;
            }
            $('#btnSaveData').click();
        }

        function divClose() {
            $('#btnCancel').click(function () {
                $('#div4').dialog('close');
            });
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
                                项目经理
                            </td>
                            <td>
                                <span class="spanSelect" style="width: 122px;">
                                    <asp:TextBox ID="txtTenderPrjManager" Style="width: 97px; height: 15px;
                                        border: none; line-height: 16px; margin: 1px 0px 1px 2px;" CssClass="easyui-validatebox" data-options="validType:'validChars[50]'" runat="server"></asp:TextBox>
                                    <img src="../../images/icon.bmp" style="float: right; height: 18px;" alt="选择" id="img2"
                                        onclick="selectUser('hfldTenderPrjManager','txtTenderPrjManager');" />
                                </span>
                                
                                <input id="hfldTenderPrjManager" type="hidden" style="width: 1px" runat="server" />

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
                                    <asp:TextBox ID="txtName" Style="width: 97px; height: 15px; border: none; line-height: 16px;
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
                <td class="divFooter" style="text-align: left;">
                    <input type="button" name="state" value="编辑" id="btnEdit" disabled="disabled" style="width: auto; display:none;"
                        state="0" />
                    <input type="button" name="state" value="中标" id="btnGet" disabled="disabled" style="width: auto;"
                        state="5" />
                    <input type="button" name="state" value="落标" id="btnLost" disabled="disabled" style="width: auto;"
                        state="6" />
                    
                    <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="137" BusiClass="001" runat="server" />
                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvwProject" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="gvwProject_RowDataBound" DataKeyNames="PrjGuid,PrjState,PrjInfoState,OldState,BidFlowState" runat="server">
<EmptyDataTemplate>
                            <table class="gvdata" cellspacing="0" id="emptyProject" rules="all" border="1" style="width: 100%;
                                border-collapse: collapse;">
                                <tr>
                                    <th class="header">
                                        序号
                                    </th>
                                    <th class="header">
                                        项目状态
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
                                        立项申请日期
                                    </th>
                                   
                                    <th class="header">
                                        流程状态
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                    <%# Eval("No") %>
                                </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="项目状态" DataField="StateText" /><asp:TemplateField HeaderText="项目经理"><ItemTemplate>
                                    <span class="tooltip" title='<%# Eval("Person").ToString() %>'>
                                        <%# StringUtility.GetStr(Eval("Person").ToString(), 25) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:BoundField DataField="PrjCode" HeaderText="项目编号" /><asp:TemplateField HeaderText="项目名称" HeaderStyle-Width="150px"><ItemTemplate>
                                    <asp:HyperLink ID="hlinkShowName" CssClass="tooltip" Style="text-decoration: none;
                                        color: Black;" ToolTip='<%# System.Convert.ToString(Eval("PrjName").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server">
                        <span class="link" onclick="viewopen('/TenderManage/InfoView.aspx?&&ic=<%# Eval("PrjGuid") %>', '项目信息查看')">
                        <%# StringUtility.GetStr(Eval("PrjName").ToString(), 25) %>
                       </span></asp:HyperLink>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="建设单位"><ItemTemplate>
                                    <span class="tooltip" title='<%# Eval("WorkUnitName").ToString() %>'>
                                        <%# StringUtility.GetStr(Eval("WorkUnitName").ToString(), 25) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程造价" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                    <%# Eval("PrjCost") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="工程工期" DataField="Duration" /><asp:TemplateField HeaderText="立项申请日期"><ItemTemplate>
                                    <%# Common2.GetTime(Eval("InputDate")) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
                                    <%# Common2.GetState(Eval("BidFlowState").ToString()) %>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
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
    </div>
    
 
    
  
   
    
    <div id="divFramPerson" title="选择人员" style="display: none">
        <iframe id="IframePerson" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    
    <asp:HiddenField ID="hfldCheckedIds" runat="server" />
    <asp:HiddenField ID="hfldOldState" runat="server" />

    <asp:HiddenField ID="hfldClinkButton" runat="server" />
    <asp:HiddenField ID="hfldDivId" runat="server" />

    </form>
</body>
</html>
