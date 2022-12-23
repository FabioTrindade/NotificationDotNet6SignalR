using System.ComponentModel.DataAnnotations;
using Flunt.Notifications;
using Flunt.Validations;
using NotificationDotNet6SignalR.Domain.Contracts;

namespace NotificationDotNet6SignalR.Domain.Commands.Notifications;

public class NotificationCreateCommand : Notifiable<Notification>, ICommand
{
    [Required(ErrorMessage = "O cabeçalho é obrigatório", AllowEmptyStrings = false)]
    [Display(Name = "Cabeçalho")]
    public string Header { get; set; }

    [Required(ErrorMessage = "O conteudo é obrigatório", AllowEmptyStrings = false)]
    [Display(Name = "Conteúdo")]
    public string Content { get; set; }

    [Display(Name = "Para")]
    public Guid? ToUserId { get; set; }

    // Validations
    public void Validate()
    {
        AddNotifications(new Contract<Notification>().Requires()
            .IsNotNullOrEmpty(Header, "Header", "O cabeçalho deve ser informado.")
            .IsNotNullOrEmpty(Content, "Content", "O conteudo deve ser informado.")
        );
    }
}