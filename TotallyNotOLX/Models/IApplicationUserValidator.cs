namespace TotallyNotOLX.Models
{
    public interface IApplicationUserValidator
    {
        bool CheckUserAligibility(ApplicationUser user);
    }
}
