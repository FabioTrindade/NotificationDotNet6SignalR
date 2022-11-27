using System;
using Microsoft.AspNetCore.Identity;
using NotificationDotNet6SignalR.Domain.Commands;
using NotificationDotNet6SignalR.Domain.Commands.User;
using NotificationDotNet6SignalR.Domain.Entities;
using NotificationDotNet6SignalR.Domain.Services;

namespace NotificationDotNet6SignalR.Services
{
	public class UserService : IUserService
	{
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<GenericCommandResult> Handle(UserRegisterCommand command)
        {
            // Executa as validacoes
            command.Validate();

            // Utilizado para retornar as validações caso aponte algo
            if (!command.IsValid)
                return new GenericCommandResult(false, "", command.Notifications);

            // Verifica se o e-mail esta em uso
            var isEmailInUse = await _userManager.FindByEmailAsync(command.Email);

            // Se e-mail existe adiciona uma notificação e devolve
            if (isEmailInUse != null)
            {
                command.AddNotification("Email", $"O e-mail {command.Email} já está sendo usado.");
                return new GenericCommandResult(false, "", command.Notifications);
            }

            // Preenche a classe IdentityUser
            var user = new User(command.FirstName, command.LastName, command.Email, command.Email);

            // Cria usuário na tabela AspNetUsers
            var userCreate = await _userManager.CreateAsync(user, command.Password);

            // Caso apresente algum erro na criação do usuário adiciona as notificações e devolve
            if (!userCreate.Succeeded)
            {
                userCreate.Errors.ToList().ForEach(erro =>
                {
                    command.AddNotification("", erro.Description);
                });

                return new GenericCommandResult(false, "", command.Notifications);
            }

            return new GenericCommandResult(true, "", userCreate);
        }

        public async Task<GenericCommandResult> Handle(UserLoginCommand command)
        {
            // Executa as validacoes
            command.Validate();

            // Utilizado para retornar as validações caso aponte algo
            if (!command.IsValid)
                return new GenericCommandResult(false, "", command.Notifications);

            // Verifica se o e-mail existe
            var signedUser = await _userManager.FindByEmailAsync(command.Email);

            // Caso não existe adicionar uma notificação e devolve
            if (signedUser == null)
            {
                command.AddNotification("", "E-mail e/ou senha inválida.");
                return new GenericCommandResult(false, "", command.Notifications);
            }

            // Verifica se o e-mail foi confirmado
            if (!await _userManager.IsEmailConfirmedAsync(signedUser))
            {
                command.AddNotification("", "Verifique sua caixa de e-mail e confirme o cadastro.");
                return new GenericCommandResult(false, "", command.Notifications);
            }

            // Verifica se o usuário esta bloqueado
            if (await _userManager.IsLockedOutAsync(signedUser))
            {
                command.AddNotification("", "Conta bloqueada, entre em contato com suporte.");
                return new GenericCommandResult(false, "", command.Notifications);
            }

            // realiza o login e autentica no identity
            SignInResult login = await _signInManager.PasswordSignInAsync(signedUser, command.Password, false, lockoutOnFailure: false);

            if (!login.Succeeded)
            {
                command.AddNotification("", "E-mail e/ou senha inválida.");
                return new GenericCommandResult(false, "", command.Notifications);
            }

            // Register last access in application
            signedUser.SetLastAccess(DateTime.Now);

            await _userManager.UpdateAsync(signedUser);

            return new GenericCommandResult(true, "", login);
        }

        public void Logout()
        {
            _signInManager.SignOutAsync();
        }
    }
}