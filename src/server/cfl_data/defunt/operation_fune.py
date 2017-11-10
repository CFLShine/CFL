from mongoengine import *


class OperationFune(EmbeddedDocument):
    lieu = GenericReferenceField(default=None)
    """
    lieu peut être un objet de Lieu mais aussi
    un objet qui contient un lieu, comme une chambre funéraire, une salle, etc
    """

    defunt = ReferenceField('Defunt', default=None)
    datetime = DateTimeField(default=None)
    commentaire = StringField(default="")
