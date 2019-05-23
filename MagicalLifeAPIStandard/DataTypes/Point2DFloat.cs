﻿using ProtoBuf;
using System;
using System.Globalization;

namespace MagicalLifeAPI.DataTypes
{
    [ProtoBuf.ProtoContract]
    public struct Point2DFloat : IEquatable<Point2DFloat>
    {
        [ProtoMember(1)]
        public float X { get; set; }

        [ProtoMember(2)]
        public float Y { get; set; }

        public Point2DFloat(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            return "{ " + this.X.ToString(CultureInfo.InvariantCulture) + ", " + this.Y.ToString(CultureInfo.InvariantCulture) + " }";
        }

        public Point2D ToPoint2D()
        {
            return new Point2D((int)this.X, (int)this.Y);
        }

        public bool Equals(Point2DFloat other)
        {
            return Math.Abs(other.X - X) < 0.00001f && Math.Abs(other.Y - Y) < 0.00001f;
        }

        public override bool Equals(object obj)
        {
            if (obj is Point2DFloat point2DFloat)
            {
                return this.Equals(point2DFloat);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (int)X ^ (int)Y;
        }

        public static bool operator ==(Point2DFloat left, Point2DFloat right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Point2DFloat left, Point2DFloat right)
        {
            return !left.Equals(right);
        }
    }
}