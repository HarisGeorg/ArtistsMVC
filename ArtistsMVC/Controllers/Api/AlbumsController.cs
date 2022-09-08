using ArtistsMVC.Models;
using ArtistsMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArtistsMVC.Controllers.Api
{
    public class AlbumsController : ApiController
    {
        private readonly AlbumRepository _albumRepository;

        public AlbumsController()
        {
            _albumRepository = new AlbumRepository();
        }

        public IEnumerable<Album> GetAlbums()
        {
            return _albumRepository.GetAll();
        }

        protected override void Dispose(bool disposing) // override alla kratao kai tin arxiki ilopiisi
        {
            if (disposing)
            {
                _albumRepository.Dispose(); // otan kanei dispose o controller ta ginoun ola ta default + i to _albumRepository
            }
            base.Dispose(disposing);
        }
    }
    
}
