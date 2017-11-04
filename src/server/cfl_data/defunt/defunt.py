from mongoengine import *


class Defunt(Document):
    personne = ReferenceField('Personne', default=None, reverse_delete_rule=CASCADE)
    pouvoir = ReferenceField('Personne', default=None)
    operations = ListField(ReferenceField('OperationFune'), default=list)

if __name__ == '__main__':
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
    import src.server.sh_core.db_helper.dbobjectsprovider as objectsprovider

    defunt = objectsprovider.getDefunt_searchIn_Identite({'$and': [{'nom': 'DUPONT'}, {'prenom': 'Michel'}]})[0]

    print(defunt.id)
    print(defunt.personne.identite.nom)
    exec(
        """
        defunt.personne.identite.nom = 'Machin'
        """)
    print(defunt.personne.identite.nom)

    """
    operation_fune = inhumation.Inhumation()
    operation_fune.enveloppe = "cercueil"
    operation_fune.commentaire = "un commentaire"
    operation_fune.defunt = defunt

    operation_fune.save(cascade = True)

    defunt.operations.append(operation_fune)
    defunt.save(cascade = True)
    #"""