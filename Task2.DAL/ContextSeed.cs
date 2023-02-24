using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.DAL.DbModels;

namespace Task2.DAL {
    public static class ContextSeed {
        public static void Seed(ApiContext context) {
            var books = new List<Book>() {
                new Book() {
                    Id = 1,
                    Author = "J.K. Rowling",
                    Content = "Harry Potter is about to start his fifth year at Hogwarts School of Witchcraft and Wizardry." +
                    " Unlike most schoolboys, Harry never enjoys his summer holidays, but this summer is even worse than usual." +
                    " The Dursleys, of course, are making his life a misery, but even his best friends, Ron and Hermione, seem to be neglecting him.",
                    Cover = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1546910265i/2.jpg",
                    Genre = "Fantasy",
                    Title = "Harry Potter and the Order of the Phoenix"
                },
                new Book() {
                    Id = 2,
                    Author = "J.K. Rowling",
                    Content = "In the idyllic small town of Pagford, a councillor dies and leaves a \"casual vacancy\" - an " +
                    "empty seat on the Parish Council.\r\n\r\nIn the election for his successor that follows, it is clear that " +
                    "behind the pretty surface this is a town at war. Rich at war with poor, wives at war with husbands, teachers at " +
                    "war with pupils... Pagford is not what it first seems.",
                    Cover = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1509893913i/13497818.jpg",
                    Genre = "Adult",
                    Title = "The Casual Vacancy"
                },
                new Book() {
                    Id = 3,
                    Author = "J.K. Rowling",
                    Content = "\"The Tales of Beedle the Bard\" contains five richly diverse fairy tales," +
                    " each with its own magical character, that will variously bring delight, laughter and the" +
                    " thrill of mortal peril.\r\nAdditional notes for each story penned by Professor Albus" +
                    " Dumbledore will be enjoyed by Muggles and wizards alike, as the Professor muses on the morals" +
                    " illuminated by the tales, and reveals snippets of information about life at Hogwarts.",
                    Cover = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1630876355i/3950967.jpg",
                    Genre = "Fantasy",
                    Title = "The Tales of Beedle the Bard"
                },
                new Book() {
                    Id = 4,
                    Author = "Jane Austen",
                    Content = "Since its immediate success in 1813, Pride and Prejudice has remained one of the most " +
                    "popular novels in the English language. Jane Austen called this brilliant work \"her own darling " +
                    "child\" and its vivacious heroine, Elizabeth Bennet, \"as delightful a creature as ever appeared in" +
                    " print.\" The romantic clash between the opinionated Elizabeth and her proud beau, Mr. Darcy, is a" +
                    " splendid performance of civilized sparring. And Jane Austen's radiant wit sparkles as her characters " +
                    "dance a delicate quadrille of flirtation and intrigue, making this book the most superb comedy of manners " +
                    "of Regency England.",
                    Cover = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1320399351i/1885.jpg",
                    Genre = "Romance",
                    Title = "Pride and Prejudice"
                },
                new Book() {
                    Id = 5,
                    Author = "Jane Austen",
                    Content = "'The more I know of the world, the more am I convinced that I shall never see a man whom" +
                    " I can really love. I require so much!'\r\n\r\nMarianne Dashwood wears her heart on her sleeve, and" +
                    " when she falls in love with the dashing but unsuitable John Willoughby she ignores her sister Elinor's " +
                    "warning that her impulsive behaviour leaves her open to gossip and innuendo. Meanwhile Elinor, always" +
                    " sensitive to social convention, is struggling to conceal her own romantic disappointment, even from" +
                    " those closest to her. Through their parallel experience of love—and its threatened loss—the sisters " +
                    "learn that sense must mix with sensibility if they are to find personal happiness in a society where" +
                    " status and money govern the rules of love.\r\n\r\nThis edition includes explanatory notes, textual " +
                    "variants between the first and second editions, and Tony Tanner's introduction to the original Penguin" +
                    " Classic edition.",
                    Cover = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1397245675i/14935.jpg",
                    Genre = "Classics",
                    Title = "Sense and Sensibility"
                },
                new Book() {
                    Id = 6,
                    Author = "Jane Austen",
                    Content = "Emma Woodhouse is one of Austen's most captivating and vivid characters. Beautiful," +
                    " spoilt, vain and irrepressibly witty, Emma organizes the lives of the inhabitants of her sleepy" +
                    " little village and plays matchmaker with devastating effect.",
                    Cover = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1373627931i/6969.jpg",
                    Genre = "Classics",
                    Title = "Emma"
                },
                new Book() {
                    Id = 7,
                    Author = "John Green",
                    Content = "Despite the tumor-shrinking medical miracle that has bought her a few years, Hazel " +
                    "has never been anything but terminal, her final chapter inscribed upon diagnosis. But when a gorgeous" +
                    " plot twist named Augustus Waters suddenly appears at Cancer Kid Support Group, Hazel's story is about " +
                    "to be completely rewritten.",
                    Cover = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1660273739i/11870085.jpg",
                    Genre = "Romance",
                    Title = "The Fault in Our Stars"
                },
                new Book() {
                    Id = 8,
                    Author = "John Green",
                    Content = "Before. Miles “Pudge” Halter is done with his safe life at home. His" +
                    " whole life has been one big non-event, and his obsession with famous last" +
                    " words has only made him crave “the Great Perhaps” even more (Francois Rabelais," +
                    " poet). He heads off to the sometimes crazy and anything-but-boring world of Culver" +
                    " Creek Boarding School, and his life becomes the opposite of safe. Because down the" +
                    " hall is Alaska Young. The gorgeous, clever, funny, sexy, self-destructive, screwed up," +
                    " and utterly fascinating Alaska Young. She is an event unto herself. She pulls Pudge into " +
                    "her world, launches him into the Great Perhaps, and steals his heart. Then. . . .",
                    Cover = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1652042180i/99561.jpg",
                    Genre = "Romance",
                    Title = "Looking for Alaska"
                },
                new Book() {
                    Id = 9,
                    Author = "John Green",
                    Content = "Quentin Jacobsen has spent a lifetime loving the magnificently adventurous Margo " +
                    "Roth Spiegelman from afar. So when she cracks open a window and climbs into his life—dressed" +
                    " like a ninja and summoning him for an ingenious campaign of revenge—he follows. After their" +
                    " all-nighter ends, and a new day breaks, Q arrives at school to discover that Margo, always an" +
                    " enigma, has now become a mystery. But Q soon learns that there are clues—and they're for him. " +
                    "Urged down a disconnected path, the closer he gets, the less Q sees the girl he thought he knew...",
                    Cover = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1349013610i/6442769.jpg",
                    Genre = "Adult",
                    Title = "Paper Towns"
                }
            };
            var ratings = new List<Rating>();
            var reviews = new List<Review>();

            Random random = new Random();
            int idCounter = 1;

            for (int i = 1; i <= 9; i++) {
                for (int j = 1; j < random.Next(8, 26); j++) { // Create from 8 to 25 reviews and rates for each book
                    //Create Rating
                    ratings.Add(new Rating() {
                        Id = idCounter,
                        Score = random.Next(1, 6),
                        BookId = i
                    });
                    // Create Review
                    reviews.Add(new Review() {
                        Id = idCounter,
                        BookId = i,
                        Message = $"That's default review-{j} for book-{i}",
                        Reviewer = $"Reviewer-{j}"
                    });
                    idCounter++;
                }
            }

            context.Books.AddRangeAsync(books);
            context.Ratings.AddRangeAsync(ratings);
            context.Reviews.AddRangeAsync(reviews);
            //
            context.SaveChangesAsync();
        }
    }
}
