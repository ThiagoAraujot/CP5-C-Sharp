using System.ComponentModel.DataAnnotations;

namespace GameStoreMVC.Models.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Nome obrigatório")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-mail obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Senha obrigatória")]
    [MinLength(6, ErrorMessage = "Mínimo 6 caracteres")]
    [DataType(DataType.Password)]
    public string Senha { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirmação obrigatória")]
    [DataType(DataType.Password)]
    [Compare("Senha", ErrorMessage = "As senhas não conferem")]
    [Display(Name = "Confirmação de Senha")]
    public string ConfirmacaoSenha { get; set; } = string.Empty;
}
