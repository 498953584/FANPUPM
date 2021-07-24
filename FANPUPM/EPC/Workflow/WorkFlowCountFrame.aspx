<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="WorkFlowCountFrame.aspx.cs" Inherits="ModuleSet_Workflow_WorkFlowCountFrame" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>流程统计</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript">
        function setHeight() {
            var height = document.body.offsetHeight;
            $('#divtree').height(height - 5);
            $('#frmPage').height(height - 5);
        }	
    </script>
</head>
<body onload="setHeight();">
    <form id="form1" runat="server">
    <table style="height: 100%;" width="100%" id="tableframe">
        <tr>
            <td valign="top">
                <div style="overflow: auto; width: 100%; height: 200px" id="divtree">
                    <asp:TreeView ID="TVRole" Font-Size="12px" ShowLines="true" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
                </div>
            </td>
            <td valign="top" width="80%" style="border-bottom: solid 2px #cccccc;">
                <iframe id="frmPage" name="frmPage" src="WorkFlowCount.aspx" frameborder="0" width="100%" height="100%" runat="server"></iframe>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
