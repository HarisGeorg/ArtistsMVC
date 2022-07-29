﻿using ArtistsMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ArtistsMVC.ViewModels;

namespace ArtistsMVC.Controllers
{
    public class SongsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SongsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Songs
        public ActionResult Index()
        {
            var songs = _context
                .Songs
                .Include(s => s.Album.Artist)
                .ToList();

            //var songs = _context
            //    .Songs
            //    .Where(s => s.Album.Artist.ID == 1)
            //    .Include(s => s.Album.Artist)
            //    .ToList();

            return View(songs);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                //return RedirectToAction("Index");
            }

            var song = _context
                .Songs
                .Include(s => s.Album.Artist)
                .SingleOrDefault(s => s.ID == id);

            if (song == null)
            {
                return HttpNotFound();
            }

            return View(song);
        }

        public ActionResult New()
        {
            // Get albums from the database
            var albums = _context.Albums.ToList();

            // Init and fill the viewmodel
            var viewmodel = new SongFormViewModel()
            {
                Song = new Song(),
                Albums = albums
            };

            // Return the appropriate view with the viewmodel
            return View("SongForm", viewmodel);
        }


        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}