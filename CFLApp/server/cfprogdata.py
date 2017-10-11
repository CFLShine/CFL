import json
from flask import Flask
from flask_restful import Api, Resource

app = Flask(__name__)
api = Api(app)


@app.route('/data/<path:filepath>')
def getData(filepath):
    try:
        return CfProgData("E:/dev/CFLApp/server/" + filepath).parse_data().toJSON()
    except Exception as e:
        return "{\"err\": \"" + str(e) + "\"}"


@app.route('/data/users')
def getUsers():
    return open('E:/dev/CFLApp/server/users.json').read()


@app.route('/connect/<username>/<password>')
def connect(username, password):
    users = json.loads(open('E:/dev/CFLApp/server/users.json'), 'utf-8')
    if users[username]["password"] == password:
        return '{"connect_accepted": true}';
    return '{"connect_accepted": false}';


class CfProgData(Resource):
    def __init__(self, filepath=None):
        self.filepath = filepath

        self.data = dict()
        self.data["__children"] = list()

    def parse_data(self, content=None):
        if not content:
            content = open(self.filepath + ".txt", "r").readlines()

        while content:
            l = content.pop(0)
            if l.startswith("With"):
                withobj = CfProgData()
                withobj.parse_data(content)

                self.data["__children"] += [withobj]
            elif l.startswith("End With"):
                return
            elif l.startswith("<METASF>") or l.startswith("<\\METASF>"):
                pass
            else:
                datalist = l.split("=")
                dataname = datalist[0].strip()
                data     = datalist[1].strip()
                self.data[dataname] = data

        return self

    def toJSON(self):
        return json.dumps(self.data, default=lambda o: o.data,
                          sort_keys=True, indent=4)


if __name__ == "__main__":
    app.run(port=5003)
