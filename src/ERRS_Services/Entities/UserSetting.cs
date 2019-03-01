using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dapper;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper.Contrib.Extensions;

namespace Entities
{
    public class UserSetting : IEntityBase
    {
        [Dapper.Contrib.Extensions.Key]
        [Required]
        public int Id { get; set; }
        public int memberid { get; set; }
        public int agencyid { get; set; }
        public string ModelJson { get; set; }
        public int ColumnSize { get; set; }
        public int Theme { get; set; }
        public bool Controls { get; set; }
        [Computed]
        public List<UserDashboardItem> Items { get; set; }
    }
}