jQuery(function($) {
		$('.map .property-filter').show();

    function InitCarousel() {
        if ($('.carousel .content ul').length !== 0) {
            $('.carousel .content ul').carouFredSel({
                scroll: {
                    items: 1
                },
                auto: false,
                items: {
                    minimum: 1,
                    width: 145
                },
                next: {
                    button: '#carousel-next',
                    key: 'right'
                },
                prev: {
                    button: '#carousel-prev',
                    key: 'left'
                }
            });
        }

        if ($('.carousel-wrapper .content ul').length !== 0) {
            $('.carousel-wrapper .content ul').carouFredSel({
                scroll: {
                    items: 1
                },
                auto: false,
                items: {
                    minimum: 1
                },
                next: {
                    button: '#carousel-next',
                    key: 'right'
                },
                prev: {
                    button: '#carousel-prev',
                    key: 'left'
                }
            });
        }
    }

    InitCarousel();
});

jQuery(document).ready(function($) {    
    $('input[type=checkbox]').not('.no-ezmark').ezMark();
    $('input[type=radio]').not('.no-ezmark').ezMark();

    $('.properties-grid .property .title').hover(function() {
        $(this).closest('.property').addClass('hover');
    }, function() {
        $(this).closest('.property').removeClass('hover');
    });

    $('.property-filter .property-types .property-type').live('click', function(e) {
        e.preventDefault();
        if ($(this).hasClass('active')) {
            $(this).removeClass('active');
            $('input[type=checkbox]', this).attr('checked', null).change();
        } else {
            $(this).addClass('active');
            $('input[type=checkbox]', this).attr('checked', 'checked').change();
        }
    });

    $('.property-filter .property-types .property-type input[type=checkbox]').change(function() {
        $(this).closest('form').submit();
    });

    $('.property-filter .property-types').bxSlider({
        slideWidth: 180,
        infiniteLoop: false,
        moveSlides: 1,
        minSlides: 1,
        maxSlides: 6,
        pager: false,
        hideControlOnEnd: true,
        oneToOneTouch: false
    });

    $('.bxslider').bxSlider({
	    auto: true,
        slideWidth: 180,
        moveSlides: 1,
        minSlides: 1,
        maxSlides: 4,
        slideMargin: 20,
        infiniteLoop: false,
        hideControlOnEnd: true,
        pager: false
    });

		$('.submission-form .span4').hover(function() {
			var index = $(this).index();
			$(this).addClass('active');
			$('.progressbar .number').eq(index).addClass('active');

		}, function() {
			var index = $(this).index();
			$(this).removeClass('active');
			$('.progressbar .number').eq(index).removeClass('active');
		});

        $('.progressbar .item').hover(function() {
            var index = $(this).index();
            $('.number', this).addClass('active');
            $('.submission-form .span4').eq(index).addClass('active');

        }, function() {
            var index = $(this).index();
            $('.number', this).removeClass('active');
            $('.submission-form .span4').eq(index).removeClass('active');
        });


		

    InitChosen();
    InitPropertyCarousel();
		InitImageSlider();

    function InitPropertyCarousel() {
        $('.property-detail .gallery .content img').on({
            click: function(e) {
                var src = $(this).attr('src');
                var img = $(this).closest('.gallery').find('.preview img');

                img.attr('src', src);

                $('.property-detail .gallery .content li').each(function() {
                    $(this).removeClass('active');
                });

                $(this).closest('li').addClass('active');
            }
        });
        $('.property-detail .gallery .content a').on({
            click: function(e) {
                e.preventDefault();
            }
        })
    }

    function InitChosen() {
        $('select').each(function(index) {
            $(this).chosen({
                disable_search_threshold: 20
            });
        })
    }

	function InitImageSlider() {
		$('.iosSlider').iosSlider({
			desktopClickDrag: true,
			snapToChildren: true,
			infiniteSlider: true,
			navSlideSelector: '.slider .navigation li',
			onSlideComplete: function(args) {
				if(!args.slideChanged) return false;

				$(args.sliderObject).find('.slider-info').attr('style', '');

				$(args.currentSlideObject).find('.slider-info').animate({
					left: '15px',
					opacity: '.9'
				}, 'easeOutQuint');
			},
			onSliderLoaded: function(args) {
				$(args.sliderObject).find('.slider-info').attr('style', '');

				$(args.currentSlideObject).find('.slider-info').animate({
					left: '15px',
					opacity: '.9'
				}, 'easeOutQuint');
			},
			onSlideChange: function(args) {
				$('.slider .navigation li').removeClass('active');
				$('.slider .navigation li:eq(' + (args.currentSlideNumber - 1) + ')').addClass('active');
			},
			autoSlide: true,
			scrollbar: true,
			scrollbarContainer: '.sliderContainer .scrollbarContainer',
			scrollbarMargin: '0',
			scrollbarBorderRadius: '0',
			keyboardControls: true
		});
	}
});

