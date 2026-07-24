# Hands On 5 - CORS and JWT Security

This folder contains the JWT authentication and CORS exercise.

## Focus

- CORS enablement for local Web API access
- JWT generation and validation
- `Authorize` and `AllowAnonymous`
- Role-based authorization with claims
- `Bearer` token handling in Postman

## Run

1. Open this folder in a terminal.
2. Run `dotnet run --launch-profile http`.
3. Open `http://localhost:5165/swagger`.
4. Use `GET /api/auth/token` to generate a JWT.
5. Send the token as `Authorization: Bearer <token>` in Postman.

## What To Test

- `GET /api/auth/token` returns a JWT for role `Admin`.
- `GET /api/employee` succeeds with a valid token and roles `Admin,POC`.
- `GET /api/employee/poc-only` requires role `POC` and therefore does not allow an `Admin` token.
- Invalid or missing tokens return `401 Unauthorized`.
- A valid token with the wrong role returns `403 Forbidden`.

## Notes

- The JWT expires after 2 minutes in `AuthController`.
- CORS is enabled in `Program.cs` with a `LocalApp` policy.
- In ASP.NET Core, `Program.cs` is the equivalent place for the Startup-style configuration shown in earlier Web API samples.