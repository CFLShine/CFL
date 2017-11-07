from mongoengine import *

from src.settings import Config

connect(Config.db_name, host=Config.db_host, port=Config.db_port)

def getDocuments(classtype, members, conditions):
    """
    utilisation  : nous voulons les objets de la classe Personne répondants à\n
    Personne.identite.nom == 'DUPONT' and Personne.identite.prenom == 'Michel' : \n
    getDocuments(Persone, (identite,), {'$and': [{'nom': 'DUPONT'}, {'prenom': 'Michel'}]})\n

    classtype le type racine qui sera retourné dans une liste<classtype>\n
    members : tuple de strings\n

    conditions : les conditions qui seront appliquées pour le tri sur le dernier\n
    membre de members sous le format \n
    {'$operator': [ {'member1': value1}, {'member2': value2},...]}
    """

    if len(members) == 0:
        return classtype.objects(__raw__=conditions).select_related(10)

    def search(resultlist, classt, field):
        lst = list()
        for obj in resultlist:
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
        t = type(dic[member].document_type())
        members_types.insert(0, (member, t))

    currentResult = members_types[0][1].objects(__raw__=conditions)
    currentMember = members_types[0][0]

    if len(members_types) > 1:
        for m_t in members_types[1:]:
            currentResult = search(currentResult, m_t[1], currentMember)
            currentMember = m_t[0]

    return search(currentResult, classtype, currentMember)


################################################################################################################


if __name__ == "__main__":
    print("start test")
    import src.server.cfl_data.defunt.defunt as defunt

    defunt = \
        getDocuments \
            (defunt.Defunt, ('personne', 'identite'), {'$and': [{'nom': 'DUPONT'}, {'prenom': 'Michel'}]})[0]

    print(defunt.personne.identite.nom + " " + defunt.personne.identite.prenom)
