from flask import Blueprint, render_template
from settings import Config

fiche_defunt = Blueprint('fiche_defunt', __name__,
                         template_folder='templates')


@fiche_defunt.route('/fiche_defunt/')
def show():
    from server.models.defunt import Defunt

    return render_template('fiche_defunt.html', config=Config, page_name='fiche_defunt', dfts=Defunt.objects)
