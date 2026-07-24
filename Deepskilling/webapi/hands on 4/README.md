# Hands On 4 - CRUD Web API

This folder contains the CRUD exercise focused on create, update, and delete operations.

## Focus

- Custom `Employee` model
- `FromBody` for POST and PUT request bodies
- Hardcoded employee list
- `PUT` validation for invalid ids
- Swagger UI for testing
- Postman collection for request testing

## Run

1. Open this folder in a terminal.
2. Run `dotnet run --launch-profile http`.
3. Open `http://localhost:5164/swagger`.
4. Import `Postman/WebApiDemo.postman_collection.json` into Postman if needed.

## Notes

- `PUT /api/employee/{id}` returns the updated employee as `ActionResult<Employee>`.
- Invalid ids return `BadRequest("Invalid employee id")`.
- Updates and deletes are applied to the in-memory employee list.