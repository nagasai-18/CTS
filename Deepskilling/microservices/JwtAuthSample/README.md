# JwtAuthSample

A minimal ASP.NET Core Web API sample that demonstrates:

- JWT authentication
- `[Authorize]`-protected endpoints
- role-based authorization with `Admin`
- custom handling for expired and unauthorized tokens

## Requirements

- .NET 8 SDK

## Run

```bash
cd JwtAuthSample
dotnet restore
dotnet run
```

## Test Flow

1. Open Swagger UI.
2. Call `POST /api/auth/login` with one of these credentials:
  - `admin` / `admin123`
  - `user` / `user123`

Note: the sample uses a 32-character HS256 signing key in `appsettings.json` so token creation works on every run.
3. Copy the returned token.
4. Use the token in the Swagger authorization dialog as a Bearer token.
5. Call `GET /api/secure/data`.
6. Call `GET /api/admin/dashboard` with the `admin` token to verify role-based access.

## Expected Output Examples

### Successful login
```json
{
  "token": "eyJhbGciOiJIUzI1NiIs..."
}
```

### Protected endpoint
```text
This is protected data.
```

### Admin endpoint
```text
Welcome to the admin dashboard.
```

### Unauthorized response
```json
{
  "message": "Unauthorized access. Please provide a valid token."
}
```

### Forbidden response
```json
{
  "message": "You do not have permission to access this resource."
}
```

### Expired token header
`Token-Expired: true`
