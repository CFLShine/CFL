from mongoengine import *


class Transport(Document):
    operation = EmbeddedDocumentField("OperationFune", default=None)
    """
        operation sera utilisé pour le départ du transport,\
        le commentaire et la référence vers le défunt.
    """

    lieuArrivee = ReferenceField('Lieu', default=None)
    dateArrivee = DateTimeField(default=None)
    heureArrivee = DateTimeField(default=None)

    TAMB = BooleanField(default=False)
