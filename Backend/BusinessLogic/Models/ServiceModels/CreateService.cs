using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Models.ServiceModels
{
    public class CreateService : ServiceModel
    {
        [Required]
        public new string Name
        {
            get => base.Name;
            set => base.Name = value;
        }

        [Required]
        public new string? Description
        {
            get => base.Description;
            set => base.Description = value;
        }

        [Required]
        public new decimal Price
        {
            get => base.Price;
            set => base.Price = value;
        }
    }
}
