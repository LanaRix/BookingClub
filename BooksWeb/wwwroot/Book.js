//запрос на книги
function GetBooks() {
    var request = new XMLHttpRequest();
    request.open("GET", "/api/OnlyBook/", false);
    request.send();
    return request;
}

function GetBook(id) {
    //alert("test");
    var request = new XMLHttpRequest();
    request.open("GET", "/api/OnlyBook/" + id, false);
    request.send();
    return request;
}

function LoadBooks() {
    var myObj, i, j, x, x2 = "";
    var request = GetBooks();

    myObj = JSON.parse(request.responseText);
    var x = "";
    var name = "", photo = "", info = "", id = "",author="",year="";
    for (i in myObj) {
        id = myObj[i].bookId;
        name = myObj[i].bookName;
        photo = myObj[i].bookImage;
        info = myObj[i].bookInformation;
        author = myObj[i].bookAuthor;
        year = myObj[i].bookYear;
        //x += '<div class="item"><img  class="leftimg" src="' + photo + '"><h2>' + name + '</h2><br>' + info + '<br>' + author + '<br>' + year + '<br><input type="button" value="Изменить" onclick="Edit('+id+')"/><input type="button" value="Удалить" onclick="DeleteBook(' + id + ')"/></div>';
        x += '<div class="item"><img  class="leftimg" src="' + photo + '"><h2>' + name + '</h2><br>' + info + '<br>' + author + '<br>' + year + '<br><a href="#edit"><input style="margin:20px 20px 20px 20px" type="submit" class="btn btn - dark" value="Изменить" onclick="disp(' + id + ',form1)"/></a><input type="submit" class="btn btn - dark" value="Удалить" onclick="DeleteBook(' + id + ')"/></div>';
        document.getElementById("Books").innerHTML = x;
    }
}

function DeleteBook(id) {
    var request = new XMLHttpRequest();
    request.onload = function (ev) {
        var msg = ""
        if (request.status == 401) {
            msg = "У вас не хватает прав для удаления";
        } else if (request.status == 200) {
            msg = "Запись удалена";
        } else {
            msg = "Неизвестная ошибка";
        }
        document.getElementById("msgDel").innerHTML = msg;
    }
    //select = document.getElementById("deleteBook");
    //id = select.options[select.selectedIndex].value;
    url = "/api/OnlyBook/" + id;
    request.open("DELETE", url, false);
    request.send();
    window.location.reload();
    //GetBooks();
}

//добавление новой книги
function CreateBooks() {
    //alert("test");
    urlName = document.getElementById("createName").value;
    urlAuthor = document.getElementById("createAuthor").value;
    urlInformation = document.getElementById("createInfo").value;
    urlImage = document.getElementById("createImage").value;
    urlYear = document.getElementById("createYear").value;
    var xmlhttp = new XMLHttpRequest();
   /* xmlhttp.onload = function (ev) {
        var msg = ""
        if (xmlhttp.status == 401) {
            msg = "У вас не хватает прав для создания";
        } else if (xmlhttp.status == 200) {
            msg = "Запись создана";
        } else {
            msg = "Неизвестная ошибка";
        }
        document.getElementById("msgCreate").innerHTML = msg;
    }*/
    xmlhttp.open("POST", "/api/OnlyBook/");
    xmlhttp.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    xmlhttp.send(JSON.stringify({ bookName: urlName, bookAuthor: urlAuthor, bookInformation: urlInformation, bookImage: urlImage, bookYear: urlYear }));
}

// Изменение книги
function EditBook(id) {
    urlName = document.getElementById("editName").value;
    urlAuthor = document.getElementById("editAuthor").value;
    urlInformation = document.getElementById("editInfo").value;
    urlImage = document.getElementById("editImage").value;
    urlYear = document.getElementById("editYear").value;

    var xmlhttp = new XMLHttpRequest();
    xmlhttp.onload = function (ev) {
        var msg = ""
        if (xmlhttp.status == 401) {
            msg = "У вас не хватает прав для редактирования";
        } else if (xmlhttp.status == 200) {
            msg = "Запись создана";
        } else {
            msg = "Неизвестная ошибка";
        }
        document.getElementById("msgCreate").innerHTML = msg;
    }
    url1 = "/api/OnlyBook/" + id;
    xmlhttp.open("PUT", url1);
    xmlhttp.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    xmlhttp.send(JSON.stringify({ bookId: id, bookName: urlName, bookAuthor: urlAuthor, bookInformation: urlInformation, bookImage: urlImage, bookYear: urlYear }));
}

//для входа
function dispLongIn(longIn) {
    GetCurrentUser();
    if (longIn.style.display === "none") {
        longIn.style.display = "block";
    }
   /* else {
        longIn.style.display = "none";
    }*/
}

//для формы
function disp(id,form) {
  //  alert(id);
    if (form.style.display === "none") {
        var myObj;
        var request = GetBook(id);
        myObj = JSON.parse(request.responseText);
        var name = "", author = "", info = "", photo = "", year = "";
        name = myObj.bookName;
       // alert(name);
        author = myObj.bookAuthor;
       // alert(author);
        info = myObj.bookInformation;
        //alert(info);
        photo = myObj.bookImage;
       // alert(photo);
        year = myObj.bookYear;
       // alert(year);
       // alert("test");
        editId.value = id;
        editName.value = name;
        editAuthor.value = author;
        editInfo.value = info;
        editImage.value = photo;
        editYear.value = year;
        form.style.display = "block";
    }
    else {
        form.style.display = "none";
    }
}



function ParseResponseMsg() {
    // Считывание данных с формы
    email = document.getElementById("Email").value;
    password = document.getElementById("Password").value;
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.open("POST", "/api/Account/Login");
    xmlhttp.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    xmlhttp.onreadystatechange = function () {
            // Очистка контейнера вывода сообщений
            document.getElementById("msg").innerHTML = ""
            var mydiv = document.getElementById('formError');
            while (mydiv.firstChild) {
                mydiv.removeChild(mydiv.firstChild);
            }
            // Обработка ответа от сервера
            myObj = JSON.parse(this.responseText);
            document.getElementById("msg").innerHTML = myObj.message;
            // Вывод сообщений об ошибках
            if (typeof myObj.error !== "undefined" && myObj.error.length > 0) {
                for (var i = 0; i < myObj.error.length; i++) {
                    var ul = document.getElementsByTagName("ul");
                    var li = document.createElement("li");
                    li.appendChild(document.createTextNode(myObj.error[i]));
                    ul[0].appendChild(li);
                }
            }
            document.getElementById("Password").value = "";
        };
    // Запрос на сервер
    xmlhttp.send(JSON.stringify({
        email: email,
        password: password
    }));
};
// Обработка клика по кнопке
document.getElementById("loginBtn").addEventListener("click",
    ParseResponseMsg);




function GetCurrentUser() {
    //alert("text");
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.open("POST", "/api/Account/isAuthenticated", true);
    xmlhttp.onreadystatechange = function () {
        var myObj = "";
        myObj = xmlhttp.responseText != "" ? JSON.parse(xmlhttp.responseText) :
            {};
       // alert("text");
        document.getElementById("msgAuth").innerHTML = myObj.message;
    }
    xmlhttp.send();
}
