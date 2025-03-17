namespace Laba_11.Models
{
    public class EcologicalNiche
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<BirdEcologicalNiche> BirdEcologicalNiches { get; set; } = [];
    }
}
