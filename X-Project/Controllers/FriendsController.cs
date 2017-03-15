using Facebook;
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
using X_Project.Repository;
using X_Project.Services;

namespace X_Project.Controllers
{
    [RoutePrefix("friends")]
    [Authorize]
    [FacebookAccessToken]
    public class FriendsController : Controller
    {
        //private WishListContext _context;
        private IFacebookService _facebookService;
        private IWishRepository _iRepository;

        public FriendsController(IFacebookService facebookService,/* WishListContext context,*/ IWishRepository Irepository)
        {
            _iRepository = Irepository;
            //_context = context;
            _facebookService = facebookService;
        }

        // GET: Friends
        public async Task<ActionResult> Index()
        {
            var access_token = HttpContext.Items["access_token"].ToString();
            if (access_token != null)
            {
                try
                {
                    var appsecret_proof = access_token.GenerateAppSecretProof();
                    
                    var friendsList = await _facebookService.GetFriends(access_token, appsecret_proof);

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
        
        public async Task<ActionResult> UserProfile(string userId)
        {
            var wishlists = await _iRepository.GetWishlistById(userId);

            return View(wishlists);
        }
    }
}