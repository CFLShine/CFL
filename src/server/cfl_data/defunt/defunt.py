from mongoengine import *


class Defunt(Document):
    personne = ReferenceField('Personne', default=None)
    deces = ReferenceField('Deces', default=None)
    situation = StringField(default="") # enumSituation

    operations = ListField(ReferenceField('OperationFune'), default=None)

if __name__ == '__main__':
    import src.server.cfl_data.etat_civil.personne as personne
    import src.server.cfl_data.etat_civil.identite as identite

    print("try to record defunt")

    defunt = Defunt()
    defunt.personne = personne.Personne()
    defunt.personne.identite = identite.Identite()
    defunt.personne.identite.prenom = 'Marcel'
    defunt.personne.identite.nom = 'DURAND'
    defunt.save()