# TrueStoryAssignment

## Overview
This project is a simple .NET 8 Web API that integrates with https://restful-api.dev/. It adds:
- Filtering by product name (substring)
- Pagination
- POST, PUT, PATCH, and DELETE operations

## Setup Instructions
1. Clone the repository.
2. Open the solution in Visual Studio 2022 or later.
3. Build and run the project.
4. Swagger UI will be available at https://localhost:{port}/swagger

## API Endpoints
- `GET /api/Product?nameFilter=...&page=1&pageSize=10`
- `POST /api/Product` with JSON body:
```json
{
  "name": "Product Name",
  "data": {
    "key1": "value1",
    "key2": 123
  }
}
```
- `PUT /api/Product/{id}`
- `PATCH /api/Product/{id}`
- `DELETE /api/Product/{id}`
