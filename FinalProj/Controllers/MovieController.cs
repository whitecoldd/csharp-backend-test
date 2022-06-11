using Dapper;
using FinalProj.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalProj.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View(ORM.ReturnList<MovieModel>("MovieViewAll"));
        }


        [HttpGet]
        public IActionResult AddOrUpdate(int id = 0)
        {
            if (id == 0)
                return View();
            else {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@MovieID", id);
                return View(ORM.ReturnList<MovieModel>("MovieViewByID", parameter).FirstOrDefault<MovieModel>());

            }
        }


        [HttpPost]
        public IActionResult AddOrUpdate(MovieModel mov)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@MovieID",mov.MovieID);
            parameter.Add("@MovieName", mov.MovieName);
            parameter.Add("@MovieGenre", mov.MovieGenre);
            parameter.Add("@MovieDate", mov.MovieDate);
            ORM.NoReturn("MovieAddOrUpdate", parameter);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@MovieID", id);
            ORM.NoReturn("MovieDeleteByID", parameter);
            return RedirectToAction("Index");
        }
    }
}
