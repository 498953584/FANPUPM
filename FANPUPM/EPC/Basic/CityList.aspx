<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CityList.aspx.cs" Inherits="EPC_Basic_AddCity" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>模板列表</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script language="javascript" type="text/jscript">
        $(document).ready(function () {
            var gvCity = new JustWinTable('gvCity');
            setButton(gvCity, 'btnDel', 'btnEdit', 'btnLook', 'hfldCityID');
            $('#btnEdit').click(function () {
                rowEdit()
            });
            $('#btnAdd').click(function () {
                rowAdd();
            });
            if ($('#hdProvinceID').val() == '') {
                $('#btnAdd').attr('disabled', 'disabled');
            } else {
                $('#btnAdd').removeAttr('disabled');
            }
            gvCity.registClickTrListener(function () {
                //$('#hfldCityID')
                if ($('#hfldCityID').val() == '') {
                    $('#btnDel').attr('disabled', 'disabled');
                    $('#btnEdit').attr('disabled', 'disabled');
                } else {
                    $('#btnDel').removeAttr('disabled');
                    $('#btnEdit').removeAttr('disabled');
                }
            });
        });
        function rowEdit() {
            top.ui._cityEdit = window;
            var url = '/EPC/Basic/cityEdit.aspx?action=Edit&provinceId=' + $('#hdProvinceID').val() + '&cityID=' + $('#hfldCityID').val();
            toolbox_oncommand(url, '编辑城市信息');
        }
        function rowAdd() {
            top.ui._cityEdit = window;
            var url = '/EPC/Basic/cityEdit.aspx?action=Add&provinceId=' + $('#hdProvinceID').val();
            toolbox_oncommand(url, '新增城市信息');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%; height: 95%;">
        <tr>
            <td style="width: 195px; vertical-align: top; height: 100%;">
                <div class="pagediv" style="width: 195px; height: 105%; vertical-align: top; position: relative;">
                    <asp:TreeView ID="tvProVince" Font-Size="12px" ShowLines="true" Style="top: 0px; position: absolute;" OnSelectedNodeChanged="TreeView_SelectedNodeChanged" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
                </div>
            </td>
            <td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 100%; vertical-align: top;">
                            <table border="0" class="tab">
                                <tr>
                                    <td class="divFooter" style="text-align: left;">
                                        <input type="button" id="btnAdd" value="新增" runat="server" />

                                        <input type="button" id="btnEdit" disabled="disabled" value="编辑" />
                                        <asp:Button ID="btnDel" Text="删  除" disabled="disabled" OnClientClick="return confirm('系统提示：\n\n您确定要删除吗?')" OnClick="btnDel_Click" runat="server" />
                                        <input type="button" id="btnLook" style="display: none;" disabled="disabled" value="查看" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 100%; vertical-align: top;">
                                        <div>
                                            <asp:GridView ID="gvCity" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" OnPageIndexChanging="gvCity_OnPageIndexChanging" OnRowDataBound="gvPrjType_RowDataBound" DataKeyNames="id,ProvinceId" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                            <span style="cursor: default;">
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </span>
                                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="城市名称" HeaderStyle-Width="25px"><ItemTemplate>
                                                            <span style="cursor: default;">
                                                                <%# Eval("Name") %>
                                                            </span>
                                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="城市编码" HeaderStyle-Width="25px"><ItemTemplate>
                                                            <span style="cursor: default;">
                                                                <%# Eval("Code") %>
                                                            </span>
                                                        </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
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
    <asp:HiddenField ID="hfldCityID" runat="server" />
    <asp:HiddenField ID="hdProvinceID" runat="server" />
    </form>
</body>
</html>
