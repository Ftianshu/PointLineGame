using Godot;
using Godot.Collections;

namespace Survival
{
    public partial class Face : Area2D
    {
        public Vector2[] points;

        public CollisionPolygon2D polygonPhysics;

        public Polygon2D polygonShape;

        private double Lifetime = 0.5;

        private double time = 0;

        public override void _Ready()
        {
            polygonPhysics = GetNode<CollisionPolygon2D>("CollisionPolygon2D");
            polygonShape = GetNode<Polygon2D>("Polygon2D");
            polygonPhysics.SetDeferred("polygon", points);
            polygonShape.Polygon = points;
            GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D").Play();
            GameEntry.Entity.CreateHud(GetSquare(), GetCenter());
        }

        public override void _Process(double delta)
        {
            time += delta;
            if (time > Lifetime)
            {
                // 播放破碎效果,目前暂时用消失效果
                //GameEntry.Entity.CreateEffect("FaceBreakEffect");
                QueueFree();
            }
            (polygonShape.Material as ShaderMaterial).SetShaderParameter("alpha", 1 - time / Lifetime);
        }

        private Vector2 GetCenter()
        {
            float minX = float.MaxValue;
            float minY = float.MaxValue;
            float maxX = float.MinValue;
            float maxY = float.MinValue;
            foreach (Vector2 vector in points)
            {
                if (vector.X < minX)
                {
                    minX = vector.X;
                }
                else if (vector.X > maxX)
                {
                    maxX = vector.X;
                }

                if (vector.Y < minY)
                {
                    minY = vector.Y;
                }
                else if (vector.Y > maxY)
                {
                    maxY = vector.Y;
                }
            }
            return new Vector2((minX + maxX) / 2, (minY + maxY) / 2);
        }

        private int GetSquare()
        {
            float square = 0;
            for (int i = 0; i < points.Length - 1; i++)
            {
                square += (points[i].Y + points[i + 1].Y) / 2 * (points[i].X - points[i + 1].X);
            }
            square += (points[0].Y + points[points.Length - 1].Y) / 2 * (points[0].X - points[points.Length - 1].X);
            return (int)Mathf.Abs(square / 100);
        }

        public float GetDamage()
        {
            switch (GameEntry.Player.faceDamageType)
            {
                case FaceDamageType.BaseMode:
                    {
                        return GameEntry.Player.faceDamage;
                    }
                case FaceDamageType.IncreaseWithArea:
                    {
                        int square = GetSquare();

                        return square / 50 * GameEntry.Player.faceDamage;
                    }
                case FaceDamageType.DecreaseWithArea:
                    {
                        return GameEntry.Player.faceDamage;
                    }
                case FaceDamageType.IncreaseWithGirth:
                    {
                        return GameEntry.Player.faceDamage;
                    }
                case FaceDamageType.DecreaseWithGirth:
                    {
                        return GameEntry.Player.faceDamage;
                    }
                case FaceDamageType.Other:
                    {
                        break;
                    }
            }
            return 0;
        }
    }

}
