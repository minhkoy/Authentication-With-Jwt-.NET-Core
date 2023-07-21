using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Domain.Models
{
    [Table("ENTERPRISE")]
    public class Enterprise : EntityBase
    {
        public Enterprise()
        {
            Employees = new HashSet<UserInfo>();
        }
        [Column("ENTERPRISE_CODE")]
        public string EnterpriseCode { get; set; }
        [Column("ENTERPRISE_NAME")]
        public string EnterpriseName { get; set; }
        [Column("FOUNDATION_DATE")]
        public long? FoundationDate { get; set; }

        [InverseProperty(nameof(UserInfo.EnterpriseOfUser))]
        public virtual ICollection<UserInfo> Employees { get; set; }
    }
}
