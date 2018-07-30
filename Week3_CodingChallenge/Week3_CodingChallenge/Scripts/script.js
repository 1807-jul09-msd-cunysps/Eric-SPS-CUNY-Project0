var requestURL = 'https://mdn.github.io/learning-area/javascript/oojs/json/superheroes.json';
var request = new XMLHttpRequest();
request.open('GET', requestURL);

request.responseType = 'json';
request.send();

request.onload = function () {

    var superHeroes = request.response;

    populateHeader(superHeroes);

    showHeroes(superHeroes);

}

function populateHeader(jsonHeroes) {
    var myH1 = document.createElement('h1');
    myH1.textContent = jsonHeroes['squadName'];
    header.appendChild(myH1);

    var myPara = document.createElement('p');
    myPara.textContent = 'Hometown: ' + jsonHeroes['homeTown'] + ' // Formed: ' + jsonHeroes['formed'];
    header.appendChild(myPara);
}

function showHeroes(jsonHeroes) {
    var members = jsonHeroes['members'];
    for (var i = 0; i < members.length; i++) {
        var memberArticle = document.createElement('article');
        var memberName = document.createElement('h2');
        var identity = document.createElement('p');
        var age = document.createElement('p');
        var powers = document.createElement('p');
        var powerList = document.createElement('ul');

        var powerDetails = members[i].powers;

        memberName.innerText = members[i].name;
        identity.innerText = 'Secret identity: ' + members[i].secretIdentity;
        age.innerText = 'Age: ' + members[i].age;
        powers.innerText = 'Powers: ';
        for (var j = 0; j < powerDetails.length; j++) {
            var listItem = document.createElement('li');
            listItem.innerText = powerDetails[j];
            powerList.appendChild(listItem);
        }

        memberArticle.appendChild(memberName);
        memberArticle.appendChild(identity);
        memberArticle.appendChild(age);
        memberArticle.appendChild(powers);
        memberArticle.appendChild(powerList);

        section.appendChild(memberArticle);
    }
}