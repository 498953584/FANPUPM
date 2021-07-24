<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageConType.aspx.cs" Inherits="ContractManage_ContractType_ManageConType" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>权限设置</title>
    <style type="text/css">
        #departmentDiv
        {
            width: 40%;
            height: 100%;
            float: left;
            overflow: auto;
            border-right: solid 0px #AAAAAA;
        }
        #DivBtn
        {
            width: 20%;
            height: 100%;
            float: left;
            overflow: auto;
            border-right: solid 0px #AAAAAA;
        }
        #userNameDiv
        {
            width: 40%;
            height: 100%;
            float: left;
            overflow: auto;
            border-right: solid 0px #AAAAAA;
        }
    </style>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script src="/Script/jquery.js" type="text/javascript"></script>
    <script src="/Script/jwJson.js" type="text/javascript"></script>
    <script type="text/javascript">

        function lbselect(lb1, lb2, type) {
            lbselect2(lb1, lb2, type);
            setVal();
        }
        function lbselect2(lb1, lb2, type) {
            var a = document.getElementById(lb1);
            var b = document.getElementById(lb2);
            var types = "";
            var options = a.getElementsByTagName('OPTION');
            for (var i = 0; i < options.length; i++) {
                if (type == 1) {
                    if (options[i].selected) {
                        b.appendChild(options[i]);
                        lbselect2(lb1, lb2, type);
                    }
                }

                else {
                    b.appendChild(options[i]);
                    lbselect2(lb1, lb2, type);
                }
            }
        }

        function setVal() {
            var lst = document.getElementById('lbtValid');
            var lst1 = document.getElementById('lbtInValid');
            document.getElementById('hdValidTypeId').value = "";
            document.getElementById('hdInValidTypeId').value = "";
            for (var i = 0; i < lst.options.length; i++) {
                document.getElementById("hdValidTypeId").value += "'" + lst.options[i].value + "'" + ",";

            }
            for (var j = 0; j < lst1.options.length; j++) {
                document.getElementById("hdInValidTypeId").value += "'" + lst1.options[j].value + "'" + ",";

            }
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="tab" style="height: 95%;">
        <tr style="display: none">
            <td id="header">
                权限设置
            </td>
        </tr>
        <tr>
            <td style="height: 99%; vertical-align: top;">
                <div id="departmentDiv" style="text-align: center;">
                    无效合同类型
                    <asp:ListBox ID="lbtInValid" SelectionMode="Multiple" Style="height: 96%;
                        width: 100%; border: solid 1px #cdd4df;" runat="server"></asp:ListBox>
                </div>
                <div id="DivBtn" style="vertical-align: middle; text-align: center;">
                    <div style="height: 30%; width: 100%;">
                    </div>
                    <div style="height: 30px;">
                        <img src="../../images1/4_03.jpg" alt="添加" style="cursor: pointer;" onclick="lbselect('lbtInValid','lbtValid',1)"
                            id="btnAdd" />
                    </div>
                    <div style="height: 30px;">
                        <img src="../../images1/4_06.jpg" alt="全部" style="cursor: pointer;" onclick="lbselect('lbtInValid','lbtValid',2)"
                            id="btnAddAll" />
                    </div>
                    <div style="height: 60px;">
                    </div>
                    <div style="height: 30px;">
                        <img src="../../images1/4_08.jpg" alt="移除" style="cursor: pointer;" onclick="lbselect('lbtValid','lbtInValid',1)"
                            id="btnDel" />
                    </div>
                    <div style="height: 80px; border: solid 0px red;">
                        <img src="../../images1/4_10.jpg" alt="全部" style="cursor: pointer;" onclick="lbselect('lbtValid','lbtInValid',2)"
                            id="btnDelAll" />
                    </div>
                </div>
                <div id="userNameDiv" style="text-align: center;">
                    有效合同类型
                    <asp:ListBox ID="lbtValid" SelectionMode="Multiple" Style="height: 96%;
                        width: 100%; border: solid 1px #cdd4df;" runat="server"></asp:ListBox>
                </div>
            </td>
        </tr>
    </table>
    <div style="text-align: right">
        <asp:Button ID="btnSave" Text="确定" OnClick="btnSave_Click" runat="server" />
        <input type="button" id="btnCancel" value="取消" onclick="top.ui.closeWin();" />
    </div>
    
    <asp:HiddenField ID="hdInValidTypeId" runat="server" />
    
    <asp:HiddenField ID="hdValidTypeId" runat="server" />
    </form>
</body>
</html>
