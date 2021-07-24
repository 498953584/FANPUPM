<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BuildDiary_MX.aspx.cs" Inherits="OPM_BuildDiary_BuildDiary_MX" %>
<%@ Import Namespace="cn.justwin.opm.Public"%>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title></title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/Script/wf.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/Script/jquery.cookie.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript" src="/UserControl/FileUpload/GetFiles.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script src="../../Web_Client/TreeNew.js" type="text/javascript"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            replaceEmptyTable('emptyBuildDiary_MX', 'gvBuildDiary_MX');
            var contractTable = new JustWinTable('gvBuildDiary_MX');
            if (document.getElementById("hfldBDID").value == "") {
                document.getElementById("btnAdd").setAttribute('disabled', 'disabled');
                document.getElementById("btnUpdate").setAttribute('disabled', 'disabled');
                document.getElementById("btnQuery").setAttribute('disabled', 'disabled');
            }
            else {
                if (document.getElementById('hfldFlowstate').value == "-1" || document.getElementById('hfldFlowstate').value == "-3") {
                    setButton(contractTable, 'btnDelete', 'btnUpdate', 'btnQuery', 'hdBuildDiary_MXID');
                }
                else {
                    document.getElementById("btnAdd").setAttribute('disabled', 'disabled');
                    document.getElementById("btnUpdate").setAttribute('disabled', 'disabled');
                    document.getElementById("btnDelete").setAttribute('disabled', 'disabled');
                }
                //                setWfButtonState2(contractTable, 'WF1');
                //document.getElementById("btnAdd").removeAttribute('disabled');
            }
            registerAddEvent();
            registerDeleteEvent();
            registerUpdateEvent();
            registerQueryEvent();
            if (document.getElementById('hfldFlowstate').value == "1") {
                document.getElementById("btnAdd").setAttribute('disabled', 'disabled');
                document.getElementById("btnUpdate").setAttribute('disabled', 'disabled');
                document.getElementById("btnDelete").setAttribute('disabled', 'disabled');
            }
        });

        function registerAddEvent() {
            var btnAdd = document.getElementById('btnAdd');
            addEvent(btnAdd, 'click', function () {
                var modifyId = document.getElementById('hdBuildDiary_MXID').value;
                var bdid = document.getElementById("hfldBDID").value;
                parent.parent.parent.desktop.flowclass = window; //不可少
                var url = "/EPC/BuildDiary/AddBuildDiary_MX.aspx?type=add&mxid=" + modifyId + "&UID=" + bdid;
                top.ui._BuildDiary_MX = window;
                toolbox_oncommand(url, "新增施工日志明细");
            });
        }

        function registerDeleteEvent() {
            var btnDelete = document.getElementById('btnDelete');
            btnDelete.onclick = function () {
                if (!confirm("确定要删除吗？")) {
                    return false;
                }
            }
        }

        function registerUpdateEvent() {
            var btnUpdate = document.getElementById('btnUpdate');
            addEvent(btnUpdate, 'click', function () {
                var modifyId = document.getElementById('hdBuildDiary_MXID').value;
                var bdid = document.getElementById("hfldBDID").value;
                //                parent.parent.desktop.ModifyEdit = window;
                var url = "/EPC/BuildDiary/AddBuildDiary_MX.aspx?type=edit&mxid=" + modifyId + "&UID=" + bdid;
                top.ui._BuildDiary_MX = window;
                toolbox_oncommand(url, "编辑施工日志明细");
            });
        }
        //点击行事件
        function clickRows(id) {

            if (id != "") {
                $("#btnUpdate").removeAttr("disabled");
                $("#btnDelete").removeAttr("disabled");
                $("#btnQuery").removeAttr("disabled");
                $("#hdBuildDiary_MXID").val(id);
            } else {
                $("#btnUpdate").attr("disabled", "disabled");
                $("#btnDelete").attr("disabled", "disabled");
                $("#btnQuery").attr("disabled", "disabled");
            }
            if (document.getElementById('hfldFlowstate').value != "-1" || document.getElementById('hfldFlowstate').value != "-3") {
                $("#btnUpdate").attr("disabled", "disabled");
                $("#btnDelete").attr("disabled", "disabled");
                //$("#btnQuery").attr("disabled", "disabled");
            } else {
                $("#btnUpdate").removeAttr("disabled");
                $("#btnDelete").removeAttr("disabled");
                //$("#btnQuery").removeAttr("disabled");
            }
        }

        function registerQueryEvent() {
            var btnQuery = document.getElementById('btnQuery');
            addEvent(btnQuery, 'click', function () {
                var modifyId = document.getElementById('hdBuildDiary_MXID').value;
                var bdid = document.getElementById("hfldBDID").value;
                //                parent.parent.desktop.ModifyEdit = window;
                var url = "/EPC/BuildDiary/AddBuildDiary_MX.aspx?type=query&mxid=" + modifyId + "&UID=" + bdid;
                toolbox_oncommand(url, "查看施工日志明细");
            });
        }
        function CheckType(uid) {
            //            parent.desktop.flowclass = window; //不可少
            var modifyId = document.getElementById('hdBuildDiary_MXID').value;
            var bdid = document.getElementById("hfldBDID").value;

            var url = "/EPC/BuildDiary/AddBuildDiary_MX.aspx?type=query&mxid=" + modifyId + "&UID=" + bdid;
            if (modifyId != "") {
                toolbox_oncommand(url, "查看施工日志明细");
            }
            else {
                alert("请选择日志明细！");
            }
        }
    </script>
</head>
<body onload="selrow1('gvBuildDiary_MX');">
    <form id="form1" runat="server">
    <table class="tab" width="100%">
        <tr>
            <td class="divHeader" colspan="2">
                施工日志明细
            </td>
        </tr>
        <tr style="vertical-align: top;" enableviewstate="true">
            <td class="divFooter" style="text-align: left;">
                <asp:Button ID="btnAdd" Text="新增" runat="server" />
                <input type="button" id="btnUpdate" value="编辑" disabled="true" runat="server" />

                <asp:Button ID="btnDelete" Text="删除" disabled="disabled" OnClick="btnDelete_Click" runat="server" />
                <input type="button" id="btnQuery" disabled="disabled" value="查看" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <div class="pagediv">
                    <asp:GridView ID="gvBuildDiary_MX" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvBuildDiary_MX_RowDataBound" DataKeyNames="UID" runat="server">
<EmptyDataTemplate>
                            <table id="emptyBuildDiary_MX" class="gvdata">
                                <tr class="header">
                                    <th scope="col" style="width: 20px;">
                                        <input id="chk1" type="checkbox" />
                                    </th>
                                    <th scope="col" style="width: 25px;">
                                        序号
                                    </th>
                                    <th scope="col">
                                        任务代码
                                    </th>
                                    <th scope="col">
                                        任务名称
                                    </th>
                                    <th scope="col">
                                        工作班组
                                    </th>
                                    <th scope="col">
                                        工作人数
                                    </th>
                                    <th scope="col">
                                        施工部位
                                    </th>
                                    <th scope="col">
                                        进度情况
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
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="任务代码"><ItemTemplate>
                                    <%# Eval("TaskCode") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="任务名称"><ItemTemplate>
                                    <span class="link" onclick="CheckType('<%# Eval("UID") %>')">
                                        
                                        <%# GridViewStringSub.StrSub(Eval("TaskName").ToString(), 10) %>
                                    </span>
                                    
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工作班组"><ItemTemplate>
                                    
                                    <%# GridViewStringSub.StrSub(Eval("WorkGroup").ToString(), 10) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工作人数"><ItemTemplate>
                                    <%# Eval("WorkersCount") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="施工部位"><ItemTemplate>
                                    
                                    <%# GridViewStringSub.StrSub(Eval("BuildPosition").ToString(), 10) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="进度情况"><ItemTemplate>
                                    
                                    <%# GridViewStringSub.StrSub(Eval("jdqk").ToString(), 10) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
                                    
                                    <%# GridViewStringSub.StrSub(Eval("Remark").ToString(), 10) %>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdBuildDiary_MXID" runat="server" />
    <asp:HiddenField ID="hfldBDID" runat="server" />
    <asp:HiddenField ID="hfldFlowstate" runat="server" />
    </form>
</body>
</html>
