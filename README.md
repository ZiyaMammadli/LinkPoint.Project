# LinkPoint Social Media

## Description

LinkPoint is a **social media platform** developed using **ASP.NET Core 8**. This project includes both **MVC** and **API**, where the MVC consumes the API endpoints. The application follows the **Onion Architecture** with layers:
- **API** (Handles HTTP requests and responses)
- **Business** (Contains business logic and services)
- **Core** (Includes domain entities and interfaces)
- **Data** (Handles database interactions with EF Core)

Additionally, an **Admin Panel** is implemented for managing the platform.

## Features

- **User Authentication** (JWT-based login, registration, and token management)
- **Post Management** (Text, images, videos, likes, and comments)
- **Real-time Messaging** (User-to-user chat functionality)
- **Friendship System** (Friend requests and connections)
- **Admin Panel** (Manage users, posts, and reports)
- **Validation & Exception Handling** (FluentValidation & global error handling)
- **Rate Limiting** (Prevents brute-force attacks)
- **Google Cloud Storage** (For storing images and videos)
- **Email Service** (For user notifications and password recovery)
- **Bootstrap & SweetAlert** (Enhanced UI and user notifications)
- **SignalR** (Real-time communication for messaging and notifications)
- **MVC Architecture** (Used for the frontend structure)

## Technologies Used

- **ASP.NET Core 8**
- **Entity Framework Core**
- **Repository Pattern**
- **Google Cloud Storage**
- **FluentValidation**
- **Rate Limiting Middleware**
- **Bootstrap** (Frontend styling)
- **SweetAlert** (User-friendly notifications)
- **Email Service** (SMTP-based notifications)
- **SignalR** (Real-time WebSocket communication)
- **MVC Pattern**

## Installation

1. Clone the repository:

```bash
git clone https://github.com/ZiyaMammadli/LinkPoint.Project.git
cd LinkPoint
```

2. Configure **appsettings.json** with your database and Google Cloud settings.
3. Apply migrations:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

4. Run the project:

```bash
dotnet run
```

## Database Schema

The following tables are included in the database:

- **Users**
- **UserAbout**
- **UserWork**
- **UserEducation**
- **UserInterests**
- **FriendShips**
- **Posts**
- **Images**
- **Videos**
- **Likes**
- **Comments**
- **Messages**
- **Conversations**
- **ContactMessages**
  

## Security

- **JWT Token**: Secure API endpoints
- **Input Validation**: All requests validated using FluentValidation
- **Rate Limiting**: Prevents brute-force attacks
- **Exception Handling**: Structured error responses
- **HTTPS**: Enforced in production mode

## Usage

To authenticate and access protected endpoints:

1. Obtain JWT token from `/api/auth/login`.
2. Include the token in the **Authorization** header as:

```bash
Authorization: Bearer {your_token}
```

## Contact

For questions or issues, please reach out to:

- Email: [ziyam040@gmail.com](mailto:ziyam040@gmail.com)
- GitHub: [Profile](https://github.com/ZiyaMammadli)

## License

This project is licensed under the MIT License.

