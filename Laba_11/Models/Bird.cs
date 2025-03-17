namespace Laba_11.Models
{
    public class Bird
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ScientificName { get; set; } = string.Empty;
        public double Wingspan { get; set; }
        public double Weight { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public List<BirdEcologicalNiche> BirdEcologicalNiches { get; set; } = [];
    }
}
