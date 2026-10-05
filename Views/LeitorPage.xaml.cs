using MinhaBiblioteca.Models;

namespace MinhaBiblioteca.Views;

public partial class LeitorPage : ContentPage
{
    private readonly Livro _livro;

    public LeitorPage(Livro livro)
    {
        InitializeComponent();
        _livro = livro;
        lblTitulo.Text = _livro.Titulo;
        lblAutor.Text = _livro.Autor;
       pdfWebView.Source = new HtmlWebViewSource
        {
            Html = """
                <html>
                    <body style="font-family: Arial;">
                       <h2>Leitor Funcionando</h2>
                       <p>Aqui será exibido o PDF.</p>'
                    </body>
                </html>
                """
       };
    }
}