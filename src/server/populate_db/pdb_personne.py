def populate():
    from src.server.cfl_data.etat_civil.personne import Personne
    from src.server.cfl_data.etat_civil.identite import Identite

    p1 = Personne()
    p2 = Personne()

    id1 = Identite.objects(nom="DUPONT")[0]
    id2 = Identite.objects(nom="DUPONNE")[0]

    p1.identite = id1
    p2.identite = id2

    p1.situation = "personne1"
    p2.situation = "personne2"

    p1.save()
    p2.save()
