var DeleteCustomer = function (id) {
    Swal.fire({
        title: 'Do you want to delete this item?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "DELETE",
                url: "Customer/DeleteConfirmed/" + id,
                success: function (result) {
                    var message = "Customer has been deleted successfully.";
                    Swal.fire({
                        title: message,
                        icon: 'info',
                        onAfterClose: () => {
                            location.reload();
                        }
                    });
                }
            });
        }
    });
};