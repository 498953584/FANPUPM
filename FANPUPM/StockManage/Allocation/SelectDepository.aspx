<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectDepository.aspx.cs" Inherits="StockManage_Allocation_SelectDepository" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>选择仓库</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript">
        //        $(document).ready(function () {
        //            $('#btnSave').click(setopnerVal);
        //            $('#btnCancel').click(function () {
        //                $(parent.document).find('.ui-icon-closethick').each(function () {
        //                    this.click();
        //                });
        //            });
        //        });

        function setNoAleave(message) {
            var message = message || '提示:您没有选择此仓库的权限';
            document.getElementById("lblText").innerHTML = "<span style='color:Red;'>" + message + "</span>";
        }

        //仓库单击事件
        function dbclick(txt, hid, flag) {
            document.getElementById("hdName").value = txt;
            document.getElementById("hdId").value = hid;
            document.getElementById("hdFlag").value = flag;
            document.getElementById("lblText").innerHTML = "";
        }

        // 点击保存后设置
        function setopnerVal() {
            var txt1 = document.getElementById("Hdnshow").value;
            var hid1 = document.getElementById("Hdnhid").value;
            var op = document.getElementById("HdnOperat").value;
            var depoCode = document.getElementById("HdnDCode").value;
            var it = document.getElementById("HdnIt").value;
            var txt = document.getElementById("hdName").value
            var hid = document.getElementById("hdId").value
            var flag = document.getElementById("hdFlag").value
            try {
                if (op != "" && depoCode != "") {
                    if (op == "Add") {
                        if (depoCode != hid) {
                            parent.document.getElementById(txt1).value = txt;
                            parent.document.getElementById(hid1).value = hid;
                            parent.document.getElementById(it).value = flag;
                            document.getElementById("HdnViewCodeList").value = "(scode='-1' and sprice='-1' and corp='-1')";
                            //alert(parent.document.getElementById("HdnViewCodeList").value);
                            parent.parent.form1.btnBind.click();
                        }
                    }
                    if (op == "Edit") {
                        if (depoCode != hid) {
                            if (confirm("系统提示：\n\n在编辑状态下两次选择的仓库不一样！如果继续执行上次选择的资源将被清除！\n\n确定继续吗？")) {
                                parent.document.getElementById(txt1).value = txt;
                                parent.document.getElementById(hid1).value = hid;
                                parent.document.getElementById(it).value = flag;
                                parent.document.getElementById("HdnViewCodeList").value = "(scode='-1' and sprice='-1' and corp='-1')";
                                parent.form1.btnAllDel.click();
                            }
                            else {
                                $(parent.document).find('.ui-icon-closethick').each(function () {
                                    this.click();
                                });
                                return false;
                            }
                        }
                    }
                }
                else {
                    parent.document.getElementById(txt1).value = txt;
                    parent.document.getElementById(hid1).value = hid;
                    parent.document.getElementById(it).value = flag;
                }
            }
            catch (ex) { }
            $(parent.document).find('.ui-icon-closethick').each(function () {
                this.click();
            });
        }

        //仓库双击
        function dblTreeNode(mEvent, text) {
            var o;
            // IE   
            if (mEvent.srcElement) {
                o = mEvent.srcElement;
            }
            // Netscape 和 Firefox
            else if (mEvent.target) {
                o = mEvent.target;
            }
            if (o.tagName == 'A' || o.tagName == 'a') {
                eval(o.nameProp);
                // 只有在没有提示信息的情况下才可以保存
                if (!$('#lblText').text())
                    saveEvent();
            }
        }

        // 保存
        function saveEvent() {
            if (typeof top.ui.callback == 'function') {
                //                var op = document.getElementById("HdnOperat").value;
                //                var depoCode = document.getElementById("HdnDCode").value;
                var name = document.getElementById("hdName").value;
                var code = document.getElementById("hdId").value;
                var flag = document.getElementById("hdFlag").value;

                top.ui.callback({ code: code, name: name, flag: flag });

                top.ui.callback = null;
            }
            top.ui.closeWin();
        }

        // 取消
        function cancelEvent() {
            top.ui.closeWin();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="tab">
        <tr>
            <td valign="top" style="height: 400px">
                <div class="pagediv">
                    <asp:TreeView ID="TVDepository" ImageSet="Msdn" ShowLines="true" ExpandDepth="-1" AutoGenerateDataBindings="false" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
                </div>
                <asp:Label ID="lblText" Text="" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right">
                <input type="button" id="btnSave" value="保存" onclick="saveEvent();" />
                <input type="button" id="btnCancel" value="取消" onclick="cancelEvent();" />
            </td>
        </tr>
    </table>
    <input type="hidden" id="Hdnshow" style="width: 1px" runat="server" />

    <input type="hidden" id="Hdnhid" style="width: 1px" runat="server" />

    <input type="hidden" id="HdnDCode" style="width: 1px" runat="server" />

    <input type="hidden" id="HdnOperat" style="width: 1px" runat="server" />

    <input type="hidden" id="Hdnno" style="width: 1px" runat="server" />

    <input type="hidden" id="HdnStockModel" style="width: 1px" runat="server" />

    <input type="hidden" id="HdnIt" style="width: 1px" runat="server" />

    <asp:HiddenField ID="hdId" runat="server" />
    <asp:HiddenField ID="hdName" runat="server" />
    <asp:HiddenField ID="hdFlag" runat="server" />
    <asp:HiddenField ID="HdnViewCodeList" runat="server" />
    </form>
</body>
</html>
