<%@ Page Language="C#" AutoEventWireup="true" CodeFile="conState.aspx.cs" Inherits="ContractManage_ContractState_conState" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>合同状态</title>
    <style type="text/css">
        #spanContractType
        {
            width: 120px;
        }
    </style>
    <link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/wf.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            replaceEmptyTable('emptyContractType', 'gvwContract');
            var contractTable = new JustWinTable('gvwContract');

            showTooltip('tooltip', 20);
        });

        function ClickRow(AuditState) {
            if (AuditState == "0") {
                document.getElementById('btnConState').disabled = true;
                document.getElementById('btnpansul').disabled = false;
                document.getElementById('btnbn').disabled = false;
                document.getElementById('btnbw').disabled = false;
                document.getElementById('btnjc').disabled = false;
                document.getElementById('btnzz').disabled = false;
            }
            else if (AuditState == "1") {
                document.getElementById('btnConState').disabled = false;
                document.getElementById('btnpansul').disabled = true;
                document.getElementById('btnbn').disabled = false;
                document.getElementById('btnbw').disabled = false;
                document.getElementById('btnjc').disabled = false;
                document.getElementById('btnzz').disabled = false;
            }
            else if (AuditState == "2") {
                document.getElementById('btnConState').disabled = false;
                document.getElementById('btnpansul').disabled = false;
                document.getElementById('btnbn').disabled = true;
                document.getElementById('btnbw').disabled = false;
                document.getElementById('btnjc').disabled = false;
                document.getElementById('btnzz').disabled = false;
            }
            else if (AuditState == "3") {
                document.getElementById('btnConState').disabled = false;
                document.getElementById('btnpansul').disabled = false;
                document.getElementById('btnbn').disabled = false;
                document.getElementById('btnbw').disabled = true;
                document.getElementById('btnjc').disabled = false;
                document.getElementById('btnzz').disabled = false;
            }
            else if (AuditState == "4") {
                document.getElementById('btnConState').disabled = false;
                document.getElementById('btnpansul').disabled = false;
                document.getElementById('btnbn').disabled = false;
                document.getElementById('btnbw').disabled = false;
                document.getElementById('btnjc').disabled = true;
                document.getElementById('btnzz').disabled = true;
            }
            else if (AuditState == "5") {
                document.getElementById('btnConState').disabled = false;
                document.getElementById('btnpansul').disabled = false;
                document.getElementById('btnbn').disabled = false;
                document.getElementById('btnbw').disabled = false;
                document.getElementById('btnjc').disabled = true;
                document.getElementById('btnzz').disabled = true;
            }

        }

        // 选择项目
        function openProjPicker() {
            jw.selectOneProject({ nameinput: 'txtProject' });
        }

        function rowQueryA(url) {
            parent.desktop.AddIncometContract = window;
            toolbox_oncommand(url, "查看合同");
        }
        // 选择合同类型
        function selectConType() {
            jw.selectOneConType({ nameinput: 'txtConType' });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top;">
                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                    <tr>
                        <td class="descTd">
                            合同编号
                        </td>
                        <td>
                            <asp:TextBox ID="txtContractCode" Width="120px" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            合同名称
                        </td>
                        <td>
                            <asp:TextBox ID="txtContractName" Width="120px" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            合同类型
                        </td>
                        <td>
                            <asp:TextBox ID="txtConType" Width="120px" CssClass="select_input" imgclick="selectConType" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            项目
                        </td>
                        <td>
                            <asp:TextBox ID="txtProject" Style="width: 120px;" CssClass="select_input" imgclick="openProjPicker" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            合同状态
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlState" Width="123px" runat="server"><asp:ListItem Value="-1" Text="" /><asp:ListItem Value="0" Text="执行" /><asp:ListItem Value="1" Text="中止" /><asp:ListItem Value="2" Text="保内" /><asp:ListItem Value="3" Text="保外" /><asp:ListItem Value="4" Text="解除" /><asp:ListItem Value="5" Text="终止" /></asp:DropDownList>
                        </td>
                        <td class="descTd">
                            乙方
                        </td>
                        <td>
                            <asp:TextBox ID="txtBName" Width="120px" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            结算方式
                        </td>
                        <td>
                            <asp:DropDownList ID="dropBalanceMode" Width="125px" Height="20px" CssClass="{required:true, messages:{required:'结算方式必须输入'}}" DataValueField="NoteID" DataTextField="CodeName" runat="server"></asp:DropDownList>
                        </td>
                        <td class="descTd">
                            签约时间
                        </td>
                        <td>
                            <asp:TextBox ID="txtSignDate" Width="120px" onclick="WdatePicker()" CssClass="{required:true, messages:{required:'签约时间必须输入'}}" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" Text="查  询" OnClick="btnSearch_Click" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <table class="tab" style="vertical-align: top;">
                    <tr>
                        <td class="divFooter" style="text-align: left;">
                            <asp:Button ID="btnConState" Text="执 行" Enabled="false" OnClick="btnConState_Click" runat="server" />
                            &nbsp;<asp:Button ID="btnpansul" Text="中 止" Enabled="false" OnClick="btnpansul_Click" runat="server" />
                            &nbsp;<asp:Button ID="btnbn" Text="保 内" Enabled="false" OnClick="btnbn_Click" runat="server" />
                            &nbsp;<asp:Button ID="btnbw" Text="保 外" Enabled="false" OnClick="btnbw_Click" runat="server" />
                            &nbsp;<asp:Button ID="btnjc" Text="解 除" Enabled="false" OnClick="btnjc_Click" runat="server" />
                            &nbsp;<asp:Button ID="btnzz" Text="终 止" Enabled="false" OnClick="btnzz_Click" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="">
                                <asp:GridView ID="gvwContract" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwContract_RowDataBound" DataKeyNames="ContractID,IsMainContract,PrjGuid,ContractCode,ConState" runat="server">
<EmptyDataTemplate>
                                        <table id="emptyContractType" class="gvdata">
                                            <tr class="header">
                                                <th scope="col" style="width: 20px;">
                                                    <input id="chk1" type="checkbox" />
                                                </th>
                                                <th scope="col" style="width: 25px;">
                                                    序号
                                                </th>
                                                <th scope="col">
                                                    合同编号
                                                </th>
                                                <th scope="col">
                                                    合同状态
                                                    <th scope="col">
                                                        合同名称
                                                    </th>
                                                    <th scope="col">
                                                        最终金额
                                                    </th>
                                                    <th scope="col">
                                                        合同类型
                                                    </th>
                                                    <th scope="col">
                                                        乙方
                                                    </th>
                                                    <th scope="col">
                                                        结算方式
                                                    </th>
                                                    <th scope="col">
                                                        付款方式
                                                    </th>
                                                </th>
                                                <th scope="col">
                                                    签约时间
                                                </th>
                                                <th scope="col">
                                                    项目
                                                </th>
                                                <th scope="col">
                                                    附件
                                                </th>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" runat="server" />
                                            </HeaderTemplate><ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" />
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("Num").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:BoundField DataField="ContractCode" HeaderText="合同编号" /><asp:TemplateField HeaderText="合同状态"><ItemTemplate>
                                                <asp:Label ID="labState" Text='<%#Eval("conState") %>' runat="server"></asp:Label>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同名称"><ItemTemplate>
                                                <span class="link tooltip" title='<%# Eval("ContractName").ToString() %>' onclick="viewopen ('/ContractManage/PayoutContract/ParyoutContractQuery.aspx?bc=081&bcl=001&ic=<%# Eval("ContractID") %>', '支出合同查看')">
                                                    <%# StringUtility.GetStr(Eval("ContractName").ToString(), 20) %>
                                                </span>
                                            </ItemTemplate></asp:TemplateField><asp:BoundField DataField="ModifiedMoney" ItemStyle-HorizontalAlign="Right" HeaderText="最终金额" ItemStyle-CssClass="decimal_input" /><asp:TemplateField HeaderText="合同类型"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("TypeName").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="乙方"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("CorpName").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结算方式"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("BalanceModeName").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="付款方式"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("PayModeName").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="签约时间"><ItemTemplate>
                                                <%# Common2.GetTime(Eval("SignDate").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("PrjName").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                                <%# GetAnnx(System.Convert.ToString(Eval("ContractID"))) %>
                                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                <webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
                                </webdiyer:AspNetPager>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="divFram" title="" style="display: none">
        <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <div id="divFramRole" title="" style="display: none">
        <iframe id="iframeRole" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <asp:HiddenField ID="hfldContract" runat="server" />
    </form>
</body>
</html>
