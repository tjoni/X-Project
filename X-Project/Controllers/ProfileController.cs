using Facebook;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var getTitle = Request["Title"];
            var getImage = Request["ImageUrl"];
            string GetuserId = User.Identity.GetUserId();

            var context = new WishListContext();

            context.WishListItems.Add(new WishListItem
            {
                Id = Guid.NewGuid().ToString(),
                UserId = GetuserId,
                Title = getTitle,
                Image = getImage,
                Time = DateTime.Now
            });
            context.SaveChanges();
            return RedirectToAction("Index", "Profile");
        }
    }
}