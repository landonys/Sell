//дcookie
function setCookie(name,value,expires){
var exp=new Date();
exp.setTime(exp.getTime()+expires*24*60*60*1000); //��
document.cookie=name+"="+escape(value)+";expires="+exp.toGMTString()+";path=/";
} 

//��ȡcookie
function readcookie(name){
var oRegex=new RegExp(name+'=([^;]+)','i');
var oMatch=oRegex.exec(document.cookie);
if(oMatch&&oMatch.length>1)return unescape(oMatch[1]);
else return '';
}

//��ȡurl��"?"������ִ�
function GetRequest() {
   var url = location.search; //��ȡurl��"?"������ִ�
   var theRequest = new Object();
   if (url.indexOf("?") != -1) {
      var str = url.substr(1);
      strs = str.split("&");
      for(var i = 0; i < strs.length; i ++) {
         theRequest[strs[i].split("=")[0]]=unescape(strs[i].split("=")[1]);
      }
   }
   return theRequest;
}

//��ȡurl��"?"������ִ�
function GetRequesta(aaa) {
   var bbb= aaa.indexOf('?');
   var url=aaa.substr(bbb);
   var theRequest = new Object();
   if (url.indexOf("?") != -1) {
      var str = url.substr(1);
      strs = str.split("&");
      for(var i = 0; i < strs.length; i ++) {
         theRequest[strs[i].split("=")[0]]=unescape(strs[i].split("=")[1]);
      }
   }
   return theRequest;
}

var getstr = new Object();
getstr = GetRequest();
var gzid=getstr["gzid"];
var qz_gdt=getstr["qz_gdt"];
var gdt_vid=getstr["gdt_vid"];

  if (gzid != null && gzid != "") {
     setCookie("gzid",gzid,7)
  }
  
  if (qz_gdt != null && qz_gdt != "") {
     setCookie("click_id",qz_gdt,7)
  }
    
  if (gdt_vid != null && gdt_vid != "") {
     setCookie("click_id",gdt_vid,7)
  }