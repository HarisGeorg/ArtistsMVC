using ArtistsMVC.Dtos;
using ArtistsMVC.Models;
using ArtistsMVC.Repositories;
using AutoMapper;
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

        public IHttpActionResult GetAlbums()
        {
            //var albums = _albumRepository.GetAll();
            //var albumDtos = new List<AlbumDto>();

            //foreach (var album in albums)
            //{
            //    var albumDto = new AlbumDto
            //    {
            //        ID = album.ID,
            //        Title = album.Title,
            //        Description = album.Description,
            //        ArtistId = album.ArtistId
            //    };

            //    albumDtos.Add(albumDto);
            //}

            //return albumDtos;

            return Ok(_albumRepository.GetAll()        //isodinamo me ola ta apo pano
                .Select(Mapper.Map<Album, AlbumDto>));
        }

        public IHttpActionResult GetAlbum(int id)
        {
            var album = _albumRepository.GetById(id);

            if (album == null)
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();

            //var albumDto = new AlbumDto
            //{
            //    ID = album.ID,
            //    Title = album.Title,
            //    Description = album.Description,
            //    ArtistId = album.ArtistId
            //};

                //return albumDto;

            return Ok(Mapper.Map<Album, AlbumDto>(album));
        }

        [HttpPost]
        public IHttpActionResult CreatedAlbum(AlbumDto albumDto)
        {
            if (!ModelState.IsValid)     //paei sto Model/Album kai vlepei an isxioun oi periorismoi ton Annotations
                return BadRequest();

            //var album = new Album()
            //{
            //    Title = albumDto.Title,
            //    Description = albumDto.Description,
            //    ArtistId = albumDto.ArtistId
            //};
            
            var album = Mapper.Map<AlbumDto, Album>(albumDto);

            _albumRepository.Create(album);
            albumDto.ID = album.ID;
            //return albumDto;

            return Created(new Uri(Request.RequestUri + "/" + album.ID), albumDto); //Uri: pairnei to URL tis current action. Mesa tou vazo ena string pou to kano concatenate me to album ID
        }                                                                           //afto tha girisei piso to last inserted object -> album

        [HttpPut]
        public IHttpActionResult Update(int id, AlbumDto albumDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var albumInDb = _albumRepository.GetById(id);

            if (albumInDb == null)
                return NotFound();

            //_albumRepository.Update(album);   //Throws Error
            //albumInDb.Title = albumDto.Title;
            //albumInDb.Description = albumDto.Description;
            //albumInDb.ArtistId = albumDto.ArtistId;

            Mapper.Map(albumDto, albumInDb);    //Edo tha prepei na perasoume to id apo to body
            _albumRepository.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var albumInDb = _albumRepository.GetById(id);

            if (albumInDb == null)
                return NotFound();

            _albumRepository.Delete(id);

            return Ok(albumInDb);
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
