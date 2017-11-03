from mongoengine import *

import src.server.cfl_data.defunt.operation_fune as operationFune

class Inhumation(operationFune.OperationFune):

    enveloppe = StringField(default="") # EnumEnveloppe
    sepulture = ReferenceField('Sepulture', default=None)
    scellementUrne = BooleanField(default=False)