using ArtistsMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ArtistsMVC.Repositories
{
    public class AlbumRepository
    {
        private readonly ApplicationDbContext _context;

        public AlbumRepository()
        {
                            _context = new ApplicationDbContext();
        }

        public IEnumerable<Album> GetAll()
        {
            return _context.Albums;
        }

        public IEnumerable<Album> GetAllWithArtists()
        {
            return _context.Albums
                .Include(a => a.Artist);
        }

    }
}