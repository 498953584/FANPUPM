<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResourceList.aspx.cs" Inherits="BudgetManage_Construct_TaskResource" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_usercontrol_selectresource_ascx" Src="~/StockManage/UserControl/SelectResource.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title></title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="../../Script/Budget/BudgetPait.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/DecimalInput.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnBindResource').hide();
            replaceEmptyTable('emptyTable', 'gvResource');
            var table = new JustWinTable('gvResource');
            var gvResource = new JustWinTable('gvResource');
            Val.validate('form1', 'btnSave');
            setButton2(gvResource, 'btnDelete', 'btnEdit', 'btnTemImport', 'hfldPurchaseChecked');
            setWidthHeight();
            showTooltip('tooltip', 10);
        });

        function setWidthHeight() {
            $("#divBudget").width($("#divTop").width() - 5);
            $("#divBudget").height($(this).height() - $("#divBottom").height() - $("#divTop").height() - 5);
        }

        function validate() {
            if (!document.getElementById('gvResource') || document.getElementById('gvResource').firstChild.childNodes.length == 1) {
                alert('系统提示：\n\n请选择资源');
                return false;
            }
        }

        function setButton2(jwTable, btnDel, btnUpdate, btnQuery, hdChkId) {
            if (!jwTable.table) return;
            if (jwTable.table.firstChild.childNodes.length == 1) {
                //table中没有数据
                return;
            }
            jwTable.registClickTrListener(function () {
                if (hdChkId != '')
                    document.getElementById(hdChkId).value = this.id;
                if (this.guid) {
                    document.getElementById(btnQuery).guid = this.guid;
                }
                document.getElementById(btnDel).removeAttribute('disabled');
            });
            jwTable.registClickSingleCHKListener(function () {
                var checkedChk = jwTable.getCheckedChk();
                if (checkedChk.length == 0) {
                    document.getElementById(btnDel).setAttribute('disabled', 'disabled');


                }
                else if (checkedChk.length == 1) {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson(checkedChk);
                    document.getElementById(btnDel).removeAttribute('disabled');
                }
                else {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson(checkedChk);

                    var checkedChks = jwTable.getCheckedChk();
                }
            });
            jwTable.registClickAllCHKLitener(function () {
                if (this.checked) {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson();
                    var checkedChks = jwTable.getAllChk();
                    document.getElementById(btnDel).removeAttribute('disabled');
                }
                else {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = '';
                    document.getElementById(btnDel).setAttribute('disabled', 'disabled');
                }

                if (jwTable.table.rows.length == 2 && this.checked == true) {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson();

                }
            });
        };

        // 添加资源
        function addResource() {
            var src = '/StockManage/UserControl/SelectResource.aspx?type=2&TypeCode=0';
            top.ui.callback = function (obj) {
                $('#hfldResourceId').val(obj.id);
                $('#btnBindResource').click();
            }
            top.ui.openWin({ title: '选择资源', url: src, width: 1010, height: 500 });
        }

        // 计算小计
        function calcTotal(e) {
            var $tr = $(getFirstParentElement(e, 'TR'));
            var price = _strToDecimal($tr.find('.price').val());
            var number = _strToDecimal($tr.find('.number').val());
            var total = price * number;
            $tr.find('.total').val(_formatDecimal(total));
        }
    </script>
    <style type="text/css">
        .descTd
        {
            text-align: right;
            font-weight: normal;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="border: 0px; width: 100%;">
            <tr id="divTop">
                <td class="divFooter" style="text-align: left;">
                    <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload1" Name="图片" FileType="*.jpg;*.gif;*.png" Class="ConstructReport" runat="server" />
                </td>
                <td style="text-align: right;" class="divFooter">
                    <input type="button" id="btnResource" style="width: 70px;" value="增加资源" onclick="addResource();" />
                    <asp:Button Width="70px" ID="btnDelete" Text="删除" disabled="disabled" OnClientClick="return confirm('您确定要删除吗??')" OnClick="btnDelete_Click" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="vertical-align: top;">
                    <div class="pagediv" id="divBudget" style="height: 200px;">
                        <asp:GridView ID="gvResource" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="resourceId" runat="server">
<EmptyDataTemplate>
                                <table id="emptyTable" style="background-color: Red;" class="tab" width="100%">
                                    <tr class="header">
                                        <td style="width: 20px">
                                            <asp:CheckBox ID="cbAllBox" runat="server" />
                                        </td>
                                        <td style="width: 25px">
                                            序号
                                        </td>
                                        <td style="width: 100px">
                                            资源编号
                                        </td>
                                        <td style="width: 150px">
                                            资源名称
                                        </td>
                                        <td>
                                            规格
                                        </td>
                                        <td>
                                            型号
                                        </td>
                                        <td>
                                            品牌
                                        </td>
                                        <td>
                                            技术参数
                                        </td>
                                        <td>
                                            单位
                                        </td>
                                        <td>
                                            单价
                                        </td>
                                        <td>
                                            数量
                                        </td>
                                        <td>
                                            小计
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
                                        <asp:CheckBox ID="cbAllBox" runat="server" />
                                    </HeaderTemplate>

<ItemTemplate>
                                        <asp:CheckBox ID="cbBox" ToolTip='<%# Convert.ToString(Eval("consTaskId")) %>' runat="server" />
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="资源编号" HeaderStyle-Width="20%"><ItemTemplate>
                                        <asp:Label ID="lblCode" Text='<%# Convert.ToString(Eval("Resource.Code")) %>' runat="server"></asp:Label>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="资源名称">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("Resource.Name").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("Resource.Name").ToString(), 10) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="规格">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("Resource.Specification").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("Resource.Specification").ToString(), 10) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="型号">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("Resource.ModelNumber") %>'>
                                            <%# StringUtility.GetStr(Eval("Resource.ModelNumber"), 10) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="品牌">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("Resource.brand") %>'>
                                            <%# StringUtility.GetStr(Eval("Resource.Brand"), 10) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="技术参数">
<ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("Resource.TechnicalParameter") %>'>
                                            <%# StringUtility.GetStr(Eval("Resource.TechnicalParameter"), 10) %>
                                        </span>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="10%"><ItemTemplate>
                                        <%# WebUtil.GetUnitNameByResId(Eval("Resource.Id").ToString()) %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单价" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="90px"><ItemTemplate>
                                        <asp:TextBox Style="text-align: right;" Width="90px" Height="15px" onblur="calcTotal(this);" CssClass="mustInput decimal_input price" ID="txtPrice" Text='<%# Convert.ToString((Eval("unitPrice").ToString() == "") ? "0.000" : Convert.ToDecimal(Eval("unitPrice")).ToString("0.000")) %>' No='<%# Convert.ToString(Container.DataItemIndex) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数量" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="90px"><ItemTemplate>
                                        <asp:TextBox Style="text-align: right;" Width="90px" Height="15px" onblur="calcTotal(this);" CssClass="mustInput decimal_input number" ID="txtNumber" Text='<%# Convert.ToString((Eval("Quantity").ToString() == "") ? "0.000" : Convert.ToDecimal(Eval("Quantity")).ToString("0.000")) %>' No='<%# Convert.ToString(Container.DataItemIndex) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="小计" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="90px"><ItemTemplate>
                                        <asp:TextBox Style="text-align: right;" Enabled="false" Width="90px" CssClass="total" Height="15px" ID="txtTotal" Text='<%# Convert.ToString(((Convert.ToDecimal(Eval("unitPrice")) * Convert.ToDecimal(Eval("Quantity"))).ToString() == "") ? "0.000" : (Convert.ToDecimal(Eval("unitPrice")) * Convert.ToDecimal(Eval("Quantity"))).ToString("0.000")) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 100%; height: 15%;">
                    <div id="divBottom" class="divFooter2" style="width: 99.9%">
                        <table class="tableFooter2">
                            <tr>
                                <td>
                                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_ServerClick" runat="server" />
                                    <asp:Button ID="btnClose" Text="关闭" OnClick="btnClose_Click" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    <asp:Button ID="btnBindResource" OnClick="btnBindResource_Click" runat="server" />
    <asp:HiddenField ID="hfldTypeCode" runat="server" />
    <asp:HiddenField ID="hfldButtonId" runat="server" />
    <asp:HiddenField ID="hfldResourceId" runat="server" />
    <asp:HiddenField ID="hfldResourceCode" runat="server" />
    <asp:HiddenField ID="hfldPrjId" runat="server" />
    </form>
</body>
</html>
