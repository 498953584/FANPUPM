<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DivSelectAllUser.aspx.cs" Inherits="Common_DivSelectAllUser" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>选择人员</title>
    <style type="text/css">
        
    </style>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../Script/jw.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            hiddenC();
            $('#btnCancel').click(function () {
                if (getRequestParam('Method') != '')
                    divClose(parent);
                else
                    top.ui.closeWin();
            });
            if (document.getElementById("hdName").value != "") {
                var names = document.getElementById("hdName").value.split(',');
                var ids = document.getElementById("hdId").value.split(',');
                for (var i = 0; i < names.length - 1; i++) {
                    var newItem = document.createElement("OPTION");
                    newItem.text = names[i];
                    newItem.value = ids[i];
                    document.getElementById("lbUser").add(newItem, i);
                }
            }
        });

        //把值存到隐藏字段里
        function setVal() {
            var a = document.getElementById('lbUser');
            var options = a.getElementsByTagName('OPTION');
            for (var i = 0; i < options.length; i++) {
                if (document.getElementById("hdId").value.indexOf(options[i].value) == -1) {
                    document.getElementById("hdId").value += options[i].value + ",";
                    document.getElementById("hdName").value += options[i].text + ",";
                    if (getRequestParam("type") == "Sms") {
                        document.getElementById("hdIdAndName").value += options[i].text + ":" + options[i].value + ",";
                    }
                }
            }
            //alert(document.getElementById("hdName").value);                       
        }
        //从隐藏字段里删除
        function delVal(id, text) {
            var val = document.getElementById("hdId").value.split(',');

            for (var i = 0; i < val.length; i++) {
                if (id == val[i]) {
                    document.getElementById("hdId").value = document.getElementById("hdId").value.replace(val[i] + ",", "");
                }
            }
            var names = document.getElementById("hdName").value.split(',');

            for (var j = 0; j < names.length; j++) {
                if (text == names[j]) {
                    document.getElementById("hdName").value = document.getElementById("hdName").value.replace(names[j] + ",", "");
                }
            }
        }
        //给父页面设置值
        function setOpenerVal() {
            var method = getRequestParam('Method'); //方法
            if (method != '') {
                parent[method](document.getElementById("hdId").value, document.getElementById("hdName").value);
                if (getRequestParam('parurl') != null && getRequestParam('parurl') != "") { alert("已成功选择人员!"); } else {
                    divClose(parent);
                }
            }
            else {
                var type = getRequestParam("type");
                if (type == "Sms") {
                    top.ui.closeWin();
                    top.ui.callback(document.getElementById("hdIdAndName").value);
                    top.ui.callback = null;
                } else {
                    $('#btnSave2').click();
                }
            }
            //alert(document.getElementById("hdIdAndName").value);
        }
        //添加options
        function lbselect(lb1, lb2, type) {
            //alert(document.getElementById("hdIdAndName").value);
            lbselect2(lb1, lb2, type);
            setVal();
        }
        function lbselect2(lb1, lb2, type) {
            var a = document.getElementById(lb1);
            var b = document.getElementById(lb2);
            var options = a.getElementsByTagName('OPTION');
            var noSelCodes = document.getElementById('hfldNoSelCodes').value;
            var selCodes = document.getElementById("hdId").value;
            for (var i = 0; i < options.length; i++) {
                if (selCodes.indexOf(options[i].value) == -1 && noSelCodes.indexOf(options[i].value) == -1) {
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
                else {
                    if (type == 1 && noSelCodes.indexOf(options[i].value) == -1) {
                        if (options[i].selected) {

                            //                            alert("对不起,该人员号码已被选中或者不存在!");
                        }
                    }
                }
            }
        }
        //删除用户
        function delUser(type) {
            var options = document.getElementById("lbUser").getElementsByTagName('OPTION');
            for (var i = 0; i < options.length; i++) {

                if (type == 1) {
                    if (options[i].selected) {
                        delVal(options[i].value, options[i].text);
                        document.getElementById("lbUser").remove(i);
                        delUser(1);
                    }
                }
                else {
                    delVal(options[i].value, options[i].text);
                    document.getElementById("lbUser").remove(i);
                    delUser(0);
                }
            }
        }
        function hiddenC() {
            if (getRequestParam("parurl") != null && getRequestParam('parurl') != "") {
                document.getElementById('btnSave').value = "选择";
                document.getElementById('btnCancel').style.display = "none";
            }
        }
   
    </script>
</head>
<body style="overflow: hidden;">
    <form id="form1" runat="server">
    <span id="lbmsg"></span>
    <div class="divContent" style="height: 450px; margin-left: 20px; overflow: hidden;">
        <table class="tableContent" cellpadding="5px" cellspacing="0">
            <tr>
                <td style="vertical-align: top; border: solid 0px red; width: 200px; text-align: left;"
                    rowspan="3">
                    <div style="font-weight: bold; margin-left: 10px; margin-top: 10px; margin-bottom: 10px;">
                        请选择</div>
                    <div id="departmentDiv" style="height: 400px; width: 200px; overflow: auto; border: solid 1px #cdd4df;">
                        <asp:TreeView ID="TvBranch" Width="100%" ShowLines="true" ExpandDepth="1" OnSelectedNodeChanged="trvwDepartment_SelectedNodeChanged" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
                    </div>
                </td>
                <td style="border: solid 0px red; height: 30px; text-align: left; vertical-align: bottom;">
                    <div style="margin-left: 10px;">
                    </div>
                </td>
            </tr>
            <tr>
                <td style="border: solid 0px red; height: 320px; vertical-align: top;">
                    <table style="height: 320px;" cellpadding="0px" cellspacing="0">
                        <tr>
                            <td style="border: solid 0px red; width: 130px; vertical-align: top; text-align: right;">
                                <div style="height: 30px; margin-right: 10px;">
                                    <asp:TextBox ID="txtQuery" BorderColor="#CDD4DF" Height="15px" Width="95px" runat="server"></asp:TextBox>
                                </div>
                                <div style="margin-right: 10px;">
                                    <asp:ListBox ID="lbSelect" Style="border: solid 1px #cdd4df;" DataTextField="v_xm" DataValueField="v_yhdm" Width="100px" Height="330px" SelectionMode="Multiple" runat="server"></asp:ListBox>
                                </div>
                            </td>
                            <td style="border: solid 0px red; width: 20px; text-align: center;">
                                <div style="vertical-align: top; border: solid 0px red; height: 130px;">
                                    <asp:Button ID="btnQuery" Text="查询" OnClick="btnQuery_Click" runat="server" />
                                </div>
                                <div style="height: 30px;">
                                    <img src="../images1/4_03.jpg" alt="添加" style="cursor: pointer;" onclick="lbselect('lbSelect','lbUser',1)"
                                        id="btnAdd" />
                                </div>
                                <div style="height: 30px;">
                                    <img src="../images1/4_06.jpg" alt="全部" style="cursor: pointer;" onclick="lbselect('lbSelect','lbUser',2)"
                                        id="btnAddAll" />
                                </div>
                                <div style="height: 60px;">
                                </div>
                                <div style="height: 30px;">
                                    <img src="../images1/4_08.jpg" alt="删除" style="cursor: pointer;" onclick="delUser(1)"
                                        id="btnDel" />
                                </div>
                                <div style="height: 80px; border: solid 0px red;">
                                    <img src="../images1/4_10.jpg" alt="全部" style="cursor: pointer;" onclick="delUser(0)"
                                        id="btnDelAll" />
                                </div>
                            </td>
                            <td style="border: solid 0px red; width: 140px; vertical-align: top; text-align: left;">
                                <div style="height: 30px;">
                                </div>
                                <div style="margin-left: 10px;">
                                    <asp:ListBox ID="lbUser" Style="border: solid 1px #cdd4df;" Width="100px" Height="330px" DataTextField="v_xm" DataValueField="v_yhdm" SelectionMode="Multiple" runat="server"></asp:ListBox>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="border: solid 0px red; text-align: right;">
                    <input type="button" id="btnSave" onclick="setOpenerVal()" value="确定" runat="server" />

                    <input type="button" id="btnCancel" value="取消" />
                </td>
            </tr>
        </table>
    </div>
    
    <asp:HiddenField ID="hdId" runat="server" />
    
    <asp:HiddenField ID="hdName" runat="server" />
    <asp:HiddenField ID="hdIdAndName" runat="server" />
    
    <asp:HiddenField ID="hfldNoSelCodes" runat="server" />
    <asp:Button ID="btnSave2" Style="display: none;" OnClick="btnSave_Click" runat="server" />
    </form>
</body>
</html>
