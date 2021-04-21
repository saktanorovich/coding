

/**
 * 
 */
$(window).load(function(){
  var histAPI = !!(window.history && history.pushState)
  if( histAPI && ((document.location.hash == "#habracut") || (document.location.hash == "#comments")) ){
    history.replaceState({}, document.title, document.location.pathname + document.location.search) 
  }
})




/**
 * Хэлперы, помогающие работать с формами и плагином jqueryForm
 * В стандартных хабраформах - это делать обязательно.
 * Используется так,
     $('#form').ajaxForm({
        form: $('#form'),
        beforeSubmit: ajaxFormBeforSubmit,
        error: ajaxFormError,
        beforeSubmit: function(formData, jqForm, options){
          // do
          return ajaxFormBeforSubmit(formData, jqForm, options)
        },
        success: ajaxFormSuccess(function(json, statusText, xhr, jqForm){
          $.jGrowl('Новые настройки успешно сохранились');
        })
        
        
        success: ajaxFormSuccess(ajaxFormRedirect)
        
        success: ajaxFormSuccess(function(json, statusText, xhr, jqForm){
          $.jGrowl('Новые настройки успешно сохранились');
        }, [ function(json, statusText, xhr, jqForm){
          $.jGrowl('как то особенно обработать ошибку :) ');
        } ])
      });
      
      ajaxFormRedirect
 */



 
/**
 * Хэлпер рекомендуюется использовать перед сабмитом формы.
 */
var ajaxFormBeforSubmit = function (formData, jqForm, options){

  $('.error', jqForm).text('').hide()

  $('input[type="submit"], input[type="button"]', jqForm).attr('disabled', true)

}
/**
 * Хэлпер рекомендуется использовать для обработки ошибок при сабмите формы.
 */
var ajaxFormError = function (formData, jqForm){
  
  $('input[type="submit"], input[type="button"]', jqForm).attr('disabled', false).removeClass('loading')
}
/**
 * Хэлпер рекомендуется использовать для обработки успешного сабмита формы.
 */
var ajaxFormSuccess = function (callback, err_callback, noscroll){
  return function(json, statusText, xhr, jqForm){ 
    $('input[type="submit"], input[type="button"]', this.form).attr('disabled', false).removeClass('loading');

    if(json.messages == 'ok'){
      if(ajaxFormRedirect(json)) return;
      else return callback(json, statusText, xhr, jqForm)
    }else{       
      show_form_errors(json)

      if( typeof(err_callback) == 'function' ){
        err_callback(json, statusText, xhr, jqForm)
      }
    }
  }
}

var ajaxFormRedirect = function(json){
  if( typeof( json.redirect_debug ) != 'undefined'){
    $.jGrowl('REDIRECT DEBUG: <a href="'+json.redirect_debug+'">'+json.redirect_debug+'</a>', { theme: 'message', sticky: true });
    return true;
  }else if( typeof( json.redirect ) != 'undefined'){
    document.location.href = json.redirect;
    return true;
  }
  return false;
}

/**
 * Функция отображает ошибки в результате сабмита формы или system_error
 */
function show_form_errors(data){
  // покажем ошибки в форме - под каждым полем. Если такие имеются :) 
  if(typeof(data.errors) != 'undefined'){
    for(key in data.errors){ 
      // иногда ошибки приходят в виде: 
      // {"errors":
      //  {"contacts[5][type]":"URL сайта введен неверно"}
      // поэтому нужно вырезать [ и ]
      classname = key.replace(/[\]\[]/g,'')
      
      $('.'+classname+' .error').html(data.errors[key]).fadeIn();
    }
    $.scrollTo( $('.error:visible').first(), 800, { axis: 'y', offset: -150 } );
  }
      
  // покажем системные ошибки, если что то пошло не так.

  show_system_error(data);
}
  
  
/**
 * Функция отображает системные ошибки
 */
function show_system_error(data){
  show_system_warnings(data);
  if(typeof(data.system_errors) != 'undefined'){
    for(key in data.system_errors){ 
      $.jGrowl(data.system_errors[key], { theme: 'error' });
    }
  }
}
  
/**
 * Функция отображает системные предупреждения 
 */
function show_system_warnings(data){
  if(typeof(data.system_warnings) != 'undefined'){
    for(key in data.system_warnings){
      $.jGrowl(data.system_warnings[key], { theme: 'warning' });
    }
  }
}

$(document).ready(function(){
	
  $(document).on('submit', 'form[data-remote="true"]', function(){
    var form = $(this)
   
    form.ajaxSubmit({
      form: form,
      beforeSubmit: ajaxFormBeforSubmit,
      error: ajaxFormError,
      success: ajaxFormSuccess(function(json){
        if( typeof(json.message) != 'undefined' ){
          $.jGrowl(json.message);
        }
      })
    })
    return false;
  })

    $(document).on('click', 'a[data-remote="true"]', function(event){
        var link = $(this);

        var arr = {};
        for (var i=0, attrs=link.get(0).attributes, l=attrs.length; i<l; i++){
            if ((attrs.item(i).nodeName + '').indexOf('data-', 0) >= 0)
            {
                var at = attrs.item(i).nodeName;
                at = at.replace(/data-/g, '');
                arr[at] = $(this).attr('data-' + at);
            }
        }

        var post = $.map(arr, function(val, index) {
            return index + "=" + val;
        }).join("&");

        $.post(link.attr('href'), post, function(response) {});

        event.preventDefault();
    });
  
  /*
  $('form[data-remote="true"]').each(function(i, form){ 
    var form = $(form);
    form.ajaxForm({
        form: form,
        beforeSubmit: ajaxFormBeforSubmit,
        error: ajaxFormError,
        success: ajaxFormSuccess(function(json){
          if( typeof(json.message) != 'undefined' ){
            $.jGrowl(json.message);
          }
        })
      });
  });
  */
  
  /** 
   * Нравится/не нравится хаб - скрипты для сайдбара
   */
  $('.join_hub_panel .join_hub').click(function(){
    var link = $(this);
        link.addClass('loading');
    var id = $(this).attr('data-id');
    var hub_item = $('#hub_item_'+id);
        

    $.post('/json/hubs/subscribe/', {'hub_id':id}, function(json){
      if(json.messages =='ok'){
        $('.leave_hub', hub_item).removeClass('hidden');
        $('.join_hub', hub_item).addClass('hidden'); 
        hub_item.addClass('membership')
      }else{
        show_system_error(json);
      }
      link.removeClass('loading');
    },'json');
    return false;
  });
  $('.join_hub_panel .leave_hub').click(function(){
    var id = $(this).attr('data-id');
    var link = $(this);
        link.addClass('loading');
    var hub_item = $('#hub_item_'+id);
  
      $.post('/json/hubs/unsubscribe/', {'hub_id':id}, function(json){
        if(json.messages =='ok'){
        $('.leave_hub', hub_item).addClass('hidden');
        $('.join_hub', hub_item).removeClass('hidden'); 
        hub_item.removeClass('membership')
        }else{
          show_system_error(json);
        }
        link.removeClass('loading');
      },'json');

    return false;
  });




  
  /**
   * В случае ajax ошибок, выведем сообщение юзеру.
   */
  window.ajax_errors_count = 0
  
  $(document).ajaxError(function(event, request, settings){
    console.log('ajaxError', settings.url, event, request, settings)
    if(window.ajax_errors_count > 2){
      //$.jGrowl('Ошибка при отправке AJAX-запроса ('+settings.url+') повторяется. Сообщите об этом разработчикам на support@habrahabr.ru', { theme: 'error', sticky: true })
    }else{
      //$.jGrowl('При отправке AJAX-запроса ('+settings.url+') произошла ошибка. Попробуйте еще раз?', { theme: 'error' })
    }
    $('input.loading, button.loading').attr('disabled',false).removeClass('loading')
    window.ajax_errors_count++
  });
  
  $(document).ajaxSuccess(function(event, request, settings, json){
    if( settings.dataType == 'json' ){
      if(typeof(json.debug) == 'string') {
        $('#php-debug').replaceWith(json.debug);
      }
    }
  });

  /**
   * включим подсветку синтаксиса.
   */
  
  $('pre code').each(function(i, e) { 
  
    hljs.highlightBlock(e, '    ');
    
  });
  
  // Добавим поддержку тега-спойлера
  $('.html_format .spoiler_title').live('click', function(){
    var spoiler = $(this).parent('.spoiler');
    
    if(spoiler.hasClass('spoiler_open')) {
      $('iframe', spoiler).attr('src', $('iframe', spoiler).attr('src'));
    }

    $('> .spoiler_text', spoiler).slideToggle();
    spoiler.toggleClass('spoiler_open');

  });
  

  $('#global_notify .close').live('click', function(){

    var id = $(this).attr('data-id');
    var curent_notification = $('#notification_'+id);
    var next_notification = curent_notification.next('.inner_notice');
    curent_notification.remove();
    if(next_notification) next_notification.removeClass('hidden');
    
    $.post('/json/notifications/close/',  { 'id' : id }, function(json){
        if(json.messages =='ok'){
          
        }else{
          show_system_error(json);
        }
    },'json');
    
    return false;
  });
});



function _getDate(d) {      
  var month_names = new Array("января", "февраля", "марта", "апреля", "мая", "июня", "июля", "августа", "сентября", "октября", "ноября", "декабря");  
  //var d = new Date();
  var current_date = d.getDate();
  var current_month = d.getMonth();
  var current_year = d.getFullYear();
  
  return current_date + " " + month_names[current_month]  + " " + current_year;
  
}
function _getTime(currentTime) {
  //var currentTime = new Date();
  var hours = currentTime.getHours();
  var minutes = currentTime.getMinutes();
  
  if (minutes < 10){
    minutes = "0" + minutes;
  }
  return hours + ":" + minutes;
}



/* 
 diff - колво секунд
*/
function timer_countdown(diff) {

  days  = Math.floor( diff / (60*60*24) );
  hours = Math.floor( diff / (60*60) );
  mins  = Math.floor( diff / (60) );
  secs  = Math.floor( diff );

  dd = days;
  hh = hours - days  * 24;
  mm = mins  - hours * 60;
  ss = secs  - mins  * 60;
  
  var result = [];

  if( hh > 0) result.push(hh ? addzero(hh) : '00') //+ ':'; // ' + plural_str(hh, 'час', 'часа', 'часов') + ' ';
  result.push(mm ? addzero(mm) : '00') //+ ':'; // ' + plural_str(mm, 'минута', 'минуты', 'минут') + ' ';
  result.push(ss ? addzero(ss) : '00') //+ ' ' + plural_str(ss, 'секунда', 'секунды', 'секунд') + ' ';
  
  return result.join(':');
}

function addzero(n){
  return (n < 10) ? '0'+n : n;
}

/*
var num = 12;
plural_str(num, ‘товар’,'товара’,'товаров’); // 12 товаров
*/
function plural_str(i, str1, str2, str3){
  function plural (a){
    if ( a % 10 == 1 && a % 100 != 11 ) return 0
    else if ( a % 10 >= 2 && a % 10 <= 4 && ( a % 100 < 10 || a % 100 >= 20)) return 1
    else return 2;
  }

  switch (plural(i)) {
    case 0: return str1;
    case 1: return str2;
    default: return str3;
  }
}


function replaceURLWithHTMLLinks(text) {
    var    exp_link = /(\b(https?|ftp|file):\/\/[-A-Z0-9+&@#\/%?=~_|!:,.;]*[-A-Z0-9+&@#\/%=~_|])/i;
    var    exp_user = /(^|\s)@(\w+)/g;
    var    exp_hash = /(^|\s)#(\w+)/g;
    var    text = text.replace(exp_link,'<a href="$1">$1</a>'); 
           text = text.replace(exp_user, '$1@<a href="https://www.twitter.com/$2">$2</a>');
           text = text.replace(exp_hash, '$1<a href="https://twitter.com/search?q=%23$2">#$2</a>');

    return text; 
}  










// from http://widgets.twimg.com/j/1/widget.js
var K = function () {
    var a = navigator.userAgent;
    return {
        ie: a.match(/MSIE\s([^;]*)/)
    }
}();
 
var H = function (a) {
    var b = new Date();
    var c = new Date(a);
    if (K.ie) {
        c = Date.parse(a.replace(/( \+)/, ' UTC$1'))
    }
    var d = b - c;
    var e = 1000,
        minute = e * 60,
        hour = minute * 60,
        day = hour * 24,
        week = day * 7;
    if (isNaN(d) || d < 0) {
        return ""
    }
    if (d < e * 7) {
        return "right now"
    }
    if (d < minute) {
        return Math.floor(d / e) + " s"
    }
    if (d < minute * 2) {
        return "1 m"
    }
    if (d < hour) {
        return Math.floor(d / minute) + " m"
    }
    if (d < hour * 2) {
        return "1 h"
    }
    if (d < day) {
        return Math.floor(d / hour) + " h"
    }
    if (d > day && d < day * 2) {
        return "yesterday"
    }
    if (d < day * 365) {
        return Math.floor(d / day) + " d"
    } else {
        return "over a year ago"
    }
};
 
function empty(text){
      var allSpacesRe = /\s+/g;
      text = text.replace(allSpacesRe, "")
      if(text == ''){
        return true
      }else{
        return false
      }
}
