$('input[type="submit"]').mousedown(function(){
  $(this).css('background', '#2ecc71');
});
$('input[type="submit"]').mouseup(function(){
  $(this).css('background', '#1abc9c');
});

$('#loginform').click(function(){
  $('.login').fadeToggle('slow');
  $(this).toggleClass('green');
});



$(document).mouseup(function (e)
{
    var container = $(".login");

    if (!container.is(e.target) // if the target of the click isn't the container...
        && container.has(e.target).length === 0) // ... nor a descendant of the container
    {
        container.hide();
        $('#loginform').removeClass('green');
    }
});

/*For Registration Form*/
$(document).ready(function () {

    $('#password').on('keyup', function () {
        var pswd = $('#password').val();

        //validate the length
        if (pswd.length < 1 || pswd.length > 5) {
            $('#poor').removeClass('valid').addClass('invalid');
        } else {
            $('#poor').removeClass('invalid').addClass('valid');
        }

        if (pswd.length < 6 || pswd.length > 11) {
            $('#middle').removeClass('valid').addClass('invalid');
        } else {
            $('#middle').removeClass('invalid').addClass('valid');
        }

        if (pswd.length < 12) {
            $('#strong').removeClass('valid').addClass('invalid');
        } else {
            $('#strong').removeClass('invalid').addClass('valid');
        }

    }).focus(function () {
        $('#pswd_info').show();
    }).blur(function () {
        $('#pswd_info').hide();
    });

    $('#cpassword').on('keyup', function () {
        //validate the length
        if ($('#password').val() != $('#cpassword').val()) {
            $('#confirm').removeClass('valid').addClass('invalid');
            $('#message').html('Not Matching');
        } else {
            $('#confirm').removeClass('invalid').addClass('valid');
            $('#message').html('Matching');
        }

    }).focus(function () {
        $('#pswd_info2').show();
    }).blur(function () {
        $('#pswd_info2').hide();
    });


    //$('#email').on('keyup', function () {
    //    var emailReg = /^([\w-\.]+@@([\w-]+\.)+[\w-]{2,4})?$/;
    //    //validate the length
    //    if ($('#email').val() != emailReg) {
    //        $('#checkemail').removeClass('valid').addClass('invalid');
    //        $('#message1').html('Invalid Email');
    //    } else {
    //        $('#checkemail').removeClass('invalid').addClass('valid');
    //        $('#message1').html('Valid');
    //    }

    //}).focus(function () {
    //    $('#pswd_info3').show();
    //}).blur(function () {
    //    $('#pswd_info3').hide();
    //});

    

    

});


/*For Registration Form*/
$(document).ready(function () {

    $('#password').on('keyup', function () {
        var pswd = $('#password').val();

        //validate the length
        if (pswd.length < 1 || pswd.length > 5) {
            $('#poor').removeClass('valid').addClass('invalid');
        } else {
            $('#poor').removeClass('invalid').addClass('valid');
        }

        if (pswd.length < 6 || pswd.length > 11) {
            $('#middle').removeClass('valid').addClass('invalid');
        } else {
            $('#middle').removeClass('invalid').addClass('valid');
        }

        if (pswd.length < 12) {
            $('#strong').removeClass('valid').addClass('invalid');
        } else {
            $('#strong').removeClass('invalid').addClass('valid');
        }

    }).focus(function () {
        $('#pswd_info3').show();
    }).blur(function () {
        $('#pswd_info3').hide();
    });

    $('#cpassword').on('keyup', function () {
        //validate the length
        if ($('#password').val() != $('#cpassword').val()) {
            $('#confirm').removeClass('valid').addClass('invalid');
            $('#message').html('Not Matching');
        } else {
            $('#confirm').removeClass('invalid').addClass('valid');
            $('#message').html('Matching');
        }

    }).focus(function () {
        $('#pswd_info4').show();
    }).blur(function () {
        $('#pswd_info4').hide();
    });


    //$('#email').on('keyup', function () {
    //    var emailReg = /^([\w-\.]+@@([\w-]+\.)+[\w-]{2,4})?$/;
    //    //validate the length
    //    if ($('#email').val() != emailReg) {
    //        $('#checkemail').removeClass('valid').addClass('invalid');
    //        $('#message1').html('Invalid Email');
    //    } else {
    //        $('#checkemail').removeClass('invalid').addClass('valid');
    //        $('#message1').html('Valid');
    //    }

    //}).focus(function () {
    //    $('#pswd_info3').show();
    //}).blur(function () {
    //    $('#pswd_info3').hide();
    //});





});



/*For change password Form*/
$(document).ready(function () {

    $('#npassword').on('keyup', function () {
        var pswd = $('#npassword').val();

        //validate the length
        if (pswd.length < 1 || pswd.length > 5) {
            $('#poor').removeClass('valid').addClass('invalid');
        } else {
            $('#poor').removeClass('invalid').addClass('valid');
        }

        if (pswd.length < 6 || pswd.length > 11) {
            $('#middle').removeClass('valid').addClass('invalid');
        } else {
            $('#middle').removeClass('invalid').addClass('valid');
        }

        if (pswd.length < 12) {
            $('#strong').removeClass('valid').addClass('invalid');
        } else {
            $('#strong').removeClass('invalid').addClass('valid');
        }

    }).focus(function () {
        $('#npassword_info').show();
    }).blur(function () {
        $('#npassword_info').hide();
    });

    $('#ncpassword').on('keyup', function () {
        //validate the length
        if ($('#npassword').val() != $('#ncpassword').val()) {
            $('#nconfirm').removeClass('valid').addClass('invalid');
            $('#nmessage').html('Not Matching');
        } else {
            $('#nconfirm').removeClass('invalid').addClass('valid');
            $('#nmessage').html('Matching');
        }

    }).focus(function () {
        $('#npassword_info2').show();
    }).blur(function () {
        $('#npassword_info2').hide();
    });


    //$('#email').on('keyup', function () {
    //    var emailReg = /^([\w-\.]+@@([\w-]+\.)+[\w-]{2,4})?$/;
    //    //validate the length
    //    if ($('#email').val() != emailReg) {
    //        $('#checkemail').removeClass('valid').addClass('invalid');
    //        $('#message1').html('Invalid Email');
    //    } else {
    //        $('#checkemail').removeClass('invalid').addClass('valid');
    //        $('#message1').html('Valid');
    //    }

    //}).focus(function () {
    //    $('#pswd_info3').show();
    //}).blur(function () {
    //    $('#pswd_info3').hide();
    //});





});