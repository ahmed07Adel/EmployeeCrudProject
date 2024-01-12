using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public int Mobile { get; set; }
        public string Email { get; set; }
        public int? EducationId { get; set; }
        [ForeignKey(nameof(EducationId))]
        public Education Education { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}
