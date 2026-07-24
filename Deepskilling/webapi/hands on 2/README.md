# Hands On 2 - Web API with Swagger and Postman

This folder contains the Swagger-enabled Web API exercise.

## Focus

- Swagger installation with `Swashbuckle.AspNetCore`
- `AddSwaggerGen` and `UseSwaggerUI`
- `ProducesResponseType` on API actions
- Postman request structure and collection usage
- Route naming and `ActionName` examples
- Employee controller route changed to `api/Emp`

## Run

1. Open this folder in a terminal.
2. Run `dotnet run --launch-profile http`.
3. Open `http://localhost:5162/swagger`.
4. Import `Postman/WebApiDemo.postman_collection.json` into Postman.

## Notes

- `Program.cs` wires Swagger and controller routing.
- `Controllers/ValuesController.cs` and `Controllers/EmployeeController.cs` are exposed in Swagger.
- The Employee endpoints use the `api/Emp` route for Postman testing.