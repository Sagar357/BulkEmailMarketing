//language//
$(document).ready(function () {
    $(".Lang_ag").click(function () {
        $("#Lng").toggle();
    });
    //--endd---//

    //--login
    $(".Login").click(function () {
        $("#Login_wrapper").toggle();
    });
    //end---//
    $(".UserPro").click(function () {
        $("#UserProfile_Wrapper").toggle();
    });
    //---//
    $(".Camp_i01").click(function () {
        $(".camp_list").toggle();
    });

    //---start---//
    $(".Filter").click(function () {
        $("#Search_form").toggle();
        $("#Setting-child,#Setting-child1,#Setting-child2").css('display', 'none');
    });
    // ---setting---
    $(".setting").click(function () {
        $("#Setting-child").toggle();
        $("#Search_form").css('display', 'none');
    });
    $("#setting1").click(function () {
        $("#Setting-child1").toggle();
        $("#Setting-child2,#Search_form").css('display', 'none');
    });
    $("#setting2").click(function () {
        $("#Setting-child2").toggle();
        $("#Setting-child1,#Search_form").css('display', 'none');
    });
    // ----end--here---
    // add & Remove class
    $(".List01").click(function () {
        if (!$(this).hasClass('List_active')) {
            $(".List01.List_active").removeClass("List_active");
            $(this).addClass("List_active");
        }
    });
});

//--end---here----//

function openList(ListName) {
    var i;
    var x = document.getElementsByClassName("List");
    for (i = 0; i < x.length; i++) {
        x[i].style.display = "none";
    }
    document.getElementById(ListName).style.display = "block";
}



// --counter---//
// $(document).ready(function() {

//   var counters = $(".counter");
//   var countersQuantity = counters.length;
//   var counter = [];

//   for (i = 0; i < countersQuantity; i++) {
//     counter[i] = parseInt(counters[i].innerHTML);
//   }

//   var count = function(start, value, id) {
//     var localStart = start;
//     setInterval(function() {
//       if (localStart < value) {
//         localStart++;
//         counters[id].innerHTML = localStart;
//       }
//     }, 40);
//   }

//   for (j = 0; j < countersQuantity; j++) {
//     count(0, counter[j], j);
//   }
// });
