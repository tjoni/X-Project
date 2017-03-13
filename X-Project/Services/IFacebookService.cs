using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using X_Project.Models;

namespace X_Project.Services
{
    public interface IFacebookService
    {
        Task<List<FacebookFriendsModel>> GetFriends(string accesToken, string accesTokenProof);
    }
}