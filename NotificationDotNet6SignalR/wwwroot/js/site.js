// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

});

// Auxiliar para ajustar parametros da requisição
const transformRequestOptions = params => {

    let options = '';
    let x = 0;
    let item;
    let newItem;
    for (const key in params) {
        if (typeof params[key] !== 'object' && params[key]) {
            options += `${key}=${params[key]}&`;
        } else if (typeof params[key] === 'object' && params[key] && params[key].length) {
            params[key].forEach(el => {
                newItem = params[key];
                if (newItem != item)
                    x = 0

                options += `${key}[${[x++]}]=${el}&`
                item = params[key];
            });
        }
    }
    return options ? options.slice(0, -1) : options;
};

// Padronização das chamadas
function sendRequest(method, url, responseType, parameters, callbackSuccess, callbackError) {

    if (!url || url.length == 0)
        return false;

    axios({
        method: (method == undefined || method == '' ? "GET" : method),
        url: url,
        responseType: (responseType == undefined || responseType == '' ? "json" : responseType),
        params: parameters,
        //paramsSerializer: function (parameters) {
        //    return Qs.stringify(params, { arrayFormat: 'repeat' })
        //}
        //paramsSerializer: params => transformRequestOptions(params)
    })
    .then(function (response) {
        if (callbackSuccess)
            return callbackSuccess(response);
    })
    .catch(function (error) {
        if (callbackError)
            return callbackError(error.data, error.status);
    })
}

// SweetAlert - Alert
function showAlert(type, title, message, callbackOK) {
    Swal.fire({
        icon: type,
        title: title,
        html: message,
        showCancelButton: false,
        allowOutsideClick: false
    }).then(callbackOK)
}

// SweetAlert - Confirm
function showConfirm(type, title, message, callbackContinuar, callbackCancelar, suplirLabelCancelar) {
    Swal.fire({
        icon: type,
        title: title,
        html: message,
        showCancelButton: true,
        allowOutsideClick: false,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim',
        cancelButtonText: !suplirLabelCancelar ? 'Cancelar' : suplirLabelCancelar
    }).then((result) => {
        if (result.isConfirmed)
            return callbackContinuar();
        else
            return callbackCancelar();
    });
}

// SweetAlert - Loading
function showLoading(titulo, mensagem, tempo, callbackExecutar, callbackAposExecutar) {
    Swal.fire({
        title: (titulo == undefined || titulo == '' ? 'Aguarde' : titulo),
        padding: 50,
        showCancelButton: false,
        showConfirmButton: false,
        timer: (tempo == undefined || !tempo ? 5500 : tempo),
        html: (mensagem == undefined || mensagem == '' ? 'Aguarde, sua solicitação está sendo processada...' : mensagem),
        showClass: {
            popup: 'swal2-noanimation',
            backdrop: 'swal2-noanimation'
        },
        hideClass: {
            popup: '',
            backdrop: ''
        },
        allowOutsideClick: false,
        allowEscapeKey: false,
        customClass: 'animated zoomIn',
        didOpen: function () {
            swal.showLoading();
            if (callbackExecutar)
                return callbackExecutar();
        }
    }).then(function () {
        if (callbackAposExecutar)
            return callbackAposExecutar();
    }, function (dismiss) {
        if (callbackAposExecutar)
            return callbackAposExecutar();
    });
}

// SweetAlert - Mixin
const Mixin = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 3000,
    timerProgressBar: true,
    didOpen: (toast) => {
        toast.addEventListener('mouseenter', Swal.stopTimer)
        toast.addEventListener('mouseleave', Swal.resumeTimer)
    }
});

// SweetAlert - Toast
function toast(icon = 'success', title = 'Salvo com sucesso', redirect = false, url = null) {
    Mixin.fire({
        icon: icon,
        title: title
    }).then(function () {
        if (redirect) {
            if (!url) {
                window.location.reload();
            } else {
                window.location.href = url;
            }
        }
    });
}

// Popula Combobox via Ajax Paginado
function createComboAjaxPagination(elemento, placeholder, url, modal) {
    $(`#${elemento}`).select2({
        placeholder: !placeholder ? 'Selecionar' : placeholder,
        dropdownParent: !modal ? null : $(`#modal_${modal}`),
        language: 'pt-BR',
        allowClear: true,
        ajax: {
            url: url,
            dataType: 'json',
            method: 'GET',
            delay: 250,
            data: function (params) {
                return {
                    currentPage: params.page || 0,
                    perPage: params.pageSize || 30,
                    searchParameter: params.term
                };
            },
            processResults: function (data, params) {
                params.page = params.page || 1;
                return {
                    pagination: {
                        more: (params.page * 30) < (data != null) ? data[0].Total : 0
                    },
                    results: $.map(data, function (obj) {
                        return { id: obj.value, text: obj.text };
                    })
                };
            },
            cache: true
        },
        escapeMarkup: function (markup) { return markup; },
        minimumInputLength: 0
    });
}