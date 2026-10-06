using MinhaBiblioteca.Models;
using MinhaBiblioteca.Services;

namespace MinhaBiblioteca.Views;

public partial class LeitorPage : ContentPage
{
    private readonly Livro _livro;
    private readonly DatabaseService _databaseService;

    public LeitorPage(Livro livro)
    {
        InitializeComponent();
        _livro = livro;
        _databaseService = new DatabaseService();
        lblTitulo.Text = _livro.Titulo;
        lblAutor.Text = _livro.Autor;
        pdfWebView.Source = "pdfjs/web/viewer.html?file=";
    }

    private async void pdfWebView_Navigated(object sender, WebNavigatedEventArgs e)
    {
        if (!File.Exists(_livro.CaminhoLocal))
        {
            await DisplayAlertAsync("Erro", "O arquivo PDF não foi encontrado.", "OK");
            return;
        }

        byte[] bytes = await File.ReadAllBytesAsync(_livro.CaminhoLocal);

        string base64 = Convert.ToBase64String(bytes);


        string script = $$"""
            (async function () {

                const base64 = "{{base64}}";

                const binary = atob(base64);

                const bytes = new Uint8Array(binary.length);

                for (let i = 0; i < binary.length; i++) {
                    bytes[i] = binary.charCodeAt(i);
                }

                await PDFViewerApplication.initializedPromise;

                await PDFViewerApplication.open({
                    data: bytes
                });

                PDFViewerApplication.page = {{_livro.PaginaAtual}};

            })();
            """;

        try
        {
            string resultado = await pdfWebView.EvaluateJavaScriptAsync(script);

            await DisplayAlertAsync("JavaScript Executado", resultado ?? "Sem retorno", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro no leitor", ex.Message, "OK");
        }
    }
}