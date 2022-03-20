// ! Make sure to select (press) the first cell of the board

const keypressTimer = 1000;
async function run(data)
{
    console.table(data);
    // data.splice(data.length - 1, 1);
    // return;
    var lastRow = 0;
    var lastCol = 0;
    for (let pos of data)
    {
        var cell = pos.row * 9 + pos.col;
        workoncell(cell);
        addnumbertocell(cell, pos.number, false);
        displaycell(cell);
        lastRow = pos.row;
        lastCol = pos.col;
        await new Promise(r => setTimeout(r, keypressTimer));
    }
    checksolution('button');
}

var game = [...document.querySelectorAll('#playtable td')].map(e => parseInt(e.querySelector('span')?.innerHTML) || 0).join('');
fetch(`http://localhost:5189/?game=${game}`)
    .then(response => response.json())
    .then(async data => await run(data));
