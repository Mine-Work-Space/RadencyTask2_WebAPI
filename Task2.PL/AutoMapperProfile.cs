using AutoMapper;
using Task2.DAL.DbModels;
using Task2.ModelsLibrary;

namespace Task2.PL {
    public class AutoMapperProfile : Profile {
        public AutoMapperProfile() {
            CreateMap<Book, BookDTO>()
                .BeforeMap((x, expr) => x.Ratings = x.Ratings != null ? x.Ratings = x.Ratings : x.Ratings = new List<Rating>())
                .ForMember(x => x.Rating, expr => 
                    expr.MapFrom(y => Math.Round(y.Ratings.Sum(g => g.Score) > 0 ? y.Ratings.Sum(g => g.Score) / y.Ratings.Count() : 0, 2)))
                // If no ratings 0, if sum of scores > 0 -> scores / count of scores
                .ForMember(x => x.ReviewsNumber, expr => expr.MapFrom(y => y.Reviews.Count()));
            CreateMap<Book, BookWithReviewsDTO>()
                .BeforeMap((x, expr) => x.Ratings = x.Ratings != null ? x.Ratings = x.Ratings : x.Ratings = new List<Rating>())
                .ForMember(x => x.Rating, expr =>
                    expr.MapFrom(y => Math.Round(y.Ratings.Sum(g => g.Score) > 0 ? y.Ratings.Sum(g => g.Score) / y.Ratings.Count() : 0, 2)));
            CreateMap<Review, ReviewDTO>();
            CreateMap<ReviewWithoutIdDTO, Review>();
            CreateMap<Rating, RatingDTO>();
            CreateMap<SaveBookDTO, Book>();
        }
    }
}
