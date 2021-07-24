<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExcelImportContractSpecial.aspx.cs" Inherits="BudgetManage_Budget_ExcelImportContractSpecial" %>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>目标成本预算导入（特别版）</title>

    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>

    <script type="text/jscript" src="../../StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript" src="../../StockManage/script/ValidateInput.js"></script>

    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>

    <script type="text/javascript" src="../../Script/Budget/BudgetPait.js"></script>

    <script type="text/javascript" src="../../Script/jquery.blockUI.js"></script>

    <script type="text/javascript" src="../../Script/jquery-budgetpait.js"></script>

    <link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />


    <script type="text/javascript" src="/Script/jquery.cookie.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            //多选WBS
            var gvBudget = new JustWinTable('gvwWBSData');
            setButton2(gvBudget, 'hfldWBSChecked');
            //多选Res
            var gvBudget = new JustWinTable('gvwResData');
            setButton2(gvBudget, 'hfldResChecked');
            setWidthHeight();
            $('#btnSelectResType').bind('click', openDialog);
        });
        function setWidthHeight() {
            $('#divWBS').width($(this).width() - 7);
            $('#divWBS').height($(this).height() - 50);
            $('#divRes').width($(this).width() - 7);
            $('#divRes').height($(this).height() - 50);
            $('#divError').width($(this).width() - 7);
            $('#divError').height($(this).height() - 50);
        }
        $(document).ready(function() {
            $('#gvwWBSData select').bind('change', function() {
                var $cSelect = $(this);
                var $cOption = $cSelect.find('option:selected')
                $('#gvwWBSData select').each(function() {
                    if ($(this).attr('id') != $cSelect.attr('id')) {
                        if ($(this).find('option:selected').val() == $cOption.val()) {
                            if ($cOption.val() != 'Invalid')
                                $(this).find('option')[0].selected = true;
                        }
                    }
                });
                getSelectValue('gvwWBSData select', 'hfldExcelColumns');
            });

            $('#gvwResData select').bind('change', function() {
                var $cSelect2 = $(this);
                var $cOption2 = $cSelect2.find('option:selected')
                $('#gvwResData select').each(function() {
                    if ($(this).attr('id') != $cSelect2.attr('id')) {
                        if ($(this).find('option:selected').val() == $cOption2.val()) {
                            if ($cOption2.val() != 'Invalid')
                                $(this).find('option')[0].selected = true;
                        }
                    }
                });
                getSelectValue('gvwResData select', 'hfldResource');
            });
            //导入数据
            $('#btmImport').click(function() {
                if ($('#gvwResData') != null) {
                    setTem(1);
                    return confirm("系统提示：\n资源信息中的资源未添加到资源库，资源无法配置到任务节点！\n是否继续导入？");
                }
            });
        });
        //获取select 值
        function getSelectValue(selectType, hfldId) {
            var columns = '';
            $('#' + selectType).each(function() {
                columns = columns + $(this).find('option:selected').val() + ',';
            });
            columns = columns.substring(0, columns.length - 1);
            $('#' + hfldId).val(columns);
            return columns;
        }
        //设置模板选项卡
        function setTem(num) {
            $('#hfldCurrentTabIndex').val(num);
            if (num == '0') {
                //WBS信息
                $('#SpWBS').attr('class', 'temShow');
                $('#SpResource').attr('class', 'temHide');
                $('#SpError').attr('class', 'temHide');
                $('#WBSData').show();
                $('#ErrorData').hide();
                $('#ResData').hide();
            } else if (num == '1') {
                //资源信息
                $('#SpResource').attr('class', 'temShow');
                $('#SpWBS').attr('class', 'temHide');
                $('#SpError').attr('class', 'temHide');
                $('#ResData').show();
                $('#ErrorData').hide();
                $('#WBSData').hide();
            } else if (num == '2') {
                //警告信息
                $('#SpError').attr('class', 'temShow');
                $('#SpWBS').attr('class', 'temHide');
                $('#SpResource').attr('class', 'temHide');
                $('#ErrorData').show();
                $('#WBSData').hide();
                $('#ResData').hide();
            }
        }
        function setButton2(jwTable, hdChkId) {
            if (!jwTable.table) return;
            if (jwTable.table.firstChild.childNodes.length == 1) {
                //table中没有数据
                return;
            }
            jwTable.registClickTrListener(function() {
                if (hdChkId != '') {
                    document.getElementById(hdChkId).value = this.id;
                }
                if (document.getElementById(hdChkId).value != '') {
                    document.getElementById('btnDel').removeAttribute('disabled');
                    disabledBtnAddRes(false);
                }
            });
            jwTable.registClickSingleCHKListener(function() {
                var checkedChk = jwTable.getCheckedChk();
                if (checkedChk.length == 0) {
                    if (hdChkId != '') {
                        document.getElementById(hdChkId).value = "";
                    }
                    document.getElementById('btnDel').setAttribute('disabled', 'disabled');
                    disabledBtnAddRes(true);
                }
                else {
                    var checkedChks = jwTable.getCheckedChk();
                    if (hdChkId != '') {
                        var taskId = jwTable.getCheckedChkIdJson(checkedChk);
                        document.getElementById(hdChkId).value = taskId;
                    }
                    document.getElementById('btnDel').removeAttribute('disabled');
                    disabledBtnAddRes(false);
                }
            });
            jwTable.registClickAllCHKLitener(function() {
                if (this.checked) {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson();
                    var checkedChks = jwTable.getAllChk();
                    document.getElementById('btnDel').removeAttribute('disabled');
                    disabledBtnAddRes(false);
                }
                else {
                    document.getElementById('btnDel').setAttribute('disabled', 'disabled');
                    disabledBtnAddRes(true);
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = '';
                }
            });
        }
        //disabled true： 禁用  false ：启用
        function disabledBtnAddRes(disabled) {
            if ($('#SpResource').attr('class') == 'temShow') {
                if (disabled)
                    $('#btnSelectResType').attr('disabled', 'disabled');
                else
                    $('#btnSelectResType').removeAttr('disabled');
            }
        }
        function openDialog() {
            $('#divFramSeleResType').dialog({
                open: function() { $(this).parent().appendTo("form:first"); },
                title: '选择资源类型',
                width: 600,
                height: 300,
                modal: true
            });
        }
        
    </script>

    <style type="text/css">
        .temShow
        {
            height: 23px;
            width: 84px;
            border: 0px solid #aa0000;
            font-size: 12px;
            text-align: center;
            float: left;
            line-height: 25px;
            cursor: pointer;
            background-image: url('/images1/Res1.jpg');
            margin-left: 2px;
        }
        .temHide
        {
            height: 23px;
            width: 84px;
            border: 0px solid #aa0000;
            font-size: 12px;
            text-align: center;
            float: left;
            line-height: 25px;
            cursor: pointer;
            background-image: url('/images1/Res2.jpg');
            margin-left: 2px;
        }
        .tabdivgv
        {
            height: auto;
            border: solid 1px #d4d4d4;
            padding: 2px 2px 0px 2px;
        }
        td
        {
            cursor: default;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="0px" cellspacing="0px" style="width: 100%; vertical-align: top;">
        <tr>
            <td class="divFooter" style="text-align: left;">
                <asp:FileUpload ID="fupExcel" BackColor="#FEFEF4" Height="20px" Width="300px" runat="server" />
                <asp:Button ID="btnConnectExcel" Style="width: 75px; cursor: pointer;" Text="连接数据" OnClientClick="setTem('0')" OnClick="btnConnectExcel_Click" runat="server" />
                <asp:Button ID="btmImport" Text="导入数据" Style="width: 75px; cursor: pointer;" Enabled="false" OnClick="btnImport_Click" runat="server" />
                <asp:Button ID="btnDel" OnClientClick="return confirm('您确定要删除吗?');" Text="删除" disabled="disabled" Style="cursor: pointer;" OnClick="btnDel_Click" runat="server" />
            </td>
        </tr>
        <tr style="vertical-align: top; height: 70%;">
            <td>
                <div style="height: 22px; line-height: 22px;">
                    <span id="SpWBS" onclick="setTem('0')" class="temShow" style="margin-left: 0px;" runat="server">
                        WBS信息</span> <span id="SpResource" onclick="setTem('1')" class="temHide" runat="server">
                            资源信息</span> <span id="SpError" onclick="setTem('2')" class="temHide" runat="server">
                                警告信息 </span>
                </div>
                <div id="WBSData" class="tabdivgv">
                    <div id="divWBS" style="overflow: auto;">
                        <asp:GridView ID="gvwWBSData" CssClass="gvdata" OnRowDataBound="gvwWBSData_RowDataBound" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                        <asp:CheckBox ID="cbAllBox" runat="server" />
                                    </HeaderTemplate><ItemTemplate>
                                        <asp:CheckBox ID="cbBox" runat="server" />
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                                        <%# (this.AspNetPagerWBS.CurrentPageIndex - 1) * this.AspNetPagerWBS.PageSize + Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
</asp:TemplateField></Columns></asp:GridView>
                        <div style="float: left;">
                            <webdiyer:AspNetPager ID="AspNetPagerWBS" Width="100%" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPagerWBS_PageChanged" runat="server">
                            </webdiyer:AspNetPager>
                        </div>
                    </div>
                </div>
                <div id="ResData" class="tabdivgv" style="display: none;">
                    <div id="divRes" style="overflow: auto;">
                        <input type="button" id="btnSelectResType" value="添加到资源库" style="width: auto;" disabled="disabled" />
                        <span style="color: Red;">以下资源在资源库中不存在，请手动添加到资源库！如果删除或未添加到资源库，资源无法配置到任务节点！ </span>
                        <asp:GridView ID="gvwResData" CssClass="gvdata" OnRowDataBound="gvwResData_RowDataBound" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                        <asp:CheckBox ID="cbAllBox" runat="server" />
                                    </HeaderTemplate><ItemTemplate>
                                        <asp:CheckBox ID="cbBox" runat="server" />
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                                        <%# (this.AspNetPagerRes.CurrentPageIndex - 1) * this.AspNetPagerRes.PageSize + Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
</asp:TemplateField></Columns></asp:GridView>
                        <div style="float: left;">
                            <webdiyer:AspNetPager ID="AspNetPagerRes" UrlPaging="false" Width="100%" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPagerRes_PageChanged" runat="server">
                            </webdiyer:AspNetPager>
                        </div>
                    </div>
                </div>
                <div id="ErrorData" class="tabdivgv" style="display: none;">
                    <div id="divError" style="overflow: auto;">
                        <asp:GridView ID="gvwError" CssClass="gvdata" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                                        <%# (this.AspNetPagerError.CurrentPageIndex - 1) * this.AspNetPagerError.PageSize + Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
</asp:TemplateField></Columns></asp:GridView>
                        <div style="float: left;">
                            <webdiyer:AspNetPager ID="AspNetPagerError" Style="float: left;" Width="100%" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPagerError_PageChanged" runat="server">
                            </webdiyer:AspNetPager>
                        </div>
                        <div style="clear: left;">
                            <asp:Button ID="btnClose" Text="关闭" Visible="false" OnClick="btnClose_Click" runat="server" />
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <div id="divFramSeleResType" style="display: none;">
        <iframe id="ifSelResType" title="" src="../Resource/SelectResType.aspx" frameborder="0"
            width="100%" height="100%"></iframe>
        
        <asp:Button ID="btnAddRes" Style="display: none;" Text="选择资源类型后保存到数据库" OnClick="btnAddRes_Click" runat="server" />
    </div>
    <asp:HiddenField ID="hfldExcelName" runat="server" />
    <asp:HiddenField ID="hfldExcelColumns" runat="server" />
    <asp:HiddenField ID="hfldResource" runat="server" />
    <asp:HiddenField ID="hfldCurrentTabIndex" runat="server" />
    <asp:HiddenField ID="hfldValidRes" runat="server" />
    
    <asp:HiddenField ID="hfldWBSChecked" runat="server" />
    <asp:HiddenField ID="hfldResChecked" runat="server" />
    
    <asp:HiddenField ID="hfldSelResType" runat="server" />
    </form>
</body>
</html>
