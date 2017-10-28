from mongoengine import *


class Defunt(Document):
    nom = StringField()
    prenom = StringField()


if __name__ == '__main__':
    from src.settings import Config

    connect(Config.db_name, host=Config.db_host, port=Config.db_port)
    for name in ['DUPONT Jean', 'MARCEL Luc', "DUTROU Henri", "DEVOL Marion", "RATEL Isabelle"]:
        d = Defunt(nom=name.split(' ')[0], prenom=name.split(' ')[1])
        d.save()
