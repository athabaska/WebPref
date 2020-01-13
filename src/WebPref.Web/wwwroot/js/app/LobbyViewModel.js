﻿define([
    'lib/knockout/knockout-min',
    'jquery'],
    function (ko, jquery) {
        function LobbyViewModel() {

            this.tables = ko.observableArray();

            this.getTableList = jquery.proxy(this.getTableList, this);
            this.refreshTables = jquery.proxy(this.refreshTables, this);
        }

        LobbyViewModel.prototype.getTableList = function () {
            jquery.getJSON('https://localhost:44394/api/Lobby/GetTableList')
                .done(this.refreshTables)
                .fail(function (jqxhr, textStatus, error) {
                    var err = textStatus + ", " + error;
                    alert("Ошибка получения списка столов: " + err);
                });
        };

        LobbyViewModel.prototype.refreshTables = function (tableList) {
            this.tables.removeAll();
            for (var i = 0; i < tableList.length; i++) {
                this.tables.push(tableList[i]);
            }

        };

        return LobbyViewModel;
    });