using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Models.OrderModels;

public class CreateOrder
{
    [Required]
    public int MasterId { get; set; }

    [Required]
    public int CarId { get; set; }

    [Required]
    public List<int> ServiceIds { get; set; } = new();

}
