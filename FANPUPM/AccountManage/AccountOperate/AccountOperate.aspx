<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountOperate.aspx.cs" Inherits="AccountManage_AccountOperate_AccountOperate" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>入账单列表</title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
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
//            setButton(jwTable, 'btnDel', 'btnEdit', 'btnQuery', 'hfldPurchaseChecked');
            setBtn(jwTable, 'btnDel', 'btnEdit', 'btnQuery', 'btnOperate', 'hfldPurchaseChecked');
        });

        function checktxt() {
            document.getElementById('tbxmony').value
        }
        function addPurchase() {
            parent.desktop.fund_ReqinfoEdit = window;
            var url = "/AccountManage/AccountOperate/AccountOperateAdd.aspx?Action=Add"
            toolbox_oncommand(url, "新增入账单");
        }

        function queryPurchase() {
            var url = "/AccountManage/AccountOperate/AccountOperateAdd.aspx?Action=Query&Pcode=" + document.getElementById('hfldPurchaseChecked').value;
        if (!document.getElementById('hfldPurchaseChecked')) return false;
       // window.open(url, "", height=800 , width= 600 , toolbar =no, menubar=no, scrollbars=yes, resizable=yes, location=no, status=no);
        toolbox_oncommand(url, "查看入账单");
        }

        function deletePurchase() {
            if (!document.getElementById('hfldPurchaseChecked')) return false;
            if (!confirm("确定要删除吗？")) {
                return false;
            }
        }

        function updatePurchase() {
            parent.desktop.fund_ReqinfoEdit = window;
            var url = "/AccountManage/AccountOperate/AccountOperateAdd.aspx?Action=Update&Pcode=" + document.getElementById('hfldPurchaseChecked').value;
            if (!document.getElementById('hfldPurchaseChecked')) return false;
            toolbox_oncommand(url, "编辑入账单");
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
           document.getElementById("iframeRole").src = "/Common/DivSelectContrtAccon.aspx?Method=returnContr";
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
       function setBtn(jwTable, btnDel, btnEdit, btnQuery, btnOperate,hdfid) {
           if (jwTable.table.firstChild.childNodes.length == 1) {
               //table中没有数据
               return;
           }
           jwTable.registClickTrListener(function() {
           document.getElementById(hdfid).value = this.id;
               if (this.bstate == "False") {//代表未入账
                   //document.getElementById(btnAdd).setAttribute('disabled', 'disabled');
                   document.getElementById(btnEdit).removeAttribute('disabled');
                   document.getElementById(btnQuery).removeAttribute('disabled');
                   document.getElementById(btnOperate).removeAttribute('disabled');
                   document.getElementById(btnDel).removeAttribute('disabled');
               }
               else {
                   document.getElementById(btnDel).setAttribute('disabled', 'disabled');
                   document.getElementById(btnEdit).setAttribute('disabled', 'disabled');
                   document.getElementById(btnQuery).setAttribute('disabled', 'disabled');
                   document.getElementById(btnOperate).setAttribute('disabled', 'disabled');
                }
           });
           jwTable.registClickSingleCHKListener(function() {
               var checkedChk = jwTable.getCheckedChk();
               if (checkedChk.length == 0) {
                   document.getElementById(btnDel).setAttribute('disabled', 'disabled');
                   document.getElementById(btnEdit).setAttribute('disabled', 'disabled');
                   document.getElementById(btnQuery).setAttribute('disabled', 'disabled');
                   document.getElementById(btnOperate).setAttribute('disabled', 'disabled');
               }
               else if (checkedChk.length == 1) {
                   document.getElementById(btnQuery).removeAttribute('disabled');
//                   document.getElementById(btnEdit).removeAttribute('disabled');
//                   document.getElementById(btnDel).removeAttribute('disabled');
                   if (this.parentNode.parentNode.parentNode.bstate == "False")//未入账
                   {
                       document.getElementById(btnEdit).removeAttribute('disabled');
                       document.getElementById(btnQuery).removeAttribute('disabled');
                       document.getElementById(btnOperate).removeAttribute('disabled');
                   }
                   else {//已入账
                       document.getElementById(btnDel).setAttribute('disabled', 'disabled');
                       document.getElementById(btnOperate).setAttribute('disabled', 'disabled');
                       document.getElementById(btnEdit).setAttribute('disabled', 'disabled');
                       document.getElementById(btnQuery).removeAttribute('disabled');
                   }
               }
               else {//多选行
                   //document.getElementById(btnDel).setAttribute('disabled', 'disabled');
                   document.getElementById(btnOperate).setAttribute('disabled', 'disabled');
                   document.getElementById(btnEdit).setAttribute('disabled', 'disabled');
                   document.getElementById(btnQuery).setAttribute('disabled', 'disabled');
               }

           });
           jwTable.registClickAllCHKLitener(function() {
               document.getElementById(btnDel).setAttribute('disabled', 'disabled');
               document.getElementById(btnOperate).setAttribute('disabled', 'disabled');
               document.getElementById(btnEdit).setAttribute('disabled', 'disabled');
               if (jwTable.table.rows.length == 2 && this.checked == true) {
                   document.getElementById(btnDel).removeAttribute('disabled');
                   //document.getElementById(btnEdit).removeAttribute('disabled');
                   if (jwTable.table.rows[1].bstate == "False")//未入账
                   {
                       document.getElementById(btnEdit).removeAttribute('disabled');
                       document.getElementById(btnQuery).removeAttribute('disabled');
                      // document.getElementById(btnOperate).removeAttribute('disabled');
                   }
                   else {
                       document.getElementById(btnEdit).setAttribute('disabled', 'disabled');
                       document.getElementById(btnOperate).setAttribute('disabled', 'disabled');
                       document.getElementById(btnQuery).setAttribute('disabled', 'disabled');
                   }
               }
           });
       }
       function isMoney(str) {
           var c, i, checkresult, length;
          
           str = trim(str);
           if (str == "") {
               alert("请填数字！");
           }
           length = str.length;
           checkresult = false;
           for (i = 0; i < length; i++) {
               c = str.substr(i, 1);
               if (i == 0 && c == '.') {
                   checkresult = false;
                   alert("请填数字！");
                   break;
               }
               if (i == length - 1 && c == '.') {
                   checkresult = false;
                   alert("请填数字！");
                   break;
               }
               if (((c >= '0') && (c <= '9')) || (c == '.')) {
                   checkresult = true;
               }
               else {
                   checkresult = false;
                   alert("请填数字！");
                   break;
               }
           }
           if (!checkresult) {
               return false;
               alert("请填数字！");
           }
           else {
               return true;
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
            入账操作管理
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <table class="queryTable" cellpadding="3px" cellspacing="0px">
                    <tr>
                        <td class="descTd"  visible="false">
                            凭证编码
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
                        <asp:Button ID="btnOperate" disabled="disabled" Text="入账" OnClick="BtnInAccount" runat="server" /> 
                            <asp:Button ID="btnDel" Text="删  除" Enabled="false" OnClientClick="return confirm('您确定要删除吗??')" OnClick="btnDel_Click" runat="server" />
                                 </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="pagediv">
                                <asp:GridView ID="gvMony" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvPurchaseplan_PageIndexChanging" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="AoID,Acredence,IsAccount" runat="server">
                                <Columns><asp:TemplateField HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
                                                <asp:CheckBox ID="cbAllBox" runat="server" />
                                            </HeaderTemplate><ItemTemplate>
                                                <asp:CheckBox ID="cbBox" ToolTip='<%# Convert.ToString(Eval("AoID")) %>' runat="server" />
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="账号" ItemStyle-Width="50px"><ItemTemplate>
                                                <%# GetAccount(Eval("AccountNum").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField ItemStyle-Width="50px" HeaderText="凭证编码"><ItemTemplate>
                                                <%# Eval("Acredence").ToString().ToLower() %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="入账类型" ItemStyle-Width="50px"><ItemTemplate>
                                                <%# GetAccounType(Eval("AccounType").ToString().ToLower()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="账单金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px"><ItemTemplate>
                                                <%# Eval("AccountMony").ToString() %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="入账金额" ItemStyle-Width="50px"><ItemTemplate>
                                            <asp:TextBox ID="tbxmony" Text='<%# Convert.ToString(Eval("RealMony").ToString()) %>' runat="server"></asp:TextBox>
                                                
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="部门名称" ItemStyle-Width="50px"><ItemTemplate>
                                                <%# GetDeptName(Eval("DepID").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="提交人" ItemStyle-Width="50px"><ItemTemplate>
                                                <%# GetUserName(Eval("SumitMan").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:BoundField DataField="SumiTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="提交时间" ItemStyle-Width="50px" /><asp:TemplateField HeaderText="是否入账" ItemStyle-Width="50px"><ItemTemplate>
                                                <%# (Eval("IsAccount").ToString() == "0") ? "否" : "是" %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="入账人" ItemStyle-Width="50px"><ItemTemplate>
                                                <%# (Eval("AccountMan").ToString() == "") ? "" : base.GetUserName(Eval("AccountMan").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:BoundField ItemStyle-Width="50px" DataField="AccounTime" HeaderText="入账时间" DataFormatString="{0:yyyy-MM-dd}" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
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
