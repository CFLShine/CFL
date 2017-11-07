from mongoengine import *


class Entreprise(Document):
    lieu = ReferenceField('Lieu', default=None)
    habilitation = StringField(default="")

    elements = ListField(GenericReferenceField(), default=list())
    """ 
    les éléments contenus dans elements devraient être de type\n
    ChambreFuneraire,
    Crematorium,
    PompeFunebre,
    Salle 
    """
