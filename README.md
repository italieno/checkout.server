# checkout.server
REST Api designed to handle a shopping list hosted in [azure](https://checkout-shopping-server.azurewebsites.net)

### Main Features

- ***OAuth 2.0*** Authentication (client_credentials/password):
- Full Shopping List
- Item Details
- Item Addition (auth)
- Item Removal (auth)
- Item Update (auth)
- Shoping List Reset (available only for "client_credentials" auth)
- Compatible with [checkout-net-library 2.0](https://github.com/italieno/checkout-net-library)
- Azure ARM template deployment
- Comprehensive Unit Tests

### Ideas for Future Improvements
-   Event Broadcasting
-   Thread Safe InMemory Repository
-   Logging
-   Pagination