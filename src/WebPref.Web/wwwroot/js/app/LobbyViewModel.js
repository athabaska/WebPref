define([
    'lib/knockout/knockout-min',
    'jquery'],
    function (ko, jquery) {
        function LobbyViewModel() {

            this.tables = ko.observableArray();
            this.currentTable = ko.observable();

            this.getTableList = jquery.proxy(this.getTableList, this);
            this.refreshTables = jquery.proxy(this.refreshTables, this);
            this.processTableCreation = jquery.proxy(this.processTableCreation, this);
            this.getCurrentTableInfo = jquery.proxy(this.getCurrentTableInfo, this);
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

        LobbyViewModel.prototype.createTable = function () {
            var params = { "testParam1": "testValue1" };
            jquery.ajax({
                type: 'POST',
                url: 'https://localhost:44394/api/Lobby/CreateTable',
                contentType: 'application/json',
                data: JSON.stringify(params),
                success: this.processTableCreation
            });
        };

        LobbyViewModel.prototype.processTableCreation = function (result) {
            if (result.success) {
                var table = JSON.parse(result.data);
                this.currentTable(table);
                this.getTableList();
            } else {
                alert("Не удалось создать стол: " + result.description);
            }
        };

        LobbyViewModel.prototype.getCurrentTableInfo = function () {
            if (!this.currentTable()) {
                return "Нет выбранного стола";
            } else {
                var table = this.currentTable();
                var result = "Выбран стол № " + table.Name;
                return result;
            }
        };

        LobbyViewModel.prototype.goToTable = function () {
            if (!this.currentTable()) {
                alert("Вы не сидите за столом");
            } else {
                alert("Переходим за стол");
            }
        }

        return LobbyViewModel;
    });