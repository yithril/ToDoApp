using Microsoft.AspNetCore.Identity;

namespace TaskManagerToDo.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
        public string SomeOtherCustomField { get; set; }
    }
}
