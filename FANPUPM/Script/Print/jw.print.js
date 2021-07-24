// 初始化打印页面样式
function initialPrint() {
	$('*')
        .css('font-size', '12px')
        .css('font-weight', 'normal')


	$('table')
        .css('border-collapse', 'collapse')
        .css('border-spacing', '0px 0px')
        .css('text-align', 'center')
        .css('margin', '0 auto')

	$('.tab2')
        .css('border', '2px solid Black')
        .css('width', '670px')
        .css('border-bottom', '2px solid Black')

	$('.tab2 tr, tab2 th')
        .css('border', '1px solid Black')
        .css('height', '25px');

	$('.tab2 td, .tab2 td')
        .css('padding-left', '5px')
        .css('border', '1px solid Black')
        .css('text-align', 'left');

	$('.tab2 input[type=text]')
        .css('border', 'none')
        .css('padding-bottom', '0px');

	$('.tab2 textarea')
        .css('width', '98%')
        .css('border', 'none');

	$('.descTd')
        .css('white-space', 'nowrap')

	$('.descTd span')
        .css('margin', '0px')
        .css('color', 'Red');

	$('.tabHeader')
        .css('border', 'none')
        .css('width', '670px');

	$('.tabHeader tr')
        .css('height', '25px');

	$('.tabHeader input[type=text]')
        .css('border', 'none')

	$('#gvwPettyCash')
        .css('border-top', 'none');

	$('.title')
        .css('font-size', '22px')
        .css('font-weight', 'bold');

	$('#tabAudit tr')
        .css('height', '80px');
}
