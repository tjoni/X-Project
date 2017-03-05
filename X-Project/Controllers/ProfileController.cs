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
                    var eee = profileImgResult.data.url;

                    var facebookProfile = new FacebookProfileModel()
                    {
                        FirstName = myInfo.first_name,
                        LastName = myInfo.last_name,
                        ImageURL = eee,
                        LinkURL = myInfo.link,
                        Fullname = myInfo.name,
                    };
                    return View(facebookProfile);

                }
                catch (Exception)
                {

                    throw;
                }
            }
            return View();
        }
    }
}