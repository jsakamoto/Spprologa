"use strict";
document.body.addEventListener('input', onInput);
function onInput(ev) {
    var srcElement = ev.srcElement;
    if (srcElement === null || srcElement.classList.contains('sudoku-cell') === false)
        return;
    var prevValue = srcElement.dataset.prevValue;
    if (srcElement.value.match(/^[1-4]?$/) === null) {
        srcElement.value = prevValue || '';
    }
    else {
        prevValue = srcElement.value;
    }
    srcElement.dataset.prevValue = prevValue || '';
}
// This sample source codes are distributed under The Unlicense. (https://unlicense.org/)
