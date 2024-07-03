function toggleNav() {
    var sidebar = document.getElementById("sidebarMenu");
    var overlay = document.getElementById("overlay");
    var openbtn = document.querySelector(".openbtn");

    if (sidebar.classList.contains("active")) {
        sidebar.classList.remove("active");
        overlay.classList.remove("active");
        openbtn.style.opacity = "1";
    } else {
        sidebar.classList.add("active");
        overlay.classList.add("active");
        openbtn.style.opacity = "0";
    }
}