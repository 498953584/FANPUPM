<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fund_ReqinfoView.aspx.cs" Inherits="AccountManage_fund_Reqinfo_fund_ReqinfoView" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>资金申请</title>
    <style type="text/css">
        </style>
    <link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />


    <script type="text/javascript" src="/Script/jquery.js"></script>

    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>

    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>

    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>

    <script type="text/javascript" src="../../Script/jw.js"></script>

    <script type="text/javascript" src="../../Script/jwJson.js"></script>

    <script type="text/javascript" src="../../Script/wf.js"></script>

    <script type="text/javascript">
        addEvent(window, 'load', function() {
//            //设置项目的宽度
//            if (getRequestParam('Action') == 'Query' || !getRequestParam('Action')) {
                setAllInputDisabled();
//            }

//            setAuditState();
        });

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <input id="hfldIncomeContractId" type="hidden" runat="server" />

    <div class="divHeader2">
        <table class="tableHeader2">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="资金申请" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div id="divContent2" class="divContent2">
        <div style="text-align: left; margin-left: 130px">
            资金申请方式&nbsp; &nbsp;
            <asp:DropDownList ID="DroType" AutoPostBack="true" runat="server"><asp:ListItem Text="按合同" Value="0" /><asp:ListItem Text="按项目" Value="1" /></asp:DropDownList>
            <asp:HiddenField ID="hdfaction" runat="server" />
        </div>
        <table id="tableContent2" class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">申请单编号<asp:HiddenField ID="HiddenField1" runat="server" />
                    <input id="hdnProjectCode" type="hidden" name="hdnProjectCode" runat="server" />

                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtContractCode" Height="15px" ReadOnly="true" Width="100%" runat="server"></asp:TextBox>
                </td>
                <td class="word" id="Pro1" visible="false" runat="server">
                    项目
                </td>
                <td class="txt" style="padding-right: 3px;" id="Pro2" visible="false" runat="server">
                    <span id="spanPrj" class="spanSelect" style="width: 100%;">
                        <input type="text" style="width: 90%; height: 15px; border: none; line-height: 16px;
                            margin: 1px 2px" readonly="readonly" id="txtProject" runat="server" />

                        <img src="/images1/iconSelect.gif" alt="选择项目" id="imgPrj" onclick="openProjPicker();" />
                    </span>
                </td>
                <td class="word" id="Pro3" runat="server">
                    合同
                </td>
                <td class="txt" style="padding-right: 3px;" id="Pro4" runat="server">
                    <span id="span2" class="spanSelect" style="width: 100%;">
                        <input type="text" style="width: 90%; height: 15px; border: none; line-height: 16px;
                            margin: 1px 2px" readonly="readonly" id="txtContr" runat="server" />

                        <img src="/images1/iconSelect.gif" alt="选择合同" id="img2" onclick="selectContr();" />
                    </span>
                </td>
            </tr>
            <tr>
                <td class="word" runat="server">申请金额
                </td>
                <td class="mustInput">
                    <input id="txtContractMoney" height="15px" width="100%" runat="server" />

                </td>
                <td class="word" runat="server">
                    申请类型
                </td>
                <td style="padding-right: 1px;">
                    <span id="spanIncomeContract" class="spanSelect" style="width: 100%;">
                        <asp:DropDownList ID="DropDownList1" Width="100%" AutoPostBack="true" runat="server"><asp:ListItem Text="启动资金" Value="0" /><asp:ListItem Text="合同款" Value="1 " /><asp:ListItem Text="拆借" Value="2" /></asp:DropDownList>
                        &nbsp; </span>
                </td>
            </tr>
            <tr>
                <td class="noteTd">申请原因
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtMainItem" TextMode="MultiLine" Height="50px" Rows="3" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="noteTd">
                备注
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtNotes" TextMode="MultiLine" Height="50px" Rows="3" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">申请时间
                </td>
                <td class="txt mustInput">
                    <JWControl:DateBox ID="txtStartDate" Height="15px" Width="100%" runat="server"></JWControl:DateBox>
                </td>
                <td class="word">
                    用款时间
                </td>
                <td class="txt mustInput">
                    <JWControl:DateBox ID="txtEndDate" Height="15px" Width="100%" runat="server"></JWControl:DateBox>
                </td>
            </tr>
            <tr>
                <td class="word">申请人员
                </td>
                <td>
                    <span id="span1" class="spanSelect" style="width: 100%;">
                        <input type="text" style="width: 90%; height: 15px; border: none; line-height: 16px;
                            margin: 1px 2px" readonly="readonly" id="txtUsercode" runat="server" />

                    </span>
                    <asp:HiddenField ID="hdfusercode" runat="server" />
                </td>
                <td class="word">
                    &nbsp;是否支付利息
                </td>
                <td>
                    <asp:RadioButton ID="RadioButton1" GroupName="dda" AutoPostBack="true" runat="server" />是
                    <asp:RadioButton ID="RadioButton2" GroupName="dda" AutoPostBack="true" Checked="true" runat="server" />否
                </td>
            </tr>
            <tr id="isshow" visible="false" runat="server"><td class="word" runat="server">
                    是否默认利率
                </td><td style="padding-right: 1px;" runat="server">
                    <asp:RadioButton ID="RadioButton3" GroupName="dd" Checked="true" AutoPostBack="true" runat="server" />是
                    <asp:RadioButton ID="RadioButton4" GroupName="dd" AutoPostBack="true" runat="server" />否 &nbsp;
                </td><td class="word" id="td1" visible="false" runat="server">
                    利率值
                </td><td class="txt" id="td2" visible="false" runat="server">
                    <asp:TextBox ID="TextBox1" Height="15px" Width="100%" runat="server"></asp:TextBox>
                </td></tr>
        </table>
    </div>
    
    </form>
</body>
</html>
