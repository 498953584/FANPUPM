<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormulaEdit.aspx.cs" Inherits="HR_Salary_Controls_FormulaEdit" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>计算公式修改</title>
    <script language="javascript" type="text/javascript">
<!--
      function step_clear(){

	//alert(jsgs.value.length);
	//alert(jsgs.value.charAt(0,3));
	for(i=document.form1.jsgs.value.length;i>=0;i--){
		char1=document.form1.jsgs.value.charAt(i-1);
		//alert(char1);
		if(char1=="+"||char1=="-"||char1=="*"||char1=="/"||char1=="("||char1==")"){
			document.form1.jsgs.value=document.form1.jsgs.value.substr(0,i-1);
			//document.form1.Textarea1.value=document.form1.jsgs.value.substr(0,i-1);
			break;
		}
		if(i==0)
			all_clear();
		if(char1=="H")
			gs_sjly_count.value--;
	}	
	for(i=document.form1.Textarea1.value.length;i>=0;i--)
	{
		char1=document.form1.Textarea1.value.charAt(i-1);
		//alert(char1);
		if(char1=="+"||char1=="-"||char1=="*"||char1=="/"||char1=="("||char1==")"){
			//document.form1.jsgs.value=document.form1.jsgs.value.substr(0,i-1);
			document.form1.Textarea1.value=document.form1.Textarea1.value.substr(0,i-1);
			break;
		}
		if(i==0)
			all_clear();
		if(char1=="H")
			gs_sjly_count.value--;}
	
}
     function all_clear(){
	document.form1.jsgs.value='';
	document.form1.Textarea1.value='';
	//col.value='';
	//row.value='';
	//gs_sjly_count.value=0;
	//add.disabled=false
}


function toclick(sign){

   document.form1.Textarea1.value=document.form1.Textarea1.value+sign.options[sign.selectedIndex].text
   //alert(sign.name)
	//document.form1.Select1.selectedIndkex.text
    //document.form1.Textarea1.value
    //document.form1.jsgs.value=document.form1.Textarea1.value+document.form1.Select1.options[document.form1.Select1.selectedIndex].text;
    if (sign.name=="LBSalaryItem")
    {
     document.form1.jsgs.value=document.form1.jsgs.value+"[F"+sign.options[sign.selectedIndex].value+"]";
     //alert("1")
     }
     if (sign.name=="xjll")
     {
    document.form1.jsgs.value=document.form1.jsgs.value+"S"+sign.options[sign.selectedIndex].value;
     //alert("2")
     }
    //document.form1.jsgs.value=document.form1.jsgs.value+document.form1.Select2.options[document.form1.Select2.selectIndex].value
}
    function bmurl()
    {
        
            var len,j;
	        len = document.form1.jsgs.value.length;	        
	        for(j=0;j<len;j++)
	        {  
		        switch(document.form1.jsgs.value.charAt(j))
		        {
			        case '+':
        			
				        //strSearch="+-*/)";
				        if(j==(len-1))
				        {
					        errstr="公式错误：以‘+’结尾";
					        alert(errstr);
					        return false;
				        }
			         if (document.form1.jsgs.value.charAt(j+1)=='+'||document.form1.jsgs.value.charAt(j+1)=='-'||document.form1.jsgs.value.charAt(j+1)=='*'||document.form1.jsgs.value.charAt(j+1)=='/'||document.form1.jsgs.value.charAt(j+1)==')')
				        {
					        errstr = "公式错误：‘+’" + "后面不能跟‘" + document.form1.jsgs.value.charAt(j+1) + "’";
					        alert(errstr);
					        return false;
				        }
				        break;
			        case '-':
			            //strSearch="+-*/)";
			            if(j==(len-1))
				        {
					        errstr="公式错误：以‘-’结尾";
					        alert(errstr);
					        return false;
				        }
				        if (document.form1.jsgs.value.charAt(j+1)=='+'||document.form1.jsgs.value.charAt(j+1)=='-'||document.form1.jsgs.value.charAt(j+1)=='*'||document.form1.jsgs.value.charAt(j+1)=='/'||document.form1.jsgs.value.charAt(j+1)==')')
				        {
					        errstr = "公式错误：‘-’" + "后面不能跟‘" + document.form1.jsgs.value.charAt(j+1) + "’";
					        alert(errstr);
					        return false;
				        }
			            break;
			        case '*':
				        //strSearch="+-*/)";
				        if(j==(len-1))
				        {
					        errstr="公式错误：以‘*’结尾";
					        alert(errstr);
					        return false;
				        }
				        if (document.form1.jsgs.value.charAt(j+1)=='+'||document.form1.jsgs.value.charAt(j+1)=='-'||document.form1.jsgs.value.charAt(j+1)=='*'||document.form1.jsgs.value.charAt(j+1)=='/'||document.form1.jsgs.value.charAt(j+1)==')')
				        {
					        errstr = "公式错误：‘*’" + "后面不能跟‘" + document.form1.jsgs.value.charAt(j+1) + "’";
					        alert(errstr);
					        return false;
				        }
				        break;
			        case '/':
				        //strSearch="+-*/)";
				        if(j==(len-1))
				        {
					        errstr="公式错误：以‘/’结尾";
					        alert(errstr);
					        return false;
				        }
				        if (document.form1.jsgs.value.charAt(j+1)=='+'||document.form1.jsgs.value.charAt(j+1)=='-'||document.form1.jsgs.value.charAt(j+1)=='*'||document.form1.jsgs.value.charAt(j+1)=='/'||document.form1.jsgs.value.charAt(j+1)==')')
				        {
					        errstr = "公式错误：‘/’" + "后面不能跟‘" + document.form1.jsgs.value.charAt(j+1) + "’";
					        alert(errstr);
					        return false;
				        }
				        break;
			        case '(':
			            
				        //strSearch="+-*/";
				        if(j==(len-1))
				        {
					        errstr="公式错误：以‘（’结尾";
					        alert(errstr);
					        return false;
				        }
				        if (document.form1.jsgs.value.charAt(j+1)=='+'||document.form1.jsgs.value.charAt(j+1)=='-'||document.form1.jsgs.value.charAt(j+1)=='*'||document.form1.jsgs.value.charAt(j+1)=='/'||document.form1.jsgs.value.charAt(j+1)==')')
				        {
					        errstr = "公式错误：‘(’" + "后面不能跟‘" + document.form1.jsgs.value.charAt(j+1) + "’";
					        alert(errstr);
					        return false;
				        }
				        break;
			        case ')':
				        //strSearch="RCH";
				        if(j==(len-1))
				        {
					        //如果是公式结尾不执行
				        }
				        else{
					        if (document.form1.jsgs.value.charAt(j+1)=='R'||document.form1.jsgs.value.charAt(j+1)=='N'||document.form1.jsgs.value.charAt(j+1)=='S')
					        {
						        errstr = "公式错误：‘)’" + "后面不能跟‘" + document.form1.jsgs.value.charAt(j+1) + "’";
						        alert(errstr);
						        return false;
					        }
				        }	
				        break;
			        default:
			            
			            //bincat,如果有(，则判断是否是计算所得税
			            if(document.form1.jsgs.value.substr(0,15)== 'dbo.f_HR_GetTax')
			            {
			            }
			            //bincat，end
			            else if (document.form1.jsgs.value.charAt(j)!='R'||document.form1.jsgs.value.charAt(j)!='N'||document.form1.jsgs.value.charAt(j)!='S')
			            {
					        //strSearch="RCH(";
					        if(j==(len-1))
					        {
					        }
					        else
					        {
						        if (document.form1.jsgs.value.charAt(j+1)=='R'||document.form1.jsgs.value.charAt(j+1)=='N'||document.form1.jsgs.value.charAt(j+1)=='S'||document.form1.jsgs.value.charAt(j+1)=='(')
						        {
							        errstr = "公式错误：‘计算公式 或 所得税’" + "后面不能跟‘" + document.form1.jsgs.value.charAt(j+1) + "’";
							        alert(errstr);
							        return false;
						        }
					        }
				        }
		        }
	        }


   var sData = window.dialogArguments;
  sData.jsgs = document.form1.jsgs.value;
    sData.fnUpdate(); 
 window.close();
}
function save()
   {
    alert ("asdfsf");

   }
   
      function getTax(obj)
       {
            form1.Textarea1.value = 'dbo.f_HR_GetTax(' + form1.Textarea1.value + ')';
            //form1.jsgs.value = 'f_HR_GetTax(\'' + form1.jsgs.value + '\')';
            form1.jsgs.value = 'dbo.f_HR_GetTax(' + form1.jsgs.value + ')';
            obj.disabled = true;
       }
//-->
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table class="table-normal" cellspacing="0" cellpadding="0" width="100%" border="1">
			<tr>
             <td class="td-title" colspan="3">计算公式</td>
      </tr>
			<tr>
				<td width="100%" colspan="3" height="65" >								
					<textarea id="Textarea1" name="Textarea1" style="width:100%" cols="43" rows="4" readonly></textarea></td>
			</tr>
			<tr>
			    
				<td align="center" width="40%" height="100px">
                <input type="hidden" name="jsgs" id="jsgs" style="width: 1px" />
                    <asp:ListBox ID="LBSalaryItem" DataSourceID="SqlLBSalaryItem" DataTextField="ItemName" DataValueField="ItemID" Width="90%" Height="100%" runat="server"></asp:ListBox>
                               				
				</td>
				  <td width="30%">
			       <input type="button" name="plus" value="  +  " onclick="form1.Textarea1.value+='+';form1.jsgs.value+='+'" id="button14"/>
			       <input type="button" name="minus" value="  -  " onclick="form1.Textarea1.value+='-';form1.jsgs.value+='-'" id="button18"/>
			       <input type="button" name="times" value="  x  " onclick="form1.Textarea1.value+='*';form1.jsgs.value+='*'" id="button22"/><br />
			       <input type="button" name="div" value="  /  " onclick="form1.Textarea1.value+='/';form1.jsgs.value+='/'" id="button26"/>
			       <input type="button" name="left" value="  (  " onclick="form1.Textarea1.value+='(';form1.jsgs.value+='('" id="button27"/>
			       <input type="button" name="right" value="  )  " onclick="form1.Textarea1.value+=')';form1.jsgs.value+=')'" id="button28"/> <br/>
			       <input type="button" name="tax" value="计算所得税" onclick="javascript:getTax(this);" id="Button29"/>
			    
			    
			    </td>
				<td width="30%">
				<!-- all the buttons go here, just add as many as you like! --> 

                  <input type="button" name="one"  value="  1  " onclick="form1.Textarea1.value += '1';form1.jsgs.value+='1'" id="Button7"/> 
                  <input type="button" name="two"  value="  2  " onclick="form1.Textarea1.value += '2';form1.jsgs.value+='2'" id="Button12"/> 
                  <input type="button" name="three" value="  3  " onclick="form1.Textarea1.value += '3';form1.jsgs.value+='3'" id="Button13"/> 
                  <br/> 
                  <input type="button" name="four" value="  4  " onclick="form1.Textarea1.value += '4';form1.jsgs.value+='4'" id="Button15"/> 
                  <input type="button" name="five" value="  5  " onclick="form1.Textarea1.value += '5';form1.jsgs.value+='5'" id="Button16"/> 
                  <input type="button" name="six"  value="  6  " onclick="form1.Textarea1.value += '6';form1.jsgs.value+='6'" id="Button17"/> 
                  <br/> 
                  <input type="button" name="seven" value="  7  " onclick="form1.Textarea1.value += '7';form1.jsgs.value+='7'" id="Button19"/> 
                  <input type="button" name="eight" value="  8  " onclick="form1.Textarea1.value += '8';form1.jsgs.value+='8'" id="Button20"/> 
                  <input type="button" name="nine" value="  9  " onclick="form1.Textarea1.value += '9';form1.jsgs.value+='9'" id="Button21"/> 
                  <br/> 
                  <input type="button" name="clear" value="  C  " onclick="form1.Textarea1.value = '';form1.jsgs.value=''" id="Button23"/> 
                  <input type="button" name="zero" value="  0  " onclick="form1.Textarea1.value += '0';form1.jsgs.value+='0'" id="Button24"/>
                  <br/> 
                  </td>
			  
			</tr>
			<tr>
				
			</tr>
			<tr>
				<td colspan="3">
				</td>
			</tr>
			<tr>
				<td width="100%" colspan="3" align="center">
					<input id="Button8" type="button" value="单步清除" name="Button8" onclick="javascript:step_clear();"/>
					<input id="Button9" type="button" value="全部清除" name="Button9" onclick="form1.Textarea1.value = '';form1.jsgs.value=''"/>
				</td>
			</tr>
			<tr>
				<td width="100%" colspan="3" align="center">
					<asp:SqlDataSource ID="SqlLBSalaryItem" SelectCommand="SELECT [TempletID], [ItemID], [ItemName] FROM [HR_Salary_TempletItem] WHERE ([TempletID] = @TempletID)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="TempletID" QueryStringField="tid" Type="Int32"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
					<input id="Button10" type="button" value="确定" name="Button10" onclick="javascript:bmurl();"> 
					<input id="Button11" type="button" value="取消" name="Button11" onclick="javascript:returnValue=false;window.close();">
				</td>
			</tr>
		</table>
    </div>
    </form>
</body>
</html>
