namespace GameStoreMVC.Models;

public class Game
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string DescricaoCurta { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public string UrlCapa { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public bool Destaque { get; set; }
}
