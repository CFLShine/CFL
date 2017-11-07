from mongoengine import *


class Defunt(Document):
    personne = ReferenceField('Personne', default=None, reverse_delete_rule=CASCADE)
    pouvoir = ReferenceField('Personne', default=None)
    operations = ListField(GenericReferenceField(), default=list())
    """ liste de documents contenant un membre operation <OperationFune>"""
