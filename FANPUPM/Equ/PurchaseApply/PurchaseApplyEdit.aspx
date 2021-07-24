<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurchaseApplyEdit.aspx.cs" Inherits="Equ_PurchaseApply_PurchaseApplyEdit" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>固定资产采购申请单</title><link rel="stylesheet" type="text/css" href="../../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnAddDetail').hide();
            Val.validate('form1', 'btnSave');
            $('#txtApplicant').attr('readonly', 'readonly');
            $('#btnDelete').attr('disabled', 'disabled');
            $('#btnCancel').click(btnCancelClick);
            applyDetail();
            $('#btnAdd').click(selectResource);
            $('#txtQuantity').blur(function () {
                intValidator(txtQuantity);
            })
            setDisabled();
            // 显示被截取的信息
            jw.tooltip();
        })
        // 取消按钮事件
        function btnCancelClick() {
            winclose('PurchaseApplyEdit', 'PurchaseApplyList.aspx', false);
        }
        // 设置readonly
        function setDisabled() {
            $('#txtDeployPlan').attr('readonly', 'readonly');
            $('#txtApplicant').attr('readonly', 'readonly');
            $('#txtApplyDate').attr('readonly', 'readonly');
        }
        // 选择设备调配计划
        function selectEquDeployPlan() {
            var url = '/Equ/EquDeployPlan/SelectEquDeployPlan.aspx?' + new Date().getDate();
            top.ui.callback = function (deployPlanInfo) {
                $('#hfldDeployPlanId').val(deployPlanInfo.id);
                $('#txtDeployPlan').val(deployPlanInfo.code);
            };
            top.ui.openWin({ title: '选择设备调配计划', url: url, width: 1000, height: 500 });
        }
        //选择人员
        function selectUser(id, name) {
            var func = function () {
                $.ajax({
                    type: "GET",
                    async: false,
                    url: '/TenderManage/Handler/GetUserDep.ashx?usercode=' + $('#hfldApplicant').val(),
                    success: function (data) {
                        if (data != undefined) {
                            $('#txtApplyDep').val(data);
                        }
                    }
                });
            }
            jw.selectOneUser({ nameinput: name, codeinput: id, func: func });
        }
        // 采购申请明细
        function applyDetail() {
            var jwTable = new JustWinTable('gvwApplyDetail');
            setButton(jwTable, '', '', '', 'hfldDetailID');
            // 单击行
            jwTable.registClickTrListener(function () {
                $('#btnDelete').removeAttr('disabled');
            });
            // 单选
            jwTable.registClickSingleCHKListener(function () {
                var checkedChk = jwTable.getCheckedChk();
                if (checkedChk.length == 0) {
                    $('#btnDelete').attr('disabled', 'disabled');
                }
                else if (checkedChk.length == 1) {
                    $('#btnDelete').removeAttr('disabled');
                }
                else {
                    $('#btnDelete').removeAttr('disabled');
                }
            });
            // 全选
            jwTable.registClickAllCHKLitener(function () {
                if (this.checked) {
                    $('#btnDelete').removeAttr('disabled');
                }
                else {
                    $('#btnDelete').attr('disabled', 'disabled');
                }
            });
            replaceEmptyTable('emptyApplyDetail', 'gvwApplyDetail');
        }
        // 从材料库中选择资源
        function selectResource() {
            var typeCode = '0003';     //控制选择资源的类型 0002代表材料  0003代表机械
            var src = '/StockManage/UserControl/SelectResource.aspx?type=2&TypeCode=' + typeCode;
            top.ui.callback = function (obj) {
                $('#hfldResourceId').val(obj.id);
                $('#btnAddDetail').click();
            }
            top.ui.openWin({ title: '选择资源', url: src, width: 1010, height: 500 });
        }
        //选择资源
        function selectRes(txtRes) {
            var hfldRes = txtRes.id.replace('txt', 'hfld');
            var url = '/Equ/Equipment/SelectResource.aspx?' + new Date().getTime();
            var resInfo = { width: 1000, height: 550, url: url, title: '选择资源' };
            top.ui.callback = function (resInfo) {
                $('#' + hfldRes).val(resInfo.resId);
                $('#' + txtRes.id).val(resInfo.resName);
            };
            top.ui.openWin(resInfo);
        }
        // 验证整数
        function intValidator(id) {
            var value = $('#' + id).val();
            var reg = new RegExp('^[1-9]*[1-9][0-9]*$');
            if (!reg.test(value)) {
                $('#' + id).val('0');
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader2">
        <table class="tableHeader">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="采购申请" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="divContent2">
        <table class="tableContent2" style="" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    申请单号
                </td>
                <td class="txt mustInput" style="padding-right: 3px">
                    <asp:TextBox ID="txtApplyCode" CssClass="{required:true, messages:{required:'申请单号不能为空'}}" runat="server"></asp:TextBox>
                    <asp:HiddenField ID="hfldApplyId" runat="server" />
                </td>
                <td class="word">
                    设备调配计划单号
                </td>
                <td style="padding-right: 3px">
                    <span class="spanSelect" style="width: 100%;">
                        <asp:TextBox type="text" ID="txtDeployPlan" Style="width: 90%; height: 15px; border: none;
                            line-height: 16px; margin: 1px 0px 1px 2px" runat="server"></asp:TextBox>
                        <img src="../../../images/icon.bmp" style="float: right;" alt="设备调配计划单号" id="img1"
                            onclick="selectEquDeployPlan();" />
                    </span>
                    <asp:HiddenField ID="hfldDeployPlanId" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    申请人
                </td>
                <td class="txt mustInput">
                    <span class="spanSelect" style="width: 100%">
                        <asp:TextBox ID="txtApplicant" data-options="validType:'validQueryChars[50]'" CssClass="{required:true, messages:{required:'申请人不能为空'}}" Style="width: 97px;
                            height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                        <img alt="选择申请人" onclick="selectUser('hfldApplicant','txtApplicant');" src="../../images/icon.bmp"
                            style="float: right;" />
                        <asp:HiddenField ID="hfldApplicant" runat="server" />
                    </span>
                </td>
                <td class="word">
                    申请部门
                </td>
                <td>
                    <input type="text" id="txtApplyDep" width="100%" readonly="readonly" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="word">
                    申请日期
                </td>
                <td class="txt mustInput">
                    <asp:TextBox type="text" ID="txtApplyDate" Width="100%" CssClass="{required:true, messages:{required:'申请日期不能为空'}}" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table class="tableContent2" cellpadding="5" cellspacing="0">
            <tr>
                <td>
                    <hr class="sp" />
                </td>
            </tr>
            <tr>
                <td>
                    <div class="headerDiv" style="text-align: left;">
                        <input type="button" id="btnAdd" value="添加明细" style="width: 150px" />
                        <asp:Button ID="btnAddDetail" OnClick="btnAddDetail_Click" runat="server" />
                        <asp:Button ID="btnDelete" Text="删除" OnClientClick="return confirm('您确定要删除吗?')" OnClick="btnDelete_Click" runat="server" />
                    </div>
                </td>
            </tr>
            <tr style="vertical-align: top">
                <td style="text-align: right">
                    <div class="pagediv">
                        <asp:GridView ID="gvwApplyDetail" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwApplyDetail_RowDataBound" DataKeyNames="Id" runat="server">
<EmptyDataTemplate>
                                <table id="emptyApplyDetail" class="tab">
                                    <tr class="header">
                                        <th scope="col" style="width: 20px;">
                                            <asp:CheckBox ID="chkSelectAll" runat="server" />
                                        </th>
                                        <th scope="col" style="width: 25px;">
                                            序号
                                        </th>
                                        <th scope="col">
                                            资源名称
                                        </th>
                                        <th scope="col">
                                            申请数量
                                        </th>
                                        <th scope="col">
                                            预计单价
                                        </th>
                                        <th scope="col">
                                            建议厂家
                                        </th>
                                        <th scope="col">
                                            需求原因
                                        </th>
                                        <th scope="col">
                                            计划到货日期
                                        </th>
                                        <th scope="col">
                                            到货地点
                                        </th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" runat="server" />
                                    </HeaderTemplate><ItemTemplate>
                                        <asp:CheckBox ID="chkSelectSingle" runat="server" />
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                        
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="资源名称"><ItemTemplate>
                                        <span class="tooltip" title=''>
                                            
                                        </span>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="申请数量">
<ItemTemplate>
                                        <asp:TextBox ID="txtQuantity" Style="text-align: right;" Width="60px" onblur="intValidator(this.id);" Text='<%# System.Convert.ToString(Eval("Quantity").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' No='<%# System.Convert.ToString(Container.DataItemIndex, System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="预计单价">
<ItemTemplate>
                                        <asp:TextBox ID="txtUnitPrice" Style="text-align: right;" Width="60px" CssClass="decimal_input" Text='<%# System.Convert.ToString(System.Convert.ToDecimal(Eval("UnitPrice")).ToString("0.000"), System.Globalization.CultureInfo.CurrentCulture) %>' No='<%# System.Convert.ToString(Container.DataItemIndex, System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="建议厂家">
<ItemTemplate>
                                        <asp:TextBox ID="txtSuggestFactory" Width="60px" Text='<%# System.Convert.ToString((Eval("SuggestFactory") == null) ? "" : Eval("SuggestFactory").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' No='<%# System.Convert.ToString(Container.DataItemIndex, System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="需求原因">
<ItemTemplate>
                                        <asp:TextBox ID="txtRequireReason" Width="60px" Style="height: 15px; width: 90px; text-align: left;" Text='<%# System.Convert.ToString((Eval("RequireReason") == null) ? "" : Eval("RequireReason").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' No='<%# System.Convert.ToString(Container.DataItemIndex, System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="计划到货日期">
<ItemTemplate>
                                        <asp:TextBox ID="txtArriveDate" onclick="WdatePicker()" Style="height: 15px;
                                            width: 90px; text-align: left;" Text='<%# System.Convert.ToString((Eval("ArriveDate") == null) ? "" : System.Convert.ToDateTime(Eval("ArriveDate")).ToString("yyyy-MM-dd"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="到货地点">
<ItemTemplate>
                                        <asp:TextBox ID="txtArrivePlace" Style="height: 15px; width: 90px; text-align: left;" Text='<%# System.Convert.ToString((Eval("ArrivePlace") == null) ? "" : Eval("ArrivePlace").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" />
                </td>
            </tr>
        </table>
    </div>
    
    <asp:HiddenField ID="hfldDetailID" runat="server" />
    
    <asp:HiddenField ID="hfldResourceId" runat="server" />
    
    <asp:HiddenField ID="hfldEquTypeName" runat="server" />
    </form>
</body>
</html>
