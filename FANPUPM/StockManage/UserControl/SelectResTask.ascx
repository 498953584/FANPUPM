<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectResTask.ascx.cs" Inherits="StockManage_UserControl_SelectResTask" %>

<link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/Script/jquery.js"></script>
<script type="text/javascript" src="/Script/jquery.cookie.js"></script>
<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        //弹出DIV
        $('input[id$=btnSelectResource]').get(0).onclick = function (para, prjId) {
            var src = '';
            if (para == 'resource') {
                // 如果资源名称中含有#号，则把#号后面的字符全部删除
                if ((index = prjId.indexOf('#')) > -1) {
                    prjId = prjId.substr(index + 1);
                }

                var typeCode = $('input[id$=hfldTypeCode]').val() || '0';
                src = '/StockManage/UserControl/SelectResourceTemp.aspx?TypeCode=' + typeCode + '&resourceName=' + encodeURI(prjId);
                var buttonId = $('input[id$=hfldButtonId]').val() || '0';
                src += ('&ButtonId=' + buttonId);
                $('#ifResouece').attr('src', src);
                $('#divSelectResource').dialog({ width: 1010, height: 499, modal: true });
                document.getElementById('ui-dialog-title-divSelectResource').innerText = '选择资源';
            }
            else if (para == 'task') {
                var typeCode = $('input[id$=hfldTypeCode]').val() || '0';
                src = '/StockManage/UserControl/SelectTask.aspx?prjId=' + prjId;
                var buttonId = $('input[id$=hfldButtonId]').val() || '0';
                src += ('&ButtonId=' + buttonId);
                $('#ifResouece').attr('src', src);
                $('#divSelectResource').dialog({ width: 800, height: 460, modal: true });
                document.getElementById('ui-dialog-title-divSelectResource').innerText = '选择任务';
            } else if (para == 'MultiTask') {
                var typeCode = $('input[id$=hfldTypeCode]').val() || '0';
                src = '/StockManage/UserControl/MultiSelectTask.aspx?prjId=' + prjId;
                var buttonId = $('input[id$=hfldButtonId]').val() || '0';
                src += ('&ButtonId=' + buttonId);
                //$('#ifResouece').attr('src', src);
                top.ui._MultiSelectTask = window;
                top.ui.callback = function (info) {
                    $('#hfldResourceId').val(info.TaskIds);
                    $('#hfldType').val(info.type);
                    $('#' + info.btnBindResTask).click();
                }
                top.ui.openWin({ title: '选择任务', url: src, width: 800, height: 460 });
                //$('#divSelectResource').dialog({ width: 600, height: 460, modal: true });
                //document.getElementById('ui-dialog-title-divSelectResource').innerText = '选择任务';
            }
            // 添加选择进度任务节点
            else if (para == 'PMultiTask') {
                var typeCode = $('input[id$=hfldTypeCode]').val() || '0';
                src = '/StockManage/UserControl/SelMultiProgressTask.aspx?proVerId=' + prjId;
                var buttonId = $('input[id$=hfldButtonId]').val() || '0';
                src += ('&ButtonId=' + buttonId);
                $('#ifResouece').attr('src', src);
                top.ui._SelMultiProgressTask = window;
                top.ui.callback = function (info) {
                    $('#hfldTaskId').val(info.TaskIds);
                    $('#hfldType').val(info.type);
                    $('#' + info.btnBindResTask).click();
                }
                top.ui.openWin({ title: '选择任务', url: src, width: 800, height: 500 });
            }
            //添加ButtonID
            //            var buttonId = $('input[id$=hfldButtonId]').val() || '0';
            //            src += ('&ButtonId=' + buttonId);
            //            $('#ifResouece').attr('src', src);
            //$('#hfldType').value = para;
            //}

            return false;
        }
    });
</script>
<asp:Button ID="btnSelectResource" Style="display: none;" UseSubmitBehavior="false" Width="150px" Text="选择资源" runat="server" />
<div id="divSelectResource" title="选择" style="display: none;">
	<iframe id="ifResouece" frameborder="0" width="100%" height="100%"></iframe>
</div>
<asp:HiddenField ID="hfldTypeCode" runat="server" />
<asp:HiddenField ID="hfldButtonId" runat="server" />
<asp:HiddenField ID="hfldResourceId" runat="server" />
<asp:HiddenField ID="hfldResourceCode" runat="server" />
<asp:HiddenField ID="hfldType" Value="" runat="server" />
