import importlib
import json

import devpy.develop as log

from src.settings import Config


def start(app):
    packages = json.load(open('./blueprints.json'))

    for package in packages:
        log.info(f"Registering blueprint {package}")
        blueprint = importlib.import_module(f"src.client.blueprints.{package}.{package}")
        app.register_blueprint(blueprint.__dict__[package])

    app.run("localhost", 5003, Config.DEBUG, use_reloader=False)
