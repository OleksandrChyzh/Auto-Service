using DAL.Interfaces;
using DAL.Entities;
using DAL.Repositories;
using DAL.Data;

namespace DAL
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        public ICarRepository CarRepository { get; set; } = new CarRepository(context);
        public IMasterRepository MasterRepository { get; set; } = new MasterRepository(context);
        public IOrderRepository OrderRepository { get; set; } = new OrderRepository(context);
        public IOrderServiceRepository OrderServiceRepository { get; set; } = new OrderServiceRepository(context);
        public IPaymentRepository PaymentRepository { get; set; } = new PaymentRepository(context);
        public IScheduleRepository ScheduleRepository { get; set; } = new ScheduleRepository(context);
        public IServiceRepository ServiceRepository { get; set; } = new ServiceRepository(context);
        public IReviewRepository ReviewRepository { get; set; } = new ReviewRepository(context);

        public IUserRepository UserRepository { get; set; } = new UserRepository(context);

        public Task SaveAsync()
        {
            return context.SaveChangesAsync();
        }

        public IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class,  IBaseEntity
        {
            return typeof(TEntity).Name switch
            {
                nameof(Car) => (IRepository<TEntity>)CarRepository,
                nameof(Master) => (IRepository<TEntity>)MasterRepository,
                nameof(Order) => (IRepository<TEntity>)OrderRepository,
                nameof(OrderService) => (IRepository<TEntity>)OrderServiceRepository,
                nameof(Payment) => (IRepository<TEntity>)PaymentRepository,
                nameof(Schedule) => (IRepository<TEntity>)ScheduleRepository,
                nameof(Service) => (IRepository<TEntity>)ServiceRepository,
                nameof(Review) => (IRepository<TEntity>)ReviewRepository,
                nameof(User) => (IRepository<TEntity>)UserRepository,
                _ => throw new InvalidOperationException($"Repository for type {typeof(TEntity)} not found"),
            };
        }
    }
}
