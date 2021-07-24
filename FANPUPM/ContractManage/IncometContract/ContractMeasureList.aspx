<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContractMeasureList.aspx.cs" Inherits="ContractManage_IncometContract_ContractMeasureList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>合同计量列表</title><link type="text/css" rel="Stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/wf.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var table = new JustWinTable('gvConract');
            replaceEmptyTable('emptyTable', 'gvConract');
            setWfButtonState2(table, 'WF1');
            setButton(table, 'btnDel', 'btnEdit', 'btnLook', 'hfldMeasureID');
            addEvent($('#btnEdit'), 'click', rowEdit);
            addEvent($('#btnLook'), 'click', rowQuery);
            addEvent($('#btnBAdd'), 'click', rowBAdd);
            $('#hfldContractID').val(getRequestParam('ContractID'));
            $('#hfldPrjGuid').val(getRequestParam('prjId'));
            table.registClickTrListener(function () {
                $('#hfldMeasureID').val($(this).attr('id'));
            });
            table.registClickSingleCHKListener(function () {
                $('#hfldMeasureID').val($(this).attr('id'));
            })
        })
            function rowEdit() {
                top.ui._AddIncometMeasure = window;
                var url = '/ContractManage/IncometContract/AddIncometMeasure.aspx?id=' + $('#hfldMeasureID').val() + '&ContractID=' + $('#hfldContractID').val();
                toolbox_oncommand(url, "编辑合同计量");

            }
            function rowBAdd() {
                top.ui._AddIncometMeasure = window;
                var url = '/ContractManage/IncometContract/AddIncometMeasure.aspx?ContractID=' + $('#hfldContractID').val();
                toolbox_oncommand(url, "新增合同计量");
            }
            function rowQuery() {
                top.ui._AddIncometMeasure = window;
                var url = '/ContractManage/IncometContract/AddIncometMeasure.aspx?t=1&id=' + $('#hfldMeasureID').val() + '&ContractID=' + $('#hfldContractID').val();
                toolbox_oncommand(url, '查看合同计量');
            }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="tab">
        <tr>
            <td style="width: 100%; vertical-align: top;">
                <table border="0" class="tab">
                    <tr>
                        <td class="divFooter" style="text-align: left;">
                            <input type="button" id="btnAdd" value="新增" onclick="rowBAdd();" />
                            <input type="button" id="btnEdit" disabled="disabled" value="编辑"  onclick="rowEdit();"/>
                            <asp:Button ID="btnDel" Text="删  除" disabled="disabled" OnClientClick="return confirm('您确定要删除吗?')" OnClick="btnDel_Click" runat="server" />
                            <input type="button" id="btnLook" disabled="disabled" value="查看" onclick="rowQuery();" />
                            <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiClass="001" BusiCode="133" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 100%; vertical-align: top;">
                            <div class="pagediv">
                                <asp:GridView ID="gvConract" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="RptId,ContractId,BalanceId,PrjId,FlowState" runat="server">
<EmptyDataTemplate>
                                        <table id="emptyTable">
                                           <tr>
                                              <th scope="col" style="width:25px">
                                              </th>
                                              <th scope="col"  style="width:25px">
                                                序号
                                              </th>
                                              <th scope="col">
                                                记录人
                                              </th>
                                              <th scope="col">
                                               上报日期
                                              </th>
                                              <th scope="col">
                                               安全工作记录
                                              </th>
                                              <th scope="col"  style="width:50px">
                                                流程状态
                                              </th>
                                           </tr>
                                        </table>
                                    </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                                <asp:CheckBox ID="cbAllBox" runat="server" />
                                            </HeaderTemplate><ItemTemplate>
                                                <asp:CheckBox ID="cbBox" runat="server" />
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="记录人">
<ItemTemplate>
                                                <%# Eval("InputUser") %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="上报日期">
<ItemTemplate>
                                                <%# Common2.GetTime(Eval("InputDate")) %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="安全工作记录"><ItemTemplate>
                                                <span class="tooltip" title='<%# StringUtility.StripTagsCharArray(Eval("Note").ToString()) %>'>
                                                    <%# StringUtility.GetStr(StringUtility.StripTagsCharArray(Eval("Note").ToString()), 25) %>
                                                </span>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态">
<ItemTemplate>
                                                <%# Common2.GetState(Eval("FlowState").ToString()) %>
                                            </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfldContractID" runat="server" />
    <asp:HiddenField ID="hfldMeasureID" runat="server" />
    <asp:HiddenField ID="hfldPrjGuid" runat="server" />
    </form>
</body>
</html>
