// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.
var SinglePage = {};

var ShowModal = function () {
    $("#modal").modal("show");
}

SinglePage.EnableModal = function () {
    window.onhashchange = function () {
        var url = window.location.hash.toLowerCase();
        if (!url.startsWith("#showmodal="))
            return;
        url = url.split("showmodal=")[1];
        $.get(url, null, function (htmlResult) {
            $("#modal-body").html(htmlResult);
            const container = document.getElementById("modal-body");
            const forms = container.getElementsByTagName("form");
            const newForm = forms[forms.length - 1];
            $.validator.unobtrusive.parse(newForm);
            ShowModal();
        });
        $("#modal").on("shown.bs.modal", function () { window.location.hash = "##"; });
    };
};

$(document).ready(function () {
    SinglePage.EnableModal();
});

$(document).on("submit", 'form[data-ajax="true"]', function (e) {
    e.preventDefault();
    const form = $(this);
    const method = form.attr("method").toLocaleLowerCase();
    const targetUrl = form.attr("action");
    const action = form.attr("data-action");
    if (method === "get") {
        form = form.serializeArray();
        $.get(targetUrl, data, function (response) { CallBackHandler(response, action, form) });
    }
    else if (method === "post") {
        var data = new FormData(this);
        $.ajax({
            url: targetUrl,
            type: "post",
            data: data,
            enctype: "multipart/form-data",
            dataType: "json",
            processData: false,
            contentType: false,
            success: function (response) {
                CallBackHandler(response, action, form)
            },
            error: function (response) { alert(response.Message) }
        });
    }
});

function CallBackHandler(data, action, form) {
    switch (action) {
        case "message":
            alert(data.Message);
            break;
        case "refresh":
            if (data.isSucceeded) {
                window.location.reload();
            }
            else {
                alert(data.Message)
            }
            break;
        case "relist":
            hideModal();
            const refreshUrl = form.attr("data-refreshurl");
            const refreshDiv = form.attr("data-refreshdivbyid");
            get(refereshUrl, refereshDiv);
            break;
        case "setValue":
    
        const element = form.data("element");
        $(`#${element}`).html(data);
        break;
        default: 
}
}

function get(url, refreshDiv) {
    const searchModel = window.location.search;
    $.get(url,
        searchModel,
        function (result) {
            $("#" + refreshDiv).html(result);
        });
}
function makeSlug(source, dist) {
    const value = $('#' + source).val();
    $('#' + dist).val(convertToSlug(value));
}

var convertToSlug = function (str) {
    var $slug = '';
    const trimmed = $.trim(str);
    $slug = trimmed.replace(/[^a-z0-9-آ-ی-]/gi, '-').replace(/-+/g, '-').replace(/^-|-$/g, '');
    return $slug.toLowerCase();
};

function fillField(source, dist) {
    const value = $('#' + source).val();
    $('#' + dist).val(value);
}
