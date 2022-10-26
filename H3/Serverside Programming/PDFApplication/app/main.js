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

$(document).ready(function() {
    call("GET", "clients").then((response) => {
        response.forEach(i => {
            $('#clientSelector').append(`<option value="${i.id}">${i.username}</option>`)
        })
    });

    $('#clientSelector').on('change', function() {
        console.log(this.value)
    });

});
