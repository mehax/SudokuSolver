// ! Make sure to select (press) the first cell of the board

const keypressTimer = 200;
const maxMoves = 99;

async function run(data) {
    console.table(data);
    // return;
    let lastRow = 0;
    let lastCol = 0;
    let index = 0;
    for (let pos of data) {
        if (index >= maxMoves) {
            break;
        }
        
        await executeStep(lastRow, lastCol, pos);
        lastRow = pos.row;
        lastCol = pos.col;
        index++;
    }
    
    await executeStep(lastRow, lastCol, data[0]);
}

async function executeStep(lastRow, lastCol, pos) {
    await goSteps(Math.abs(pos.row - lastRow), pos.row - lastRow < 0 ? 'Up' : 'Down');
    await goSteps(Math.abs(pos.col - lastCol), pos.col - lastCol < 0 ? 'Left' : 'Right');
    await setNumber(pos.number);
}

async function goSteps(count, direction) {
    for (let i = 0; i < count; i++)
    {
        await dispatchKey('Arrow' + direction);
    }
}

async function setNumber(nr) {
    await dispatchKey('' + nr);
}

async function dispatchKey(key) {
    document.dispatchEvent(new KeyboardEvent('keydown', {
        'key': key
    }));
    await new Promise(r => setTimeout(r, keypressTimer));
}

const game = [...document.querySelectorAll('.w-full div:nth-child(3) div:nth-child(2) > div > div')].map(e => !isNaN(e.innerHTML) ? parseInt(e.innerHTML) : 0).filter(e => !isNaN(e)).join('');
fetch(`http://localhost:5189/?game=${game}`)
    .then(response => response.json())
    .then(async data => await run(data));
