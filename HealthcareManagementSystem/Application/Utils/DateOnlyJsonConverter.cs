﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Utils
{
	public class DateOnlyJsonConverter : JsonConverter<DateOnly>
	{
		public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var stringValue = reader.GetString();
			return DateOnly.ParseExact(stringValue, "yyyy-MM-dd");
		}

		public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
		}
	}
}
