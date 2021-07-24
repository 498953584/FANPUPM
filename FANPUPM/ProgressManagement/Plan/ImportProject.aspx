<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImportProject.aspx.cs" Inherits="demo_Import" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>导入项目</title>

    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>

    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>

    <script type="text/javascript">

        //关闭
        function closeTab(rebool) {
            var projectUID = $('#hfldProjectUID').val();
            winclose('ImportProject', '/ProgressManagement/Gantt/PlanDetail.htm?id=' + projectUID, rebool);
        }

        function isEmptyFile() {
            var fileName = $('#fupData').val();
            var result = true;
            if (fileName == '') {
                alert("系统提示：\n\n请选择文件");
                result = false;
            }
            else {
                var fileType = fileName.substring(fileName.lastIndexOf('.'));
                if (fileType != '.xml') {
                    alert("系统提示：\n\n必须选择Xml文件类型");
                    return false;
                }
            }
            return result;
        }

        //单选按钮
        function radionCheck() {
            var checked = $('#raXml').attr('checked');
            if (checked) {
                $('#fupData').removeAttr('disabled');
                $('#btnImport').removeAttr('disabled');
                $('#btnVersion').attr('disabled', 'disabled');
                $('#dropVersion').attr('disabled', 'disabled');
            }
            else {
                $('#fupData').attr('disabled', 'disabled');
                $('#btnImport').attr('disabled', 'disabled');
                $('#btnVersion').removeAttr('disabled');
                $('#dropVersion').removeAttr('disabled');
            }

        }

        function isVersion() {
            var value = $('#dropVersion').val();
            if (value == null) {
                alert("系统提示：\n\n没有历史版本");
                return false;
            }
            else {
                return true;
            }
        }
    </script>

    <style type="text/css">
        p
        {
            padding-left: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style=" text-align:center" >
    <table style="margin:0 auto; text-align:left;">
    <tr>
    <td>
    <div style="padding-left: 5px; padding-top: 5px; ">
        
        <input type="radio" id="raXml" name="import" checked="checked" onclick="radionCheck()" />
        <asp:FileUpload ID="fupData" Width="237px" runat="server" />
        <asp:Button ID="btnImport" OnClientClick="return isEmptyFile()" Text="XML导入" Style="width: auto;" OnClick="btnImport_Click" runat="server" />
        <p>
            * 只能导入从微软Project的XML文件。</p>
        <p>
            * 如果没有微软Project，可以使用本系统导出的XML文件。</p>
       <p>
            * XML覆盖当前执行版本。</p>
        <input type="radio" id="raVersion" name="import" onclick="radionCheck()" />
        <asp:DropDownList ID="dropVersion" disabled="disabled" Width="80px" runat="server"></asp:DropDownList>
        <asp:Button ID="btnVersion" Text="版本导入" Style="width: auto;" disabled="disabled" OnClientClick="return isVersion()" OnClick="btnVersion_Click" runat="server" />
        <p>
            * 选择的历史版本覆盖当前执行版本。
        </p>
       
        <div style="text-align: right; position: absolute; bottom: 2px; right: 2px;">
            <input type="button" value="关闭" onclick="closeTab(false)" />
        </div>
    </div>
    </td>
    </tr>
    </table>
    
    </div>
    <asp:HiddenField ID="hfldProjectUID" runat="server" />
    </form>
</body>
</html>
