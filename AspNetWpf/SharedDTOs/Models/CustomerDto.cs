using System.ComponentModel.DataAnnotations;

namespace SharedDTOs.Models
{
    public record CustomerDto()
    {
        [Required]
        [StringLength(10, MinimumLength = 1)]
        public string Code { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string NameKana { get; set; } = null!;

        [Required]
        public string Prefecture { get; set; } = null!;
    };
}
