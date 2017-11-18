using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MSTD.ShBase;

namespace CFL_1.CFL_Data
{
    [DataContract]
    public enum TITRE
    {
        [EnumMember(Value = "")]
        UNDIFINED,
        [EnumMember(Value = "M.")]
        M,
        [EnumMember(Value = "Mme")]
        MME,
        [EnumMember(Value = "Melle")]
        MELLE,
        [EnumMember(Value = "Enfant")]
        ENFANT
    }

    /// <summary>
    /// Nom, nom jf, prenom, ..., nationalite, profession
    /// </summary>
    public class Identite : Base
    {
        public Identite() { }

        [DataMember]
        public TITRE Titre { get ; set ; } = TITRE.UNDIFINED;

        [DataMember]
        public string Nom { get ; set ; } = "";

        [DataMember]
        [Display(Name = "Nom de jeune fille")]
        public string NomJf { get ; set ; } = "";
        
        [DataMember]
        [Display(Name = "Prénom")]
        public string Prenom { get ; set ; } = "";

        [DataMember]
        [Display(Name = "Prénoms")]
        public string Prenoms { get ; set ; } = "";
        
        [DataMember]
        [Display(Name = "Nationalité")]
        public string Nationalite { get ; set ; } = "";
        
        [DataMember]
        public string Profession { get ; set ; } = "";
        
    }
}
