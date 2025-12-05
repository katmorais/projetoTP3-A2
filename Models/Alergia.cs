namespace projetoTP3_A2.Models
{
    public class Alergia
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } // Ex: Penicilina, Amendoim
        public string Reacao { get; set; } // Ex: Anafilaxia, Urticária
    }
}
