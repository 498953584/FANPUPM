<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IEPInfoList.aspx.cs" Inherits="AccountManage_IEPInfo_IEPInfoList" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />


    <script type="text/javascript" src="/Script/jquery.js"></script>

    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>

    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>

    <script type="text/javascript" src="../../Script/jw.js"></script>

    <script type="text/javascript" src="../../Script/jwJson.js"></script>

    <script type="text/javascript" src="../../Script/wf.js"></script>

    <script type="text/javascript">
        addEvent(window, 'load', function() {
            replaceEmptyTable();
            var contractTable = new JustWinTable('gvwIEPInfo');
            setButton(contractTable, 'btnDelete', 'btnUpdate', 'btnQuery', 'hfldContract')

        });

        function add() {
            parent.parent.desktop.flowclass = window;
            var IEPid = document.getElementById("hdfIEPId").value; 
            var url = "/AccountManage/IEPInfo/IEPInfoEdit.aspx?id=&type=add&IEPid="+IEPid;
            toolbox_oncommand(url, "新增收支计划详情");
        }

        function registerDeleteContractEvent() {
            var btnDelete = document.getElementById('btnDelete');
            btnDelete.onclick = function() {
                if (!window.confirm('系统提示：\n\n确定要删除吗？')) {
                    return false;
                }
            }
        }
        function update() {
            var hfldContract = document.getElementById('hfldContract');
            parent.parent.desktop.flowclass = window;
            var IEPid = document.getElementById("hdfIEPId").value; 
            var url = "/AccountManage/IEPInfo/IEPInfoEdit.aspx?id=" + hfldContract.value + "&type=edit&IEPid="+IEPid;
            toolbox_oncommand(url, "编辑收支计划详情");

        }
        function view() {
            var hfldContract = document.getElementById('hfldContract');
            parent.parent.desktop.flowclass = window;
            var IEPid = document.getElementById("hdfIEPId").value; 
            var url = "/AccountManage/IEPInfo/IEPInfoView.aspx?id=" + hfldContract.value + "&IEPid=" + IEPid;
            toolbox_oncommand(url, "查看收支计划详情");
        }

        function rowQueryA(path) {
            parent.parent.desktop.flowclass = window;
            var IEPid = document.getElementById("hdfIEPId").value; 
            path += "&IEPid=" + IEPid;
            toolbox_oncommand(path, "查看收支计划详情");
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table class="tab">
        <tr style="vertical-align: top;">
            <td class="divFooter" style="text-align: left;">
                <input type="button" id="btnAdd" value="新增" onclick="add()" runat="server" />

                <input type="button" id="btnUpdate" value="编辑" disabled="disabled" onclick="update()" />
                <asp:Button ID="btnDelete" Text="删除" disabled="disabled" OnClick="btnDelete_Click" runat="server" />
                <input type="button" id="btnQuery" disabled="disabled" value="查看" onclick="view()" />
                <asp:HiddenField ID="hdfIEPId" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <div class="pagediv">
                    <asp:GridView ID="gvwIEPInfo" CssClass="gvdata" AllowPaging="true" PageSize="5" AutoGenerateColumns="false" OnRowDataBound="gvwIEPInfo_RowDataBound" OnPageIndexChanging="gvwIEPInfo_PageIndexChanging" DataKeyNames="ID" runat="server">
<EmptyDataTemplate>
                            <table id="emptyContractType" class="gvdata">
                                <tr class="header">
                                    <th scope="col" style="width: 20px;">
                                        <input id="chk1" type="checkbox" />
                                    </th>
                                    <th scope="col" style="width: 25px;">
                                        序号
                                    </th>
                                    <th scope="col">
                                        详情编号
                                    </th>
                                    <th scope="col">
                                        收支类型
                                    </th>
                                    <th scope="col">
                                        金额
                                    </th>
                                    <th scope="col">
                                        合同名称
                                    </th>
                                    <th scope="col">
                                        添加日期
                                    </th>
                                    <th scope="col">
                                        添加人
                                    </th>
                                    <th scope="col">
                                        备注
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                    <asp:CheckBox ID="chkAll" runat="server" />
                                </HeaderTemplate><ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" />
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="计划编号"><ItemTemplate>
                                    <span class="link" onclick="rowQueryA('/AccountManage/IEPInfo/IEPInfoView.aspx?id=<%# Eval("id") %>')">
                                        <%# StringUtility.GetStr(Eval("infoNum").ToString()) %>
                                    </span>
                                    <asp:HiddenField ID="hidIEPid" Value='<%# Convert.ToString(Eval("id")) %>' runat="server" />
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="收支类型"><ItemTemplate>
                                    <%# (Eval("type").ToString() == "0") ? "收" : "支" %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="金额"><ItemTemplate>
                                    <%# Eval("Money") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="添加日期"><ItemTemplate>
                                    <%# Common2.GetTime(Eval("addDate").ToString()) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="添加人"><ItemTemplate>
                                    <%# Eval("v_xm") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
                                    <%# StringUtility.GetStr(Eval("remark").ToString()) %>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    <div id="divFram" title="" style="display: none">
        <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <asp:HiddenField ID="hfldContract" runat="server" />
    </form>
</body>
</html>
