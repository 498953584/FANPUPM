<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fund_ReqinfoList.aspx.cs" Inherits="AccountManage_fund_Reqinfo_fund_ReqinfoList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>资金申请列表</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
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
            addEvent(document.getElementById('btnAdd'), 'click', addPurchase);
            addEvent(document.getElementById('btnQuery'), 'click', queryPurchase);
            document.getElementById('btnDel').onclick = deletePurchase;
            addEvent(document.getElementById('btnEdit'), 'click', updatePurchase);
           // addEvent(document.getElementById('Text1'), 'dblclick', selectProject); //选择项目
            var jwTable = new JustWinTable('gvMony');
            setButton(jwTable, 'btnDel', 'btnEdit', 'btnQuery', 'hfldPurchaseChecked');
            setWfButtonState2(jwTable, 'WF_1');
        });


        function addPurchase() {
            parent.desktop.fund_ReqinfoEdit = window;
            var url = "/AccountManage/fund_Reqinfo/fund_ReqinfoEdit.aspx?Action=Add"
            toolbox_oncommand(url, "新增资金申请");
        }

        function queryPurchase() {
            var url = "/AccountManage/fund_Reqinfo/fund_ReqinfoEdit.aspx?Action=Query&Pcode=" + document.getElementById('hfldPurchaseChecked').value;
        if (!document.getElementById('hfldPurchaseChecked')) return false;
       // window.open(url, "", height=800 , width= 600 , toolbar =no, menubar=no, scrollbars=yes, resizable=yes, location=no, status=no);
        toolbox_oncommand(url, "查看资金申请");
        }

        function deletePurchase() {
            if (!document.getElementById('hfldPurchaseChecked')) return false;
            if (!confirm("确定要删除吗？")) {
                return false;
            }
        }

        function updatePurchase() {
            parent.desktop.fund_ReqinfoEdit = window;
            var url = "/AccountManage/fund_Reqinfo/fund_ReqinfoEdit.aspx?Action=Update&Pcode=" + document.getElementById('hfldPurchaseChecked').value;
            if (!document.getElementById('hfldPurchaseChecked')) return false;
            toolbox_oncommand(url, "编辑资金申请");
       }
       //选择项目
       function selectProject() {
           document.getElementById("divFramPrj").title = "选择项目";
           document.getElementById("ifFramPrj").src = "/Common/DivSelectMyProject.aspx?Method=returnPrj";
           selectnEvent('divFramPrj');
       }
       //选择项目返回值
      
       function returnPrj(id, name) {
           document.getElementById('hdnProjectCode').value = id;
           document.getElementById('txtProject').value = name;
       }
    
       function selectContr() {
           document.getElementById("divFramRole").title = "选择合同";
           document.getElementById("iframeRole").src = "/Common/DivSelectMyContrt.aspx?Method=returnContr&typevalue=addoper";
           selectnEvent('divFramRole');
       }

       //选择合同返回值
       function returnContr(id, name) {
           document.getElementById('HiddenField1').value = id;
           document.getElementById('txtContr').value = name;
       }
       function check() {
           var val1 = document.getElementById('txtStartContractPrice').value;
           var val2 = document.getElementById('txtEndContractPrice').value;
           if(val1!=""||val2!="")
           {
               if ((!/^[1-9]\d*$/.test(val1)) && (!/^[1-9]\d*$/.test(val2))) {
                   alert('请输入一个整数！');
                   document.getElementById('txtStartContractPrice').focus();
                   document.getElementById('txtStartContractPrice').value = "";
                   document.getElementById('txtEndContractPrice').value = "";

               } 
               else {
                   if (val1 > val2) {
                       alert('起始金额必须小于结束金额！');
                       document.getElementById('txtStartContractPrice').value = "";
                       document.getElementById('txtEndContractPrice').value = "";
                   }
               }

           }
           var time1 = document.getElementById('txtStartSignedTime').value;
           var time2 = document.getElementById('txtEndSignedTime').value;
           if (time1 != "" && time2 != "") {
               if (time1 > time2) {
                   alert('起始时间必须小于结束时间');
                   document.getElementById('txtStartSignedTime').value="";
                 document.getElementById('txtEndSignedTime').value="";
           }
           
           }
         
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
        <tr style="height: 20px;">
            <td class="divHeader">
                资金申请
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                    <tr>
                  
                    <td><asp:DropDownList ID="DroType" AutoPostBack="true" OnSelectedIndexChanged="DroType_SelectedIndexChanged" runat="server"><asp:ListItem Text="按合同" Value="0" /><asp:ListItem Text="按项目" Value="1" /></asp:DropDownList></td>
                    <td class="descTd" id="Pro1" runat="server">
                            <span style="word-spacing: 20px;">项 目</span>
                        </td>
                        <td colspan="2" align="left" style="border: solid 0px red;" id="Pro2" runat="server">
                            <span id="Span1" class="spanSelect" style="width: 121px; margin-left: 0px;">
                                <input type="text" id="txtProject" readonly="readonly" style="width: 98px;
                                    height: 15px; border: none; line-height: 16px; margin: 1px 2px" runat="server" />

                                <img src="/images1/iconSelect.gif" alt="选择项目" id="img1" onclick="selectProject();" />
                            </span>
                            <asp:HiddenField ID="hdnProjectCode" runat="server" />
                        </td>
                        <td class="descTd" id="Pro3" runat="server">
                            <span style="word-spacing: 20px;">合同</span>
                        </td>
                        <td colspan="2" align="left" style="border: solid 0px red;" id="Pro4" runat="server">
                            <span id="Span2" class="spanSelect" style="width: 121px; margin-left: 0px;">
                                <input type="text" id="txtContr" readonly="readonly" style="width: 98px;
                                    height: 15px; border: none; line-height: 16px; margin: 1px 2px" runat="server" />

                                <img src="/images1/iconSelect.gif" alt="选择合同" id="img2" onclick="selectContr();" />
                            </span>
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                        </td>
                        <td class="descTd"  visible="false">
                            资金申请编号
                        </td>
                        <td>
                            <asp:TextBox ID="Monycode" Width="80px" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            起始金额
                        </td>
                        <td>
                            <asp:TextBox ID="txtStartContractPrice" Width="80px" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            结束金额
                        </td>
                        <td>
                            <asp:TextBox ID="txtEndContractPrice" Width="80px" runat="server"></asp:TextBox>
                        </td>
                        <td class="descTd">
                            起始时间
                        </td>
                        <td>
                            <JWControl:DateBox ID="txtStartSignedTime" Width="80px" runat="server"></JWControl:DateBox>
                        </td>
                        <td class="descTd">
                            结束时间
                        </td>
                        <td>
                            <JWControl:DateBox ID="txtEndSignedTime" Width="80px" runat="server"></JWControl:DateBox>
                        </td>
                        <td style="border: solid 0px red;">
                            <asp:Button ID="btn_Search" Text="查询" Style="cursor: pointer;" OnClientClick="check()" OnClick="btn_Search_Click" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        <tr>
            <td style="vertical-align: top;">
                <table class="tab" style="vertical-align: top;">
                    <tr>
                        <td class="divFooter" style="text-align: left">
                            <input type="button" id="btnAdd" value="新增" />
                            <input type="button" id="btnEdit"  disabled="disabled" value="编辑" />
                            <input type="button" id="btnQuery" disabled="disabled" value="查看" />
                            <asp:Button ID="btnDel" Text="删  除" Enabled="false" OnClientClick="return confirm('您确定要删除吗??')" OnClick="btnDel_Click" runat="server" />
                                 <MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="085" BusiClass="001" runat="server" />
                                 </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="pagediv">
                                <asp:GridView ID="gvMony" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvPurchaseplan_PageIndexChanging" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="reqNum,auditState" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
                                                <asp:CheckBox ID="cbAllBox" runat="server" />
                                            </HeaderTemplate><ItemTemplate>
                                                <asp:CheckBox ID="cbBox" ToolTip='<%# Convert.ToString(Eval("reqNum")) %>' runat="server" />
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="申请方式" ItemStyle-Width="50px"><ItemTemplate>
                                                <%# (Eval("IsContr").ToString() == "0") ? "按合同" : "按项目" %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField ItemStyle-Width="50px" HeaderText="合同名称"><ItemTemplate>
                                                <%# GetWhich(Eval("PrjNum").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="申请编号" ItemStyle-Width="50px" Visible="false"><ItemTemplate>
                                                <%# Eval("reqNum").ToString().ToLower() %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="申请金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px"><ItemTemplate>
                                                <%# Eval("amount").ToString() %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="利率值" ItemStyle-Width="50px"><ItemTemplate>
                                                <%# Eval("InterestNum").ToString() %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="申请类型" ItemStyle-Width="50px"><ItemTemplate>
                                                <%# GetTypeName(Eval("reqType").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:BoundField DataField="reqDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="申请时间" ItemStyle-Width="50px" /><asp:BoundField ItemStyle-Width="50px" DataField="useDate" HeaderText="使用时间" DataFormatString="{0:yyyy-MM-dd}" /><asp:TemplateField HeaderText="流程状态" ItemStyle-Width="50px"><ItemTemplate>
                                                <%# Common2.GetState(Eval("auditState").ToString()) %>
                                            </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="divFramPrj" title="" style="display: none">
        <iframe id="ifFramPrj" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
       <div id="divFramRole" title="" style="display: none">
        <iframe id="iframeRole" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    </form>
</body>
</html>
