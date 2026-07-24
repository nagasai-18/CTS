# Hands On 3 - Custom Models and Filters

This folder contains the third Web API exercise.

## Focus

- Custom `Employee` model with nested `Department` and `Skill`
- `FromBody` usage for request payloads
- `AllowAnonymous` on GET actions
- `ActionFilterAttribute`-based auth filter
- `IExceptionFilter`-based exception filter
- Swagger visibility for success and error responses

## Run

1. Open this folder in a terminal.
2. Run `dotnet run --launch-profile http`.
3. Open `http://localhost:5163/swagger`.

## Notes

- `Filters/CustomAuthFilter.cs` checks for `Authorization: Bearer ...`.
- `Filters/CustomExceptionFilter.cs` writes exception details to `custom-exceptions.log`.
- `Controllers/EmployeeController.cs` contains read/write actions and a throwing GET endpoint.
- The controller uses `AllowAnonymous` for GET routes and `ProducesResponseType` for Swagger metadata.