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
    var ele = event.target.dataset.target;
    var store = event.target.dataset.store;
    debugger
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
            var str = ` <table>
                <thead>
                    <tr>
                        <th>Email To</th>
                         <th>Email Subject</th>
                        <th>Email Body</th>
                    </tr>
                </thead>
                <tbody>`;
            debugger
            var listElements = ``;
            for (var i = 0; i < data.records.length; i++)
            {
                listElements = listElements+ `<tr>`;
                listElements = listElements + `<td>` + data.records[i].to + `</td>`;
                listElements = listElements + `<td>` + data.records[i].subject + `</td>`
                listElements = listElements + `<td>` + data.records[i].emailBody + `</td>`
                listElements = listElements + `</tr>`;
            }
            str = str + listElements + ` </tbody>
               </table>`;
            $(ele).empty();
            $(ele).append(str);
            $(store).val(JSON.stringify(data.records));
        },
        error: error => {
            alert("error");
        }

    });
});
