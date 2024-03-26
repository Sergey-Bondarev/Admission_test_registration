function DeleteConfirm(obj_mes, deleteUrl) {
    if (confirm("Are you sure you want to delete this " + obj_mes + " ?"))
    {
        window.location.href = deleteUrl;
    }
}