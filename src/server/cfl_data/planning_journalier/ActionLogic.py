from mongoengine import *


class ActionLogic(EmbeddedDocument):
    """

    """
    heureCode = StringField(default="")
    actionCode = StringField(default="")
