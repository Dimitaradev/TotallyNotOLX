namespace TotallyNotOLX.Models
{
    public class ApplicationUserValidator : IApplicationUserValidator
    {
        public bool CheckUserAligibility(ApplicationUser user)
        {
            if(user.FirstName != null && user.LastName != null)
                return true;
            else
                return false;
        }
    }
}
