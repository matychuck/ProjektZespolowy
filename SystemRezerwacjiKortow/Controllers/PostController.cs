using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemRezerwacjiKortow.Database;
using SystemRezerwacjiKortow.Models;

namespace SystemRezerwacjiKortow.Controllers
{
    public class PostController : Controller
    {
        List<Post> list = SqlPost.GetPosts();
        // GET: Post
        public ActionResult Index()
        {
            return View(list);
        }

        public ActionResult PostsForUsers()
        {
            return View(list);
        }

        // GET: Post/Details/5
        public ActionResult Details(int id)
        {
            Post post = SqlPost.GetPost(id);
            return View(post);
        }

        // GET: Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "TitlePL, TitleEN, TitleDE, DescriptionPL, DescriptionEN, DescriptionDE")] Post post)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SqlPost.InsertPost(post);                 
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(post);
            }
        }

        // GET: Post/Edit/5
        public ActionResult Edit(int id)
        {
            Post post = SqlPost.GetPost(id);           
            return View(post);
        }

        // POST: Post/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, [Bind(Include = "TitlePL, TitleEN, TitleDE, DescriptionPL, DescriptionEN, DescriptionDE")] Post post)
        {
            try
            {
                SqlPost.UpdatePost(id,post); //???? czemu id i post, a nie samo post?
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View(post);
            }
        }

        // GET: Post/Delete/5
        public ActionResult Delete(int id)
        {
            Post post = SqlPost.GetPost(id);
            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
               // Post deletedPost = new Post();
               // deletedPost = SqlPost.GetPost(id);
                SqlPost.DeletePost(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}