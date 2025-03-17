using Laba_11.Dtos;
using Laba_11.Models;

namespace Laba_11.Mappers
{
    public static class BirdMapper
    {
        public static BirdDto ToDto(this Bird bird)
        {
            return new BirdDto
            {
                Id = bird.Id,
                Name = bird.Name,
                ScientificName = bird.ScientificName,
                Wingspan = bird.Wingspan,
                Weight = bird.Weight,
                ImageUrl = bird.ImageUrl,
                BirdEcologicalNicheNames = bird.BirdEcologicalNiches
                    .Select(x => x.EcologicalNiche.Name).ToList()
            };
        }

        public static Bird ToModel(this CreateBirdDto birdDto)
        {
            var bird = new Bird
            {
                Name = birdDto.Name,
                ScientificName = birdDto.ScientificName,
                Wingspan = birdDto.Wingspan,
                Weight = birdDto.Weight,
                ImageUrl = birdDto.ImageUrl,
            };

            bird.BirdEcologicalNiches = birdDto.BirdEcologicalNicheIds
                .Select(id => new BirdEcologicalNiche { BirdId = bird.Id, EcologicalNicheId = id })
                .ToList();

            return bird;
        }

        public static Bird FromUpdateToModel(this UpdateBirdDto birdDto, Bird existingBird)
        {
            existingBird.Name = birdDto.Name;
            existingBird.ScientificName = birdDto.ScientificName;
            existingBird.Wingspan = birdDto.Wingspan;
            existingBird.Weight = birdDto.Weight;
            existingBird.ImageUrl = birdDto.ImageUrl;
            existingBird.BirdEcologicalNiches = birdDto.BirdEcologicalNicheIds
                .Select(id => new BirdEcologicalNiche { 
                    BirdId = existingBird.Id, 
                    EcologicalNicheId = id 
                })
                .ToList();

            return existingBird;
        }
    }
}
