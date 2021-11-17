/// <summary>
/// Retornar el equivalente de un número en letras
/// </summary>
/// <param name="p_dblValor">double con el número</param>
/// <returns>string con el equivalente del numero en letras</returns>
function ObtenerNumero(p_dblValor, p_verificarUno)
{
    var Num2Text = "";  

    if (p_verificarUno == undefined) p_verificarUno = false;

    if (p_dblValor == 0) Num2Text = "CERO";
    else if (p_dblValor == 1 && !p_verificarUno) Num2Text = "UNO";
    else if (p_dblValor == 1 && p_verificarUno) Num2Text = "UN";
    else if (p_dblValor == 2) Num2Text = "DOS";
    else if (p_dblValor == 3) Num2Text = "TRES";
    else if (p_dblValor == 4) Num2Text = "CUATRO";
    else if (p_dblValor == 5) Num2Text = "CINCO";
    else if (p_dblValor == 6) Num2Text = "SEIS";
    else if (p_dblValor == 7) Num2Text = "SIETE";
    else if (p_dblValor == 8) Num2Text = "OCHO";
    else if (p_dblValor == 9) Num2Text = "NUEVE";
    else if (p_dblValor == 10) Num2Text = "DIEZ";
    else if (p_dblValor == 11) Num2Text = "ONCE";
    else if (p_dblValor == 12) Num2Text = "DOCE";
    else if (p_dblValor == 13) Num2Text = "TRECE";
    else if (p_dblValor == 14) Num2Text = "CATORCE";
    else if (p_dblValor == 15) Num2Text = "QUINCE";
    else if (p_dblValor < 20) Num2Text = "DIECI" + ObtenerNumero(p_dblValor - 10);
    else if (p_dblValor == 20) Num2Text = "VEINTE";
    else if (p_dblValor < 30) Num2Text = "VEINTI" + (p_verificarUno && (p_dblValor - 20) == 1 ? "UN" : ObtenerNumero(p_dblValor - 20));
    else if (p_dblValor == 30) Num2Text = "TREINTA";
    else if (p_dblValor == 40) Num2Text = "CUARENTA";
    else if (p_dblValor == 50) Num2Text = "CINCUENTA";
    else if (p_dblValor == 60) Num2Text = "SESENTA";
    else if (p_dblValor == 70) Num2Text = "SETENTA";
    else if (p_dblValor == 80) Num2Text = "OCHENTA";
    else if (p_dblValor == 90) Num2Text = "NOVENTA";
    else if (p_dblValor < 100) Num2Text = ObtenerNumero(parseInt(p_dblValor / 10) * 10) + " Y " + (p_verificarUno && (p_dblValor % 10) == 1 ? "UN" : ObtenerNumero(p_dblValor % 10));
    else if (p_dblValor == 100) Num2Text = "CIEN";
    else if (p_dblValor < 200) Num2Text = "CIENTO " + (p_verificarUno && (p_dblValor - 100) == 1 ? "UN" : ObtenerNumero(p_dblValor - 100));
    else if ((p_dblValor == 200) || (p_dblValor == 300) || (p_dblValor == 400) || (p_dblValor == 600) || (p_dblValor == 800)) Num2Text = ObtenerNumero(parseInt(p_dblValor / 100)) + "CIENTOS";
    else if (p_dblValor == 500) Num2Text = "QUINIENTOS";
    else if (p_dblValor == 700) Num2Text = "SETECIENTOS";
    else if (p_dblValor == 900) Num2Text = "NOVECIENTOS";
    else if (p_dblValor < 1000) Num2Text = ObtenerNumero(parseInt(p_dblValor / 100) * 100) + " " + ObtenerNumero(p_dblValor % 100, p_verificarUno);
    else if (p_dblValor == 1000 && !p_verificarUno) Num2Text = "MIL";
    else if (p_dblValor == 1000 && p_verificarUno) Num2Text = "UN MIL";
    else if (p_dblValor < 2000) Num2Text = "MIL " + ObtenerNumero(p_dblValor % 1000);
    else if (p_dblValor < 1000000)
    {
        Num2Text = ObtenerNumero(parseInt(p_dblValor / 1000), true) + " MIL";
        if ((p_dblValor % 1000) > 0) Num2Text = Num2Text + " " + ObtenerNumero(p_dblValor % 1000);
    }
    else if (p_dblValor == 1000000) Num2Text = "UN MILLON";
    else if (p_dblValor < 2000000) Num2Text = "UN MILLON " + ObtenerNumero(p_dblValor % 1000000, true);
    else if (p_dblValor < 1000000000000)
    {
        Num2Text = ObtenerNumero(parseInt(p_dblValor / 1000000), true) + " MILLONES ";
        if ((p_dblValor - parseInt(p_dblValor / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + ObtenerNumero(p_dblValor - parseInt(p_dblValor / 1000000) * 1000000, true);
    }

    else if (p_dblValor == 1000000000000) Num2Text = "UN BILLON";
    else if (p_dblValor < 2000000000000) Num2Text = "UN BILLON " + ObtenerNumero(p_dblValor - parseInt(p_dblValor / 1000000000000) * 1000000000000);

    else
    {
        Num2Text = ObtenerNumero(parseInt(p_dblValor / 1000000000000)) + " BILLONES";
        if ((p_dblValor - parseInt(p_dblValor / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + ObtenerNumero(p_dblValor - parseInt(p_dblValor / 1000000000000) * 1000000000000, true);
    }

    

    return Num2Text;
}


function NumeroALetras(campoValor, campoDestino) {
    var valorNumeroTexto = $("#" + campoValor).val();
    var valorNumero = 0;
    var numeroLetras = "";

    if (valorNumeroTexto != "")
    {
        try{
            valorNumero = parseInt(valorNumeroTexto);
            numeroLetras = ObtenerNumero(valorNumero);
        }
        catch (e) { numeroLetras = ""; }
    }

    $("#" + campoDestino).val(numeroLetras);
}


function ValidarValorProyectoAutoliquidacion(p_event)
{
    var objTecla = (document.all) ? p_event.keyCode : p_event.which;
    var objPatron = /[0-9]/;
    var objValorTecla = "";

    //Validar teclas basicas
    if (objTecla == 8 || objTecla == 0 || objTecla == 9)
    {
        return true;
    }

    //Validar que solo se ingrsen numeros
    objValorTecla = String.fromCharCode(objTecla);
    return objPatron.test(objValorTecla);
}