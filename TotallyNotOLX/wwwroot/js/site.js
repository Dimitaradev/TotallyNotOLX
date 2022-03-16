function clearFormInputs(formId) {
    document.querySelectorAll('#' + formId+' input').forEach((e) => {
        if (e.type == "password" || e.type == "text" || e.type == "email") {
            e.value = "";
        }
        else if (e.type == "checkbox") {
            e.checked = false;
        }
        else if (e.type == "file") {
            e.value = null;
        }
    })
}