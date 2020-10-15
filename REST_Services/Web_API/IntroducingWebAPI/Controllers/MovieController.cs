using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IntroducingWebAPI.Models;

namespace IntroducingWebAPI.Controllers
{
    // GET all movies
    public class MovieController : ApiController
    {
        [Route("movies/all")]
        [AcceptVerbs("GET")]
        public IHttpActionResult All()
        {
            return Ok(MovieRepository.GetAll());
        }

        // GET one movie by ID
        [Route("movies/get/{movieId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Get(int movieId)
        {
            Movie movie = MovieRepository.Get(movieId);

            if (movie == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(movie);
            }
        }

        // POST (add) a movie
        [Route("movies/add")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Add(AddMovieRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Movie movie = new Movie()
            {
                Title = request.Title,
                Rating = request.Rating
            };

            MovieRepository.Add(movie);
            return Created($"movies/get/{movie.MovieId}", movie);
        }

        // PUT (update) a movie
        [Route("movies/update")]
        [AcceptVerbs("PUT")]
        public IHttpActionResult Update(UpdateMovieRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Movie movie = MovieRepository.Get(request.MovieId);

            if(movie == null)
            {
                return NotFound();
            }

            movie.Title = request.Title;
            movie.Rating = request.Rating;

            MovieRepository.Edit(movie);
            return Ok(movie);
        }

        // DELETE a movie
        [Route("movies/delete")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult Delete(int movieId)
        {
            Movie movie = MovieRepository.Get(movieId);

            if (movie == null)
            {
                return NotFound();
            }

            MovieRepository.Delete(movieId);
            return Ok();
        }
    }
}