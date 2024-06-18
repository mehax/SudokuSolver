// ! Make sure to select (press) the first cell of the board

const keypressTimer = 100;
const maxMoves = 99;

function getRowNumbers(grid, index) {
    const rowNumber = Math.floor(index / 9);
    const startIndex = rowNumber * 9;
    return grid.slice(startIndex, startIndex + 9);
}
function getColNumbers(grid, index) {
    const colNumber = index % 9;
    const col = [];
    for (let i = colNumber; i < 81; i += 9) {
        col.push(grid[i]);
    }
    return col;
}
function getBlockNumbers(grid, index) {
    const blockNumber = Math.floor(index / 27) * 3 + Math.floor((index % 9) / 3);
    const startRowIndex = Math.floor(blockNumber / 3) * 27;
    const startColIndex = (blockNumber % 3) * 3;
    const block = [];
    for (let i = 0; i < 3; i++) {
        for (let j = 0; j < 3; j++) {
            block.push(grid[startRowIndex + i * 9 + startColIndex + j]);
        }
    }
    return block;
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

async function run() {
    const numbers = [...document.querySelectorAll('.w-full div:nth-child(3) div:nth-child(2) > div > div')].map(e => !isNaN(e.innerHTML) ? parseInt(e.innerHTML) : 0).filter(e => !isNaN(e));

    let index = 0;
    for (const number of numbers)
    {
        if (number != '0')
        {
            index++;
            continue;
        }

        document.querySelectorAll('.w-full+div:nth-child(2) > div')[index].click();

        const blockedNumbers = [... new Set(getRowNumbers(numbers, index).concat(getColNumbers(numbers, index)).concat(getBlockNumbers(numbers, index)))].filter(nr => nr !== 0);

        for (const nr of [1, 2, 3, 4, 5, 6, 7, 8, 9])
        {
            if (blockedNumbers.includes(nr))
            {
                continue;
            }

            await setNumber(nr);
        }

        index++;
    }

}

await run();
