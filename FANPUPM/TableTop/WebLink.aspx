<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebLink.aspx.cs" Inherits="TableTop_ModelList" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>桌面外部快捷链接</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script src="../Script/jquery.ui/jquery.ui.sortable.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#Button1').hide();
            var contractTable = new JustWinTable('gvwWebLineList');
            replaceEmptyTable('emptyContractType', 'gvwWebLineList');

            $('#btnDel').get(0).onclick = function () {
                return jw.confirm({ id: 'btnDel' });
            }

            $("#gvwWebLineList").sortable({ stop: function (event, ui) {
                var str = "";
                $("#gvwWebLineList tr").each(function (i) {
                    if ($(this).attr("id").toString() != "") {
                        str += "|";
                        str += i.toString();
                        str += ":"
                        str += $(this).attr("id").toString()
                    }
                });
                //alert(str);
                if (str != "") {
                    $.get("WebLink.aspx", { orderid: str }, function (cdate) {
                        //alert(cdate);
                        if (cdate == "1") {
                            $("#gvwWebLineList tr").each(function (i) {
                                $(this).find("td").eq(1).html(i);
                            });
                        }
                    });
                }
            }, axis: 'y', items: 'tr:not(.header)', delay: 300
            });
        });


        function addList() {
            var url = '/TableTop/WebLinkAdd.aspx';
            top.ui.openWin({ title: '添加收藏夹', url: url, width: 400, height: 280 });
        }
        //选择模块返回值
        function returnModelId(id) {
            document.getElementById('hdfHD').value = id;
        }
        function ClickRow(model) {
            document.getElementById('hdfNotModel').value = "'" + model + "'";
            document.getElementById('btnDel').disabled = false;
            document.getElementById('btnSave').disabled = false;
        }
        $(function () {
            $("input[type='checkbox']").click(function () {
                document.getElementById('btnDel').disabled = false;
                document.getElementById('btnSave').disabled = false;
            })
        });         
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr id="header">
            <td>
                桌面外部快捷链接
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <table class="tab" style="vertical-align: top;">
                    <tr>
                        <td class="divFooter" style="text-align: left">
                            <input id="btnAdd" type="button" value="新增" onclick="addList()" />
                            <asp:Button ID="btnDel" Text="删除" Enabled="false" OnClick="btnDel_Click" runat="server" />
                            <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                            <asp:HiddenField ID="hdfModel" runat="server" />
                            <asp:HiddenField ID="hdfNotModel" runat="server" />
                            <asp:HiddenField ID="hdfHD" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <asp:GridView ID="gvwWebLineList" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwModelList_RowDataBound" DataKeyNames="LinkID" runat="server">
<EmptyDataTemplate>
                                        <table id="emptyContractType" class="gvdata">
                                            <tr class="header">
                                                <th scope="col" style="width: 20px;">
                                                    <input id="chk1" type="checkbox" />
                                                </th>
                                                <th scope="col" style="width: 25px;">
                                                    序号
                                                </th>
                                                <th scope="col" style="width: 80px;">
                                                    网站名称
                                                </th>
                                                <th scope="col">
                                                    外部网址
                                                </th>
                                                <th scope="col" style="width: 80px;">
                                                    备注
                                                </th>
                                                <th scope="col">
                                                </th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
<HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" onkeydown="if(event.keyCode==13)event.keyCode=9" runat="server" />
                                            </HeaderTemplate>

<ItemTemplate>
                                                <asp:CheckBox ID="chk" onkeydown="if(event.keyCode==13)event.keyCode=9" runat="server" />
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="排列序号" HeaderStyle-Width="40px" Visible="false">
<ItemTemplate>
                                                <input type="text" id="txtOrderID" style="text-align: right" title="首页显示按此列排序" onkeydown="if(event.keyCode==13)event.keyCode=9" runat="server" />

                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="网站名称" HeaderStyle-Width="100px">
<ItemTemplate>
                                                <input type="text" id="txtWebName" onkeydown="if(event.keyCode==13)event.keyCode=9" runat="server" />

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="网站名称不能为空" Display="None" ControlToValidate="txtWebName" runat="server"></asp:RequiredFieldValidator>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="外部网址" HeaderStyle-Width="160px">
<ItemTemplate>
                                                <input type="text" id="txtWebAddr" title="网址不能为空,格式如:http://www.justwin.cn" onkeydown="if(event.keyCode==13)event.keyCode=9" runat="server" />

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="网址不能为空" Display="None" ControlToValidate="txtWebAddr" runat="server"></asp:RequiredFieldValidator>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注">
<ItemTemplate>
                                                <input type="text" id="txtRemark" onkeydown="if(event.keyCode==13)event.keyCode=9" runat="server" />

                                            </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div style="height: 0px; width: 0px;">
        <asp:Button ID="Button1" Text="" Width="0px" OnClick="Button1_Click" runat="server" />
    </div>
    <div>
        <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary>
    </div>
    </form>
</body>
</html>
