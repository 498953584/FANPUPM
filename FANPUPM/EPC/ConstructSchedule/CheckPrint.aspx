<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckPrint.aspx.cs" Inherits="EPC_ConstructSchedule_CheckPrint" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
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
                <td id="Td_title" class="td-head" align="center" colspan="4" height="30">
                    <b style="font-size:24">施工日志</b></td>
            </tr>
            
            </tr>
            <tr style="height:25px">
                <td width="17%" align="right" class="td-label">
                    编码：</td>
                <td style="text-align:left" width=33% >
                    <asp:Label ID="lblcode" Height="22px" runat="server"></asp:Label>
                    </td>
                <td align="right"  width=17% class="td-label">
                    施工人员：</td>
                <td style="text-align:left">
                   <asp:Label ID="lbloperations" Text="My God!" runat="server"></asp:Label></td>
            </tr> 
            <tr style="height:25px">
                <td class="td-label"   style="width:104px" align="right">
                    记录人：</td>
                <td style="text-align:left" >
                   <asp:Label ID="lblrecord" runat="server"></asp:Label>
                   </td>
                <td class="td-label" align="right" >
                    工程负责人：</td>
                <td style="text-align:left">
                   <asp:Label ID="lblblame" runat="server"></asp:Label></td>
            </tr> 
             <tr>
                <td class="td-label" style="width:104px; height: 25px;" align="right">
                     施工日期：
                </td>
                <td colspan="3" style="height: 25px;text-align:left" >
                    <asp:Label ID="lbltime" Height="25px" runat="server"></asp:Label>
                    <asp:Label ID="lbWeek" runat="server"></asp:Label>
                </td>
            </tr>
            <tr align="center">
            <td class="td-label">
            
            </td>           
            <td class="td-label" style="text-align:left">
            白天
            </td>           
            <td colspan="2" style="height:25px;text-align:left" class="td-label">
            夜间
            </td>           
            </tr>
             <tr>
                <td class="td-label" style="width:104px; height: 25px;" align="right">
                     天气：
                </td>
                <td colspan="1" style="height: 25px;text-align:left">
                    <asp:Label ID="lblamweather" Height="24px" Width="128px" runat="server"></asp:Label>
                </td>
                <td colspan="2" style="height: 25px;text-align:left">
                    <asp:Label ID="lblpmweather" Height="24px" Width="128px" runat="server"></asp:Label>
                 </td>
             </tr>
             <tr>
                <td class="td-label" style="width:104px; height: 25px;" align="right">
                    温度：
                </td>
                <td colspan="1" style="height: 25px;text-align:left">
                    <asp:Label ID="lblPart" Height="20px" runat="server"></asp:Label>
                </td>
                <td colspan="2" style="height: 25px;text-align:left">
                    <asp:Label ID="lblnightPart" Height="20px" runat="server"></asp:Label>
                 </td>
            </tr>
             <tr>
                <td class="td-label" style="width:104px; height: 25px;" align="right">
                    风力：</td>
                <td colspan="1" style="height: 25px;text-align:left">
                    <asp:Label ID="lbldaywindpower" Height="20px" runat="server"></asp:Label>
                 </td>
                <td colspan="2" style="height: 25px;text-align:left">
                    <asp:Label ID="lblnightwindpower" Height="20px" runat="server"></asp:Label>
                 </td>
            </tr>
            <tr>
                <td class="td-label" style="width:104px" align="right">
                    生产情况记录：
                </td>
                <td colspan="3"style="height: 50px">
                    <asp:Label Width="100%" Height="100%" ID="lbldaycontent" TextMode="MultiLine" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td-label" style="width:104px" align="right">
                 技术质量安全记录：
                </td>
                <td  align="left" colspan="3"style="height: 50px">
                    <asp:Label ID="lblbeton" Width="100%" Height="100%" TextMode="MultiLine" runat="server"></asp:Label>
                </td>
            </tr>        
            <tr>
                <td class="td-label" style="width:104px;height:50px" align="right">
                    材料构配件纪录：</td>
                <td  align="left" colspan="3"style="height: 50px">
                    <asp:Label ID="lblstuffenter" Width="100%" Height="100%" TextMode="MultiLine" runat="server"></asp:Label>
                </td>
            </tr>        
            <tr>
                <td class="td-label" style="width:104px" align="right">
                    备注：</td>
                <td  align="left" colspan="3"style="height: 50px">
                    <asp:Label Width="100%" Height="100%" ID="lblremark" TextMode="MultiLine" runat="server"></asp:Label>
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

                    <input id="BtnPrint" type="button" value="打  印" onclick="this.style.display='none';BtnClose.style.display='none';window.print();" runat="server" />
</asp:Button>&nbsp;
                    <input id="BtnClose" onclick="javascript:window.close();" type="button" value="关  闭" name="BtnClose" >
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
