
# Getting Started with Swagger Petstore

## Introduction

This is a sample server Petstore server.  You can find out more about Swagger at [http://swagger.io](http://swagger.io) or on [irc.freenode.net, #swagger](http://swagger.io/irc/).  For this sample, you can use the api key `special-key` to test the authorization filters.

Find out more about Swagger: [http://swagger.io](http://swagger.io)

## Building

The generated code uses the Newtonsoft Json.NET NuGet Package. If the automatic NuGet package restore is enabled, these dependencies will be installed automatically. Therefore, you will need internet access for build.

* Open the solution (SwaggerPetstore.sln) file.

Invoke the build process using Ctrl + Shift + B shortcut key or using the Build menu as shown below.

The build process generates a portable class library, which can be used like a normal class library. The generated library is compatible with Windows Forms, Windows RT, Windows Phone 8, Silverlight 5, Xamarin iOS, Xamarin Android and Mono. More information on how to use can be found at the MSDN Portable Class Libraries documentation.

## Installation

The following section explains how to use the SwaggerPetstore.Standard library in a new project.

### 1. Starting a new project

For starting a new project, right click on the current solution from the solution explorer and choose `Add -> New Project`.

![Add a new project in Visual Studio](https://apidocs.io/illustration/cs?workspaceFolder=Swagger%20Petstore-CSharp&workspaceName=SwaggerPetstore&projectName=SwaggerPetstore.Standard&rootNamespace=SwaggerPetstore.Standard&step=addProject)

Next, choose `Console Application`, provide `TestConsoleProject` as the project name and click OK.

![Create a new Console Application in Visual Studio](https://apidocs.io/illustration/cs?workspaceFolder=Swagger%20Petstore-CSharp&workspaceName=SwaggerPetstore&projectName=SwaggerPetstore.Standard&rootNamespace=SwaggerPetstore.Standard&step=createProject)

### 2. Set as startup project

The new console project is the entry point for the eventual execution. This requires us to set the `TestConsoleProject` as the start-up project. To do this, right-click on the `TestConsoleProject` and choose `Set as StartUp Project` form the context menu.

![Adding a project reference](https://apidocs.io/illustration/cs?workspaceFolder=Swagger%20Petstore-CSharp&workspaceName=SwaggerPetstore&projectName=SwaggerPetstore.Standard&rootNamespace=SwaggerPetstore.Standard&step=setStartup)

### 3. Add reference of the library project

In order to use the Tester library in the new project, first we must add a project reference to the `TestConsoleProject`. First, right click on the `References` node in the solution explorer and click `Add Reference...`

![Adding a project reference](https://apidocs.io/illustration/cs?workspaceFolder=Swagger%20Petstore-CSharp&workspaceName=SwaggerPetstore&projectName=SwaggerPetstore.Standard&rootNamespace=SwaggerPetstore.Standard&step=addReference)

Next, a window will be displayed where we must set the `checkbox` on `Tester.Tests` and click `OK`. By doing this, we have added a reference of the `Tester.Tests` project into the new `TestConsoleProject`.

![Creating a project reference](https://apidocs.io/illustration/cs?workspaceFolder=Swagger%20Petstore-CSharp&workspaceName=SwaggerPetstore&projectName=SwaggerPetstore.Standard&rootNamespace=SwaggerPetstore.Standard&step=createReference)

### 4. Write sample code

Once the `TestConsoleProject` is created, a file named `Program.cs` will be visible in the solution explorer with an empty `Main` method. This is the entry point for the execution of the entire solution. Here, you can add code to initialize the client library and acquire the instance of a Controller class. Sample code to initialize the client library and using Controller methods is given in the subsequent sections.

![Adding a project reference](https://apidocs.io/illustration/cs?workspaceFolder=Swagger%20Petstore-CSharp&workspaceName=SwaggerPetstore&projectName=SwaggerPetstore.Standard&rootNamespace=SwaggerPetstore.Standard&step=addCode)

## Initialize the API Client

**_Note:_** Documentation for the client can be found [here.](doc/client.md)

The following parameters are configurable for the API Client:

| Parameter | Type | Description |
|  --- | --- | --- |
| `Environment` | Environment | The API environment. <br> **Default: `Environment.Production`** |
| `Timeout` | `TimeSpan` | Http client timeout.<br>*Default*: `TimeSpan.FromSeconds(100)` |
| `OAuthClientId` | `string` | OAuth 2 Client ID |
| `OAuthRedirectUri` | `string` | OAuth 2 Redirection endpoint or Callback Uri |
| `OAuthToken` | `Models.OAuthToken` | Object for storing information about the OAuth token |
| `OAuthScopes` | `List<Models.OAuthScopeEnum>` |  |

The API client can be initialized as follows:

```csharp
SwaggerPetstore.Standard.SwaggerPetstoreClient client = new SwaggerPetstore.Standard.SwaggerPetstoreClient.Builder()
    .OAuthScopes(new List<OAuthScopeEnum>(){OAuthScopeEnum.Readpets, OAuthScopeEnum.Writepets})
    .ImplicitAuth("OAuthClientId", "OAuthRedirectUri")
    .Environment(SwaggerPetstore.Standard.Environment.Production)
    .HttpClientConfig(config => config.NumberOfRetries(0))
    .Build();
```

## Authorization

This API uses `OAuth 2 Implicit Grant`.

## Implicit Grant

Your application must obtain user authorization before it can execute an endpoint call incase this SDK chooses to use *OAuth 2.0 Implicit Grant* to obtain a user's consent to perform an API request on user's behalf. This authorization includes the following steps

This process requires the presence of a client-side JavaScript code on the redirect URI page to receive the *access token* after the consent step is completed.

### 1\. Obtain user consent

To obtain user's consent, you must redirect the user to the authorization page. The `BuildAuthorizationUrl()` method creates the URL to the authorization page. You must pass the scopes for which you need permission to access.

```csharp
string authUrl = client.AuthorizationCodeAuth.BuildAuthorizationUrl();
```

### 2\. Handle the OAuth server response

Once the user responds to the consent request, the OAuth 2.0 server responds to your application's access request by redirecting the user to the redirect URI specified set in `Configuration`.

The redirect URI will receive the *access token* as the `token` argument in the URL fragment.

```
https://example.com/oauth/callback#token=XXXXXXXXXXXXXXXXXXXXXXXXX
```

The access token must be extracted by the client-side JavaScript code. The access token can be used to authorize any further endpoint calls by the JavaScript code.

### Scopes

Scopes enable your application to only request access to the resources it needs while enabling users to control the amount of access they grant to your application. Available scopes are defined in the `OAuthScopeEnum` enumeration.

| Scope Name | Description |
|  --- | --- |
| `READPETS` | read your pets |
| `WRITEPETS` | modify pets in your account |

## List of APIs

* [Pet](doc/controllers/pet.md)
* [Store](doc/controllers/store.md)
* [User](doc/controllers/user.md)

## Classes Documentation

* [Utility Classes](doc/utility-classes.md)
* [HttpRequest](doc/http-request.md)
* [HttpResponse](doc/http-response.md)
* [HttpStringResponse](doc/http-string-response.md)
* [HttpContext](doc/http-context.md)
* [HttpClientConfiguration](doc/http-client-configuration.md)
* [HttpClientConfiguration Builder](doc/http-client-configuration-builder.md)
* [IAuthManager](doc/i-auth-manager.md)
* [ApiException](doc/api-exception.md)

