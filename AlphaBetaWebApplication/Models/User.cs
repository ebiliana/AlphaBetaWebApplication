using System.ComponentModel.DataAnnotations;

namespace AlphaBetaWebApplication.Models
{
    public class User
    {
    
        public int userId { get; set; }

        public string userName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public String phoneNo { get; set; }

    }
}
