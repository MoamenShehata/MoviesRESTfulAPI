using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoviesRESTfulAPI.DTOs.Actor;
using MoviesRESTfulAPI.DTOs.Movie;
using MoviesRESTfulAPI.Models;

namespace MoviesRESTfulAPI.Factory
{
    public static class Mapper
    {
        public static GetActorDTO ActorDtoFromActor(Actor actor)
        {
            return new GetActorDTO()
            {
                Id = actor.Id,
                Name = $"{actor.FirstName} {actor.LastName}",
                Age = actor.Age,
                DateOfBirth = DateTime.Now.Year - actor.Age,
                MovieId = actor.Movie.Id,
                Movie = actor.Movie.Name
            };
        }

        public static Actor ActorFromPostActorDto(PostActorDTO actorDTO)
        {
            return new Actor()
            {
                FirstName = actorDTO.FirstName,
                LastName = actorDTO.LastName,
                Age = actorDTO.Age
            };
        }

        public static Actor ActorFromPostActorDto(Actor source, PostActorDTO dest)
        {
            source.FirstName = dest.FirstName;
            source.LastName = dest.LastName;
            source.Age = dest.Age;
            return source;
        }

        public static IEnumerable<GetActorDTO> ActorsDtosFromActors(IEnumerable<Actor> actors)
        {
            if (actors != null)
            {
                foreach (var actor in actors)
                {
                    yield return ActorDtoFromActor(actor);
                }
            }

        }

        public static IEnumerable<Actor> ActorsFromPostActorsDtos(IEnumerable<PostActorDTO> actorDtos)
        {
            if (actorDtos != null)
            {
                foreach (var actorDto in actorDtos)
                {
                    yield return ActorFromPostActorDto(actorDto);
                }
            }
        }

        public static GetMovieDTO MovieDtoFromMovie(Movie movie)
        {
            return new GetMovieDTO()
            {
                Id = movie.Id,
                Name = movie.Name,
                Rate = movie.Rate,
                Genre = movie.Genre,
                Length = movie.Length.ToString(@"hh\:mm\:ss"),
                Actors = ActorsDtosFromActors(movie.Actors).ToList()
            };
        }

        public static Movie MovieFromPostMovieDto(PostMovieDTO movieDto)
        {
            return new Movie()
            {
                Name = movieDto.Name,
                Rate = movieDto.Rate,
                Genre = movieDto.Genre,
                Length = movieDto.Length,
                Actors = ActorsFromPostActorsDtos(movieDto.Actors).ToList()
            };
        }

        public static IEnumerable<GetMovieDTO> MoviesDtosFromMovies(IEnumerable<Movie> movies)
        {
            if (movies != null)
            {
                foreach (var movie in movies)
                {
                    yield return MovieDtoFromMovie(movie);
                }
            }

        }

        public static IEnumerable<Movie> MoviesFromMoviesDtos(IEnumerable<PostMovieDTO> moviesDtos)
        {
            if (moviesDtos != null)
            {
                foreach (var moviesDto in moviesDtos)
                {
                    yield return MovieFromPostMovieDto(moviesDto);
                }
            }

        }

    }
}