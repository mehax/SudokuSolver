var KEY_UP          = 38;
var KEY_DOWN        = 40;
var KEY_LEFT        = 37;
var KEY_RIGHT       = 39;
var KEY_EDIT_MODE   = 69; // e

var edit_mode   = false;

function highlight(val)
{
    $('.element').each(function(){
        $(this).removeClass('highlighted');
        if (val != '' && $(this).text() == val)
            $(this).addClass('highlighted');
    });
}

function select(element, next)
{
    element.removeClass('selected');
    next.addClass('selected');

    var val = next.text();
    highlight(val);
}

function selectNext(element)
{
    var row     = element.data('row');
    var col     = element.data('col');

    if (col == 9)
    {
        if (row == 9)
            row = 0;
        col = 0;
        row++;
    }
    col++;

    var next = $('.element[data-row="'+row+'"][data-col="'+col+'"]');
    select(element, next);
}

function get_numbers(locked)
{
    var rows = [];
    $('.sudoku-row').each(function(){
        var cols = [];
        $(this).find('.element').each(function(){
            var nr = $(this).text();
            if (locked)
                cols.push({locked: $(this).hasClass('locked'), value: nr});
            else
                cols.push(nr);
        });
        rows.push(cols);
    });
    return rows;
}


$(document)

.on('keydown', 'html', function(e){
    var element = $('.selected');
    var row     = element.data('row');
    var col     = element.data('col');

    var key     = e.keyCode;

    var next    = null;

    if (key == KEY_LEFT)
    {
        if (col == 1)
            next = $('.element[data-row="'+row+'"][data-col="'+9+'"]');
        else
            next = element.prev('.element');
    }
    else if (key == KEY_RIGHT)
    {
        if (col == 9)
            next = $('.element[data-row="'+row+'"][data-col="'+1+'"]');
        else
            next = element.next('.element');
    }
    else if (key == KEY_UP)
    {
        if (row == 1)
            next = $('.element[data-row="'+9+'"][data-col="'+col+'"]');
        else
            next = $('.element[data-row="'+(row-1)+'"][data-col="'+col+'"]')
    }
    else if (key == KEY_DOWN)
    {
        if (row == 9)
            next = $('.element[data-row="'+1+'"][data-col="'+col+'"]');
        else
            next = $('.element[data-row="'+(row+1)+'"][data-col="'+col+'"]');
    }
    else if (key == KEY_EDIT_MODE)
    {
        edit_mode = !edit_mode;
        $('.edit-mode-js').text(edit_mode ? 'true' : 'false');
        $('.edit-mode-js').removeClass('text-success');
        $('.edit-mode-js').removeClass('text-danger');
        $('.edit-mode-js').addClass(edit_mode ? 'text-danger' : 'text-success');
    }
    else if (key >= 48 && key <= 58)
    {
        var nr = key - 48;
        if (nr == 0)
        {
            element.text('');
            if (!edit_mode)
                selectNext(element);
            else
                element.removeClass('locked');
        }
        else if (edit_mode || element.hasClass('locked') == false)
        {
            element.text(nr);
            selectNext(element);

            if (edit_mode && element.hasClass('locked') == false)
                element.addClass('locked');
        }
    }

    if (next != null)
        select(element, next);
})

.on('click', '.element', function(){
    select($('.selected'), $(this));
})

.on('click', '.insert-js', function(){
    var rows = get_numbers(true);

    $.ajax({
        type: 'post',
        url: path + 'insert',
        dataType: 'json',
        data: {value: rows},
        success: function(html){
            $('.list-group').append('<button class="list-group-item" id="'+html.id+'">'+html.date+'</button>')
        }
    });
})

.on('click', '.list-group-item', function(){
    var id = $(this).attr('id');

    $.ajax({
        type: 'post',
        url: path + 'get_sudoku',
        dataType: 'json',
        data: {sudoku_id: id},
        success: function(html){
            for (var i = 0; i < 9; i++)
                for (var j = 0; j < 9; j++) {
                    var element = $('.element[data-row="' + (i + 1) + '"][data-col="' + (j + 1) + '"]');
                    element.removeClass('locked');
                    element.text(html.value[i][j].value);
                    if (html.value[i][j].locked == 'true')
                        element.addClass('locked');
                }

            edit_mode = false;
            $('.edit-mode-js').text('false');
            $('.edit-mode-js').removeClass('text-danger');
            $('.edit-mode-js').addClass('text-success');
        }
    })
})