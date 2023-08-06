using System.ComponentModel.DataAnnotations.Schema;

namespace JWT.Domain.Models;

[Table("ROLE")]
public class Role : EntityBase
{
    [Column("ROLE_CODE")]
    public string RoleCode { get; set; }
    [Column("ROLE_NAME")]
    public string RoleName { get; set; }
    //[Column("")]
}