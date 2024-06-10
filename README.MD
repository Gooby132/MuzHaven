# MuzHaven

MuzHaven is an ongoing project designed to facilitate collaboration among team mates and bands working on musical projects. Built using .NET for the backend and React for the frontend, MuzHaven aims to provide a robust platform for musicians to connect, share, and manage their musical endeavors. The project is developed with a distributed monolith architecture in mind, ensuring scalability and maintainability.

## Table of Contents
1. [Project Overview](#project-overview)
2. [Features](#features)
3. [Technology Stack](#technology-stack)
4. [Architecture](#architecture)
5. [Installation](#installation)
6. [Usage](#usage)
7. [Contributing](#contributing)
8. [License](#license)
9. [Contact](#contact)

## Project Overview
MuzHaven is a collaborative platform that allows musicians to:
- Connect with team members and bands.
- Share musical ideas and resources.
- Manage projects efficiently.

## Features
- **User Authentication**: Secure user registration and login.
- **Project Management**: Create and manage musical projects.
- **Collaboration Tools**: Share files, comment on stems, and track project progress.
- **Music Library**: Upload and organize music files.

## Technology Stack
- **Frontend**: React
- **Backend**: .NET
- **Database**: In Memory (All Domains)
- **Authentication**: JWT (JSON Web Tokens)
- **State Management**: Redux (for React)
- **Styling**: CSS Modules / Styled Components

## Architecture
MuzHaven follows a distributed monolith architecture. This approach allows the application to be modular while maintaining a single deployable unit. Key components include:

- **API Service**: Exposes endpoints for frontend consumption, and orchistrats all their interactions.
- **Permission Service**: Manages tokenizations and authorizations
- **Project Service**: Contains all project related functionalities.
- **Stem Service**: Contains all stem and files.
- **User Service**: Manages users and their musical persona

## Installation
### Prerequisites
- .NET SDK
- Node.js and npm

### Backend
1. Clone the repository:
   ```sh
   git clone https://github.com/Gooby132/MuzHaven.git
   ```

2. Restore .NET dependencies:
   ```sh
   dotnet restore
   ```

3. Run the backend server:
   ```sh
   dotnet run
   ```

### Frontend
1. Navigate to the frontend directory:
   ```sh
   cd ../web.client
   ```

2. Install npm dependencies:
   ```sh
   npm install
   ```

3. Start the frontend server:
   ```sh
   npm start
   ```

## Usage
1. Open your browser and navigate to `http://localhost:3000`.
2. Register a new user account or log in with existing credentials.
3. Create a new project and upload stems for your next song.
4. Start collaborating by sharing files, commenting, and tracking progress.
a
## Contributing
We welcome contributions to MuzHaven! To contribute:

1. Fork the repository.
2. Create a new feature branch.
3. Commit your changes.
4. Push to your fork and submit a pull request.

Please ensure your code follows the project's coding standards and includes relevant tests.

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more information.

## Contact
For questions or feedback, please contact the project maintainers:
- [Your Name](benlevy1999@walla.co.il)

Thank you for contributing to MuzHaven! Together, we can create an amazing platform for musicians.