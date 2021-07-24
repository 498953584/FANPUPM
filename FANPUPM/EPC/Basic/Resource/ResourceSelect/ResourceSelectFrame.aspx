<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResourceSelectFrame.aspx.cs" Inherits="EPC_Basic_Resource_ResourceSelect_ResourceSelectFrame" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>资源选择</title>
    <script type="text/javascript" src="../../../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../../../../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript">
    function setFrameSrc(pt,rs)
    {
        document.getElementById("resFraTree").src="ResourceTree.aspx?pt="+pt+"&rs="+rs;
    }
    
    //获得选中的资源
    function resourceCodeList()
    {
      window.opener.document.getElementById('hdnCodeList').value=document.getElementById('HdnResCodeList').value;
      window.opener.document.getElementById('btnShowList').click();
      window.close();
    }
    
    function initCheckBox()
	{
	    var DUTYIDs = document.getElementById('HdnResCodeList').value + "'";
	    if (DUTYIDs != "")
	    {
	        var arrDUTYIDs = DUTYIDs.split(',');
	        
	        var table = top.resFraList.document.getElementById('GridView1');
	        for(var i=1;i<table.rows.length;i++)
            {
                for(var j=0;j<arrDUTYIDs.length;j++)
	            {
	                if (arrDUTYIDs[j] == table.rows[i].title)
	                {
                        var cellObj = table.rows[i].cells[1].all.tags("input")[0];
                        cellObj.checked = true;
                          
                    }
	            }
                
            }
	    } 
	}
		
	function SelectCBox(objChecked,DutyID,TBName,pt)
    {
    var DUTYIDs = document.getElementById('HdnResCodeList').value;
    var table = top.resFraList.document.getElementById(TBName);
    //alert(table.rows[1].cells[2].innerText);
	
        if (objChecked)//选中时的处理
        {
            //先在选中的岗位ID串检查是否存在，如果不存在则进行追加该ＩＤ，否则不作处理
            
            if(DUTYIDs == "")
            {
                DUTYIDs = "'"+DutyID+"'"+,;
            }
            else
            {
                DUTYIDs += "'"+DutyID+"'"+,;
            }
             //alert(DUTYIDs);   
        }
        else//取消选中的处理
        {
            if(DUTYIDs != "")
            {
                DUTYIDs = DUTYIDs.replace(("'"+DutyID+"'"+,),"");
            }
        }
        
        document.getElementById('HdnResCodeList').value =DUTYIDs;
        
        DUTYIDs = DUTYIDs + "'0'"//为了下面的处理而设
        document.getElementById('resFraSelected').src = "ResourceSelected.aspx?rc="+ DUTYIDs+"&pt="+pt;
        
        
    }
        
    function SelectCBox2(objChecked,DutyID,TBName,pt)
    {
        var DUTYIDs = document.getElementById('HdnResCodeList').value;
        var table = top.resFraList.document.getElementById(TBName);
        if (!objChecked)
        {
            if(DUTYIDs != "")
            {
                DUTYIDs = DUTYIDs.replace(("'"+DutyID+"'"+,),"");
                document.getElementById('HdnResCodeList').value = DUTYIDs;
            }
            
            for(var i=1;i<table.rows.length;i++)
            {
	            var cellObj = table.rows[i].cells[0].all.tags("input")[0];
	            if (table.rows[i].title == DutyID )
	            {
	                cellObj.checked = false;
	                //alert(table.rows[i].title);
	            }
            }
            DUTYIDs = DUTYIDs + "'0'"//为了下面的处理而设
            document.getElementById('resFraSelected').src = "ResourceSelected.aspx?rc="+ DUTYIDs+"&pt="+pt;
            
            
        }
    }
    
    function selectAllChk(objChecked,TBName,pt,ispage)
    {
        document.getElementById('HdnResCodeList').value="";
        var DUTYIDs = "";
        var table = top.resFraList.document.getElementById(TBName);
	
        if (objChecked)//选中时的处理
        {
            var rowcount=table.rows.length;
            if(ispage=="1")
            {
                rowcount-=1;
            }
            for( var i=1;i<rowcount;i++)
            {
                if(DUTYIDs == "")
                {
                    DUTYIDs = "'"+table.rows[i].cells[2].innerText+"',";
                } 
                else
                {
                    DUTYIDs += "'"+table.rows[i].cells[2].innerText+"',";
                }
            }
             //alert(DUTYIDs);   
        }
        else//取消选中的处理
        {
            document.getElementById('HdnResCodeList').value="";
        }
        
        document.getElementById('HdnResCodeList').value =DUTYIDs;
        
        DUTYIDs = DUTYIDs + "'0'"//为了下面的处理而设
        document.getElementById('resFraSelected').src = "ResourceSelected.aspx?rc="+ DUTYIDs+"&pt="+pt;
    }
    
    function selectCancelCHK(objchecked)
    {
        if(!objchecked)
        {
           document.getElementById('resFraSelected').src = "ResourceSelected.aspx?rc='0'&pt=-1"; 
        }
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="tab" cellpadding="0" cellspacing="0">
        <tr>
            <td class="bottonrow">
                <asp:Button ID="btnResPerson" UseSubmitBehavior="false" Width="80px" Enabled="false" Text="人力资源" runat="server" />
                <asp:Button ID="btnResMaterial" UseSubmitBehavior="false" Width="80px" Enabled="false" Text="材料资源" runat="server" />
                <asp:Button ID="btnResMachinery" UseSubmitBehavior="false" Width="80px" Enabled="false" Text="机械资源" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="width:100%;height:406px;">
                <table cellpadding="0" cellspacing="0" border="0" style="width:100%;height:100%;">
                    <tr>
                        <td style="vertical-align:top;width:200px;height:100%;">
                            <iframe id="resFraTree" frameborder="0" name="resFraTree" width="200" height="100%" runat="server"></iframe>
                        </td>
                        <td style="height:100%;border-left:solid 1px #999999;">
                            <table cellpadding="0" cellspacing="0" border="0" style="height:100%;width:100%;">
                                <tr style="height:50%;"><td valign="top" style="   vertical-align:top;"><iframe id="resFraList" width="395px" height="200px" scrolling="no" name="resFraList" frameborder="0" src="ResourceDetails.aspx?rs=-2&pt=-1&cc=-1" runat="server"></iframe></td></tr>
                                <tr style="height:50%;"><td valign="top" style=" border-top:solid 1px #999999; "><iframe id="resFraSelected" width="395px" height="202px;" name="resFraSelected" frameborder="0" runat="server"></iframe></td></tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="bottonrow" style="text-align:right;">
                <input type="button" id="btnSave" value="保存" onclick="resourceCodeList();" />
                <input type="button" id="btnCancel" value="取消" onclick="window.close();return false;"/></td>
        </tr>
    </table>
    
    <input id="HdnResCodeList" type="hidden" style="width:2px;" runat="server" />

    <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
