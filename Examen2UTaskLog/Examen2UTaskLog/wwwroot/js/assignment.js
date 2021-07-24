
function assignment_search() {
    const search = document.getElementById("txt-search").value;
    const currentURL = window.location.href.substring(
        window.location.href.lastIndexOf('/') + 1
    ).split("?")[0];

    window.location = `${currentURL}?search=${search}`;
}

const assignments_input_search = document.getElementById("txt-search");

assignments_input_search.addEventListener("keyup", function (event) {
    if (event.keyCode == 13) {
        event.preventDefault();
        assignment_search();
    }
});