namespace MinhaBiblioteca.Models;

public class Livro
{
    public int Id {get; set;}
    public string Titulo {get; set;} = string.Empty;
    public string Autor {get; set;} = string.Empty;
    public string NomeArquivo {get;set;}= string.Empty;
    public string CaminhoLocal {get;set;}= string.Empty;
    public int PaginaAtual {get;set;}= 1;
    public bool Favorito{get;set;}=false;
    public DateTime DataAdicionado {get;set;}= DateTime.Now;
}