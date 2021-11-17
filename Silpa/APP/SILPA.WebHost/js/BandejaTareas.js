function abrirFormulario( url) {
   //window.showModalDialog(url, null, 'scrollbars=1,resizable=0,width=800,height=600'); 
   window.open(url, null, 'scrollbars=1,resizable=0,width=800,height=600'); 
}
function abrirFormulario(url,width,height) {
    //window.showModalDialog(url, null, 'scrollbars=1,resizable=0,width=800,height=600'); 
    window.open(url, null, 'scrollbars=1,resizable=0,left=400,top=100,width=' + width + ',height=' + height + '');
}
