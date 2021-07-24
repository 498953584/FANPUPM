<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IncometContractList.aspx.cs" Inherits="ContractManage_IncometContract_IncometContractList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>收入合同列表</title><link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />
<link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../../Script/wf.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var aa = new JustWinTable('gvConract');
            setButton(aa, 'btnDel', 'btnEdit', 'btnLook', 'hfldPurchaseChecked');
            setBtn(aa, 'btnBAdd');
            addEvent(document.getElementById('btnEdit'), 'click', rowEdit);
            addEvent(document.getElementById('btnLook'), 'click', rowQuery);
            addEvent(document.getElementById('btnBAdd'), 'click', rowBAdd);
            addEvent(document.getElementById('btnAdd'), 'click', rowAdd);
            formateTable('gvConract', 3);
            showTooltip('tooltip', 25);
            setWfButtonState2(aa, 'WF1');
            if (document.getElementById('hldfIsExamineApprove').value == '0') {
                document.getElementById('btnStartWF').style.display = 'none';
                document.getElementById('CancelBt').style.display = 'none';
                document.getElementById('WF1_btnViewWF').style.display = 'none';
                document.getElementById('WF1_btnWFPrint').style.display = 'none';
                document.getElementById('WF1_BtnWFDel').style.display = 'none';
            }

            //绑定参数设置事件
            $('#btnConfig').bind('click', function () {
                parent.desktop.Config = window;
                var url = "/ContractManage/Config.aspx?contype=IncometContractList&type=Update&id=" + $('#hfldPurchaseChecked').val();
                toolbox_oncommand(url, "编辑参数设置");
            });
        });

        function rowEdit() {
            parent.desktop.AddIncometContract = window;
            var url = "/ContractManage/IncometContract/AddIncometContract.aspx?id=" + document.getElementById("hfldPurchaseChecked").value;
            toolbox_oncommand(url, "编辑收入合同");
        }

        function rowBAdd() {
            parent.desktop.AddIncometContract = window;
            var url = "/ContractManage/IncometContract/AddIncometContract.aspx?b=" + document.getElementById("hfldPurchaseChecked").value;
            toolbox_oncommand(url, "新增补充协议");
        }

        function rowAdd() {
            parent.desktop.AddIncometContract = window;
            var url = "/ContractManage/IncometContract/AddIncometContract.aspx";
            toolbox_oncommand(url, "新增收入合同");
        }

        function rowQuery() {

            //viewopen('/ContractManage/IncometContract/IncometContractQuery.aspx?ic=' + document.getElementById("hfldPurchaseChecked").value, '收入合同查看');
            var url = "/ContractManage/IncometContract/IncometContractQuery.aspx?ic=" + document.getElementById("hfldPurchaseChecked").value;
            viewopen(url, 1000, 600);

        }

        function rowQueryA(cid) {
            parent.desktop.AddIncometContract = window;
            var url = "/ContractManage/IncometContract/AddIncometContract.aspx?t=1&id=" + cid;
            toolbox_oncommand(url, "查看收入合同");
        }
        // 选择项目
        function openProjPicker() {
            jw.selectOneProject({ nameinput: 'txtProject' });
        }

        function setBtn(jwTable, btnBAdd) {
            if (jwTable.table.firstChild.childNodes.length == 1) {
                //table中没有数据
                return;
            }
            jwTable.registClickTrListener(function () {
                document.getElementById('btnConfig').removeAttribute('disabled');
                document.getElementById(btnBAdd).removeAttribute('disabled');
                if (this.bstate == "False") {
                    document.getElementById(btnBAdd).setAttribute('disabled', 'disabled');
                }
                if (document.getElementById('hldfIsExamineApprove').value == '1') {
                    if (this.getAttribute('FlowState') == 1) {
                        document.getElementById(btnBAdd).removeAttribute('disabled');
                    } else {
                        document.getElementById(btnBAdd).setAttribute('disabled', 'disabled');
                    }
                }
            });
            jwTable.registClickSingleCHKListener(function () {
                var checkedChk = jwTable.getCheckedChk();
                if (checkedChk.length == 0) {
                    document.getElementById(btnBAdd).setAttribute('disabled', 'disabled');
                    document.getElementById('btnConfig').setAttribute('disabled', 'disabled');
                }
                else
                    if (checkedChk.length == 1) {
                        document.getElementById(btnBAdd).removeAttribute('disabled');
                        document.getElementById('btnConfig').removeAttribute('disabled');
                        if (this.parentNode.parentNode.parentNode.bstate == "False")
                            document.getElementById(btnBAdd).setAttribute('disabled', 'disabled');
                        else
                            document.getElementById(btnBAdd).removeAttribute('disabled');
                        var tr1 = getFirstParentElement(checkedChk[0].parentNode, 'TR');
                        if (document.getElementById('hldfIsExamineApprove').value == '1') {
                            if (tr1.getAttribute('FlowState') == 1) {
                                document.getElementById(btnBAdd).removeAttribute('disabled');
                            } else {
                                document.getElementById(btnBAdd).setAttribute('disabled', 'disabled');
                            }
                        }
                    }
                    else {
                        document.getElementById(btnBAdd).setAttribute('disabled', 'disabled');
                        document.getElementById('btnConfig').setAttribute('disabled', 'disabled');
                    }
            });
            jwTable.registClickAllCHKLitener(function () {
                document.getElementById(btnBAdd).setAttribute('disabled', 'disabled');
                document.getElementById('btnConfig').setAttribute('disabled', 'disabled');
                if (jwTable.table.rows.length == 2 && this.checked == true) {
                    document.getElementById(btnBAdd).removeAttribute('disabled');
                    if (jwTable.table.rows[1].bstate == "False")
                        document.getElementById(btnBAdd).setAttribute('disabled', 'disabled');
                    else
                        document.getElementById(btnBAdd).removeAttribute('disabled');
                }
            });
        }
        function viewopen_n(url) {
            viewopen(url);

        }

        //选择人员
        function selectUser() {
            jw.selectOneUser({ nameinput: 'txtSignPeople', codeinput: 'hfldSignPeople' });
        }


        //往来单位
        function pickCorp() {
            jw.selectOneCorp({ nameinput: 'txtParty', idinput: 'hdParty' });
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
                        <td>
                            <asp:TextBox ID="txtConType" Width="120px" CssClass="select_input" imgclick="selectConType" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            项 目
                        </td>
                        <td colspan="2" align="left" style="border: solid 0px red;">
                            <asp:TextBox ID="txtProject" Width="120px" CssClass="select_input" imgclick="openProjPicker" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            起始金额
                        </td>
                        <td>
                            <asp:TextBox ID="txtStartContractPrice" CssClass="decimal_input" Width="120px" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            结束金额
                        </td>
                        <td>
                            <asp:TextBox ID="txtEndContractPrice" CssClass="decimal_input" Width="120px" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            签约日期
                        </td>
                        <td>
                            <asp:TextBox ID="txtStartSignedTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            至
                        </td>
                        <td>
                            <asp:TextBox ID="txtEndSignedTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="descTd">
                            签订人
                        </td>
                        <td>
                            <input type="text" id="txtSignPeople" class="select_input" style="width: 120px;" imgclick="selectUser" runat="server" />

                            <input id="hfldSignPeople" type="hidden" style="width: 1px" runat="server" />

                        </td>
                        <td class="descTd">
                            甲 方
                        </td>
                        <td>
                            <input type="text" id="txtParty" class="select_input" style="width: 120px;" imgclick="pickCorp" runat="server" />

                            <asp:HiddenField ID="hdParty" runat="server" />
                        </td>
                        <td class="descTd">
                            丙方
                        </td>
                        <td>
                            <asp:TextBox ID="txtCParty" Width="120px" runat="server"></asp:TextBox>
                        </td>
                        <td style="border: solid 0px red;" colspan="2">
                            <asp:Button ID="btn_Search" Text="查询" Style="cursor: pointer;" OnClick="btn_Search_Click" runat="server" />
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
                            <input type="button" id="btnAdd" value="新增" />
                            <input type="button" id="btnBAdd" style="width: 100px;" disabled="disabled" value="新增补充协议" />
                            <input type="button" id="btnEdit" disabled="disabled" value="编辑" />
                            <asp:Button ID="btnDel" Text="删  除" Enabled="false" OnClientClick="return confirm('您确定要删除吗??')" OnClick="btnDel_Click" runat="server" />
                            <input type="button" id="btnLook" disabled="disabled" value="查看" />
                            <input type="button" id="btnConfig" value="参数设置" style="width: 100px;" disabled="disabled" />
                            <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="103" BusiClass="001" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="">
                                <asp:GridView ID="gvConract" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="ContractId,isFContract,FlowState,Project" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                                <asp:CheckBox ID="cbAllBox" runat="server" />
                                            </HeaderTemplate><ItemTemplate>
                                                <asp:CheckBox ID="cbBox" ToolTip='<%# System.Convert.ToString(Eval("ContractId"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("Num").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同编号"><ItemTemplate>
                                                <span class="tooltip" title='<%# Eval("ContractCode").ToString() %>'>
                                                    <%# StringUtility.GetStr(Eval("ContractCode"), 30) %>
                                                </span>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同名称"><ItemTemplate>
                                                <span class="link tooltip" title='<%# Eval("ContractName").ToString() %>' onclick="viewopen_n ('/ContractManage/IncometContract/IncometContractQuery.aspx?ic=<%# Eval("ContractId") %>', '收入合同查看')">
                                                    <%# StringUtility.GetStr(Eval("ContractName").ToString(), 25) %>
                                                </span>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同金额" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
                                                <%# WebUtil.GetEnPrice(Eval("ContractPrice").ToString(), Eval("ContractId").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同类型"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("TypeName").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="签订类型" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                                <asp:Label ID="labsign" Text='<%# System.Convert.ToString((Eval("sign").ToString() == "1") ? "已签订" : "未签订", System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="甲方"><ItemTemplate>
                                                <%# StringUtility.GetStr(base.GetParty(Eval("Party").ToString())) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="结算方式"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("BMode").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="付款方式"><ItemTemplate>
                                                <%# StringUtility.GetStr(Eval("PMode").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合同状态" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
                                                <%# WebUtil.GetConState(Eval("conState").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="签订人"><ItemTemplate>
                                                <%# Eval("SignPeopleName") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="签约日期"><ItemTemplate>
                                                <%# Common2.GetTime(Eval("SignedTime").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目"><ItemTemplate>
                                                <span class="tooltip" title='<%# Eval("prjName").ToString() %>'>
                                                    <%# StringUtility.GetStr(Eval("prjName").ToString()) %>
                                                </span>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件"><ItemTemplate>
                                                <%# GetAnnx(System.Convert.ToString(Eval("ContractId"))) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
                                                <%# Common2.GetState(Eval("FlowState").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="丙方"><ItemTemplate>
                                                <span class="tooltip" title='<%# Eval("CParty").ToString() %>'>
                                                    <%# StringUtility.GetStr(Eval("CParty").ToString(), 25) %>
                                                </span>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
                                                <span class="tooltip" title='<%# Eval("Remark").ToString() %>'>
                                                    <%# StringUtility.GetStr(Eval("Remark").ToString(), 25) %>
                                                </span>
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
    <asp:HiddenField ID="hdReturnVal" runat="server" />
    <asp:HiddenField ID="hldfIsExamineApprove" runat="server" />
    <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    </form>
</body>
</html>
