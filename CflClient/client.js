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
Client = require('jsonrpc-node').TCP.Client;
const client = new Client(3333, '192.168.1.70');

client.call("Ping", [],
    function(err, result){if(err) throw err; console.log(result)});
