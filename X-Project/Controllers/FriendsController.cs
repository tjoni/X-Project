using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using X_Project.Attribute;
using X_Project.Extension;
using X_Project.Models;

namespace X_Project.Controllers
{
    [Authorize]
    [FacebookAccessToken]
    public class FriendsController : Controller
    {
        // GET: Friends
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
                    dynamic friends =
                        await
                            fb.GetTaskAsync(
                                "me/friends?fields=name,id,picture.width(300).height(250)"
                                    .GraphAPICall(appsecret_proof));

                    var friendsList = new List<FacebookFriendsModel>();
                    foreach (dynamic friend in friends.data)
                    {
                        friendsList.Add(new FacebookFriendsModel
                        {
                            Id = friend.id,
                            Name = friend.name,
                            ImageUrl = friend.picture.data.url
                        });
                    }
                    ViewBag.friends = friendsList;
                    return View(friendsList);
                }
                catch (Exception)
                {

                    throw;
                }

            }
            else
            {
                throw new HttpException(404, "Missing Access Token");
            }

        }
    }
}