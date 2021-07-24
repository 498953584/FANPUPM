<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InitializeStorage.aspx.cs" Inherits="StockManage_Storage_InitializeStorage" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>初始化仓库</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../script/Config2.js"></script>
    <script type="text/javascript" src="../script/JustWinTable.js"></script>
    <script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('gvwTreasury');
            replaceEmptyTable('tbEmpty', 'gvwTreasury')
            showTooltip('tooltip', 10);
            jwTable.registClickTrListener(function () {
                $('#hfldResourceCodes').val(this.id);
                $('#btnDelete').removeAttr('disabled');
            });
            jwTable.registClickSingleCHKListener(function () {
                if (jwTable.getCheckedChk().length > 0) {
                    $('#hfldResourceCodes').val(jwTable.getCheckedChkIdJson(jwTable.getCheckedChk()));
                    $('#btnDelete').removeAttr('disabled');
                } else {
                    $('#hfldResourceCodes').val('');
                    $('#btnDelete').attr('disabled', 'disabled');
                }
            });
            jwTable.registClickAllCHKLitener(function () {
                if (this.checked) {
                    //全选
                    $('#hfldResourceCodes').val(jwTable.getCheckedChkIdJson());
                    $('#btnDelete').removeAttr('disabled');
                } else {
                    //全否
                    $('#hfldResourceCodes').val('');
                    $('#btnDelete').attr('disabled', 'disabled');
                }
            });
            $('#btnDelete').get(0).onclick = function () {
                var state = confirm('系统提示:\n\n您确定要删除吗?');
                if (!state) {
                    return false;
                }
            }

            $('#divTree').height($(this).height());
        });
        //初始化仓库
        function initialize() {
            parent.desktop.DirectStorageEdit = window;
            var url = '/StockManage/Storage/DirectStorageEdit.aspx?tcode=' + document.getElementById('hfldTcode').value;
            var storageName = $('#hfldTcodeText').val();
            toolbox_oncommand(url, storageName + "-初始化");
        }

        function registerEditListener() {
            alert($('#hfldResourceCodes').val() + '\n' + $('#hfldTcode').val())
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="tab" style="overflow: hidden;">
        <tr>
            <td class="divHeader" style="display: none;">
                初始化仓库
            </td>
        </tr>
        <tr>
            <td style="height: 100%; width: 100%;">
                <table class="tab">
                    <tr style="height: 100%">
                        <td style="width: 220px; vertical-align: top;">
                            <div id="divTree" style="width: 220px; height: 200px; overflow: auto;">
                                <asp:TreeView ID="tvTreasury" ShowLines="true" OnSelectedNodeChanged="TreeView_SelectedNodeChanged" runat="server"><SelectedNodeStyle Font-Underline="false" BackColor="#3399FF" ForeColor="White" HorizontalPadding="0px" VerticalPadding="0px" /></asp:TreeView>
                            </div>
                        </td>
                        <td style="vertical-align: top; border-left: solid 2px #CADEED; height: 100%;">
                            <table class="tab">
                                <tr>
                                    <td class="divFooter" style="text-align: left;">
                                        
                                        <input type="button" id="btnInitializeStorage" value="初始化仓库" style="width: 80px;" disabled="true" onclick="initialize()" runat="server" />

                                        <asp:Button ID="btnDelete" Enabled="false" Text="删除" OnClick="btnDelete_Click" runat="server" />
                                        <asp:Label ID="lblWarningMessage" Style="color: Red;" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">
                                        <div class="pagediv">
                                            <asp:GridView ID="gvwTreasury" Width="100%" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwTreasury_RowDataBound" DataKeyNames="ResourceCode,sprice,CorpId" runat="server">
<EmptyDataTemplate>
                                                    <table id="tbEmpty" class="gvdata" width="100%">
                                                        <tr class="header">
                                                            <th scope="col" style="width: 20px">
                                                                <input type="checkbox" />
                                                            </th>
                                                            <th scope="col" style="width: 30px;">
                                                                序号
                                                            </th>
                                                            <th scope="col" style="width: 20px">
                                                                物资编号
                                                            </th>
                                                            <th scope="col" style="width: 100px">
                                                                物资名称
                                                            </th>
                                                            <th scope="col" style="width: 150px">
                                                                规格单位
                                                            </th>
                                                            <th scope="col" style="width: 100px;">
                                                                单价
                                                            </th>
                                                            <th scope="col">
                                                                数量
                                                            </th>
                                                            <th scope="col">
                                                                小计
                                                            </th>
                                                            <th scope="col">
                                                                供应商
                                                            </th>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                                            <asp:CheckBox ID="chkSelectAll" runat="server" />
                                                        </HeaderTemplate><ItemTemplate>
                                                            <asp:CheckBox ID="chkSelectSingle" runat="server" />
                                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                                            <%# (this.AspNetPager1.CurrentPageIndex - 1) * NBasePage.pagesize + Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="物资编号" DataField="ResourceCode" /><asp:TemplateField HeaderText="物资名称" HeaderStyle-Width="200px"><ItemTemplate>
                                                                <span class="tooltip" title='<%# Eval("ResourceName").ToString() %>'>
                                                                <%# StringUtility.GetStr(Eval("ResourceName").ToString(), 10) %>
                                                            </span>
                                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格"><ItemTemplate>
                                                                <span class="tooltip" title='<%# Eval("Specification").ToString() %>'>
                                                                <%# StringUtility.GetStr(Eval("Specification").ToString(), 10) %>
                                                            </span>
                                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="型号"><ItemTemplate>
                                                            <span class="tooltip" title='<%# Eval("ModelNumber").ToString() %>'>
                                                                <%# StringUtility.GetStr(Eval("ModelNumber").ToString(), 10) %>
                                                            </span>
                                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="品牌"><ItemTemplate>
                                                            <span class="tooltip" title='<%# Eval("brand").ToString() %>'>
                                                                <%# StringUtility.GetStr(Eval("brand").ToString(), 10) %>
                                                            </span>
                                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="技术参数"><ItemTemplate>
                                                            <span class="tooltip" title='<%# Eval("TechnicalParameter").ToString() %>'>
                                                                <%# StringUtility.GetStr(Eval("TechnicalParameter").ToString(), 10) %>
                                                            </span>
                                                        </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="单位" DataField="UnitName" /><asp:BoundField HeaderText="单价" DataField="sprice" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:F3}" /><asp:BoundField HeaderText="数量" DataField="snumber" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:F3}" /><asp:TemplateField HeaderText="小计">
<ItemTemplate>
                                                            <%# (decimal.Parse(Eval("sprice").ToString()) * decimal.Parse(Eval("snumber").ToString())).ToString("F3") %>
                                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="供应商"><ItemTemplate>
                                                                <span class="tooltip" title='<%# Eval("CorpName").ToString() %>'>
                                                                <%# StringUtility.GetStr(Eval("CorpName").ToString(), 10) %>
                                                            </span>
                                                        </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                            <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                                            </webdiyer:AspNetPager>
                                            <asp:HiddenField ID="hfSelectValue" runat="server" />
                                            <asp:HiddenField ID="hfTcode" runat="server" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfldTcode" runat="server" />
    <asp:HiddenField ID="hfldTcodeText" runat="server" />
    <asp:HiddenField ID="hfldResourceCodes" runat="server" />
    </form>
</body>
</html>
