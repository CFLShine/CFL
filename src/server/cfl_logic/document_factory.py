def Lieu():
    """
    Retourne un objet de Lieu\n
    lieu instancié\
    adresse instancié
    """
    import src.server.cfl_data.coordonnees.lieu as lieu
    import src.server.cfl_data.coordonnees.adresse as adresse

    l = lieu.Lieu()
    l.adresse = adresse.Adresse()

    return l


def Identite():
    import src.server.cfl_data.etat_civil.identite as identite

    return identite.Identite()


def Naissnce():
    """
    Retourne un objet de Naissance\n
    naissance instancié\n
    lieu instancié
    """
    import src.server.cfl_data.etat_civil.naissance as naissance

    n = naissance.Naissance()
    n.lieu = Lieu()
    return n


def Personne():
    """
    Retourne un objet de Personne\n
    identite instancié\n
    naissance instancié avec ses membres instanciés\n
    deces = None
    situation_avec = None
    pere = None
    mere = None
    """
    import src.server.cfl_data.etat_civil.personne as personne
    p = personne.Personne()
    p.identite = Identite()
    p.naissance = Naissnce()

    return p


def Qualite():
    """
    Retourne un objet de Pouvoir\n
    personne = None
    """

    import src.server.cfl_data.etat_civil.pouvoir as pouvoir

    return pouvoir.Pouvoir()


############# utilisateur #############

def Utilisateur():
    """
    Retourne un objet de Utilisateur\n
    personne instancié\n
    login instancié\n
    autorisations instancié (= list())
    """

    import src.server.cfl_data.utilisateur.utilisateur as utilisateur

    u = utilisateur.Utilisateur()
    u.personne = Personne()
    u.login = Login()
    u.autorisations = list()


def Login():
    import src.server.cfl_data.utilisateur.login as login

    return login.Login()


def Autorisation():
    import src.server.cfl_data.utilisateur.autorisation as autorisation

    return autorisation.Autorisation()


############# planing #################

def Planning():
    """
    Retourne un objet de Planning()\n
    zonesLogic instancié (= list())
    """

    import src.server.cfl_data.planning_journalier.planning as planning

    p = planning.Planning()
    p.zonesLogic = list()
    return p


def Equipier():
    """
    Retourne un objet de Equipier\n
    personne = None
    """

    import src.server.cfl_data.planning_journalier.equipe as equipier

    return equipier.Equipier()


def Equipe():
    """
    Retourne un objet de Equipe\n
    equipiers instancié (= list())

    """
    import src.server.cfl_data.planning_journalier.equipe as equipe

    e = equipe.Equipe()
    e.equipiers = list()
    return e


def Page():
    """
    Retourne un objet de Page\n
    zonesMatin instancié (= list())
    zonesApresMidi instancié (= list())
    """

    import src.server.cfl_data.planning_journalier.page as page

    p = page.Page()
    p.zonesMatin = list()
    p.zonesApresMidi = list()


def Zone():
    """
    Retourne un objet de Zone()\n
    page = None
    subject = None
    equipe = None
    """
    import src.server.cfl_data.planning_journalier.zone as zone

    return zone.Zone()


def ActionLogic():
    import src.server.cfl_data.planning_journalier.ActionLogic as actioLogic

    return actioLogic.ActionLogic()


############# entreprise ##############

def Entreprise():
    """
    Retourne un objet de Entreprise\n
    lieu = None\n
    elements instancié (= list())
    """

    import src.server.cfl_data.entreprise.entreprise as entreprise

    e = entreprise.Entreprise()
    e.elements = list()


def ChambreFuneraire():
    """
    Retourne un objet de ChambreFuneraire\n
    lieu = None\n
    casee instancié (= list())\n
    salon instancié (= list())
    """

    import src.server.cfl_data.chambre_funeraire.chambre_funeraire as chambrefune

    c = chambrefune.ChambreFuneraire()
    c.cases = list()
    c.salons = list()


def Salon():
    import src.server.cfl_data.chambre_funeraire.salon as salon

    return salon.Salon()


def Case():
    import src.server.cfl_data.chambre_funeraire.case as case

    return case.Case()


def Crematorium():
    """
    Retourne un objet de Crematorium\n
    lieu = None\n
    fours instancié (= list())
    """

    import src.server.cfl_data.crematorium.crematorium as crema

    c = crema.Crematorium()
    c.fours = list()
    return c


def Four():
    import src.server.cfl_data.crematorium.four as four

    return four.Four()


############# defunt ###################

def Defunt():
    """
    Retourne un objet de Defunt\n
    Defunt.personne instancié avec tous ses membres instanciés\n
    Defunt.operation instancié (=list())
    """
    import src.server.cfl_data.defunt.defunt as defunt
    import src.server.cfl_data.etat_civil.deces as deces

    dft = defunt.Defunt()
    dft.personne = Personne()
    dft.personne.deces = deces.Deces()
    dft.operations = list()

    return dft


def OperationFune():
    """
    Retourne un objet de OperationFune\
    lieu = None
    """

    import src.server.cfl_data.defunt.operation_fune as operation

    return operation.OperationFune()


def Transport():
    """
    Retourne un objet de Transport\
    operation instancié\n
    lieuArrivee = None
    """

    import src.server.cfl_data.defunt.transport as transport

    t = transport.Transport()
    t.operation = OperationFune()


def MEB():
    """
    Retourne un objet de MEB\n
    operation instancié
    """

    import src.server.cfl_data.defunt.meb as meb

    m = meb.MEB()
    m.operation = OperationFune()
    return m


def Ceremonie():
    import src.server.cfl_data.defunt.ceremonie as ceremonie

    c = ceremonie.Ceremonie()
    c.operation = OperationFune()


def Inhumation():
    """
    Retourne un objet de Inhumation\n
    Tous ses mebres instanciés sauf sepulture
    """
    import src.server.cfl_data.defunt.inhumation as inhumation

    i = inhumation.Inhumation()
    i.operation = OperationFune()
