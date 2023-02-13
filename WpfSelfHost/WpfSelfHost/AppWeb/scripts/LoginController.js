(function () {
	var app = angular.module('LoginModule', ['SifBranch']);
	app.controller('LoginController', ['$scope', '$http',
		function ($scope, $http) {
			this.dictionary = dictionary;
			this.securityLabels = securityLabels;
			this.sifLabels = sifLabels;

			this.submit = function () {
				webApiInvoke("security", "Login", $http, $scope,
					onSuccess = function () {
						//Invocar el mensaje de confirmación
						userConfirm($http, $scope, "U0001", 1, function yesPressed() {
							console.log("Se ingresa");
							webApiInvoke("", "closeTransaction", $http, $scope);
						},
							function noPressed() {
								console.log("No se ingresa");
							});
					},
					onRejected = function () {
						console.log("Login rechazado!");
						showMessage($http, $scope, "E0001", onClose = function () {
							webApiInvoke("", "closeTransaction", $http, $scope);
						}, null);
					});
			}

			this.cancel = function () {
				userContinueConfirm($http, $scope, continueAction = function () {
					console.log("El usuario quiere continuar.")
				}, function noPressed() {
					webApiInvoke("", "closeTransaction", $http, $scope, "");
				});
			}

		}
	]);
})();