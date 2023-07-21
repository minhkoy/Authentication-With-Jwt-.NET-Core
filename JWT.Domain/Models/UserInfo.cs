using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Domain.Models
{
    [Table("USER_INFO")]
    public class UserInfo : EntityBase
    {
        [Column("USER_NAME")]
        public string Username { get; set; }
        [Column("SECURITY_KEY")]
        public string SecurityKey { get; set; } //As salt
        [Column("HASHED_PASSWORD")]
        public string HashedPassword { get; set; }
        [Column("FULLNAME")]
        public string Fullname { get; set; }
        [Column("EMAIL")]
        public string Email { get; set; }
        [Column("IS_EMAIL_CONFIRMED")]
        public bool IsEmailConfirmed { get; set; } = false;
        [Column("PHONE_NUMBER")]
        public string PhoneNumber { get; set; }
        [Column("ADDRESS")]
        public string Address { get; set; }
        [Column("DATE_OF_BIRTH")]
        public long? DateOfBirth { get; set; }
        [Column("ENTERPRISE_ID")]
        public string EnterpriseId { get; set; }

        [ForeignKey(nameof(EnterpriseId))]
        [InverseProperty(nameof(Enterprise.Employees))]
        public virtual Enterprise EnterpriseOfUser { get; set; }
    }
}
