<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InComeReport.aspx.cs" Inherits="Fund_AccounIncome_InComeReport" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>资金支出报表</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // 添加验证
            $("#btn_Search")[0].onclick = function () {
                if (!$('#form2').form('validate')) {
                    return false;
                }
            }
            var aa = new JustWinTable('gvConract');
            formateTable('gvConract', 2, true);
            addPregressBar('percent');
            replaceEmptyTable('emptygvConract', 'gvConract');
        });

        //选择项目
        function openProjPicker() {
            jw.selectOneProject({ ininput: 'hdPrjCode', nameinput: 'txtPrjName' });
            //			document.getElementById("divFram").title = "选择项目";
            //			document.getElementById("ifFram").src = "/Common/DivSelectProject.aspx?Method=returnPrj";
            //			selectnEvent('divFram');
        }

        //选择账户
        function openAccoun() {
            var url = '/Common/DivSelectFundAccoun.aspx';
            top.ui.callback = function (o) {
                $('#hdfAccoun').val(o.id);
                $('#txtAccoun').val(o.name);
            }
            top.ui.openWin({ title: '选择账户', url: url });
        }
    </script>
</head>
<body>
    <form id="form2" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr id="header" style="height: 20px;">
            <td class="divHeader">
                <asp:Label ID="lblTitle" Text="资金收入报表" runat="server"></asp:Label>
                <asp:HiddenField ID="hdfIncomSum" runat="server" />
            </td>
        </tr>
        <tr style="height: 30px;">
            <td style="height: 20px;">
                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                    <tr>
                        <td class="descTd">
                            项目
                        </td>
                        <td>
                            <span class="spanSelect" style="width: 122px;">
                                <asp:TextBox ID="txtPrjName" CssClass="easyui-validatebox" data-options="required:false,                                          validType:'validChars[50]'" Style="width: 73%; height: 15px; border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
                                <img alt="选择项目" onclick="openProjPicker();" src="../../images/icon.bmp" style="float: right;" />
                            </span>
                            <asp:HiddenField ID="hdPrjCode" runat="server" />
                        </td>
                        <td class="descTd">
                            账户名称
                        </td>
                        <td>
                            <span class="spanSelect" style="width: 122px">
                                <input id="txtAccoun" readonly="readonly" style="width: 97px; height: 15px;
                                    border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

                                <img alt="选择账户" onclick="openAccoun();" src="../../images/icon.bmp" style="float: right;" />
                            </span>
                            <asp:HiddenField ID="hdfAccoun" runat="server" />
                        </td>
                        <td class="descTd">
                            起始时间
                        </td>
                        <td>
                            <asp:TextBox ID="txtStartTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            结束时间
                        </td>
                        <td>
                            <asp:TextBox ID="txtEndTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            入账类型
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSubject" Width="120px" runat="server"><asp:ListItem Value="-1" Text="请选择" /><asp:ListItem Value="0" Text="合同入账" /><asp:ListItem Value="1" Text="启动资金" /><asp:ListItem Value="2" Text="其他收入" /></asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btn_Search" Text="查询" Style="cursor: pointer;" OnClick="btn_Search_Click" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" class="tab">
                    <tr>
                        <td class="divFooter" style="text-align: left">
                            <asp:Button ID="btnexecl" CssClass="button-normal" Text="导出Excel" Width="80px" OnClick="btnexecl_Click" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%;" valign="top">
                            <div style="width: 100%">
                                <asp:GridView ID="gvConract" AutoGenerateColumns="false" CssClass="gvdata" ShowFooter="true" AllowPaging="true" PageSize="15" OnPageIndexChanging="gvPurchaseplan_PageIndexChanging" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="Inuid" runat="server">
<EmptyDataTemplate>
                                        <table id="emptygvConract" class="gvdata">
                                            <tr class="header">
                                                <th scope="col" style="width: 25px;">
                                                    序号
                                                </th>
                                                <th scope="col">
                                                    项目
                                                </th>
                                                <th scope="col">
                                                    账户名称
                                                </th>
                                                <th scope="col">
                                                    回款单号
                                                </th>
                                                <th scope="col">
                                                    合同
                                                </th>
                                                <th scope="col">
                                                    入账类型
                                                </th>
                                                <th scope="col">
                                                    发生日期
                                                </th>
                                                <th scope="col">
                                                    入账人
                                                </th>
                                                <th scope="col">
                                                    金额
                                                </th>
                                                <th scope="col">
                                                    备注
                                                </th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="项目">
<ItemTemplate>
                                                <%# Eval("PrjName").ToString() %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="账户名称"><ItemTemplate>
                                                <%# Eval("acountName").ToString() %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="回款单号">
<ItemTemplate>
                                                <%# Eval("cllectionCode") %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="合同">
<ItemTemplate>
                                                <%# Eval("ContName") %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="入账类型">
<ItemTemplate>
                                                <%# Eval("Subject") %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="发生日期">
<ItemTemplate>
                                                <%# Common2.GetTime(Eval("GetDate").ToString()) %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="入账人">
<ItemTemplate>
                                                <%# Eval("people") %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="金额" ItemStyle-HorizontalAlign="Right">
<ItemTemplate>
                                                <%# Eval("Inmoney") %>
                                            </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
                                                <span class="tooltip" title='<%# Eval("Remark").ToString() %>'>
                                                    <%# StringUtility.GetStr(Eval("Remark"), 25) %>
                                                </span>
                                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdfsummony" runat="server" />
    <div id="divFram" title="" style="display: none">
        <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    </form>
</body>
</html>
