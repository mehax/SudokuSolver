/**
 * Created by mehaX on 9/11/2016.
 */

var poss = [
    [
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true]
    ],
    [
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true]
    ],
    [
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true]
    ],
    [
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true]
    ],
    [
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true]
    ],
    [
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true]
    ],
    [
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true]
    ],
    [
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true]
    ],
    [
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true],
        [true, true, true, true, true, true, true, true, true, true]
    ]
]

function ready()
{
    for (var i = 0; i < 9; i++)
        for (var j = 0; j < 9; j++)
            for (var k = 0; k <= 9; k++)
                poss[i][j][k] = false;

}

function update_numbers(numbers)
{
    for (var i = 0; i < 9; i++)
        for (var j = 0; j < 9; j++)
            $('.element[data-row="' + (i + 1) + '"][data-col="' + (j + 1) + '"]').text(numbers[i][j]);
}

function algo0()
{
    var numbers = get_numbers(false);

    for (var i = 0; i < 9; i++)
        for (var j = 0; j < 9; j++)
        {
            var val;
            var count = 0;
            for (var k = 1; k <= 9; k++)
                if (poss[i][j][k])
                {
                    count++;
                    val = k;
                }

            if (count == 1)
                numbers[i][j] = val;
        }

    update_numbers(numbers);
}

function algo1()
{
    var numbers = get_numbers(false);

    for (var i = 0; i < 9; i++)
        for (var j = 0; j < 9; j++)
        {
            var nr = numbers[i][j];
            if (nr != '')
            {
                for (var k = 0; k < 9; k++)
                {
                    if (i != k)
                        poss[k][j][nr] = false;
                    if (j != k)
                        poss[i][k][nr] = false;
                }

            }
        }

    update_numbers(numbers);
}

function algo2()
{
    var numbers = get_numbers(false);



    update_numbers(numbers);
}