const API = "http://localhost:5000";

function Error(){
 const Error = document.querySelector(".error");
 Error.style.display = "block";
 setTimeout(() => {
    Error.style.display = "none";
 }, 3000);
}

async function getData(e) {
    try {
    e.preventDefault();
    const response = await fetch(`${API}/getData`);
    const data = await response.json();
    return data;
    } catch (error) {
        return Error();
    }
}