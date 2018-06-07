//запрос на разделы
function GetSections() {
    var request = new XMLHttpRequest();
    request.open("GET", "/api/OnlySection/", false);
    request.send();
    return request;
}

function LoadSections() {
    var myObj, i, j, x, x2 = "";
    var request = GetSections();

    myObj = JSON.parse(request.responseText);
    var x = "";
    var name = "", photo = "", info="",id="";
    for (i in myObj) {
        id = myObj[i].sectionId;
        name = myObj[i].sectionName;
        photo = myObj[i].sectionImage;
        info = myObj[i].sectionInformation;

        x += '<div class="item"><img  class="leftimg" src="' + photo + '"><h2>' + name + '</h2><br>' + info + '<a href="Books.html#'+id+'#"><br><br>Перейти к списку книг »</a><br><br></div>';
        document.getElementById("Sections").innerHTML = x;  
    }
}

function DeleteSection() {
    var request = new XMLHttpRequest();
    select = document.getElementById("deleteSelecton");
    id = select.options[select.selectedIndex].value;
    url = "/api/OnlySection/" + id;
    request.open("DELETE", url, false);
    request.send();
}

function CreateSection() {
    urlText = document.getElementById("createSection").value;

    var xmlhttp = new XMLHttpRequest();   // new HttpRequest instance
    xmlhttp.open("POST", "/api/OnlySection/");
    xmlhttp.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    xmlhttp.send(JSON.stringify({ url: urlText }));
}

