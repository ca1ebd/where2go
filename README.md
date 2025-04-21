# Where2Go - Place Sharing Platform

A simple and fast platform for sharing places with friends, featuring both Google Maps and Apple Maps integration.

## Features

- Create and share places with unique URLs
- Automatic generation of Google Maps and Apple Maps links
- Optional descriptions and special instructions
- Image upload support
- Simple management with password protection
- Mobile-responsive design

## Tech Stack

- Backend: C#/.NET Core API
- Frontend: React
- Database: PostgreSQL
- Infrastructure: Azure Functions

## Project Structure

```
where2go/
├── backend/           # C#/.NET Core API
├── frontend/          # React SPA
└── infrastructure/    # Terraform configurations
```

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- Node.js 18+
- PostgreSQL
- Azure CLI (for deployment)

### Development Setup

1. Clone the repository
2. Set up the database
3. Configure environment variables
4. Run the backend and frontend applications

### Environment Configuration

1. Copy the example environment files:
   ```bash
   cp backend/appsettings.example.json backend/appsettings.json
   cp .env.example .env
   ```

2. Update the `.env` file with your local configuration:
   - Set the correct path to your .NET tools
   - Configure your database connection string
   - Add any other required environment variables

3. The application will use the following environment variables:
   - `DATABASE_URL`: PostgreSQL connection string
   - Additional variables may be required for specific features

### Security Notes

- Never commit `.env` or `appsettings.json` files containing real credentials
- Use different credentials for development, testing, and production
- Keep your management passwords secure and never share them

## License

MIT
