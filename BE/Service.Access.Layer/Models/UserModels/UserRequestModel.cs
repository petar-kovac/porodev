namespace Api.Access.Layer.Models.UserModels
{
    public class UserRequestModel
    {
        public string? Name { get; set; }

        public string? Lastname { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Role { get; set; }

        public string? Department { get; set; }

        public string? Position { get; set; }

        public string? AvatarUrl { get; set; }
    }
}
