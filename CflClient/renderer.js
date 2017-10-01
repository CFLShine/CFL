// This file is required by the index.html file and will
// be executed in the renderer process for that window.
// All of the Node.js APIs are available in this process.
/*
document.getElementById("btnload").onclick = getDefunt;

let populateDatalist = function populateDatalist() {
    client.call("GetDefuntId", [], function(err, result){
        if(err) throw err;
        const jres = JSON.parse(result);
        for (let val in jres.id)
        {
            document.getElementById("defuntID").innerHTML += "\n<option value=\"" + jres.id[val] + "\">" + jres.id[val] + "</option>";
        }
    });
};

populateDatalist();*/