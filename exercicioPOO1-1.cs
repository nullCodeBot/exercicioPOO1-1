using System;
using System.Collections.Generic;

// Classe base Documento
public abstract class Documento
{
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public DateTime DataCriacao { get; set; }

    public Documento(string titulo, string autor)
    {
        Titulo = titulo;
        Autor = autor;
        DataCriacao = DateTime.Now;
    }

    public virtual void Imprimir()
    {
        Console.WriteLine($"Título: {Titulo}, Autor: {Autor}, Data: {DataCriacao}");
    }

    public abstract string ConteudoFormatado();
}

// Classe DocumentoTexto
public class DocumentoTexto : Documento
{
    public string Conteudo { get; set; }
    
    public DocumentoTexto(string titulo, string autor, string conteudo)
        : base(titulo, autor)
    {
        Conteudo = conteudo;
    }
    
    public override void Imprimir()
    {
        base.Imprimir();
        Console.WriteLine("Conteúdo:");
        Console.WriteLine(Conteudo);
    }
    
    public override string ConteudoFormatado()
    {
        return Conteudo;
    }
    
    public int ContarPalavras()
    {
        return Conteudo.Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;
    }
}

// Classe DocumentoHTML
public class DocumentoHTML : Documento
{
    public string Html { get; set; }
    public string Css { get; private set; } = "";
    
    public DocumentoHTML(string titulo, string autor, string html)
        : base(titulo, autor)
    {
        Html = html;
    }
    
    public override void Imprimir()
    {
        base.Imprimir();
        Console.WriteLine("HTML:");
        Console.WriteLine(Html);
        Console.WriteLine("CSS:");
        Console.WriteLine(Css);
    }
    
    public override string ConteudoFormatado()
    {
        return $"<style>{Css}</style>{Html}";
    }
    
    public void AdicionarEstilo(string css)
    {
        Css += css;
    }
}

// Classe DocumentoPDF
public class DocumentoPDF : Documento
{
    public string Texto { get; set; }
    public string MarcaDagua { get; private set; } = "";
    
    public DocumentoPDF(string titulo, string autor, string texto)
        : base(titulo, autor)
    {
        Texto = texto;
    }
    
    public override void Imprimir()
    {
        base.Imprimir();
        Console.WriteLine("Texto:");
        Console.WriteLine(Texto);
        if (!string.IsNullOrEmpty(MarcaDagua))
        {
            Console.WriteLine("Marca d'água: " + MarcaDagua);
        }
    }
    
    public override string ConteudoFormatado()
    {
        return Texto + (string.IsNullOrEmpty(MarcaDagua) ? "" : "\nMarca d'água: " + MarcaDagua);
    }
    
    public void AdicionarMarcaDagua(string texto)
    {
        MarcaDagua = texto;
    }
}

// Classe ProcessadorDocumentos
public class ProcessadorDocumentos
{
    public void ProcessarLote(List<Documento> documentos)
    {
        foreach (var doc in documentos)
        {
            doc.Imprimir();
            Console.WriteLine("--------------------------------");
        }
    }
}

// Programa principal
class Program
{
    static void Main()
    {
        var documentos = new List<Documento>
        {
            new DocumentoTexto("Relatório", "Alice", "Este é um relatório de exemplo."),
            new DocumentoHTML("Página Web", "Bob", "<h1>Bem-vindo</h1><p>Este é um documento HTML.</p>"),
            new DocumentoPDF("Contrato", "Carlos", "Este é um contrato oficial.")
        };
        
        ((DocumentoHTML)documentos[1]).AdicionarEstilo("h1 { color: blue; }");
        ((DocumentoPDF)documentos[2]).AdicionarMarcaDagua("Confidencial");
        
        var processador = new ProcessadorDocumentos();
        processador.ProcessarLote(documentos);
    }
}
