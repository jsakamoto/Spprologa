document.body.addEventListener('input', onInput);

function onInput(ev: Event): void {
    const srcElement = ev.srcElement as HTMLInputElement | null;
    if (srcElement === null || srcElement.classList.contains('sudoku-cell') === false) return;
    let prevValue = srcElement.dataset.prevValue as string | undefined;
    if (srcElement.value.match(/^[1-4]?$/) === null) {
        srcElement.value = prevValue || '';
    }
    else {
        prevValue = srcElement.value;
    }
    srcElement.dataset.prevValue = prevValue || '';
}

// This sample source codes are distributed under The Unlicense. (https://unlicense.org/)