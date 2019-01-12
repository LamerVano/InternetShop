using System.ComponentModel.DataAnnotations;

namespace Common
{

    public class User: IModel
    {
        public int UserId { get; set; }
        
        public string FirstName { get; set; }
       
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string EMail { get; set; }
        
        [StringLength(36, MinimumLength = 6)]
        public string Password { get; set; }
        
        [Phone]
        public string Phone { get; set; }
        private string _role;
        public string Role
        {
            get
            {                
                return _role;
            }
            set
            {
                if (value == "Admin" || value == "Moderator" || value == "User")
                {
                    _role = value;
                }
            }
        }
    }
}
