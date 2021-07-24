<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PostWeave.aspx.cs" Inherits="HR_Organization_PostWeave" %>
<%@ Import Namespace="cn.justwin.BLL"%>

<html>
<head runat="server"><title></title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            showTooltip('tooltip', 20);

            $('#btnAdd').click(function () { OpenWin('add'); });    // 添加
            $('#btnEdit').click(function () { OpenWin('upd'); });   // 修改
            $('#btnDel').get(0).onclick = function () {
                jw.confirm('系统提示', '确定删除?', 'btnDel');
            }
        });


        function ClickRow(RecordID, isdy) {
            document.getElementById('btnEdit').disabled = false;
            if (isdy == 0) {
                document.getElementById('btnDel').disabled = false;
            }
            else {
                document.getElementById('btnDel').disabled = true;
            }
            document.getElementById('HdnDUTYID').value = RecordID;
            frmBooks.location.href = "PostWeavePerson.aspx?id=" + RecordID;
        }

        function OpenWin(op) {
            var RecordID = "";
            if (op != 'add') {
                RecordID = document.getElementById('HdnDUTYID').value;
            }
            var DeptID = document.getElementById('HdnDeptID').value;
            var DeptName = document.getElementById('HdnDeptName').value;
            var url = "/HR/Organization/PostWeaveEdit.aspx?t=" + op + "&rid=" + RecordID + "&dc=" + DeptID + "&dn=" + escape(DeptName);

            top.ui.parentWin = window;
            top.ui.openWin({ title: '岗位', url: url });
        }
        function selrow() {
            try {
                window.document.getElementById('GVBook').click();
            }
            catch (e) { }
        }
    </script>
</head>
<body class="body_clear" scroll="no" onload="selrow();">
    <form id="formx" runat="server">
    <table width="100%" height="99%" cellpadding="0" cellspacing="0" border="1" id="tablex"
        class="table-normal">
        <tr>
            <td rowspan="4" valign="top" width="20%">
                <div class="gridBox">
                    <asp:TreeView ID="TVDept" ShowLines="true" ExpandDepth="1" OnSelectedNodeChanged="TVDept_SelectedNodeChanged" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
                </div>
            </td>
            <td height="20px" class="td-title">
                岗位编制列表
            </td>
        </tr>
        <tr>
            <td height="20px" valign="middle" class="td-toolsbar">
                <asp:Button ID="btnAdd" Text="新 增" Enabled="false" OnClick="btnAdd_Click" runat="server" />
                <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
                <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
                <input id="HdnDUTYID" style="width: 20px" type="hidden" runat="server" />

                <input id="HdnDeptID" value="-1" style="width: 20px" type="hidden" runat="server" />

                <input id="HdnDeptName" value="" style="width: 20px" type="hidden" runat="server" />

            </td>
        </tr>
        <tr style="">
            <td height="260" valign="top">
                <div style="overflow: auto; width: 100%; height: 100%">
                    <asp:GridView CssClass="grid" ID="GVBook" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%" PageSize="10" BorderWidth="0px" CellPadding="0" DataSourceID="SQLDataSource" OnRowDataBound="GVBook_RowDataBound" runat="server">
<EmptyDataTemplate>
                            <table id="Table1" border="0" cellpadding="0" cellspacing="0" class="grid" rules="all"
                                style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px;
                                width: 100%; border-collapse: collapse; border-right-width: 0px">
                                <tr align="center" class="grid_head">
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 40px">
                                        序号
                                    </th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        部门名称
                                    </th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        岗位名称
                                    </th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        编制人数
                                    </th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        在编人数
                                    </th>
                                    <th align="center" nowrap="nowrap" scope="col" style="width: 70px">
                                        空缺人数
                                    </th>
                                    <th align="center" nowrap="nowrap" scope="col">
                                        备注
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField HeaderText="序号" HtmlEncode="false" /><asp:BoundField DataField="V_BMMC" HeaderText="部门名称" HtmlEncode="false" /><asp:BoundField DataField="DutyName" HeaderText="岗位名称" HtmlEncode="false" /><asp:BoundField DataField="DutyNumber" HeaderText="编制人数" HtmlEncode="false" /><asp:BoundField DataField="HaveNumber" HeaderText="在编人数" HtmlEncode="false" /><asp:BoundField DataField="Vacancy" HeaderText="空缺人数" HtmlEncode="false" /><asp:TemplateField HeaderText="备注" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
                                    <span class="tooltip" title='<%# Eval("Remark").ToString() %>'>
                                        <%# StringUtility.GetStr(Eval("Remark").ToString(), 20) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Center"></PagerStyle><PagerSettings FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页"></PagerSettings></asp:GridView>
                    <asp:SqlDataSource ID="SQLDataSource" ProviderName="System.Data.SqlClient" SelectCommand="select a.I_DUTYID,a.i_bmdm,b.V_BMMC,a.DutyName,a.DutyNumber,
                        ((SELECT COUNT(v_yhdm) FROM pt_yhmc WHERE i_bmdm=a.i_bmdm AND I_DUTYID=a.I_DUTYID AND state=1)) as HaveNumber,
                        (a.DutyNumber-(SELECT COUNT(v_yhdm) FROM pt_yhmc WHERE i_bmdm=a.i_bmdm AND I_DUTYID=a.I_DUTYID AND state=1)) as Vacancy,
                        a.Remark from PT_DUTY a left join PT_d_bm b on a.i_bmdm=b.i_bmdm WHERE (a.i_bmdm = @CorpCode)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:ControlParameter ControlID="TVDept" Name="CorpCode" PropertyName="SelectedValue" Type="String" runat="server" /></SelectParameters></asp:SqlDataSource>
                    
                </div>
            </td>
        </tr>
        <tr style="text-align: center;">
            <td height="100%" valign="top">
                <iframe id="frmBooks" name="frmBooks" src="PostWeavePerson.aspx?id=0" frameborder="0" width="99%" height="100%" runat="server"></iframe>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
