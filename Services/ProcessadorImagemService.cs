namespace App.Services
{
    public class ProcessadorImagemService : IProcessadorImagem
    {
        public async Task<bool> AplicarEfeitoAsync(string caminhoArquivoImagem, EfeitoImagem efeito)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExcluirImagemAsynn(string caminhoArquivoImagem)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SalvarUploadIagemAsync(string caminhoArquivoImagem, IFormFile imagem)
        {
            if(imagem is null) return false;

            var ms = new MemoryStream();
            await imagem.CopyToAsync(ms);
            ms.Position = 0;
            return await SalvarImagemComoWebpAsync(caminhoArquivoImagem, ms, true);
        }

        private static async Task<bool> SalvarImagemComoWebpAsync(string caminhoArquivoImagem, MemoryStream ms, bool quadrada = true)
        {
            var img = await Image.LoadAsync(ms);
            var extensaoImagem = caminhoArquivoImagem.Substring(caminhoArquivoImagem.LastIndexOf('.')).ToLower();

            if (quadrada)
            {
                var tamanho = img.Size;
                var ladoMenor = (tamanho.Height < tamanho.Width) ? tamanho.Height : tamanho.Width;

                img.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(ladoMenor, ladoMenor),
                    Mode = ResizeMode.Crop

                }).BackgroundColor(new Rgba32(255, 255, 255, 0)));
            }
            caminhoArquivoImagem = caminhoArquivoImagem.Replace(extensaoImagem, ".webp");
            await img.SaveAsWebpAsync(caminhoArquivoImagem);
            return true;
        }
    }
}
