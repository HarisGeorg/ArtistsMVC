﻿using ArtistsMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ArtistsMVC.Repositories
{
    public class AlbumRepository : IDisposable
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

        public Album GetById(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return _context.Albums.SingleOrDefault(a => a.ID == id);
        }

        public Album GetByIdWithArtist(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return _context.Albums
                .Include(a => a.Artist)
                .SingleOrDefault(a => a.ID == id);
        }

        public void Create(Album album)
        {
            _context.Albums.Add(album);
            _context.SaveChanges();
        }

        public void Update(Album album)
        {
            _context.Entry(album).State = EntityState.Modified;         //einai sintomografia ton apopano entolon
            _context.SaveChanges();
        }

        public void Delete(int? id)
        {
            Album album = GetById(id);
            _context.Albums.Remove(album);
            _context.SaveChanges();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}