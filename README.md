Here's a revised README format tailored to your project structure, similar to the format you provided for "Simplified Blockchain-Golang":

---

<div align="center">

  <h1>Parky</h1>
  Parky is a web application developed using .NET Core WebAPI and .NET Core MVC, designed for mountain climbing enthusiasts to select routes and embark on adventures. The application offers RESTful APIs with user authentication and authorization features, allowing both casual users and administrators to interact with the platform in different ways.
  
  <p>
     Author: :copyright: Nguyễn Cao Nam Vũ - 20127670 
  </p>

<p>
  <a href="https://github.com/NoNameNo1F/Parky/graphs/contributors">
    <img src="https://img.shields.io/github/contributors/NoNameNo1F/Parky" alt="contributors" />
  </a>
  <a href="">
    <img src="https://img.shields.io/github/last-commit/NoNameNo1F/Parky" alt="last update" />
  </a>
  <a href="https://github.com/NoNameNo1F/Parky/network/members">
    <img src="https://img.shields.io/github/forks/NoNameNo1F/Parky" alt="forks" />
  </a>
  <a href="https://github.com/NoNameNo1F/Parky/stargazers">
    <img src="https://img.shields.io/github/stars/NoNameNo1F/Parky" alt="stars" />
  </a>
  <a href="https://github.com/NoNameNo1F/Parky/issues/">
    <img src="https://img.shields.io/github/issues/NoNameNo1F/Parky" alt="open issues" />
  </a>
  <a href="https://github.com/NoNameNo1F/Parky/blob/master/LICENSE">
    <img src="https://img.shields.io/github/license/NoNameNo1F/Parky.svg" alt="license" />
  </a>
</p>

<h4>
    <a href="https://github.com/NoNameNo1F/Parky">View Demo</a>
  <span> · </span>
    <a href="https://github.com/NoNameNo1F/Parky">Documentation</a>
  <span> · </span>
    <a href="https://github.com/NoNameNo1F/Parky/issues">Report Bug</a>
  <span> · </span>
    <a href="https://github.com/NoNameNo1F/Parky/issues">Request Feature</a>
  </h4>
</div>

# :notebook_with_decorative_cover: Table of Contents

1. [About the Project](#about-the-project)
2. [Key Features](#key-features)
3. [Getting Started](#getting-started)
   - [Prerequisites](#prerequisites)
   - [Installation](#installation)
   - [Usage](#usage)
4. [API Endpoints](#api-endpoints)
5. [Contributing](#contributing)
6. [License](#license)
7. [Contact](#contact)

# :star2: About the Project

Parky is a web-based application developed to cater to mountain climbing enthusiasts by providing route selection, user authentication, and administrative management of routes. Built on .NET Core WebAPI and .NET Core MVC, the app offers a responsive UI and secure backend services.

## :dart: Key Features

- **Route Selection:** Explore and select climbing routes tailored to your preferences.
- **Filtering Options:** Use filters to find routes based on various criteria.
- **User Authentication:** Secure login system for casual users and administrators.
- **Admin Management:** Admins can add, edit, and delete route information.
- **Responsive UI:** Built with .NET Core MVC for a seamless user experience.

# :rocket: Getting Started

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0 or above)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) for the backend database
- [Visual Studio](https://visualstudio.microsoft.com/) or any compatible IDE for development

## Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/NoNameNo1F/Parky.git
   cd parky
   ```

2. Set up the database by running the provided SQL scripts in the `/Database` folder.

3. Update the `appsettings.json` file with your database connection string.

4. Build and run the application:

   ```bash
   dotnet build
   dotnet run
   ```

## Usage

- Access the web application at `http://localhost:5000`.
- Use the login page to sign in as a user or an admin.
- Navigate through the routes, use filters to customize your search, and manage your climbing plans.
- Administrators can manage route data through the admin dashboard.

# :hammer: API Endpoints

### National Parks Endpoints

- **GET /api/v{version}/nationalparks** - Retrieve a list of all national parks.
- **POST /api/v{version}/nationalparks** - Create a new national park.
- **GET /api/v{version}/nationalparks/{nationalParkId}** - Retrieve details of a specific national park by its ID.
- **PATCH /api/v{version}/nationalparks/{nationalParkId}** - Update information for a specific national park.
- **DELETE /api/v{version}/nationalparks/{nationalParkId}** - Delete a specific national park by its ID.

### Trails Endpoints

- **GET /api/v{version}/trails** - Retrieve a list of all trails.
- **POST /api/v{version}/trails** - Create a new trail.
- **GET /api/v{version}/trails/{trailId}** - Retrieve details of a specific trail by its ID.
- **PATCH /api/v{version}/trails/{trailId}** - Update information for a specific trail.
- **DELETE /api/v{version}/trails/{trailId}** - Delete a specific trail by its ID.
- **GET /api/v{version}/trails/GetTrailsInNationalPark/{nationalParkId}** - Retrieve trails located within a specific national park.

### Users Endpoints

- **POST /api/v{version}/users/authenticate** - Authenticate a user and provide access tokens for secured actions.
- **POST /api/v{version}/users/register** - Register a new user account.

# :handshake: Contributing

1. Fork the project.
2. Create your feature branch (`git checkout -b feature/AmazingFeature`).
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`).
4. Push to the branch (`git push origin feature/AmazingFeature`).
5. Open a pull request.

# :memo: License

Distributed under the MIT License. See `LICENSE` for more information.

# :mailbox: Contact

- **Author**: Nguyễn Cao Nam Vũ - [GitHub](https://github.com/NoNameNo1F)
- **Project Link**: [Parky GitHub Repository](https://github.com/NoNameNo1F/Parky)

---

Feel free to reach out if you have any questions or suggestions regarding Parky!