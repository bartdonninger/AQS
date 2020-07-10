using System;
using System.ComponentModel.DataAnnotations;

namespace AQS.Api.Reading.Domain
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public DateTime UpdatedDateUtc { get; set; }
    }
}
