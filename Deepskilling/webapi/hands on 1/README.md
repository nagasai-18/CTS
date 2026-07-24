# Hands On 1 - Basic Web API

This folder contains the simple ASP.NET Core Web API exercise.

## Focus

- RESTful Web API basics
- HttpRequest and HttpResponse
- HttpGet, HttpPost, HttpPut, HttpDelete
- Status codes such as 200, 400, 401, 500
- Controller-based routing with `ValuesController`

## Run

1. Open this folder in a terminal.
2. Run `dotnet run --launch-profile http`.
3. Open `http://localhost:5162/api/values`.

## Notes

- `Program.cs` contains the minimal pipeline.
- `Controllers/ValuesController.cs` contains the read/write actions.
- No Swagger or Postman setup is included here.