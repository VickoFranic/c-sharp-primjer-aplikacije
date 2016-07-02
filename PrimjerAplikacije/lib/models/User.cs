
namespace PrimjerAplikacije.lib.models
{
    /**
     * User model class
     * 
     * @param id - User`s unique Facebook ID
     * @param name - User`s name on Facebook
     * @param picture - URL of profile picture
     * @param email - Email field for user on Facebook
     */
    public class User
    {
        public string id;
        public string name;
        public string picture;
        public string email;
    }
}
