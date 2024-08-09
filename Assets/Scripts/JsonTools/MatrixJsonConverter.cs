using System;
using Newtonsoft.Json;
using UnityEngine;

namespace JsonTools
{
    internal class MatrixJsonConverter : JsonConverter
    {
        private readonly int _matrixSize = 4;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Matrix4x4);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Matrix4x4 matrix = new Matrix4x4();

            reader.Read();

            for (int i = 0; i < _matrixSize; i++)
            {
                for (int j = 0; j < _matrixSize; j++)
                {
                    reader.Read();
                    matrix[i, j] = Convert.ToSingle(reader.Value);
                }
            }

            reader.Read();

            return matrix;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Matrix4x4 matrix = (Matrix4x4)value;

            writer.WriteStartObject();

            for (int i = 0; i < _matrixSize; i++)
            {
                for (int j = 0; j < _matrixSize; j++)
                {
                    writer.WritePropertyName($"m{i}{j}");
                    writer.WriteValue(matrix[i, j]);
                }
            }

            writer.WriteEndObject();
        }
    }
}