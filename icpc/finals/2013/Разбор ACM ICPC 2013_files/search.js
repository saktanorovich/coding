$(document).ready(function(){
  $('#search_form input[type="text"]').autocomplete({
    serviceUrl: '/json/suggest/', 
    minChars: 2, 
    delimiter: /(,|;)\s*/, // Разделитель для нескольких запросов, символ или регулярное выражение
    maxHeight: 400, 
    width: 300, 
    zIndex: 9999, 
    deferRequestBy: 500, 
    params: { type: 'search'}
  });
});