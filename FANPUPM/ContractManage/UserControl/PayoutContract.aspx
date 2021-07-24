<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayoutContract.aspx.cs" Inherits="ContractManage_UserControl_PayoutContract" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>选择合同信息</title><link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/icon.css" />
<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
    <script language="Javascript" type="text/javascript" src="../../Web_Client/Tree.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script language="Javascript" type="text/javascript">
        $(document).ready(function () {
            // 添加验证
            $('#searcheBt')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
            var gvw = new JustWinTable('GridView1');
            replaceEmptyTable('emptyTable', 'GridView1');
            showTooltip('tooltip', 10);
        });

        function saveEvent() {
            if (typeof top.ui.callback == 'function') {
                var con = {};
                con.id = $('#hfldContractId').val();
                con.name = $('#hfldContractName').val();
                con.bname = $('#hfldBName').val();
                con.prjId = $('#hfldPrjID').val();
                con.prjName = $('#hfldPrjName').val();
                top.ui.callback(con);
                top.ui.callback = null;
            }
            top.ui.closeWin();
        }

        function clickRow(theId, theName, theBName, prjID, prjName) {
            document.getElementById("btnSave").disabled = false;
            $('#hfldContractId').val(theId);
            $('#hfldContractName').val(theName);
            $('#hfldBName').val(theBName);
            $('#hfldPrjID').val(prjID);
            $('#hfldPrjName').val(prjName);
        }
        //双击获得合同信息
        function doDblClickRow(ContCode) {
            $('#btnSave').click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divContent">
        <table class="tableContent" cellpadding="2px" cellspacing="0" style="margin-left: auto;">
            <tr>
                <td>
                    <table style="border-collapse: separate; border-spacing: 0px; border-width: 0px;">
                        <tr style="height: 25px;">
                            <td class="queryTd">
                                合同名称
                                <asp:TextBox ID="txtcontname" Width="80px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                            </td>
                            <td class="queryTd">
                                合同编码
                                <asp:TextBox ID="txtcontcode" Width="80px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="searcheBt" CssClass="button-normal" Text="查询" OnClick="searcheBt_Click" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="vertical-align: top">
                <td style="width: 100%;">
                    <div class="pagediv" style="">
                        <asp:GridView ID="GridView1" AutoGenerateColumns="false" PageSize="12" AllowPaging="true" Width="100%" CssClass="gvdata" DataKeyName="ContractCode" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging" runat="server">
<EmptyDataTemplate>
                                <table id="emptyTable" style="background-color: Red;" class="tab" width="100%">
                                    <tr class="header">
                                        <th scope="col">
                                            序号
                                        </th>
                                        <th scope="col">
                                            合同编号
                                        </th>
                                        <th scope="col">
                                            合同名称
                                        </th>
                                        <th scope="col">
                                            签约日期
                                        </th>
                                        <th scope="col">
                                            合同最终金额
                                        </th>
                                        <th scope="col">
                                            合同性质
                                        </th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号"><ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="合同编号" DataField="ContractCode" ItemStyle-HorizontalAlign="Left" /><asp:TemplateField HeaderText="合同名称" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("ContractName").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("ContractName").ToString(), 10) %>
                                        </span>
                                    </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="签约日期" DataField="SignDate" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" ApplyFormatInEditMode="true" /><asp:BoundField HeaderText="合同最终金额" DataField="ModifiedMoney" ItemStyle-HorizontalAlign="Right" /><asp:TemplateField HeaderText="合同性质" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                        <%# (Eval("fictitious").ToString() == "0") ? "虚拟合同" : "非虚拟合同" %>
                                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div style="text-align: right">
        <input id="btnSave" type="button" class="button-normal" value="确 定" disabled="disabled"
            onclick="saveEvent();" />
        <input id="btnCancel" type="button" value="取 消" class="button-normal" onclick="top.ui.closeWin();" />
    </div>
    <asp:HiddenField ID="hfldContractId" runat="server" />
    <asp:HiddenField ID="hfldContractName" runat="server" />
    <asp:HiddenField ID="hfldBName" runat="server" />
    <asp:HiddenField ID="hfldPrjID" runat="server" />
    <asp:HiddenField ID="hfldPrjName" runat="server" />
    </form>
</body>
</html>
