function displayError(elementId, errorMessage) {
    var errorElement = document.getElementById(elementId);
    errorElement.style.display = "flex";
    errorElement.style.flexDirection = "column";
    errorElement.style.textAlign = "left";
    errorElement.style.paddingBottom = "15px";
    errorElement.innerHTML = errorMessage;
}
function validateRegisterForm() {
    const fname = document.getElementById('firstname');
    const lname = document.getElementById('lastname');
    const PhoneNum = document.getElementById('phone');
    const Email = document.getElementById('email');
    const Password = document.getElementById('password');
    const ManagerName = document.getElementById('managerName');

    if (fname.value == "") {
        displayError("fnameError", "First Name is required");
        return false;
    } else {
        fname.style.display = "none";
    }
    if (lname.value == "") {
        displayError("lnameError", "Last Name is required");
        return false;
    } else {
        lname.style.display = "none";
    }
    if (PhoneNum.value == "") {
        displayError("phoneError", "First Name is required");
        return false;
    } else {
        PhoneNum.style.display = "none";
    }
    if (Email.value == "") {
        displayError("emailError", "Phone Number should start with 5 and followed by 6 numeric number");
        return false;
    } else {
        Email.style.display = "none";
    }
    if (Password.value == "") {
        displayError("passwordError", "First Name is required");
        return false;
    } else {
        Password.style.display = "none";
    }
    if (ManagerName.value == "") {
        displayError("managerError", "First Name is required");
        return false;
    } else {
        ManagerName.style.display = "none";
    }
   
    return true;
}
function validateEmail(email) {
    var regex = /^[^\s@@]+@@[^\s@@]+\.[^\s@@]+$/;
    return regex.test(email);
}
function validatePhoneNuumber(phoneNum) {
    var Regex = /^5\d{6}$/;
    return Regex.test(phoneNum);
}