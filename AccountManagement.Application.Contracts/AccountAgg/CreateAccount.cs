using Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contracts.AccountAgg
{
    public class CreateAccount
    {
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string FullName { get; set; }
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Username { get; set; }
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string Password { get; set; }
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public long RoleId { get; set; }
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        public string PhoneNumber { get; set; }
        public string ProfilePicture { get; set; }
    }
}
