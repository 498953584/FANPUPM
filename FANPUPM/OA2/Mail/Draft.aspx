<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Draft.aspx.cs" Inherits="OA2_Mail_Draft" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>草稿箱</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <!-- jQuery.EasyUI-->
    <link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <!-- jQuery.EasyUI-->
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript" src="/Script/wf.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            var gvEmail = new JustWinTable('gvEmail');
            replaceEmptyTable('gvEmailEmpty', 'gvEmail');
            setButton(gvEmail, 'btnDelete', 'btnUpdate', '', 'hfldEmailID');
            addEvent($('#btnUpdate')[0], 'click', UpdateMail);
            displayLookAdjuncts();
            showTooltip('tooltip', 50);
            showTooltip('tool', 10);


            //按钮的点击后的样式
            function funonclickCss(name) {
                parent.document.getElementById(name).style.background = 'url(/images1/email/over.gif) repeat-x';
                parent.document.getElementById(name).style.fontWeight = 'bold';
                parent.document.getElementById(name).style.color = '#004592';
            }

            //按钮的点击前的样式
            function funfrontCss(name) {
                parent.document.getElementById(name).style.background = 'url(/images1/email/understood.gif) repeat-x';
                parent.document.getElementById(name).style.fontWeight = 'normal';
                parent.document.getElementById(name).style.color = '#333';
            }

            funfrontCss('spanwrite_mail');
            funfrontCss('write_mail');
            funfrontCss('spaninbox');
            funfrontCss('inbox');
            funonclickCss('spandraft');
            funonclickCss('draft');
            funfrontCss('spanrepeal');
            funfrontCss('repeal');
            funfrontCss('spanoutbox');
            funfrontCss('outbox');
            funfrontCss('spandeleted');
            funfrontCss('deleted');


            var btnMove = document.getElementById('btnMove');
            gvEmail.registClickTrListener(function () {
                document.getElementById('hfldCheckedIds').value = this.id;
                if (this.id != '') {
                    btnMove.removeAttribute('disabled');
                }
                else {
                    btnMove.setAttribute('disabled', 'disabled');
                }
            });

            gvEmail.registClickSingleCHKListener(function () {
                var checkedChk = gvEmail.getCheckedChk();
                if (checkedChk.length == 1) {
                    var tr1 = getFirstParentElement(checkedChk[0].parentNode, 'TR');
                    btnMove.removeAttribute('disabled');
                    document.getElementById('hfldCheckedIds').value = tr1.id;
                }
                else if (checkedChk.length > 1) {
                    var ids = "";
                    for (var i = 0; i < checkedChk.length; i++) {
                        var trs = getFirstParentElement(checkedChk[i].parentNode, 'TR');
                        ids += trs.id + ",";

                    }
                    document.getElementById('hfldCheckedIds').value = ids;
                    btnMove.removeAttribute('disabled');
                }
                else {
                    btnMove.setAttribute('disabled', 'disabled');
                }
            });

            gvEmail.registClickAllCHKLitener(function () {
                if (gvEmail.isCheckedAll()) {
                    btnMove.removeAttribute('disabled');
                    var checkedChk = gvEmail.getAllChk();
                    var ids = "";

                    for (var i = 0; i < checkedChk.length; i++) {
                        var trs = getFirstParentElement(checkedChk[i].parentNode, 'TR');

                        ids += trs.id + ",";
                    }
                    document.getElementById('hfldCheckedIds').value = ids;
                    btnMove.removeAttribute('disabled');
                }
                else {
                    btnMove.setAttribute('disabled', 'disabled');
                }
            });
        })
        //查看附件
        function queryAdjunct(id) {
            var path = $('#hfldAdjunctPath').val() + '/' + id;
            parent.window.showFiles(path, 'divOpenAdjunct');
        }
        //附件显示
        function displayLookAdjuncts() {
            var tab = document.getElementById('gvEmail');
            if (tab.rows.length > 0) {
                for (i = 1; i < tab.rows.length; i++) {
                    var id = tab.rows[i].AnnexId;
                    var path = $('#hfldAdjunctPath').val() + '/' + id;
                    var showCount = getFilesCount(path);
                    if (showCount == 0)
                        tab.rows[i].cells[3].innerText = '';
                }
            }
        }
        function getFilesCount(path) {
            var count = 0;
            $.ajax({
                type: "GET",
                url: "/UserControl/FileUpload/GetFiles.ashx?" + new Date().getTime() + '&Path=' + path,
                async: false,
                dataType: "JSON",
                success: function (data) {
                    count = data.length;
                }
            });
            return count;
        }

        // 查看邮件内容
        function btnQueryClick(MailId) {
            parent.parent.desktop.ViewMail = window;
            var url = '/OA2/Mail/WriteMail.aspx?mailId=' + MailId + "&&edit=" + "1";
            this.location.href = url;
        }

        // 编辑按钮单击事件
        function UpdateMail() {
            parent.parent.desktop.PrjInfoAdd = window;
            var url = "/OA2/Mail/WriteMail.aspx?mailId=" + document.getElementById('hfldEmailID').value + "&&edit=" + "1";
            window.location.href = url;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="vertical-align: top; width: 99.99%; height: 100%;">
            <tr>
                <td style="vertical-align: top;">
                    <table cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;">
                        <tr>
                            <td align="left">
                                <table cellpadding="5" cellspacing="5">
                                    <tr>
                                        <td class="descTd">
                                            主题
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtName" Width="120px" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="descTd">
                                            内容
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtContent" Width="120px" runat="server"></asp:TextBox>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td class="descTd">
                                            日期
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtStartTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="descTd">
                                            至
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEndTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td id="td_buttons" class="divFooter" style="text-align: left;">
                                <asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
                                <asp:Button ID="btnDelete" Text="删 除" disabled="disabled" OnClick="btnDelete_Click" runat="server" />
                                <input type="button" id="btnUpdate" value="编辑" disabled="true" runat="server" />

                                <asp:Button ID="btnMove" Text="转移到" disabled="disabled" OnClick="btnMove_Click" runat="server" />
                                <asp:DropDownList ID="DDLtBox" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        
                        <tr>
                            <td align="center">
                                <asp:GridView ID="gvEmail" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" OnRowDataBound="gvEmail_RowDataBound" DataKeyNames="MailId,AnnexId,MailName" runat="server">
<EmptyDataTemplate>
                                        <table class="gvdata" cellspacing="0" rules="all" border="1" id="gvEmailEmpty" style="width: 100%;
                                            border-collapse: collapse;">
                                            <tr class="header">
                                                <th scope="col" style="width: 20px;">
                                                    <input id="gvEmail_ctl01_chkSelectAll" type="checkbox" />
                                                </th>
                                                <th scope="col" style="width: 150px;">
                                                    收件人
                                                </th>
                                                <th scope="col">
                                                    主题
                                                </th>
                                                <th scope="col" style="width: 25px;">
                                                    附件
                                                </th>
                                                <th scope="col" style="width: 100px;">
                                                    时间
                                                </th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
                                                <asp:CheckBox ID="chkSelectAll" runat="server" />
                                            </HeaderTemplate><ItemTemplate>
                                                <asp:CheckBox ID="chkSelectSingle" runat="server" />
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="收件人" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left"><ItemTemplate>
                                                <span class="tool" title='<%# Eval("AllMailTo").ToString() %>'>
                                                    <%# Eval("AnnexId") %>
                                                </span>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="主题" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
                                                <span class="link tooltip" title='<%# Eval("MailName").ToString() %>' onclick="btnQueryClick('<%# Eval("MailId") %>','<%# Eval("AnnexId") %>')">
                                                    <asp:Label ID="lbName" Width="100%" Text='<%# System.Convert.ToString(string.IsNullOrEmpty(Eval("MailName").ToString()) ? "【无主题】" : StringUtility.GetStr(Eval("MailName").ToString(), 50), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                                </span>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件" HeaderStyle-Width="25px"><ItemTemplate>
                                                <span class="link" onclick="queryAdjunct('<%# Eval("AnnexId") %>')">
                                                    <img src='/images1/icon_att0b3dfa.gif' style='cursor: pointer; border-style: none' />
                                                </span>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="时间" HeaderStyle-Width="100px"><ItemTemplate>
                                                <%# Eval("InputDate") %>
                                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                                </webdiyer:AspNetPager>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hfldCheckedIds" runat="server" />
    <asp:HiddenField ID="hfldEmailID" runat="server" />
    <asp:HiddenField ID="hfldAdjunctPath" runat="server" />
    <asp:HiddenField ID="hfldAnnexId" runat="server" />
    </form>
</body>
</html>
