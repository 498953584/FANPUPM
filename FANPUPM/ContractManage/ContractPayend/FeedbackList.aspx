<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FeedbackList.aspx.cs" Inherits="ContractManage_ContractPayend_FeedbackList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="com.jwsoft.pm.entpm"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>反馈列表</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript">  
    window.onload = function(){
       showTooltip('tooltip', 50);         
        }    
      
     function rowEdit()
     {
        var url="AddPayendFeedback.aspx?pid=" + document.getElementById("hfldPurchaseChecked").value;
        location=url;
        //winopen(url)
     }
      function rowAdd()
     {
        var url="AddContractPayend.aspx"
        location=url;
        //winopen(url)
     }
     function rowQuery()
     {
       var url="AddPayendFeedback.aspx?t=1&pid=" + document.getElementById("hfldPurchaseChecked").value;
       location=url;
       //winopen(url);
     }
    
       function setDisabled(jwTable,trId, btnUpdate, btnDel){
        var flowstateIndex = getFlowIndex(jwTable,'反馈状态');
        if (flowstateIndex == 0) 
            return;
        var cellVal = Trim(document.getElementById(trId).childNodes[flowstateIndex].innerText);
        if (cellVal == '已反馈') {
             document.getElementById(btnUpdate).setAttribute('disabled', 'disabled');
        }       
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="tab">
        <tr style="height: 20px;">
            <td class="headerrow">
                反馈信息列表
            </td>
        </tr>
        <tr>
            <td style="width: 100%; vertical-align:top;">
                <table border="0" class="tab">
                    
                    <tr>
                        <td style="height: 80%; vertical-align: top;">
                            <div class="pagediv">
                                <table class="viewTable">
                                    <asp:Repeater ID="rpFeedback" runat="server">
<ItemTemplate>
                                            <tr>
                                                <td style="width: 30px;">
                                                    反馈人
                                                </td>
                                                <td style="width: 100px;">
                                                    <%# PageHelper.QueryUser(this, Eval("FeedbackPerson").ToString()) %>
                                                </td>
                                                <td style="width: 30px;">
                                                    反馈状态
                                                </td>
                                                <td style="width: 100px;">
                                                    <%# WebUtil.GetFeedBackState(Eval("FeedbackState").ToString()) %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    反馈信息
                                                </td>
                                                <td colspan="3">
                                                    <span class="tooltip" title='<%# Eval("FeedbackOpinion").ToString() %>'>
                                                        <%# StringUtility.GetStr(Eval("FeedbackOpinion").ToString(), 50) %>
                                                    </span>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
</asp:Repeater>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="bottonrow">
            <td style="text-align: right; height: 20px;">
                <input type="button" id="btnReturn" onclick="location='PayendSend.aspx'" value="返回" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    </form>
</body>
</html>
