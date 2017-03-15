using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_Project.ModelData;

namespace X_Project.Repository
{
    public interface IWishRepository
    {
        Task<List<WishListItem>> GetWishlistById(string userId);
    }
}
