using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        ICarRepository CarRepository { get; set; }
        IMasterRepository MasterRepository { get; set; }
        IOrderRepository OrderRepository { get; set; }
        IOrderServiceRepository OrderServiceRepository { get; set; }
        IPaymentRepository PaymentRepository { get; set; }
        IScheduleRepository ScheduleRepository { get; set; }
        IServiceRepository ServiceRepository { get; set; }
        IUserRepository UserRepository { get; set; }

        IReviewRepository ReviewRepository { get; set; }

        Task SaveAsync();
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IBaseEntity;
    }
}
