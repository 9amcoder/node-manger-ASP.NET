# NodeManager API

This is a ASP.Net Web API project that enables Network Operations teams to monitor and manage various nodes within a network.

## Features

* Add/Remove nodes
* Bring a node online/offline
* Check if a node is online or offline
* View telemetry data from nodes
* Set maximum threshold values for node telemetry metrics
* Receive an alarm when metrics exceed maximum limits

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

What things you need to install the software.

* .NET 7

### Fonder Structure

```
<root project folder>/
|-- Controllers/
|   |-- NodesController.cs
|-- Services/
|   |-- Interfaces/
|   |   |-- INodeService.cs
|   |-- NodeService.cs
|-- Models/
|   |-- Interfaces/
|   |   |-- INode.cs
|   |   |-- INodeManager.cs
|   |-- Classes/
|   |   |-- Node.cs
|   |   |-- NodeManager.cs
|-- Helpers/
|-- Startup.cs
|-- Program.cs
|-- README.md
|-- .gitignore
|-- appsettings.json

```

### Description of the files:

* Controllers/NodesController.cs: In the MVC model, this is where define endpoints for the node operations.

* Services/Interfaces/INodeService.cs: A service interface that defines the contract for the Service layer.

* Services/NodeService.cs: The concrete implementation of INodeService interface, using INodeManager to manipulate Node entities.

* Models/Interfaces/INode.cs, Models/Interfaces/INodeManager.cs: The provided interfaces.

* Models/Classes/Node.cs, Models/Classes/NodeManager.cs: The provided concrete implementation of those interfaces.

* Helpers/: Store utility classes and methods, such as custom exception handlers, here.

* Startup.cs: The default file created by .NET Core for setup and initial configurations.

* Program.cs: The entry point into the .NET Core application.

* README.md: Documentation on how to setup and run the project.

* .gitignore: To tell Git what files and folders not to track or handle.

* appsettings.json: .NET configuration file.


### Installing

Follow the steps to run this project in your local machine.

1. Clone the repository.

git clone https://github.com/username/NodeManagerAPI.git

2. Navigate to the project directory.
dotnet build
dotnet run

### Running the tests

dotnet test

### Build with

* .NET 7
* ASP.NET WEB API
* MongoDB

### Author

* Steve Sultan
