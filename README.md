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