<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="BidManage.aspx.cs" Inherits="TenderManage_BidManage" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>投标管理 </title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js" charset="gb2312"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
    <link type="text/css" rel="stylesheet" href="/Script/themes/base/jquery.ui.all.css" />

    <script type="text/ecmascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../Script/jw.js"></script>
    <script type="text/javascript" src="/Script/wf.js"></script>
    <script type="text/javascript" src="/Script/DecimalInput.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // 添加验证
            $('#brnQuery')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
            replaceEmptyTable('emptyProject', 'gvwProject');
            var table = new JustWinTable('gvwProject');
            showTooltip('tooltip', 25);
            setButton2(table, 'hfldCheckedIds');
            setWfButtonState2(table, 'WF1');
            btnGiveUpClick();
            Val.validate('form1', 'btnSave');

            $('#gvwProject tr').live('click', function () {
                var prjState = $(this).attr('state');
                var flowState = $(this).attr('flowState');
                if (prjState == jw.ProjectParameter.Bid || prjState == jw.ProjectParameter.QualificationPass) {
                    $('#btnStartWF').attr('disabled', 'disabled');
                } else if (prjState == jw.ProjectParameter.GiveUpState) {
                    if (flowState == '-1' || flowState == '-3') {
                        $('#btnStartWF').removeAttr('disabled');
                    } else {
                        $('#btnStartWF').attr('disabled', 'disabled');
                        $('#btnGiveUp').attr('disabled', 'disabled');
                    }
                }
            });

            table.registClickTrListener(function () {
                $('#hfldPrjId').val($(this).attr('id'));
                var prjState = $(this).attr('state');
                if (prjState == jw.ProjectParameter.QualificationPass) {
                    $('#btnTender').removeAttr('disabled');
                    $('#btnGiveUp').removeAttr('disabled');
                } else if (prjState == jw.ProjectParameter.GiveUpState) {
                    $('#btnTender').attr('disabled', 'disabled');
                    $('#btnGiveUp').removeAttr('disabled');
                } else {
                    $('#btnTender').attr('disabled', 'disabled');
                    $('#btnGiveUp').attr('disabled', 'disabled');
                }
            });
        });

        // 放弃
        //点击放弃按钮事件
        function btnGiveUpClick() {
            $('#btnGiveUp').bind('click', function () {
                top.ui._GiveUpInfo=window;
                var url = '/TenderManage/GiveUpInfo.aspx?prjId=' + $('#hfldPrjId').val();
                top.ui.openWin({ title: '放弃管理', url: url });
//                var url = '/TenderManage/Handler/GetPrjInfoZTB.ashx?' + new Date().getTime() + '&prjId=' + $('#hfldPrjId').val();
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
//                $('#div2').dialog({
//                    open: function () {
//                        $(this).parent().appendTo("form:first");
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
//            updateRecordCode('FileGiveUp', recordCode);
//        }
        //建设单位(甲方)名称
        function pickCorp(param) {
            jw.selectOneCorp({ nameinput: 'txtName', idinput: 'hfldOwner' });
        }

        //选择人员
        function selectUser(id, name) {
            jw.selectOneUser({ nameinput: name, codeinput: id });
        }
        //控制按钮投标
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
            jwTable.registClickSingleCHKListener(function () {
                var checkedChk = jwTable.getCheckedChk();
                if (checkedChk.length == 0 || checkedChk.length > 1) {
                    if (hdChkId != '') {
                        document.getElementById(hdChkId).value = "";
                    }
                    setAllBttons('disalbed');
                }
                else {
                    var checkedChks = jwTable.getCheckedChk();
                    if (hdChkId != '') {
                        var taskId = jwTable.getCheckedChkIdJson(checkedChk);
                        document.getElementById(hdChkId).value = taskId;
                    }
                    if (checkedChks.length == 1)
                        setAllBttons('');
                    else
                        setAllBttons('disalbed');
                }
            });
            jwTable.registClickAllCHKLitener(function () {
                if (this.checked) {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson();
                    var checkedChks = jwTable.getAllChk();
                    if (checkedChks.length == 1) {
                        setAllBttons('');
                    }
                    else {
                        setAllBttons('disalbed');
                    }
                }
                else {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = '';
                    setAllBttons('disalbed');
                }
            });
        }
        //禁用/释放
        function setAllBttons(disabled) {
            if (disabled != undefined && disabled != '') {
                $('#btnTender').attr('disabled', 'disabled');
                $('#btnEdit').attr('disabled', 'disabled');
            }
            else {
                var state = $('#' + $('#hfldCheckedIds').val()).attr('state');
                if (state == jw.ProjectParameter.QualificationPass) {
                    $('#btnTender').removeAttr('disabled');
                }
                else
                    arguments.callee('disabled');
                $('#btnEdit').removeAttr('disabled');
            }

        }
        //投标标签页
        function openTabPage() {
            parent.desktop.BidSet = window;
            var id = document.getElementById('hfldCheckedIds').value;
            var url = "/TenderManage/BidSet.aspx?purl=BidManage.aspx&id=" + id;
            toolbox_oncommand(url, "投标基本资料");
        }
        //状态变更标签页
        function openEditTabPage(id) {
            parent.desktop.InfoAdd = window;
            var url = "/TenderManage/InfoAdd.aspx?Action=Update&purl=BidManage.aspx&PrjId=" + id;
            toolbox_oncommand(url, "编辑项目信息");
        }
        function openEdit() {
            openEditTabPage($('#hfldCheckedIds').val());
        }

        function divClose() {
            $('#btnCancel').click(function () {
                $('#div2').dialog('close');
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
                    <input type="button" id="btnEdit" value="编辑" disabled="disabled" onclick="openEdit()"  style="display:none;"/>
                    <input type="button" id="btnTender" value="投标" disabled="disabled" onclick="openTabPage()" />
                    <input type="button" id="btnGiveUp" value="放弃" disabled="disabled" />
                    <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="138" BusiClass="001" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvwProject" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="gvwProject_RowDataBound" DataKeyNames="PrjGuid,PrjState,GiveUpFlowState" runat="server">
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
                                       流程状态
                                    </th>
                                    <th class="header">
                                        立项申请日期
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="20px">
<ItemTemplate>
                                    <%# Eval("No") %>
                                </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="项目状态" DataField="StateText" /><asp:BoundField HeaderText="项目经理" DataField="Person" /><asp:BoundField DataField="PrjCode" HeaderText="项目编号" /><asp:TemplateField HeaderText="项目名称" HeaderStyle-Width="150px"><ItemTemplate>
                                    <asp:HyperLink ID="hlinkShowName" CssClass="tooltip" Style="text-decoration: none;
                                        color: Black;" ToolTip='<%# System.Convert.ToString(Eval("PrjName").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server">
                        <span class="link"  onclick="viewopen('/TenderManage/InfoView.aspx?&&ic=<%# Eval("PrjGuid") %>', '项目信息查看')">
                        <%# StringUtility.GetStr(Eval("PrjName").ToString(), 25) %>
                       </span></asp:HyperLink>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="建设单位"><ItemTemplate>
                                    <span class="tooltip" title='<%# Eval("WorkUnitName").ToString() %>'>
                                        <%# StringUtility.GetStr(Eval("WorkUnitName").ToString(), 25) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程造价" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                    <%# Eval("PrjCost") %></ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="工程工期" DataField="Duration" /><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
                                    <%# Common2.GetState(Eval("GiveUpFlowState").ToString()) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="立项申请日期"><ItemTemplate>
                                    <%# Common2.GetTime(Eval("InputDate")) %>
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
    <asp:HiddenField ID="hfldPrjId" runat="server" />
    </form>
</body>
</html>
