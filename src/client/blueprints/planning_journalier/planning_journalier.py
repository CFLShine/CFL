from flask import Blueprint, render_template
from src.settings import Config

planning_journalier = Blueprint('planning_journalier', __name__,
                  template_folder='templates')


@planning_journalier.route('/planning_journalier/')
def show():
    return render_template('planning_journalier.html', config=Config, page_name='planning_journalier')
