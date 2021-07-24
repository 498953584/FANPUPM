<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserRole.aspx.cs" Inherits="WEB_UserRole" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>权限设置</title>
    <style type="text/css">
        #departmentDiv{
            width: 40%;
            height: 100%;
            float: left;
            overflow:auto;
            border-right: solid 1px #AAAAAA;
        }
        #userDiv{
            width: 25%;
            height: 100%;
            float: left;
            overflow:auto;
            border-right: solid 1px #AAAAAA;
        }
        #userNameDiv{
            width: 34%;
            height: 100%;
            float: left;
            overflow:auto;
            border-right: solid 1px #AAAAAA;
        }
    </style>
    <script type="text/javascript" src="../Script/jquery.js"></script>

    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>

    <script type="text/javascript">
        addEvent(window, 'load', function() {
            var userTable = new JustWinTable('gvwUser');
            userTable.registClickSingleCHKListener(function() {
                var hfldUserCodes = document.getElementById('hfldUserCodes');
                hfldUserCodes.value = userTable.getCheckedChkIdJson(userTable.getCheckedChk());

            });
            userTable.registClickAllCHKLitener(function() {
                var hfldUserCodes = document.getElementById('hfldUserCodes');
                if (userTable.isCheckedAll()) {
                    hfldUserCodes.value = userTable.getCheckedChkIdJson(userTable.getCheckedChk());
                }
                else {
                    var hfldUserCodes = '';
                }
            });

            setEmptyTableStyle();
        });
        
        function setEmptyTableStyle(){
            if(document.getElementById('gvwUser')) return false;
            var gvwUser = document.createElement('TABLE');
            gvwUser.className = 'gvdata';
            gvwUser.setAttribute('cellspacing','0');
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
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table class="tab">
            <tr class="headerrow">
                <td style="height: 20px">
                    权限设置</td>
            </tr>
            <tr>
                <td style="height: 96%; vertical-align: top;">
                    <div id="departmentDiv">
                        <asp:TreeView ID="trvwDepartment" ShowLines="true" OnSelectedNodeChanged="trvwDepartment_SelectedNodeChanged" runat="server"><SelectedNodeStyle Font-Underline="true" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" /></asp:TreeView>
                    </div>
                    <div id="userDiv">
                        <asp:GridView ID="gvwUser" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwUser_RowDataBound" DataKeyNames="v_yhdm" runat="server"><Columns><asp:TemplateField>
<HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged" runat="server" />
                                    </HeaderTemplate>

<ItemTemplate>
                                        <asp:CheckBox ID="chk" AutoPostBack="true" OnCheckedChanged="chk_CheckedChanged" runat="server" />
                                    </ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="姓名"><ItemTemplate>
                                        <asp:Label ID="lblName" Text='<%# Convert.ToString(Eval("v_xm")) %>' runat="server"></asp:Label>
                                    </ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
                    </div>
                    <div id="userNameDiv">
                        <table cellpadding="0" cellspacing="0" style="height: 100%; width: 100%">
                            <tr>
                                <td style="height: 410px; vertical-align: top;">
                                    <asp:Label ID="lblUserNames" Width="170px" Text="" runat="server"></asp:Label>
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
    </form>
</body>
</html>
