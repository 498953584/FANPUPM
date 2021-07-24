<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Treasury.aspx.cs" Inherits="StockManage_Treasury_Treasury" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="cn.justwin.stockBLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>仓库管理</title><link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../script/Config2.js"></script>
    <script type="text/javascript" src="../script/JustWinTable.js"></script>
    <script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            document.getElementById('btnAdd').onclick = addTreasury
            document.getElementById('btnUpdate').onclick = updateTreasury
            document.getElementById('btnDelete').onclick = deleteTreasury;
            document.getElementById('btnQuery').onclick = queryTreasury;
            var jwTable = new JustWinTable('gvwTreasury');
            replaceEmptyTable('emptyTreasury', 'gvwTreasury');
            showTooltip('tooltip', 20);
            jwTable.registClickTrListener(clickTR);

            jwTable.registClickAllCHKLitener(function () {
                if (this.checked) {
                    var str = getCheckedChkId(jwTable.getAllChk());
                    document.getElementById('hfTcode').value = str;
                    document.getElementById('btnAdd').setAttribute('disabled', 'disabled');
                    document.getElementById('btnUpdate').setAttribute('disabled', 'disabled');
                    document.getElementById('btnDelete').removeAttribute('disabled');
                    document.getElementById('btnQuery').setAttribute('disabled', 'disabled');
                }
            });

            jwTable.registClickSingleCHKListener(function () {
                document.getElementById('btnAdd').setAttribute('disabled', 'disabled');
                var checkedChk = jwTable.getCheckedChk();
                if (checkedChk.length == 0) {
                    document.getElementById('btnDelete').setAttribute('disabled', 'disabled');
                }
                else if (checkedChk.length == 1) {
                    document.getElementById('hfTcode').value = checkedChk[0].parentNode.parentNode.id;
                    document.getElementById('btnUpdate').removeAttribute('disabled');
                    document.getElementById('btnDelete').removeAttribute('disabled');
                    document.getElementById('btnQuery').removeAttribute('disabled');
                }
                else {
                    var str = getCheckedChkId(checkedChk);
                    document.getElementById('hfTcode').value = str;
                    document.getElementById('btnUpdate').setAttribute('disabled', 'disabled');
                    document.getElementById('btnDelete').removeAttribute('disabled');
                    document.getElementById('btnQuery').setAttribute('disabled', 'disabled');
                }
            });
        });

        function getCheckedChkId(checkedChk) {
            var str = '';
            for (var i = 0; i < checkedChk.length; i++) {
                str += '^' + checkedChk[i].parentNode.parentNode.id;
            }
            return str;
        }


        function addTreasury() {
            parent.desktop.TreasuryEdit = window;
            var tcode = document.getElementById('hfSelectValue');
            var url = '/StockManage/Treasury/TreasuryEdit.aspx?Action=Add&Tcode=' + tcode.value;
            toolbox_oncommand(url, "新增仓库");
        }

        function updateTreasury() {
            parent.desktop.TreasuryEdit = window;
            var tcode = document.getElementById('hfTcode');
            if (!tcode.value) return;
            var url = '/StockManage/Treasury/TreasuryEdit.aspx?Action=Update&Tcode=' + tcode.value;
            toolbox_oncommand(url, "编辑仓库");
        }

        function deleteTreasury() {
            if (!confirm("确定要删除吗？")) {
                return false;
            }
        }

        function queryTreasury() {
            parent.desktop.TreasuryEdit = window;
            var tcode = document.getElementById('hfTcode');
            if (!tcode.value) return;
            var url = '/StockManage/Treasury/TreasuryEdit.aspx?Action=Query&Tcode=' + tcode.value
            toolbox_oncommand(url, "查看仓库");
        }

        function clickTR() {
            document.getElementById('hfTcode').value = this.id;
            document.getElementById('btnAdd').setAttribute('disabled', 'disabled');
            document.getElementById('btnUpdate').removeAttribute('disabled');
            document.getElementById('btnDelete').removeAttribute('disabled');
            document.getElementById('btnQuery').removeAttribute('disabled');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="tab">
        
        <tr>
            <td style="height: 96%; width: 100%;">
                <table class="tab">
                    <tr style="height: 100%">
                        <td style="width: 220px; vertical-align: top; height: 100%">
                            <div class="pagediv" style="width: 220px;">
                                <asp:TreeView ID="tvTreasury" ShowLines="true" OnSelectedNodeChanged="TreeView_SelectedNodeChanged" runat="server"><SelectedNodeStyle Font-Underline="false" BackColor="#3399FF" ForeColor="White" HorizontalPadding="0px" VerticalPadding="0px" /></asp:TreeView>
                            </div>
                        </td>
                        <td style="vertical-align: top; border-left: solid 2px #CADEED; height: 100%;">
                            <table class="tab">
                                <tr>
                                    <td class="divFooter" style="text-align: left;">
                                        <input type="button" id="btnAdd" value="新增" />
                                        <input type="button" id="btnUpdate" disabled="disabled" value="编辑" />
                                        <asp:Button ID="btnDelete" Enabled="false" Text="删除" OnClick="btnDelete_Click" runat="server" />
                                        <input type="button" id="btnQuery" disabled="disabled" value="查看" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 100%;">
                                        <div class="pagediv">
                                            <asp:GridView ID="gvwTreasury" Width="100%" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" OnRowDataBound="gvwTreasury_RowDataBound" OnPageIndexChanging="gvwTreasury_PageIndexChanging" DataKeyNames="tcode" runat="server">
<EmptyDataTemplate>
                                                    <table id="emptyTreasury" class="tab" width="100%">
                                                        <tr class="header">
                                                            <td style="width: 30px">
                                                                <asp:CheckBox ID="chkAll" runat="server" />
                                                            </td>
                                                            <td style="width: 20px">
                                                            </td>
                                                            <td style="width: 100px">
                                                                仓库名称
                                                            </td>
                                                            <td style="width: 150px">
                                                                仓库地址
                                                            </td>
                                                            <td style="width: 100px;">
                                                                关联项目
                                                            </td>
                                                            <td>
                                                                仓库说明
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                                            
                                                            <input type="checkbox" id="chkAll" />
                                                        </HeaderTemplate><ItemTemplate>
                                                            
                                                            <input type="checkbox" id="chkbox" />
                                                        </ItemTemplate></asp:TemplateField><asp:BoundField DataField="rowNumber" HeaderStyle-Width="25px" /><asp:TemplateField HeaderText="仓库名称" HeaderStyle-Width="100px"><ItemTemplate>
                                                            <span class="al tooltip" title='<%# Eval("tname").ToString() %>' onclick="toolbox_oncommand('/StockManage/Treasury/TreasuryEdit.aspx?Action=Query&Tcode=<%# Eval("tCode") %>','仓库查看')">
                                                                <%# StringUtility.GetStr(Eval("tname").ToString(), 20) %>
                                                            </span>
                                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="仓库标识" HeaderStyle-Width="100px"><ItemTemplate>
                                                            <%# (base.GetModel() == SmEnum.SmSetValue.TotalMode) ? Eval("tflag") : Eval("IsContrast") %>
                                                        </ItemTemplate></asp:TemplateField><asp:BoundField DataField="taddress" HeaderText="仓库地址" /><asp:TemplateField HeaderText="关联项目" HeaderStyle-Width="100px"><ItemTemplate>
                                                            <asp:Label ID="labprjName" Text='<%# Convert.ToString(Eval("prjCode")) %>' runat="server"></asp:Label>
                                                        </ItemTemplate></asp:TemplateField><asp:BoundField DataField="texplain" HeaderText="仓库说明" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                            <asp:HiddenField ID="hfSelectValue" runat="server" />
                                            <asp:HiddenField ID="hfTcode" runat="server" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
