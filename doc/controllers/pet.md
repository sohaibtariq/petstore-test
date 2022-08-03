# Pet

Everything about your Pets

Find out more: [http://swagger.io](http://swagger.io)

```csharp
PetController petController = client.PetController;
```

## Class Name

`PetController`

## Methods

* [Upload File](../../doc/controllers/pet.md#upload-file)
* [Add Pet](../../doc/controllers/pet.md#add-pet)
* [Update Pet](../../doc/controllers/pet.md#update-pet)
* [Find Pets by Status](../../doc/controllers/pet.md#find-pets-by-status)
* [Find Pets by Tags](../../doc/controllers/pet.md#find-pets-by-tags)
* [Get Pet by Id](../../doc/controllers/pet.md#get-pet-by-id)
* [Update Pet With Form](../../doc/controllers/pet.md#update-pet-with-form)
* [Delete Pet](../../doc/controllers/pet.md#delete-pet)


# Upload File

uploads an image

```csharp
UploadFileAsync(
    long petId,
    string additionalMetadata = null,
    FileStreamInfo file = null)
```

## Parameters

| Parameter | Type | Tags | Description |
|  --- | --- | --- | --- |
| `petId` | `long` | Template, Required | ID of pet to update |
| `additionalMetadata` | `string` | Form, Optional | Additional data to pass to server |
| `file` | `FileStreamInfo` | Form, Optional | file to upload |

## Requires scope

`WRITEPETS`, `READPETS`

## Response Type

[`Task<Models.ApiResponse>`](../../doc/models/api-response.md)

## Example Usage

```csharp
long petId = 152L;

try
{
    ApiResponse result = await petController.UploadFileAsync(petId, null, null);
}
catch (ApiException e){};
```


# Add Pet

Add a new pet to the store

```csharp
AddPetAsync(
    Models.Pet body)
```

## Parameters

| Parameter | Type | Tags | Description |
|  --- | --- | --- | --- |
| `body` | [`Models.Pet`](../../doc/models/pet.md) | Body, Required | Pet object that needs to be added to the store |

## Requires scope

`WRITEPETS`, `READPETS`

## Response Type

`Task`

## Example Usage

```csharp
var body = new Pet();
body.Name = "name6";
body.PhotoUrls = new List<string>();
body.PhotoUrls.Add("photoUrls1");

try
{
    await petController.AddPetAsync(body);
}
catch (ApiException e){};
```

## Errors

| HTTP Status Code | Error Description | Exception Class |
|  --- | --- | --- |
| 405 | Invalid input | `ApiException` |


# Update Pet

Update an existing pet

```csharp
UpdatePetAsync(
    Models.Pet body)
```

## Parameters

| Parameter | Type | Tags | Description |
|  --- | --- | --- | --- |
| `body` | [`Models.Pet`](../../doc/models/pet.md) | Body, Required | Pet object that needs to be added to the store |

## Requires scope

`WRITEPETS`, `READPETS`

## Response Type

`Task`

## Example Usage

```csharp
var body = new Pet();
body.Name = "name6";
body.PhotoUrls = new List<string>();
body.PhotoUrls.Add("photoUrls1");

try
{
    await petController.UpdatePetAsync(body);
}
catch (ApiException e){};
```

## Errors

| HTTP Status Code | Error Description | Exception Class |
|  --- | --- | --- |
| 400 | Invalid ID supplied | `ApiException` |
| 404 | Pet not found | `ApiException` |
| 405 | Validation exception | `ApiException` |


# Find Pets by Status

Multiple status values can be provided with comma separated strings

```csharp
FindPetsByStatusAsync(
    List<Models.Status2Enum> status)
```

## Parameters

| Parameter | Type | Tags | Description |
|  --- | --- | --- | --- |
| `status` | [`List<Models.Status2Enum>`](../../doc/models/status-2-enum.md) | Query, Required | Status values that need to be considered for filter |

## Requires scope

`WRITEPETS`, `READPETS`

## Response Type

[`Task<List<Models.Pet>>`](../../doc/models/pet.md)

## Example Usage

```csharp
var status = new List<Status2Enum>();
status.Add(Status2Enum.Pending);
status.Add(Status2Enum.Sold);
status.Add(Status2Enum.Available);

try
{
    List<Pet> result = await petController.FindPetsByStatusAsync(status);
}
catch (ApiException e){};
```

## Errors

| HTTP Status Code | Error Description | Exception Class |
|  --- | --- | --- |
| 400 | Invalid status value | `ApiException` |


# Find Pets by Tags

**This endpoint is deprecated.**

Multiple tags can be provided with comma separated strings. Use tag1, tag2, tag3 for testing.

```csharp
FindPetsByTagsAsync(
    List<string> tags)
```

## Parameters

| Parameter | Type | Tags | Description |
|  --- | --- | --- | --- |
| `tags` | `List<string>` | Query, Required | Tags to filter by |

## Requires scope

`WRITEPETS`, `READPETS`

## Response Type

[`Task<List<Models.Pet>>`](../../doc/models/pet.md)

## Example Usage

```csharp
var tags = new List<string>();
tags.Add("tags5");
tags.Add("tags6");

try
{
    List<Pet> result = await petController.FindPetsByTagsAsync(tags);
}
catch (ApiException e){};
```

## Errors

| HTTP Status Code | Error Description | Exception Class |
|  --- | --- | --- |
| 400 | Invalid tag value | `ApiException` |


# Get Pet by Id

Returns a single pet

```csharp
GetPetByIdAsync(
    long petId)
```

## Parameters

| Parameter | Type | Tags | Description |
|  --- | --- | --- | --- |
| `petId` | `long` | Template, Required | ID of pet to return |

## Response Type

[`Task<Models.Pet>`](../../doc/models/pet.md)

## Example Usage

```csharp
long petId = 152L;

try
{
    Pet result = await petController.GetPetByIdAsync(petId);
}
catch (ApiException e){};
```

## Errors

| HTTP Status Code | Error Description | Exception Class |
|  --- | --- | --- |
| 400 | Invalid ID supplied | `ApiException` |
| 404 | Pet not found | `ApiException` |


# Update Pet With Form

Updates a pet in the store with form data

```csharp
UpdatePetWithFormAsync(
    long petId,
    string name = null,
    string status = null)
```

## Parameters

| Parameter | Type | Tags | Description |
|  --- | --- | --- | --- |
| `petId` | `long` | Template, Required | ID of pet that needs to be updated |
| `name` | `string` | Form, Optional | Updated name of the pet |
| `status` | `string` | Form, Optional | Updated status of the pet |

## Requires scope

`WRITEPETS`, `READPETS`

## Response Type

`Task`

## Example Usage

```csharp
long petId = 152L;

try
{
    await petController.UpdatePetWithFormAsync(petId, null, null);
}
catch (ApiException e){};
```

## Errors

| HTTP Status Code | Error Description | Exception Class |
|  --- | --- | --- |
| 405 | Invalid input | `ApiException` |


# Delete Pet

Deletes a pet

```csharp
DeletePetAsync(
    long petId,
    string apiKey = null)
```

## Parameters

| Parameter | Type | Tags | Description |
|  --- | --- | --- | --- |
| `petId` | `long` | Template, Required | Pet id to delete |
| `apiKey` | `string` | Header, Optional | - |

## Requires scope

`WRITEPETS`, `READPETS`

## Response Type

`Task`

## Example Usage

```csharp
long petId = 152L;

try
{
    await petController.DeletePetAsync(petId, null);
}
catch (ApiException e){};
```

## Errors

| HTTP Status Code | Error Description | Exception Class |
|  --- | --- | --- |
| 400 | Invalid ID supplied | `ApiException` |
| 404 | Pet not found | `ApiException` |

