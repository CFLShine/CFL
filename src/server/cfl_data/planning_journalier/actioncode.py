from mongoengine import *


class ActionCode(EmbeddedDocument):
    classname = StringField(default="")
    """
    le type de l'opération à laquelle s'applique le code
    """

    code = StringField(default="")
