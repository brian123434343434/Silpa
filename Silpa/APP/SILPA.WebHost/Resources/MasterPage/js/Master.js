$(document).ready(function () {

    $('#alto-contraste').click(function () {

        if ($('html').hasClass('html-inverse')) {
            $('html').removeClass('html-inverse');
        }
        else {
            $('html').addClass('html-inverse');
        }
    });

    $('a, p, h1, h2, h3, h4, h5, input, label, td').jfontsize({
        btnMinusClasseId: '#letra-pequena',
        btnPlusClasseId: '#letra-grande',
        btnMinusMaxHits: 5,
        btnPlusMaxHits: 5,
        sizeChange: 1
    });

});