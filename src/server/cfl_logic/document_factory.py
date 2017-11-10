# *************** coordonnees *****************


class AdresseFactory:
    """
    Retourne un objet de Adresse\n
    commune = None
    """

    @staticmethod
    def new():
        import src.server.cfl_data.coordonnees.adresse as adresse
        a = adresse.Adresse()
        return a


class CommuneFactory:
    @staticmethod
    def new():
        import src.server.cfl_data.coordonnees.commune as commune
        c = commune.Commune()
        return c


class ContactFactory:
    @staticmethod
    def new():
        import src.server.cfl_data.coordonnees.contacts as contacts
        c = contacts.Contacts()
        return c

class LieuFactory:
    """
    Retourne un objet de Lieu\n
    lieu instancié\
    adresse instancié
    """
    @staticmethod
    def new():
        import src.server.cfl_data.coordonnees.lieu as lieu


        l = lieu.Lieu()
        l.adresse = AdresseFactory.new()
        return l


# ***************etat civil ********************

class IdentiteFactory:
    @staticmethod
    def new():
        import src.server.cfl_data.etat_civil.identite as identite

        return identite.Identite()


class NaissanceFactory:
    """
        Retourne un objet de Naissance\n
        naissance instancié\n
        lieu instancié
    """
    @staticmethod
    def new():
        import src.server.cfl_data.etat_civil.naissance as naissance

        n = naissance.Naissance()
        n.lieu = LieuFactory.new()
        return n


class PersonneFactory:
    """
    Retourne un objet de Personne\n
    identite instancié\n
    naissance instancié avec ses membres instanciés\n
    deces = None
    situation_avec = None
    pere = None
    mere = None
    """
    @staticmethod
    def new():
        import src.server.cfl_data.etat_civil.personne as personne
        p = personne.Personne()
        p.identite = IdentiteFactory.new()
        p.naissance = NaissanceFactory.new()

        return p


class QualiteFactory:
    """
    Retourne un objet de Pouvoir\n
    personne = None
    """
    @staticmethod
    def new():
        import src.server.cfl_data.etat_civil.pouvoir as pouvoir

        return pouvoir.Pouvoir()


# *************** utilisateur ******************

class UtilisateurFactory:
    """
    Retourne un objet de Utilisateur\n
    personne instancié\n
    login instancié\n
    autorisations instancié (= list())
    """
    @staticmethod
    def new():
        import src.server.cfl_data.utilisateur.utilisateur as utilisateur

        u = utilisateur.Utilisateur()
        u.personne = PersonneFactory.new()
        u.login = LoginFactory.new()
        u.autorisations = list()


class LoginFactory:
    @staticmethod
    def new():
        import src.server.cfl_data.utilisateur.login as login

        return login.Login()


class AutorisationFactory:
    @staticmethod
    def new():
        import src.server.cfl_data.utilisateur.autorisation as autorisation

        return autorisation.Autorisation()


# ***************** planing ******************

class PlanningFactory:
    """
    Retourne un objet de Planning()\n
    zonesLogic instancié (= list())
    """
    @staticmethod
    def new():
        import src.server.cfl_data.planning_journalier.planning as planning

        p = planning.Planning()
        p.zonesLogic = list()
        return p


class ActionCodeFactory:
    @staticmethod
    def new():
        import src.server.cfl_data.planning_journalier.actioncode as actioncode

        ac = actioncode.ActionCode()
        return ac


class EquipierFactory:
    """
    Retourne un objet de Equipier\n
    personne = None
    """
    @staticmethod
    def new():
        import src.server.cfl_data.planning_journalier.equipe as equipier

        return equipier.Equipier()


class EquipeFactory:
    """
    Retourne un objet de Equipe\n
    equipiers instancié (= list())
    """
    @staticmethod
    def new():
        import src.server.cfl_data.planning_journalier.equipe as equipe

        e = equipe.Equipe()
        e.equipiers = list()
        return e


class PageFactory:
    """
    Retourne un objet de Page\n
    zonesMatin instancié (= list())\n
    zonesApresMidi instancié (= list())
    """
    @staticmethod
    def new():
        import src.server.cfl_data.planning_journalier.page as page

        p = page.Page()
        p.zonesMatin = list()
        p.zonesApresMidi = list()
        return p


class ZoneFactory:
    """
    Retourne un objet de Zone()\n
    page = None
    subject = None
    equipe = None
    """
    @staticmethod
    def new():
        import src.server.cfl_data.planning_journalier.zone as zone

        return zone.Zone()

# ****************** entreprise ********************


class EntrepriseFactory:
    """
    Retourne un objet de Entreprise\n
    lieu = None\n
    elements instancié (= list())
    """
    @staticmethod
    def new():
        import src.server.cfl_data.entreprise.entreprise as entreprise

        e = entreprise.Entreprise()
        e.elements = list()
        return e


class ChambreFuneraireFactory:
    """
    Retourne un objet de ChambreFuneraire\n
    lieu = None\n
    casee instancié (= list())\n
    salon instancié (= list())
    """
    @staticmethod
    def new():
        import src.server.cfl_data.chambre_funeraire.chambre_funeraire as chambrefune

        c = chambrefune.ChambreFuneraire()
        c.cases = list()
        c.salons = list()
        return c


class SalonFactory:
    @staticmethod
    def new():
        import src.server.cfl_data.chambre_funeraire.salon as salon

        return salon.Salon()


class CaseFactory:
    @staticmethod
    def new():
        import src.server.cfl_data.chambre_funeraire.case as case

        return case.Case()


class CrematoriumFactory:
    """
    Retourne un objet de Crematorium\n
    lieu = None\n
    fours instancié (= list())
    """
    @staticmethod
    def new():
        import src.server.cfl_data.crematorium.crematorium as crema

        c = crema.Crematorium()
        c.fours = list()
        return c


class FourFactory:
    @staticmethod
    def new():
        import src.server.cfl_data.crematorium.four as four

        return four.Four()


# **************** defunt *********************


class DefuntFactory:
    """
    Retourne un objet de Defunt\n
    Defunt.personne instancié avec tous ses membres instanciés\n
    Defunt.operation instancié (=list())
    """
    @staticmethod
    def new():
        import src.server.cfl_data.defunt.defunt as defunt
        import src.server.cfl_data.etat_civil.deces as deces

        d = defunt.Defunt()
        d.personne = PersonneFactory.new()
        d.personne.deces = deces.Deces()
        d.operations = list()
        return d


class OperationFuneFactory:
    """
    Préférer utiliser directement CeremonieFactory, MEBFactory, etc.
    Retourne un objet de OperationFune\
    lieu = None
    """
    @staticmethod
    def new():
        import src.server.cfl_data.defunt.operation_fune as operation

        return operation.OperationFune()


class TransportFactory:
    """
    Retourne un objet de Transport\
    operation instancié\n
    lieuArrivee = None
    """
    @staticmethod
    def new():
        import src.server.cfl_data.defunt.transport as transport

        t = transport.Transport()
        t.operation = OperationFune()
        return t


class MEBFactory:
    """
    Retourne un objet de MEB\n
    operation instancié
    """
    @staticmethod
    def new():
        import src.server.cfl_data.defunt.meb as meb

        m = meb.MEB()
        m.operation = OperationFune()
        return m


class CeremonieFactory:
    @staticmethod
    def new():
        import src.server.cfl_data.defunt.ceremonie as ceremonie

        c = ceremonie.Ceremonie()
        c.operation = OperationFune()
        return c


class InhumationFactory:
    """
    Retourne un objet de Inhumation\n
    Tous ses mebres instanciés sauf sepulture
    """
    @staticmethod
    def new():
        import src.server.cfl_data.defunt.inhumation as inhumation

        i = inhumation.Inhumation()
        i.operation = OperationFuneFactory()
        return i


# **************** cimetiere *******************

class CimetiereFactory:
    """
    Retourne un objet de Cimetiere\
    lieu instancié\
    sepultures instancié (= list())
    """

    @staticmethod
    def new():
        import src.server.cfl_data.cimetiere.cimetiere as cimetiere
        c = cimetiere.Cimetiere()
        c.lieu = LieuFactory.new()
        c.sepultures = list()
        return c


class SepultureFactory:
    """
    Retourne un objet de Sepulture\n
    cimetiere = None
    """

    @staticmethod
    def new():
        import src.server.cfl_data.cimetiere.sepulture as sepulture
        s = sepulture.Sepulture()
        return s
