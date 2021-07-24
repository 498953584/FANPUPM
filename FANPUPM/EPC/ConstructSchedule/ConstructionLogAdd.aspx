<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="ConstructionLogAdd.aspx.cs" Inherits="ProjectManage_Construction_ConstructionLogAdd" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<html>
<head id="Head1" runat="server"><title>施工日志</title>
    <base target="_self">
    </base>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/Script/Watermark/jquery_Watermark.js"></script>


    <script language="javascript" type="text/javascript">	
	function UpFile(t)
	{
	    //t=5 为 施工日记
	    var RecordCode = document.getElementById('hdnRecordId').value;//编号
		var url = "../../CommonWindow/Annex/AnnexList.aspx?mid="+t+"&rc="+RecordCode+"&at=0&type=2";			
		window.showModalDialog(url,window,'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');
	}
	function download(filepath,OriginalName)
	{
	    var url = filepath;
        try   
        {   
            var   _http=new ActiveXObject("MSXML2.XMLHTTP");   
            _http.open("Get",url,false);    
            _http.send();     

            if(_http.status==404)
            { 
                alert('文件不存在!'); 
                return false; 
            }
            else
            { 
                if(url!=null)
	            {
	                var i=url.lastIndexOf("."); 
                    var ext=url.substring(i);                      
                    if(ext!='')
                    {
                        window.open(url);
                    }
                    else if(ext=='.doc')
                    { 
                       window.open(url);
                    }
                }
                else
                {
                    alert("找不到该文件");
                }
            } 
        }
        catch(e)
        {   
            alert('该文件不存在!');  
            return false; 
        }           
  
	}
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="divContent2">
        <table width="100%">
            <tr>
                <td class="divHeader" style="width: 100%">
                    施工日志管理
                </td>
            </tr>
        </table>
        <table class="tableContent2" id="Table1" cellspacing="0" cellpadding="5px" border="0"
            width="100%">
            <tr>
                <td class="word" style="white-space: nowrap;">
                    编码：
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtcode" runat="server"></asp:TextBox>
                </td>
                <td class="word" style="white-space: nowrap;">
                    记录人：
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtoperations" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    施工日期：
                </td>
                <td colspan="3" class="txt">
                    <JWControl:DateBox ID="txtthisDate" Width="95%" runat="server"></JWControl:DateBox>
                     <asp:Label ID="lbWeek" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    天气：
                </td>
                <td colspan="3" class="txt">
                    <asp:TextBox ID="txtamweather" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    最高温度：
                </td>
                <td colspan="3" class="txt">
                    <asp:TextBox ID="txtPart" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    工作情况：
                </td>
                <td colspan="3" class="txt">
                    <asp:TextBox ID="txtdaycontent" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    工作记要：
                </td>
                <td colspan="3" class="txt">
                    <asp:TextBox ID="txtbeton" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap; vertical-align:top;">
                    附件：
                </td>
                <td colspan="3" class="txt" >
                    <MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" />
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    备注：
                </td>
                <td colspan="3" class="txt">
                    <asp:TextBox ID="txtremark" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="divFooter2">
            <table class="tableFooter2">
                <tr>
                    <td>
                        <input type="hidden" id="hdnRecordId" name="hdnRecordId" style="width: 10px" runat="server" />

                        <asp:Button ID="BtnAdd" Text="保  存" OnClick="BtnAdd_Click" runat="server" />&nbsp;
                        <input id="BtnClose" onclick="javascript:top.frameWorkArea.window.desktop.getActive().close();" type="button" value="关  闭" name="BtnClose" runat="server" />

                        <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
                    </td>
                </tr>
            </table>
        </div>
        <asp:TextBox ID="txtdatum" Width="100%" Height="100%" Visible="false" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtdesign" Width="100%" Height="100%" Visible="false" runat="server"></asp:TextBox>
        <asp:TextBox Width="100%" Height="100%" ID="txtacceptance" Visible="false" runat="server"></asp:TextBox>
        <asp:TextBox Width="100%" Height="100%" ID="txtproduct" Visible="false" runat="server"></asp:TextBox>
    </div>
    </form>
</body>
</html>
