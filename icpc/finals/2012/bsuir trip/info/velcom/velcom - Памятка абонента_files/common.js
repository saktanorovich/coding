


    var oPlugInWindow=null;
    var sWindowGuid="";
    function openCustomPlugIn(sURL, sFeatures) {
     if (sWindowGuid.length==0)
       sWindowGuid=NewGuid();
       oPlugInWindow = window.open(sURL, sWindowGuid, sFeatures, "");
       oPlugInWindow.focus();
       return false;
    }
    function closeWindow() {
     oPlugInWindow.opener=top.opener;
     top.close();
     return true;
    }
    function NewGuid() {
      var sGuid="";
      for (var i=0; i<32; i++) {
       sGuid+=Math.floor(Math.random()*0xF).toString(0xF);
      }
      return sGuid;
    } 

function searchSubmit(){
  if (document.search_form){
    var str = document.search_form.query.value.replace(/\s*/g,'');
    if ( str.length>0  ){ 
          if (str.length>2 && typeof(gLastRequest) !== 'undefined' && gLastRequest!= null && gLastRequest!="" && gVisibility){
              document.search_form.query.value = gLastRequest;
          }
          document.search_form.submit(); 
    } 
    else { return false; 
    }
  }
}
function searchSubmitOnEnter(event) {
  var ENTER_KEY = 13;
  var code = "";
  if (window.event) { code = event.keyCode; // IE
  }
  else if (event.which) { code = event.which; // Netscape/Firefox/Opera
  }
  if (code == ENTER_KEY) { searchSubmit(); 
  }
}

function isNumber(inputVal) {
 var match = /^[1-9][0-9]{0,}$/.test(inputVal);
 return match;
}


function add_actions(){ 
        var tableList=document.getElementsByTagName("table");
        c_name=/shop_cart/;
        c2_name=/tariff_simple/;
        c3_name=/roaming_complex/;
        c_addName=/protect/;
        for (var i=1; i<tableList.length; i++){
            if(c3_name.test(tableList[i].className) || c_name.test(tableList[i].className) || c2_name.test(tableList[i].className)){
                if (c_addName.test(tableList[i].className)) continue;                
                trList=tableList[i].getElementsByTagName("tr");
                for(var j=0; j<trList.length; j++){
                     if (c_addName.test(trList[j].className)) continue; 
                    trList[j].setAttribute("onmouseover", "change_bg(this);");                
                    //alert(trList[j].onmouseout);        
                    trList[j].setAttribute("onmouseout", "change_bg_out(this);");
                     exp_ver6=/msie 6/;
                     exp_ver7=/msie 7/;
                     if (exp_ver6.test(ua)||exp_ver7.test(ua)) {
                        trList[j].setAttribute("onmouseover", function() {change_bg(this)} );
                        trList[j].setAttribute("onmouseout", function() {change_bg_out(this)} );
                    }
                }
            }
        }    
    }

    
    function change_bg(tr){
        tdList=tr.getElementsByTagName("td");
        if(tdList[0].className!="t_header")
            for(var k=0; k<tdList.length; k++){
                    tdList[k].className+=" y_style";
            }
    }
    
    function change_bg_out(tr){
        tdList=tr.getElementsByTagName("td");
        for(var k=0; k<tdList.length; k++){        
            tdList[k].className=String(tdList[k].className).replace(/ y_style/, "")            
        }
    }
    
    function change_bg_div(t){
        t.className+=" y_style";
    }
    
    function change_bg_out_div(t){            
        t.className=String(t.className).replace(/ y_style/g, "")                    
    }

function clearInput(input){
    alert("Common JS says: clearInput(input) function deprecated, please remove it from template... ");
    if (input) {
        if (input.cleared){
            return;
        } else {
            input.value="";
            input.cleared = true;
        }
    }
}

// ******************* FORM INPUTS MANAGEMENT
function onLoseFocus(input){
        if (input && input.cleared && input.value && input.value.length > 0){
            input.def = 0; // is not default value
        } else {
            input.value = input.def_value;
            input.cleared = null;
            input.def = 1;
        }
}          
function onFocus(input){
    if (input && !input.cleared){
            input.def_value = input.value; 
            input.value="";
            input.cleared = true;
            input.focus(); 
        }
}     
           
function onFocusOut(input){
    this.run = function(){
            onFocus(input);
        }
}
function onLoseFocusOut(input){
    this.run = function(){
            //alert("inner onLoseFocusOut " + input.id);
            onLoseFocus(input);
        }
}
function setEvent(theInput){
    if (!theInput.onfocus && !theInput.onblur){
    theInput.onfocus=new onFocusOut(theInput).run;    
    theInput.onblur=new onLoseFocusOut(theInput).run;
    theInput.def = 1;
    theInput.def_value = theInput.value;
    }
}
function setEvents(){
    var formInputs = document.getElementsByTagName('input');
    for (var i = 0; i < formInputs.length; i++) {
        var theInput = formInputs[i];
        if (theInput.type == 'text' ) {
            setEvent(theInput)
        }
    }    
}
function clearDefaultValues(form){
    var elems = form.elements;
    var i;
    for ( i=0; i<elems.length; i++){
        if (elems[i].type == 'text'){
            if ( elems[i].def && elems[i].def == 1  ){
                elems[i].value="";
            }
        }
    }
}

function addOnloadEvent(fnc){
  if ( typeof window.addEventListener != "undefined" )
    window.addEventListener( "load", fnc, false );
  else if ( typeof window.attachEvent != "undefined" ) {
    window.attachEvent( "onload", fnc );
  }
  else {
    if ( window.onload != null ) {
      var oldOnload = window.onload;
      window.onload = function ( e ) {
        oldOnload( e );
        window[fnc]();
      };
    }
    else
      window.onload = fnc;
  }
}
addOnloadEvent(setEvents);
addOnloadEvent(add_actions);

//******************************************* SCRIPTS MANAGEMENT
var scripts = new Array();
function wscript(url, name){
    this.url = url;
    this.name = name;
}
function getURLByName(name){
    var i=0;
    for(i; i<scripts.length; i++ ){
        if (scripts[i].name == name){
            return scripts[i].url;
        }
    }
}



scripts.push(new wscript('/ru/js/popup.js','JavaScriptPopup'));

scripts.push(new wscript('/ru/js/application-form.js','JavaScriptApplicationForm'));

scripts.push(new wscript('/ru/js/forms.js','JavaScriptForms'));

scripts.push(new wscript('/ru/js/banner_management.js','JavaScriptBannerManagement'));

scripts.push(new wscript('/ru/js/campaigns.js','JavaScriptCampaigns'));

scripts.push(new wscript('/ru/js/connection-application.js','JavaScriptConnectionApplication'));

scripts.push(new wscript('/ru/js/devices_settings.js','JavaScriptDevicesSettings'));

scripts.push(new wscript('/ru/js/418.htm','JavaScriptEmbedSWFObject'));

scripts.push(new wscript('/ru/js/gallery.js','JavaScriptGallery'));

scripts.push(new wscript('/ru/js/companycenters.js','JavaScriptCompanyCenters'));

scripts.push(new wscript('/ru/js/ask-question.js','JavaScriptAskQuestion'));

scripts.push(new wscript('/ru/js/web-sms.js','JavaScriptSendSMS'));

scripts.push(new wscript('/ru/js/services.js','JavaScriptServices'));

scripts.push(new wscript('/ru/js/show-hints.js','JavaScripShowHints'));

scripts.push(new wscript('/ru/js/swf_object.js','JavaScriptSWFObject'));

scripts.push(new wscript('/ru/js/tarifs.js','JavaScriptTarifs'));

scripts.push(new wscript('/ru/js/calendar_simplified.js','JavaScriptCalendarSimplified'));

scripts.push(new wscript('/ru/js/calendar.js','JavaScriptCalendar'));

scripts.push(new wscript('/ru/js/pagination.js','JavaScriptPagination'));

scripts.push(new wscript('/ru/js/shop_card.js','JavaScriptShopCard'));

scripts.push(new wscript('/ru/js/phones.js','JavaScriptPhones'));



var imported_functions = new Array();
function wrapper(name, params, id){
    this.id=id;
    this.params = params;
    this.name = name;
    this.run = function(){
        eval( name+'(imported_functions['+id+'].params);' );
    }
}

function runFunk(funk, param){
    imported_functions[imported_functions.length] = new wrapper(funk, param, imported_functions.length);
    var id= imported_functions.length-1;
    imported_functions[id].run();
}
function callWrapper(funk, param){
    this.run = function(){
        runFunk(funk, param);
    }
}
function connect(name, funk, param){
    var script = document.createElement('script');
    script.type = 'text/javascript';
    script.language="javascript"
    script.src = getURLByName(name);
    var head = document.getElementsByTagName('head').item(0);
    
    if (funk){
        var cW = new callWrapper(funk, param);
        script.onload = cW.run;
        script.onreadystatechange = function() {
         if (this.readyState == 'complete' || this.readyState =='loaded') {
          runFunk(funk, param);
         }
        }
    } 
    head.appendChild(script);
}


//***********************************************  SHOP CART PART
function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for(var i=0;i < ca.length;i++) {
        var c = ca[i];
        while (c.charAt(0)==' ') c = c.substring(1,c.length);
        if (c.indexOf(nameEQ) == 0) return unescape(c.substring(nameEQ.length,c.length));
    }
    return null;
}

function getCookieArray(name){
    var cookie = getCookie(name); 
    if (cookie && cookie != 'undefined') {
        return cookie.split(":");
    }
}
function checkShopCart(){
    var temp = getCookieArray("velcomphones");
    if (temp && temp.length>0){
        return true;
    } 
    temp =  getCookieArray("velcomtarifs");
    if (temp && temp.length>0){
        return true;    
    } 
}

var card = new Object;
function getCartOnClick(params){
  card = shopCard();
  card.addToOrder(params[0], params[1], params[2], params[3], params[4]);
}
function cartLoader(){
    connect('JavaScriptShopCard', 'cartRunner');
}
function manage_cart(){
    if ( checkShopCart() ){
        addOnloadEvent(cartLoader);
    } else {
        try{
            //document.getElementById('shop_card').style.display='none';
            card.addToOrder = function(par_1,par_2,par_3,par_4,par_5){
                var params = new Array(par_1,par_2,par_3,par_4,par_5); 
                connect('JavaScriptShopCard','getCartOnClick',params);
            }
        } catch (err){
        }
    } 
}
manage_cart();
//addOnloadEvent(manage_cart);
 
function replace_title() {
    try { 
      var el = document.getElementById("page_title");
      if (el) {
          document.title = document.title + " - " + el.innerHTML;
      }
    } catch(err) {};
}
 
function changeDisplayState (id) {
e=document.getElementById(id);
if (e.style.display == 'none' || e.style.display =="") {
e.style.display = 'block';
} else {
e.style.display = 'none';
}
}

function lightMe(row) {
        try {
            if (!mainTable) {
                var mainTable = row.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode; 
           }
            indexRow = row.rowIndex;
            for (i = 0; i < mainTable.children.length; i++) {
                el = mainTable.children[i].children[0].children[0].children[1].children[0].children[0].children[0].children[indexRow].children[0];
                el.className+=" y_style";
                //el.style.background = 'yellow';
            }
        } catch (e) {
        }
    }
    function unlightMe(row) {
        try {
            if (!mainTable) {
                var mainTable = row.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode; 
            }
            indexRow = row.rowIndex;
            for (i = 0; i < mainTable.children.length; i++) {
                el = mainTable.children[i].children[0].children[0].children[1].children[0].children[0].children[0].children[indexRow].children[0];               
                el.className=String(el.className).replace(/ y_style/, "");
                //el.style.background = '';
            }
        } catch (e) {
        }
    }
 function cdma_add_actions(){ 
        var tableList=document.getElementsByTagName("table");
        c_name=/tariff_simple/;
        c_addName=/protect/;
        for (var i=1; i<tableList.length; i++){
            if(c_name.test(tableList[i].className)){ 
                tableList[i].className=tableList[i].className+" "+c_addName;
                tableList[i].parentNode.parentNode.style.backgroundImage="none";               
                trList=tableList[i].getElementsByTagName("tr");
                for(var j=0; j<trList.length; j++){
                    trList[j].setAttribute("onmouseover", "lightMe(this);");                
                    //alert(trList[j].onmouseout);        
                    trList[j].setAttribute("onmouseout", "unlightMe(this);");
                     exp_ver6=/msie 6/;
                     exp_ver7=/msie 7/;
                     if (exp_ver6.test(ua)||exp_ver7.test(ua)) {
                        trList[j].setAttribute("onmouseover", function() {lightMe(this)} );
                        trList[j].setAttribute("onmouseout", function() {unlightMe(this)} );
                     }
                }
            }
        }    
 }
 //FOR PAGES WITH PHONE AND WITH MOBILE
    function setFontSingleLi(el,size){
        el.style.fontSize=size+"px";
    }
    function setFontAllLi(size){
        var divs = document.getElementsByTagName("DIV");
        for (var i=0;i<divs.length;i++){
            if (divs[i].className=="textHolder") {
                var ul=divs[i].children[0];
                var masLi = ul.children;
                for (var j=0;j<masLi.length;j++){
                    setFontSingleLi(masLi[j],size);
                }
            }
        }
    }
    function getMaxWidth(){
        return showLen.innerHTML;
    }
    function getElementFontSize(el){
        var elSize = el.currentStyle.fontSize;
        return elSize.substr(0, elSize.length-2);   
    }
    function insertSpaces(text){
        for (var i=0;i<delimiters.length;i++)
            text=text.replace(delimiters.charAt(i),"&nbsp;"+delimiters.charAt(i)+"&nbsp;"); 
        return text;   
    }
    function normalizeWidth(){
        var maxWidth = getMaxWidth();
        var isChanged = false;
        divs = document.getElementsByTagName("DIV");
        for (var i=0;i<divs.length;i++){
            if (divs[i].className=="textHolder") {
                var ul=divs[i].children[0];
                var masLi = ul.children;
                for (var j=0;j<masLi.length;j++){
                    var size = masLi[j].offsetWidth;
                    var curFontSize = getElementFontSize(masLi[j]);
                    //showLen.innerHTML=showLen.innerHTML+"<br>"+size;
                    var isChanged = false;
                    
                    if (size>=maxWidth) masLi[j].innerHTML=insertSpaces(masLi[j].innerHTML);
                    while(curFontSize>minFontSize){                       
                        if (size>=maxWidth){
                            curFontSize--;
                            setFontSingleLi(masLi[j],curFontSize );                                    
                            isChanged = true;
                        }else {
                            if (isChanged) setFontAllLi(curFontSize);
                            isChanged = false;
                            break;    
                        }
                    }
                    if (isChanged) setFontAllLi(curFontSize);
                }
            }
        }
     }
  
      function movetophone(path){       	 
		document.location=path;
	 }   
