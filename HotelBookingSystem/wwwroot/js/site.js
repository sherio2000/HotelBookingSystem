// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

(function ($) {

    'use strict';
    // aos
    AOS.init({
        duration: 1000
    });

    var windowScroll = function () {

        $(window).scroll(function () {
            var $win = $(window);
            if ($win.scrollTop() > 200) {
                $('.js-site-header').addClass('scrolled');
            } else {
                $('.js-site-header').removeClass('scrolled');
            }

        });

    };
    windowScroll();



    



})(jQuery);
