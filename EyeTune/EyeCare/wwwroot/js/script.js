console.log("EyeTune project loaded successfully.");

document.addEventListener("DOMContentLoaded", function () {
    const savedTheme = localStorage.getItem("theme");
    if (savedTheme === "dark") {
        document.body.classList.add("dark-mode");
    }
});

function darkModeToggle() {
    let body = document.body;
    // 2. Toggle the class
    body.classList.toggle("dark-mode");

    // 3. Save the choice
    if (body.classList.contains("dark-mode")) {
        localStorage.setItem("theme", "dark");
    } else {
        localStorage.setItem("theme", "light");
    }
}
// Displaying tips in the console as loop 
let tips = ["Blink often", "Follow 20-20-20 rule", "Adjust brightness"];

for (let i = 0; i < tips.length; i++) {
  console.log("Tip: " + tips[i]);
}

function changeMessage() {
  let msg = document.getElementById("dynamicText");
  msg.innerText = "EyeTune recommends taking a break now!";
  msg.style.color = "blue";
}

function checkEyeCondition() {
  let hours = document.getElementById("hoursInput").value;
  let img = document.getElementById("conditionImg");
  let text = document.getElementById("conditionText");

  // if empty or invalid input
  if (hours === "" || hours < 0) {
    text.innerText = "Please enter a valid number of hours.";
    img.style.display = "none";
    return;
  }

  // Normal Condition
  if (hours <= 3) {
    text.innerText =
      "Normal Condition: Your eyes are in a healthy state. Keep taking short breaks!";
    img.src = "images/normal1.jpg";
    img.style.display = "block";
  }

  // Mild Condition
  else if (hours > 3 && hours <= 6) {
    text.innerText =
      "Mild Eye Strain: Mild strain detected. Reduce brightness and blink more often.";
    img.src = "images/mild1.jpeg";
    img.style.display = "block";
  }

  // Severe Condition
  else {
    text.innerText =
      "Severe Eye Strain: Take a break now and rest your eyes immediately!";
    img.src = "images/strain1.jpeg";
    img.style.display = "block";
  }
}

document.querySelectorAll(".expandable-section").forEach((section) => {
  // mouse over event
  section.addEventListener("mouseover", function () {
    section.style.backgroundColor = "#f0f0f0";
  });

  // mouse leave event
  section.addEventListener("mouseout", function () {
    section.style.backgroundColor = "white";
  });
});

// Password strength checker
document.getElementById("password").addEventListener("keyup", function () {
  let strength = document.getElementById("passStrength");

  if (this.value.length < 4) {
    strength.innerText = "Weak Password";
    strength.style.color = "red";
  } else if (this.value.length < 8) {
    strength.innerText = "Medium Strength";
    strength.style.color = "orange";
  } else {
    strength.innerText = "Strong Password";
    strength.style.color = "green";
  }
});

// LOGIN VALIDATION
document
  .getElementById("loginForm")
  .addEventListener("submit", function (event) {
    let email = document.getElementById("loginEmail").value.trim();
    let pass = document.getElementById("loginPassword").value.trim();
    let error = document.getElementById("loginError");

    error.innerText = ""; // reset

    if (email === "" || pass === "") {
      error.innerText = "All fields are required!";
      event.preventDefault();
      return;
    }

    if (!email.includes("@") || !email.includes(".")) {
      error.innerText = "Invalid email format!";
      event.preventDefault();
      return;
    }

    if (pass.length < 6) {
      error.innerText = "Password must be at least 6 characters.";
      event.preventDefault();
      return;
    }

    alert("Login successful!");
  });


