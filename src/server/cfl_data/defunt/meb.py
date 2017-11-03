from mongoengine import *

import src.server.cfl_data.defunt.operation_fune as operationFune


class MEB(operationFune.OperationFune):
    salon = ReferenceField('Salon', default=None)