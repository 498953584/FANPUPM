<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="PrequalificationManage.aspx.cs" Inherits="TenderManage_PrequalificationManage" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>预审管理</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
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
    <script type="text/javascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../Script/jw.js"></script>
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
            $('#txtQualificationReadOne').attr('readOnly', 'readOnly');
            $('#txtAgent').attr('readOnly', 'readOnly');

            //行单击事件
            jwTable.registClickTrListener(function () {
                var prjState = $(this).attr('state');
                if (prjState == jw.ProjectParameter.Initiate) {
                    $('#btnStart').removeAttr('disabled');
                } else {
                    $('#btnStart').attr('disabled','disabled');
                }
                $('#hfldPrjId').val($(this).attr('id'));
                $('#hfldOldState').val(this.getAttribute('OldState'));
            });

            btnStartClick();
            //放弃事件
           // btnGiveUpClick();
            registerUpdateTenderEvent();
        });

        //点击放弃按钮事件

        function btnGiveUpClick() {
            $('#btnGiveUp').bind('click', function () {
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
                $('#div4').dialog({
                    open: function () {
                        $(this).parent().appendTo("form:first");
                    },
                    close: function () {
                        $('#txtGiveUpTime').val('');
                        $('#hfldOperator').val('');
                        $('#txtOperator').val('');
                        $('#txtGiveUPReason').val('');
                        $('#txtNote').val('');
                    },
                    width: 600,
                    height: 310,
                    modal: true
                });
            });
        }

        function registerUpdateTenderEvent() {
            var btnUpdate = document.getElementById('btnEdit');
            var hfldPrjId = document.getElementById('hfldPrjId');
            addEvent(btnUpdate, 'click', function () {
                parent.desktop.InfoAdd = window;
                var url = "/TenderManage/InfoAdd.aspx?Action=Update&purl=PrequalificationManage.aspx&PrjId=" + hfldPrjId.value;
                toolbox_oncommand(url, "编辑项目信息");
            });
        }
        function btnStartClick() {
            var btnStart = document.getElementById('btnStart');
            addEvent(btnStart, 'click', function () {
                $('#div3').append($('#btnSaveData'));
                $.ajax({
                    type: "GET",
                    async: false,
                    url: '/TenderManage/Handler/GetTenderInfo.ashx?' + new Date().getTime() + '&guid=' + $('#hfldPrjId').val() + '&part=14&userCode=' + $('#hfldUserCode').val(),
                    success: function (datas) {
                        var data = datas[0];
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
                });

                $('#div3').dialog({
                    open: function () {
                        $(this).parent().appendTo("form:first");
                        setFilelUpPath();
                    },
                    width: 600,
                    height: 310,
                    modal: true,
                    buttons: {
                        "保存": function () {
                            saveData();
                        },
                        "取消": function () {
                            $(this).dialog("close");
                        }
                    }
                });
            });
        }
        //保存数据
        function saveData() {
            if ($('#txtApprovalDate').val() == '') {
                //$.messager.alert('系统提示', '预审日期必须输入'); 和jQuery UI冲突，IE9下变形
                alert('系统提示:\n\n预审日期必须输入');
                $('#txtApprovalDate').focus();
                return;
            }
            $('#btnSaveData').click();

        }
        //设置上传附件路径
        function setFilelUpPath() {
            var recordCode = $('#hfldPrjId').val() + '_14';
            updateRecordCode('FileAudit', recordCode);
        }


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

        function divClose() {
            $('#btnCancel').click(function () {
                $('#div4').dialog('close');
            });
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
                                <input type="button" id="btnEdit" value="编辑" disabled="disabled" style="display:none;" />
                                <input type="button" id="btnStart" disabled="disabled" value="资格预审" style="width: 80px" />
                                <input type="button" id="btnGiveUp" disabled="disabled" value="放弃" style="width:50px; display:none;" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <asp:GridView ID="gvDataInfo" CssClass="gvdata" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="gvDataInfo_RowDataBound" DataKeyNames="PrjGuid,PrjState,OldState,PftFlowState" runat="server">
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
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="建设单位"><ItemTemplate>
                                                    <span class="tooltip" title="<%# Eval("WorkUnitName") %>">
                                                        <%# StringUtility.GetStr(Eval("WorkUnitName").ToString(), 25) %>
                                                    </span>
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程造价" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                                    <%# Eval("PrjCost") %></ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="工程工期" DataField="Duration" /><asp:TemplateField HeaderText="立项申请日期"><ItemTemplate>
                                                    <%# Common2.GetTime(Eval("InputDate")) %>
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
    <div id="div3" title="项目资格预审基本资料" style="display: none" runat="server">
        <table id="table1" class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    预审日期
                </td>
                <td class="txt mustInput ">
                    <asp:TextBox ID="txtApprovalDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    预审保证金(元)
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtQualificationMargin" CssClass="decimal_input" onkeyup="this.value=this.value.replace(/[^\d]/g,'') " onafterpaste="this.value=this.value.replace(/[^\d]/g,'') " runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    投标日期
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtTenderDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    登记期限(天)
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtRegistDeadline" onkeyup="this.value=this.value.replace(/[^\d]/g,'') ; var txtValue = this.value; if (txtValue.length > 10) {this.value = txtValue.substring(0, 10);$.messager.alert('系统提示', '输入的字数不能大于10个字');}" onafterpaste="this.value=this.value.replace(/[^\d]/g,'') " runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    经办人
                </td>
                <td class="txt">
                    <span class="spanSelect" style="width: 100%;">
                        <asp:TextBox ID="txtAgent" Style="width: 80%; height: 15px; border: none;
                            line-height: 16px; margin: 1px 2px" runat="server"></asp:TextBox>
                        <img src="../../images/icon.bmp" style="float: right;" alt="选择" id="imgAgent" onclick="selectUser('hfldAgent','txtAgent');" runat="server" />

                    </span>
                    <input id="hfldAgent" type="hidden" style="width: 1px" runat="server" />

                </td>
                <td class="word">
                    报名日期
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtApplyDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    阅知人
                </td>
                <td class="txt ">
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtQualificationReadOne" Style="width: 80%; height: 15px; border: none;
                            line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择阅知人" onclick="selectUser('hfldQualificationReadOne', 'txtQualificationReadOne');" src="../../images/icon.bmp" style="float: right;" id="img3" runat="server" />

                    </span>
                    <asp:HiddenField ID="hfldQualificationReadOne" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    资格预审要求
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtPrequalificationRequire" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    附件
                </td>
                <td colspan="3" id="file_ys" style="white-space: nowrap" runat="server">
                    <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileAudit" Name="附件" Class="ProjectFile" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div id="div4" title="项目放弃基本信息" style="display: none" runat="server">
        <table id="table2" class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    放弃时间
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtGiveUpTime" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    操作人
                </td>
                <td class="txt">
                    <span class="spanSelect" style="width: 99%">
                        <asp:TextBox ID="txtOperator" Style="width: 80%; height: 15px; border: none;
                            line-height: 16px; margin: 1px 2px" runat="server"></asp:TextBox>
                        <img src="../../images/icon.bmp" style="float: right;" alt="选择" id="img1" onclick="selectUser('hfldOperator','txtOperator');" runat="server" />

                    </span>
                    <input type="hidden" id="hfldOperator" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="word">
                    放弃原因
                </td>
                <td class="txt" colspan="3">
                    <asp:TextBox ID="txtGiveUPReason" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    备注
                </td>
                <td class="txt" colspan="3">
                    <asp:TextBox ID="txtNote" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div style="position: absolute; right: 10px; bottom: 10px;">
            <asp:Button Text="保存" ID="btnSave" OnClick="btnSave_Click" runat="server" />
            <input type="button" id="btnCancel" value="取消" onclick="divClose()" />
        </div>
    </div>
    
    <asp:Button ID="btnSaveData" Text="保存" Style="display: none;" OnClick="btnSaveData_Click" runat="server" />
    
    <asp:HiddenField ID="hfldPrjId" runat="server" />
    
    <asp:HiddenField ID="hfldOldState" runat="server" />
    <div id="div1" title="选择人员" style="display: none">
        <iframe id="Iframe1" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <asp:HiddenField ID="hdReturnVal" runat="server" />
    <asp:HiddenField ID="hfldUserCode" runat="server" />
    <link rel="Stylesheet" type="text/css" href="../Script/jquery.easyui/themes/default/easyui.css" />
    <link rel="Stylesheet" type="text/css" href="../Script/jquery.easyui/themes/icon.css" />
    </form>
</body>
</html>
