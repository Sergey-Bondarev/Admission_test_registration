var temp = null;
tabl.onmouseover = function (event) {
    if (temp)
        return;
    let targ = event.target.closest('tr');
    if (!targ || !tabl.contains(targ))
        return;
    temp = targ;
    targ.style.background = '#39f6ff';
};

tabl.onmouseout = function (event) {
    if (!temp)
        return;
    let reltarg = event.relatedTarget;
    while (reltarg) {
        if (reltarg == temp)
            return;
        reltarg = reltarg.parentNode;
    }
    temp.style.background = '';
    temp = null;
};