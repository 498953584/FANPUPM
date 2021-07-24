<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MultiSelectEquType.aspx.cs" Inherits="Equ_Equipment_MultiSelectEquType" %>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>设备类别设置</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('gvwEquipmentType');
            replaceEmptyTable('emptyEquipmentType', 'gvwEquipmentType')
            setButton(jwTable, '', '', '', 'hfldId');
            //禁用/启用 保存按钮
            jwTable.registClickTrListener(function () {
                $('#btnSave').removeAttr('disabled');
            });
            jwTable.registClickSingleCHKListener(function () {
                var checkedChk = jwTable.getCheckedChk();
                if (checkedChk.length == 0)
                    $('#btnSave').attr('disabled', 'disabled');
                else
                    $('#btnSave').removeAttr('disabled');
            });
            jwTable.registClickAllCHKLitener(function () {
                if (this.checked)
                    $('#btnSave').removeAttr('disabled');
                else
                    $('#btnSave').attr('disabled', 'disabled');
            });
            //取消
            $('#btnCancel').click(function () {
                top.ui.closeWin();
            });
            //保存
            $('#btnSave').click(function () {
                var idsChecked;
                if ($('#hfldId').val().indexOf('[') < 0) {
                    idsChecked = '["' + $('#hfldId').val() + '"]';
                } else {
                    idsChecked = $('#hfldId').val();
                }
                top.ui.callback(idsChecked);
                top.ui.callback = null;
                top.ui.closeWin();
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%; height: 89%;">
            <tr>
                <td style="width: 150px; vertical-align: top; height: 420px;">
                    <div class="pagediv" style="width: 150px; height: 100%;" id="div1" runat="server">
                        <asp:TreeView ID="trvwEquipmentType" ShowLines="true" ExpandDepth="2" OnSelectedNodeChanged="TreeView_SelectedNodeChanged" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
                    </div>
                </td>
                <td style="width: 100%; vertical-align: top; border-left: solid 2px #CADEED;">
                    <table border="0" class="tab">
                        <tr>
                            <td style="height: 100%; vertical-align: top;">
                                <div class="pagediv">
                                    <asp:GridView ID="gvwEquipmentType" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwEquipmentType_RowDataBound" DataKeyNames="Id" runat="server">
<EmptyDataTemplate>
                                            <table id="emptyEquipmentType" style="width: 100%; margin-left: 0px; margin-right: 0px;
                                                padding: 0;">
                                                <tr class="header" style="width: 100%;">
                                                    <th style="width: 20px;">
                                                        <asp:CheckBox runat="server" />
                                                    </th>
                                                    <th scope="col" style="width: 25px;">
                                                        设备序号
                                                    </th>
                                                    <th scope="col">
                                                        设备名称
                                                    </th>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                                    <asp:CheckBox ID="cbAllBox" runat="server" />
                                                </HeaderTemplate><ItemTemplate>
                                                    <asp:CheckBox ID="chkBox" runat="server" />
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="设备序号"><ItemTemplate>
                                                    
                                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="设备名称"><ItemTemplate>
                                                    
                                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                    <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                                    </webdiyer:AspNetPager>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div style="width: 98%; height: 25px; float: left; text-align: right; vertical-align: middle">
        <input type="button" id="btnSave" disabled="disabled" value="保存" />
        <input type="button" id="btnCancel" value="取消" />
    </div>
    
    <asp:HiddenField ID="hfldId" runat="server" />
    </form>
</body>
</html>
