using AutoMapper;
using Task2.DAL.DbModels;
using Task2.ModelsLibrary;

namespace Task2.PL {
    public class AutoMapperProfile : Profile {
        public AutoMapperProfile() {
            CreateMap<Book, BookDTO>()
                .ForMember(x => x.Rating, expr => expr.MapFrom(y => Math.Round(y.Ratings.Sum(g => g.Score) / y.Ratings.Count(), 2)))
                .ForMember(x => x.ReviewsNumber, expr => expr.MapFrom(y => y.Reviews.Count()));
            CreateMap<Book, BookWithReviewsDTO>()
                .ForMember(x => x.Rating, expr => expr.MapFrom(y => Math.Round(y.Ratings.Sum(g => g.Score) / y.Ratings.Count(), 2)));
            CreateMap<Review, ReviewDTO>();
            CreateMap<SaveBookDTO, Book>();
        }
    }
}
