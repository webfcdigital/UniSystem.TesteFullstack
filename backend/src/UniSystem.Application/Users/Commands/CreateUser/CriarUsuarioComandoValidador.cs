using FluentValidation;

namespace UniSystem.Application.Users.Commands.CreateUser
{
    public class CriarUsuarioComandoValidador : AbstractValidator<CriarUsuarioComando>
    {
        public CriarUsuarioComandoValidador()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MaximumLength(100).WithMessage("O nome não deve exceder 100 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("Formato de e-mail inválido.")
                .MaximumLength(100).WithMessage("O e-mail não deve exceder 100 caracteres.");

            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres.");
        }
    }
}
