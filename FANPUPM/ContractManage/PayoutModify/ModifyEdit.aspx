<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModifyEdit.aspx.cs" Inherits="ContractManage_PayoutModify_ModifyEdit" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>合同变更</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnBindResource').hide();
            registerCancelEvent();
            if (getRequestParam('Action') == 'Query' || !getRequestParam('Action')) {
                setAllInputDisabled();
            }
            Val.validate('form1', 'btnSave');
            setAuditState();

            // 资源关联WBS，才可以进行资源配置， 暂时不做资源配置，先解决完客户主要需求，合同计量功能
            if ($('#hfldIsWBSRelevance').val() == '1') {
                $('#btnAllocRes').show();
            }

            // 预算变更明细表
            var bugTable = new JustWinTable('gvBudget');
            bugTable.registClickTrListener(function () {
                $('#hfldEditModifyTaskId').val(this.id);
                $('#btnEditBudModify').attr('disabled', false);
                $('#btnDelBudModify').attr('disabled', false);
                $('#btnAllocRes').attr('disabled', false);
            });
            setButton(bugTable, 'btnDelBudModify', 'btnEditBudModify', 'btnAddBudModify', 'hfldEditModifyTaskId');

            //从采购计划中增加采购单
            var jwTable = new JustWinTable('gvwPurchaseplanStock');
            jwTable.registClickTrListener(function () {
                document.getElementById('hfldPurchaseplanChecked').value = this.id;
                document.getElementById('hfldModifyStockId').value = this.modifyStockId;
                document.getElementById('hfldPurchaseIds').value = this.purchaseId;
                document.getElementById('btnDelete').removeAttribute('disabled');
            });

            jwTable.registClickSingleCHKListener(function () {
                var checkedChk = jwTable.getCheckedChk();
                var modifyStockIds;
                var purchaseIds;
                if (checkedChk.length == 0) {
                    document.getElementById('btnDelete').setAttribute('disabled', 'disabled');
                }
                else if (checkedChk.length == 1) {
                    document.getElementById('hfldPurchaseplanChecked').value = jwTable.getCheckedChkIdJson(checkedChk);
                    var tr = getFirstParentElement(checkedChk[0].parentNode, 'TR');
                    modifyStockIds = tr.modifyStockId;
                    purchaseIds = tr.purchaseId;
                    document.getElementById('btnDelete').removeAttribute('disabled');
                }
                else {
                    document.getElementById('hfldPurchaseplanChecked').value = jwTable.getCheckedChkIdJson(checkedChk);
                    document.getElementById('btnDelete').removeAttribute('disabled');
                    modifyStockIds = '[';
                    purchaseIds = '[';
                    for (var i = 0; i < checkedChk.length; i++) {
                        var trs = getFirstParentElement(checkedChk[i].parentNode, 'TR');
                        modifyStockIds += '"' + trs.modifyStockId + '",';
                        purchaseIds += '"' + trs.purchaseId + '",';
                    }
                    modifyStockIds = modifyStockIds.substring(0, modifyStockIds.length - 1);
                    modifyStockIds += ']';
                    purchaseIds = purchaseIds.substring(0, purchaseIds.length - 1);
                    purchaseIds += ']';
                }
                document.getElementById('hfldModifyStockId').value = modifyStockIds;
                document.getElementById('hfldPurchaseIds').value = purchaseIds;
            });

            jwTable.registClickAllCHKLitener(function () {
                if (this.checked) {
                    var checkedChk = jwTable.getAllChk();
                    document.getElementById('hfldPurchaseplanChecked').value = jwTable.getCheckedChkIdJson();
                    document.getElementById('btnDelete').removeAttribute('disabled');
                    var modifyStockIds;
                    var purchaseIds;
                    if (checkedChk.length > 0) {
                        if (checkedChk.length == 1) {
                            document.getElementById('hfldPurchaseplanChecked').value = jwTable.getCheckedChkIdJson(checkedChk);
                            var tr = getFirstParentElement(checkedChk[0].parentNode, 'TR');
                            modifyStockIds = tr.modifyStockId;
                            purchaseIds = tr.purchaseId;
                        } else {
                            modifyStockIds = "[";
                            purchaseIds = "[";
                            for (var i = 0; i < checkedChk.length; i++) {
                                var trs = getFirstParentElement(checkedChk[i].parentNode, 'TR');
                                modifyStockIds += '"' + trs.modifyStockId + '",';
                                purchaseIds += '"' + trs.purchaseId + '",';
                            }
                            modifyStockIds = modifyStockIds.substring(0, modifyStockIds.length - 1);
                            modifyStockIds += ']';
                            purchaseIds = purchaseIds.substring(0, purchaseIds.length - 1);
                            purchaseIds += ']';
                        }
                    } else {
                        modifyStockIds = '';
                        purchaseIds = '';
                    }
                    document.getElementById('hfldModifyStockId').value = modifyStockIds;
                    document.getElementById('hfldPurchaseIds').value = purchaseIds;
                }
                else {
                    document.getElementById('hfldPurchaseplanChecked').value = '';
                    document.getElementById('hfldModifyStockId').value = '';
                    document.getElementById('hfldPurchaseIds').value = '';
                    document.getElementById('btnDelete').setAttribute('disabled', 'disabled');
                }
            });
            replaceEmptyTable('emptyStock', 'gvwPurchaseplanStock');
            showTooltip('tooltip', 10);

            // 计算小计
            calcTotal();
        });

        // 计算小计
        function calcTotal() {
            $('#gvwPurchaseplanStock .num, #gvwPurchaseplanStock .price').blur(function () {
                var tr = getFirstParentElement(this, 'TR');
                var num = _strToDecimal($(tr).find('.num').val());
                var price = _strToDecimal($(tr).find('.price').val());
                var total = _formatDecimal(num * price);
                $(tr).find('.total').html(total);
                calcSumTotal();
            });
        }

        // 计算合计
        function calcSumTotal() {
            var sumTotal = 0.0;
            $('#gvwPurchaseplanStock .total').each(function () {
                sumTotal += _strToDecimal($(this).html());
            });
            $('#gvwPurchaseplanStock ._total_').html(_formatDecimal(sumTotal));
        }

        function setTxtCorpDisabled() {
            var inputs = document.getElementsByTagName('INPUT');
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].id.indexOf('txtCorp') > 0) {
                    inputs[i].setAttribute('readonly', 'readonly');
                }
            }
        }

        function registerCancelEvent() {
            $('#btnCancel').click(function () {
                winclose('ModifyEdit', 'PayoutModifyEdit.aspx', false);
            });
        }

        // 从采购计划中选择
        function selectPurchaseplanEvent() {
            var url = '/StockManage/Purchase/PurchaseplanList.aspx';
            top.ui.callback = function (o) {
                $('#hfldResourceId').val(o.id);
                $('#btnBindResource').click();
            }
            top.ui.openWin({ title: '从采购计划中选择', url: url });
        }

        // 选择供应商
        function pickCorp(param) {
            var txtID = param.id.replace('txt', 'hfld');
            jw.selectOneCorp({ idinput: txtID, nameinput: param.id });
        }

        // 从材料库中选择资源
        function selectResource() {
            var typeCode = '0002,0003';
            var src = '/StockManage/UserControl/SelectResource.aspx?type=2&TypeCode=' + typeCode;
            top.ui.callback = function (obj) {
                $('#hfldResourceId').val(obj.id);
                $('#btnBindResource').click();
            }
            top.ui.openWin({ title: '选择资源', url: src, width: 1010, height: 500 });
        }

        function ManageTargetPurchase(param) {
            if (param == 'Target') {
                document.getElementById('divPurchase').style.display = 'none';
                document.getElementById('divTarget').style.display = '';
                document.getElementById('spTarget').className = 'xxkd';
                document.getElementById('spPurchase').className = 'xxk';
                $('#hfldIsTarPur').val('Target');
            } else if (param == 'Purchase') {
                document.getElementById('divTarget').style.display = 'none';
                document.getElementById('divPurchase').style.display = '';
                document.getElementById('spTarget').className = 'xxk';
                document.getElementById('spPurchase').className = 'xxkd';
                $('#hfldIsTarPur').val('Purchase');
            }
        }

        // 新增预算变更
        function addBudModify() {
            var contractId = jw.getRequestParam('ContractId');
            var url = '/ContractManage/PayoutModify/AddBudModify.aspx?type=add&prjId=' + $('#hdnProjectCode').val()
				+ '&contractId=' + contractId
				+ '&modifyId=' + $('#hfldBudModifyId').val();
            top.ui.callback = function (t) {
                var arr = new Array();
                var oldJson = $('#hfldAllModifyTaskJson').val();
                if (oldJson == '') {
                    arr.push(t);
                } else {
                    arr = JSON.parse(oldJson);
                    arr.push(t);
                }
                $('#hfldAllModifyTaskJson').val(JSON.stringify(arr));
                $('#btnRefresh').click();
            }
            top.ui.openWin({ title: '新增预算变更', url: url, width: 700, height: 500 });
        }

        // 编辑预算变更
        function editBudModify() {
            var mtid = $('#hfldEditModifyTaskId').val(); 	// ModifyTaskId
            if (!mtid) return false;
            var json = $('#hfldAllModifyTaskJson').val();
            if (json.length <= 2) return false;

            var arr = JSON.parse(json);
            for (var i = 0; i < arr.length; i++) {
                if (arr[i].ModifyTaskId == mtid) {
                    setCookie("modifyTaskJson", JSON.stringify(arr[i]));
                    break;
                }
            }

            var contractId = jw.getRequestParam('ContractId');
            var url = '/ContractManage/PayoutModify/AddBudModify.aspx?type=edit&prjId=' + $('#hdnProjectCode').val()
				+ '&contractId=' + contractId
				+ '&modifyId=' + $('#hfldBudModifyId').val()
				+ '&modifyTaskId=' + mtid;

            top.ui._payconmodify = window;

            top.ui.callback = function (t) {
                var arr = JSON.parse($('#hfldAllModifyTaskJson').val());
                for (var i = 0; i < arr.length; i++) {
                    if (arr[i].ModifyTaskId == t.ModifyTaskId) {
                        arr.splice(i, 1, t);
                    }
                }
                $('#hfldAllModifyTaskJson').val(JSON.stringify(arr));
                $('#btnRefresh').click();
            }

            top.ui.openWin({ title: '编辑预算变更', url: url, width: 700, height: 500 });
            $('#btnEditBudModify').attr('mtid', '')
        }

        // 删除预算变更
        function delBudModify() {
            var mtid = $('#hfldEditModifyTaskId').val(); 	// ModifyTaskId
            if (!mtid) return false;
            var json = $('#hfldAllModifyTaskJson').val();
            if (json.length <= 2) return false;

            var arr = JSON.parse(json);
            for (var i = 0; i < arr.length; i++) {
                if (arr[i].ModifyTaskId == mtid) {
                    arr.splice(i, 1);
                }
            }

            $('#hfldAllModifyTaskJson').val(JSON.stringify(arr));
            $('#btnRefresh').click();
        }

        // 资源配置
        function allocRes() {
            var mtid = $('#hfldEditModifyTaskId').val();
            var modifyType = $('#' + mtid).attr('modifyType')
            var spType = 'in';
            if (modifyType == '0')
                spType = 'out';
            parent.desktop.BudModifyResList = window;
            var url = '/BudgetManage/Budget/BudModifyResList.aspx?' + new Date().getTime() + '&spType=' + spType;
            url += '&modifyId=' + $('#hfldBudModifyId').val() + '&modifyTaskId=' + mtid + '&modifyPage=1';
            toolbox_oncommand(url, '选择资源');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader2">
        <table class="tableHeader2">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="支出合同变更" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div id="divContent2" class="divContent2">
        <table id="tableContent" class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td colspan="4" class="groupInfo">
                    合同基本信息
                </td>
            </tr>
            <tr>
                <td class="word">
                    合同编号
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtContractCode" ReadOnly="true" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    合同名称
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtContractName" Height="15px" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    原始金额
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtContractMoney" Height="15px" Width="100%" class="decimal_input" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    最终金额
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtFinalMoney" Height="15px" class="decimal_input" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    签订时间
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtSignDate" Height="15px" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="groupInfo">
                    合同变更信息
                </td>
            </tr>
            <tr>
                <td class="word">
                    变更编号
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtModifyCode" Height="15px" CssClass="{required:true, messages:{required:'变更编号必须输入'}}" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    变更金额
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtModifyMoney" CssClass=" decimal_input  {required:true, number:true, messages:{required:'变更金额必须输入', number:'变更金额格式错误'}}" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    办理人
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtModifyPerson" CssClass="{required:true, messages:{required:'办理人必须输入'}}" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    变更时间
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtModifyDate" CssClass="{required:true, messages:{required:'变更时间必须输入'}}" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    变更原因
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtReason" TextMode="MultiLine" Height="50px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    备注
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtNotes" TextMode="MultiLine" Height="50px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    录入人
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtInputPerson" CssClass="{required:true, messages:{required:'办理人必须输入'}}" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    录入时间
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtInputDate" CssClass="{required:true, messages:{required:'录入时间'}}" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    附件
                </td>
                <td colspan="3" style="padding-right: 0px;">
                    <MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <div id="div">
                        <span id="spTarget" style="margin-left: 0px;" class="xxkd" onclick="ManageTargetPurchase('Target')" runat="server">
                            控制指标</span> <span id="spPurchase" class="xxk" onclick="ManageTargetPurchase('Purchase')" runat="server">
                                采购单</span>
                    </div>
                </td>
            </tr>
        </table>
        <div id='divTarget'>
            <table class="tableContent2" cellpadding="5px" cellspacing="0">
                <tr>
                    <td>
                        <input type="button" id="btnAddBudModify" value="新增" onclick="addBudModify();" />
                        <input type="button" id="btnEditBudModify" value="编辑" onclick="editBudModify();"
                            disabled="disabled" />
                        <input type="button" id="btnDelBudModify" value="删除" onclick="delBudModify();" disabled="disabled" />
                        <input type="button" id="btnAllocRes" value="资源配置" style="width: 70px; display: none;"
                            onclick="allocRes();" disabled="disabled" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvBudget" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvBudget_RowDataBound" DataKeyNames="ModifyTaskId,TaskId,OrderNumber,ModifyType" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                        <asp:HiddenField ID="hfldInModifyTaskId" Value='<%# Convert.ToString(Eval("ModifyTaskId")) %>' runat="server" />
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="名称"><ItemTemplate>
                                        <span style="vertical-align: middle;">
                                            <asp:Label ID="lblModifyTaskContent" flag="Quantity" Style="text-align: right;" Text='<%# Convert.ToString(Eval("ModifyTaskContent")) %>' runat="server"></asp:Label>
                                        </span>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="编码"><ItemTemplate>
                                        <asp:Label ID="lblModifyTaskCode" flag="Quantity" Style="text-align: right;" Text='<%# Convert.ToString(Eval("ModifyTaskCode")) %>' runat="server"></asp:Label>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="类型" HeaderStyle-Width="50px"><ItemTemplate>
                                        <%# Common2.GetTaskTypeName(Eval("OrderNumber").ToString()) %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="50px"><ItemTemplate>
                                        <%# Eval("Unit") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程量" HeaderStyle-Width="70px">
<ItemTemplate>
                                        <asp:Label ID="lblQuantity" flag="Quantity" CssClass="decimal_input" Style="text-align: right;" Text='<%# Convert.ToString(Eval("Quantity")) %>' runat="server"></asp:Label>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="开始时间" HeaderStyle-Width="50px"><ItemTemplate>
                                        <%# Common2.GetTime(Eval("StartDate")) %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结束时间" HeaderStyle-Width="50px"><ItemTemplate>
                                        <%# Common2.GetTime(Eval("EndDate")) %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="综合单价" HeaderStyle-Width="70px">
<ItemTemplate>
                                        <%# Common2.FormatDecimal(Eval("UnitPrice")) %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="小计" HeaderStyle-Width="70px">
<ItemTemplate>
                                        <%# Common2.FormatDecimal(Eval("Total2")) %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="变更类型" HeaderStyle-Width="70px">
<ItemTemplate>
                                        <%# (Eval("ModifyType").ToString() == "0") ? "清单外" : "清单内" %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
                                        <asp:HyperLink ID="hlinkShowNote" Style="text-decoration: none; color: Black;" ToolTip='<%# Convert.ToString(Eval("Note").ToString()) %>' runat="server"><%# StringUtility.GetStr(Eval("Note").ToString()) %></asp:HyperLink>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="排序" Visible="false"><ItemTemplate>
                                        <%# StringUtility.GetStr(Convert.ToString(Eval("OrderNumber"))) %>
                                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divPurchase" style="display: none;" runat="server">
            <table class="tableContent2" cellpadding="5px" cellspacing="0">
                <tr>
                    <td>
                        <input type="button" id="btnSelectPurchaseplan" style="width: 110px" value="从采购计划中选择"
                            onclick="selectPurchaseplanEvent();" />
                        <input type="button" id="btnSelectResource" value="从材料库中选择" style="width: 100px;"
                            onclick="selectResource();" />
                        <asp:Button ID="btnDelete" Text="删除" OnClick="btnDelete_Click" runat="server" />
                    </td>
                </tr>
                <tr style="vertical-align: top">
                    <td style="width: 100%;">
                        <asp:GridView ID="gvwPurchaseplanStock" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwPurchaseplanStock_RowDataBound" DataKeyNames="ResourceCode,ModifyStockId,psid" runat="server">
<EmptyDataTemplate>
                                <table id="emptyStock" class="tab">
                                    <tr class="header">
                                        <th scope="col" style="width: 20px;">
                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                        </th>
                                        <th scope="col" style="width: 25px;">
                                            序号
                                        </th>
                                        <th scope="col">
                                            物资编号
                                        </th>
                                        <th scope="col">
                                            物资名称
                                        </th>
                                        <th scope="col">
                                            规格
                                        </th>
                                        <th scope="col">
                                            单位
                                        </th>
                                        <th scope="col">
                                            数量
                                        </th>
                                        <th scope="col">
                                            采购价格
                                        </th>
                                        <th scope="col">
                                            小计
                                        </th>
                                        <th scope="col">
                                            供应商
                                        </th>
                                        <th scope="col">
                                            型号
                                        </th>
                                        <th scope="col">
                                            品牌
                                        </th>
                                        <th scope="col">
                                            技术参数
                                        </th>
                                        <th scope="col">
                                            采购单号
                                        </th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" runat="server" />
                                    </HeaderTemplate><ItemTemplate>
                                        <asp:CheckBox ID="chkSelectSingle" runat="server" />
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资编号" HeaderStyle-Width="100px">
<ItemTemplate>
                                        <%# Eval("ResourceCode") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="物资名称">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("ResourceName").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("ResourceName").ToString(), 10) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="规格">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("Specification").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("Specification").ToString(), 10) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="50px">
<ItemTemplate>
                                        <%# Eval("Name") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="库存量" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                        <%# WebUtil.GetStockNumberByCode(Eval("ResourceCode").ToString()).ToString() %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数量" HeaderStyle-Width="70px"><ItemTemplate>
                                        <asp:TextBox ID="txtNumber" Style="text-align: right;" Width="60px" CssClass="decimal_input num" Text='<%# Convert.ToString((Eval("number").ToString() == "") ? "0.000" : Convert.ToDecimal(Eval("number")).ToString("0.000")) %>' No='<%# Convert.ToString(Container.DataItemIndex) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="采购价格" HeaderStyle-Width="70px"><ItemTemplate>
                                        <asp:TextBox ID="txtSprice" Style="text-align: right;" Width="60px" CssClass="decimal_input price" Text='<%# Convert.ToString((Eval("sprice").ToString() == "") ? "0.000" : Convert.ToDecimal(Eval("sprice")).ToString("0.000")) %>' No='<%# Convert.ToString(Container.DataItemIndex) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="小计" HeaderStyle-Width="70px" ItemStyle-CssClass="decimal_input total">
<ItemTemplate>
                                        <%# Convert.ToDecimal(Eval("Total")).ToString("0.000") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="供应商" HeaderStyle-Width="100px">
<ItemTemplate>
                                        <asp:TextBox ID="txtCorp" Width="90px" CssClass="{required:true, messages:{required:'供应商必须输入'}}" ondblclick="pickCorp(this)" Style="background-color: #ffffc0;" Text='<%# Convert.ToString(Eval("CorpName")) %>' runat="server"></asp:TextBox>
                                        <asp:HiddenField ID="hfldCorp" Value='<%# Convert.ToString(Eval("corp")) %>' runat="server" />
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="型号">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("ModelNumber").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("ModelNumber").ToString(), 10) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="品牌">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("brand").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("brand").ToString(), 10) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="技术参数">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("TechnicalParameter").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("TechnicalParameter").ToString(), 10) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="采购单号" HeaderStyle-Width="50px">
<ItemTemplate>
                                        <%# Eval("pscode") %>
                                    </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="divFooter" class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" Height="21px" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hfldModifyId" runat="server" />
    <!--从采购计划中选择物资的物资ID-->
    <asp:HiddenField ID="hfldResourceId" runat="server" />
    <!--存放所有被选中物质采购计划编号-->
    <asp:HiddenField ID="hfldPurchaseplanChecked" runat="server" />
    <!--采购单对应的单据号-->
    <asp:HiddenField ID="hfldppcode" runat="server" />
    <!--合同的乙方-->
    <asp:HiddenField ID="hfldBId" runat="server" />
    <!--合同项目-->
    <asp:HiddenField ID="hdnProjectCode" runat="server" />
    <!--合同变更物资Ids-->
    <asp:HiddenField ID="hfldModifyStockId" runat="server" />
    <!--采购物资Ids-->
    <asp:HiddenField ID="hfldPurchaseIds" runat="server" />
    <!-- 预算变更Id -->
    <asp:HiddenField ID="hfldBudModifyId" runat="server" />
    <!-- 最后添加或者变更的预算变更信息-->
    <asp:HiddenField ID="hfldAllModifyTaskJson" runat="server" />
    <!-- 要编辑的预算变更节点Id-->
    <asp:HiddenField ID="hfldEditModifyTaskId" runat="server" />
    
    <asp:HiddenField ID="hfldIsWBSRelevance" runat="server" />
    <!--从采购计划中选择物资后执行-->
    <asp:Button ID="btnBindResource" Text="Button" OnClick="btnBindResource_Click" runat="server" />
    <asp:Button ID="btnRefresh" Text="" Style="display: none;" OnClick="btnRefresh_Click" runat="server" />
    </form>
</body>
</html>
