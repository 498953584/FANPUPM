<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="WorkLogAdd.aspx.cs" Inherits="ProjectManage_Construction_ConstructionLogAdd" %>

<html>
<head id="Head1" runat="server"><title>施工日志</title>
       <base target="_self">
    </base>
    
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
<body class="body_popup" style="overflow-x:hidden; overflow-y:auto;">
    <form id="form1" runat="server">
        <table class="table-form" id="Table1" cellspacing="0" cellpadding="0" border="1" style="height:520px; width:650px">
            <tr>
                <td id="Td_title" class="td-head" align="center" colspan="4" runat="server">
                    工作日志管理</td>
            </tr>
            <tr>
                <td class="td-label"   style="width:104px" align="right">
                    编码：</td>
                <td align="left" >
                    <asp:TextBox ID="txtcode" runat="server"></asp:TextBox>
                    </td>
                <td class="td-label" align="right">
                    记录人：</td>
                <td align="left" >
                   <asp:TextBox ID="txtoperations" runat="server"></asp:TextBox></td>
            </tr> 
             <tr>
                <td class="td-label" style="width:104px; height: 25px;" align="right">
                     施工日期：
                </td>
                <td colspan="3" style="height: 25px">
                    <JWControl:DateBox ID="txtthisDate" Height="20px" runat="server"></JWControl:DateBox>
                    <asp:Label ID="lbWeek" runat="server"></asp:Label>
                </td>
            </tr>
             <tr>
                <td class="td-label" style="width:104px; height: 25px;" align="right">
                     天气：
                </td>
                <td colspan="3" style="height: 25px">
                    <asp:TextBox ID="txtamweather" Height="20px" runat="server"></asp:TextBox>
                </td>
            </tr>
                   <tr>
                <td class="td-label" style="width:104px; height: 25px;" align="right">
                    最高温度：
                </td>
                <td colspan="3" style="height: 25px">
                    <asp:TextBox ID="txtPart" Height="20px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td-label" style="width:104px" align="right">
                    生产情况：
                </td>
                <td colspan="3"style="height: 50px">
                    <asp:TextBox Width="100%" Height="100%" ID="txtdaycontent" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td-label" style="width:104px" align="right">
                    技术安全工作记录：
                </td>
                <td  align="left" colspan="3"style="height: 50px">
                    <asp:TextBox ID="txtbeton" Width="100%" Height="100%" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>        
            <tr>
                <td class="td-label" style="width:104px" align="right">
                    备注：</td>
                <td  align="left" colspan="3"style="height: 50px">
                    <asp:TextBox Width="100%" Height="100%" ID="txtremark" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
            <td class="td-label" style="width:104px; height: 28px;" align="right">
                附件：</td>
            <td colspan="3" style="height: 28px"><asp:TextBox ID="TBoxAnnex" ReadOnly="true" Visible="false" runat="server"></asp:TextBox>
			<asp:Literal ID="Literal1" runat="server"></asp:Literal><input id="BtnAnnex" class="button-normal" style=" margin-left :450px; margin-bottom:2px" onclick="UpFile(5)" type="button" value="编辑附件..." runat="server" />
</tr>
            <tr>
                <td class="td-submit" colspan="4">    
			<input type="hidden" id="hdnRecordId" name="hdnRecordId" style="width : 10px" runat="server" />

                    <asp:Button ID="BtnAdd" Text="保  存" OnClick="BtnAdd_Click" runat="server" />&nbsp;
                    <input id="BtnClose" onclick="javascript:window.close();" type="button" value="关  闭" name="BtnClose">
                    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
                </td>
            </tr>
        </table>
         <asp:TextBox ID="txtdatum" Width="100%" Height="100%" Visible="false" runat="server"></asp:TextBox>
         <asp:TextBox ID="txtdesign" Width="100%" Height="100%" Visible="false" runat="server"></asp:TextBox>
         <asp:TextBox Width="100%" Height="100%" ID="txtacceptance" Visible="false" runat="server"></asp:TextBox>
         <asp:TextBox Width="100%" Height="100%" ID="txtproduct" Visible="false" runat="server"></asp:TextBox>
    </form>
</body>
</html>
