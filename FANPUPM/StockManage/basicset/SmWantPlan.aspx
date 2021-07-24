<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SmWantPlan.aspx.cs" Inherits="StockManage_basicset_SmWantPlan" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>物资需求计划</title>
    <base target="_self" />
    <link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="../script/Config2.js"> </script>
    <script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnBindResource').hide();
            replaceEmptyTable('emptyTable', 'gvSmWantPlanStock');
            showTooltip('tooltip', 10);
            var table = new JustWinTable('gvSmWantPlanStock');
            table.registClickTrListener(function () {
                document.getElementById('btnDesignCode').disabled = false;
                document.getElementById('hfldResourceCode').value = this.id;
            });
            table.registClickSingleCHKListener(function () {
                var checkedChk = table.getCheckedChk();
                if (checkedChk.length == 0) {
                    document.getElementById('btnDesignCode').disabled = true;
                    document.getElementById('hfldResourceCode').value = "";
                }
                else {
                    document.getElementById('btnDesignCode').disabled = false;
                    document.getElementById('hfldResourceCode').value = "";
                    document.getElementById('hfldResourceCode').value = table.getCheckedChkIdJson(checkedChk);
                }
            });
            table.registClickAllCHKLitener(function () {
                document.getElementById('hfldResourceCode').value = "";
                var checkedChk = table.getCheckedChk();
                if (this.checked) {
                    document.getElementById('btnDesignCode').disabled = false;
                    document.getElementById('hfldResourceCode').value = table.getCheckedChkIdJson();
                }
                else {
                    document.getElementById('btnDesignCode').disabled = true;
                }
            });
            registerSaveEvent();
            setBtnState();
        });

        function setBtnState() {
            if (window.location.href.indexOf('look') != -1) {
                document.getElementById('btnDelMaterial').disabled = '!enable';
                document.getElementById('btnSave').disabled = '!enable';
            }
        }

        function registerSaveEvent() {
            document.getElementById('btnSave').onclick = function () {
                if (!validate()) {
                    return false;
                }
            }
        }

        function validate() {
            //alert($('#gvSmWantPlanStock tr').length);
            // if (!document.getElementById('gvSmWantPlanStock') || document.getElementById('gvSmWantPlanStock').firstChild.childNodes.length == 1) {
            if (!$('#gvSmWantPlanStock') || $('#gvSmWantPlanStock tr').length == 1) {
                top.ui.alert('请选择需求物资');
                return false;
            }
            return true;
        }

        // 输入设计编码
        function displayDesignCode() {
            top.ui.prompt('系统提示', '请输入设计编码', function (r) {
                $('#txtDesignCode').val(r);
                $('#btnSaveDesignCode').click();
            });
        }

        // 显示从预算材料中选择
        function displaySelectByBud() {
            if ($('#hfldBudFlowState').val() != '1') {
                top.ui.alert('该项目的预算没有审核通过，不能从预算中选择材料');
                return false;
            }

            var url = '/StockManage/basicset/SelectByBud.aspx?prjId=' + $('#hdnProjectCode').val();
            top.ui.callback = function (resObj) {
                var codelist = "";
                var resId = "";
                codelist = "[";
                for (var i = 0; i < resObj.length; i++) {
                    codelist += "{scode:'" + resObj[i].resCode + "', sprice:'" + resObj[i].price + "', quantity:'" + resObj[i].number + "', taskId:'" + resObj[i].taskId + "', taskName:'" + resObj[i].taskName + "'},";
                    resId += "'" + resObj[i].id + "',";
                }
                resId = resId.substring(0, resId.length - 1);
                codelist = codelist.substring(0, codelist.length - 1);
                codelist += "]";
                $('#hfldResourceIds').val(resId);
                $('#HdnViewCodeList').val(codelist);
                $('#btnBind').click();
            }
            top.ui.openWin({ title: '从预算中选择材料', url: url, width: 1025, height: 500 });
        }

        function saveSignInfo() {
            document.getElementById('btnSaveDesignCode').click();
        }

        // 从材料库中选择资源
        function selectResource() {
            var typeCode = '0002,0003';
            var src = '/StockManage/UserControl/SelectResource.aspx?type=2&TypeCode=' + typeCode;
            top.ui.callback = function (obj) {
                $('#hfldResourceIds').val(obj.id);
                $('#btnBindResource').click();
            }
            top.ui.openWin({ title: '选择资源', url: src, width: 1010, height: 500 });
        }

        //选择设备
        function selectEqu() {
            var url = '/Equ/Equipment/SelectEquipment.aspx?' + new Date().getTime();
            var equDivInfo = { url: url, title: '选择设备', width: 800, height: 500 };
            top.ui.callback = function (equInfo) {
                $('#hfldEquId').val(equInfo.id);
                $('#txtEquCode').val(equInfo.code);
            };
            top.ui.openWin(equDivInfo);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader2">
        <table class="tableHeader">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="物资需求计划" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="divContent2">
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    计划编号
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtCode" ReadOnly="true" Height="15px" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    项目名称
                </td>
                <td class="txt readonly">
                    <input id="txtProName" type="text" readonly="readonly" runat="server" />

                    <input id="hdnProjectCode" name="hdnProjectCode" style="width: 10px;" type="hidden" runat="server" />
<img alt="选择" height="16" onclick="openProjPicker();" src="/Images/corp.gif"
                            style="cursor: hand; border: 0px; display: none" width="16" />
                </td>
            </tr>
            <tr>
                <td class="word">
                    录入人
                </td>
                <td class="txt readonly">
                    <asp:TextBox ID="txtPerson" Height="15px" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    录入时间
                </td>
                <td class="txt readonly">
                    <asp:TextBox ReadOnly="true" ID="txtInputDate" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    设备
                </td>
                <td>
                    <input type="text" id="txtEquCode" readonly="readonly" style="width: 100%;" class="select_input" imgclick="selectEqu" runat="server" />

                    <asp:HiddenField ID="hfldEquId" runat="server" />
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="word">
                    备注
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtRemark" TextMode="MultiLine" Height="40px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    附件
                </td>
                <td colspan="3" style="padding-right: 0px;">
                    <MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" />
                </td>
            </tr>
            <tr visible="false" runat="server"><td colspan="4" style="text-align: left; padding-left: 70px;" runat="server">
                    <asp:CheckBox ID="chkContainPending" Text=" 包含待审数据" runat="server" />
                </td></tr>
        </table>
        <table class="tableContent2" style="margin-left: auto;" cellpadding="5px" cellspacing="0">
            <tr>
                <td style="height: 10px">
                    <hr class="sp" />
                </td>
            </tr>
            <tr>
                <td>
                    <div class="headerDiv" style="text-align: left;">
                        <input type="button" id="btnSelectByBud" value="从预算材料中选择" onclick="displaySelectByBud();" style="width: 120px;" runat="server" />

                        <input type="button" value="从材料库中选择" id="btnSelectResource" onclick="selectResource();"
                            style="width: 100px;" />
                        <input id="btnDelMaterial" type="button" value="删除材料" style="width: 75px; background: #fff url(/images1/btnBack.jpg);" OnServerClick="btnDelMaterial_ServerClick" runat="server" />

                        <input id="btnDesignCode" type="button" value="增加设计编码" style="width: 90px;" onclick="displayDesignCode();"
                            disabled="disabled" />
                        <input id="hdnCodeList" type="hidden" runat="server" />

                    </div>
                </td>
            </tr>
            <tr style="vertical-align: top">
                <td>
                    <div class="pagediv">
                        <asp:GridView ID="gvSmWantPlanStock" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvSmWantPlanStock_RowDataBound" DataKeyNames="ResourceCode" runat="server">
<EmptyDataTemplate>
                                <table id="emptyTable" style="background-color: Red;" class="tab" width="100%">
                                    <tr class="header">
                                        <td style="width: 20px">
                                            <asp:CheckBox ID="chkAll" runat="server" />
                                        </td>
                                        <td style="width: 25px" align="center">
                                            序号
                                        </td>
                                        <td style="width: 100px" align="center">
                                            资源编号
                                        </td>
                                        <td style="width: 150px" align="center">
                                            资源名称
                                        </td>
                                        <td align="center">
                                            规格
                                        </td>
                                        <td align="center">
                                            型号
                                        </td>
                                        <td align="center">
                                            品牌
                                        </td>
                                        <td align="center">
                                            技术参数
                                        </td>
                                        <td align="center">
                                            单位
                                        </td>
                                        <td align="center">
                                            单价
                                        </td>
                                        <td align="center">
                                            需求数量
                                        </td>
                                        <td align="center">
                                            到货日期
                                        </td>
                                        <td align="center">
                                            设计编码
                                        </td>
                                        <td align="center">
                                            分部分项
                                        </td>
                                        <td align="center">
                                            备注
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server" />
                                    </HeaderTemplate>

<ItemTemplate>
                                        <asp:CheckBox ID="chkBox" ToolTip='<%# System.Convert.ToString(Eval("wpsid"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="资源编号" HeaderStyle-Width="100px"><ItemTemplate>
                                        <asp:Label ID="lblCode" Text='<%# System.Convert.ToString(Eval("ResourceCode"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="资源名称" HeaderStyle-Width="150px"><ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("ResourceName").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("ResourceName").ToString(), 10) %>
                                        </span>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格" HeaderStyle-Width="40px"><ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("Specification").ToString() %>'>
                                            <%# StringUtility.GetStr(Eval("Specification").ToString(), 10) %>
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
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="30px"><ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "UnitName") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单价" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="60px"><ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Price") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="库存量" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
                                        <%# WebUtil.GetStockNumberByCode(Eval("ResourceCode").ToString()).ToString() %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="需求数量" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80px"><ItemTemplate>
                                        <asp:TextBox Style="text-align: right;" Width="70px" Height="15px" ID="txtNumber" CssClass="decimal_input" Text='<%# System.Convert.ToString((Eval("number").ToString() == "") ? "0.000" : Eval("number"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="到货日期" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80px"><ItemTemplate>
                                        <asp:TextBox ID="txtarrivalDate" onclick="WdatePicker()" Width="70px" Height="15px" Text='<%# System.Convert.ToString((Eval("arrivalDate").ToString() == "") ? "" : Eval("arrivalDate").ToString().Substring(0, Eval("arrivalDate").ToString().LastIndexOf(" ") + 1), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="设计编码" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px"><ItemTemplate>
                                        <span class="tooltip" title='<%# Eval("DesignCode").ToString() %>'>
                                            <asp:Label ID="lblDesignCode" Text='<%# System.Convert.ToString(StringUtility.GetStr(Eval("DesignCode"), 10), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblDesignCode1" Visible="false" Text='<%# System.Convert.ToString(Eval("DesignCode"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                        </span>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="分部分项" HeaderStyle-Width="60px"><ItemTemplate>
                                        <asp:Label ID="lblTaskName" Text='<%# System.Convert.ToString(Eval("TaskName"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                        <asp:HiddenField ID="hfldTaskId" Value='<%# System.Convert.ToString(Eval("TaskId"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px"><ItemTemplate>
                                        <asp:TextBox ID="txtRemark" Text='<%# System.Convert.ToString(Eval("Remark"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_ServerClick" runat="server" />
                    <input id="btnClose" type="button" value="取消" onclick="winclose('SmWantPlan', 'SmWantPlanList.aspx', false)" />
                </td>
            </tr>
        </table>
    </div>
    
    <div id="divDesignCode" class="divContent2" title="添加设计编码" style="display: none;
        text-align: center;">
        <asp:TextBox ID="txtDesignCode" runat="server"></asp:TextBox>
        <asp:Button ID="btnSaveDesignCode" Text="确定" Style="display: none;" OnClick="btnSaveDesignCode_Click" runat="server" />
    </div>
    <asp:HiddenField ID="hfldWantPlanGuid" runat="server" />
    
    <!--从采购计划中选择物资后执行-->
    <asp:Button ID="btnBindResource" OnClick="btnBindResource_Click" runat="server" />
    
    <asp:HiddenField ID="hfldResourceCode" runat="server" />
    <asp:HiddenField ID="HdnViewCodeList" runat="server" />
    <asp:HiddenField ID="hfldResourceIds" runat="server" />
    <asp:HiddenField ID="hfldBudFlowState" runat="server" />
    <input type="button" id="btnBind" style="display: none;" OnServerClick="btnBind_ServerClick" runat="server" />

    </form>
</body>
</html>
