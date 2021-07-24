<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Start.aspx.cs" Inherits="TenderManage_Window_Start" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>启动信息</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript">
        //选择人员
        function selectUser(id, name) {
            jw.selectOneUser({ nameinput: name, codeinput: id });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div title="项目启动基本资料" runat="server">
        <table id="table1" class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr>
                <td class="word">
                    启动日期
                </td>
                <td class="txt mustInput">
                    <JWControl:DateBox ID="txtProjStartDate" Height="15px" Width="100%" runat="server"></JWControl:DateBox>
                </td>
                <td class="word">
                    项目跟踪人
                </td>
                <td class="txt ">
                    <span class="spanSelect" style="width: 99%;">
                        <asp:TextBox ID="txtPrjDutyPersonName" Style="width: 80%; height: 15px;
                            border: none; line-height: 16px; margin: 1px 2px" runat="server"></asp:TextBox>
                        <img src="../../images/icon.bmp" style="float: right;" alt="选择" id="imgBusManager" onclick="selectUser('hfldPrjDutyPerson','txtPrjDutyPersonName');" runat="server" />

                    </span>
                    <input id="hfldPrjDutyPerson" type="hidden" style="width: 1px" runat="server" />

                </td>
            </tr>
            <tr id="tr_qdPrj" runat="server"><td class="word" runat="server">
                    项目经理
                </td><td class="txt" colspan="3" style="padding-right: 3px;" runat="server">
                    <span class="spanSelect" style="width: 99%;">
                        <asp:TextBox ID="txtStartManager" Style="width: 80%; height: 15px;
                            border: none; line-height: 16px; margin: 1px 2px" runat="server"></asp:TextBox>
                        <img src="../../images/icon.bmp" style="float: right;" alt="选择" id="img5" onclick="selectUser('hfldStartManager','txtStartManager');" />
                    </span>
                    <input id="hfldStartManager" type="hidden" style="width: 1px" runat="server" />

                </td><td class="word" runat="server">
                </td><td class="txt" runat="server">
                </td></tr>
            <tr>
                <td class="word">
                    备注
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtStartRemark" TextMode="MultiLine" Height="50px" Rows="3" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word">
                    相关附件
                </td>
                <td colspan="3" id="file_qd" style="white-space: nowrap" runat="server">
                    <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileStart" Name="附件" Class="ProjectFile" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
