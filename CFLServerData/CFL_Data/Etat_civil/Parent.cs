
using System;
using System.ComponentModel.DataAnnotations;

namespace CFL_1.CFL_Data
{
    public class Parent : Personne
    {
        [Display(Name = "Décédé(e)")]
        public bool decede { get; set; }

        [Display(Name = "Décédé(e) le")]
        public DateTime? decesDate { get; set; }
    }
}
