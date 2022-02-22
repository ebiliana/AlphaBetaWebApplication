using AlphaBetaWebApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace AlphaBetaWebApplication.ViewModels
{
    public class UserLogin:User
    {
 
        [StringLength(60, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
