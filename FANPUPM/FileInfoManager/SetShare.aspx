<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetShare.aspx.cs" Inherits="FileInfoManager_SetShare" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>设置共享</title>
    <style type="text/css">
        #departmentDiv
        {
            width: 40%;
            height: 100%;
            float: left;
            overflow: auto;
            border-right: solid 1px #AAAAAA;
        }
        #userDiv
        {
            width: 25%;
            height: 100%;
            float: left;
            overflow: auto;
            border-right: solid 1px #AAAAAA;
        }
        #userNameDiv
        {
            width: 34%;
            height: 100%;
            float: left;
            overflow: auto;
            border-right: solid 1px #AAAAAA;
        }
    </style>

    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>

    <script src="/Script/jquery.js" type="text/javascript"></script>
     <script src="/Script/jwJson.js" type="text/javascript"></script>

    <script type="text/javascript">
        addEvent(window, 'load', function() {
            // var userTable = new JustWinTable('gvwUser');    
            setEmptyTableStyle();       
            setIds();
        });
        function setIds() {
            obj = window.opener;
            obj.$("#gvFile tr input:[type=checkbox]").each(function() {
                if (this.checked) {
                  document.getElementById("hdIds").value+=this.parentNode.parentNode.parentNode.id+",";
                }
            });
        }

        function setEmptyTableStyle() {
            if (document.getElementById('gvwUser')) return false;
            var gvwUser = document.createElement('TABLE');
            gvwUser.className = 'gvdata';
            gvwUser.setAttribute('cellspacing', '0');
            gvwUser.setAttribute('border', '1px');
            gvwUser.setAttribute('rules', 'all');
            gvwUser.style.borderCollapse = 'collapse';

            var tr = document.createElement('TR');
            tr.className = 'header';

            var th1 = document.createElement('TH');
            th1.style.width = '20px';
            var chk = document.createElement('INPUT');
            chk.setAttribute('type', 'checkbox')
            th1.appendChild(chk);
            tr.appendChild(th1);

            var th2 = document.createElement('TH');
            var txt = document.createTextNode('姓名');
            th2.appendChild(txt);
            tr.appendChild(th2);

            gvwUser.appendChild(tr);

            var div = document.getElementById('userDiv');
            div.appendChild(gvwUser);
        }
        //删除用户
        function setDelUser(obj) {          
            var p = obj.parentNode;
            $("#gvwUser tr:[id=" + obj.id + "] input:[type=checkbox]").each(function() { this.checked = false; });
            obj.parentNode.parentNode.removeChild(obj.parentNode);

            //删除用户代码还后面的逗号分隔符，
            //为了对最后一个用户代码进行统一处理,需要先做替换操作，
            //然后再替换过来
            var userCodes = document.getElementById('hfldUserCodes').value.replace(']', ',]');
            userCodes = userCodes.replace('"' + obj.id + '",', '');
            userCodes = userCodes.replace(',]', ']')
            document.getElementById('hfldUserCodes').value = userCodes;   
        }       
        //全选按钮
        function chkAll(obj) {
            $("#gvwUser tr input:[type=checkbox]").each(function() {
                if (this.id != obj.id) {
                    this.checked = obj.checked;
                };
            });
            setChecked();
        }
        //点击复选框后增加相应的人员
        function setChecked() {
            var users = eval($('#hfldUserCodes').val());
            if (users == null) users = new Array();
            $("#gvwUser tr input:[type=checkbox]").each(function() {
                //当前用户ID
                var uid = this.parentNode.parentNode.id;
                if (uid == $("#thisUserCode").val()) return;
                if (uid == "") return;
                var uname = this.parentNode.parentNode.childNodes[1].childNodes[0].innerHTML;
                var isId = false;
                if (this.checked == true) {
                    $("#lblUserNames span").each(function() {
                        if (uid == this.id) {
                            isId = true;
                        }
                    });
                    if (isId == false) {                     
                        users.push(uid);
                        var addStr = "";
                        addStr += "<div  style='margin: 2px 2px 1px 2px; padding:2px 7px 2px 2px; border:solid 1px grey; width:180px;'>";
                        addStr += "<span>" + uname + "</span>";
                        addStr += "<span id='" + uid + "' style='float:right;cursor:pointer;' onclick='setDelUser(this)' class='userName'>×</span>";
                        addStr += "</div>";
                        $("#lblUserNames").append(addStr);
                    }
                } else {
                    //删除右边用户在GV里边没有的
                    $("#lblUserNames span[id]").each(function() {
                        if (uid == this.id) {
                            this.parentNode.parentNode.removeChild(this.parentNode);
                            for (var i = 0; i < users.length; i++) {
                                if (users[i] == uid) {
                                    users.splice(i, 1);
                                };
                            }
                        }
                    });
                }

            });
            if (users != "")
                $('#hfldUserCodes').val(array1dToJson(users));
            else
                $('#hfldUserCodes').val("");     
        }      
       
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table class="tab">
        <tr>
            <td id="header">
                权限设置
            </td>
        </tr>
        <tr>
            <td style="height: 95%; vertical-align: top;">
                <div id="departmentDiv">
                    <asp:TreeView ID="trvwDepartment" ShowLines="true" OnSelectedNodeChanged="trvwDepartment_SelectedNodeChanged" runat="server"><SelectedNodeStyle Font-Underline="true" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" /></asp:TreeView>
                </div>
                <div id="userDiv">
                    <asp:GridView ID="gvwUser" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwUser_RowDataBound" DataKeyNames="v_yhdm" runat="server"><Columns><asp:TemplateField>
<HeaderTemplate>
                                    <asp:CheckBox ID="chkAll" onclick="chkAll(this)" runat="server" />
                                </HeaderTemplate>

<ItemTemplate>
                                    <asp:CheckBox ID="chk" onclick="setChecked()" runat="server" />
                                </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="姓名"><ItemTemplate>
                                    <asp:Label ID="lblName" Text='<%# System.Convert.ToString(Eval("v_xm"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                </div>
                <div id="userNameDiv">
                    <table cellpadding="0" cellspacing="0" style="height: 100%; width: 100%">
                        <tr>
                            <td style="height: 410px; vertical-align: top;">
                                <asp:Label ID="lblUserNames" Text="" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    
    <asp:HiddenField ID="hfldUserCodes" runat="server" />
    <asp:HiddenField ID="hdIds" runat="server" />
    <asp:HiddenField ID="thisUserCode" Value="0" runat="server" />
    
    </form>
</body>
</html>
