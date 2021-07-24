<%@ Page Language="C#" AutoEventWireup="true" CodeFile="menuList.aspx.cs" Inherits="menuList" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>内部应用</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
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
        addEvent(window, 'load', function () {
            var jwTable = new JustWinTable('gvwModelList');
            replaceEmptyTable('emptyContractType', 'gvwModelList');

            jwTable.registClickTrListener(function () {
                var btnDel = document.getElementById('btnDel');
                btnDel.removeAttribute('disabled');
            });

            jwTable.registClickSingleCHKListener(function () {
                var checkedChk = jwTable.getCheckedChk();
                var btnSelectPerson = document.getElementById('btnDel');
                btnSelectPerson.removeAttribute('disabled');
                if (checkedChk.length > 0) {
                    btnSelectPerson.removeAttribute('disabled');
                }
                else {
                    document.getElementById('btnDel').setAttribute('disabled', 'disabled');
                }
            });
        });
        //屏蔽回车
//        function document.onkeypress() {
//            if (event.keyCode == 13) {
//                event.keyCode = 0; event.returnvalue = false;
//            }
//        }

        function addList() {
            document.getElementById("divFram").title = "选择显示模块";
            var url =  '/TableTop/menuListUserSel.aspx?Method=returnModelId&op=' + $('#hdfOp').val();
            top.ui.openWin({title: '选择显示模块', url: url });
//            $('#divFram').dialog({ width: 600, height: 450, modal: true });
        }
        //选择模块返回值
        function returnModelId(id) {
            document.getElementById('hdfHD').value = id;
        }

        function ClickRow(model) {
            document.getElementById('hdfNotModel').value = "'" + model + "'";
        }

        //火狐浏览器
//        if (navigator.userAgent.indexOf('Firefox') >= 0) {
//            function document.onkeypress() {
//                if (event.keyCode == 13) {
//                    event.keyCode = 0; event.returnvalue = false;
//                }
//            }
//        }
        $(document).ready(function () {
            $("#gvwModelList_ctl01_chkAll").click(function () {
                var flag = $(this).attr("checked");
                if (flag == 'checked') {
                    $("#btnDel").removeAttr("disabled");
                } else {
                    $("#btnDel").attr("disabled", "disabled");
                }
            });
            $("#gvwModelList").sortable({ stop: function (event, ui) {
                var str = "";
                $("#gvwModelList tr").each(function (i) {
                    if ($(this).attr("id").toString() != "") {
                        str += "|";
                        str += i.toString();
                        str += ":"
                        str += $(this).attr("id").toString()
                    }
                });
                if (str != "") {
                    $.get("Handler.ashx", { orderid: str }, function (cdate) {
                        if (cdate == "1") {
                            $("#gvwModelList tr").each(function (i) {
                                $(this).find("td").eq(1).html(i);
                            });
                        }
                    });
                }
            }, axis: 'y', items: 'tr:not(.header)', delay: 300
            });

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr id="header">
            <td>
                <asp:Label ID="labTitle" Text="内部应用" runat="server"></asp:Label><asp:HiddenField ID="hdfOp" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <table class="tab" style="vertical-align: top;">
                    <tr>
                        <td class="divFooter" style="text-align: left">
                            <input id="btnAdd" type="button" value="新增" onclick="addList()" />
                            <asp:Button ID="btnDel" Text="删除" Enabled="false" OnClick="btnDel_Click" runat="server" />
                            <asp:Button ID="btnSave" Text="保存" Enabled="false" OnClick="btnSave_Click" runat="server" />
                            <asp:HiddenField ID="hdfModel" runat="server" />
                            <asp:HiddenField ID="hdfNotModel" runat="server" />
                            <asp:HiddenField ID="hdfHD" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <asp:GridView ID="gvwModelList" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwModelList_RowDataBound" DataKeyNames="ModelId" runat="server">
<EmptyDataTemplate>
                                        <table id="emptyContractType" class="gvdata">
                                            <tr class="header">
                                                <th scope="col" style="width: 20px;">
                                                    <input id="chk1" type="checkbox" />
                                                </th>
                                                <th scope="col" style="width: 25px;">
                                                    序号
                                                </th>
                                                <th scope="col" style="width: 50px;">
                                                    模块编号
                                                </th>
                                                <th scope="col" style="width: 80px;">
                                                    模块名称
                                                </th>
                                                <th scope="col">
                                                    排列序号
                                                </th>
                                                <th scope="col" style="width: 80px;">
                                                    自定栏目标题名称
                                                </th>
                                                <th scope="col">
                                                </th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" onkeydown="if(event.keyCode==13)event.keyCode=9" runat="server" />
                                            </HeaderTemplate><ItemTemplate>
                                                <asp:CheckBox ID="chk" onkeydown="if(event.keyCode==13)event.keyCode=9" runat="server" />
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="模块编号" HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Left"><ItemTemplate>
                                                <%# Eval("modelid") %>
                                            </ItemTemplate></asp:TemplateField><asp:BoundField DataField="v_mkmc" HeaderText="模块名称" HeaderStyle-Width="120px" /><asp:TemplateField Visible="false" HeaderText="排列序号" HeaderStyle-Width="50px"><ItemTemplate>
                                                <asp:TextBox ID="txtOrderID" Style="text-align: right" onkeydown="if(event.keyCode==13)event.keyCode=9" Text='<%# Convert.ToString(Eval("orderId")) %>' runat="server"></asp:TextBox>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="自定义栏目名称"><ItemTemplate>
                                                <asp:TextBox ID="txtModelName" onkeydown="if(event.keyCode==13)event.keyCode=9" Text='<%# Convert.ToString(Eval("MNewName")) %>' runat="server"></asp:TextBox>
                                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="divFram" title="添加" style="display: none">
        <iframe id="ifAdd" frameborder="0" width="100%" height="100%" src="/TableTop/menuListUserSel.aspx?Method=returnModelId">
        </iframe>
    </div>
    
    </form>
</body>
</html>
