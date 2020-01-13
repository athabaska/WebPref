define(function (require) {
    require([
        'lib/knockout/knockout-min',
        'jquery',
        'js/app/LobbyViewModel',
        'lib/domReady!'], function (ko, jquery, lobbyViewModel) {
            alert('I am started!!!');  

            //регистрация модулей, их пока не придумали

            //проверяем, есть ли авторизация, пока не очень ясно, как

            //
            var model = new lobbyViewModel();
            ko.applyBindings(model);
    });    
});