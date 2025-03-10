// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function changeClass(element, condition, trueClass, falseClass, trueCallback = null, falseCallback = null) {
    if (condition) {
        if (trueCallback)
            trueCallback();
        element.classList.remove(falseClass);
        element.classList.add(trueClass);
    }
    else {
        if (falseCallback)
            falseCallback();
        element.classList.remove(trueClass);
        element.classList.add(falseClass);
    }
}