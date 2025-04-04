namespace Company.Zeinab4.PL.DTO
{
    public class UserToReturnDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Eamil { get; set; }
        public IEnumerable<string>? Roles { get; set; }
    }
}
