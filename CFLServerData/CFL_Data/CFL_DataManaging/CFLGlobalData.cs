using System.ComponentModel.DataAnnotations.Schema;
using CFL_1.CFL_System.SqlServerOrm;
using MSTD.ShBase;

namespace CFL_1.CFL_Data.CFL_DataManaging
{
    [NotMapped]
    public class CFLGlobalData : Base
    {

        #region utilisateurs

        //TODO Chargement des utilisateurs si non fait.
        public bool LoadUsers()
        {
            return false;
        }

        /// <summary>
        /// Retourne l'utilisateur connecté à la session, 
        /// </summary>
        public Utilisateur utilisateurCourant
        {
            get
            {
                if (__utilisateurCourant == null)
                    __utilisateurCourant = new Utilisateur();
                return __utilisateurCourant;
            }
            set 
            { 
                // TODO 
            }
        }

        #endregion utilisateurs

        public void GetNotification(DBNotification _notification)
        {
            
        }

        //private:
        Utilisateur __utilisateurCourant;

        // Singleton:
        private CFLGlobalData() { }
        public static readonly CFLGlobalData instance = new CFLGlobalData() ;
    }
}
