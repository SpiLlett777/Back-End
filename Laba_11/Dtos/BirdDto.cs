using System.ComponentModel.DataAnnotations;
namespace Laba_11.Dtos
{
    public class BirdDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ScientificName { get; set; } = string.Empty;
        public double Wingspan { get; set; }
        public double Weight { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public List<string> BirdEcologicalNicheNames { get; set; } = [];
    }
    public class CreateBirdDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string ScientificName { get; set; } = string.Empty;
        [Required]
        public double Wingspan { get; set; }
        [Required]
        public double Weight { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public List<int> BirdEcologicalNicheIds { get; set; } = [];
    }
    public class UpdateBirdDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string ScientificName { get; set; } = string.Empty;
        [Required]
        public double Wingspan { get; set; }
        [Required]
        public double Weight { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public List<int> BirdEcologicalNicheIds { get; set; } = new List<int>();
    }
}
