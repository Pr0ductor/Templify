namespace Templify.Shared.Constants;

public static class AppConstants
{
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string Author = "Author";
        public const string User = "User";
    }
    
    public static class Claims
    {
        public const string UserId = "UserId";
        public const string UserName = "UserName";
        public const string Email = "Email";
        public const string Role = "Role";
    }
    
    public static class Pagination
    {
        public const int DefaultPageSize = 10;
        public const int MaxPageSize = 100;
    }
}
