using Domain.EntitiesModels;

namespace Domain.DTOs.User;

public class UserDTO : BaseEntity
{
    public string FullName { get; set; }
    public string UserName { get; set; }
}
