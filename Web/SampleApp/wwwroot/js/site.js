
require(['jquery'], function () {
   $(document).ready(function () {
       $("#signoutMenu").click(function(e){
            e.preventDefault();
            $("#logoutForm").submit();
       });
    });
});
 



