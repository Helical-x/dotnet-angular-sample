# NoteSolution - A Notes Management Application

NoteSolution is a web application that allows you to create, read, update, and delete notes. It's built using .NET Core
for the backend and Angular for the frontend.

## Requirements

To run and use NoteSolution, you'll need the following prerequisites:

- [.NET Core SDK](https://dotnet.microsoft.com/download) - Version 7.0.305 or later
- [Node.js and npm](https://nodejs.org/) - Version v16.16.0 or later
- [Visual Studio Code](https://code.visualstudio.com/) or your preferred code editor
- [Docker](https://www.docker.com/get-started) - Version 20.10.0 or later
- [Docker Compose](https://docs.docker.com/compose/install/) - Version 1.29.0 or later

## Getting Started

Follow these steps to get NoteSolution up and running on your local machine:

1. Clone the repository:

   ```shell
   git clone https://github.com/camilapensolvers/GitHubHolisticDevelop-Ensolvers-Challenge.git
   
   ```

2. Run SQL Server with Docker Compose.
   ```shell
    docker-compose up -d
      ```

3. Run the application

    ```shell
   cd NoteSolution
   dotnet run
   ```
4. Go to https://localhost:7252.
5. User and password to login is `admin@example.com` & `Dotnet@example`
