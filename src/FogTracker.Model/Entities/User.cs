namespace FogTracker.Model.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Key]
        public string UserId { get; set; }

        [MaxLength(250)]
        public string Username { get; set; }
        [MaxLength(250)]
        public string FirstName { get; set; }
        [MaxLength(250)]
        public string LastName { get; set; }
        [MaxLength(250)]
        public string EmailAddress { get; set; }
        public string PictureUrl { get; set; }

        public string PasswordHash { get; set; }
    }
}