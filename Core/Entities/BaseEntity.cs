using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class BaseEntity
    {
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // [Key]
		public int Id { get; protected set; }
    }
}