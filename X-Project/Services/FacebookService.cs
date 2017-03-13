using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using X_Project.Extension;
using X_Project.ModelData;
using X_Project.Models;

namespace X_Project.Services
{
    public class FacebookService : IFacebookService
    {
        private WishListContext _Wishcontext;

        public FacebookService(WishListContext Wishcontext)
        {
            _Wishcontext = Wishcontext;
        }

        public async Task<List<FacebookFriendsModel>> GetFriends(string accesToken, string accesTokenProof)
        {
            var fb = new FacebookClient(accesToken);
            //Get current user profile
            dynamic friends =
                await
                    fb.GetTaskAsync(
                        "me/friends?fields=name,id,picture.width(300).height(250)"
                            .GraphAPICall(accesTokenProof));

            var context = new ApplicationDbContext();

            var friendsList = new List<FacebookFriendsModel>();
            foreach (dynamic friend in friends.data)
            {
                var facebookId = (string)friend.id;
                var userId = context.Users.FirstOrDefault(x => x.FacebookId == facebookId)?.Id;

                friendsList.Add(new FacebookFriendsModel
                {
                    Id = friend.id,
                    Name = friend.name,
                    ImageUrl = friend.picture.data.url,
                    UserId = userId,
                    WishListCount = _Wishcontext.WishListItems.Count(x => x.UserId == userId)
                });
            }

            return friendsList;
        }
    }
}