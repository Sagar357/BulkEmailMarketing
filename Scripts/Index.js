$('#File3').change(event => {
    var files = event.currentTarget;
    var formData = new FormData();
    formData.append("files", files.files[0]);
    debugger
    $.ajax({
        url: '/File/Upload',
        data: formData,
        method: 'POST',
        contentType: false,
        processData: false,
        async: true,
        success: data => {

            var nameele = $('#File3').val().split('\\');
            $('#camp_logo').attr('src', "../upload/" + data.fileName);
            $('#hiddenLogo').val(data.fileName);
        },
        error: error => {
            alert("error");
        }

    });
});

$('.file-upload').change(event => {
    var files = event.currentTarget;
    var formData = new FormData();
    formData.append("files", files.files[0]);
    $.ajax({
        url: '/File/Upload',
        data: formData,
        method: 'POST',
        contentType: false,
        processData: false,
        async: true,
        success: data => {

            $('#btnfile').attr('data-name', data);
            alert("success");
           
            debugger

        },
        error: error => {
            alert("error");
        }

    });
});
