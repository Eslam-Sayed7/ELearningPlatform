namespace Infrastructure.Dtos;
public class loggedUserDto {
    public string Message { get; set;}
    public string Username { get; set; }
    public Guid UserId { get; set; }
    public bool IsAuthenticated { get; set; }

}
