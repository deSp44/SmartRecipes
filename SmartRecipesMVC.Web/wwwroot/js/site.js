// DROPDOWN TOGGLE
$(document).ready(function () {
    $(".dropdown-toggle").dropdown();
});


// SCROLL TOP BUTTON
var mybutton = document.getElementById("btn-back-to-top");

window.onscroll = function () {
    scrollFunction();
};

function scrollFunction() {
    if (
        document.body.scrollTop > 20 ||
        document.documentElement.scrollTop > 20
    ) {
        mybutton.style.display = "block";
    } else {
        mybutton.style.display = "none";
    }
}

mybutton.addEventListener("click", backToTop);

function backToTop() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}

// FACEBOOK CALLBACK
if (window.location.hash && window.location.hash == '#_=_') {
    window.location.hash = '';
}