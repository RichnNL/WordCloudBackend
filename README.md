# WordCloudBackend

WordCloudBackend is a lightweight backend service designed to parse text and count the frequency of each word. It provides an API that takes a string of text and returns a list of words alongside their respective counts, sorted by frequency.

## Live Demo
You can interact with and test the API live via its Swagger UI:
[http://richard-dyer.runasp.net/swagger/index.html](http://richard-dyer.runasp.net/swagger/index.html)

## Architecture

This project is structured using a **Vertical Slice Architecture**. 

Because this is a relatively small service focused on a specific domain (Word Counting), vertical slicing allows us to keep all related files—such as Models, Commands, Services, and Extensions—grouped together by feature rather than by technical layer. This approach minimizes cognitive load, making the codebase easier to navigate and maintain as features evolve independently.

### Features
* **Word Counter:** The core feature slice. It contains the business logic for parsing text, removing unwanted characters, and accurately counting word occurrences.

## Tech Stack
* .NET 10
* Minimal APIs
* Mediator Pattern (Custom implementation for simplicity)
* Swagger/OpenAPI for documentation and testing