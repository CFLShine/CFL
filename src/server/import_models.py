import devpy.develop as log

log.info("Initializing MongoDB Documents...")

import src.server.cfl_data.etat_civil.naissance
import src.server.cfl_data.etat_civil.identite
import src.server.cfl_data.etat_civil.deces
import src.server.cfl_data.etat_civil.personne
import src.server.cfl_data.etat_civil.pouvoir

import src.server.cfl_data.defunt.operation_fune
import src.server.cfl_data.defunt.meb
import src.server.cfl_data.defunt.transport
import src.server.cfl_data.defunt.ceremonie
import src.server.cfl_data.defunt.inhumation



log.info("...importing MongoDB Documents done")
