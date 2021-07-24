<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InExPlanView.aspx.cs" Inherits="AccountManage_IncomeExpensePlan_InExPlanView" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <base target="_self" />
 <script src="../../StockManage/script/JustWinTable.js" type="text/javascript"></script>

    <script src="../../StyleCss/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    
     <link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />


    <script type="text/javascript" src="/Script/jquery.js"></script>

    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>

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
<script type ="text/javascript" language ="javascript" >
    function rowQueryA(path) {
        parent.desktop.flowclass = window;
        var IEPid = document.getElementById("hdfID").value;
        path += "&IEPid=" + IEPid;
        toolbox_oncommand(path, "查看收支计划详情");
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader2">
        <table class="tableHeader">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="收支计划查看" runat="server"></asp:Label>
                    <asp:HiddenField ID="hdfID" runat="server" />
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
                <td class="txt">
                    <asp:TextBox ID="txtCode" Enabled="false" Height="15px" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    计划名称
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtName" Height="15px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    计划类型
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtType" runat="server"></asp:TextBox>
                </td>
                <td class="word">
                    指定日期
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtData" CssClass="Wdate" onfocus="WdatePicker({el:'txtData',dateFmt:'yyyy-MM-dd'})" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    项目名称
                </td>
                <td class="txt">
                    <span id="spanPrj" class="spanSelect" style="width: 100%; background-color: #FEFEF4;">
                        <input type="text" readonly="readonly" style="width: 90%; background-color: #FEFEF4;
                            height: 15px; border: none; line-height: 16px; margin: 1px 2px" id="txtProjectName" runat="server" />

                        <img src="/images1/iconSelect.gif" alt="选择项目" id="imgPrj" onclick="openProjPicker()" />
                    </span>
                    <input id="hdnProjectCode" type="hidden" name="hdnProjectCode" runat="server" />

                    <asp:HiddenField ID="hdfCropCode" runat="server" />
                    <asp:HiddenField ID="hdfCropName" runat="server" />
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
                <td style="vertical-align: top;" colspan="4">
                    <div class="pagediv">
                        <asp:GridView ID="gvwIEPInfo" CssClass="gvdata" AllowPaging="true" PageSize="5" AutoGenerateColumns="false" OnRowDataBound="gvwIEPInfo_RowDataBound" OnPageIndexChanging="gvwIEPInfo_PageIndexChanging" DataKeyNames="ID" runat="server">
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
                                            详情编号
                                        </th>
                                        <th scope="col">
                                            收支类型
                                        </th>
                                        <th scope="col">
                                            金额
                                        </th>
                                        <th scope="col">
                                            合同名称
                                        </th>
                                        <th scope="col">
                                            添加日期
                                        </th>
                                        <th scope="col">
                                            添加人
                                        </th>
                                        <th scope="col">
                                            备注
                                        </th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server" />
                                    </HeaderTemplate><ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" />
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="计划编号"><ItemTemplate>
                                        <span class="link" onclick="rowQueryA('../AccountManage/IEPInfo/IEPInfoView.aspx?id=<%# Eval("id") %>')">
                                            <%# StringUtility.GetStr(Eval("infoNum").ToString()) %>
                                        </span>
                                        <asp:HiddenField ID="hidIEPid" Value='<%# Convert.ToString(Eval("id")) %>' runat="server" />
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="收支类型"><ItemTemplate>
                                        <%# (Eval("type").ToString() == "0") ? "收" : "支" %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="金额"><ItemTemplate>
                                        <%# Eval("Money") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="添加日期"><ItemTemplate>
                                        <%# Common2.GetTime(Eval("addDate").ToString()) %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="添加人"><ItemTemplate>
                                        <%# Eval("v_xm") %>
                                    </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
                                        <%# StringUtility.GetStr(Eval("remark").ToString()) %>
                                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
