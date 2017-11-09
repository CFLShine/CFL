from flask import Blueprint, render_template

from settings import Config

fiche_defunt = Blueprint('fiche_defunt', __name__,
                         template_folder='templates')


@fiche_defunt.route('/fiche_defunt/<dftid>')
def show(dftid):
    from src.server.cfl_data.defunt.defunt import Defunt
    from src.server.populate_db.pdb_populate import populate

    dft = Defunt.objects(id=dftid)[0]
    return render_template('fiche_defunt.html', config=Config, page_name='fiche_defunt', dft=dft)
