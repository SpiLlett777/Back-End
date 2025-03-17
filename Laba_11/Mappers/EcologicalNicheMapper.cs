using Laba_11.Dtos;
using Laba_11.Models;

namespace Laba_11.Mappers
{
    public static class EcologicalNicheMapper
    {
        public static EcologicalNicheDto ToDto(this EcologicalNiche niche)
        {
            return new EcologicalNicheDto
            {
                Id = niche.Id,
                Name = niche.Name,
                Description = niche.Description,
                BirdNames = niche.BirdEcologicalNiches.Select(x => x.Bird.Name).ToList()
            };
        }

        public static EcologicalNiche ToModel(this CreateEcologicalNicheDto nicheDto)
        {
            return new EcologicalNiche
            {
                Name = nicheDto.Name,
                Description = nicheDto.Description
            };
        }

        public static EcologicalNiche FromUpdateToModel(this UpdateEcologicalNicheDto dto, EcologicalNiche existingNiche)
        {
            existingNiche.Name = dto.Name;
            existingNiche.Description = dto.Description;

            return existingNiche;
        }
    }
}
