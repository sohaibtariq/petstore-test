# User

Operations about user

Find out more about our store: [http://swagger.io](http://swagger.io)

```csharp
UserController userController = client.UserController;
```

## Class Name

`UserController`

## Methods

* [Create Users With Array Input](../../doc/controllers/user.md#create-users-with-array-input)
* [Create Users With List Input](../../doc/controllers/user.md#create-users-with-list-input)
* [Get User by Name](../../doc/controllers/user.md#get-user-by-name)
* [Update User](../../doc/controllers/user.md#update-user)
* [Delete User](../../doc/controllers/user.md#delete-user)
* [Login User](../../doc/controllers/user.md#login-user)
* [Logout User](../../doc/controllers/user.md#logout-user)
* [Create User](../../doc/controllers/user.md#create-user)


# Create Users With Array Input

Creates list of users with given input array

```csharp
CreateUsersWithArrayInputAsync(
    List<Models.User> body)
```

## Parameters

| Parameter | Type | Tags | Description |
|  --- | --- | --- | --- |
| `body` | [`List<Models.User>`](../../doc/models/user.md) | Body, Required | List of user object |

## Response Type

`Task`

## Example Usage

```csharp
var body = new List<User>();

var body0 = new User();
body.Add(body0);

var body1 = new User();
body.Add(body1);


try
{
    await userController.CreateUsersWithArrayInputAsync(body);
}
catch (ApiException e){};
```

## Errors

| HTTP Status Code | Error Description | Exception Class |
|  --- | --- | --- |
| Default | successful operation | `ApiException` |


# Create Users With List Input

Creates list of users with given input array

```csharp
CreateUsersWithListInputAsync(
    List<Models.User> body)
```

## Parameters

| Parameter | Type | Tags | Description |
|  --- | --- | --- | --- |
| `body` | [`List<Models.User>`](../../doc/models/user.md) | Body, Required | List of user object |

## Response Type

`Task`

## Example Usage

```csharp
var body = new List<User>();

var body0 = new User();
body.Add(body0);

var body1 = new User();
body.Add(body1);


try
{
    await userController.CreateUsersWithListInputAsync(body);
}
catch (ApiException e){};
```

## Errors

| HTTP Status Code | Error Description | Exception Class |
|  --- | --- | --- |
| Default | successful operation | `ApiException` |


# Get User by Name

Get user by user name

```csharp
GetUserByNameAsync(
    string username)
```

## Parameters

| Parameter | Type | Tags | Description |
|  --- | --- | --- | --- |
| `username` | `string` | Template, Required | The name that needs to be fetched. Use user1 for testing. |

## Response Type

[`Task<Models.User>`](../../doc/models/user.md)

## Example Usage

```csharp
string username = "username0";

try
{
    User result = await userController.GetUserByNameAsync(username);
}
catch (ApiException e){};
```

## Errors

| HTTP Status Code | Error Description | Exception Class |
|  --- | --- | --- |
| 400 | Invalid username supplied | `ApiException` |
| 404 | User not found | `ApiException` |


# Update User

This can only be done by the logged in user.

```csharp
UpdateUserAsync(
    string username,
    Models.User body)
```

## Parameters

| Parameter | Type | Tags | Description |
|  --- | --- | --- | --- |
| `username` | `string` | Template, Required | name that need to be updated |
| `body` | [`Models.User`](../../doc/models/user.md) | Body, Required | Updated user object |

## Response Type

`Task`

## Example Usage

```csharp
string username = "username0";
var body = new User();

try
{
    await userController.UpdateUserAsync(username, body);
}
catch (ApiException e){};
```

## Errors

| HTTP Status Code | Error Description | Exception Class |
|  --- | --- | --- |
| 400 | Invalid user supplied | `ApiException` |
| 404 | User not found | `ApiException` |


# Delete User

This can only be done by the logged in user.

```csharp
DeleteUserAsync(
    string username)
```

## Parameters

| Parameter | Type | Tags | Description |
|  --- | --- | --- | --- |
| `username` | `string` | Template, Required | The name that needs to be deleted |

## Response Type

`Task`

## Example Usage

```csharp
string username = "username0";

try
{
    await userController.DeleteUserAsync(username);
}
catch (ApiException e){};
```

## Errors

| HTTP Status Code | Error Description | Exception Class |
|  --- | --- | --- |
| 400 | Invalid username supplied | `ApiException` |
| 404 | User not found | `ApiException` |


# Login User

Logs user into the system

```csharp
LoginUserAsync(
    string username,
    string password)
```

## Parameters

| Parameter | Type | Tags | Description |
|  --- | --- | --- | --- |
| `username` | `string` | Query, Required | The user name for login |
| `password` | `string` | Query, Required | The password for login in clear text |

## Response Type

`Task<string>`

## Example Usage

```csharp
string username = "username0";
string password = "password4";

try
{
    string result = await userController.LoginUserAsync(username, password);
}
catch (ApiException e){};
```

## Errors

| HTTP Status Code | Error Description | Exception Class |
|  --- | --- | --- |
| 400 | Invalid username/password supplied | `ApiException` |


# Logout User

Logs out current logged in user session

```csharp
LogoutUserAsync()
```

## Response Type

`Task`

## Example Usage

```csharp
try
{
    await userController.LogoutUserAsync();
}
catch (ApiException e){};
```

## Errors

| HTTP Status Code | Error Description | Exception Class |
|  --- | --- | --- |
| Default | successful operation | `ApiException` |


# Create User

This can only be done by the logged in user.

```csharp
CreateUserAsync(
    Models.User body)
```

## Parameters

| Parameter | Type | Tags | Description |
|  --- | --- | --- | --- |
| `body` | [`Models.User`](../../doc/models/user.md) | Body, Required | Created user object |

## Response Type

`Task`

## Example Usage

```csharp
var body = new User();

try
{
    await userController.CreateUserAsync(body);
}
catch (ApiException e){};
```

## Errors

| HTTP Status Code | Error Description | Exception Class |
|  --- | --- | --- |
| Default | successful operation | `ApiException` |

