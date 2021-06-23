# TestServerExtensions

A suite of extensions for Microsoft.AspNetCore.TestHost.

## List of extensions

- Create Request using a lambda expression.

  
## Installation 

Install TestServerExtensions nuget package in your test project.

```bash 
PM> Install-Package TestServerExtensions
```
   
## Usage

#### Make a request using lambda expression
By default, testServer have a CreateRequest method that returns a RequestBuilder instance useful for making calls to your endpoint, this method uses the uri of the endpoint so you need to know it.

TestServerExtensions provide a new overload of this method that uses a lambda expression of test action instead the url of the endpoint.

```csharp 
var response = await _server.CreateRequest<TestController>(controller => controller.TestAction(request)).GetAsync();
```

  
## Roadmap

- Add ability to add user identity 
- Add ability to easily replace some injected dependencies

  
## Contributing

Contributions are always welcome! 
I'll give priority to pull requests that solves any bug related with implemented functionality, anyway if you have any suggestion or miss a needed functionality don't hesitate to open a new issue.