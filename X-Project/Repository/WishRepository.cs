using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using X_Project.ModelData;

namespace X_Project.Repository
{
    public class WishRepository : IWishRepository
    {
        private WishListContext _WishlistContext;

        public WishRepository(WishListContext Wishlistcontext)
        {
            _WishlistContext = Wishlistcontext;
        }
        public async Task<List<WishListItem>> GetWishlistById(string userId)
        {

            var wishlists = _WishlistContext.WishListItems.Where(x => x.UserId == userId).ToList();

            return wishlists;
        }
    }
}