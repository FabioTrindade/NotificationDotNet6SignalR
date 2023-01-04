$(function(){
    $('#IsRead').on('change.bootstrapSwitch', function(e) {
        const id = e.target.dataset.notificationId;
        const status = e.target.checked;

        ChangeStatus(id, status);
    });

    function ChangeStatus(id, status) {
        sendRequest('PUT', '/Notification/ChangeRead', 'json', { id, status } , (retorno) => {
            if (!retorno.data.success) {
                showAlert('warning', 'Mensagem Retorno : ', retorno.data.message, () => { }, () => { });
                return;
            }
            toast('success', 'Notificação alterada com sucesso!');
        }, (err) => {
            showAlert('info', 'Ooops!', 'Verifique sua conexão e tente novamente', () => { });
        })
    }
});
