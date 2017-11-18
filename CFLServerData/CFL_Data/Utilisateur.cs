using System.Collections.Generic;

namespace CFL_1.CFL_Data
{
    /// <summary>
    /// Hérite de Personne.
    /// Un employé est représenté par la classe Utilisateur,
    /// même s'il n'a pas d'accès au logiciel (autorisations vide).
    /// Contient string habilitaion, <see cref="Login"/> login, 
    /// <see cref="DBList"/>(d' <see cref="Autorisation"/>) autorisations.
    /// </summary>
    public class Utilisateur : Personne
    {
        public string habilitation { get; set; }
        public Login login { get; set; } = new Login();
        public List<Autorisation> autorisations { get; set; } = new List<Autorisation>();
    }
}
