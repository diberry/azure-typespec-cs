# Build a .NET API server with TypeSpec and deploy to Azure

This project demonstrates how to create and deploy a RESTful API using TypeSpec and .NET, with Azure Cosmos DB for persistence and Azure Container Apps for hosting.

## What is TypeSpec?

TypeSpec is a language for describing cloud service APIs and generating other API description languages, client and service code, documentation, and other assets. TypeSpec provides highly extensible core language primitives that can describe API shapes common among REST, GraphQL, gRPC, and other protocols.

## Deploy this repo to Azure

You must have an Azure Subscription. If you don't have one, [create one here for free](https://azure.microsoft.com/free/). 

1. Open in [Codespaces](https://github.com/diberry/azure-typespec-cs) or [Visual Studio Developer Container](https://code.visualstudio.com/docs/devcontainers/containers). This has all prereqs installed for you.

1. Sign into Azure Developer CLI. 

    ```console
    azd auth login
    ```

1. Deploy to Aazure

    ```console
    azd up
    ```

## Learn how to scaffold a new .NET API server from a TypeSpec defintion

Use this readme to learn the process of how this repo was built.

## Prerequisites

- [Node.js](https://nodejs.org/) (v16 or later)
- [.NET SDK](https://dotnet.microsoft.com/download) (v9.0 or later)
- [Azure CLI](https://docs.microsoft.com/cli/azure/install-azure-cli)
- [Azure Developer CLI](https://learn.microsoft.com/azure/developer/azure-developer-cli/install-azd)

## Install TypeSpec and .NET

First, make sure you have Node.js and .NET installed. Then, install the TypeSpec compiler globally:

```bash
npm install -g @typespec/compiler
```

Install TypeSpec extension for VS Code for a better development experience:

```bash
code --install-extension ms-typespec.typespec-vscode
```

Verify TypeSpec is installed correctly:

```bash
tsp --version
```

## Create new TypeSpec project

1. Create a new directory for your project:

```bash
mkdir typespec-api
cd typespec-api
```

2. Initialize a new TypeSpec project:

```bash
npm init -y
npm install --save-dev @typespec/compiler @typespec/http @typespec/rest @typespec/openapi @typespec/openapi3 @typespec/http-server-csharp
```

3. Create a main.tsp file with your API specification:

```typespec
import "@typespec/http";

using Http;
@service(#{ title: "Widget Service" })
namespace DemoService;

model Widget {
  id: string;
  weight: int32;
  color: "red" | "blue";
}

model WidgetList {
  items: Widget[];
}

@error
model Error {
  code: int32;
  message: string;
}

@route("/widgets")
@tag("Widgets")
interface Widgets {
  /** List widgets */
  @get list(): WidgetList | Error;
  /** Read widgets */
  @get read(@path id: string): Widget | Error;
  /** Create a widget */
  @post create(@body body: Widget): Widget | Error;
  /** Update a widget */
  @patch update(@path id: string, @body body: MergePatchUpdate<Widget>): Widget | Error;
  /** Delete a widget */
  @delete delete(@path id: string): void | Error;
}
```

## Configure TypeSpec server scaffolding with tspconfig.yaml

Create a `tspconfig.yaml` file to configure the TypeSpec emitters:

```yaml
emit:
  - "@typespec/openapi3"
  - "@typespec/http-server-csharp"
options:
  "@typespec/openapi3":
    emitter-output-dir: "{output-dir}/server/wwwroot"
    openapi-versions:
      - 3.1.0
  "@typespec/http-server-csharp":
    emitter-output-dir: "{output-dir}/server/"
```

Generate the server code using TypeSpec:

```bash
tsp compile .
```

This will create a new server directory with a fully functional .NET API server, including controllers, models, and an OpenAPI specification available from a Swagger UI.

## Integrate Cosmos DB to TypeSpec server project

1. Add the necessary NuGet packages to your server project:

```bash
cd server
dotnet add package Microsoft.Azure.Cosmos
dotnet add package Azure.Identity
```

2. Create Azure directory and CosmosDbRegistration.cs file in your server project to handle Cosmos DB integration.

3. Implement the WidgetsCosmos class to handle CRUD operations with Cosmos DB.

4. Update Program.cs to use Cosmos DB instead of mock data.

## Use Azure Developer CLI and Bicep to deploy to Azure

1. Create an `azure.yaml` file in the project root:

```yaml
name: azure-typespec-scaffold-dotnet
metadata:
  template: azd-init@1.14.0
services:
  api:
    project: ./server
    host: containerapp
    language: dotnet
pipeline:
  provider: github
```

This uses the latest Azure Developer CLI (AZD) and .NET abilities to deploy a .NET app to Azure Container apps without a container defintion. 

2. Create Bicep templates in an `infra` directory to define the Azure resources with Azure Verified Modules:

The main.bicep file defines:
- Azure Cosmos DB with serverless configuration
- Azure Container Registry
- Azure Container Apps Environment
- Azure Container App to host the API
- Log Analytics Workspace
- Managed Identities for secure connections

3. Deploy to Azure using Azure Developer CLI:

```bash
azd auth login
azd up
```

This command:
- Provisions all required Azure resources using the Bicep templates
- Builds the .NET application
- Packages it into a container image
- Deploys the container to Azure Container Apps

## Use API with Swagger UI

Once deployed, you can access your API using Swagger UI:

1. Get the URL for your deployed Container App:
```bash
azd env get-values
```

2. Open the URL in your browser to access the Swagger UI interface.

3. Use the Swagger UI to test your API endpoints:
   - GET /widgets - List all widgets
   - GET /widgets/{id} - Get a specific widget
   - POST /widgets - Create a new widget
   - PATCH /widgets/{id} - Update a widget
   - DELETE /widgets/{id} - Delete a widget

## Project Structure

- **main.tsp**: TypeSpec API definition
- **tspconfig.yaml**: TypeSpec compiler configuration
- **server/**: Generated .NET API server
  - **azure/**: Custom implementations for Azure services
  - **generated/**: Auto-generated TypeSpec code
  - **Program.cs**: Application entry point
- **infra/**: Azure infrastructure as code (Bicep)
- **azure.yaml**: Azure Developer CLI configuration

## Next Steps

- Add authentication and authorization
- Implement monitoring and logging
- Add GitHub Actions for CI/CD
- Explore more TypeSpec features for complex APIs

