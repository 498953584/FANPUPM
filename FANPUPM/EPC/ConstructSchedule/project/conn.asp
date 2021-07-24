<% 
dim xml,objNode,objAtr,nCntChd,nCntAtr,str
Set xml=Server.CreateObject("Microsoft.XMLDOM") 
xml.Async=False 
xml.Load(Server.MapPath("/Web.config")) 

Set objNode=xml.documentElement

'nCntChd=objNode.ChildNodes(0).ChildNodes.length-1 
Set ObjAts = objNode.ChildNodes.item(0).ChildNodes.item(0).Attributes
<!--'这个可以定义asp读取xml文件的那一个值，通过传递这个值来确定读取的数据 
for i = 0 to ObjAts.length
	if ObjAts.item(i).Text = "Asp" then
		
		str= ObjAts.item(i+1).Text
		'Response.Write(str)
		exit for
	end if 
next-->



Set objAtr=Nothing 
Set objNode=Nothing 
Set xml=Nothing 
%>