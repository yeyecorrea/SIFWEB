/*Métodos y funciones para interactuar con Bootstrap*/

String.isNullOrEmpty = function (value) {
	return (!value || value == undefined || value == "" || value.length == 0);
}

function showMessage($http, $scope, messageCode, closeAction, replacements) {
	/*if (!$("element").data('bs.modal')?.isShown) {*/
	$("#errorDialog").modal("hide");

	SIF = $scope.SIF;
	SIF.errorCode = messageCode;

	$http.post("http://localhost:12345/messageGet", {
		timeout: serviceTimeout, params: messageCode
	})
		.then(
			function successCallback(response) {
				SIF.errorMessage = response.data.Message.Text;
				$("#errorDialog").unbind("shown.bs.modal");
				$("#errorDismiss").unbind("keypress");
				$("#errorDismiss").unbind("click");
				$("#errorDialog").on('shown.bs.modal', function () {
					$("#errorDismiss").focus();
					$("#errorDismiss").on("keypress click", function (e) {
						if (e.which == 13 ||
							e.which == 27 ||
							e.which == 32 ||
							e.type == "click") {
							$("#errorDialog").modal("hide");
							if (closeAction != null && closeAction != undefined) {
								$scope.$apply(function () {
									closeAction();
									closeAction = null;
								});
							}

						}
					});
				});
				$("#errorDialog").modal();
			},
			function errorCallback(response) {
				console.log("Error al obtener mensaje: " + response);
			});

}
//}

function showWaitDialog($http, $scope, messageText, messageCode, replacements) {
	closeWaitDialog($scope);
	SIF = $scope.SIF;
	if (!String.isNullOrEmpty(messageText)) {
		SIF.waitMessageText = messageText;
		SIF.showWaitMessage = true;
	}
	else if (!String.isNullOrEmpty(messageCode)) {
		$http.post("http://localhost:12345/messageGet", {
			timeout: serviceTimeout,
			params: messageCode
		})
			.then(
				function successCallback(response) {
					SIF.errorCode = messageCode;
					SIF.waitMessageText = response.data.Message.Text;
					SIF.showWaitMessage = true;

				}, function errorCallback(response) {
					console.log("Error al obtener el mensaje: " + response);
				});
	} else {
		SIF.waitMessageText = "Procesando información...";
		SIF.showWaitMessage = true;
	}
}

function closeWaitDialog($scope) {
	if ($scope.SIF.showWaitMessage) {
		$scope.SIF.showWaitMessage = false;
		$("div.modal-backdrop").remove();
	}
}

function userConfirm($http, $scope, confirmationMessage, defaultButton, yesPressed, noPressed, replacements, automaticClose) {
	closeWaitDialog($scope);
	if ($("modal-open").length == 0) {
		SIF = $scope.SIF;
		$http.post("http://localhost:12345/messageGet", {
			timeout: serviceTimeout, params: confirmationMessage
		})
			.then(
				function successCallback(response) {
					SIF.messageCode = confirmationMessage;
					SIF.confirmationMessage = response.data.Message.Text;
					$("#confirmDialog").unbind("shown.bs.modal");
					$("#confirmDialog").unbind("mousedown");
					$("#confirmDialog").unbind("keypress");
					$("#confirmDialog").on("shown.bs.modal", function () {
						if (defaultButton == 1) {
							$("#confirmOk").focus();
						} else if (defaultButton == 2) {
							$("#confirmCancel").focus();
						}
						$("#confirmDialog").mousedown(function (e) {
							if (e != null && e != undefined && e.target != null && e.target != undefined) {
								//Solo si se da click en los botones
								if (e.target.id == "confirmOk" || e.target.id == "confirmCancel") {
									if (automaticClose == null || automaticClose == true) {
										$("#confirmDialog").modal("hide");
									}
									if (e.target.id == "confirmOk") {
										yesPressed.call($scope.SIF);
									} else if (e.target.id == "confirmCancel") {
										noPressed.call($scope.SIF);
									}
								}
							}
							return;
						});
						$("#confirmDialog").keypress(function (e) {
							switch (e.which) {
								// Presionaron N
								case 78:
								case 110:
									try {
										$("#confirmDialog").modal("hide");
										noPressed.call($scope.SIF);
									} catch (e) {
										console.log(e.message);
									}
									break;
								case 83:
								case 115:
									try {
										$("#confirmDialog").modal("hide");
										yesPressed.call($scope.SIF);
									} catch (e) {
										console.log(e.message);
									}
									break;
								case 13:
								case 32:
									if (document.activeElement != null && document.activeElement != undefined) {
										if (document.activeElement.id == "confirmOk") {
											try {
												$("#confirmDialog").modal("hide");
												yesPressed.call($scope.SIF);
											} catch (e) {
												generateError(e.message, 'confirmDialog', e.name);
											}
										}
										else if (document.activeElement.id == "confirmCancel") {
											try {
												e.preventDefault();
												$("#confirmDialog").modal("hide");
												noPressed.call($scope.SIF);
											} catch (e) {
												console.log("Error: " + e);
											}
										}
									}
									break;
								default:
									if (e.preventDefault != null && e.preventDefault != undefined) {
										e.preventDefault();
										e.stopPropagation();
									}
							}
							return;
						});
					});
					$("#confirmDialog").modal();

				}, function errorCallback(response) {
					console.log("Error al mostrar el diálogo: " + response);
				});
	}
}

function userContinueConfirm($http, $scope, continueAction, close, callEscape, closeAction) {
	closeWaitDialog($scope);
	if ($(".modal-open").length == 0) {

		$("#confirmTransaction").keypress(function (e) {
			if (e.keyCode == 78) {
				$("#confirmTransaction").modal("hide");
				$("#confirmTransaction").unbind("keydown");
				if (closeAction) {
					closeAction.call($scope.SIF);
				} else {
					webApiInvoke('', 'closeTransaction', $http, $scope, "");
				}
				return;
			}
			if (e.keyCode == 83 || e.keyCode == 13) {
				$(this).off('shown.bs.modal');
				$("#confirmTransaction").modal("hide");
				$("#confirmTransaction").unbind("keydown");
				if (continueAction != undefined && continueAction != null) {
					continueAction.call($scope.SIF);
				}
				return;
			}
			if (e.keyCode == 27) {
				if (close) {
					$("#confirmTransaction").modal("hide");
					$("#confirmTransaction").unbind("keydown");
					webApiInvoke('', 'closeTransaction', $http, $scope, "");
				} else {
					$(this).off('shown.bs.modal');
					$("#confirmTransaction").modal("hide");
					$("#confirmTransaction").unbind("keydown");
					$(':input:enabled:visible:not([readonly]).first').focus();
					if (callEscape != undefined && callEscape != null) {
						callEscape.call($scope.SIF);
					}
				}
				return;
			}
		});
		$("#confirmTransaction").modal();
	}
}