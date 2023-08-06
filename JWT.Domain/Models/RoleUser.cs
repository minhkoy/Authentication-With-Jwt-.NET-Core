using System.ComponentModel.DataAnnotations.Schema;

namespace JWT.Domain.Models;

[Table("ROLE_USER")]
public class RoleUser : EntityBase
{
    [Column("ROLE_ID")]
    public string RoleId { get; set; }
    [Column("USER_ID")]
    public string UserId { get; set; }
}