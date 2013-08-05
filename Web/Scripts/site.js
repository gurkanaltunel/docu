(function ($, window) {
    var getDialog = function (html) {
        var $div = $("<div/>").attr("id","dialogbox");
        $div.appendTo("body");
        return $div.html(html).dialog({
            modal: true,
            autoOpen: true,
            close: function () {
                $div.remove();
            }
        });
    };
    var menuReady = function() {
        $(".menu").hide();
        $(".hasmenu").mouseover(function() {
            $(this).find(".menu").show();
        }).mouseleave(function() {
            $(".menu").hide();
        });
    };
    var commands = {
        changeFolder: function (id) {
            $.post("/Home/ChangeFolder?id=" + id).success(function (result) {
                $("#itemsContainer").html(result);
                debugger;
                menuReady();
            });
        },
        addFolder: function () {
            $.get("/Home/CreateFolder").success(function(result) {
                getDialog(result);
            });
        },
        addFile: function () {
            $.get("/Home/AddFile").success(function (result) {
                getDialog(result);
            });
        },
        openFile :function(id) {
            $.get("/Home/OpenFile?id=" + id).success(function(result) {
                $("#itemsContainer").html(result);
                
            });
        }
    };
    window.commands = commands;
    var lastlocation = "";
    var parseLocation = function () {
        lastlocation = window.location.href;
        var hashlocation = lastlocation.indexOf('#');
        if (hashlocation > 0) {
            var info = {};
            var items = lastlocation.substr(hashlocation + 1);
            if (items && items.length) {
                var question = items.indexOf('?');
                if (question > 0) {
                    info.MethodName = items.substr(0, question);
                    items = items.substr(question + 1).split('&');
                    info.arguments = [];
                    for (var i = 0; i < items.length; i += 1) {
                        var params = items[i].split('=');
                        //info.arguments.push({
                        //    name: params[0],
                        //    val: params[1]
                        //});
                        info.arguments.push(params[1]);
                    }
                }
            }
        }
        if (info && commands[info.MethodName]) {
            commands[info.MethodName].apply(null, info.arguments);
        }
    };


    $().ready(function () {
        parseLocation();
        $(window).bind("hashchange", function () {
            //if (lastlocation !== window.location.href) {
                parseLocation();
            //}
        });
        $("#addfile").click(function () {
            commands.addFile();
        });
        $("#addfolder").click(function () {
            commands.addFolder();
        });
    });
})($, window);