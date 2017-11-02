from mongoengine import *

import src.server.cfl_data.etat_civil.personne as personne

class Defunt(Document):
    personne = ReferenceField(personne.Personne, default=None, reverse_delete_rule=CASCADE)
    operations = ListField(ReferenceField('OperationFune'), default=list)

if __name__ == '__main__':
    import src.server.cfl_data.etat_civil.personne as personne
    import src.server.cfl_data.etat_civil.identite as identite
    import src.server.cfl_data.etat_civil.naissance as naissance
    import src.server.cfl_data.coordonnees.lieu as lieu
    import src.server.cfl_data.coordonnees.adresse as adresse
    from src.settings import Config

    connect(Config.db_name, host=Config.db_host, port=Config.db_port)

    """
    print("try to record defunt")

    defunt = Defunt()

    defunt.personne = personne.Personne()
    defunt.personne.identite = identite.Identite()

    defunt.personne.naissance = naissance.Naissance()
    defunt.personne.naissance.lieu = lieu.Lieu()
    defunt.personne.naissance.lieu.adresse = adresse.Adresse()

    defunt.personne.identite.prenom = "Jeanne"
    defunt.personne.identite.nom = "DUPONT"
    defunt.personne.naissance.date = "01/10/1959"
    defunt.personne.naissance.lieu.adresse.adresse1 = "chemin des Annes"

    defunt.personne.naissance.lieu.adresse.save(cascade = True)
    defunt.personne.naissance.lieu.save(cascade = True)
    defunt.personne.naissance.save(cascade = True)

    defunt.personne.identite.save(cascade = True)

    defunt.personne.save(cascade = True)

    defunt.save(cascade = True)
    
    #"""

    print("try to find back defunt Michel DUPONT")

    from mongoengine.queryset.visitor import Q

    identites = identite.Identite.objects(Q(nom='DUPONT')&Q(prenom='Michel'))
    print(identites[0].nom + ' ' + identites[0].prenom)

    personnes = personne.Personne.objects(identite=identites[0].id)
    print(personnes[0].id)

    defunt = Defunt.objects(personne=personnes[0].id)[0]
    print(defunt.id)