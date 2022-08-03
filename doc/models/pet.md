
# Pet

## Structure

`Pet`

## Fields

| Name | Type | Tags | Description |
|  --- | --- | --- | --- |
| `Id` | `long?` | Optional | - |
| `Category` | [`Models.Category`](../../doc/models/category.md) | Optional | - |
| `Name` | `string` | Required | - |
| `PhotoUrls` | `List<string>` | Required | - |
| `Tags` | [`List<Models.Tag>`](../../doc/models/tag.md) | Optional | - |
| `Status` | [`Models.StatusEnum?`](../../doc/models/status-enum.md) | Optional | pet status in the store |

## Example (as JSON)

```json
{
  "id": null,
  "category": null,
  "name": "name0",
  "photoUrls": [
    "photoUrls5",
    "photoUrls6",
    "photoUrls7"
  ],
  "tags": null,
  "status": null
}
```

