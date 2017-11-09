def populate():
    from src.server.cfl_data.defunt.defunt import Defunt
    from src.server.cfl_data.etat_civil.personne import Personne

    dft1 = Defunt()
    dft2 = Defunt()

    p1 = Personne.objects(situation="personne1")[0]
    p2 = Personne.objects(situation="personne2")[0]

    dft1.personne = p1
    dft2.personne = p2

    dft1.save()
    dft2.save()
