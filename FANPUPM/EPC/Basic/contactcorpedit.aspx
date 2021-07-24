<%@ Page Language="C#" AutoEventWireup="true" CodeFile="contactcorpedit.aspx.cs" Inherits="ContactCorpEdit" %>
<%@ Import Namespace="Microsoft.Web.UI.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="Head1" runat="server"><title>往来单位信息</title>
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

    <base target="_self">
    <link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="../Script/jquery.autocomplete/jquery.autocomplete.css" />
<link rel="Stylesheet" type="text/css" href="CSS/PettyCash.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
    <script language="javascript" src="../../web_client/validator.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script language="javascript" type="text/javascript">
        addEvent(window, 'load', function () {
            //设置项目的宽度
            if (getRequestParam('Action') == 'Query') {
                setAllInputDisabled();
            }
            //		        registerBtnCancelEvent();
            //		        Val.validate('form1');
            //		        setAuditState();
        });
        function openCodeEdit(type) {
            var url = "../Basic/CodeList.aspx?tid=" + type + "&w=1";
            return window.showModalDialog(url, window, 'dialogHeight: 450px; dialogWidth: 445px; center: Yes; help: No; resizable: No; status: No;');
        }
        function judge() {
            var text = document.getElementById("TxtCorpName").value;
            var textLink = document.getElementById('TxtLinkMan').value;
            //alert(textLink);
            if (text == "") {
                alert("请填写单位名称！");
            }
            if (text.length > 1000) {

                alert("单位名称长度过长，已大于1000！");
            }
            if (textLink != "") {
                if (textLink.indexOf(' ') != -1) {
                    textLink = textLink.replace(/ /g, ",");
                    document.getElementById('TxtLinkMan').value = textLink;
                }
                if (textLink.indexOf('、') != -1) {
                    document.getElementById('TxtLinkMan').value = textLink.replace(/、/g, ",");
                }
                if (textLink.indexOf('，') != -1) {
                    document.getElementById('TxtLinkMan').value = textLink.replace(/，/g, ",");
                }
                if (textLink.indexOf(' ') < 0 && textLink.indexOf('、') < 0 && textLink.length > 8 && textLink.indexOf('，') < 0) {
                    alert('系统提示:\n\n输入多个联系人请用逗号隔开');
                    return;
                }
            }
        }        
			
    </script>
</head>
<body scroll="no" class="body_popup">
    <form id="Form1" method="post" runat="server">
    <table height="100%" cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td valign="bottom" height="22">
                <iewc:TabStrip ID="TabRBS" SepDefaultStyle="border-top:#999AB5 solid 0px;border-left:#999AB5 solid 0px;border-right:#999AB5 solid 0px;border-bottom:#999AB5 solid 1px;" TabSelectedStyle="background-image:url(../../images/2_1.gif);color:#000000;border-left:#999AB5 solid 1px;border-top:#999AB5 solid 1px;border-right:#999AB5 solid 1px;border-bottom:#999AB5 solid 0px;width:80;height:20;font-size:12px;text-align:center;" TabHoverStyle="background-color:#777777;" TabDefaultStyle="background-image:url(../../images/2_2.gif);border-left:#999AB5 solid 1px;border-top:#999AB5 solid 1px;border-right:#999AB5 solid 1px;border-bottom:#999AB5 solid 1px;font-family:verdana;font-weight:bold;font-size:12px;color:#000000;width:80;height:20;text-align:center;" TargetID="MPageRBS" TabIndex="-1" runat="server"><Items><iewc:TabSeparator ID="TabSeparator1" runat="server"></iewc:TabSeparator><iewc:Tab ID="Tab1" Text="基本信息" runat="server"></iewc:Tab><iewc:TabSeparator ID="TabSeparator2" runat="server"></iewc:TabSeparator><iewc:Tab ID="Tab2" Text="其它信息" runat="server"></iewc:Tab><iewc:TabSeparator ID="TabSeparator3" DefaultStyle="width:100%;" runat="server"></iewc:TabSeparator></Items></iewc:TabStrip>
            </td>
        </tr>
        <tr>
            <td class="mp_frame_top" style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px;
                padding-top: 3px" valign="top">
                <iewc:MultiPage ID="MPageRBS" Height="100%" runat="server"><iewc:PageView ID="PageView1" runat="server">
                        <table class="table-normal" cellspacing="0" cellpadding="0" width="100%" border="1">
                            <tr>
                                <td align="right" width="80px" class="td-label">
                                    单位名称：
                                </td>
                                <td colspan="3" width="610px">
                                    <asp:TextBox ID="TxtCorpName" CssClass="textarea-input" MaxLength="1000" Width="610px" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="公司名称不能为空！" ControlToValidate="TxtCorpName" Display="None" runat="server"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="td-label" width="80px">
                                    推荐供应商：
                                </td>
                                <td width="295px">
                                    <asp:CheckBox ID="IsHot" runat="server" />是
                                </td>
                                <td align="right" class="td-label" width="80px">
                                    单位类型：
                                </td>
                                <td width="235px">
                                    <asp:DropDownList ID="ddlCorpType" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="80px" class="td-label">
                                    品&nbsp;&nbsp;&nbsp;&nbsp;牌：
                                </td>
                                <td colspan="3" width="610px">
                                    <asp:TextBox ID="txtBrand" Width="610px" Rows="2" TextMode="MultiLine" CssClass="textarea-input" runat="server"></asp:TextBox>
                                    <!---->
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="80px" class="td-label">
                                    产品概况：
                                </td>
                                <td colspan="3" width="610px">
                                    <asp:TextBox ID="TxtSpeciality" Width="610px" Rows="2" TextMode="MultiLine" CssClass="textarea-input" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="80px" class="td-label">
                                    联 系 人：
                                </td>
                                <td width="295px">
                                    <asp:TextBox ID="TxtLinkMan" Width="295px" CssClass="textarea-input" Rows="2" TextMode="MultiLine" runat="server"></asp:TextBox>
                                    <!---->
                                </td>
                                <td align="right" width="80px" class="td-label">
                                    手机号码：
                                </td>
                                <td width="235px">
                                    <asp:TextBox ID="TxtHandPhone" Width="235px" CssClass="textarea-input" Rows="2" TextMode="MultiLine" runat="server"></asp:TextBox>
                                    <!---->
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="td-label" width="80px">
                                    联系电话：
                                </td>
                                <td colspan="3" width="610px">
                                    <asp:TextBox ID="TxtTelephone" Width="610px" CssClass="textarea-input" Rows="2" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="80px" class="td-label">
                                    传&nbsp;&nbsp;&nbsp;&nbsp;真：
                                </td>
                                <td width="295px">
                                    <asp:TextBox ID="TxtFax" Width="295px" Height="100%" CssClass="textarea-input" Rows="2" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </td>
                                <td align="right" width="80px" class="td-label">
                                    法人代表：
                                </td>
                                <td width="235px">
                                    <asp:TextBox ID="TxtCorporation" Width="235px" CssClass="textarea-input" Rows="2" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="80px" class="td-label">
                                    资&nbsp;&nbsp;&nbsp;&nbsp;质：
                                </td>
                                <td colspan="3" width="610px">
                                    <asp:TextBox ID="TxtAptitude" Width="610px" CssClass="textarea-input" Rows="2" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="80px" class="td-label">
                                    单位简介：
                                </td>
                                <td colspan="3" width="610px">
                                    <asp:TextBox ID="TxtCorpBrief" Width="610px" CssClass="textarea-input" Rows="2" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="80px" class="td-label">
                                    地&nbsp;&nbsp;&nbsp;&nbsp;址：
                                </td>
                                <td colspan="3" width="610px">
                                    <asp:TextBox ID="TxtAddress" Width="610px" CssClass="textarea-input" Rows="2" TextMode="MultiLine" runat="server"></asp:TextBox>
                                    <!---->
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="td-label" width="80px">
                                    邮政编码：
                                </td>
                                <td width="295px">
                                    <asp:TextBox ID="TxtPostCode" Width="295px" CssClass="textarea-input" Rows="2" TextMode="MultiLine" runat="server"></asp:TextBox>
                                    <!---->
                                </td>
                                <td align="right" class="td-label" width="80px">
                                    国&nbsp;&nbsp;&nbsp;&nbsp;家：
                                </td>
                                <td width="235px">
                                    <asp:TextBox ID="txtContry" Width="235px" CssClass="textarea-input" Rows="2" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="td-label" width="80px">
                                    邮&nbsp;&nbsp;&nbsp;&nbsp;箱：
                                </td>
                                <td width="295px">
                                    <asp:TextBox ID="txtEmail" Width="295px" CssClass="textarea-input" Rows="2" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </td>
                                <td align="right" class="td-label" width="80px">
                                    网&nbsp;&nbsp;&nbsp;&nbsp;址：
                                </td>
                                <td width="235px">
                                    <asp:TextBox ID="TxtWebSite" Width="235px" CssClass="textarea-input" Rows="2" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                        </table>
                    </iewc:PageView><iewc:PageView ID="PageView2" runat="server">
                        <table class="table-normal" cellspacing="0" cellpadding="0" border="1" width="100%">
                            <tr>
                                <td align="right" class="td-label" width="80px">
                                    单位性质：
                                </td>
                                <td width="260px">
                                    <JWControl:DropDownTree ID="DDTCorpKind" TabString="" Width="260px" Height="30px" runat="server"></JWControl:DropDownTree>
                                    
                                </td>
                                <td align="right" class="td-label" width="80px">
                                    经营类别：
                                </td>
                                <td width="260px">
                                    <JWControl:DropDownTree ID="DDTFareSort" TabString="" Width="260px" Height="30px" runat="server"></JWControl:DropDownTree>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="td-label" width="80px">
                                    营业执照：
                                </td>
                                <td colspan="3" width="610px">
                                    <asp:TextBox ID="TxtShopCard" Width="610px" CssClass="textarea-input" Rows="2" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="td-label" width="80px">
                                    税&nbsp;&nbsp;&nbsp;&nbsp;号：
                                </td>
                                <td colspan="3" width="610px">
                                    <asp:TextBox ID="TxtTaxCard" Width="610px" CssClass="textarea-input" Rows="2" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="td-label" width="80px">
                                    <p>
                                        银行帐号：</p>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="TxtBankAccounts" Width="610px" CssClass="textarea-input" Rows="2" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="td-label" width="80px">
                                    开户银行：
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="TxtAccountBank" Width="610px" CssClass="textarea-input" Rows="2" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="td-label" width="80px">
                                    委 托 人：
                                </td>
                                <td colspan="3" width="610px">
                                    <asp:TextBox ID="TxtClient" Width="610px" CssClass="textarea-input" Rows="2" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="td-label" width="80px">
                                    垫资能力：
                                </td>
                                <td colspan="3" width="610px">
                                    <asp:TextBox ID="TxtUnderlayAbility" Width="610px" Rows="2" TextMode="MultiLine" CssClass="textarea-input" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="td-label" width="80px">
                                    固定资产：
                                </td>
                                <td colspan="3" width="610px">
                                    <asp:TextBox ID="TxtCapital" Width="610px" Rows="2" TextMode="MultiLine" CssClass="textarea-input" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </iewc:PageView></iewc:MultiPage>
            </td>
        </tr>
        <tr>
            <td colspan="4" class="td-submit">
                <asp:Button ID="BtnSave" Text="保 存" OnClientClick="judge()" OnClick="BtnSave_Click" runat="server" />&nbsp;
                <input onclick="top.ui.closeTab();" id="btnCancel" type="button" value="取 消">
                <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
