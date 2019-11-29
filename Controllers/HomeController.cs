using EnglishGram.Models;
using EnglishGram.Utils;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EnglishGram.Controllers
{
    /**
     * Except those actions which have [AllowAnonymous] attribute,
     * by default visitors can't access this controller if they aren't authorized user.
     */
    [Authorize]
    public class HomeController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            var photo = GetRandomPhoto() as Photo;

            if (!ctx.Photos.Any())
            {
                var res = GetInstagramThings();
                var shortcodes = JsonConvert.DeserializeObject<PhotoIns>(res.Content);
                foreach (var shortcode in shortcodes.graphql.user.edge_owner_to_timeline_media.edges)
                {
                    var resPhoto = GetInstagramThings(shortcode.node.shortcode);
                    var photos = JsonConvert.DeserializeObject<PhotoIns>(resPhoto.Content);
                    var photoUrl = photos.graphql.shortcode_media.edge_sidecar_to_children.edges[0].node.display_url;
                    var subPhotoUrl = photos.graphql.shortcode_media.edge_sidecar_to_children.edges[1].node.display_url;
                    ctx.Photos.Add(new Photo
                    {
                        PhotoUrl = photoUrl,
                        SubPhotoUrl = subPhotoUrl,
                        CreatedAt = DateTime.Now
                    });
                }

                ctx.SaveChanges();
            }

            return View(photo);
        }


        public IRestResponse GetInstagramThings(string shortcode = "")
        {
            var client = shortcode == "" ? new RestClient("https://www.instagram.com/ingilizcebilgibankasi/?__a=1") : new RestClient($"https://www.instagram.com/p/{shortcode}?__a=1");
            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);
            return response;
        }

        [AllowAnonymous]
        public dynamic GetRandomPhoto(bool isJson = false)
        {
            Random rnd = new Random();

            /**
             * Authorized user processes
             */
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    var id = User.Identity.GetUserId();
                    var user = ctx.Users.Include(x => x.Photos).FirstOrDefault(x => x.Id == id);
                    var photos = ctx.Photos.ToList().Except(user.Photos).ToList();
                    /**
                     * Alternative way to include related photos to user.
                     */
                    // var usedPhotoIds = user.Photos.Select(x => x.Id).ToList();
                    // var photos = ctx.Photos.Where(x => !usedPhotoIds.Contains(x.Id)).ToList();
                    if (photos.Count == 0) return null;

                    var randomPhoto = photos[rnd.Next(photos.Count)];

                    if (isJson)
                    {
                        return Json(new
                        {
                            randomPhoto.Id,
                            randomPhoto.PhotoUrl,
                            randomPhoto.Description,
                            randomPhoto.SubPhotoUrl,
                            randomPhoto.SubDescription,
                            IsFinished = photos.Count == 1 // if it's final photo to show then return true
                        });
                    }
                    return randomPhoto;

                }
                catch (Exception)
                {
                    throw;
                }
            }

            /**
             * Unauthorized user processes
             */
            var photo = ctx.Photos.Find(rnd.Next(ctx.Photos.Count()));
            if (isJson)
            {
                return Json(new
                {
                    photo.Id,
                    photo.PhotoUrl,
                    photo.Description,
                    photo.SubPhotoUrl,
                    photo.SubDescription
                });
            }
            return photo;
        }

        /**
         * Add photo to user photos
         */
        [HttpPost]
        public async Task<JsonResult> Learned(int photoId)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var user = ctx.Users.Include(x => x.Photos).FirstOrDefault(x => x.Id == userId); // including related photos to users.
                var photo = ctx.Photos.Find(photoId);
                if (user.Photos.Contains(photo)) // if user photo does exist than remove otherwise add.
                {
                    user.Photos.Remove(photo);
                    await ctx.SaveChangesAsync();
                    return Json(new { success = false });
                }
                user.Photos.Add(photo);
                await ctx.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /**
         * Delete all learned photos
         */
        [HttpPost]
        public async Task<ActionResult> ResetStatus()
        {
            var id = User.Identity.GetUserId();
            var user = ctx.Users.Include(x => x.Photos).FirstOrDefault(x => x.Id == id);
            user.Photos.Clear();
            await ctx.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}