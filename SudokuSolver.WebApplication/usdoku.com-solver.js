// ! Make sure to select (press) the first cell of the board

const keypressTimer = 100;
async function run(data)
{
    console.table(data);
    // return;
    var lastRow = 0;
    var lastCol = 0;
    for (let pos of data)
    {
        await goSteps(Math.abs(pos.row - lastRow), pos.row - lastRow < 0 ? 'Up' : 'Down');
        await goSteps(Math.abs(pos.col - lastCol), pos.col - lastCol < 0 ? 'Left' : 'Right');
        await setNumber(pos.number);
        lastRow = pos.row;
        lastCol = pos.col;
    }
}

async function goSteps(count, direction)
{
    for (let i = 0; i < count; i++)
    {
        await dispatchKey('Arrow' + direction);
    }
}

async function setNumber(nr)
{
    await dispatchKey('' + nr);
}

async function dispatchKey(key) {
    document.dispatchEvent(new KeyboardEvent('keydown', {
        'key': key
    }));
    await new Promise(r => setTimeout(r, keypressTimer));
}

var game = [...document.querySelectorAll('.w-full div:nth-child(3) div:nth-child(2) > div > div')].map(e => !isNaN(e.innerHTML) ? parseInt(e.innerHTML) : 0).filter(e => !isNaN(e)).join('');
fetch(`http://localhost:5189/?game=${game}`)
    .then(response => response.json())
    .then(async data => await run(data));
