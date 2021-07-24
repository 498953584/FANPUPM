$(document).ready(function () {
	// 延迟4秒执行
	setTimeout("_tooltip();", 4000);
});

function _tooltip() {
    $('.tooltip').each(function () {
        // 如果内容和title不一样,才需要鼠标提示
        if ($(this).text() != $(this).attr('title')) {
	        $(this).tooltip({
	        	track: true,
	        	delay: 0,
	        	showURL: false,
	        	fade: true,
	        	showBody: " - ",
	        	extraClass: "solid 1px blue",
	        	fixPNG: true
	        });  
        } else {
             $(this).attr('title', '');
        }
    });

}
