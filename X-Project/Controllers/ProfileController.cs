using Facebook;
using HtmlAgilityPack;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using X_Project.Attribute;
using X_Project.Extension;
using X_Project.ModelData;
using X_Project.Models;

namespace X_Project.Controllers
{
    [Authorize]
    [FacebookAccessTokenAttribute]
    public class ProfileController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var access_token = HttpContext.Items["access_token"].ToString();
            if (access_token != null)
            {
                try
                {
                    var appsecret_proof = access_token.GenerateAppSecretProof();

                    var fb = new FacebookClient(access_token);
                    //Get current user profile
                    dynamic myInfo =
                        await
                            fb.GetTaskAsync(
                                "me?fields=first_name,last_name,email,name,link"
                                    .GraphAPICall(appsecret_proof));
                    

                    //get current picture
                    dynamic profileImgResult =
                        await
                            fb.GetTaskAsync(
                                "{0}/picture?width=200&height=250&redirect=false".GraphAPICall((string)myInfo.id,
                                    appsecret_proof));
                    var picture = profileImgResult.data.url;
                    
                    var facebookProfile = new FacebookProfileModel()
                    {
                        FirstName = myInfo.first_name,
                        LastName = myInfo.last_name,
                        ImageURL = picture,
                        LinkURL = myInfo.link,
                        Fullname = myInfo.name,
                    };
                    ViewModel _viewModel = new ViewModel();
                    WishListContext context = new WishListContext();
                    string userId = User.Identity.GetUserId();
                    string baseURL = Request.Url.ToString();
                    ViewBag.WishlistUrl = baseURL + "Friends/UserProfile?userId=" + userId;
                    _viewModel._FacebookProfileModel = facebookProfile;
                    _viewModel._WishListItem = context.WishListItems.Where(x => x.UserId == userId).ToList();
                    return View(_viewModel);

                }
                catch (Exception)
                {

                    throw;
                }
            }
            return View();
        }
        public ActionResult AddWishList(string url)
        {
            //var getTitle = Request["Title"];
            //var getImage = Request["ImageUrl"];

            //var getUrl = Request["urlString"];
            string GetuserId = User.Identity.GetUserId();
            
            var metaInformation = GetMetaDataFromUrl(url);

            if(metaInformation == null || string.IsNullOrEmpty(metaInformation.Title) || string.IsNullOrEmpty(metaInformation.ImageUrl))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad url");
            }

            var context = new WishListContext();

            context.WishListItems.Add(new WishListItem
            {
                Id = Guid.NewGuid().ToString(),
                UserId = GetuserId,
                Title = HttpUtility.HtmlDecode(metaInformation.Title),
                Image = metaInformation.ImageUrl,
                WishUrl = url,
                Time = DateTime.Now
            });
            context.SaveChanges();

            ViewModel _viewModel = new ViewModel();
            string userId = User.Identity.GetUserId();
            _viewModel._WishListItem = context.WishListItems.Where(x => x.UserId == userId).ToList();
            return PartialView("_wishlist", _viewModel);

        }
        public ActionResult testajax()
        {
            ViewModel _viewModel = new ViewModel();
            WishListContext context = new WishListContext();
            string userId = User.Identity.GetUserId();

            _viewModel._WishListItem = context.WishListItems.Where(x => x.UserId == userId).ToList();
            return PartialView("_wishlist", _viewModel);
        }
        public static MetaInformation GetMetaDataFromUrl(string url)
        {
            // Get the URL specified
            var webGet = new HtmlWeb();
            try
            {
                var document = webGet.Load(url);
                var metaTags = document.DocumentNode.SelectNodes("//meta");
                MetaInformation metaInfo = new MetaInformation(url);
                if (metaTags != null)
                {
                    int matchCount = 0;
                    foreach (var tag in metaTags)
                    {
                        var tagName = tag.Attributes["name"];
                        var tagContent = tag.Attributes["content"];
                        var tagProperty = tag.Attributes["property"];
                        if (tagName != null && tagContent != null)
                        {
                            switch (tagName.Value.ToLower())
                            {
                                case "title":
                                    metaInfo.Title = tagContent.Value;
                                    matchCount++;
                                    break;
                                case "description":
                                    metaInfo.Description = tagContent.Value;
                                    matchCount++;
                                    break;
                                case "keywords":
                                    metaInfo.Keywords = tagContent.Value;
                                    matchCount++;
                                    break;
                            }
                        }
                        else if (tagProperty != null && tagContent != null)
                        {
                            switch (tagProperty.Value.ToLower())
                            {
                                case "og:title":
                                    metaInfo.Title = string.IsNullOrEmpty(metaInfo.Title) ? tagContent.Value : metaInfo.Title;
                                    matchCount++;
                                    break;
                                case "og:description":
                                    metaInfo.Description = string.IsNullOrEmpty(metaInfo.Description) ? tagContent.Value : metaInfo.Description;
                                    matchCount++;
                                    break;
                                case "og:image":
                                    metaInfo.ImageUrl = string.IsNullOrEmpty(metaInfo.ImageUrl) ? tagContent.Value : metaInfo.ImageUrl;
                                    matchCount++;
                                    break;
                            }
                        }
                    }
                    metaInfo.HasData = matchCount > 0;
                }
                return metaInfo;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public ActionResult DeleteWish(string id)
        {
            ViewModel _viewModel = new ViewModel();
            WishListContext context = new WishListContext();
            string userId = User.Identity.GetUserId();

            var wishItem = context.WishListItems.Find(id);
            context.WishListItems.Remove(wishItem);
            context.SaveChanges();

            _viewModel._WishListItem = context.WishListItems.Where(x => x.UserId == userId).ToList();

            return PartialView("_wishlist", _viewModel);
        }

    }
    public class MetaInformation
    {
        public bool HasData { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string ImageUrl { get; set; }

        public MetaInformation(string url)
        {
            Url = url;
            HasData = false;
        }

        //public MetaInformation(string url, string title, string description, string keywords, string imageUrl, string siteName)
        //{
        //    Url = url;
        //    Title = HttpUtility.HtmlDecode(title);
        //    Description = description;
        //    Keywords = keywords;
        //    ImageUrl = imageUrl;
        //    SiteName = siteName;
        //}
    }

}