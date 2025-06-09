using AutoMapper;
using DAL.Entities;
using BusinessLogic.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using BusinessLogic.Models.OrderModels;
using BusinessLogic.Models.MasterModel;
using BusinessLogic.Models.ScheduleModel;
using BusinessLogic.Models.ServiceModels;
using BusinessLogic.Models.CarModel;
using BusinessLogic.Models.User;

namespace BusinessLogic
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Car, GetCar>();
            CreateMap<CreateCar, Car>();


            CreateMap<AddMaster, Master>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => new User
                {
                    UserName = src.UserName,
                    Email = src.Email,
                    PhoneNumber = src.PhoneNumber
                }));

            CreateMap<Master, GetMaster>();
            CreateMap<Payment, PaymentModel>();

            CreateMap<PaymentModel, Payment>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.OrderId))
            .ReverseMap();

            CreateMap<ReviewModel, Review>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.OrderId)) 
                .ForMember(dest => dest.ReviewDate, opt => opt.MapFrom(_ => DateOnly.FromDateTime(DateTime.Now)));


            CreateMap<Review, ReviewModel>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id));
                

            CreateMap<CreateSchedule, Schedule>();
            CreateMap<Schedule, GetSchedule>();

            CreateMap<Service, GetService>();
            CreateMap<CreateService, Service>()
                .ForMember(dest => dest.OrderServices, opt => opt.Ignore());

            CreateMap<CreateOrder, Order>()
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(_ => DateOnly.FromDateTime(DateTime.Now)))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => "Created"));

            CreateMap<Order, MasterOrder>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Client.UserName));

            CreateMap<Order, ClientOrder>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src =>$"{src.Master.FirstName} {src.Master.LastName}"));



            CreateMap<Register, User>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<UpdateUser, User>();
            CreateMap<User, GetUser>();


        }
    }
}
