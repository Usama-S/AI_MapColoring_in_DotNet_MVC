function SetColors() {
    var colorsCount = document.getElementById('colorCount').value;

    var colorsForm = document.getElementById('colorsForm');

    colorsForm.innerHTML = '';

    for (var i = 0; i < colorsCount; i++) {
        colorsForm.innerHTML += 'Color' + (i + 1) + ': <input class="colorPicker" type="color" /><br/><br/>';
    }
}

function FillColors() {
    // get all colors in array
    var colors = [];

    // get all color input elements
    var colorsInputs = document.getElementsByClassName('colorPicker');

    var colorsCount = colorsInputs.length

    // add input colors to the colors array
    for (var i = 0; i < colorsCount; i++) {
        colors.push(colorsInputs[i].value);
    }

    // get mapId
    var mapId = document.getElementById('mapId').value;

    // get all maps edges

    // send to ajax
    var url = '/api/getFillColors/' + mapId + '/' + colorsCount;

    var xhr = new XMLHttpRequest;

    xhr.open("get", url);

    xhr.onload = function () {
        // onload code here...

        // if solution was true
            // iterate over map edges
            // assign background color to edges on the same position in colors array
        // else 
            // change background of all provinces to white
            // display solution not possible

        var response = JSON.parse(xhr.response);

        if (response.result) {
            var solutionColors = response.colors.split(',');

            document.getElementById('noSolution').style.display = 'none';

            for (var i = 0; i < solutionColors.length; i++) {
                var elemId = 'prov' + i;

                document.getElementById(elemId).style.backgroundColor = colors[solutionColors[i] - 1];
            }
        }
        else {
            document.getElementById('noSolution').style.display = 'block';
            var provinces = document.getElementsByClassName('province');

            for (var i = 0; i < provinces.length; i++) {
                provinces[i].style.backgroundColor = 'white';
            }
        }
    }
    xhr.send();
}



