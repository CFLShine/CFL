def populate():
    from src.server.cfl_data.etat_civil.identite import Identite

    id1 = Identite()
    id2 = Identite()

    id1.civilite = "M"
    id1.nom = "DUPONT"
    id1.prenom = "Jean"

    id2.civilite = "Mme"
    id2.nom = "DUPONNE"
    id2.prenom = "Jeanette"

    id1.save()
    id2.save()
