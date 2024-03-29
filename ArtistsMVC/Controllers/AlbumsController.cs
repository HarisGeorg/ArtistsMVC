﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArtistsMVC.Models;
using ArtistsMVC.Repositories;

namespace ArtistsMVC.Controllers
{
    public class AlbumsController : Controller
    {        
        private readonly AlbumRepository _albumRepository;
        private readonly ArtistsRepository _artistsRepository;

        public AlbumsController()
        {
            _albumRepository = new AlbumRepository();
            _artistsRepository = new ArtistsRepository();
        }

        // GET: Albums
        public ActionResult Index()
        {
            var albums = _albumRepository.GetAllWithArtists();
            return View(albums.ToList());
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            
            try
            {
                Album album = _albumRepository.GetByIdWithArtist(id);

                if (album == null)
                {
                    return HttpNotFound();
                }
                return View(album);
            }
            catch (Exception)  //ex
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);       
            }

            //Album album = _albumRepository.GetByIdWithArtist(id);

            
        }

        // GET: Albums/Create
        public ActionResult Create()
        {
            ViewBag.ArtistId = new SelectList(_artistsRepository.GetAll(), "ID", "FullName");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description,ArtistId")] Album album)
        {
            if (ModelState.IsValid)
            {
                //db.Albums.Add(album);
                //db.SaveChanges();
                _albumRepository.Create(album);
                return RedirectToAction("Index");
            }

            ViewBag.ArtistId = new SelectList(_artistsRepository.GetAll(), "ID", "FirstName", album.ArtistId);
            return View(album);
        }

        // GET: Albums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Album album = _albumRepository.GetById(id);

            if (album == null)
            {
                return HttpNotFound();
            }

            ViewBag.ArtistId = new SelectList(_artistsRepository.GetAll(), "ID", "FullName", album.ArtistId);
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,ArtistId")] Album album)
        {
            if (ModelState.IsValid)
            {
                //var albumInDb = db.Albums.SingleOrDefault(a => a.ID == album.ID);
                //albumInDb.Title = album.Title;
                //albumInDb.Description = album.Description;
                //albumInDb.ArtistId = album.ArtistId;
                //db.Entry(album).State = EntityState.Modified;         //einai sintomografia ton apopano entolon

                _albumRepository.Update(album);
                return RedirectToAction("Index");
            }
            ViewBag.ArtistId = new SelectList(_artistsRepository.GetAll(), "ID", "FullName", album.ArtistId);
            return View(album);
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Album album = _albumRepository.GetByIdWithArtist(id);

            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _albumRepository.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _artistsRepository.Dispose();
                _albumRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
