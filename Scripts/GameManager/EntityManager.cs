using Godot;
using System;

namespace Survival
{

    public class EntityManager
    {
        private Node EntityRoot;
        public Node EnemyRoot;
        private Node PointsRoot;
        private Node FaceRoot;

        public Line line;
        private int currentMap = 0;

        public EntityManager(Node entityRoot, Node enemyRoot)
        {
            EntityRoot = entityRoot;
            EnemyRoot = enemyRoot;
            PointsRoot = entityRoot.FindChild("PointsRoot");
            FaceRoot = entityRoot.FindChild("FaceRoot");
            line = (Line)entityRoot.FindChild("Line");
        }

        public Node CreateEntity(string AssetName)
        {
            var entity = GD.Load<PackedScene>(AssetUtility.GetEntityAsset(AssetName));
            Node n = entity.Instantiate();
            EntityRoot.AddChild(n);
            return n;
        }

        public Node CreateItem(string AssetName)
        {
            var entity = GD.Load<PackedScene>(AssetUtility.GetItemAsset(AssetName));
            Node n = entity.Instantiate();
            EntityRoot.AddChild(n);
            return n;
        }

        public void CreatePoint(string AssetName, Vector2 position = default(Vector2))
        {
            var entity = GD.Load<PackedScene>(AssetUtility.GetPointAsset(AssetName));
            Area2D n = (Area2D)entity.Instantiate();
            n.Position = position;
            PointsRoot.AddChild(n);
        }

        // public void CreatePoint(string AssetName, Vector2 position = default(Vector2))
        // {
        //     var collision = new CollisionShape2D();
        //     var circle = new CircleShape2D();
        //     collision.Position = position;
        //     circle.Radius = 1.5f;
        //     collision.Shape = circle;
        //     //collision.SetDeferred("shape", circle);
        //     Points.AddChild(collision);
        // }

        public Node CreateLine(string AssetName, Vector2 position = default(Vector2))
        {
            var entity = GD.Load<PackedScene>(AssetUtility.GetPointAsset(AssetName));
            Node n = entity.Instantiate();
            PointsRoot.AddChild(n);
            return n;
        }

        public Node CreateEnemy(string AssetName)
        {
            var entity = GD.Load<PackedScene>(AssetUtility.GetEnemyAsset(AssetName));
            Node n = entity.Instantiate();
            EnemyRoot.AddChild(n);
            return n;
        }

        public Node CreateSkill(string AssetName)
        {
            var entity = GD.Load<PackedScene>(AssetUtility.GetSkillEntityAsset(AssetName));
            Node n = entity.Instantiate();
            EntityRoot.AddChild(n);
            return n;
        }


        public void CreateHud(int numb, Vector2 position)
        {
            var entity = GD.Load<PackedScene>(AssetUtility.GetUIItemAsset("NumbHud"));
            Hud n = (Hud)entity.Instantiate();
            n.num = numb;
            n.Position = position;
            EntityRoot.AddChild(n);
        }

        public void CreateEffect(string AssetName, Vector2 position = default(Vector2))
        {
            var entity = GD.Load<PackedScene>(AssetUtility.GetEffectAsset(AssetName));
            Node2D n = (Node2D)entity.Instantiate();
            n.Position = position;
            EntityRoot.AddChild(n);
        }

        public Node CreateMainSkillBook(int id)
        {
            var entity = GD.Load<PackedScene>(AssetUtility.GetItemAsset("MainSkillBook"));
            MainSkillBook n = (MainSkillBook)entity.Instantiate();
            n.id = id;
            EntityRoot.AddChild(n);
            return n;
        }

        public void ClearPoints()
        {
            foreach (Node point in PointsRoot.GetChildren())
            {
                point.QueueFree();
            }
            //line.ClearPoints();
        }

        public void AddLinePoint(Vector2 position)
        {
            line.AddPoint(position);
        }

        public void CreateFace(string AssetName, int startPointIndex)
        {
            if (FaceRoot.GetChildCount() > 0)
            {
                return;
            }
            var entity = GD.Load<PackedScene>(AssetUtility.GetFaceAsset(AssetName));
            Face n = (Face)entity.Instantiate();
            n.points = GetFacePoints(startPointIndex);
            FaceRoot.AddChild(n);
        }

        public Vector2[] GetFacePoints(int start)
        {
            Vector2[] points = new Vector2[PointsRoot.GetChildCount() - start];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = PointsRoot.GetChild<Node2D>(i + start).Position;
            }
            return points;
        }

        public int GetPointsLength()
        {
            return PointsRoot.GetChildCount();
        }
    }
}
