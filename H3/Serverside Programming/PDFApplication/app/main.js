let state = {
    data: null,

    client: null,
    document: null,

    async init(){
        await call("GET", "clients").then(response => state.data = response)
        populateClients();
    },

    updateClient(id){
        for (const client of state.data) {
            if (client.id == id) {
                this.client = client;
            }
        }
    },
    updateDocument(id){
        for (const document of state.client.documents){
            if (document.id == id){
                this.document = document;
            }
        }
    },
    removeDocument(id) {
        let index = -1;
        for (let i = 0; i < this.client.documents.length; i++) {
            const document = this.client.documents[i];
            if (document.id == id) {
                index = i;
            }
        }

        if (index > -1) {
            this.client.documents.splice(index, 1);
        }
    }
}

function call(type, endpoint, data = {}) {
    url = "https://localhost:7027/api/" + endpoint;

    return new Promise((resolve, reject) => {
        $.ajax({
            type: type,
            url: url,
            contentType: "application/json",
            headers: { "Content-Type": "application/json" },
            data: JSON.stringify(data),
            success: (response) => resolve(response),
            error: (response) => reject(response),
        });
    });
}

function populateClients() {
    if (state.data) {
        $('#clientSelector').empty()
        state.data.forEach(i => {
            $('#clientSelector').append(`<option value="${i.id}">${i.username}</option>`)
        })

        if (!state.client) {
            state.updateClient($("#clientSelector").val());
            populateDocuments();
        }
    }
}

function populateDocuments(){
    if (state.client){
        $('#documentSelector').empty()
        state.client.documents.forEach(i => {
            $('#documentSelector').append(`<option value="${i.id}">${i.title}</option>`)
        })

        if (!state.document){
            const selectedDocument = $("#documentSelector").val()

            if (selectedDocument) {
                state.updateDocument(selectedDocument);
                $("#pdfViewer").attr("data", "data:application/pdf;base64," + state.document.encodedData);
            } else {
                $("#pdfViewer").attr("data", "");
            }
        } 
    }
}


$(document).ready(async function() {

    await state.init()

    $('#clientSelector').on('change', function() {
        state.updateClient(this.value)
        state.document = null;
        populateDocuments();
    });

    $('#documentSelector').on('change', function() {
        state.updateDocument($("#documentSelector").val())
        $("#pdfViewer").attr("data", "data:application/pdf;base64," + state.document.encodedData);
    });


    $("#deleteDocumentBtn").click(function(){
        call("DELETE", `documents/${state.document.id}`).then(_ => {
            state.removeDocument(state.document.id)
            state.document = null;
            populateDocuments();
        })
        
    });

    $("#documentForm").submit(function(e){
        var values = {};
        $.each($('#documentForm').serializeArray(), function(i, field) {
            console.log(i)
            values[field.name] = field.value;
        });

        call("POST", "documents", {
            clientId: state.client.id,
            title: values.title,
            attributes: `title=${values.title}$description=${values.description}`
            
        }).then(response => {
            console.log(state)
            state.client.documents.push(response)
            populateDocuments();
        })

        $("#title").val("");
        $("#description").val("");

        e.preventDefault();

    });
});