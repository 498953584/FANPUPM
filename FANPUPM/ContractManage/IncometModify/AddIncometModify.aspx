<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddIncometModify.aspx.cs" Inherits="ContractManage_IncometModify_AddIncometModify" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>收入合同变更</title>
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#hfldBtnMidifyTask').hide();
            $('#divInTask').hide();
            if (getRequestParam('t') == '1') {
                setAllInputDisabled();
            }
            Val.validate('form1', 'btnSave');
            var GvbudModify = new JustWinTable('GvbudModify');
            replaceEmptyTable('emptyTask', 'GvbudModify');
            GvbudModify.registClickTrListener(function () {
                $('#hfldMoidyTaskId').val(this.id);
                $('#btnEdit').removeAttr('disabled');
                $('#btnDel').removeAttr('disabled');
            });

            $('#GvbudModify tr').live('click', function () {
                if (getRequestParam('t') == '1') {
                    $('#btnAdd').attr('disabled', 'disabled');
                    $('#btnEdit').attr('disabled', 'disabled');
                    $('#btnDel').attr('disabled', 'disabled');
                }
            });

            GvbudModify.registClickSingleCHKListener(function () {
                var checkCHK = GvbudModify.getCheckedChk();
                $('#hfldMoidyTaskId').val(GvbudModify.getCheckedChkIdJson(checkCHK));
                if (checkCHK.length) {
                    $('#btnEdit').removeAttr('disabled');
                    $('#btnDel').removeAttr('disabled');
                } else {
                    $('#btnEdit').attr('disabled', 'disabled');
                    $('#btnDel').attr('disabled', 'disabled');
                }
                if (checkCHK.length != 1) {
                    $('#btnEdit').attr('disabled', 'disabled');
                }
            });
            GvbudModify.registClickAllCHKLitener(function () {
                var checkALL = GvbudModify.getAllChk();
                $('#hfldMoidyTaskId').val(GvbudModify.getCheckedChkIdJson(checkALL));
                if (this.checked) {
                    $('#btnEdit').attr('disabled', 'disabled');
                    $('#btnDel').removeAttr('disabled');
                } else {
                    $('#btnEdit').attr('disabled', 'disabled');
                    $('#btnDel').attr('disabled', 'disabled');
                }
            });
        });



        function pickSheet() {
            var action = 'modify';
            if (!getRequestParam("id")) {
                action = 'add';
            }
            var prjGuid = document.getElementById('hfldPrjGuid').value;
            var modifyId = document.getElementById('hdGuid').value;
            var src = "/ContractManage/UserControl/PickSheet.aspx?ModifyId=" + modifyId +
                '&PrjGuid=' + prjGuid + '&Action=' + action +
                '&time=' + new Date().getTime();
            top.ui.callback = function (obj) {
                $('#hfldSheetId').val(obj.id);
                $('#txtSheetName').val(obj.name);
            }
            top.ui.openWin({ title: '关联单据', url: src });
        }

        function addBud() {
            top.ui._AddConBudModify = window;
            var prjId = $('#hfldPrjGuid').val();
            var contractId = $('#hfldConIncometID').val();
            var modifyId = $('#hfldconBudModifyID').val();
            var url = '/ContractManage/IncometModify/AddConBudModify.aspx?action=add&prjId=' + prjId + '&contractId=' + contractId + '&modifyId=' + modifyId
            top.ui.callback = function () {
                $('#hfldBtnMidifyTask').click();
            }
            top.ui.openWin({ title: '新增合同预算变更', url: url, width: 700, height: 500 });
        }

        function editBud() {
            top.ui._AddConBudModify = window;
            var prjId = $('#hfldPrjGuid').val();
            var contractId = $('#hfldConIncometID').val();
            var modifyId = $('#hfldconBudModifyID').val();
            var url = '/ContractManage/IncometModify/AddConBudModify.aspx?action=Edit&prjId=' + prjId + '&contractId=' + contractId + '&modifyId=' + modifyId; // + '&modifyTask=' +$('#hfldModifyTaskJson').val();
            top.ui.callback = function () {
                $('#hfldBtnMidifyTask').click();
            }
            top.ui.openWin({ title: '编辑合同预算变更', url: url, width: 700, height: 500 });
        }
    </script>
    <style type="text/css">
        #FileLink1_But_UpFile
        {
            width: auto;
        }
        #FileLink1_Btn_Upload
        {
            width: auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader2">
        <table class="tableHeader2">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="divContent2">
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
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
                    <asp:TextBox ID="txtContractCode" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    合同名称
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtContractName" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    原始金额
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtContractPrice" CssClass="easyui-numberbox" data-options="min:-99999999999,max:99999999999,precision:3,groupSeparator:','" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    签订时间
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtSignedTime" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="groupInfo">
                    变更信息
                </td>
            </tr>
            <tr>
                <td class="word">
                    变更编号
                </td>
                <td class="txt mustInput">
                    <asp:TextBox Height="15px" Width="100%" CssClass="{required:true, messages:{required:'变更编号必须输入'}}" ID="txtChangeCode" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    办理人
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtTransactor" Height="15px" Width="100%" CssClass="{required:true, messages:{required:'办理人必须输入'}}" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    变更金额
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtChangePrice" Height="15px" Width="100%" CssClass="easyui-numberbox {required:true,number:true, messages:{required:'变更金额必须输入',number:'变更金额格式错误'}}" data-options="min:-99999999999,max:99999999999,precision:3,groupSeparator:','" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    变更时间
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtChangeTime" Width="100%" onclick="WdatePicker()" CssClass="{required:true, messages:{required:'变更时间必须输入'}}" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    最终金额
                </td>
                <td class="txt readonly">
                    <asp:TextBox Width="100%" CssClass="easyui-numberbox" data-options="min:-99999999999,max:99999999999,precision:3,groupSeparator:','" ReadOnly="true" ID="txtEndPrice" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    关联单据
                </td>
                <td style="width: 33%; text-align: left; white-space: nowrap; padding-right: 3px;">
                    <span id="spanSheet" class="spanSelect" style="width: 100%;">
                        <input type="text" style="width: 90%; height: 15px; border: none; line-height: 16px;
                            margin: 1px 0px 1px 2px" readonly="readonly" id="txtSheetName" runat="server" />

                        <img src="../../images/icon.bmp" style="float: right;" alt="选择单据" id="imgSheet" onclick="pickSheet();" />
                    </span>
                    <asp:HiddenField ID="hfldSheetId" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    变更原因
                </td>
                <td colspan="3" class="txt">
                    <asp:TextBox TextMode="MultiLine" ID="txtChangeReason" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    备注
                </td>
                <td colspan="3" class="txt">
                    <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" id="td1">
                    附件
                </td>
                <td colspan="3" style="text-align: left; padding-right: 0px;" class="txt">
                    <MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    录入人
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtInputPerson" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    录入时间
                </td>
                <td class="txt readonly">
                    <asp:TextBox Width="100%" ReadOnly="true" ID="txtInputDate" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <div id="ContractBud">
                        <span id="WBSBud" class="xxkd" style="margin-left: 0px;" runat="server">合同预算变更</span>
                    </div>
                </td>
            </tr>
            <tr style="vertical-align: top;">
                <td colspan="4" style="text-align: left;">
                    <input id="btnAdd" type="button" value="新增" onclick="addBud();" />
                    <input id="btnEdit" type="button" value="编辑" disabled="true" OnServerClick="btnEdit_Click" runat="server" />

                    <input id="btnDel" type="button" value="删除" disabled="true" OnServerClick="BtnDel_Click" runat="server" />

                </td>
            </tr>
        </table>
        <div id="conBud">
            <table class="tableContent2" cellpadding="5px" cellspacing="0">
                <tr>
                    <td>
                        <asp:GridView ID="GvbudModify" Width="100%" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="GvbudModify_RowDataBound" DataKeyNames="ModifyTaskId,ModifyId,TaskId,ModifyType,Quantity,Total" runat="server">
<EmptyDataTemplate>
                                <table id="emptyTask" class="tab" width="100%">
                                    <tr class="header">
                                        <td style="width: 20px">
                                            <asp:CheckBox ID="chkAll" runat="server" />
                                        </td>
                                        <td style="width: 25px;">
                                            序号
                                        </td>
                                        <td align="center">
                                            任务编码
                                        </td>
                                        <td align="center">
                                            任务名称
                                        </td>
                                        <td>
                                            类型
                                        </td>
                                        <td align="center">
                                            单位
                                        </td>
                                        <td align="center">
                                            开始时间
                                        </td>
                                        <td align="center">
                                            结束时间
                                        </td>
                                        <td align="center">
                                            工期
                                        </td>
                                        <td>
                                            工程量
                                        </td>
                                        <td align="center">
                                            单价
                                        </td>
                                        <td>
                                            小计
                                        </td>
                                        <td>
                                            附件
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                        <asp:CheckBox ID="cbAllBox" runat="server" />
                                    </HeaderTemplate><ItemTemplate>
                                        <asp:CheckBox ID="cbBox" ToolTip='<%# Convert.ToString(Eval("ModifyTaskId")) %>' runat="server" />
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="任务编码"><ItemTemplate>
                                        <%# Eval("ModifyTaskCode") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="任务名称"><ItemTemplate>
                                        <%# Eval("ModifyTaskContent") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="类型">
<ItemTemplate>
                                        <%# (Eval("ModifyType").ToString() == "1") ? "清单内" : "清单外" %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单位">
<ItemTemplate>
                                        <%# Eval("Unit") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="开始时间">
<ItemTemplate>
                                        <%# Common2.GetTime(Eval("StartDate")) %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="结束时间">
<ItemTemplate>
                                        <%# Common2.GetTime(Eval("EndDate")) %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="工期">
<ItemTemplate>
                                        <%# Eval("ConstructionPeriod") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="工程量" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# Eval("Quantity") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单价" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# Eval("UnitPrice") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="小计" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                        <%# Eval("Total") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="调整后数量" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="调整后小计" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="附件"><ItemTemplate>
                                        <asp:HiddenField ID="hfldParentId" Value='<%# Convert.ToString(Eval("ParentId")) %>' runat="server" />
                                        <%# StringUtility.FilesBind(Eval("ModifyTaskId").ToString(), "ContractBudChange") %>
                                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClientClick="return valForm()" OnClick="btnSave_Click" runat="server" />
                     <asp:Button type="button" ID="btnCancel" Text="取消" OnClick="btnCancel_Click" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdGuid" runat="server" />
    <asp:HiddenField ID="hdChangeCode" runat="server" />
    
    <asp:HiddenField ID="hfldPrjGuid" runat="server" />
    
    <asp:HiddenField ID="hfldSpType" runat="server" />
    
    <asp:HiddenField ID="hfldModifyId" runat="server" />
    
    <asp:HiddenField ID="hfldConIncometID" runat="server" />
    
    <asp:HiddenField ID="hfldconModifyID" runat="server" />
    
    <asp:HiddenField ID="hfldconBudModifyID" runat="server" />
    
    <asp:HiddenField ID="hfldCheckedTaskIdOut" runat="server" />
    
    <asp:HiddenField ID="hfldCheckedTaskIdIn" runat="server" />
    
    <asp:HiddenField ID="hfldTatolAmount" runat="server" />
    
    <asp:HiddenField ID="hfldMoidyTaskId" runat="server" />
    
    <asp:HiddenField ID="hfldModifyTaskJson" runat="server" />
    <asp:Button ID="hfldBtnMidifyTask" OnClick="hfldBtnMidifyTask_Click" runat="server" />
    </form>
</body>
</html>
