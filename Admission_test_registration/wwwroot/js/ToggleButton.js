function ToggleButton(checkboxId, buttonId) {
    var checkbox = document.getElementById(checkboxId);
    var button = document.getElementById(buttonId);

    button.disabled = !checkbox.checked;
}