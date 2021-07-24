<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="SelectResource.aspx.cs" Inherits="StockManage_UserControl_SelectResource" MaintainScrollPositionOnPostBack="true" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>资源选择</title><link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/icon.css" />
<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // 添加验证
            $('#btnSelect')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
            $('#btnBindSelectedResource').hide();
            //上面的GVW
            var resourceTable = new JustWinTable('gvwResource');
            replaceEmptyTable('emptyResource', 'gvwResource');
            showTooltip('tooltip', 20);
            //            resourceTable.registClickTrListener(function () {
            //                var $tr = $('<TR id=' + this.id + ' code=' + $(this).attr('code') + '>' + this.innerHTML + '</TR>');
            //                $('#emptySelectedResource tr:gt(0)').remove();
            //                $('#emptySelectedResource').append($tr);
            //                $('#hfldResource').val($('#emptySelectedResource').html());
            //                $('#btnSave').removeAttr('disabled');

            //            });

            //下一页的初始化
            if ($('#hfldshow').val() == '0') {
                if ($('#hfldResource').val()) {
                    $('#emptySelectedResource').html($('#hfldResource').val());
                    $('#gvwResource tr:gt(0)').each(function () {
                        if ($('#hfldResource').val().indexOf(this.id) >= 0) {
                            $(this).find('input[type=checkbox]').attr('checked', true);
                        }
                    });
                }
                isSaveDisabled();
            }
            //保存按钮是否启用     wdd updata 2014/03/27
            function isSaveDisabled() {
                $('#hfldResource').val($('#emptySelectedResource').html());
                if ($('#emptySelectedResource tr:gt(0)').length > 0) {
                    $('#btnSave').attr('disabled', false);
                }
                else {
                    $('#btnSave').attr('disabled', true);
                }
            }

            resourceTable.registClickSingleCHKListener(function () {
                var resourceId = this.parentNode.parentNode.id;
                var code = $(this.parentNode.parentNode).attr('code'); //加code
                if (this.checked && $('#emptySelectedResource').html().indexOf(resourceId) < 0) {
                    var $tr = $('<TR id=' + resourceId + ' code=' + code + '>' + this.parentNode.parentNode.innerHTML + '</TR>');
                    $('#emptySelectedResource').append($tr);
                } else if (!this.checked && $('#emptySelectedResource').html().indexOf(resourceId) >= 0) {
                    $('#emptySelectedResource tr[id=' + resourceId + ']').remove();
                }
                isSaveDisabled();
            });

            // wdd updata 2014/03/27
            resourceTable.registClickAllCHKLitener(function () {
                if (this.checked) {
                    $('#gvwResource tr:gt(0) [type=checkbox]').each(function () {
                        var resourceId = this.parentNode.parentNode.id;
                        var code = this.parentNode.parentNode.code;
                        var $tr = $('<TR id=' + resourceId + ' code=' + code + '>' + this.parentNode.parentNode.innerHTML + '</TR>');
                        if ($('#emptySelectedResource').html().indexOf(resourceId) < 0) {
                            $('#emptySelectedResource').append($tr);
                        }
                    });
                } else {
                    $('#gvwResource tr:gt(0) [type=checkbox]').each(function () {
                        var resourceId = this.parentNode.parentNode.id;
                        var code = this.parentNode.parentNode.code;
                        if ($('#emptySelectedResource').html().indexOf(resourceId) >= 0) {
                            $('#emptySelectedResource tr[id=' + resourceId + ']').remove();
                        }
                    });
                }
                isSaveDisabled();
            });

            if ($('#hfldResource').val()) {
                $('#emptySelectedResource').html($('#hfldResource').val());
                $('#gvwResource tr:gt(0)').each(function () {
                    if ($('#hfldResource').val().indexOf(this.id) >= 0) {
                        $(this).find('input[type=checkbox]').attr('checked', true);
                    }
                });
            }

            var resourceTable = new JustWinTable('gvwSelectedResource');
        });


        $(function () {
            $("#gvwResource tr>td>a").hover(
          function () {
              $(this).addClass("hover");
          },
          function () {
              $(this).removeClass("hover");
          });

        });

        // 保存
        function saveEvent() {
            var idArr = new Array();
            var codeArr = new Array();
            $('#emptySelectedResource tr:gt(0)').each(function () {
                idArr.push(this.id);
                codeArr.push($(this).attr('code'));
            });
            var resObj = {};
            resObj.id = array1dToJson(idArr);
            resObj.code = array1dToJson(codeArr);
            if (getRequestParam('type') == '2') {
                // 新界面
                if (typeof top.ui.callback == 'function') {
                    top.ui.callback(resObj);
                    top.ui.callback = null;
                }
                top.ui.closeWin();
            } else {
                // 老界面的代码，以后会删除掉， 等没有页面再使用 /UserControl/SelectResource.ascx 这个时，可以删除下面代码
                $(parent.document).find('.ui-icon-closethick').click();
                $(parent.document).find('input[id$=hfldResourceId]').val(resObj.id);
                $(parent.document).find('input[id$=hfldResourceCode]').val(resObj.code);
                $(parent.document).find('input[id$=hfldppcode]').val('');
                if (getRequestParam('ButtonId') != '0') {
                    $(parent.document).find('#' + getRequestParam('ButtonId')).click();
                }
            }
        }

        // 取消
        function calcelEvent() {
            if (getRequestParam('type') == '2') {
                top.ui.closeWin();
            } else {
                $(parent.document).find('.ui-icon-closethick').click();
            }
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
            编码:&nbsp;&nbsp;<asp:TextBox ID="txtCode" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
            名称:&nbsp;&nbsp;<asp:TextBox ID="txtName" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
            <asp:Button ID="btnSelect" Text="查询" OnClick="btnSelect_Click" runat="server" />
        </div>
    </div>
    <hr />
    <div style="width: 100%;">
        <div style="width: 150px; height: 400px; overflow: auto; float: left; border-right: solid 1px #B5CCDE;">
            <asp:TreeView ID="trvwResourceType" ExpandDepth="1" ShowLines="true" OnSelectedNodeChanged="trvwResourceType_SelectedNodeChanged" OnTreeNodePopulate="trvwResourceType_TreeNodePopulate" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
        </div>
        <div style="width: 810px; height: 150px; overflow: auto; float: left;">
            <asp:GridView ID="gvwResource" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwResource_RowDataBound" DataKeyNames="ResourceId,ResourceCode" runat="server">
<EmptyDataTemplate>
                    <table id="emptyResource" class="gvdata">
                        <tr class="header">
                            <th scope="col" style="width: 20px;">
                                <input type="checkbox" />
                            </th>
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
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                            <asp:CheckBox ID="chkAll" runat="server" />
                        </HeaderTemplate><ItemTemplate>
                            <asp:CheckBox ID="chkSingle" runat="server" />
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                            <%# (this.AspNetPager1.CurrentPageIndex - 1) * 15 + Container.DataItemIndex + 1 %>
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
        </div>
        <div style="width: 810px; height: 30px; overflow: auto; float: left;">
            <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" ShowPageIndexBox="Always" NumericButtonCount="8" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
            </webdiyer:AspNetPager>
        </div>
        <div style="width: 810px; height: 190px; overflow: scroll; float: left;">
            <table id="emptySelectedResource" class="gvdata" cellspacing="0" rules="all" border="1"
                style="border-collapse: collapse;">
                <tr class="header">
                    <th scope="col" style="width: 20px;">
                        <input id="gvwResource__ctl1_chkAll" type="checkbox" name="gvwResource:_ctl1:chkAll" />
                    </th>
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
            <asp:GridView ID="gvwSelectedResource" Visible="false" AutoGenerateColumns="false" CssClass="gvdata" DataKeyNames="ResourceId,ResourceCode" runat="server">
<EmptyDataTemplate>
                </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                            <asp:CheckBox ID="chkAll" runat="server" />
                        </HeaderTemplate><ItemTemplate>
                            <asp:CheckBox ID="chkSingle" runat="server" />
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                            <%# (this.AspNetPager1.CurrentPageIndex - 1) * NBasePage.pagesize3 + Container.DataItemIndex + 1 %>
                        </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="编码" DataField="ResourceCode" /><asp:TemplateField HeaderText="名称"><ItemTemplate>
                            
                            <asp:HyperLink ID="hlinkShowResourceName" Style="text-decoration: none; color: Black;" ToolTip='<%# System.Convert.ToString(Eval("ResourceName").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"><%# StringUtility.GetStr(Eval("ResourceName").ToString(), 20) %></asp:HyperLink>
                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格"><ItemTemplate>
                            
                            <asp:HyperLink ID="hlinkShowSpecification" Style="text-decoration: none; color: Black;" ToolTip='<%# System.Convert.ToString(Eval("Specification").ToString(), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"><%# StringUtility.GetStr(Eval("Specification").ToString(), 20) %></asp:HyperLink>
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
        </div>
        <div style="width: 810px; height: 25px; float: left; text-align: right; vertical-align: middle">
            <br />
            <input type="button" id="btnSave" disabled="disabled" value="保存" onclick="saveEvent();" />
            <input type="button" id="btnCancel" value="取消" onclick="calcelEvent();" />
        </div>
    </div>
    <asp:HiddenField ID="hfldResource" runat="server" />
    <asp:HiddenField ID="hfldConsType" runat="server" />
    <asp:HiddenField ID="hfldshow" runat="server" />
    </form>
</body>
</html>
