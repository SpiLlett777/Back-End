using System.ComponentModel.DataAnnotations;

namespace Laba_11.Dtos
{
    public class EcologicalNicheDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> BirdNames { get; set; } = [];
    }

    public class CreateEcologicalNicheDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;
    }
    
    public class UpdateEcologicalNicheDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
