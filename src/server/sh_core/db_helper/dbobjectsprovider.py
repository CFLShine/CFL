import src.server.sh_core.types_helper as types_helper

"""
ex le document de type Identite demandé répondant aux conditions
a.b.identite.nom='DUPONT' & a.b.identite.prenom='Jean'
types : A, B, Identite
conditions : 
{
    '$and':
    [{'nom':'DUPONT'},{'prenom':'Jean'}]
}
"""


# defunt.personne.identite.nom == 'Machin'

# getDocument(Defunt, ('personne','identite'), {'$and':[{'nom':'DUPONT'},{'prenom':'Michel'}]})
def getDocuments(classType, members, condition):
    pass


def getDefunt_searchIn_Identite(conditions):
    Defunt = types_helper.import_string('src.server.cfl_data.defunt.defunt.Defunt')
    Personne = types_helper.import_string('src.server.cfl_data.etat_civil.personne.Personne')
    Identite = types_helper.import_string('src.server.cfl_data.etat_civil.identite.Identite')

    identites = Identite.objects(__raw__=conditions)

    d = Defunt(parent=None)
    pers = Personne(parent=d)

    def search(list1, classt, field):
        lst = list()
        for obj in list1:
            for obj2 in classt.objects(**{field: obj.id}):
                lst.append(obj2)

        return lst

    personnes = search(identites, Personne, "identite")
    defunts = search(personnes, Defunt, "personne")

    return defunts


def getDocuments(*types, conditions=None):
    # [d for Defunt.objects if d.identite in Identite.objects(nom="machin")]

    if not types:
        return list()

    currentClass = types[-1]  # dernier
    objects = currentClass.objects if not conditions else currentClass.objects(__raw__=conditions)

    if not objects:
        return list()
