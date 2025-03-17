namespace Laba_11.Models
{
    public class BirdEcologicalNiche
    {
        public int BirdId { get; set; }
        public int EcologicalNicheId { get; set; }
        public Bird Bird { get; set; } = null!;
        public EcologicalNiche EcologicalNiche { get; set; } = null!;
    }
}
