using System.ComponentModel.DataAnnotations;

namespace GameStoreMVC.Models.ViewModels;

public class GameViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Nome obrigatório")]
    [Display(Name = "Nome do Jogo")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Descrição obrigatória")]
    [Display(Name = "Descrição Curta")]
    public string DescricaoCurta { get; set; } = string.Empty;

    [Required(ErrorMessage = "Preço obrigatório")]
    [Display(Name = "Preço (R$)")]
    [Range(0.01, 99999.99, ErrorMessage = "Preço inválido")]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "URL da capa obrigatória")]
    [Display(Name = "URL da Capa")]
    public string UrlCapa { get; set; } = string.Empty;

    [Required(ErrorMessage = "Categoria obrigatória")]
    public string Categoria { get; set; } = string.Empty;

    [Display(Name = "Em Destaque na Home")]
    public bool Destaque { get; set; }
}
