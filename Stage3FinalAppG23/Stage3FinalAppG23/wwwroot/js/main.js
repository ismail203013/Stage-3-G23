// Sliding drop down menu
$(".burger").on("click", function () {
    if (document.getElementById("burger-menu").style.display === "flex") {
        document.getElementById("burger-menu").style.display = "none"; 
    }
    else {
        document.getElementById("burger-menu").style.display = "flex";
    }
});

$(".drop-down-arrow").on("click", function () {
    if (document.getElementsByClassName("select-your-pet-menu")[0].style.display === "flex") {
        document.getElementsByClassName("select-your-pet-menu")[0].style.display = "none";
    }
    else {
        document.getElementsByClassName("select-your-pet-menu")[0].style.display = "flex";
    }
});

if (document.URL.includes("/Using/Account") || document.URL.includes("/Using/Summary")) {
    $(".details-burger-1").on("click", function () {
        $(".account-form-1").slideToggle("fast")
    });
    $(".details-burger-2").on("click", function () {
        $(".account-form-2").slideToggle("fast")
    });
    $(".details-burger-3").on("click", function () {
        $(".account-form-3").slideToggle("fast")
    });
}
// removing disabled tag on inputs so it can be updated in db (disabled tags are avoided in forms)
if (document.URL.includes("/Using/Account")) {
    $(".continue-button").on("click", function () {
        $("input").removeAttr("disabled");
    });
}

// Receipt Uploader
function readURL(input) {
    var reader = new FileReader();
    reader.onload = function (e) {
        document.getElementById("Img").setAttribute("src", e.target.result);
    };
    reader.readAsDataURL(input.files[0]);
};
// removing things on a specific page
if (document.URL.includes("/Using/LogIn") || document.URL.includes("/Using/SignUp") || document.URL.includes("/Using/Account") || document.URL.includes("/SignOut")) {
    document.getElementById("affiliates").style.display = "none";
}
if (document.URL.includes("/Using/UploadReceipt")) {
    if (document.getElementById("Img").getAttribute('src') == "" || document.getElementById("Img").getAttribute('src') == unknown) {
        document.getElementById("uploaded-files").style.display = "none";
        document.getElementById("your-files").style.display = "none";
        document.getElementById("your-files").style.display = "none";
        document.getElementById("Img").style.display = "none";
    };
    // making things visible again on click
    $(".file-uploader-input").on("click", function () {
        document.getElementById("uploaded-files").style.display = "flex";
        document.getElementById("your-files").style.display = "block";
        document.getElementById("your-files").style.display = "block";
        document.getElementById("Img").style.display = "block";
    });
}
// making progress bar move 
if (document.URL.includes("/Using/Clinic")) {
    document.getElementById("progress-bar").setAttribute("style", "width:50%");
}
else if (document.URL.includes("/Using/UploadReceipt")) {
    document.getElementById("progress-bar").setAttribute("style", "width:30%");
}
else if (document.URL.includes("/Using/Details")) {
    document.getElementById("progress-bar").setAttribute("style", "width:60%");
}
else if (document.URL.includes("/Using/PetDetails")) {
    document.getElementById("progress-bar").setAttribute("style", "width:70%");
}
else if (document.URL.includes("/Using/Summary")) {
    document.getElementById("progress-bar").setAttribute("style", "width:90%");
}
if (document.URL.includes("/Using/SignUp")) {
    document.getElementsByClassName("short-input-fields")[0].style.margin = "0px 0px 10px 0px";
}
if (document.URL.includes("/Using/RebateChoice")) {
    document.getElementById("progress-bar").setAttribute("style", "width:80%");
}
