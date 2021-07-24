onload="desktop()"
    function deskwidth() {
            var w = Math.floor((screen.availWidth)); //document.body.clientWidth
            document.getElementById("deskframe").src = "../TableTop/tableMain.aspx?deskwidth=" + w;
            
        }