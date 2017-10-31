from mongoengine import *

from src.server.cfl_data.defunt.operation_fune import OperationFune

class Defunt(Document):
    
    operations = ListField(OperationFune, default = None)


