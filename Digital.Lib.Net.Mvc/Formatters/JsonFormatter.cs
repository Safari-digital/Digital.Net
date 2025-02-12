﻿using System.Text.Json;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;

namespace Digital.Lib.Net.Mvc.Formatters;

public static class JsonFormatter
{
    public static JsonPatchDocument<T> GetPatchDocument<T>(JsonElement patch) where T : class =>
        JsonConvert.DeserializeObject<JsonPatchDocument<T>>(patch.GetRawText())
        ?? new JsonPatchDocument<T>();
}