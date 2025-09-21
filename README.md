# 🏨 Hotel Management System API

A comprehensive hotel management system with REST API for room booking, user management, and authentication.

## 📋 Table of Contents

- [Project Description](#project-description)
- [Architecture](#architecture)
- [Technologies](#technologies)
- [Installation](#installation)
- [Configuration](#configuration)
- [API Endpoints](#api-endpoints)
- [Project Structure](#project-structure)
- [Usage](#usage)

## 🎯 Project Description

Hotel Management System is a web API for hotel management that provides:
- User registration and authentication
- Room management (add, edit, delete)
- Room booking with availability checking
- Image upload for rooms
- Search available rooms by dates and parameters
- Price management by room type

## 🏗️ Architecture

The project is built following Clean Architecture principles with separation into three main layers:

- **API Layer** (`HotelHTTP.API`) - controllers, services, requests
- **Domain Layer** (`HotelHTTP.Domain`) - entities, interfaces
- **Infrastructure Layer** (`HotelHttp.Infrastracture`) - repositories, database

## 🛠️ Technologies

- **.NET 8.0** - main platform
- **ASP.NET Core Web API** - web framework
- **Entity Framework Core** - ORM
- **PostgreSQL** - database
- **JWT Bearer Authentication** - authentication
- **Swagger/OpenAPI** - API documentation
- **CORS** - cross-domain request support

## 🚀 Installation

### Prerequisites

- .NET 8.0 SDK
- PostgreSQL
- Git

### Installation Steps

1. **Clone the repository:**
```bash
git clone <repository-url>
cd Hotel_Server
```

2. **Install dependencies:**
```bash
dotnet restore
```

3. **Configure database:**
   - Create PostgreSQL database
   - Update connection string in `appsettings.json`

4. **Run migrations:**
```bash
dotnet ef database update --project HotelHttp.Infrastracture --startup-project HotelHTTP.API
```

5. **Run the project:**
```bash
dotnet run --project HotelHTTP.API
```

## ⚙️ Configuration

### appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnectionString": "Host=localhost;Database=HotelDB;Username=your_username;Password=your_password"
  },
  "Jwt": {
    "Key": "your-super-secret-key-here",
    "Issuer": "HotelAPI",
    "Audience": "HotelClients"
  }
}
```

### CORS

The API is configured to work with frontend on `http://localhost:5173`.

## 📡 API Endpoints

### Authentication (`/api/auth`)

| Method | Endpoint | Description | Authorization |
|--------|----------|-------------|---------------|
| POST | `/register` | User registration | ❌ |
| POST | `/login` | User login | ❌ |
| POST | `/logout` | User logout | ✅ |

### Rooms (`/api/rooms`)

| Method | Endpoint | Description | Authorization |
|--------|----------|-------------|---------------|
| GET | `/` | Get all rooms | ❌ |
| GET | `/{id}` | Get room by ID | ❌ |
| POST | `/` | Create room | ✅ |
| PUT | `/{id}` | Update room | ✅ |
| DELETE | `/{id}` | Delete room | ✅ |
| GET | `/available` | Search available rooms | ❌ |
| POST | `/update-prices` | Update prices by type | ✅ |

### Bookings (`/api/booking`)

| Method | Endpoint | Description | Authorization |
|--------|----------|-------------|---------------|
| GET | `/` | Get all bookings | ✅ |
| GET | `/user` | Get current user bookings | ✅ |
| POST | `/` | Create booking | ✅ |
| DELETE | `/{id}` | Cancel booking | ✅ |
| GET | `/bookings-per-day` | Bookings statistics by day | ✅ |

### Users (`/api/user`)

| Method | Endpoint | Description | Authorization |
|--------|----------|-------------|---------------|
| GET | `/me` | Get current user info | ✅ |

## 📁 Project Structure

```
Hotel_Server/
├── HotelHTTP.API/                 # API Layer
│   ├── Controllers/               # API Controllers
│   │   ├── AuthController.cs
│   │   ├── BookingController.cs
│   │   ├── RoomsController.cs
│   │   └── UserController.cs
│   ├── Requests/                  # Request Models
│   │   ├── BookingRequest.cs
│   │   ├── LoginRequest.cs
│   │   ├── RegisterRequest.cs
│   │   ├── RoomRequest.cs
│   │   └── UpdatePriceRequest.cs
│   ├── Service/                   # Business Logic
│   │   ├── AuthService.cs
│   │   ├── BookingService.cs
│   │   ├── RoomService.cs
│   │   └── TokenService.cs
│   ├── wwwroot/images/            # Room Images
│   └── Program.cs
├── HotelHTTP.Domain/              # Domain Layer
│   ├── Entities/                  # Entities
│   │   ├── User.cs
│   │   ├── Room.cs
│   │   └── Booking.cs
│   └── Interfaces/                # Interfaces
│       ├── Auth/
│       └── Repo/
└── HotelHttp.Infrastracture/      # Infrastructure Layer
    ├── Data/
    │   └── HotelDBContext.cs
    ├── Repositories/              # Repositories
    │   ├── BookingRepository.cs
    │   ├── RoomRepository.cs
    │   └── UserRepository.cs
    ├── Migrations/                # Database Migrations
    └── PasswordHasher.cs
```

## 💻 Usage

### User Registration

```http
POST /api/auth/register
Content-Type: application/json

{
  "name": "John Doe",
  "email": "john@example.com",
  "password": "password123",
  "role": "User"
}
```

### User Login

```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "john@example.com",
  "password": "password123"
}
```

### Search Available Rooms

```http
GET /api/rooms/available?checkInDate=2024-01-15&checkOutDate=2024-01-20&maxPrice=1000&capacity=2
```

### Create Booking

```http
POST /api/booking
Authorization: Bearer <token>
Content-Type: application/json

{
  "roomId": 1,
  "checkInDate": "2024-01-15T14:00:00Z",
  "checkOutDate": "2024-01-20T12:00:00Z"
}
```

## 🔐 Security

- JWT tokens stored in HTTP-only cookies
- Passwords hashed using BCrypt
- CORS configured for secure frontend communication
- Input validation for all data

## 📝 License

This project is distributed under the MIT License. See the [LICENSE](LICENSE) file for details.


