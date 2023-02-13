//Modelos
var dictionary = {
	security: {
		userLogon: "Majo",
		userPassword: "12345"
	}
};

//Módulo
(function() {
	var app = angular.module('SifBranch', []);

	//Directivas
	app.directive('username', function () {
		return {
			restrict: 'E',
			templateUrl: 'components/Username.html'
		};
	});

	app.directive('password', function () {
		return {
			restrict: 'E',
			templateUrl: 'components/Password.html'
		};
	});

	app.directive('sifErrorDialog', function () {
		return {
			restrict: 'E',
			templateUrl: 'components/sif.errorDialog.html'
		};
	});

	app.directive('sifWaitDialog', function () {
		return {
			restrict: 'E',
			templateUrl: 'components/sif.waitDialog.html'
		};
	});

	app.directive('sifConfirmDialog', function () {
		return {
			restrict: 'E',
			templateUrl: 'components/sif.confirmDialog.html'
		};
	});

	app.directive('sifContinueTrx', function () {
		return {
			restrict: 'E',
			templateUrl: 'components/sif.continueTrx.html'
		};
	});
}) ();