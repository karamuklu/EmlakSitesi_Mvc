(function($) {
    wp.customize('general_primary_color', function( value ) {
        value.bind( function( newval ) {
            $.ajax({
                type: 'GET',
                url: settings.base_url + '/wp-content/themes/properta/settings/compile-sass.php',
                data: {color: newval, type: 'primary'},
                success: function(data) {
                    if ($('#custom_styles').length != 0) {
                        $('#custom_styles').remove();
                    }
                    $('head').append('<style type="text/css" id="custom_styles">' + data + '</style>');
                }
            })
        });
    });

    wp.customize('general_secondary_color', function( value ) {
        value.bind( function( newval ) {
            $.ajax({
                type: 'GET',
                url: settings.base_url +  '/wp-content/themes/properta/settings/compile-sass.php',
                data: {color: newval, type: 'secondary'},
                success: function(data) {
                    if ($('#custom_styles').length != 0) {
                        $('#custom_styles').remove();
                    }
                    $('head').append('<style type="text/css" id="custom_styles">' + data + '</style>');
                }
            })
        });
    });
})(jQuery);