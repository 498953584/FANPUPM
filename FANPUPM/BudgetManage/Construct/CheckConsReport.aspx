<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckConsReport.aspx.cs" Inherits="BudgetManage_Construct_ConstructMain" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>施工报量</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script src="/Script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Script/Budget/BudgetPait.js"></script>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            var gvConstruct = new JustWinTable('gvConstruct');
            setButton2(gvConstruct, 'btnDel', 'btnEdit', 'btnTemImport', 'hfldPurchaseChecked');
        });
        //        window.onload = function() {
        //        var gvConstruct = new JustWinTable('gvConstruct');
        //        
        //        setButton2(gvConstruct, 'btnDel', 'btnEdit', 'btnTemImport', 'hfldPurchaseChecked');
        //        
        //        }

        function setButton2(jwTable, btnDel, btnUpdate, btnQuery, hdChkId) {

            if (!jwTable.table) return;
            if (jwTable.table.firstChild.childNodes.length == 1) {
                //table中没有数据
                return;
            }
            jwTable.registClickTrListener(function () {
                if (hdChkId != '')
                    document.getElementById(hdChkId).value = this.id;
                if (this.guid) {
                    document.getElementById(btnQuery).guid = this.guid;
                }
                var checkedChk = jwTable.getCheckedChk();
                document.getElementById(btnDel).removeAttribute('disabled');
                document.getElementById(btnUpdate).removeAttribute('disabled');
                document.getElementById("btnReported").removeAttribute('disabled');

            });
            jwTable.registClickSingleCHKListener(function () {
                var checkedChk = jwTable.getCheckedChk();
                if (checkedChk.length == 0) {
                    document.getElementById(btnDel).setAttribute('disabled', 'disabled');
                    document.getElementById(btnUpdate).setAttribute('disabled', 'disabled');
                    document.getElementById("btnReported").setAttribute('disabled', 'disabled');

                }
                else if (checkedChk.length == 1) {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson(checkedChk);
                    document.getElementById(btnDel).removeAttribute('disabled');
                    document.getElementById(btnUpdate).removeAttribute('disabled');
                    document.getElementById("btnReported").removeAttribute('disabled');
                }
                else {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson(checkedChk);
                    document.getElementById(btnUpdate).setAttribute('disabled', 'disabled');
                    var checkedChks = jwTable.getCheckedChk();
                }
            });
            jwTable.registClickAllCHKLitener(function () {
                document.getElementById(btnUpdate).setAttribute('disabled', 'disabled');
                document.getElementById("btnReported").setAttribute('disabled', 'disabled');
                if (this.checked) {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson();
                    var checkedChks = jwTable.getAllChk();
                    document.getElementById(btnDel).removeAttribute('disabled');
                    document.getElementById("btnReported").removeAttribute('disabled');
                }
                else {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = '';
                    document.getElementById(btnDel).setAttribute('disabled', 'disabled');
                    document.getElementById("btnReported").setAttribute('disabled', 'disabled');
                }

                if (jwTable.table.rows.length == 2 && this.checked == true) {
                    if (hdChkId != '')
                        document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson();

                }
            });
        };

        //添加行进行显示资源信息
        var prevId;
        function showInfo(id) {
            var tab_Resource = null;
            $.ajax({
                type: 'GET',
                async: false,
                url: '/BudgetManage/Handler/ReturnConsRep.ashx?' + new Date().getTime() + '&consId=' + id + '&consTaskId=consTaskId',
                success: function (data) {
                    tab_Resource = data;
                }
            });
            var isDisplay = $('#' + id + '1').get(0);
            if (isDisplay == undefined) {
                $('#' + id).after('<tr id="' + id + '1"><td align="center" colspan="12" style="border:solid 1px #CADEED;">' + tab_Resource + '</td></tr>');
                if (prevId != undefined && prevId != id) {
                    $('#' + prevId + '1').remove();
                }
                prevId = id;
            }
            else {
                $('#' + id + '1').remove();
                isDisplay = undefined;
            }
        }    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divContent" class="divContent2" style="width: 100%; height: 99%; overflow: hidden;">
        <table style="width: 100%; height: 88%;">
            <tr>
                <td class="divFooter">
                    <asp:Label ID="lblPrjName" Font-Bold="true" Text="" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 100%; height: 100%;">
                    <div id="divBudget" class="pagediv" style="border-bottom: solid 0px red;">
                        <asp:GridView ID="gvConsTask" Height="100%" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvConsTask_RowDataBound" DataKeyNames="ConsTaskId" runat="server"><Columns><asp:TemplateField>
<HeaderTemplate>
                                        分项工作
                                    </HeaderTemplate>

<ItemTemplate>
                                        <%# Eval("TaskCode") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
                                        分项工作
                                    </HeaderTemplate>

<ItemTemplate>
                                        <%# Eval("TaskName") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
                                        总工作量
                                    </HeaderTemplate>

<ItemTemplate>
                                        <%# Eval("Quantity") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
                                        剩余工作量
                                    </HeaderTemplate>

<ItemTemplate>
                                        <%# Eval("SurplusQuantity") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px">
<HeaderTemplate>
                                        今日完成量
                                    </HeaderTemplate>

<ItemTemplate>
                                        <%# Eval("CompleteQuantity") %>
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
                                        工作内容
                                    </HeaderTemplate>

<ItemTemplate>
                                        <%# Eval("WorkContent") %>
                                    </ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hfldConsRepId" runat="server" />
    <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    </form>
</body>
</html>
