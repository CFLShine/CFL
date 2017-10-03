/*
Schema example

{
    "Nom" {
        "type": "text",
        "default": "Jean Paul",
        "label": "Nom",
        "id" = Guid1
    },

    "dateDeces": {
        "type": "text",
        "label": "Date de Décès",
        "id" = Guid2
    },

    "communeDeces": {
        "type": "list",
        "label": "Commune de Décès",
        "id" = "Guid3",
        "data": {
            "Aix-les-Bains": "Guid4",
            "Pugny-Chatenod": "Guid5"
        }
    }
}

 */

const createList = function (id, label, data) {
    html = "<label for='" + id + "'>" + label + "</label>";
    html += "<select class='select-chosen' id='" + id + "'>";
    for (key in data)
    {
        html += "<option id='" + data[key] + "' value='" + key + "'>" + key + "</option>";
    }
    html += "</select>";
    return html;
};

const createInput = function(id, label, type) {
    return "<label for='" + id + "'>" + label + "</label><input type='" + type + "' id='" + id + "'>";
};

const createFormLayout = function(schema) {
    let callback = {
        "input": createInput,
        "list" : createList
    };

    let html = "<form class='form-layout'><ul>";

    for (key in schema)
    {
        let obj = schema[key];
        let type = obj.type.split("/"); //type[0] = category, type[1] = specification
        switch(type[0]) {
            case "input":
                html += "<li>" + createInput(obj.id, obj.label, type[1]) + "</li>";
                break;
            case "list":
                html += "<li class='form-layout-list'>" + createList(obj.id, obj.label, obj.data) + "</li>";
                break;
            default:
                console.log("FormLayout: Unrecognized type " + type[0]);
                break;
        }
    }

    html += "</ul></html>";

    return html;
};