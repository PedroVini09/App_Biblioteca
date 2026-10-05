using MinhaBiblioteca.Models;
using MinhaBiblioteca.Services;
using MinhaBiblioteca.Views;

namespace MinhaBiblioteca;

public partial class MainPage : ContentPage
{

	private readonly DatabaseService _databaseService;
	public MainPage()
	{
		InitializeComponent();
		_databaseService = new DatabaseService();
		CarregarLivros();
	}

	private void CarregarLivros()
	{
		var livros = _databaseService.ObterTodosLivros();
		listaLivros.ItemsSource = livros;
	}

	private async void btnAdicionarLivro_Clicked(object? sender, EventArgs e)
	{
		var resultado = await FilePicker.Default.PickAsync(new PickOptions
		{
			PickerTitle = "Selecione um livro em PDF"
		});

		if(resultado == null)
			return;

		string extensao = Path.GetExtension(resultado.FileName);
		if(!extensao.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
		{
			await DisplayAlertAsync("Erro", "Selecione um arquivo no formato PDF.", "OK");
			return;
		}

		string pastalivros = Path.Combine(FileSystem.AppDataDirectory, "Livros");

		Directory.CreateDirectory(pastalivros);

		string destino = Path.Combine(pastalivros, resultado.FileName);

		using Stream origem = await resultado.OpenReadAsync();
		
		using FileStream destinoStream = File.Create(destino);
		await origem.CopyToAsync(destinoStream);

		Livro livro = new Livro
		{
			Titulo = Path.GetFileNameWithoutExtension(resultado.FileName),
			Autor = "Desconhecido",
			NomeArquivo = resultado.FileName,
			CaminhoLocal = destino,
			PaginaAtual = 1,
			Favorito = false,
			DataAdicionado = DateTime.Now
		};

		_databaseService.AdicionarLivro(livro);
		CarregarLivros();
		lblStatus.Text = $"Livro '{livro.Titulo}' adicionado com sucesso!";

		
	}

	private async void listaLivros_SelectionChanged(object? sender, SelectionChangedEventArgs e)
	{
		Livro? livroSelecionado = e.CurrentSelection.FirstOrDefault() as Livro;
		if(livroSelecionado == null)
			return;

		await Navigation.PushAsync(new LeitorPage(livroSelecionado));

		listaLivros.SelectedItem = null;
	}
}
