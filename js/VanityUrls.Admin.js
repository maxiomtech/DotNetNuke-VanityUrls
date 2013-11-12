(function (name, definition) {
    var theModule = definition(),
        hasDefine = typeof define === 'function',
        hasExports = typeof module !== 'undefined' && module.exports;

    if (hasDefine) { // AMD Module
        define(theModule);
    } else if (hasExports) { // Node.js Module
        module.exports = theModule;
    } else { // Assign to common namespaces or simply the global object (window)


        // account for for flat-file/global module extensions
        var obj = null;
        var namespaces = name.split(".");
        var scope = (this.jQuery || this.ender || this.$ || this);
        for (var i = 0; i < namespaces.length; i++) {
            var packageName = namespaces[i];
            if (obj && i == namespaces.length - 1) {
                obj[packageName] = theModule;
            } else if (typeof scope[packageName] === "undefined") {
                scope[packageName] = {};
            }
            obj = scope[packageName];
        }

    }
})('VanityUrls.Admin', function () {

    var plugin = this;
    var jQueryUI_version = $.ui.version.split('.');

    plugin.defaultOptions = {};
    plugin.makeid = function () {
        var text = "";
        var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        for (var i = 0; i < 5; i++)
            text += possible.charAt(Math.floor(Math.random() * possible.length));
        return text;
    };
    plugin.opts = {};
    plugin.tabs = {};
    plugin.mapping = {
        'ActiveStartDate': {
            create: function (options) {
                return ko.observable(options.data != null ? moment(options.data).format("MM/DD/YYYY hh:mm A") : null);
            }
        },
        'ActiveEndDate': {
            create: function (options) {
                return ko.observable(options.data != null ? moment(options.data).format("MM/DD/YYYY hh:mm A") : null);
            }
        },
        'CreatedOnDate': {
            create: function (options) {
                return ko.observable(options.data != null ? moment(options.data).format("MM/DD/YYYY hh:mm A") : null);
            }
        },
        'ModifiedOnDate': {
            create: function (options) {
                return ko.observable(options.data != null ? moment(options.data).format("MM/DD/YYYY hh:mm A") : null);
            }
        },
        'LastAccessedDate': {
            create: function (options) {
                return ko.observable(options.data != null ? moment(options.data).format("MM/DD/YYYY hh:mm A") : null);
            }
        },
        'VanityUrl': {
            create: function (options) {
                return ko.observable(options.data).extend({ vanityUrlValidation: "" });
            }
        },
        'RedirectUrl': {
            create: function (options) {
                return ko.observable(options.data).extend({ redirectUrlValidation: "" });
            },
            update: function (options) {
                var parent = options.parent;
                var utm_Source = new RegExp("utm_source\=([\\w]+)[&]*", "i");
                var utm_Medium = new RegExp("utm_medium\\=([\\w]+)[&]*", "i");
                var utm_Campaign = new RegExp("utm_campaign\\=([\\w]+)[&]*", "i");
                var utm_Term = new RegExp("utm_term\\=([\\w]+)[&]*", "i");
                var utm_Content = new RegExp("utm_content\\=([\\w]+)[&]*", "i");
                parent.UTM_Source = ko.observable("");
                parent.UTM_Medium = ko.observable("");
                parent.UTM_Campaign = ko.observable("");
                parent.UTM_Term = ko.observable("");
                parent.UTM_Content = ko.observable("");
                if (options.data.match(utm_Source)) {
                    var val = options.data.match(utm_Source)[1];
                    parent.UTM_Source(val);
                    options.data = options.data.replace(utm_Source, "");
                }
                if (options.data.match(utm_Medium)) {
                    var val = options.data.match(utm_Medium)[1];
                    parent.UTM_Medium(val);
                    options.data = options.data.replace(utm_Medium, "");
                }
                if (options.data.match(utm_Campaign)) {
                    var val = options.data.match(utm_Campaign)[1];
                    parent.UTM_Campaign(val);
                    options.data = options.data.replace(utm_Campaign, "");
                }
                if (options.data.match(utm_Term)) {
                    var val = options.data.match(utm_Term)[1];
                    parent.UTM_Term(val);
                    options.data = options.data.replace(utm_Term, "");
                }
                if (options.data.match(utm_Content)) {
                    var val = options.data.match(utm_Content)[1];
                    parent.UTM_Content(val);
                    options.data = options.data.replace(utm_Content, "");
                }


                return options.data.replace(/\?$/i, "").replace(/&$/i, "");
            }
        }
    };

    var urlEntity = function (data) {
        var self = this;

        self.ID = ko.observable(-1);
        self.VanityUrl = ko.observable("").extend({ vanityUrlValidation: "" });
        self.RedirectUrl = ko.observable("").extend({ redirectUrlValidation: "" });
        self.ActiveStartDate = ko.observable();
        self.ActiveEndDate = ko.observable();
        self.Description = ko.observable();
        self.CreatedOnDate = ko.observable();
        self.CreatedByUserId = ko.observable();
        self.ModifiedOnDate = ko.observable();
        self.ModifiedByUserId = ko.observable();
        self.LastAccessedDate = ko.observable();
        self.ModifiedByUserName = ko.observable();
        self.CreatedByUserName = ko.observable();


        return self;
    };



    plugin.UpdateGoogleTrackingType = function (type, value) {
        $.ajax({
            type: "POST",
            url: opts.servicePath + "/UpdateGoogleTrackingType" + "?PortalID=" + opts.PortalId,
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify({ googleTrackInfo: { UTM_Type: type, Value: value } })
        }).success(function (data) {
            $.VanityUrls.Utils().ShowMessage("info", "UTM value added");
        }).error(function (xhr, status, error) {
            $.VanityUrls.Utils().ShowMessage("error", error);
        }).complete(function () {
        });
    };

    plugin.RemoveGoogleTrackingType = function (type, value) {
        $.ajax({
            type: "POST",
            url: opts.servicePath + "/RemoveGoogleTrackingType" + "?PortalID=" + opts.PortalId,
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify({ googleTrackInfo: { UTM_Type: type, Value: value } })
        }).success(function (data) {
            $.VanityUrls.Utils().ShowMessage("info", "UTM value removed");
        }).error(function (xhr, status, error) {
            $.VanityUrls.Utils().ShowMessage("error", error);
        }).complete(function () {
        });
    };

    plugin.model = new function () {
        var self = this;
        self.urls = ko.observableArray([]);
        self.searchValue = ko.observable();
        self.UTM_Sources = ko.observableArray([]);
        self.UTM_Mediums = ko.observableArray([]);
        self.UTM_Campaigns = ko.observableArray([]);
        self.UTM_Terms = ko.observableArray([]);
        self.UTM_Contents = ko.observableArray([]);
        self.searchUrls = ko.computed(function () {
            if (self.searchValue() == "" || self.searchValue() == null) {
                return self.urls();
            } else {
                return ko.utils.arrayFilter(self.urls(), function (item) {
                    return item.VanityUrl().indexOf(self.searchValue()) >= 0;
                });
            }
        });
        self.selectedUrl = ko.observable();
        self.selectUrl = function (data, event) {
            event.preventDefault();
            self.selectedUrl(data.ID());
            //$("#iit_utmSource").val(data.UTM_Source());

        };
        self.getSelectedUrl = ko.computed(function () {
            var value = ko.utils.arrayFirst(this.urls(), function (item) {
                return item.ID() == self.selectedUrl();
            });

            return value != null ? value : new urlEntity();

        }, self);

        self.save = function (item) {


            if (item.getSelectedUrl().VanityUrl.isDuplicate()) {
                $.VanityUrls.Utils().ShowMessage("error", "Duplicate vanity url detected. Please change your vanity url.");
                return false;
            }

            if (!item.getSelectedUrl().VanityUrl.isValid()) {
                $.VanityUrls.Utils().ShowMessage("error", "Vanity url entered is not in a proper format. (ex. coupon-25)");
                return false;
            }

            if (!item.getSelectedUrl().RedirectUrl.isValid()) {
                $.VanityUrls.Utils().ShowMessage("error", "Destination url entered is not in a proper format. (ex. /Home.aspx)");
                return false;
            }

            if (item.getSelectedUrl().VanityUrl() == "" || item.getSelectedUrl().RedirectUrl() == "") {
                $.VanityUrls.Utils().ShowMessage("error", "You must enter a vanity url and redirect url to save.");
                return false;
            }


            $.VanityUrls.Utils().ShowLoading();
            var url = JSON.parse(ko.toJSON(item.getSelectedUrl()));
            delete url.__type;
            delete url.__ko_mapping__;


            /*Add Custom RegEx logic to replease short urls into predefined redurect urls. Example:
                var productRegEx = new RegExp("^[0-9]+$", "i");
                if (url.RedirectUrl.match(productRegEx)) {
                    url.RedirectUrl = "/products.aspx?ProductId=" + url.RedirectUrl;
                }
            */
            if (url.UTM_Source != null || url.UTM_Medium != null || url.UTM_Campaign != null || url.UTM_Term != null || url.UTM_Content != null) {
                if (url.RedirectUrl.indexOf("?") == -1) {
                    url.RedirectUrl += "?";
                } else {
                    url.RedirectUrl += "&";
                }

                if (url.UTM_Source != null && url.UTM_Source != "null" && url.UTM_Source != "")
                    url.RedirectUrl += "utm_source=" + url.UTM_Source + "&";
                if (url.UTM_Medium != null && url.UTM_Medium != "null" && url.UTM_Medium != "")
                    url.RedirectUrl += "utm_medium=" + url.UTM_Medium + "&";
                if (url.UTM_Campaign != null && url.UTM_Campaign != "null" && url.UTM_Campaign != "")
                    url.RedirectUrl += "utm_campaign=" + url.UTM_Campaign + "&";
                if (url.UTM_Term != null && url.UTM_Term != "null" && url.UTM_Term != "")
                    url.RedirectUrl += "utm_term=" + url.UTM_Term + "&";
                if (url.UTM_Content != null && url.UTM_Content != "null" && url.UTM_Content != "")
                    url.RedirectUrl += "utm_content=" + url.UTM_Content + "&";

                url.RedirectUrl = url.RedirectUrl.replace(/&$/i, "");
                url.RedirectUrl = url.RedirectUrl.replace(/\?$/i, "");
            }

            //console.log(url);

            $.ajax({
                type: "POST",
                url: opts.servicePath + "/SaveUrl" + "?PortalID=" + opts.PortalId,
                contentType: 'application/json',
                dataType: 'json',
                data: JSON.stringify({ VanityUrl: url })
            }).success(function (data) {
                if (data != null && data.d != null) {
                    plugin.model.urls.remove(function (item) {
                        return item.ID() == data.d.ID;
                    });
                    plugin.model.urls.push(ko.mapping.fromJS(data.d, plugin.mapping));
                    self.newUrl();
                    self.selectedUrl(data.d.ID);

                }
                $.VanityUrls.Utils().ShowMessage("success", "Url successfully saved.");
            }).error(function (xhr, status, error) {
                $.VanityUrls.Utils().ShowMessage("error", error);
            }).complete(function () {
                $.VanityUrls.Utils().HideLoading();
            });
        };

        self.newUrl = function () {
            var value = ko.utils.arrayFirst(this.urls(), function (item) {
                return item.ID() == -1;
            });

            if (value != null) {
                self.urls.remove(value);
            }


            if (jQueryUI_version[0] >= 1 && jQueryUI_version[1] >= 10) {
                $("#iit_tabs").tabs("option", "active", 0);
            } else {
                $("#iit_tabs").tabs('select', 0);
            }


            self.urls.push(new urlEntity());
            self.selectedUrl("-1");
        };

        self.deleteUrl = function (item) {
            $.VanityUrls.Utils().ShowLoading();
            var url = JSON.parse(ko.toJSON(item.getSelectedUrl()));
            delete url.__type;
            delete url.__ko_mapping__;
            $.ajax({
                type: "POST",
                url: opts.servicePath + "/DeleteUrl" + "?PortalID=" + opts.PortalId,
                contentType: 'application/json',
                dataType: 'json',
                data: JSON.stringify({ VanityUrl: url })
            }).success(function (data) {
                $.VanityUrls.Utils().ShowMessage("success", "Url successfully removed.");
            }).error(function (xhr, status, error) {
                $.VanityUrls.Utils().ShowMessage("error", error);
            }).complete(function () {
                $.VanityUrls.Utils().HideLoading();
            });

            var value = ko.utils.arrayFirst(this.urls(), function (item) {
                return item.ID() == self.selectedUrl();
            });

            self.urls.remove(value);
            self.selectedUrl("-1");
        };

        self.makeid = function (item) {
            item.getSelectedUrl().VanityUrl(plugin.makeid());
        }

    };


    ko.extenders.vanityUrlValidation = function (target, message) {
        target.isDuplicate = ko.observable(false);
        target.isValid = ko.observable(true);
        target.cssClass = ko.observable("");

        function validate(newValue) {

            var matcher = new RegExp("^[a-zA-Z0-9][a-zA-Z0-9/\-]*$");

            var value = ko.utils.arrayFirst(plugin.model.urls(), function (item) {
                return item.VanityUrl().toLowerCase() == newValue.toLowerCase() && item.ID() != plugin.model.getSelectedUrl().ID();
            });
            target.isValid(newValue.match(matcher) ? true : false);
            target.isDuplicate(value ? true : false);

            target.cssClass(value || !newValue.match(matcher) ? "validationFailure" : "");
        }

        target.subscribe(validate);
        return target;
    };

    ko.extenders.redirectUrlValidation = function (target, message) {
        target.isValid = ko.observable(true);
        target.cssClass = ko.observable("");

        function validate(newValue) {

            var matcher = new RegExp("^/.+$");

            target.isValid(newValue.match(matcher) ? true : false);
            target.cssClass(!newValue.match(matcher) ? "validationFailure" : "");
        }

        target.subscribe(validate);
        return target;
    };



    return {
        Init: function (options) {
            tabs = $("#iit_tabs").tabs({
                select: function (event, ui) {
                    if (ui.index == 3) {
                        return false;
                    }
                }
            });
            opts = $.extend({}, defaultOptions, options);

            $("#iit_BtnTestFeed").button();

            $("#iit_StartDate").datetimepicker({ timeFormat: 'hh:mm TT' });
            $("#iit_EndDate").datetimepicker({ timeFormat: 'hh:mm TT' });
            $("#ui-datepicker-div").wrap("<div class='VanityUrls' />");

            this.GetGoogleTrackingInfo();


            this.GetVanityUrls();

            $.widget("ui.iitCombobox", {
                _create: function () {
                    var input,
                      that = this,
                      select = this.element.hide(),
                      selected = select.children(":selected"),
                      value = selected.val() ? selected.text() : "",
                      wrapper = this.wrapper = $("<div>")
                        .addClass("ui-combobox")
                        .insertAfter(select);

                    function addIfUnavailable(element) {
                        var value = $(element).val(),
                            matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(value) + "$", "i"),
                            available = false;

                        select.children("option").each(function () {
                            if ($(this).text().match(matcher)) {
                                this.selected = available = true;
                                return false;
                            }
                        });
                        if (!available) {
                            select.append("<option>" + value + "</option>");
                            plugin.UpdateGoogleTrackingType($(select).attr("id").replace("iit_utm", "UTM_"), value);

                        }

                    }

                    input = $("<input>")
                      .appendTo(wrapper)
                      .val(value)
                      .attr("title", "")
                      .attr("data-bind", "value: getSelectedUrl()." + $(select).attr("id").replace("iit_utm", "UTM_"))
                      .addClass("ui-state-default ui-combobox-input")
                      .autocomplete({
                          delay: 0,
                          minLength: 0,
                          source: function (request, response) {
                              var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
                              response(select.children("option").map(function () {
                                  var text = $(this).text();
                                  if (this.value && (!request.term || matcher.test(text)))
                                      return {
                                          label: text.replace(
                                            new RegExp(
                                              "(?![^&;]+;)(?!<[^<>]*)(" +
                                              $.ui.autocomplete.escapeRegex(request.term) +
                                              ")(?![^<>]*>)(?![^&;]+;)", "gi"
                                            ), "<strong>$1</strong>"),
                                          value: text,
                                          option: this
                                      };
                              }));
                          },
                          select: function (event, ui) {
                              ui.item.option.selected = true;
                              that._trigger("selected", event, {
                                  item: ui.item.option
                              });
                          },
                          change: function (event, ui) {
                              $(input).change();
                              if (!ui.item)
                                  return addIfUnavailable(this);
                          },
                          close: function (event, ui) {
                              $(input).change();
                          }
                      })
                      .addClass("ui-widget ui-widget-content ui-corner-left");

                    var renderItem = function (ul, item) {
                        var $span = $("<span>").addClass("remove-autocomplete").css("display", "inline").css("float", "right").text("X").click(function () {
                            plugin.RemoveGoogleTrackingType($(select).attr("id").replace("iit_utm", "UTM_"), item.value);
                            $("#" + $(select).attr("id") + " option:contains('" + item.value + "')").remove();
                            input.autocomplete("close");
                            input.val("");
                            input.trigger("change");
                            $(this).blur();
                        });

                        return $("<li>")
                          .data("item.autocomplete", item)
                          .append($span)
                          .append("<a style='display:inline'>" + item.label + "</a>")
                          .appendTo(ul);
                    };

                    if (jQueryUI_version[0] >= 1 && jQueryUI_version[1] >= 10) {
                        input.data("uiAutocomplete")._renderItem = renderItem;
                    } else {
                        input.data("autocomplete")._renderItem = renderItem;
                    }



                    $("<a>")
                      .attr("tabIndex", -1)
                      .attr("title", "Show All Items")
                      .appendTo(wrapper)
                      .button({
                          icons: {
                              primary: "ui-icon-triangle-1-s"
                          },
                          text: false
                      })
                      .removeClass("ui-corner-all")
                      .addClass("ui-corner-right ui-combobox-toggle")
                      .click(function () {
                          if (input.autocomplete("widget").is(":visible")) {
                              input.autocomplete("close");
                              addIfUnavailable(input);
                              return;
                          }

                          $(this).blur();

                          input.autocomplete("search", "");
                          input.focus();
                      });

                },

                destroy: function () {
                    this.wrapper.remove();
                    this.element.show();
                    $.Widget.prototype.destroy.call(this);
                }
            });


            $("#iit_utmSource, #iit_utmMedium, #iit_utmCampaign, #iit_utmTerm, #iit_utmContent").iitCombobox();
            $('.ui-autocomplete').wrap('<div class="VanityUrls" />');
            $("#iit_LnkExport").click(function (e) {
                e.preventDefault();
                $.VanityUrls.Admin.Export();
            });

            ko.applyBindings(plugin.model);

            return this;
        },
        GetVanityUrls: function () {
            $.VanityUrls.Utils().ShowLoading();
            $.ajax({
                type: "POST",
                url: opts.servicePath + "/GetVanityUrls" + "?PortalID=" + opts.PortalId,
                contentType: 'application/json',
                dataType: 'json'
            }).success(function (data) {
                plugin.model.urls.splice(0, plugin.model.urls().length);
                $(data.d).each(function () {

                    plugin.model.urls.push(ko.mapping.fromJS(this, plugin.mapping));
                });
            }).error(function (xhr, status, error) {
                $.VanityUrls.Utils().ShowMessage("error", error);
            }).complete(function () {
                $.VanityUrls.Admin.Model().newUrl();
                $.VanityUrls.Utils().HideLoading();
            });

            return this;
        },
        GetGoogleTrackingInfo: function () {

            $.VanityUrls.Utils().ShowLoading();
            $.ajax({
                type: "POST",
                url: opts.servicePath + "/GetGoogleTrackingTypes" + "?PortalID=" + opts.PortalId,
                contentType: 'application/json',
                dataType: 'json'
            }).success(function (data) {
                $(data.d).each(function () {
                    var utm = this;

                    switch (utm.UTM_Type) {
                        case "UTM_Source":
                            plugin.model.UTM_Sources.push(utm.Value);
                            //$("#iit_utmSource").append("<option>" + utm.Value + "</option>");
                            break;
                        case "UTM_Medium":
                            $("#iit_utmMedium").append("<option>" + utm.Value + "</option>");
                            break;
                        case "UTM_Campaign":
                            $("#iit_utmCampaign").append("<option>" + utm.Value + "</option>");
                            break;
                        case "UTM_Term":
                            $("#iit_utmTerm").append("<option>" + utm.Value + "</option>");
                            break;
                        case "UTM_Content":
                            $("#iit_utmContent").append("<option>" + utm.Value + "</option>");
                            break;
                    }

                });
            }).error(function (xhr, status, error) {
                $.VanityUrls.Utils().ShowMessage("error", error);
            }).complete(function () {
                $.VanityUrls.Utils().HideLoading();
            });


            return this;
        },


        Model: function () {
            return plugin.model;
        },
        Export: function () {
            $("#iitExport").remove();
            $("form").after().append("<form id='iitExport' action='" + opts.servicePath + "/GetVanityUrlsXml?PortalID=" + opts.PortalId + "' method='POST'></form>");
            $("#iitExport").submit();
        }
    }

});