using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace MSTD
{
    public abstract class Base
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(AutoGenerateField = false)]
        public Guid ID { get; set; } = Guid.NewGuid();
        
    }   
}
