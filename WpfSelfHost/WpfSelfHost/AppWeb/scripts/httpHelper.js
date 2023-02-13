var serviceTimeout = 30000;

function webApiInvoke(productName, webApiUrl, $http, $scope, successAction, rejectionAction) {

	var SIF = $scope.SIF;
	showWaitDialog($http, $scope, null, null, null);
	$http.post("http://localhost:12345/" + webApiUrl,
		{
			timeout: serviceTimeout, params: SIF
		})
		.then(function successCallback(response) {
			console.log(response);
			processResponseWebApiInvoke(webApiUrl, response, $http, $scope, successAction, rejectionAction);

		}, function errorCallback(response) {
			console.log(response);
		});
}

function processResponseWebApiInvoke(webApiUrl, response, $http, $scope, successAction, rejectionAction) {
	closeWaitDialog($scope);
	//Si la respuesta no es un String, debe ser un objeto.
	if (typeof (response.data) != "string") {
		//Si el estado fue aceptado (0)
		if (response.data.State == 0) {
			if (successAction != undefined && successAction != null) {
				successAction.call($scope.SIF);
			}
		} else {
			//El estado fue diferente a aceptado.
			if (rejectionAction != null && rejectionAction != undefined) {
				$scope.SIF.Message = response.data.Message;
				rejectionAction.call($scope.SIF);
			}
		}
	}
}