namespace JWT.Manager.JwtAuthentication.Response
{
    public class JwtGetUserInfoResponse
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public List<string> Roles { get; set; }
    }
}