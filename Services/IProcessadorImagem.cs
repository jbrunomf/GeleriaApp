namespace App.Services
{
    public interface IProcessadorImagem
    {
        Task<bool> SalvarUploadIagemAsync(string caminhoArquivoImagem, IFormFile imagem);
        Task<bool> ExcluirImagemAsync(string caminhoArquivoImagem);
        Task<bool> AplicarEfeitoAsync(string caminhoArquivoImagem, EfeitoImagem efeito);
    }
}
