using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Domain.Interfaces
{
    public interface IEntityBase
    {
        string Id { get; set; }
        long CreatedTime { get; set; }
        long ModifiedTime { get; set; }
        bool? IsActive { get; set; }
        bool? IsDelete { get; set; }
    }
}
