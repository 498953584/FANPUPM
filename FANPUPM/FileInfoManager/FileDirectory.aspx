<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileDirectory.aspx.cs" Inherits="FileInfoManager_FileDirectory" %>
<%@ Import Namespace="com.jwsoft.pm.entpm"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>资料目录回收站</title>

    <script type="text/javascript" src="../Script/jquery.js"></script>

    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript">

        window.onload = function() {
            var jwTable = new JustWinTable('gvFile');

            //            setButton(aa, 'btnDel', 'btnEdit', 'btnRe','hfldPurchaseChecked');
            setBtn(jwTable, 'hfldPurchaseChecked');
        }
//        function ondbSelectValue(id) {
//            location = 'FileTypeList.aspx?id=' + id;
//        }

//        function rowAleave() {
//            if (document.getElementById("hdGuid").value == "") {
//                alert('系统提示: \n\n 请选择要设置权限的节点!!');
//                return;
//            }
//            var url = "/ContractManage/SetRole.aspx?tbName=F_FileInfo&idName=id&field=UserCodes&id=" + document.getElementById("hfldPurchaseChecked").value;
//            viewopen(url);
//        }
//        function valForm() {
//            if (document.getElementById("txtFileInfo").value == "") {
//                alert("请输入文件夹名称");
//                document.getElementById("txtFileInfo").focus();
//                return false;
//            }
//            return true;
//        }
        //新增文件夹
//        function addFload() {
//            document.getElementById("hdType").value = "add";
//            document.getElementById("txtFileInfo").value = "";
//            document.getElementById("inputDiv").style.display = "block";
//            document.getElementById("lblMsg").innerHTML = "新建目录";
//        }
        //编辑文件夹
//        function editFload() {
//            document.getElementById("hdType").value = "edit";
//            document.getElementById("inputDiv").style.display = "block";
//            document.getElementById("lblMsg").innerHTML = "编辑目录";
//        }

        function setBtn(jwTable, hdChkId) {
            if (jwTable.table == undefined)
                return;
            if (jwTable.table.firstChild.childNodes.length == 1) {
                //table中没有数据
                return;
            }
            jwTable.registClickTrListener(function() {
                document.getElementById(hdChkId).value = this.id;
                setDisabledButton('');
            });
            jwTable.registClickSingleCHKListener(function() {
                var checkedChk = jwTable.getCheckedChk();
                document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson(checkedChk);
                if (checkedChk.length == 0) {
                    setDisabledButton('disabled');
                }
                else {
                    setDisabledButton('');
                }
            });
            jwTable.registClickAllCHKLitener(function() {
                document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson();
                if (this.checked) {
                    var checkedChks = jwTable.getAllChk();
                    if (checkedChks.length > 0)
                        setDisabledButton('');
                }
                else {
                    setDisabledButton('disabled');
                }
            });
        }
        //设置按钮权限
        function setDisabledButton(disabled) {
            if (disabled != undefined && disabled != '') {
                $('#btnDel').attr('disabled', 'disabled');
                $('#btnReturn').attr('disabled', 'disabled');
            }
            else {
                $('#btnDel').removeAttr('disabled');
                $('#btnReturn').removeAttr('disabled');
            }
        }
        //恢复目录检查
        function checkRecover() {
            var id = $('#hfldPurchaseChecked').val();
            var isRepeate = false;
            $.ajax({
                type: "GET",
                async: false,
                dataType: "text",
                url: "/FileInfoManager/Handler/CheckRecover.ashx?" + new Date().getTime() + "&id=" + id,
                success: function(data) {
                    if (data == "1")
                        isRepeate = true;
                }
            });
            var isOk = true;
            if (isRepeate) {
               isOk= confirm("系统提示：\n\n将覆盖与此名称相同的目录，是否继续恢复？");
           }
           return isOk;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" style="height: 100%;" cellpadding="0" cellspacing="0">
        <tr style="height: 20px;">
            <td class="divHeader">
                资料目录回收站
            </td>
        </tr>
        <tr>
            <td style="height: 94%; width: 100%; vertical-align: top;">
                <table style="height: 100%;" width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td rowspan="2" style="width: 180px; height: 100%; vertical-align: top;  border-right: solid 1px #AAAAAA;">
                            <div id="departmentDiv" style="width: 180px; height: 100%; border: solid 0px red;
                                overflow: auto;">
                                <asp:TreeView ID="tvFile" ExpandDepth="1" ShowLines="true" OnSelectedNodeChanged="tvFile_SelectedNodeChanged" runat="server"><NodeStyle HorizontalPadding="5px" /><SelectedNodeStyle HorizontalPadding="5px" BackColor="#3399FF" ForeColor="White" /></asp:TreeView>
                            </div>
                        </td>
                        <td style="vertical-align: top; height: 100%;">
                            <table style="width: 100%; height: 100%;">
                                <tr>
                                    <td class="divFooter">
                                        <table>
                                            <tr>
                                                <td>
                                                   
                                                    <asp:Button ID="btnDel" disabled="disabled" Width="80px" Text="彻底删除" style="cursor:pointer;" OnClientClick="return confirm('您确定要删除吗？')" OnClick="btnDel_Click" runat="server" />
                                                    <asp:Button ID="btnReturn" Text="恢复" style="cursor:pointer;" disabled="disabled" OnClientClick="return checkRecover()" OnClick="btnReturn_Click" runat="server" />    
                                                   
                                                </td>

                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 95%;">
                                        <div style="height: 100%; overflow: auto;">
                                            <asp:GridView CssClass="gvdata" ID="gvFile" AutoGenerateColumns="false" AllowPaging="true" OnRowDataBound="gvFile_RowDataBound" OnPageIndexChanging="gvFile_PageIndexChanging" DataKeyNames="id,fileName" runat="server"><Columns><asp:TemplateField>
<HeaderTemplate>
                                                            <asp:CheckBox ID="cbAllBox" runat="server" />
                                                        </HeaderTemplate>

<ItemTemplate>
                                                            <asp:CheckBox ID="cbBox" runat="server" />
                                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
</asp:TemplateField><asp:TemplateField><HeaderTemplate>
                                                            目录名称
                                                        </HeaderTemplate><ItemTemplate>
                                                            <a href='#' onclick='ondbSelectValue(this.parentNode.parentNode.id)'>
                                                                <%# Eval("FileName") %>
                                                            </a>
                                                        </ItemTemplate></asp:TemplateField><asp:TemplateField><HeaderTemplate>
                                                            资料权限
                                                        </HeaderTemplate><ItemTemplate>
                                                            <asp:TextBox ID="TextBox1" Style="height: 15px; border: solid 0px red;" TextMode="MultiLine" Text='<%# System.Convert.ToString(base.GetUserName(Eval("userCodes").ToString()), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px"><HeaderTemplate>
                                                            目录创建者
                                                        </HeaderTemplate><ItemTemplate>
                                                            <%# PageHelper.QueryUser(this, Eval("FileOwner").ToString()) %>
                                                        </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="70px"><HeaderTemplate>
                                                            创建时间
                                                        </HeaderTemplate><ItemTemplate>
                                                            <%# Eval("CreateTime") %>
                                                        </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdGuid" runat="server" />
    <asp:HiddenField ID="hdId" runat="server" />
    <asp:HiddenField ID="hdUserCodes" runat="server" />
    <asp:HiddenField ID="hdType" runat="server" />
    <asp:Button ID="btnRe" Style="display: none;" runat="server" />
    <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    </form>
</body>
</html>
