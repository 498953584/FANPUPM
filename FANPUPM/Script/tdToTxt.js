
function tdToTxt(className, width, height) {
    var width = width || 300;
    var height = height || 70;
    var pre = null;

    $('.' + className).click(function() {
        switchtdinput(this);
    });

    function switchtdinput(obj) {
        if (pre) {
            doinpleave(pre);
        }
        var objtxt = $.trim($(obj).text());
        var $textarea = $('<textarea></textarea>');
        $textarea.append(objtxt);
        var $obj = $(obj);
        $obj.find('span').css('display', 'none');
        $obj.find('textarea').css('display', 'block');

        $obj.find('textarea').css("border", "1px solid #CADEED").width("100%");

        pre = $(obj)[0];
        $obj.find('textarea').trigger("focus"); //.trigger("select");
        $obj.find('textarea').click(function(event) {
            stopevent(event);
            return false;
        }).blur(function(event) {
            inpleave(event, this);
        });
    }

    function stopevent(event) {
        event.stopPropagation();
    }

    function inpleave(event, obj) {
        stopevent(event);
        doinpleave(obj);
    }

    function doinpleave(obj) {
        if (!obj) return;
        var value = $(obj).val();  //当前输入框的值

        
        var $par = $(obj).parent();
        $par.find('textarea').css('display', 'none');
        $par.find('span').css('display', 'block');
        while (value.indexOf('\n') > -1) {
            value = value.replace("\n", "<br/>");
        }
        while (value.indexOf(' ') > -1) {
            value = value.replace(" ", "&nbsp;&nbsp;");
        }
        $par.find('span').html(value);

        pre = null;
    }
}

