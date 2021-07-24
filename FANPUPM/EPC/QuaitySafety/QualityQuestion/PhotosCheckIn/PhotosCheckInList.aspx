<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PhotosCheckInList.aspx.cs" Inherits="EPC_ProjectCost_PhotosCheckInList" %>

<html>
<head runat="server"><title>拍照监督登记</title><link Href="../../../../StyleCss/PM4.css" />

    <script type="text/javascript" src="../../../../Web_Client/TreeNew.js"></script>
    <script type="text/javascript" src="../../../../Script/jquery.js"></script>
    <script language="JavaScript" type="text/javascript">
        function clickRow(obj, IntendanceGuid, state, QuestionTag) {
            document.getElementById("hdnIntendanceGuid").value = IntendanceGuid;
            if (document.getElementById("hdnflag").value == 1) {

                if (state != 2) {
                    if (state == 0) {
                        document.getElementById('btnUpdate').disabled = false;
                        if (QuestionTag == 0) { //是否有回复
                            document.getElementById('btnDel').disabled = false; //没有回复可以删除
                        } else {
                            document.getElementById('btnDel').disabled = true;
                        }
                      
                    } else {
                        document.getElementById('btnUpdate').disabled = true;
                        document.getElementById('btnDel').disabled = true;
                      
                    }
                } else {
                    //document.getElementById("btnValidate").disabled = false;
                    document.getElementById('btnUpdate').disabled = true;
                    document.getElementById('btnDel').disabled = true;
                }
            } else if (document.getElementById("hdnflag").value == 2) {
                document.getElementById('btnView').disabled = false;

            }
            else if (document.getElementById("hdnflag").value == 3) {
                if (state == 1 || state == 0) {
                    document.getElementById('btnSettle').disabled = false;
                } else {
                    document.getElementById('btnSettle').disabled = true;
                }
            }
            document.getElementById('btnView').disabled = false;
            /*在这之前添加你的处理代码*/
            doClick(obj, 'grdModules'); //调用样式设置
        }
        function outRow(obj) {
            /*在这之前添加你的处理代码*/
            doMouseOut(obj); //调用样式设置
        }
        function overRow(obj) {
            /*在这之前添加你的处理代码*/
            doMouseOver(obj); //调用样式设置
        }

        function switchDisplay(obj, t1, t2) {
            /*在这之前添加你的处理代码*/
            doSwitchDisplay(obj, 'grdModules', 'hdnModuleCodeList', t1, t2, '../../../'); //调用样式设置
        }

        function validate() {
            divTdHeight(1, 0, 0, 'dvModulesBox')
        }
        var projectCode = "<%=ProjectCode %>";
        var pn = "<%=ProjectName %>";
        function ClickBtn(op) {
            var IntendanceGuid = document.getElementById('hdnIntendanceGuid').value;
            var url = ""
            var re = true;

            var w = screen.availWidth;
            var h = screen.availHeight;

            var p = "top=0,left=0,width=" + w + "px,height=" + h + "px,toolbar=no,status=yes,scrollbars=yes,resizable=yes";
            switch (op.toLowerCase()) {
                case "add":
                    url = "PhotosCheckInEdit.aspx?op=add&prj=" + projectCode + "&pn=" + escape(pn) + "&IntendanceGuid=" + IntendanceGuid;
                    re = window.showModalDialog(url, window, 'dialogHeight:' + h + 'px;dialogWidth:' + w + 'px;center:1;help:0;status:0;');
                    //  window.open(url, '', p);

                    break;
                case "upd":
                    url = "PhotosCheckInEdit.aspx?op=upd&prj=" + projectCode + "&pn=" + escape(pn) + "&IntendanceGuid=" + IntendanceGuid;
                    // window.open(url, '', p);

                    re = window.showModalDialog(url, window, 'dialogHeight:' + h + 'px;dialogWidth:' + w + 'px;center:1;help:0;status:0;');
                    break;
                case "view":
                    // url = "PhotosCheckInEdit.aspx?op=view&prj=" + projectCode + "&pn=" + escape(pn) + "&IntendanceGuid=" + IntendanceGuid;
                    url = "SettleQuestionEdit.aspx?op=add&prj=" + projectCode + "&pn=" + escape(pn) + "&IntendanceGuid=" + IntendanceGuid + "&t=2";
                    re = window.showModalDialog(url, window, 'dialogHeight:' + h + 'px;dialogWidth:' + w + 'px;center:1;help:0;status:0;');
                    //window.open(url, '', p);
                    break;

                case "settle": //解决问题
                    url = "SettleQuestionEdit.aspx?prj=" + projectCode + "&pn=" + escape(pn) + "&IntendanceGuid=" + IntendanceGuid + "&t=0";

                    re = window.showModalDialog(url, window, 'dialogHeight:' + h + 'px;dialogWidth:' + w + 'px;center:1;help:0;status:0;');
                    // window.open(url, '', p);
                    break;
                case "v": //验证
                    url = "SettleQuestionEdit.aspx?op=add&prj=" + projectCode + "&pn=" + escape(pn) + "&IntendanceGuid=" + IntendanceGuid + "&t=1";
                    re = window.showModalDialog(url, window, 'dialogHeight:' + h + 'px;dialogWidth:' + w + 'px;center:1;help:0;status:0;');
                    // window.open(url, '', p);
                    break;

            }
            return re;
        }
  
    </script>

    

    <script language="javascript" type="text/javascript">
        function blur_func(chcid, txtId) {

            if (document.getElementById(txtId).value != '') {
                document.getElementById(chcid).checked = true;
            } else {
                document.getElementById(chcid).checked = false;
            }
        }
        function blur_checkBox(chcid, txtId) {
            if (document.getElementById(chcid).checked == false) {
                document.getElementById(txtId).value = "";
            }
        }

        //设置时间复选框
        function blur_Date(beginId, endId, chcId) {
            if (document.getElementById(beginId).value != '' || document.getElementById(endId).value != '') {
                document.getElementById(chcId).checked = true;
            } else if (document.getElementById(beginId).value == '' && document.getElementById(endId).value == '') {
                document.getElementById(chcId).checked = false;
            }
        }
        function blur_checkDate(beginId, endId, chcId) {
            if (document.getElementById(chcId).checked == false) {
                document.getElementById(beginId).value = "";
                document.getElementById(endId).value = "";
            }
        }
    </script>

</head>
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0" scroll="no">
    <form id="form1" runat="server">
    <table id="Table2" class="table-normal" cellspacing="0" cellpadding="0" width="100%">
        <tr>
            <td class="td-search">
                
                <input id="chcDate" type="checkbox" onclick="blur_checkDate('txtBeginDate','txtEndDate','chcDate')" runat="server" />

                登记时间:<JWControl:DateBox ID="txtBeginDate" onblur="return blur_Date('txtBeginDate','txtEndDate','chcDate')" Width="60px" CssClass="text-input-write" runat="server"></JWControl:DateBox>-
                <JWControl:DateBox ID="txtEndDate" Width="60px" onblur="return blur_Date('txtBeginDate','txtEndDate','chcDate')" CssClass="text-input-write" runat="server"></JWControl:DateBox>
                <input id="chcTitle" type="checkbox" onclick="blur_checkBox('chcTitle','txtTitle')" runat="server" />

                问题标题:
                <input id="txtTitle" type="text" class="text-input-write" style="width: 80px;" onkeyup="return blur_func('chcTitle','txtTitle');" onchange="return blur_func('chcTitle','txtTitle');" runat="server" />

                <asp:CheckBox ID="chcState" runat="server" />处理状态:<asp:DropDownList ID="ddlState" runat="server"><asp:ListItem Value="0" Text="未解决" /><asp:ListItem Value="1" Text="解决中" /><asp:ListItem Value="2" Text="已解决" /><asp:ListItem Value="3" Text="已解决并验证" /></asp:DropDownList>
                <br />
                <asp:CheckBox ID="chcType" runat="server" />
                问题类别:<asp:DropDownList ID="ddlType" DataTextField="CodeName" DataValueField="CodeID" runat="server"></asp:DropDownList>
                <asp:Button ID="BtnSearch" CssClass="td-search-button" Text="" OnClick="BtnSearch_Click" runat="server" />
            </td>
        </tr>
        <tr class="td-toolsbar">
            <td>
                <asp:Button ID="btnAdd" Text="添加" CssClass="button-normal" OnClick="btnAdd_Click" runat="server" />
                <asp:Button ID="btnUpdate" Enabled="false" Text="编辑" CssClass="button-normal" OnClick="btnUpdate_Click" runat="server" />
                <asp:Button ID="btnDel" Enabled="false" Text="删除" CssClass="button-normal" OnClick="btnDel_Click" runat="server" />
                <asp:Button ID="btnValidate" Text="验证" CssClass="button-normal" Enabled="false" OnClick="btnValidate_Click" runat="server" />
                <asp:Button ID="btnSettle" Text="解决问题" CssClass="button-normal" Enabled="false" OnClick="btnSettle_Click" runat="server" />
                <asp:Button ID="btnView" Text="查看" CssClass="button-normal" Enabled="false" runat="server" />
            </td>
        </tr>
    </table>
    <table id="Table1" cellspacing="0" cellpadding="0" align="center" border="0" class="table-none">
        <tr>
            <td colspan="2" class="td-title">
                <asp:Label ID="lblTitle" Text="" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div id="dvModulesBox" class="gridBox" style="height: 100%">
                    <asp:DataGrid ID="grdModules" DataKeyField="IntendanceGuid" AutoGenerateColumns="false" CellPadding="0" CssClass="grid" OnItemDataBound="grdModules_ItemDataBound" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn HeaderText="序号" DataField=""></asp:BoundColumn><asp:BoundColumn HeaderText="问题标题" DataField="QuestionTitle"></asp:BoundColumn><asp:BoundColumn HeaderText="登记时间" DataFormatString="{0:d}" DataField="BookInDate"></asp:BoundColumn><asp:BoundColumn HeaderText="登记人" DataField="v_xm"></asp:BoundColumn><asp:BoundColumn HeaderText="处理状态" DataField="SettleState"></asp:BoundColumn><asp:BoundColumn HeaderText="问题类别" DataField="CodeName"></asp:BoundColumn></Columns></asp:DataGrid>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdnIntendanceGuid" runat="server" />
    <asp:HiddenField ID="hdnflag" runat="server" />
    <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
