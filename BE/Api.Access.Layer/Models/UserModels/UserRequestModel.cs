namespace Api.Access.Layer.Models.UserModels
{
    public class UserRequestModel
    {
        public string? Name { get; set; }

        public string? Lastname { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Salt { get; set; }

        public int? Role { get; set; }

        public int? Department { get; set; }

        public int? Position { get; set; }

        public string? AvatarUrl { get; set; }
    }
}