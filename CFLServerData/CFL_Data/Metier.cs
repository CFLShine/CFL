using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MSTD.ShBase;

namespace CFL_1.CFL_Data
{
    public class Metier : Base
    {
        public string Intitule { get; set; }

        [Display(AutoGenerateField = false)]
        public List<Utilisateur> Utilisateurs { get; set; } = new List<Utilisateur>();
    }
}
