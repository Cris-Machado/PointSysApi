using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Domain.DTOs.Base
{
    public abstract class DtoBase
    {
        #region ## Properties
        [Key]
        public string Id { get; set; }
        #endregion
    }
}
