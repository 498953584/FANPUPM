<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ScienceInnovateList.aspx.cs" Inherits="EPC_17_Entpm_ScienceInnovate_ScienceInnovateList" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>企业技术管理</title>
    <script type="text/javascript" src="../../../../Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript">
        window.onload = function () {
            document.getElementById('btnAdd').onclick = addTreasury
            document.getElementById('btnUpdate').onclick = updateTreasury
            document.getElementById('btnDelete').onclick = deleteTreasury;
            document.getElementById('btnQuery').onclick = queryTreasury;
            replaceEmptyTable('gvwScienceInnovateEmpty', 'gvwScienceInnovate');
            var jwTable = new JustWinTable('gvwScienceInnovate');
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
        }

        function getCheckedChkId(checkedChk) {
            var str = '';
            for (var i = 0; i < checkedChk.length; i++) {
                str += '^' + checkedChk[i].parentNode.parentNode.id;
            }
            return str;
        }


        function addTreasury() {
            parent.parent.desktop.ScienceInnovateEditor = window;
            var tcode = document.getElementById('hfSelectValue');
            var url = '/EPC/17/Entpm/ScienceInnovate/ScienceInnovateEditor.aspx?Action=Add&Tcode=' + tcode.value;
            top.ui._ScienceInnovateList = window;
            toolbox_oncommand(url, "新增企业技术");
        }

        function updateTreasury() {
            parent.parent.desktop.ScienceInnovateEditor = window;
            var tcode = document.getElementById('hfTcode');
            if (!tcode.value) return;
            var url = '/EPC/17/Entpm/ScienceInnovate/ScienceInnovateEditor.aspx?Action=Update&Tcode=' + tcode.value;
            top.ui._ScienceInnovateList = window;
            toolbox_oncommand(url, "编辑企业技术");
        }

        function deleteTreasury() {
            if (!confirm("确定要删除吗？")) {
                return false;
            }
        }

        function queryTreasury() {
            parent.parent.desktop.ScienceInnovateEditor = window;
            var tcode = document.getElementById('hfTcode');
            if (!tcode.value) return;
            var url = '/EPC/17/Entpm/ScienceInnovate/ScienceInnovateEditor.aspx?Action=Query&Tcode=' + tcode.value
            toolbox_oncommand(url, "查看企业技术");
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
                        <td style="vertical-align: top; height: 100%;">
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
                                            <asp:GridView ID="gvwScienceInnovate" Width="100%" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" OnPageIndexChanging="gvwScienceInnovate_PageIndexChanging" OnRowDataBound="gvwScienceInnovate_RowDataBound" DataKeyNames="Id_xh" runat="server">
<EmptyDataTemplate>
                                                    <table class="gvdata" cellspacing="0" rules="all" border="1" id="gvwScienceInnovateEmpty"
                                                        style="width: 100%; border-collapse: collapse;">
                                                        <tr class="header">
                                                            <th align="center" scope="col" style="width: 20px;">
                                                                <input type="checkbox" id="chkAll" />
                                                            </th>
                                                            <th scope="col" style="width: 25px;">
                                                                序号
                                                            </th>
                                                            <th scope="col" style="width: 100px;">
                                                                企业技术编号
                                                            </th>
                                                            <th scope="col" style="width: 400px;">
                                                                企业技术名称
                                                            </th>
                                                            <th scope="col">
                                                                备注
                                                            </th>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
                                                            <input type="checkbox" id="chkAll" />
                                                        </HeaderTemplate><ItemTemplate>
                                                            <input type="checkbox" id="chkbox" />
                                                        </ItemTemplate></asp:TemplateField><asp:BoundField DataField="rowNumber" HeaderText="序号" HeaderStyle-Width="25px" /><asp:TemplateField HeaderText="企业技术编号" HeaderStyle-Width="100px"><ItemTemplate>
                                                            <%# Eval("Id_xh") %>
                                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="企业技术名称" HeaderStyle-Width="400px"><ItemTemplate>
                                                            <span class="al" onclick="toolbox_oncommand('/EPC/17/Entpm/ScienceInnovate/ScienceInnovateEditor.aspx?Action=Query&Tcode=<%# Eval("Id_xh") %>','企业信息查看')">
                                                                <%# Eval("ClassName") %>
                                                            </span>
                                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
                                                            <span class="tooltip" title='<%# Eval("Remark").ToString() %>'>
                                                                <%# StringUtility.GetStr(Eval("Remark").ToString(), 35) %>
                                                            </span>
                                                        </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
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
