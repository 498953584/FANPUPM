       var fadingTooltip;
        var wnd_height, wnd_width;
        var tooltip_height, tooltip_width;
        var tooltip_shown = false;
        var transparency = 100;
        var timer_id = 1;
        var tooltiptext;

        // override events
        window.onload = WindowLoading;
        window.onresize = UpdateWindowSize;
        document.onmousemove = AdjustToolTipPosition;

        function DisplayTooltip(tooltip_text) {
            fadingTooltip.innerHTML = tooltip_text;
            tooltip_shown = (tooltip_text != "") ? true : false;
            if (tooltip_text != "") {
                // Get tooltip window height
                tooltip_height = (fadingTooltip.style.pixelHeight) ? fadingTooltip.style.pixelHeight : fadingTooltip.offsetHeight;
                transparency = 0;
                ToolTipFading();
            }
            else {
                clearTimeout(timer_id);
                fadingTooltip.style.visibility = "hidden";
            }
        }

        function AdjustToolTipPosition(e) {
            if (tooltip_shown) {
                // Depending on IE/Firefox, find out what object to use to find mouse position
                var ev;
                if (e)
                    ev = e;
                else
                    ev = event;
                //document.getElementById('Chart1').style = "cursor:pointer;";
                fadingTooltip.style.visibility = "visible";
                offset_y = (ev.clientY + tooltip_height - document.body.scrollTop + 30 >= wnd_height) ? -15 - tooltip_height : 20;
                fadingTooltip.style.left = Math.min(wnd_width - tooltip_width - 10, Math.max(3, ev.clientX + 6)) + document.body.scrollLeft + 'px';
                //fadingTooltip.style.left = ev.offsetX + 'px';
                fadingTooltip.style.top = ev.clientY + offset_y + document.documentElement.scrollTop + 'px';
            }
        }

        function WindowLoading() {
            fadingTooltip = document.getElementById('fadingTooltip');

            // Get tooltip  window width				
            tooltip_width = (fadingTooltip.style.pixelWidth) ? fadingTooltip.style.pixelWidth : fadingTooltip.offsetWidth;

            // Get tooltip window height
            tooltip_height = (fadingTooltip.style.pixelHeight) ? fadingTooltip.style.pixelHeight : fadingTooltip.offsetHeight;

            UpdateWindowSize();
        }

        function ToolTipFading() {
            if (transparency <= 100) {
                fadingTooltip.style.filter = "alpha(opacity=" + transparency + ")";
                fadingTooltip.style.opacity = transparency / 100;
                transparency += 5;
                timer_id = setTimeout('ToolTipFading()', 35);
            }
        }

        function UpdateWindowSize() {
            wnd_height = document.body.clientHeight;
            wnd_width = document.body.clientWidth;
        }
