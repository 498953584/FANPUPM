<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FlowChart.aspx.cs" Inherits="FlowChart" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>NodeList</title>
    <script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script language="javascript" type="text/javascript">
        var frontNode = "";
        var xyPos = "";
        var postPosition = "";
        var types = "";
        function trim(value) {
            var res = String(value).replace(/^[\s]+|[\s]+$/g, '');
            return res;
        }
        function doClickRow(noteId) {
            document.getElementById('btnEdit').disabled = false;
            document.getElementById('hdnNodeID').value = noteId;
            document.getElementById('btn_Del').disabled = false;
        }
        function getFrontNode(obj) {
            var str = obj.getAttribute('tag');
            str = trim(str);
            var index = str.indexOf(";");
            frontNode = str.substring(0, index);
            str = str.substr(index + 1);
            index = str.indexOf(";");
            xyPos = str.substring(0, index);
            postPosition = str.substr(index + 1);
            document.getElementById('hdnNodeID').value = frontNode;
            document.getElementById('hdnPos').value = xyPos;
            types = obj.getAttribute('types')

            //单元格选中时改变背景颜色
            for (i = 0; i < fmNavigation.document.getElementById("tbFlowChart").rows.length; i++) {
                for (var j = 0; j < fmNavigation.document.getElementById("tbFlowChart").rows[i].cells.length; j++) {
                    fmNavigation.document.getElementById("tbFlowChart").rows[i].cells[j].style.backgroundColor = "#FFFFFF";
                }
            }
            obj.style.backgroundColor = "#EEEEEE";
        }
        function delnode() {
            if (frontNode == "" || frontNode == null) {
                alert('请选择您要删除的节点！');
                return;
            }
            else if (frontNode == "0" || frontNode == "00") {
                alert('不能删除开始或结束节点！');
                document.getElementById('hdnNodeID').value = "";
                return;
            }
            else {
                if (confirm('确定要删除当前节点吗?') == false) {
                    document.getElementById('hdnNodeID').value = "";
                    return;
                }
            }
        }
        function Reflush() {
            var htmlobj = $.ajax({ type: 'POST', async: false, url: 'NodesReflush.ashx?TemplateId=' + document.getElementById('hdnTemplateID').value });
            return htmlobj.responseText;
        }
        function indexOfInArray(e, arr) {
            for (var i = 0; i < arr.length; i++) {
                if (e == arr[i]) {
                    return i;
                }
            }
            return -1;
        }

        function openRoleEdit(op, lb) {
            var time = new Date();
            var templateId = document.getElementById('hdnTemplateID').value;
            var nodeId = 0;
            if (op == 2) {
                nodeId = frontNode;
                lb = types;
            }
            if (frontNode == "00") {
                if (op == 1)
                    alert('不能在结束节点后添加节点！');
            }
            else {
                if ((frontNode == "" || frontNode == null)) {
                    if (op == 1) {
                        alert('请选择前置节点！');
                    }
                    else {
                        alert('请选择编辑节点！');
                    }
                }
                else if (postPosition == "1" && op == 1) {
                    alert('不能添加节点！');
                }
                else if (lb == '4' && document.getElementById('hfldIsProjectRelated').value == '0') {
                    // 项目相关
                    alert('不能使用项目相关!');
                }
                else {
                    var title = op == 1 ? '新增' : '编辑';
                    lb = lb + '';
                    switch (lb) {
                        case '1':
                            title += '单人流程节点';
                            break;
                        case '2':
                            title += '多人流程节点';
                            break;
                        case '3':
                            title += '群组流程节点';
                            break;
                        case '4':
                            title += '项目相关流程节点';
                            break;
                    }

                    if (op == 1) {
                        var url = "/EPC/Workflow/NodeEdit.aspx?refresh=" + time.getTime() + "&t=" + op + "&ti=" + templateId + "&ni=" + nodeId + "&front=" + frontNode + "&xy=" + xyPos + "&lb=" + lb;
                        top.ui._flowchart = window;
                        top.ui.openWin({ title: title, url: url });
                    }
                    else if (op == 2) {
                        var url = "/EPC/Workflow/NodeEdit.aspx?refresh=" + time.getTime() + "&t=" + op + "&ti=" + templateId + "&ni=" + nodeId + "&front=" + frontNode + "&xy=" + xyPos + "&lb=" + lb;
                        top.ui._flowchart = window;
                        top.ui.openWin({ title: title, url: url });
                    }
                }
            }
        }
        function checkOrder(sourObj) {
            if (sourObj.value == "") {
                sourObj.value = 0;
            }
            if (!(new RegExp(/^[0-9]*[1-9][0-9]*$/).test(sourObj.value))) {
                alert('数据类型不正确！');
                sourObj.focus();
                return;
            }
        }
        function mouseOver(obj, img) {
            obj.src = '../../EPC/workflow/workflow_icon/' + img;
        }
        function mouseOut(obj, img) {
            obj.src = '../../EPC/workflow/workflow_icon/' + img;
        }
        function setHeight() {
            var height = document.getElementById("td-nav").clientHeight;
            document.getElementById("fmNavigation").style.height = height - 5;
        }
    </script>
</head>
<body onload="setHeight();" scroll="yes">
    <form id="Form1" method="post" runat="server">
    <input id="hdnTemplateID" type="hidden" name="hdnTemplateID" runat="server" />

    <input id="hdnNodeID" type="hidden" name="hdnNodeID" runat="server" />

    <input id="hdnPos" type="hidden" name="hdnPos" style="width: 10px" runat="server" />

    <input id="hdnFrontNode" type="hidden" name="hdnFrontNode" runat="server" />

    <table style="height: 100%; width: 100%;">
        <tr>
            <td class="divHeader" style="text-align: left" colspan="7">
                工作流程图设置
            </td>
        </tr>
        <tr>
            <td style="text-align: center; border: solid 1px #cccccc; height: 84px">
                <table style="width: 100%; height: 100%">
                    <tr style="height: 60px;">
                        <td style="cursor: hand;">
                            <asp:ImageButton ID="ImgSingle" ToolTip="个人" onmouseover="mouseOver(this,'single_over.jpg');" onmouseout="mouseOut(this,'single_page.jpg');" ImageUrl="../../EPC/Workflow/workflow_icon/single_page.jpg" OnClick="ImgSingle_Click" runat="server" />
                        </td>
                        <td style="cursor: hand;">
                            <asp:ImageButton ID="ImgMulti" ToolTip="多人" onmouseover="mouseOver(this,'muti_over.jpg');" onmouseout="mouseOut(this,'muti_page.jpg');" ImageUrl="../../EPC/Workflow/workflow_icon/muti_page.jpg" OnClick="ImgMulti_Click" runat="server" />
                        </td>
                        <td style="cursor: hand;" id="td_group" runat="server">
                            <asp:ImageButton ID="ImgGroup" onmouseover="mouseOver(this,'group_over.jpg');" onmouseout="mouseOut(this,'group_page.jpg');" ImageUrl="../../EPC/Workflow/workflow_icon/group_page.jpg" ToolTip="群组" OnClick="ImgGroup_Click" runat="server" />
                        </td>
                        <td style="cursor: hand;" id="td_project" runat="server">
                            <asp:ImageButton ID="ImgRole" ImageUrl="../../EPC/Workflow/workflow_icon/project_page.jpg" onmouseover="mouseOver(this,'project_over.jpg');" onmouseout="mouseOut(this,'project_page.jpg');" ToolTip="项目相关" OnClick="ImgRole_Click" runat="server" />
                        </td>
                        <td style="cursor: hand;" id="td_corp" runat="server">
                            <asp:ImageButton ID="ImgCorp" ImageUrl="~/EPC/Workflow/workflow_icon/corp_page.jpg" onmouseover="mouseOver(this,'corp_over.jpg');" onmouseout="mouseOut(this,'corp_page.jpg');" ToolTip="部门相关" OnClick="ImgCorp_Click" runat="server" />
                        </td>
                        <td style="cursor: hand;">
                            <asp:ImageButton ID="ImgEdit" ImageUrl="../../EPC/Workflow/workflow_icon/edit_page.jpg" onmouseover="mouseOver(this,'edit_over.jpg');" onmouseout="mouseOut(this,'edit_page.jpg');" ToolTip="编辑" OnClick="ImgEdit_Click" runat="server" />
                        </td>
                        <td style="cursor: hand;">
                            <asp:ImageButton ID="ImgDelete" ToolTip="删除" onmouseover="mouseOver(this,'delete_over.jpg');" onmouseout="mouseOut(this,'delete_page.jpg');" ImageUrl="../../EPC/Workflow/workflow_icon/delete_page.jpg" OnClick="ImgDelete_Click" runat="server" />
                        </td>
                        <td style="cursor: hand;">
                            <asp:ImageButton ID="ImgComplete" ToolTip="结束" onmouseover="mouseOver(this,'over_over.jpg');" onmouseout="mouseOut(this,'over_page.jpg');" ImageUrl="../../EPC/Workflow/workflow_icon/over_page.jpg" OnClick="ImgComplete_Click" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;" width="100%" id="td-nav">
                <iframe id="fmNavigation" name="fmNavigation" frameborder="0" style="width: 100%;
                    height: 50px" runat="server"></iframe>
            </td>
        </tr>
    </table>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    <asp:HiddenField ID="hfldIsProjectRelated" runat="server" />
    </form>
</body>
</html>
