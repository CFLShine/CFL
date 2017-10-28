import argparse
import os
import json


if __name__ == '__main__':
    parser = argparse.ArgumentParser()

    parser.add_argument("b", help="génère un nouveau blueprint")

    args = parser.parse_args()
    bname = args.b

    path = f"src/client/blueprints/{bname}/"
    try:
        os.makedirs(path + 'templates')
    except Exception as e:
        print(e)

    pyfile = open(path + bname + '.py', mode='w')
    template = open(path + 'templates/' + bname + '.html', mode='w')

    pyfile.write(f"""from flask import Blueprint, render_template
from src.settings import Config

{bname} = Blueprint('{bname}', __name__,
                  template_folder='templates')


@{bname}.route('/{bname}/')
def show():
    return render_template('{bname}.html', config=Config, page_name='{bname}')
""")

    template.write("""<!DOCTYPE html>
<html lang="en">
{% include('head.html') %}
<body>
{% include('header.html') %}
{% include('nav.html') %}

<section class="body">
    
</section>

{% include('footer.html') %}
</body>
</html>""")

    packages = json.load(open('src/client/blueprints.json'))
    if bname not in packages:
        packages += [bname]
    json.dump(packages, open('src/client/blueprints.json', 'w'))
