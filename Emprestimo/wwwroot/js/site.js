$(document).ready(function () {
    $('#Emprestimo').DataTable({
        language: {
            "decimal": "",
            "emptyTable": "No data available in table",
            "info": "Mostrando _START_ registro em _END_ de um total de _TOTAL_ Entradas",
            "infoEmpty": "Mostrar 0 to 0 of 0 Entradas",
            "infoFiltered": "(filtered from _MAX_ total entries)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Carregando...",
            "processing": "",
            "search": "Procurar:",
            "zeroRecords": "No matching records found",
            "paginate": {
                "first": "Primeiro",
                "last": "Ultimo",
                "next": "Proximo",
                "previous": "Pagina"
            },
            "aria": {
                "orderable": "Order by this column",
                "orderableReverse": "Reverse order this column"
            }
        }
    });
    setTimeout(function () {
        $('.alert').fadeOut("slow", function () {
            $(this).alert('close');
        })
    },3000);
});

