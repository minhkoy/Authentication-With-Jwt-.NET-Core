using JWT.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Domain.Models
{
    public abstract class EntityBase : IEntityBase
    {
        [Key]
        [Column("ID")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Column("CREATED_TIME")]
        public long CreatedTime { get; set; } = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
        [Column("MODIFIED_TIME")]
        public long ModifiedTime { get; set; }
        [Column("IS_ACTIVE")]
        public bool? IsActive { get; set; }
        [Column("IS_DELETE")]
        public bool? IsDelete { get; set; }
    }
}
