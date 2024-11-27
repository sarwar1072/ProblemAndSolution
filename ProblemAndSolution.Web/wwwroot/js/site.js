// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.getElementById("themeToggle").addEventListener("click", function () {
    const body = document.body;
    body.classList.toggle("dark-theme");
    body.classList.toggle("light-theme");
    localStorage.setItem("theme", body.classList.contains("dark-theme") ? "dark-theme" : "light-theme");
});

// Apply saved theme on page load
document.addEventListener("DOMContentLoaded", function () {
    const savedTheme = localStorage.getItem("theme") || "light-theme";
    document.body.classList.add(savedTheme);
});
