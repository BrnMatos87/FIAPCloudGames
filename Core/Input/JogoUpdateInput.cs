namespace Core.Input
{
    public class JogoUpdateInput
    {
        public required int Id { get; set; }
        public required string Nome { get; set; }
        public required string Categoria { get; set; }
        public required decimal Preco { get; set; }
    }
}
