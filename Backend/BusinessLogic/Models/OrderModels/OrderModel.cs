using BusinessLogic.Models.CarModel;

namespace BusinessLogic.Models.OrderModels
{
    public class OrderModel
    {
        public int Id { get; set; }
        public DateOnly OrderDate { get; set; }
        public string Status { get; set; } = null!;
        public decimal TotalCost { get; set; }
        public GetCar Car { get; set; }

        public ReviewModel? Review { get; set; }

        public PaymentModel? Payment { get; set; }
        public List<string> ServiceNames { get; set; } = new();
    }
}
