import devpy.develop as log

log.info("Initializing MongoDB Documents...")

print("import personne")
import src.server.cfl_data.etat_civil.personne
print("import deces")
import src.server.cfl_data.etat_civil.deces
print("import identite")
import src.server.cfl_data.etat_civil.identite
print("import naissance")
import src.server.cfl_data.etat_civil.naissance
print("import defunt")
import src.server.cfl_data.defunt.defunt
#print("import ceremonie")
#import src.server.cfl_data.defunt.ceremonie
#print("import inhumation")
#import src.server.cfl_data.defunt.inhumation
#print("import meb")
#import src.server.cfl_data.defunt.meb
#import src.server.cfl_data.defunt.operation_fune

log.info("...importing done")
