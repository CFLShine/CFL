from mongoengine import *


class ActionCode(EmbeddedDocument):
    """

    """
    heureCode = StringField(default="")
    actionCode = StringField(default="")
