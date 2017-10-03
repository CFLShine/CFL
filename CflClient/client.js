Client = require('jsonrpc-node').TCP.Client;
const client = new Client(3333, '127.0.0.1');

/*var $ = require('jQuery');

Client = require('jsonrpc-node').TCP.Client;
const client = new Client(3333, 'localhost');

let updateDatagrid = function updateDatagrid(defunt)
{
    document.getElementById("nom").innerHTML = defunt.Nom;
    document.getElementById("prenom").innerHTML = defunt.Prenom;
    document.getElementById("datedeces").innerHTML = defunt.DateDeces;
};

let getDefunt = function getDefunt(){
    client.call("GetDefunt", [document.getElementById("defuntID").value],
        function(err, result){if(err) throw err; updateDatagrid(JSON.parse(result))});
};
*/
/*
const populateConfigForm = function populateConfigForm(config) {
    for (k in config)
    {
        $(k).val(config[k]);
    }
};

let rconfig = client.call("GetConfig", [],
    function(err, result){if(err) throw err; populateConfigForm(result); return result; });

$("#configContainer").load("views/configDB.html", complete=function(){
    document.getElementById("saveConfig").onclick = function(){alert("puuuute");}
});
*/

let schema = JSON.parse(`{
    "Nom": {
        "type": "input/text",
        "id": "0000",
        "label": "Nom"
    },
    "Prenom": {
        "type": "input/text",
        "id": "0003",
        "label": "Prénom"
    },
    "CommuneDeces": {
        "type": "list",
        "id": "0001",
        "label": "Commune du décès",
        "data": {
            "Aix-les-Bains": "1000",
            "Pugny-Chatenod": "1001",
            "Chambéry": "1002"
        }
    },
    "DateDeces": {
        "type": "input/date",
        "id": "0002",
        "label": "Date du décès"
    }
}`);

$("#formContainer").html(createFormLayout(schema));

$(document).ready(function() {
    $('.select-chosen').chosen();
});
