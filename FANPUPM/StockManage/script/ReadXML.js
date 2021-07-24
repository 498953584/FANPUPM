// JScript 文件
function loadXmlFile(xmlFile)
{
  var xmlDom = null;
  if (window.ActiveXObject)
  {
    xmlDom = new ActiveXObject("Microsoft.XMLDOM");
    //xmlDom.loadXML(xmlFile);//如果用的是XML字符串
    xmlDom.async = false;
    xmlDom.load(xmlFile);//如果用的是xml文件。
  }
  else if (document.implementation && document.implementation.createDocument)
  {
    var xmlhttp = new window.XMLHttpRequest();
    xmlhttp.open("GET", xmlFile, false);
    xmlhttp.send(null);
    xmlDom = xmlhttp.responseXML;
  }
  else{
    xmlDom = null;
  }
  return xmlDom;
}

function GetNodeValue()
{
    var xmlDom=loadXmlFile('/SalaryManage/Config/SalaryParameter.xml');
    var xmlParam=xmlDom.getElementsByTagName('param');
    var xmlCount=xmlDom.getElementsByTagName('param').length;
    var json="[{";
    for(var i=0;i<xmlCount;i++)
    {
        json+=xmlParam[i].childNodes[0].firstChild.nodeValue+":"+"\""+xmlParam[i].childNodes[1].childNodes[0].nodeValue+"\""+",";
    }
    json=json.substring(0,json.lastIndexOf(','));
    json+="}]";
    var o = eval(json);
    return (o[0]);
}

