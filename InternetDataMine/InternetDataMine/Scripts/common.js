/*

     实时时钟

*/
function Clock() {
	var date = new Date();
	this.year = date.getFullYear();
	this.month = date.getMonth() + 1;
	this.date = date.getDate();
	this.day = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六")[date.getDay()];
	this.hour = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
	this.minute = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
	this.second = date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();

	this.toString = function() {
	    //return "" + this.year + "年" + this.month + "月" + this.date + "日 " + this.hour + ":" + this.minute + ":" + this.second + " " + this.day; 
	    return "" + this.year + "年" + this.month + "月" + this.date + "日 " + " " + this.day;
	};
	
	this.toSimpleDate = function() {
		return this.year + "-" + this.month + "-" + this.date;
	};
	
	this.toDetailDate = function() {
		return this.year + "-" + this.month + "-" + this.date + " " + this.hour + ":" + this.minute + ":" + this.second;
	};
	
	this.display = function(ele) {
		var clock = new Clock();
		ele.innerHTML = clock.toString();
		window.setTimeout(function() {clock.display(ele);}, 300000);
	};


}


/*

     页面加载入口函数

*/
$(document).ready(function() {
	
    /*

    如果不要显示时间，此段可以不要

    */

    if ($("#clock").length > 0) {
        var clock = new Clock();
        clock.display($("#clock")[0]);
    }

	
	/**
	 * 2011-08-18 by zx for change stylesheet
	 */
	$('.styleswitch').click(function()
	{

		switchStylestyle(this.getAttribute("rel"));
		return false;
	});
	var c = readCookie('style');
	if (c == null)
	{
	    c = 'blue';
	}
	if (c) switchStylestyle(c);
});


/*

     更换皮肤

*/
function switchStylestyle(styleName)
{
	$('link[rel=stylesheet][title]').each(function(i) 
	{
		this.disabled = true;
		if (this.getAttribute('title') == styleName) this.disabled = false;
	});
	
	$("iframe").contents().find('link[rel=stylesheet][title]').each(function(i) 
	{
		this.disabled = true;
		if (this.getAttribute('title') == styleName) this.disabled = false;
	});
	
	createCookie('style', styleName, 365);
}
/*

    创建COOKIE，以保存皮肤的样式

*/
function createCookie(name,value,days)
{
	if (days)
	{
		var date = new Date();
		date.setTime(date.getTime()+(days*24*60*60*1000));
		var expires = "; expires="+date.toGMTString();
	}
	else var expires = "";
	document.cookie = name+"="+value+expires+"; path=/";
}

/*
    读取COOKIE


*/
function readCookie(name)
{
	var nameEQ = name + "=";
	var ca = document.cookie.split(';');
	for(var i=0;i < ca.length;i++)
	{
		var c = ca[i];
		while (c.charAt(0)==' ') c = c.substring(1,c.length);
		if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
	}
	return null;
}

/*

    删除COOKIE

*/
function eraseCookie(name)
{
	createCookie(name,"",-1);
}
// /cookie functions

//根据时间，生成URl后缀，防止URL数据不刷新
function getUrlStafix()
{
    var myDate = new Date();
    return "&myDate=" + myDate.getFullYear() + myDate.getMonth() + myDate.getDate() + myDate.getHours() + myDate.getMinutes() + myDate.getSeconds();
}

//加载煤矿信息
function loadMineInfo() {
    $("#comboMineCode").combobox({
        url: "../CompanyInfoManager/QueryJBXXAll",
        valueField: 'minecode',
        textField: 'simplename'
    });
}

//查询字符串内相同的字符的个数
function returnCharCount(InputString,MarkChar)
{
    var length = InputString.length;
    var charCount = 0;
    for(var i =0;i<length;i++)
    {
        var mySameChar = InputString.substring(i, i+1);
        if (mySameChar == MarkChar)
        {
            charCount++;
        }
    }

    return charCount;
}


//格式化日期时间
Date.prototype.Format = function(fmt) 
    { 
        var o = { 
             "M+" : this.getMonth()+1,                 //月份 
       "d+" : this.getDate(),                    //日 
        "h+" : this.getHours(),                   //小时 
          "m+" : this.getMinutes(),                 //分 
         "s+" : this.getSeconds(),                 //秒 
          "q+" : Math.floor((this.getMonth()+3)/3), //季度 
           "S"  : this.getMilliseconds()             //毫秒 
         }; 
    if(/(y+)/.test(fmt)) 
          fmt=fmt.replace(RegExp.$1, (this.getFullYear()+"").substr(4 - RegExp.$1.length)); 
     for(var k in o) 
        if(new RegExp("("+ k +")").test(fmt)) 
           fmt = fmt.replace(RegExp.$1, (RegExp.$1.length==1) ? (o[k]) : (("00"+ o[k]).substr((""+ o[k]).length))); 
   return fmt; 
  }


//查询URL中的参数
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

//保存操作日志
function AddProcessLogGlobal(ProcessLogContent) {
    $.post("/ProcessLog/ProcessLogAdd?ProcessLogName=" + getQueryString('UserName'), { ProcessLogContent: ProcessLogContent });
    //$.post("/ProcessLog/ProcessLogAdd", { ProcessLogName: ProcessLogName, ProcessLogContent: ProcessLogContent });
}

//下载文件
function downloadFile(url) {
    try {
        var elemIF = document.createElement("iframe");
        elemIF.src = url;
        elemIF.style.display = "none";
        document.body.appendChild(elemIF);
    } catch (e) {

    }
}