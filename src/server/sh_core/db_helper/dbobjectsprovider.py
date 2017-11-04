import src.server.sh_core.types_helper as types_helper
from mongoengine import *
from src.settings import Config

connect(Config.db_name, host=Config.db_host, port=Config.db_port)

def getDocuments(classtype, members, conditions):

    def search(list1, classt, field):
        lst = list()
        for obj in list1:
            for obj2 in classt.objects(**{field: obj.id}):
                lst.append(obj2)

        return lst

    members_types = list()

    t = classtype
    for member in members:

        dic = None
        if len(t.__dict__) > 0:
            dic = t.__dict__
        else:
            dic = type(t).__dict__

        print("in type ")
        print(t)
        print(type(t))
        print("look for member : " + member)
        print("keys :")
        for k in dic.items():
            print(k)
        print("end keys")

        t = dic[member].document_type()

        print("...")

        members_types.insert(0, (member, t))

    queryset = members_types[0][1].objects(__raw__=conditions)

    if len(members_types) == 0:
        return classtype.objects(__raw__=conditions)

    for m_t in members_types[1:]:
        currentResult = search(currentResult, m_t[1], m_t[0])

    return currentResult


def getDefunt_searchIn_Identite(conditions):
    Defunt = types_helper.import_string('src.server.cfl_data.defunt.defunt.Defunt')
    Personne = types_helper.import_string('src.server.cfl_data.etat_civil.personne.Personne')
    Identite = types_helper.import_string('src.server.cfl_data.etat_civil.identite.Identite')

    identites = Identite.objects(__raw__=conditions)

    print(type(identites))

    def search(list1, classt, field):
        lst = list()
        for obj in list1:
            for obj2 in classt.objects(**{field: obj.id}):
                lst.append(obj2)

        return lst

    personnes = search(identites, Personne, "identite")
    defunts = search(personnes, Defunt, "personne")

    return defunts

################################################################################################################


if __name__ == "__main__":
    print("start test")
    import src.server.cfl_data.import_models
    import src.server.cfl_data.defunt.defunt as defunt
    #getDocuments(defunt.Defunt, ('personne', 'identite'), {'$and': [{'nom': 'DUPONT'}, {'prenom': 'Michel'}]})
    getDefunt_searchIn_Identite({'$and': [{'nom': 'DUPONT'}, {'prenom': 'Michel'}]})
