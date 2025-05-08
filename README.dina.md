npm install -g @typespec/compiler
tsp init

```console
vscode ➜ /workspaces/typespec-on-azure (main) $ tsp init

warning: Folder /workspaces/typespec-on-azure is not empty.

✔ Initialize a new project here?: Yes
✔ Select a project template: Generic REST API
✔ Enter a project name: typespec-on-azure
✔ What emitters do you want to use?: @typespec/openapi3, @typespec/http-server-csharp, @typespec/http-server-js

⚠ Dependencies installed
No package manager spec found, defaulted to npm latest version. Please set devEngines.packageManager or packageManager in your package.json.

success: Project initialized!
Run tsp compile . to build the project.

Please review the following messages from emitters:
  @typespec/http-server-csharp: 

        Generated ASP.Net services require dotnet 9:
        https://dotnet.microsoft.com/download 

        Create an ASP.Net service project for your TypeSpec:
        > npx hscs-scaffold . --use-swaggerui --overwrite

        More information on getting started:
        https://aka.ms/tsp/hscs/start
         
  @typespec/http-server-js: 

        Generated JavaScript services require a service runtime:
        https://nodejs.org/download 

        Create a JavaScript service project for your TypeSpec:
        > npx hsjs-scaffold

        More information on getting started:
        https://aka.ms/tsp/hsjs/start
```

npx tsp compile . \
--emit @typespec/http-server-csharp \
--option @typespec/http-server-csharp.emitter-output-dir={project-root}/server \
--option @typespec/http-server-csharp.emit-mocks=mocks-and-project-files \
--option @typespec/http-server-csharp.project-name=ServiceProject \
--option @typespec/http-server-csharp.overwrite=true \
--option @typespec/http-server-csharp.http-port=5115 \
--option @typespec/http-server-csharp.https-port=7896 \
--option @typespec/http-server-csharp.collection-type=array \
--option @typespec/http-server-csharp.use-swaggerui=true \
--trace http-server-csharp 

npx tsp compile . \
--emit @typespec/openapi3 \
--option @typespec/openapi3.emitter-output-dir={project-root}/server/wwwroot \
--trace openapi3

npx tsp compile . \
--emit @typespec/openapi3 \
--option @typespec/openapi3.emitter-output-dir={project-root}/spec \
--trace openapi3

npx tsp compile . \
--emit @typespec/http-server-js \
--option @typespec/http-server-js.emitter-output-dir={project-root}/server-js \
--option @typespec/http-server-js.omit-unreachable-types=true \
--option @typespec/http-server-js.express=true \
--trace http-server-js

npx hsjs-scaffold

```
npx hsjs-scaffold
[hsjs] Scaffolding TypeScript project...
[hsjs] Using project file 'tspconfig.yaml' and main file 'main.tsp'
[hsjs] Emitter options have 'express: false'. Generating server model: 'Node'.
[hsjs] Generating standalone project in output directory.
[hsjs] Compiling TypeSpec project...
main.tsp:34:10 - warning @typespec/http/patch-implicit-optional: Patch operation stopped applying an implicit optional transform to the body in 1.0.0. Use @patch(#{implicitOptionality: true}) to restore the old behavior.
> 34 |   @patch update(...Update<Widget>): Read<Widget> | Error;
     |          ^^^^^^
main.tsp:34:10 - warning @typespec/http/patch-implicit-optional: Patch operation stopped applying an implicit optional transform to the body in 1.0.0. Use @patch(#{implicitOptionality: true}) to restore the old behavior.
> 34 |   @patch update(...Update<Widget>): Read<Widget> | Error;
     |          ^^^^^^
[hsjs] TypeSpec compiled successfully. Scaffolding implementation...
[hsjs] Generating controller 'WidgetsImpl'...
[hsjs] Generating server entry point...
[hsjs] Writing files...
[hsjs] Writing file 'tsp-output/server/js/src/index.ts'...
[hsjs] Writing file 'tsp-output/server/js/src/controllers/widgets.ts'...
[hsjs] Writing file 'tsp-output/server/js/src/generated/helpers/http.ts'...
[hsjs] Writing file 'tsp-output/server/js/tsconfig.json'...
[hsjs] Writing file 'tsp-output/server/js/.vscode/launch.json'...
[hsjs] Writing file 'tsp-output/server/js/.vscode/tasks.json'...
[hsjs] Failed to find version for dependency: node:http
[hsjs] FATAL: Failed to find dependency versions. This is a bug. Please report this error to https://github.com/microsoft/typespec
```

---------------------------------------------------------

npx tsp compile . \
--emit @typespec/http-server-csharp \
--option @typespec/http-server-csharp.emitter-output-dir={project-root}/server \
--option @typespec/http-server-csharp.emit-mocks=mocks-and-project-files \
--option @typespec/http-server-csharp.project-name=ServiceProject \
--option @typespec/http-server-csharp.overwrite=true \
--option @typespec/http-server-csharp.http-port=5115 \
--option @typespec/http-server-csharp.https-port=7896 \
--option @typespec/http-server-csharp.collection-type=array \
--option @typespec/http-server-csharp.use-swaggerui=true \
--trace http-server-csharp 

npx tsp compile . \
--emit @typespec/openapi3 \
--option @typespec/openapi3.emitter-output-dir={project-root}/server/wwwroot \
--trace openapi3