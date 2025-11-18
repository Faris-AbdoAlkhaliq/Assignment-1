console.log("EyeTune project loaded successfully.");

function changeMessage() {
  let msg = document.getElementById("dynamicText");
  msg.innerText = "EyeTune recommends taking a break now!";
  msg.style.color = "blue";
}

function darkModeToggle() {
  let body = document.body;
  body.style.background = "#3b2f50ff";
  body.style.color = "white";
}
