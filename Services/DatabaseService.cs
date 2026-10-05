using LiteDB;
using MinhaBiblioteca.Models;

namespace MinhaBiblioteca.Services;

public class DatabaseService
{
    private readonly string _databasePath;

    public DatabaseService()
    {
        _databasePath = Path.Combine(FileSystem.AppDataDirectory, "MinhaBiblioteca.db");
    }

    public void AdicionarLivro(Livro livro)
    {
        using var db = new LiteDatabase(_databasePath);
        var livrosCollection = db.GetCollection<Livro>("livros");
        livrosCollection.Insert(livro);
    }

    public List<Livro> ObterTodosLivros()
    {
        using var db = new LiteDatabase(_databasePath);
        var livrosCollection = db.GetCollection<Livro>("livros");
        return livrosCollection.FindAll().ToList();
    }

    public void AtualizarLivro(Livro livro)
    {
        using var db = new LiteDatabase(_databasePath);
        var livrosCollection = db.GetCollection<Livro>("livros");
        livrosCollection.Update(livro);
    }

    public void RemoverLivro(int id)
    {
        using var db = new LiteDatabase(_databasePath);
        var livrosCollection = db.GetCollection<Livro>("livros");
        livrosCollection.Delete(id);
    }

    public Livro? ObterLivroPorId(int id)
    {
        using var db = new LiteDatabase(_databasePath);
        var livrosCollection = db.GetCollection<Livro>("livros");
        return livrosCollection.FindById(id);
    }
}