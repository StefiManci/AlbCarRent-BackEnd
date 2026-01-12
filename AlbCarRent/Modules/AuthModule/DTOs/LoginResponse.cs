namespace AlbCarRent.Modules.AuthModule.DTOs
{
    public class LoginResponse
    {
        public bool Success { get; set; }

        public string Message {  get; set; }

        public string Token { get; set; }

        public string Id { get; set; }

        public string Role { get; set; }
    }
}
