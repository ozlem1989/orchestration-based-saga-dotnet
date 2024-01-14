# orchestration-saga-dotnet

.NET Core 3.1
4 console applications. 
HttpClientFactory library used to call apis. 

SAGA.Orchestrator is the orchestrator, state machine. The scenerio is that a user clicks a button to order a product. (Postman example is located below.)

Postman Example : 
Url (POST)
https://localhost:44308/api/Orchestrator
Request : 
{
    "ProductName" : "test"
}

The services are called in the following order :  Order =>  Inventory => Notify 
But there is an error in the Inventory service, therefore it will catch and call Order/Delete api to delete created order. (compensable transaction) 



