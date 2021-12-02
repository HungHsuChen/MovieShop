﻿using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CastRepository : Repository<Cast>, ICastRepository
    {
        public IEnumerable<Cast> GetCasts()
        {
            var casts = new List<Cast>
            {
                new Cast{
                    Id= 1,
                    Name= "Leonardo DiCaprio",
                    ProfilePath= "https://image.tmdb.org/t/p/w342//wo2hJpn04vbtmh0B9utCFdsQhxM.jpg",
                    Character="Dom Cobb"},
                new Cast{
                    Id= 2,
                    Name= "Ken Watanabe",
                    ProfilePath= "https://image.tmdb.org/t/p/w342//psAXOYp9SBOXvg6AXzARDedNQ9P.jpg",
                    Character="Mr. Saito"},
                new Cast{
                    Id= 3,
                    Name= "Joseph Gordon-Levitt",
                    ProfilePath= "https://image.tmdb.org/t/p/w342//4U9G4YwTlIEbAymBaseltS38eH4.jpg",
                    Character="Arthur"},
                new Cast{
                    Id= 4,
                    Name= "Marion Cotillard",
                    ProfilePath= "https://image.tmdb.org/t/p/w342//zChwjQ9D90fxx6cgWz5mUWHNd5b.jpg",
                    Character="Mal Cobb"},
                new Cast{
                    Id= 5,
                    Name= "Elliot Page",
                    ProfilePath= "https://image.tmdb.org/t/p/w342//tp157uXeMPA5G91cfecBWO2OFzn.jpg",
                    Character="Ariadne"},
                new Cast{
                    Id= 6,
                    Name= "Tom Hardy",
                    ProfilePath= "https://image.tmdb.org/t/p/w342//yVGF9FvDxTDPhGimTbZNfghpllA.jpg",
                    Character="Zak Eames"}
            };
            return casts;
        }
    }
}
