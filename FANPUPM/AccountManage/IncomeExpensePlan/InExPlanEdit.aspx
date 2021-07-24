<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InExPlanEdit.aspx.cs" Inherits="AccountManage_IncomeExpensePlan_InExPlanEdit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
    <base target="_self" />
  <link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />

  <script src="../../StyleCss/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
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
    
    <script type ="text/javascript" >
        //选择项目
        function openProjPicker() {
            document.getElementById("divFram").title = "选择项目";
            document.getElementById("ifFram").src = "../../Common/DivSelectPrjList.aspx?is=0&prjCode=''&Method=returnPrj";
            selectnEvent('divFram');
        }
        //选择项目返回值
        function returnPrj(id, name) {
            document.getElementById('hdnProjectCode').value = id;
            document.getElementById('txtProjectName').value = name;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader2">
        <table class="tableHeader">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="收支计划" runat="server"></asp:Label>
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
                    <asp:DropDownList ID="ddlType" runat="server"><asp:ListItem Value="0" Text="月度" /><asp:ListItem Value="1" Text="季度" /><asp:ListItem Value="2" Text="年度" /></asp:DropDownList>
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
        </table>
    </div>
     <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                    <input type="button" id="btnCancel" value="取消" onclick="winclose('InExPlanEdit','InExPlanList.aspx',false)" />
                </td>
            </tr>
        </table>
    </div>
   <div id="divFram" title="" style="display: none">
        <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    </form>
</body>
</html>
