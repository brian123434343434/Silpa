var spacer = null;
var curObj = null;
function openIt(obj) {
    if (spacer) return;
    spacer = document.createElement("span");
    spacer.style.width = obj.offsetWidth;
    spacer.style.height = obj.offsetHeight;
    spacer.style.display = "none";
    obj.parentNode.insertBefore(spacer, obj);


    obj.style.left = getAbsPos(obj, "Left");
    obj.style.top = getAbsPos(obj, "Top");
    obj.style.position = "top";
    obj.style.width = obj.scrollWidth;
    obj.focus();
    spacer.style.display = "inline";
    curObj = obj;
    }
function closeIt() {
    if (spacer) {
        spacer.parentNode.removeChild(spacer);
        spacer = null;
    }
    if (curObj) {
        curObj.style.width = "100px";
        curObj.style.position = "static";
    }
}
function getAbsPos(o, p) { var i = 0; while (o != null) { i += o["offset" + p]; o = o.offsetParent; } return i; } 
