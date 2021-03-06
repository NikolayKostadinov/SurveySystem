﻿var kendoAdditional = (function (kendo) {
    // ----------------- autorun function on document ready -------------------
    'use strict';
    function sendFactoryId() {
        return { "factoryId": $('input[name=factories]').val() || $('input[name=factoriesD]').val() };
    }

    String.prototype.replaceAll = function (target, replacement) {
        return this.split(target).join(replacement);
    };

    function reportDetailsPaste(e) {
        e.html = e.html.replaceAll('&nbsp;&nbsp;&nbsp;&nbsp;', '');
        e.html = e.html.replaceAll('&nbsp;&nbsp;&nbsp;', '');
        e.html = e.html.replaceAll('&nbsp;&nbsp;', '');
        e.html = e.html.replaceAll('&nbsp; &nbsp;', '');
    }

    function sendEditorText() {
        var message = '';

        if ($('#report-details').length) {
            var editor = $("#report-details").data("kendoEditor");
            var val = editor.value();
            message = encodeURI(val);
        }

        return { "reportText": message };
    }

    function addAntiForgeryToken(data) {
        data.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
        return data;
    }

    function onSaveEditorChanges() {
        var datePicker = $('input[name=date]').data('kendoDatePicker');
        if (datePicker !== undefined) {
            var date = datePicker.value();
        } else {
            return;
        }

        var result = { "date": date.toISOString(kendo.culture().name) };
        $.extend(result, sendFactoryId());
        $.extend(result, sendEditorText());
        $.extend(result, sendAntiForgery());

        $.ajax({
            url: 'SaveReport',
            type: 'POST',
            data: result,
            success: function (data) {
                var confirmed = data.IsConfirmed;
                if (confirmed === true) {
                    var message = "Вие променихте описанието на технологичният отчет успешно.";
                    $('pre#succ-message').text(message);
                    $('div#success-window').data("kendoWindow").open();

                } else {
                    if (data.errors) {
                        var errorMessage = "";
                        $.each(data.errors, function (key, value) {
                            if ('errors' in value) {
                                $.each(value.errors, function () {
                                    errorMessage += this + "\n";
                                });
                            }
                        });

                        $('pre#err-message').text(errorMessage);
                        $('div#err-window').data("kendoWindow").open();
                    }
                }
            },
            error: function (data) {
                var errorMessage = "";
                var response = JSON.parse(data.responseText).data;
                $.each(response.errors, function (key, value) {
                    errorMessage += this + "\n";
                });

                $('pre#err-message').text(errorMessage);
                $('div#err-window').data("kendoWindow").open();
            }
        });
    }

    $(function () {
        var culture = $('#culture').val();
        kendo.culture(culture);
        prepareWindow('div#err-window', 'Възникна грешка в приложението');
        prepareWindow('div#success-window', 'Успешна операция');
        var editorSaveButton = $('#editor-save-changes');
        if (editorSaveButton !== undefined && editorSaveButton !== null) {
            editorSaveButton.on("click", onSaveEditorChanges);
            editorSaveButton.parent().css('float', 'right');
        }
    });

    //------------------ private functions ------------------------------------

    function prepareWindow(selector, title) {

        var window = $(selector);
        if (window) {
            window.kendoWindow({
                width: "650px",
                title: title,
                actions: ["Close"],
                modal: true,
                position: {
                    top: "30%", // or "100px"
                    left: "30%"
                },
                minHeight: 100,
                maxHeight: 350
            });
        }
    }

    function convertValues(value) {
        var data = {};
        value = $.isArray(value) ? value : [value];
        value.forEach(function (element, index, array) {
            data["values[" + index + "]"] = element;
        });
        return data;
    }

    function serialize(data) {
        var dataKeys = Object.keys(data);
        dataKeys.forEach(function (element, index, array) {
            if ($.isArray(element)) {
                serializeArray(property, element, array);
            }
        });
        $.extend(data, sendAntiForgery());
    }

    function serializeArray(prefix, array, result) {
        array = $.isArray(array) ? array : [array];
        array.forEach(function (element, index, array) {
            if ($.isPlainObject(element)) {
                var elementKeys = Object.keys(element);
                elementKeys.forEach(function (property, ix, arr) {
                    result[prefix + "[" + index + "]." + property] = element[property];
                });
            } else {
                result[prefix + "[" + index + "]"] = element;
            }
        });

    }

    function boundEmptyRelatedRecords() {
        var grid = this;
        var propertyName = "RelatedUnitConfigs";
        if (grid) {
            var records = grid._data;
            if (records) {
                var len = records.length;
                for (var i = 0; i < len; i++) {
                    var element = records[i][propertyName];
                    if (element.length === 0) {
                        element.push({ Id: 0, Code: null, Name: null });
                    }
                }
            }
        }
    }

    //------------------ public functions ------------------------------------
    function DisplayErrors(e) {
        if (e.errors) {
            var message = "";
            $.each(e.errors, function (key, value) {

                var that = this.replace('\\r\\n', '\r\n');
                message += that + "\n";
            });
            $('pre#err-message').text(message);
            $('div#err-window').data("kendoWindow").open();
        }
    }

    function errorHandler(args) {
        let validationMessageTmpl = kendo.template($("#message").html());
        if (args.errors) {
            var grid = $('[data-role=grid]').data("kendoGrid");
            grid.one("dataBinding", function (ev) {
                ev.preventDefault(); // cancel grid rebind if error occurs
                let message = "";
                $.each(args.errors,
                    function (key, value) {
                        let containers = grid.editable.element.find("[data-valmsg-for=" + key + "],[data-val-msg-for=" + key + "]");
                        if (containers.length) {
                            containers.replaceWith(validationMessageTmpl({ field: key, message: value.errors.join('\n') }));
                            let input = grid.editable.element.find('input[name=' + key + ']');
                            input.removeClass('k-valid');
                            input.addClass('k-invalid');
                        } else {
                            if ('errors' in value) {
                                $.each(value.errors,
                                    function () {
                                        var that = this.replace('\\r\\n', '\r\n');
                                        message += that + "\n";
                                    });
                            }
                        }
                    });
                if (message !== '') {
                    $('pre#err-message').text(message);
                    $('div#err-window').data("kendoWindow").open();
                }
            });
        } else {
            location.reload();
        }
    }

    function CloseWindow(selector) {
        $(selector).data("kendoWindow").close();
    }

    function RefreshGrid(selector) {
        var grid = $(selector).data('kendoGrid');
        if (grid) {
            grid.dataSource.read();
        }
    }

    function FullyRefreshGrid(selector) {
        var grid = $(selector).data('kendoGrid');
        if (grid) {
            grid.dataSource.data([]);
            grid.dataSource.read();
        }
    }

    function AfterPlanNormGridValidation() {
        RefreshGrid('#plan-grid');
    }

    function AfterTankStatusesGridValidation() {
        RefreshGrid('#tanks-statuses');
    }

    function ValueMapper(options) {

        var splitUrl = this.dataSource.options.transport.read.url.split('/');
        var actionName = splitUrl[splitUrl.length - 1];
        var url = this.dataSource.options.transport.read.url.replace(actionName, "ValueMapper");

        $.ajax({
            url: url,
            data: convertValues(options.value),
            success: function (data) {
                options.success(data);
            }
        });
    }


    var SendHistoryData = function () {
        var result = { 'id': $('input[name=id]').val(), 'entityName': $('input[name=entityName]').val() };
        $.extend(result, sendAntiForgery());
        return result;
    };

    function DeletableDataBound() {
        var items = this.dataSource.view();
        for (var i = 0; i < items.length; i++) {
            let currentUid = items[i].uid;
            let currenRow = this.table.find("tr[data-uid='" + currentUid + "']");
            if (items[i].IsDeleted) {

                var editButton = $(currenRow).find(".k-grid-edit,.k-grid-delete, .k-grid-lock, .k-grid-unlock");
                editButton.attr("style", "display:none !important");
                var cellsForColloring = $("#" + $(this.element).attr('id') + " tbody").find("tr[data-uid=" + currentUid + "] td");
                cellsForColloring.addClass("text-danger");
                cellsForColloring.find('a').addClass("text-warning");
            }
            if (items[i].IsLocked) {
                currenRow.addClass('text-white');
                currenRow.addClass('bg-warning');
                currenRow.find(".k-grid-lock")
                    .attr("disabled", "disabled")
                    .addClass("k-state-disabled").on("click",
                        function () {
                            return false;
                        });
            } else {
                currenRow.find(".k-grid-unlock")
                    .attr("disabled", "disabled")
                    .addClass("k-state-disabled").on("click",
                        function () {
                            return false;
                        });
            }
        }
    }

    function afterterNomGridValidation(ev) {
        if (ev.type === 'update') {
            RefreshGrid('#grid');
        }
    }

    function messageBound() {
        var dataView = this.dataSource.view();
        var sender = this;
        $.each(dataView, function (key, row) {
            var uid = row.uid;
            if (row.MessageType === 1) {
                $("#" + $(sender.element).attr('id') + " tbody").find("tr[data-uid=" + uid + "]").addClass("bg-info");
            }
            if (row.MessageType === 2) {
                $("#" + $(sender.element).attr('id') + " tbody").find("tr[data-uid=" + uid + "]").addClass("bg-warning");
            }
            if (row.MessageType === 3) {
                $("#" + $(sender.element).attr('id') + " tbody").find("tr[data-uid=" + uid + "]").addClass("bg-danger");
            }
        });
    }

    function MessagesDataBound(ev) {
        var dataView = this.dataSource.view();
        var table = ev.sender.element;
        if (dataView.length > 0) {
            $.each(dataView, function (key, row) {
                var currentUid = row.uid;
                var currenRow = table.find("tr[data-uid='" + currentUid + "']");
                if (row.IsDeleted) {
                    var editButton = $(currenRow).find(".k-grid-edit");
                    if (typeof editButton !== 'undefined') {
                        editButton.attr("style", "display:none !important");
                    }
                    var deleteButton = $(currenRow).find(".k-grid-delete");
                    if (typeof deleteButton !== 'undefined') {
                        deleteButton.attr("style", "display:none !important");
                    }

                    table.find("tr[data-uid=" + currentUid + "]").addClass("bg-danger");
                } else {
                    if (row.IsExpired) {
                        table.find("tr[data-uid=" + currentUid + "]").addClass("bg-disabled");
                    }
                }
            });
        }
    }

    function onChange(ev) {
        if (ev.action === 'add') {
            var addedRecord = ev.items[0];
            if (addedRecord !== null) {
                var targetDate = $('input[name=date]').data('kendoDatePicker').value();
                addedRecord.RecordTimestamp = targetDate;
            }
        }
    }

    function onPipeChange(ev) {
        //var grid = ev.sender;
        //var currentDataItem = grid.dataItem(this.select());

        if (ev.action === 'itemchange') {
            var addedRecord = ev.items[0];
            if (addedRecord !== null) {
                var targetDate = $('input[name=date]').data('kendoDatePicker').value();
                addedRecord.RecordTimestamp = targetDate;
            }
        }
    }

    function оnRequestEnd(ev) {
        if (ev.type === "destroy") {
            RefreshGrid('#inner-pipes');
            RefreshGrid('#tanks-statuses');
        }
    }

    return {
        DisplayErrors: DisplayErrors,
        ErrorHandler: errorHandler,
        CloseWindow: CloseWindow,
        RefreshGrid: RefreshGrid,
        ValueMapper: ValueMapper,
        SendHistoryData: SendHistoryData,
        DeletableDataBound: DeletableDataBound,
        AfterterNomGridValidation: afterterNomGridValidation,
        MessageBound: messageBound,
        MessagesDataBound: MessagesDataBound,
        OnChange: onChange,
        OnPipeChange: onPipeChange,
        OnRequestEnd: оnRequestEnd,
        ReportDetailsPaste: reportDetailsPaste,
        FullyRefreshGrid: FullyRefreshGrid
    };
})(kendo);