using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSTD.ShBase
{
    public abstract class Base
    {
        public Base()
        {}
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(AutoGenerateField = false)]
        public Guid ID { get; set; } = Guid.NewGuid();
    }   
}
