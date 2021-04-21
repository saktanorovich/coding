/**
 * Скрипты для корректной работы некоторых элементов форм (аля выбор адреса из 3-х выпадающих списков).
 */

/* Настройки рекапчи */
var RecaptchaOptions = {
  lang : 'ru',
  theme : 'clean'
}; 
 
var check_progress = null;

$(window).load(function(){

	
	
	$('form').each(function(){
		var form = $(this);
		
		
		
		/**
		 * этот обработчик, добавляет класс loading на любую кнопку в формах :) при клике на кнопку.
		 */
		$('input[type="submit"], input[type="button"]', form).live('click',  function(){
			$(this).addClass('loading');
		})
		
		/**
		 * Обработка required полей
		 */
		 
		$('.item.required', form).each(function(i, item){

			if($(item).hasClass('hubs')) {
				$('input#token-input-hubs', item).live('blur', function(){
					var tokens = $('input[name="hubs"]', item).tokenInput("get")
					if(tokens.length == 0){
						$('.error', item).text('Поле обязательное для заполнения').show()
					}else{
						$('.error', item).text('').hide()
					}
					//console.log('tokens', tokens)
				})
				
			}else{
				$('input[type="text"], input[type="password"], textarea', item).live('blur', function(){
					var text = $(this).val();
					var allSpacesRe = /\s+/g;
					text = text.replace(allSpacesRe, "")
					if( text == '' ){
						$('.error', item).text('Поле обязательно для заполнения').show()
					}else{
						$('.error', item).text('').hide()
					}
				})
			}
		})
		
		
		
		
	})
  
  /***
   ***  Элемент для выбора адреса (страна, регион, город).
   ***/
  var select_date = $('.select_date');
  
  // если выбрали страну
  $('.country', select_date).live('change',function(){
    var select_region = $('.region', select_date);
    var select_city = $('.city', select_date);
        select_region.attr('disabled',true);
        select_city.attr('disabled',true);
    $.get('/json/geo/regions/',{ 'country_id': $(this).val() }, function(json){      
      select_region.html('<option value="0">Регион</option>');
      select_city.html('<option value="0">Город</option>');
      for(k in json.regions){
        var region = json.regions[k];
        select_region.append('<option value="'+region.id+'">'+region.name+'</option>');
      }
      select_region.attr('disabled',false);
    },'json');    
  });
  
  // если выбрали регион
  $('.region', select_date).live('change',function(){
    var select_city = $('.city', select_date);
        select_city.attr('disabled',true);
    $.get('/json/geo/cities/',{ 'region_id': $(this).val() }, function(json){
      select_city.html('<option value="0">Город</option>');
      for(k in json.cities){
        var city = json.cities[k];
        select_city.append('<option value="'+city.id+'">'+city.name+'</option>');
      }
      select_city.attr('disabled',false);
    },'json');    
  });
  
  

  /** 
   * Новый загрузчик изоборажений.
   */
  var image = $('.iframe_uploader_preview .image');
  $('.delete', image).live('click', function(){
  	var name = $(this).data('name');
  	$('input[name="'+name+'"]').val('-1');
		$('.'+name+' .iframe_uploader_preview').html('<div class="image">Изображение удалено</div>');
  	return false;
  });
  
  
  
 
  
  
});



$(document).ready(function(){

	 // Custom Radio buttons
  $('.radio_list.custom').each(function(i, list){
  	//console.log('list', list)
  	$('label', list).each(function(i, label){
	  	//console.log('label', label)  	
  		$('input[type="radio"]', label).each(function(i, input){
				if( $(input).attr('checked') ){
					$('label', list).removeClass('checked')
					$(label).addClass('checked')
				}
  			$(input).click(function(){
  				if( $(input).attr('checked') ){
  					$('label', list).removeClass('checked')
  					$(label).addClass('checked')
  				}
  			});
  			
  		});
  	});
  });
  
  
  /** 
	 * обработчик параметра maxlength в текстовых полях
	 */

	$('textarea[maxlength], input[maxlength]').each(function(i, input){
        if( $(input).data('nocount') === true ){ return; }
		var item = $(this).parents('.item');
		var input = $(input);
		var count = $('<div class="count" title="Доступное количество символов для ввода">'+ input.attr('maxlength') +'</div>');
		item.append(count);
		count.text(input.attr('maxlength') - input.val().length );
		input.bind('keyup blur focus change', function(){
			count.text(input.attr('maxlength') - input.val().length );
		})
	})
  
})
	




	/***
   ***  Мега загрузчик пикчей (используется в ивентах, при регистрации для загрузки инвайта - надо будет убрать его, когда мы допилим наш новый загрузчик).
   ***/
  function show_uploader(uploader){
    var img_uploader = $(uploader);
        
    var upload_id = $('input[name="upload_id"]', img_uploader);
    var offset = img_uploader.offset();
    
    $('img',img_uploader).remove(); // удалим загруженное изображение
    $('a',img_uploader).remove();   // удалим ссылку
    upload_id.val('');              // обнулим upload_id
    img_uploader.css('height','60px');
    
    var upload_form = $('<form action="/upload/" class="upload_form" method="post" enctype="multipart/form-data" accept="image/gif, image/jpeg"><input type="file" name="image" /><div class="progress"><div class="bar"></div></div><div class="state">Выберите файл для загрузки</div></form>');
    $('body').append(upload_form);
    upload_form.css({'position':'absolute','left':offset.left+1,'top':offset.top+1});
    var input_file = $('input[type="file"]',upload_form);
    var progress_bar = $('.bar',upload_form);
    var state = $('.state',upload_form);
    
    // если вывелись ошибки - надо сменить позиционирование. этот обработчик делает это.
    img_uploader.bind('reset_position', function(){
      offset = img_uploader.offset();
      upload_form.css({'position':'absolute','left':offset.left+1,'top':offset.top+1});
    });
    
    input_file.change(function(){
      
      var progress_id = g_user_login + new Date().getTime();
      
      upload_form.ajaxSubmit({
        dataType: 'xml',
        url: '/upload/?X-Progress-ID='+progress_id,
        iframe: true,
        forceSync: true,
        resetForm: true,
        
        beforeSubmit: function(json){          
          check_progress = setInterval(function(){
            $.get('/progress/',{'X-Progress-ID': progress_id}, function(json){      
              if(json.state == 'uploading') {
                var percent = json.received / json.size * 100
                progress_bar.css('width',percent+'%');
                state.html('Загружено ' + json.received + ' из ' + json.size);  
              }else if(json.state == 'done'){
                clearInterval(check_progress);
              }
            },'json');
          },500);
        },
        success: function(xml){
          
          
          clearInterval(check_progress);
          if($(xml).find('message').text() == 'ok'){
            progress_bar.css({'width':'0%'});
            state.html('Файл успешно загружен');
            upload_form.remove();            
            img_uploader.append('<img src="'+$(xml).find('url').text()+'" alt=""  /><a href="#upload" class="upload_again" onclick="return show_uploader($(this).parent());">удалить или изменить</a>');
            img_uploader.css('height','auto');
            upload_id.val($(xml).find('id').text());
          }else{
            progress_bar.css({'width':'0%'});
            var errors = $(xml).find('error');
            if(errors){
              errors.each(function(){
                state.html('<span class="error">'+$(this).text()+'</span>');  
              });
              
            }else{
              state.html('<span class="error">Загрузка не удалась</span>');
            }
          }
        }
      });
    });
    return false;
  }  // END


