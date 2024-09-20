$(function () {
    $('#blog').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": "/ForPost/Blog/GetBlog",

        "columnDefs": [
            {
                "orderable": false,
                "targets": 3,
                "render": function (data, type, row) {
                    return `<img src=${data} width='50px' />`;
                }
            },
            {
                "orderable": false,
                "targets": 7,
                "width": "20%",
                "render": function (data, type, row) {
                    return `<button type="submit" class="btn btn-info btn-sm mb-2 mb-sm-0 me-0 me-sm-2" onclick="window.location.href='/ForPost/Blog/Edit/${data}'" value='${data}'>
                                                    <i class="fas fa-pencil-alt"></i> Edit</button>
                                               <button type="submit" class="btn btn-danger btn-sm show-bs-modal" href="#" data-id='${data}' value='${data}'>
                                                                                    <i class="fas fa-trash">
                                                                                    </i>
                                                                                    Delete
                                                                                </button>`;
                }
            }
        ]
    });

    $('#blog').on('click', '.show-bs-modal', function (event) {
        var id = $(this).data("id");
        var modal = $("#modal-default");
        modal.find('.modal-body p').text('Are you sure you want to delete this record?')
        //$("#deleteForm").attr("action", "/admin/category/delete/" + id );
        $("#deleteId").val(id);
        modal.modal('show');
    });
    $("#deleteButton").click(function () {
        $("#deleteForm").submit();
    });

});