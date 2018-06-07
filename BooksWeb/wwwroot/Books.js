function getValue(s) {
    var a = s.split('#')
    if (a.length >= 3) return a[1];
}

function LoadBooks() {
    alert('test');
    var myObj, i, j, x, x2;
    var request = new XMLHttpRequest();
    var pageHref = location;
    var stroka = pageHref.toString();
    var str = getValue(stroka);

    url = "api/Sections/" + str;
    request.open("GET", url, false);
    request.send();

    myObj = JSON.parse(request.responseText);
    var x = "";
    alert('test');
    for (i in myObj) {
        x += '<h3>' + myObj.bookId + '</h3>';
        document.getElementById("bookList").innerHTML = x;
    }
}