# Store

Access to Petstore orders

```csharp
StoreController storeController = client.StoreController;
```

## Class Name

`StoreController`

## Methods

* [Place Order](../../doc/controllers/store.md#place-order)
* [Get Order by Id](../../doc/controllers/store.md#get-order-by-id)
* [Delete Order](../../doc/controllers/store.md#delete-order)
* [Get Inventory](../../doc/controllers/store.md#get-inventory)


# Place Order

Place an order for a pet

```csharp
PlaceOrderAsync(
    Models.Order body)
```

## Parameters

| Parameter | Type | Tags | Description |
|  --- | --- | --- | --- |
| `body` | [`Models.Order`](../../doc/models/order.md) | Body, Required | order placed for purchasing the pet |

## Response Type

[`Task<Models.Order>`](../../doc/models/order.md)

## Example Usage

```csharp
var body = new Order();

try
{
    Order result = await storeController.PlaceOrderAsync(body);
}
catch (ApiException e){};
```

## Errors

| HTTP Status Code | Error Description | Exception Class |
|  --- | --- | --- |
| 400 | Invalid Order | `ApiException` |


# Get Order by Id

For valid response try integer IDs with value >= 1 and <= 10. Other values will generated exceptions

```csharp
GetOrderByIdAsync(
    long orderId)
```

## Parameters

| Parameter | Type | Tags | Description |
|  --- | --- | --- | --- |
| `orderId` | `long` | Template, Required | ID of pet that needs to be fetched<br>**Constraints**: `>= 1`, `<= 10` |

## Response Type

[`Task<Models.Order>`](../../doc/models/order.md)

## Example Usage

```csharp
long orderId = 62L;

try
{
    Order result = await storeController.GetOrderByIdAsync(orderId);
}
catch (ApiException e){};
```

## Errors

| HTTP Status Code | Error Description | Exception Class |
|  --- | --- | --- |
| 400 | Invalid ID supplied | `ApiException` |
| 404 | Order not found | `ApiException` |


# Delete Order

For valid response try integer IDs with positive integer value. Negative or non-integer values will generate API errors

```csharp
DeleteOrderAsync(
    long orderId)
```

## Parameters

| Parameter | Type | Tags | Description |
|  --- | --- | --- | --- |
| `orderId` | `long` | Template, Required | ID of the order that needs to be deleted<br>**Constraints**: `>= 1` |

## Response Type

`Task`

## Example Usage

```csharp
long orderId = 62L;

try
{
    await storeController.DeleteOrderAsync(orderId);
}
catch (ApiException e){};
```

## Errors

| HTTP Status Code | Error Description | Exception Class |
|  --- | --- | --- |
| 400 | Invalid ID supplied | `ApiException` |
| 404 | Order not found | `ApiException` |


# Get Inventory

Returns a map of status codes to quantities

```csharp
GetInventoryAsync()
```

## Response Type

`Task<Dictionary<string, int>>`

## Example Usage

```csharp
try
{
    Dictionary<string, int> result = await storeController.GetInventoryAsync();
}
catch (ApiException e){};
```

