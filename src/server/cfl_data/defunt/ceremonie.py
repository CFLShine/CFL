from mongoengine import *

import src.server.cfl_data.defunt.operation_fune as operationFune


class Ceremonie(operationFune.OperationFune):
    genre = StringField(default="") #civile, religieuse, etc
    registreCondoleances = BooleanField(default=False)
    bourseDons = BooleanField(default=False)
    presse = BooleanField(default=False)