# Timelogger (server)
Timeogger allows freelancers to track their time spent on projects, generate project overviews, and sort projects by deadline. The application adheres to object-oriented programming (OOP) best practices, including encapsulation, abstraction, dependency injection (DI), loose coupling, and the repository pattern.

## Architecture
The application follows a clean architecture, consisting of the following components:

1. Core Layer: Contains the core functionality of the application, business rules, data validation, entities, DTOs, interfaces ...
2. Infrastructure Layer: Interacts with the underlying data storage (in this case, an in-memory database) using repositories.
3. Presentation Layer: Handles user interaction and displays information using Swagger, user input validation

## Key Design Decisions
1. Encapsulation: Data and behavior are encapsulated within classes to ensure data integrity and maintainability.
2. Abstraction: The application uses interfaces to define contracts between modules, promoting loose coupling and flexibility.
3. Dependency Injection: Dependencies are injected into classes, making them easier to test and maintain.
4. Repository Pattern: The repository pattern is used to abstract data access, making the application more flexible and testable.
5. Data Validation: Input validation is implemented to ensure data integrity and prevent errors.
6. Testing: Unit tests  are written to ensure the correctness and reliability of the application.

## Future Considerations
As the application grows in complexity, it may be beneficial to consider the following:

1. Database Integration: If the application needs to store a large amount of data, it may be necessary to integrate with a database.
2. Authentication and Authorization: If the application needs to be accessible to multiple users, authentication and authorization mechanisms can be implemented.
3. Scalability: As the application grows in usage, it may be necessary to consider scalability factors such as caching.
