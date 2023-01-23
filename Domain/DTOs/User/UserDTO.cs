using Domain.EntitiesModels;

namespace Domain.DTOs.User;

public class UserDTO : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
}
