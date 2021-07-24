<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IncometState.aspx.cs" Inherits="ContractManage_ContractState_IncometState" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>收入合同列表</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/jquery.easyui.extension.css" />
<link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#divSignInfo').hide();
            $('#btnSaveSignInfo').hide();

            var jwTable = new JustWinTable('gvConract');
            showTooltip('tooltip', 25);
            jwTable.registClickTrListener(function () {
                document.getElementById('btnSigned').disabled = false;
                document.getElementById('hfldContractId').value = this.id;
            });
            jwTable.registClickSingleCHKListener(function () {
                var checkedChk = jwTable.getCheckedChk();
                if (checkedChk.length == 1) {
                    document.getElementById('btnSigned').disabled = false;
                    var trChecked = getFirstParentElement(checkedChk[0].parentNode, 'TR');
                    document.getElementById('hfldContractId').value = trChecked.id;
                }
                else {
                    document.getElementById('btnSigned').disabled = true;
                    document.getElementById('hfldContractId').value = "";
                }
            });
            jwTable.registClickAllCHKLitener(function () {
                document.getElementById('btnSigned').disabled = true;
                document.getElementById('hfldContractId').value = "";
            });
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

        //选择项目
        function openProjPicker() {
            jw.selectOneProject({ nameinput: 'txtProject' });
        }

        function rowQueryA(cid) {
            parent.desktop.AddIncometContract = window;
            var url = "/ContractManage/IncometContract/AddIncometContract.aspx?t=1&id=" + cid;
            toolbox_oncommand(url, "查看收入合同");
        }

        function viewopen_n(url) {
            viewopen(url);
        }

        // 显示合同的签订信息
        function displaySignInfo() {
            var conIdChecked = document.getElementById('hfldContractId').value;
            $.ajax({
                type: 'GET',
                async: false,
                url: '/ContractManage/Handler/GetContractById.ashx?' + new Date().getTime() + '&contractId=' + conIdChecked,
                success: function (data) {
                    if (data != "") {
                        var conInfo = eval(data)[0];
                        document.getElementById('txtSignedDate').value = conInfo.signDate;
                        document.getElementById('txtReFundDate').value = conInfo.reFundDate;
                        document.getElementById('txtRemark').value = conInfo.remark;
                    }
                }
            });

            $('#divSignInfo').show().window({
                width: 450,
                height: 200,
                modal: true
            });
        }

        function saveSignInfo() {
            // 保存录入信息，EasyUI的弹出层不能保留值
            $('#hfldSignedDate').val($('#txtSignedDate').val());
            $('#hfldReFundDate').val($('#txtReFundDate').val());
            $('#hfldRemark').val($('#txtRemark').val());
            document.getElementById('btnSaveSignInfo').click();
        }

        // 选择合同类型
        function selectConType() {
            jw.selectOneConType({ nameinput: 'txtConType' });
        }
    </script>
    <style type="text/css">
        #spanContractType
        {
            width: 122px;
            margin-left: 3px;
        }
        .style1
        {
            width: 167px;
        }
    </style>
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
                        <td class="style1">
                            <asp:TextBox ID="txtConType" Width="120px" CssClass="select_input" imgclick="selectConType" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            项 目
                        </td>
                        <td colspan="2" align="left" style="border: solid 0px red;">
                            <input type="text" id="txtProject" style="width: 120px;" class="select_input" imgclick="openProjPicker" runat="server" />

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
                            甲方
                        </td>
                        <td>
                            <asp:TextBox ID="txtAName" Width="120px" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            结算方式
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="dropBalanceMode" Width="125px" Height="20px" CssClass="{required:true, messages:{required:'结算方式必须输入'}}" DataValueField="NoteID" DataTextField="CodeName" runat="server"></asp:DropDownList>
                        </td>
                        <td class="descTd">
                            签约日期
                        </td>
                        <td>
                            <asp:TextBox ID="txtSignDate" onclick="WdatePicker()" Width="120px" CssClass="{required:true, messages:{required:'签约日期必须输入'}}" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            签订状态
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSignSate" Width="123px" runat="server"><asp:ListItem Value="-1" Text="" /><asp:ListItem Value="1" Text="已签订" /><asp:ListItem Value="0" Text="未签订" /></asp:DropDownList>
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
                        <td class="divFooter" style="text-align: left">
                            <asp:Button ID="btnConState" Text="执 行" Enabled="false" OnClick="btnConState_Click" runat="server" />
                            &nbsp;<asp:Button ID="btnpansul" Text="中 止" Enabled="false" OnClick="btnpansul_Click" runat="server" />
                            &nbsp;<asp:Button ID="btnbn" Text="保 内" Enabled="false" OnClick="btnbn_Click" runat="server" />
                            &nbsp;<asp:Button ID="btnbw" Text="保 外" Enabled="false" OnClick="btnbw_Click" runat="server" />
                            &nbsp;<asp:Button ID="btnjc" Text="解 除" Enabled="false" OnClick="btnjc_Click" runat="server" />
                            &nbsp;<asp:Button ID="btnzz" Text="终 止" Enabled="false" OnClick="btnzz_Click" runat="server" />
                            &nbsp;<input type="button" id="btnSigned" value="已签订" disabled="disabled" onclick="displaySignInfo();" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="">
                                <asp:GridView ID="gvConract" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="ContractId,isFContract,ConState" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                                <asp:CheckBox ID="cbAllBox" runat="server" />
                                            </HeaderTemplate><ItemTemplate>
                                                <asp:CheckBox ID="cbBox" ToolTip='<%# System.Convert.ToString(Eval("ContractId"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("Num").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同编号"><ItemTemplate>
                                                <%# Eval("ContractCode") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同名称"><ItemTemplate>
                                                <span class="link tooltip" title='<%# Eval("ContractName").ToString() %>' onclick="viewopen_n ('/ContractManage/IncometContract/IncometContractQuery.aspx?ic=<%# Eval("ContractId") %>', '收入合同查看')">
                                                    <%# StringUtility.GetStr(Eval("ContractName").ToString(), 25) %>
                                                </span>
                                                
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同金额" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                                <%# WebUtil.GetEnPrice(Eval("ContractPrice").ToString(), Eval("ContractId").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同状态" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                                <asp:Label ID="labState" Text='<%# Eval("conState")%>' runat="server"></asp:Label>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同类型"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("TypeName").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="签订状态" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                                <asp:Label ID="labsign" Text='<%# System.Convert.ToString((Eval("sign").ToString() == "1") ? "已签订" : "未签订", System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="甲方"><ItemTemplate>
                                                <%# StringUtility.GetStr(base.GetParty(Eval("Party").ToString())) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结算方式"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("BMode").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="付款方式"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("PMode").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="签约日期"><ItemTemplate>
                                                <%# Common2.GetTime(Eval("SignedTime").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("prjName").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件"><ItemTemplate>
                                                <%# GetAnnx(System.Convert.ToString(Eval("ContractId"))) %>
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
    
    <div id="divSignInfo" class="divContent2" title="签订信息" style="text-align: center;">
        <table style="width: 100%; margin: auto;" cellpadding="3px" cellspacing="0px">
            <tr>
                <td class="descTd">
                    签约日期
                </td>
                <td class="txt mustInput" align="left">
                    <asp:TextBox ID="txtSignedDate" Width="120px" onclick="WdatePicker()" CssClass="{required:true, messages:{required:'签约日期不能为空！'}}" runat="server"></asp:TextBox>
                </td>
                <td class="descTd">
                    返还日期
                </td>
                <td align="left">
                    <asp:TextBox ID="txtReFundDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="descTd">
                    备注
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtRemark" Height="60px" Rows="3" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="right">
                    <input type="button" value="确定" onclick="saveSignInfo();" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    <asp:HiddenField ID="hfldContractId" runat="server" />
    <!-- 签约日期-->
    <asp:HiddenField ID="hfldSignedDate" runat="server" />
    <!-- 返还日期-->
    <asp:HiddenField ID="hfldReFundDate" runat="server" />
    <!-- 备注-->
    <asp:HiddenField ID="hfldRemark" runat="server" />
    <asp:Button ID="btnSaveSignInfo" Text="" OnClick="btnSaveSignInfo_Click" runat="server" />
    </form>
</body>
</html>
