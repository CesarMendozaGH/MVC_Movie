using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie_IDGS904.Data;
using MvcMovie_IDGS904.Models;

namespace MvcMovie_IDGS904.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MvcMovie_IDGS904Context _context;

        public MoviesController(MvcMovie_IDGS904Context context)
        {
            _context = context;
        }

        // GET: Movies
        //MODIFICAMOS ESTO PARA BUSCAR
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            if(_context.Movie == null)
            {
                return Problem("Entity set is null.");
            }

            //Consulta en LINQ para obtener los generos
            IQueryable<string> genreQuery = from m in _context.Movie 
                orderby m.Genre //ordenamos por genero
                select m.Genre; //seleccionamos el genero

            //Consulta en LINQ para seleccionar las peliculas
            var movies = from m in _context.Movie
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {   //buscamos las peliculas mediante la cadena de busqueda
                movies =movies.Where(s => s.Title!.ToUpper().Contains(searchString.ToUpper())); //el signo de admiracion es para que no marque error por el null

            }

            if (!String.IsNullOrEmpty(movieGenre)) { 
                
                movies = movies.Where(x => x.Genre == 
                movieGenre); //buscamos las peliculas por genero
            }

            var movieGenreVM = new MovieGenreViewModel
            //buscamos los generos de las peliculas y los convertimos a una lista
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()), 
                Movies = await movies.ToListAsync(), //convertimos la consulta a una lista
                SearchString = searchString,
                MovieGenre = movieGenre
            };

            // return View(await _context.Movie.ToListAsync()); return 
            return View(await movies.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,ReleaseDate,Genre,Price")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,ReleaseDate,Genre,Price")] Movie movie)
        {
            if (id != movie.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.ID == id);
        }
    }
}
