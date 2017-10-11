
using CFL_1.CFL_Data;
using CFL_1.CFL_Data.Cimetiere;
using CFL_1.CFL_Data.Communes;
using CFL_1.CFL_Data.Defunts;
using CFL_1.CFL_Data.Etat_civil;
using CFL_1.CFL_Data.Planings;
using CFL_1.CFL_Data.Thanatopraxie;
using CFL_1.CFLGraphics.MyControls.GraphEditor;
using MSTD;
using SqlOrm;

namespace CFL_1.CFL_System.DB
{
    public class DBContext_CFL : DBContext
    {
        private DBContext_CFL()
            : base(CFLDBConnection.instance)
        {
            CreateOrCompleteDataBase();
        }

        private static DBContext_CFL __instance = null;
        public static DBContext_CFL instance
        {
            get
            {
                if(__instance == null)
                    __instance = new DBContext_CFL();
                return __instance;
            }
        }

        public DBSet<GraphProject>        GraphProject       { get; set; }
        public DBSet<ShapeTypeInfo>       ShapeTypeInfo      { get; set; }
                                                           
        public DBSet<PlaningJournalier>  Planing_journalier  { get; set; }
        public DBSet<PageJour>            PageJour           { get; set; }
        public DBSet<ZoneInfo>            ZoneInfo           { get; set; }
        public DBSet<ZoneAction>          ZoneAction         { get; set; }

        public DBSet<Autorisation>        Autorisation       { get; set; }

        public DBSet<Commune>             Commune            { get; set; }

        public DBSet<Lieu>                Lieu               { get; set; }

        public DBSet<Entreprise>          Entreprise         { get; set; }

        public DBSet<ChambreFuneraire>    ChambreFuneraire   { get; set; }
        public DBSet<Salon>               Salon              { get; set; }
        public DBSet<CaseRefrigeree>                Case               { get; set; }

        public DBSet<Pf>                  Pf                 { get; set; }

        public DBSet<Crematorium>         Crematorium        { get; set; }
        public DBSet<Four>                Four               { get; set; }

        public DBSet<Defunt>              Defunt             { get; set; }
        public DBSet<Deces>               Deces              { get; set; }

        public DBSet<Naissance>           Naissance          { get; set; }
        public DBSet<SituationFamiliale>  SituationFamillale { get; set; }
        public DBSet<Filiation>           Filiation          { get; set; }
        public DBSet<Pouvoir>             Pouvoir            { get; set; }
        public DBSet<Parent>              Parent             { get; set; }

        public DBSet<Cimetiere>           Cimetiere          { get; set; }
        public DBSet<Sepulture>           Sepulture          { get; set; }

        public DBSet<StagiaireThanato>    StagiaireThanato   { get; set; }

        public DBSet<OperationFune>       OperationFune      { get; set; }
        public DBSet<TAMB>                TAMB               { get; set; }
        public DBSet<Soin>                Soin               { get; set; }
        public DBSet<MEB>                 MEB                { get; set; }
        public DBSet<Ceremonie>           Ceremonie          { get; set; }
        public DBSet<Transport>           Transport          { get; set; }
        public DBSet<Cremation>           Cremation          { get; set; }
        public DBSet<Inhumation>          Inhumation         { get; set; }
        public DBSet<Dispersion>          Dispersion         { get; set; }
        public DBSet<Exhumation>          Exhumation         { get; set; }
        public DBSet<RemiseCendres>       RemiseCendres      { get; set; }
        public DBSet<AutreOperation>      AutreOperation     { get; set; }

        public DBSet<Vehicule>            Vehicule           { get; set; }

        public DBSet<RaisonSociale>       Raison_sociale     { get; set; }

        public DBSet<Personne>            Personne           { get; set; }

        public DBSet<Metier>              Metier             { get; set; }
        public DBSet<Utilisateur>         Utilisateur        { get; set; }
        public DBSet<Login>               Login              { get; set; }
        public DBSet<Identite>            Identite           { get; set; }
        public DBSet<Coordonnees>         Coordonnees        { get; set; }
        public DBSet<Contacts>            Contacts           { get; set; }

    }
}
