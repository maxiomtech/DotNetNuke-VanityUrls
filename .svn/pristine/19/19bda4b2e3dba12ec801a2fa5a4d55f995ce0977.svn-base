(function ($) {

    $.widget("ui.iitPlaceholder", {
        options: {
            TextColor: "#000",
            PlaceHolderColor: "#ccc"
        },

        _create: function () {
            var self = this;
            var el = this.element;
            var defaultValue = $(el).attr("defaultvalue");

            $(el).val(defaultValue);
            $(el).css('color', self.options.PlaceHolderColor);

            $(el).bind("click.iitPlaceholder", function () {
                if ($(el).val() == defaultValue) {
                    $(el).css('color', self.options.TextColor);
                    $(el).val("");
                }
            });

            var isEmpty = function () {
                if ($(el).val() == "") {
                    $(el).val(defaultValue);
                    $(el).css('color', self.options.PlaceHolderColor);
                } else {
                    $(el).css('color', self.options.TextColor);
                }

            };

            $(el).bind("blur.iitPlaceholder", isEmpty);
        },

        _init: function () {

        },

        _setOption: function (key, value) {
            options.key = value;
        },

        destroy: function () {
            $(".iitPlaceholder").each(function () {
                $(this).unbind("click.iitPlaceholder").unbind("blur.iitPlaceholder").val("");
            });
        }

    });

})(jQuery);

$(document).ready(function () {
    setTimeout(function() {
        $(".iitPlaceholder").iitPlaceholder();
    }, 500);

});