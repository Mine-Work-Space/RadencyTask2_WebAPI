using AutoMapper;
using Task2.DAL.DbModels;
using Task2.ModelsLibrary;

namespace Task2.PL {
    public class AutoMapperProfile : Profile {
        public AutoMapperProfile() {
            CreateMap<Book, BookDTO>()
                .ForMember(x => x.Rating, expr => expr.MapFrom(y => y.Ratings.Sum(g => g.Score)))
                .ForMember(x => x.ReviewsNumber, expr => expr.MapFrom(y => y.Reviews.Count()));
        }
    }
}
