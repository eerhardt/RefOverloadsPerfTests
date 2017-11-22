using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace MyBenchmarks
{
    public class ByRefBenchmarks
    {
        [Benchmark]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Matrix4x4ByValue()
        {
            Matrix4x4 m1 = Matrix4x4.Identity;
            Matrix4x4 m2 = Matrix4x4.CreateScale(5, 6, 7);
            Matrix4x4 m3 = Matrix4x4.CreateTranslation(-10, -12, -14);
            Matrix4x4 m4 = Matrix4x4.CreateFromYawPitchRoll(1.5f, 2.5f, 3.5f);
            Matrix4x4 result = Matrix4x4.Multiply(m1, m2);
            result = Matrix4x4.Multiply(result, m3);
            result = Matrix4x4.Multiply(result, m4);
        }

        [Benchmark]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Matrix4x4ByRef()
        {
            Matrix4x4 m1 = Matrix4x4.Identity;
            Matrix4x4 m2; Matrix4x4.CreateScale(5, 6, 7, out m2);
            Matrix4x4 m3; Matrix4x4.CreateTranslation(-10, -12, -14, out m3);
            Matrix4x4 m4; Matrix4x4.CreateFromYawPitchRoll(1.5f, 2.5f, 3.5f, out m4);
            Matrix4x4 result;
            Matrix4x4.Multiply(in m1, in m2, out result);
            Matrix4x4.Multiply(in result, in m3, out result);
            Matrix4x4.Multiply(in result, in m4, out result);
        }

        [Benchmark]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void QuaterionByValue()
        {
            float yaw = 1.5f;
            float pitch = 2.5f;
            float roll = 3.5f;

            Quaternion rotation1 = Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll);
            Quaternion rotation2 = Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll);
            Quaternion rotation3 = Quaternion.CreateFromAxisAngle(Vector3.UnitX, 3f) * Quaternion.CreateFromAxisAngle(new Vector3(5, 6, 7), 10f);
            Vector4 point = new Vector4(2, 4, -6, -8);
            point = Vector4.Transform(point, rotation1);
            point = Vector4.Transform(point, rotation2);
            point = Vector4.Transform(point, rotation3);
        }

        [Benchmark]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void QuaterionByRef()
        {
            float yaw = 1.5f;
            float pitch = 2.5f;
            float roll = 3.5f;

            Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll, out Quaternion rotation1);
            Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll, out Quaternion rotation2);
            Quaternion.CreateFromAxisAngle(Vector3.UnitX, 3f, out Quaternion temp1);
            Quaternion.CreateFromAxisAngle(new Vector3(5, 6, 7), 10f, out Quaternion temp2);
            Quaternion.Multiply(in temp1, in temp2, out Quaternion rotation3);

            Vector4 point = new Vector4(2, 4, -6, -8);
            Vector4.Transform(in point, in rotation1, out point);
            Vector4.Transform(in point, in rotation2, out point);
            Vector4.Transform(in point, in rotation3, out point);
        }

        [Benchmark]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Vector3ByValue()
        {
            Vector3 value1 = new Vector3(1, 2, 3);
            Vector3 value2 = new Vector3(-10, -5, 5);
            Vector3 value3 = new Vector3(.3f, .6f, -1.9f);

            Vector3 cross = Vector3.Cross(value1, value2);
            cross = Vector3.Cross(cross, value3);
            cross = Vector3.Multiply(cross, value1);
            cross = Vector3.Lerp(cross, Vector3.Zero, 0.3f);
        }

        [Benchmark]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Vector3ByRef()
        {
            Vector3 value1 = new Vector3(1, 2, 3);
            Vector3 value2 = new Vector3(-10, -5, 5);
            Vector3 value3 = new Vector3(.3f, .6f, -1.9f);

            Vector3 cross;
            Vector3.Cross(in value1, in value2, out cross);
            Vector3.Cross(in cross, in value3, out cross);
            Vector3.Multiply(in cross, in value1, out cross);
            var zero = Vector3.Zero;
            Vector3.Lerp(in cross, in zero, 0.3f, out cross);
        }

        [Benchmark]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Vector3SimpleAddByValue()
        {
            Vector3 value1 = new Vector3(1, 2, 3);
            Vector3 value2 = new Vector3(-10, -5, 5);
            Vector3 sum = Vector3.Add(value1, value2);
        }

        [Benchmark]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Vector3SimpleAddByRef()
        {
            Vector3 value1 = new Vector3(1, 2, 3);
            Vector3 value2 = new Vector3(-10, -5, 5);
            Vector3 sum; Vector3.Add(in value1, in value2, out sum);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ByRefBenchmarks>();
        }
    }
}