<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BackStockList.aspx.cs" Inherits="StockManage_Refunding_BackStockList" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>从出库单中选物资</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../script/JustWinTable.js"></script>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var jwTable = new JustWinTable('gvNeed');

            jwTable.registClickTrListener(trClickEvent);

            $('#gvNeed a').click(function () {
                $("#hdtp").val("1");
            });
            $('#gvNeed input').click(function () {
                $("#hdtp").val("0");
            });
            $('#btnCancel').click(function () {
                $(parent.document).find('.ui-icon-closethick').each(function () {
                    this.click();
                });
            });
            showTooltip('tooltip', 10);
        });

        function trClickEvent() {
            var chk;
            var inputs = this.getElementsByTagName('INPUT');
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].getAttribute('type') == 'checkbox') {
                    chk = inputs[i];
                }
            }
            if (chk) {
                chk.click();
            }
        }

        function saveEvent() {
            var code = GetArrayToInStr(document.getElementById('hdwzId').value.split(','));
            if (typeof top.ui.callback == 'function') {
                top.ui.callback(code);
            }
            top.ui.closeWin();
        }	
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="tab" style="width: 100%;">
        <tr style="display: none;">
            <td class="divHeader">
                从出库单选择退库物资
            </td>
        </tr>
        <tr>
            <td style="height: 30px;">
                起始时间
                <asp:TextBox ID="txtStartTime" Width="115px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                结束时间
                <asp:TextBox ID="txtEndTime" Width="115px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                出库编号
                <asp:TextBox ID="txtSwcode" Width="115px" runat="server"></asp:TextBox>
                <asp:Button ID="btnSearch" Text="查询" Style="cursor: pointer;" OnClick="btnSearch_Click" runat="server" />
            </td>
        </tr>
        <tr style="width: 100%; height: 36%; vertical-align: top;">
            <td>
                <div class="pagediv" style="height: 180px;">
                    <asp:GridView ID="gvNeed" Width="100%" AutoGenerateColumns="false" AllowPaging="true" CssClass="gvdata" OnPageIndexChanging="gvNeed_PageIndexChanging" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                    <asp:CheckBox ID="chkAll" AutoPostBack="true" OnCheckedChanged="chkBox_CheckedChanged" runat="server" />
                                </HeaderTemplate><ItemTemplate>
                                    <asp:CheckBox ID="chkBox" AutoPostBack="true" ToolTip='<%# Convert.ToString(Eval("orcode")) %>' OnCheckedChanged="chkBox_CheckedChanged" runat="server" />
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单号"><ItemTemplate>
                                    <%# Eval("orcode") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="审核时间"><ItemTemplate>
                                    <%# Common2.GetTime(Eval("intime").ToString()) %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="录入人"><ItemTemplate>
                                    <%# Eval("person") %>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </div>
            </td>
        </tr>
        <tr style="width: 100%; height: 47.2%; vertical-align: top;">
            <td>
                <div class="pagediv" style="height: 200px; width: 100%;">
                    <asp:GridView ID="gvNeedNote" Width="100%" AutoGenerateColumns="false" CssClass="gvdata" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资编号"><ItemTemplate>
                                    <%# Eval("scode") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="出库单号"><ItemTemplate>
                                    <%# Eval("orcode") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="资源名称" HeaderStyle-Width="150px"><ItemTemplate>
                                    <span class="tooltip" title='<%# Eval("ResourceName").ToString() %>'>
                                        <%# StringUtility.GetStr(Eval("ResourceName").ToString(), 10) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格" HeaderStyle-Width="40px"><ItemTemplate>
                                    <span class="tooltip" title='<%# Eval("Specification").ToString() %>'>
                                        <%# StringUtility.GetStr(Eval("Specification").ToString(), 10) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="25px"><ItemTemplate>
                                    <%# Eval("Name") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数量"><ItemTemplate>
                                    <%# Eval("number") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单价"><ItemTemplate>
                                    <%# Eval("sprice") %>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="供应商"><ItemTemplate>
                                    <span class="tooltip" title='<%# Eval("corp").ToString() %>'>
                                        <%# StringUtility.GetStr(Eval("corp").ToString(), 10) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="型号"><ItemTemplate>
                                    <span class="tooltip" title='<%# Eval("ModelNumber").ToString() %>'>
                                        <%# StringUtility.GetStr(Eval("ModelNumber").ToString(), 10) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="品牌"><ItemTemplate>
                                    <span class="tooltip" title='<%# Eval("brand").ToString() %>'>
                                        <%# StringUtility.GetStr(Eval("brand").ToString(), 10) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="技术参数"><ItemTemplate>
                                    <span class="tooltip" title='<%# Eval("TechnicalParameter").ToString() %>'>
                                        <%# StringUtility.GetStr(Eval("TechnicalParameter").ToString(), 10) %>
                                    </span>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td class="bottonrow2" style="height: 20px; text-align: right;">
                <input type="button" id="btnSave" onclick="saveEvent()" style="cursor: pointer;"
                    value="确定" />
                <input type="button" id="btnCancel" value="取消" onclick="top.ui.closeWin();" style="cursor: pointer;" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdwzId" runat="server" />
    <asp:HiddenField ID="hdtp" Value="0" runat="server" />
    </form>
</body>
</html>
