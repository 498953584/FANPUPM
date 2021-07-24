<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccounIncomeMain.aspx.cs" Inherits="Fund_AccounIncome_AccounIncomeMain" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>入账信息管理</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/StockManage/script/tab.js"></script>
    <script type="text/javascript" src="/Script/DecimalInput.js"></script>
    <script src="../../Web_Client/Tree.js" type="text/javascript"></script>
    <script type="text/javascript" src="/UserControl/FileUpload/GetFiles.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            // 添加验证
            $("#btnSearch")[0].onclick = function () {
                if (!$('#form1').form('validate')) {
                    return false;
                }
            }
            replaceEmptyTable('emptyAccount', 'gvwAccount');
            var contractTable = new JustWinTable('gvwAccount');
            contractTable.registClickTrListener(function () {
                document.getElementById("hfldAccount").value = this.id;

            });

            //trFrame 为存放Frame的TR
            //必需将整个Table的高度设置为100%，且第二个TR的高度为1px
            var trWidth = document.getElementById('trFrame').offsetHeight;
            document.getElementById('framAccount').style.height = (trWidth - 34) + 'px';
            var trwid = document.getElementById('framAccount').offsetWidth;
            document.getElementById('framAccount').style.width = (trwid + 50) + 'px';
            document.getElementById('trFrame').style.width = (trwid + 55) + 'px';
        });
        function ClickRow(accountid) {
            document.getElementById("hfldAccount").value = accountid;
            setf();
        }
        //切换下窗口关联页面
        function setf() {
            var url = "";
            switch (document.getElementById("hdnType").value) {
                case "income": //收入记账
                    url = "/Fund/AccounIncome/AccounIncome.aspx";
                    document.getElementById("framAccount").src = url + "?accountid=" + document.getElementById("hfldAccount").value; //+"&prjname="+document.getElementById("hdnprjname").value;  
                    break;
                case "payout": //支出记账
                    url = "/Fund/AccountPayOut/AccountPayOut.aspx";
                    document.getElementById("framAccount").src = url + "?accountid=" + document.getElementById("hfldAccount").value + "&Sub=" + document.getElementById("hdnSub").value; //+"&prjname="+document.getElementById("hdnprjname").value;  
                    break;
                case "loan": //借款管理
                    url = "../PrjLoan/PrjLoan.aspx";
                    document.getElementById("framAccount").src = url + "?accountid=" + document.getElementById("hfldAccount").value; //+"&prjname="+document.getElementById("hdnprjname").value;
                    break;
                case "repayment": //项目还款管理
                    url = "../PrjLoan/PrjLoan_Repayment.aspx";
                    document.getElementById("framAccount").src = url + "?accountid=" + document.getElementById("hfldAccount").value; //+"&prjname="+document.getElementById("hdnprjname").value;
                    break;
                case "view": //账户查看
                    break;
            }
        }
        //选择项目
        function openProjPicker() {
            jw.selectOneProject({ idinput: 'hdnProjectCode', nameinput: 'txtProject' });
        }
    </script>
</head>
<body onload="selrow2('gvwAccount');">
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" style="border: 0px; width: 100%; height: 100%;
        vertical-align: top;">
        <tr id="header">
            <td colspan="2">
                <asp:Label ID="lblTitle" Text="" runat="server"></asp:Label>
            </td>
        </tr>
        <tr style="height: 1px; width: 100%;">
            <td style="vertical-align: top;">
                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                    <tr>
                        <td class="descTd">
                            账户编号
                        </td>
                        <td>
                            <asp:TextBox ID="txtCode" Width="120px" ClientIDMode="Static" CssClass="easyui-validatebox" data-options="required:false,validType:'validChars[50]'" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            账户名称
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrjName" Width="120px" ClientIDMode="Static" CssClass="easyui-validatebox" data-options="required:false,validType:'validChars[50]'" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            项目
                        </td>
                        <td>
                            <span class="spanSelect" style="width: 122px">
                                <input id="txtProject" readonly="readonly" style="width: 97px; height: 15px;
                                    border: none; line-height: 16px; margin: 1px 0px 1px 2px;" runat="server" />

                                <img alt="选择项目" onclick="openProjPicker();" src="../../images/icon.bmp" style="float: right;" />
                            </span>
                            <asp:HiddenField ID="hdnProjectCode" runat="server" />
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
            <td align="right" style="vertical-align: bottom;">
                <asp:Label ID="Label2" Text="单位：元" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr style="width: 100%">
            <td style="height: 180px; width: 100%;" colspan="2" runat="server">
                <div class="pagediv" style="width: 100%;">
                    <asp:GridView ID="gvwAccount" CssClass="gvdata" AllowPaging="true" PageSize="5" AutoGenerateColumns="false" OnRowDataBound="gvwAccount_RowDataBound" OnPageIndexChanging="gvwAccount_PageIndexChanging" DataKeyNames="accountid" runat="server">
<EmptyDataTemplate>
                            <table id="emptyAccount" class="gvdata">
                                <tr class="header">
                                    <th scope="col" style="width: 20px;">
                                        <input id="chk1" type="checkbox" />
                                    </th>
                                    <th scope="col" style="width: 25px;">
                                        序号
                                    </th>
                                    <th scope="col">
                                        账户编号
                                    </th>
                                    <th scope="col">
                                        账户名称
                                    </th>
                                    <th scope="col">
                                        项目
                                    </th>
                                    <th scope="col">
                                        备注
                                    </th>
                                    <th scope="col">
                                        附件
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
                                    <asp:CheckBox ID="chkAll" runat="server" />
                                </HeaderTemplate>

<ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" />
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="账户编号" DataField="accountNum" /><asp:TemplateField HeaderText="账户名称"><ItemTemplate>
                                    <span class="link" title="账户信息" onclick="viewopen('/Fund/Account/PrjAccountView.aspx?Action=query&ic=<%# Eval("accountid") %>', '项目账户查看')">
                                        <%# Eval("acountName") %>
                                    </span>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目" ItemStyle-Width="100px"><ItemTemplate>
                                    <asp:Label ID="lblPrjName" Width="100px" Style="display: table-cell;" title='<%# Convert.ToString(this.AccountLogic.getPrjName(Convert.ToString(Eval("PrjGuid")))) %>' Text='<%# Convert.ToString(StringUtility.GetStr(this.AccountLogic.getPrjName(Convert.ToString(Eval("PrjGuid"))), 25)) %>' runat="server"></asp:Label>
                                </ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="启动金额" DataField="InitialFundsum" DataFormatString="{0:F3}" ItemStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="其他收入" DataField="OtherIncome" DataFormatString="{0:F3}" ItemStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="回款入账额" DataField="ContIncomeRZ" DataFormatString="{0:F3}" ItemStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="项目借款" DataField="Loan" DataFormatString="{0:F3}" ItemStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="归还本金" DataField="ReturnLoanBJ" DataFormatString="{0:F3}" ItemStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="归还利息及其他" DataField="ReturnLoanOther" DataFormatString="{0:F3}" ItemStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="应支合同额" DataField="MustPaidCont" DataFormatString="{0:F3}" ItemStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="应支间接费" DataField="MustPaidOtherCost" DataFormatString="{0:F3}" ItemStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="账户可用余额" DataField="UsableJE" DataFormatString="{0:F3}" ItemStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="未支合同额" DataField="UnpaidCont" DataFormatString="{0:F3}" ItemStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="未支间接费" DataField="UnpaidOtherCost" DataFormatString="{0:F3}" ItemStyle-HorizontalAlign="Right" /><asp:BoundField HeaderText="账户余额" DataField="JE" DataFormatString="{0:F3}" ItemStyle-HorizontalAlign="Right" /><asp:TemplateField HeaderText="账户操作"><ItemTemplate>
                                    <span class="link" title="账户明细" onclick="viewopen('../AccounViewList.aspx?Action=query&ic=<%# Eval("accountid") %>', '账户明细')">
                                        明 细 </span>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </div>
            </td>
        </tr>
        <tr id="trFrame">
            <td style="width: 100%; vertical-align: top; padding-top: 10px;" colspan="2">
                <iframe id="framAccount" frameborder="0" scrolling="auto" width="100%" runat="server">
                </iframe>
            </td>
        </tr>
    </table>
    <div id="divOpenAdjunct" title="查看附件" style="display: none;">
        <table id="annexTable" class="gvdata" style="width: 100%;" runat="server"><tr class="header" runat="server"><td style="width: 60%" runat="server">
                        名称
                    </td><td style="width: 30%" runat="server">
                        大小
                    </td><td runat="server">
                    </td></tr></table>
    </div>
    <div id="divFram" title="" style="display: none">
        <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <asp:HiddenField ID="hfldAccount" runat="server" />
    <input type="hidden" id="hdnSub" runat="server" />

    <asp:HiddenField ID="hdframsrc" runat="server" />
    <asp:HiddenField ID="hdnType" runat="server" />
    <asp:HiddenField ID="hdnprjname" runat="server" />
    </form>
</body>
</html>
