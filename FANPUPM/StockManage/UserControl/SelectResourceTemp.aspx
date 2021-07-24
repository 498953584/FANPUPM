<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="SelectResourceTemp.aspx.cs" Inherits="StockManage_UserControl_SelectResourceTemp" MaintainScrollPositionOnPostBack="true" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>资源选择</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/json2.js"></script>
    <script type="text/javascript" src="../script/Config2.js"></script>
    <script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/jwJson.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // type == '2' 表示使用top.ui.openWin 打开
            var type = getRequestParam('type');

            $('#btnBindSelectedResource').hide();
            showTooltip('tooltip', 20);
            //上面的GVW
            replaceEmptyTable('emptyResource', 'gvwResource');
            var resourceTable = new JustWinTable('gvwResource');

            resourceTable.registClickTrListener(function () {
                var $tr = $('<TR id=' + this.id + ' code=' + this.code + '>' + this.innerHTML + '</TR>');
                $('#btnSave').removeAttr('disabled');
                $('#hfldResource').val(this.id);
                $('#hfldResourceName').val(this.getAttribute('name'));
            });

            resourceTable.registDbClickListener(function () {
                success();
            });

            if ($('#hfldResource').val()) {
                $('#emptySelectedResource').html($('#hfldResource').val());
                $('#gvwResource tr:gt(0)').each(function () {
                    if ($('#hfldResource').val().indexOf(this.id) >= 0) {
                        $(this).find('input[type=checkbox]').attr('checked', true);
                    }
                });
            }

            $('#btnCancel').click(function () {
                if (type == '2') {
                    var winNo = jw.getRequestParam('winNo');
                    top.ui.closeWin({ winNo: winNo });
                } else {
                    $(parent.document).find('.ui-icon-closethick').each(function () {
                        this.click();
                    });
                }
            });
        });

        function success() {
            var type = getRequestParam('type');
            if (type == '2') {
                // 使用top.ui.openWin 打开
                var winNo = jw.getRequestParam('winNo');
                if (winNo == '2') {
                    if (top.ui.callback != null) {
                        var id = $('#hfldResource').val();
                        var name = $('#hfldResourceName').val();
                        top.ui.callback(id);
                    }
                    top.ui.closeWin({ winNo: winNo });
                }
                else {
                    if (typeof top.ui.callback == 'function') {
                        var id = $('#hfldResource').val();
                        var name = $('#hfldResourceName').val();
                        top.ui.callback(id,name); // wdd 2014/04/29
                        top.ui.callback = null;
                    }
                    top.ui.closeWin();
                }
            } else {
                try {
                    $(parent.document).find('.ui-icon-closethick').each(function () {
                        this.click();
                    });

                    // 用新的回调方法，终止下面代码的执行
                    if (execCallback() == 1)
                        return;

                    $(parent.document).find('input[id$=hfldResourceId]').val(document.getElementById('hfldResource').value);
                    $(parent.document).find('input[id$=hfldType]').val('resource');
                    if (getRequestParam('ButtonId') != '0') {
                        parent.document.getElementById(getRequestParam('ButtonId')).click();
                    }
                } catch (ex) {
                }
            }
        }

        // 执行回调方法
        function execCallback() {
            var method = getRequestParam('callback');
            var id = document.getElementById('hfldResource').value;
            var name = document.getElementById('hfldResourceName').value;

            var obj = {};
            obj.id = id;
            obj.name = name;

            if (!!method) {
                parent[method](JSON.stringify(obj));
                return 1;
            }
            return 0;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%; height: 25px;">
        <div style="width: 150px; float: left">
            <asp:DropDownList ID="dropResourceType" DataTextField="ResourceTypeName" DataValueField="ResourceTypeCode" AutoPostBack="true" OnSelectedIndexChanged="dropResourceType_SelectedIndexChanged" runat="server"></asp:DropDownList>
        </div>
        <div style="width: 400px; float: right">
            编码:&nbsp;&nbsp;<asp:TextBox ID="txtCode" Width="120px" runat="server"></asp:TextBox>
            名称:&nbsp;&nbsp;<asp:TextBox ID="txtName" Width="120px" runat="server"></asp:TextBox>
            <asp:Button ID="btnSelect" Text="查询" OnClick="btnSelect_Click" runat="server" />
        </div>
    </div>
    <hr />
    <div style="width: 100%;">
        <div style="width: 150px; height: 420px; overflow: auto; float: left; border-right: solid 1px #B5CCDE;">
            <asp:TreeView ID="trvwResourceType" ExpandDepth="1" ShowLines="true" OnSelectedNodeChanged="trvwResourceType_SelectedNodeChanged" OnTreeNodePopulate="trvwResourceType_TreeNodePopulate" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
        </div>
        <div style="width: 810px; height: 373px; overflow: auto; float: left;">
            <asp:GridView ID="gvwResource" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwResource_RowDataBound" DataKeyNames="ResourceId,ResourceCode,ResourceName" runat="server">
<EmptyDataTemplate>
                    <table id="emptyResource" style="width: 100%; margin-left: 0px; margin-right: 0px;
                        padding: 0;">
                        <tr class="header" style="width: 100%;">
                            <th scope="col" style="width: 25px;">
                                序号
                            </th>
                            <th scope="col">
                                编码
                            </th>
                            <th scope="col">
                                名称
                            </th>
                            <th scope="col">
                                规格
                            </th>
                            <th scope="col">
                                型号
                            </th>
                            <th scope="col">
                                单位
                            </th>
                            <th scope="col">
                                单价
                            </th>
                            <th scope="col">
                                品牌
                            </th>
                            <th scope="col">
                                技术参数
                            </th>
                            <th scope="col">
                                供应商
                            </th>
                        </tr>
                    </table>
                </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                            <%# (this.AspNetPager1.CurrentPageIndex - 1) * NBasePage.pagesize3 + Container.DataItemIndex + 1 %>
                        </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="编码" DataField="ResourceCode" /><asp:TemplateField HeaderText="名称"><ItemTemplate>
                            <span class="tooltip" title='<%# Eval("ResourceName").ToString() %>'>
                                <%# StringUtility.GetStr(Eval("ResourceName").ToString(), 20) %>
                            </span>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格"><ItemTemplate>
                            <span class="tooltip" title='<%# Eval("Specification").ToString() %>'>
                                <%# StringUtility.GetStr(Eval("Specification").ToString(), 20) %>
                            </span>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="型号"><ItemTemplate>
                            <span class="tooltip" title='<%# Eval("ModelNumber").ToString() %>'>
                                <%# StringUtility.GetStr(Eval("ModelNumber").ToString(), 20) %>
                            </span>
                        </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="单位" DataField="Name" /><asp:TemplateField HeaderText="单价" HeaderStyle-Width="50px">
<ItemTemplate>
                            <%# System.Convert.ToDecimal(Eval("price")).ToString("0.000") %>
                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="品牌">
<ItemTemplate>
                            <span class="tooltip" title='<%# Eval("Brand").ToString() %>'>
                                <%# StringUtility.GetStr(Eval("Brand").ToString(), 20) %>
                            </span>
                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="技术参数">
<ItemTemplate>
                            <span class="tooltip" title='<%# Eval("TechnicalParameter").ToString() %>'>
                                <%# StringUtility.GetStr(Eval("TechnicalParameter").ToString(), 20) %>
                            </span>
                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="供应商">
<ItemTemplate>
                            <span class="tooltip" title='<%# Eval("CorpName").ToString() %>'>
                                <%# StringUtility.GetStr(Eval("CorpName").ToString(), 20) %>
                            </span>
                        </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
            <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" ShowPageIndexBox="Always" NumericButtonCount="8" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
            </webdiyer:AspNetPager>
        </div>
        <div style="width: 810px; height: 25px; float: left; text-align: right; vertical-align: middle">
            <br />
            <input type="button" id="btnSave" disabled="disabled" value="保存" onclick="success();" />
            <input type="button" id="btnCancel" value="取消" />
        </div>
    </div>
    <asp:HiddenField ID="hfldResource" runat="server" />
    <asp:HiddenField ID="hfldType" runat="server" />
    <asp:HiddenField ID="hfldResourceName" runat="server" />
    </form>
</body>
</html>
