using System.ComponentModel.DataAnnotations;
using Flunt.Notifications;
using Flunt.Validations;
using NotificationDotNet6SignalR.Domain.Contracts;

namespace NotificationDotNet6SignalR.Domain.Commands.User;

public class UserLoginCommand : Notifiable<Notification>, ICommand
{
    [Required(ErrorMessage = "O e-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public string? ReturnUrl { get; set; }

    public void Validate()
    {
        AddNotifications(new Contract<Notification>().Requires()
            .IsNotNullOrEmpty(Email, "Email", "O e-mail deve ser informado.")
            .IsNotNullOrEmpty(Password, "Password", "A senha deve ser informado.")
        );

        if (!string.IsNullOrEmpty(Email))
        {
            AddNotifications(new Contract<Notification>().Requires()
                .IsEmail(Email, "Email", "O e-mail informado parece não ser válido.")
            );
        }

        if (!string.IsNullOrEmpty(Password))
        {
            AddNotifications(new Contract<Notification>().Requires()
                .IsBetween(Password.Length, 6, 20, Password, "O senha deve ter mais de 6 e menos de 40 caracteres.")
            );
        }
    }
}