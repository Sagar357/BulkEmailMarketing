var fnclose = event => {
    var x = event.target.dataset.target;
    var elem = document.getElementsByClassName(x);
    elem[0].style.display = "none";
    if (event.target.dataset.link != undefined && event.target.dataset.link != '') {
        //     alert(event.target.dataset.link) 
        var Campaign_wrp = document.getElementsByClassName('Campaign_wrp');
        for (i = 0; i < Campaign_wrp.length; i++) {
            if (Campaign_wrp[i].attributes.id != undefined) {
                var selector = Campaign_wrp[i].attributes.id.value;
                document.getElementById(selector).style.display = 'block';
            }
        }
    }
    //     debugger
}

function FormSubmit() {
    var x = document.querySelectorAll(".FormPop");
    if (x[0].style.display === "block") {
        x[0].style.display = "none";
    } else {
        x[0].style.display = "block";
    }
}

// end first function

function FormSubmit2() {
    var y = document.getElementsByClassName("FormPop");
    var x = document.getElementsByClassName("FormPop2");
    if (x[0].style.display === "block") {
        x[0].style.display = "none";
    } else {
        x[0].style.display = "block";
        y[0].style.display = "none";
    }
}


// prodcut//

function openProduct(productName) {
    var i;
    var x = document.getElementsByClassName("product");
    for (i = 0; i < x.length; i++) {
        x[i].style.display = "none";
    }
    document.getElementById(productName).style.display = "block";
}


//chekbox
//Jquery code start

$('#checkbox').change(function () {
    // if()
    $("form input:checkbox").prop('checked', 'true');

});


// flip

// $(".Camp_chain").click(function(event)
//   {
//     $(".L2").slideToggle(500).css("display-block");           
// });

// ----***----Log In---**---//

var working = false;
$('.login').on('submit', function (e) {
    e.preventDefault();
    if (working) return;
    working = true;
    var $this = $(this),
        $state = $this.find('.btn1 > .state');
    $this.addClass('loading');
    $state.html('Authenticating');
    setTimeout(function () {
        $this.addClass('ok');
        $state.html('Welcome back!');
        setTimeout(function () {
            $state.html('Log in');
            $this.removeClass('ok loading');
            working = false;
        }, 4000);
    }, 3000);
});


var expand = event => {
    event.currentTarget.nextElementSibling.style.display = "block";
}

// sign up pop up

function mBtn() {
    var x = document.querySelectorAll(".Form_cmn");
    if (x[0].style.display === "block") {
        x[0].style.display = "none";
    } else {
        x[0].style.display = "block";
    }
}
