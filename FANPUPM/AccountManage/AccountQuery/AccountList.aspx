<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountList.aspx.cs" Inherits="AccountManage_AccountQuery_AccountList" %>
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
     //addEvent(document.getElementById('btnQuery'), 'click', queryBankQuery);
////            addEvent(document.getElementById('btnQuery'), 'click', queryPurchase);
////            document.getElementById('btnDel').onclick = deletePurchase;
////            addEvent(document.getElementById('btnEdit'), 'click', updatePurchase);
////           // addEvent(document.getElementById('Text1'), 'dblclick', selectProject); //选择项目
         var jwTable = new JustWinTable('gvMony');
////            setButton(jwTable, 'btnDel', 'btnEdit', 'btnQuery', 'hfldPurchaseChecked');
////            setWfButtonState2(jwTable, 'WF_1');
      });


//        function addPurchase() {
//            parent.desktop.fund_ReqinfoEdit = window;
//            var url = "/AccountManage/fund_Reqinfo/fund_ReqinfoEdit.aspx?Action=Add"
//            toolbox_oncommand(url, "新增资金申请");
//     }

        function queryBankQuery(checkvalue) {
            var url = "/AccountManage/AccountQuery/fund_accountInfoList.aspx?Action=Query&accouid=" + checkvalue
       // if (!document.getElementById('hfldPurchaseChecked')) return false;
       // window.open(url, "", height=800 , width= 600 , toolbar =no, menubar=no, scrollbars=yes, resizable=yes, location=no, status=no);
        toolbox_oncommand(url, "查看账户明细");
        }

//        function deletePurchase() {
//            if (!document.getElementById('hfldPurchaseChecked')) return false;
//            if (!confirm("确定要删除吗？")) {
//                return false;
//            }
//        }

//        function updatePurchase() {
//            parent.desktop.fund_ReqinfoEdit = window;
//            var url = "/AccountManage/fund_Reqinfo/fund_ReqinfoEdit.aspx?Action=Update&Pcode=" + document.getElementById('hfldPurchaseChecked').value;
//            if (!document.getElementById('hfldPurchaseChecked')) return false;
//            toolbox_oncommand(url, "编辑资金申请");
//       }
//       //选择项目
//       function selectProject() {
//           document.getElementById("divFramPrj").title = "选择项目";
//           document.getElementById("ifFramPrj").src = "/Common/DivSelectMyProject.aspx?Method=returnPrj";
//           selectnEvent('divFramPrj');
//       }
//       //选择项目返回值
//      
//       function returnPrj(id, name) {
//           document.getElementById('hdnProjectCode').value = id;
//           document.getElementById('txtProject').value = name;
//       }
//    
//       function selectContr() {
//           document.getElementById("divFramRole").title = "选择合同";
//           document.getElementById("iframeRole").src = "/Common/DivSelectMyContrt.aspx?Method=returnContr";
//           selectnEvent('divFramRole');
//       }

//       //选择合同返回值
//       function returnContr(id, name) {
//           document.getElementById('HiddenField1').value = id;
//           document.getElementById('txtContr').value = name;
//       }
//       function check() {
//           var val1 = document.getElementById('txtStartContractPrice').value;
//           var val2 = document.getElementById('txtEndContractPrice').value;
//           if(val1!=""||val2!="")
//           {
//               if ((!/^[1-9]\d*$/.test(val1)) && (!/^[1-9]\d*$/.test(val2))) {
//                   alert('请输入一个整数！');
//                   document.getElementById('txtStartContractPrice').focus();
//                   document.getElementById('txtStartContractPrice').value = "";
//                   document.getElementById('txtEndContractPrice').value = "";

//               } 
//               else {
//                   if (val1 > val2) {
//                       alert('起始金额必须小于结束金额！');
//                       document.getElementById('txtStartContractPrice').value = "";
//                       document.getElementById('txtEndContractPrice').value = "";
//                   }
//               }

//           }
//           var time1 = document.getElementById('txtStartSignedTime').value;
//           var time2 = document.getElementById('txtEndSignedTime').value;
//           if (time1 != "" && time2 != "") {
//               if (time1 > time2) {
//                   alert('起始时间必须小于结束时间');
//                   document.getElementById('txtStartSignedTime').value="";
//                 document.getElementById('txtEndSignedTime').value="";
//           }
//           
//           }
//         
//       }

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
                账户信息</td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <table class="tab" style="vertical-align: top;">
                    <tr>
                        <td>
                            <div class="pagediv">
                                <asp:GridView ID="gvMony" AutoGenerateColumns="false" CssClass="gvdata" AllowPaging="true" PageSize="5" ShowFooter="true" OnPageIndexChanging="gvPurchaseplan_PageIndexChanging" OnRowDataBound="gvMony_RowDataBound" DataKeyNames="accountNum" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="账号" ItemStyle-Width="50px"><ItemTemplate>
                                                <%# GetAccount(Eval("accountNum").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField ItemStyle-Width="50px" HeaderText="当前余额"><ItemTemplate>
                                                <%# GetSum(Eval("accountNum").ToString()) %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="别名" ItemStyle-Width="50px"><ItemTemplate>
                                                <%# GetAccountName(Eval("accountNum").ToString(), "0") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="状态" ItemStyle-Width="50px"><ItemTemplate>
                                                <%# GetAccountName(Eval("accountNum").ToString(), "1") %>
                                            </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="明细" ItemStyle-Width="50px"><ItemTemplate>
                                             <a id="btnQuery"  onclick="javascript:queryBankQuery('<%# Eval("accountNum").ToString() %>')">明细</a>   
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
