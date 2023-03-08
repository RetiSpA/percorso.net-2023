using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Esercizio4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        [Authorize]
        [HttpGet("getbooks")]
        public IEnumerable<Book> Get()
        {
            int userAge = 0;
            var currentUser = HttpContext.User;
            var resultBookList = new List<Book> {
                new Book { Author = "Ray Bradbury",Title = "Fahrenheit 451" },
                new Book { Author = "Gabriel García Márquez", Title = "One Hundred years of Solitude" },
                new Book { Author = "George Orwell", Title = "1984" },
                new Book { Author = "Anais Nin", Title = "Delta of Venus" }
              };

            if (currentUser.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            {
                DateTime birthDate = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.DateOfBirth)!.Value);
                userAge = DateTime.Today.Year - birthDate.Year;
                if (userAge >= 21)
                {
                    resultBookList.Add(new Book { Author = "Judith Levine", Title = "Harmful to Minors", AgeRestriction = true });
                }
            }

            return resultBookList;
        }

        public class Book
        {
            public string Author { get; set; }
            public string Title { get; set; }
            public bool AgeRestriction { get; set; }
        }
    }
}