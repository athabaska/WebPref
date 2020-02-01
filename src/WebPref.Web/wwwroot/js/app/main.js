define(function (require) {
    require([
        'lib/knockout/knockout-min',
        'jquery',
        'js/app/LobbyViewModel',
        'lib/domReady!'], function (ko, jquery, lobbyViewModel) {            

            //регистрация модулей, их пока не придумали

            //авторизацию отдельно проверять не надо, в данных сессии будет, если она пройдена
            //если зайти без авторизации, то контроллеры будут выдавать ошибку

            //
            var model = new lobbyViewModel();
            ko.applyBindings(model);
    });    
});