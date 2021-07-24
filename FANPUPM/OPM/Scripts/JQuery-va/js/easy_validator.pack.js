/*
	Copyright (C) 2009 - 2012
	WebSite:	Http://wangking717.javaeye.com/
	Author:		wangking
*/
var xOffset = -20; // x distance from mouse
var yOffset = 15; // y distance from mouse
//解决上传空间与验证框架冲突的变量
if (typeof (validate_IsSumbit_self) == "undefined") validate_IsSumbit_self = false;
$(function () {

    //input action
    $("[reg],[url]:not([reg]),[tip]").hover(
		function (e) {
		    if ($(this).attr('tip') != undefined) {
		        var top = (e.pageY + yOffset);
		        var left = (e.pageX + xOffset);
		        $('body').append('<p id="vtip"><img id="vtipArrow" src="/OPM/Scripts/img/vtip_arrowRed.png"/>' + $(this).attr('tip') + '</p>');
		        $('p#vtip').css("top", top + "px").css("left", left + "px");
		        $('p#vtip').bgiframe();
		    }
		},
		function () {
		    if ($(this).attr('tip') != undefined) {
		        $("p#vtip").remove();
		    }
		}
	).mousemove(
		function (e) {
		    if ($(this).attr('tip') != undefined) {
		        var top = (e.pageY + yOffset);
		        var left = (e.pageX + xOffset);
		        $("p#vtip").css("top", top + "px").css("left", left + "px");
		    }
		}
	).blur(function () {
	    if ($(this).attr("url") != undefined) {
	        ajax_validate($(this));
	    } else if ($(this).attr("reg") != undefined) {
	        validate($(this));
	    }
	});

    $("form").submit(function () {
        var isSubmit = true;
        $(this).find("[reg],[url]:not([reg])").each(function () {
            if ($(this).attr("reg") == undefined) {
                if (!ajax_validate($(this))) {
                    isSubmit = false;
                }
            } else {
                if (!validate($(this))) {
                    isSubmit = false;
                }
            }
        });
        if (typeof (isExtendsValidate) != "undefined") {
            if (isSubmit && isExtendsValidate) {
                return extendsValidate();
            }
        }
        //此句代码解决上传控件冲突问题
        if (!isSubmit)
            return validate_IsSumbit_self;
        //-----------------------------
        return isSubmit;
    });

});

function selefunction(obj) {
    obj.find("[reg],[url]:not([reg]),[tip]").hover(
		function (e) {
		    if ($(this).attr('tip') != undefined) {
		        var top = (e.pageY + yOffset);
		        var left = (e.pageX + xOffset);
		        $('body').append('<p id="vtip"><img id="vtipArrow" src="/OPM/Scripts/img/vtip_arrowRed.png"/>' + $(this).attr('tip') + '</p>');
		        $('p#vtip').css("top", top + "px").css("left", left + "px");
		        $('p#vtip').bgiframe();
		    }
		},
		function () {
		    if ($(this).attr('tip') != undefined) {
		        $("p#vtip").remove();
		    }
		}
	).mousemove(
		function (e) {
		    if ($(this).attr('tip') != undefined) {
		        var top = (e.pageY + yOffset);
		        var left = (e.pageX + xOffset);
		        $("p#vtip").css("top", top + "px").css("left", left + "px");
		    }
		}
	).blur(function () {
	    if ($(this).attr("url") != undefined) {
	        ajax_validate($(this));
	    } else if ($(this).attr("reg") != undefined) {
	        validate($(this));
	    }
	});
}

function validate(obj){
	var reg = new RegExp(obj.attr("reg"));
	var objValue = obj.attr("value");
	
	if(!reg.test(objValue)){
		change_error_style(obj,"add");
		change_tip(obj,null,"remove");
		return false;
	}else{
		if(obj.attr("url") == undefined){
			change_error_style(obj,"remove");
			change_tip(obj,null,"remove");
			return true;
		}else{
			return ajax_validate(obj);
		}
	}
}

function ajax_validate(obj){
	
	var url_str = obj.attr("url");
	if(url_str.indexOf("?") != -1){
		url_str = url_str+"&"+obj.attr("name")+"="+obj.attr("value");
	}else{
		url_str = url_str+"?"+obj.attr("name")+"="+obj.attr("value");
	}
	var feed_back = $.ajax({url: url_str,cache: false,async: false}).responseText;
	feed_back = feed_back.replace(/(^\s*)|(\s*$)/g, "");
	if(feed_back == 'success'){
		change_error_style(obj,"remove");
		change_tip(obj,null,"remove");
		return true;
	}else{
		change_error_style(obj,"add");
		change_tip(obj,feed_back,"add");
		return false;
	}
}

function change_tip(obj,msg,action_type){
	
	if(obj.attr("tip") == undefined){//初始化判断TIP是否为空
		obj.attr("is_tip_null","yes");
	}
	if(action_type == "add"){
		if(obj.attr("is_tip_null") == "yes"){
			obj.attr("tip",msg);
		}else{
			if(msg != null){
				if(obj.attr("tip_bak") == undefined){
					obj.attr("tip_bak",obj.attr("tip"));
				}
				obj.attr("tip",msg);
			}
		}
	}else{
		if(obj.attr("is_tip_null") == "yes"){
			obj.removeAttr("tip");
			obj.removeAttr("tip_bak");
		}else{
			obj.attr("tip",obj.attr("tip_bak"));
			obj.removeAttr("tip_bak");
		}
	}
}

function change_error_style(obj,action_type){
    if (action_type == "add") {
        //obj.css({ "border": "2px solid #FF0000", "color": "red" });
		obj.addClass("input_validation-failed");
	}else{
    //obj.css({ "border": "1px solid #b5ccde", "color": "black" });
		obj.removeClass("input_validation-failed");
	}
}

$.fn.validate_callback = function(msg,action_type,options){
	this.each(function(){
		if(action_type == "failed"){
			change_error_style($(this),"add");
			change_tip($(this),msg,"add");
		}else{
			change_error_style($(this),"remove");
			change_tip($(this),null,"remove");
		}
	});
};
