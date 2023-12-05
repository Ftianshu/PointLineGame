using Godot;
using System.Collections.Generic;

namespace Survival
{

    public class EntityManager
    {
        private Node EntityRoot;
        public Node EnemyRoot;
        private Node PointsRoot;
        private Node FaceRoot;

        public Node LineRoot;

        private Dictionary<int, Line> lines = new Dictionary<int, Line>();
        private int currentMap = 0;

        public EntityManager(Node entityRoot, Node enemyRoot)
        {
            EntityRoot = entityRoot;
            EnemyRoot = enemyRoot;
            FaceRoot = entityRoot.FindChild("FaceRoot");
            LineRoot = entityRoot.FindChild("LineRoot");
        }

        public Node CreateEntity(string AssetName)
        {
            var entity = GD.Load<PackedScene>(AssetUtility.GetEntityAsset(AssetName));
            Node n = entity.Instantiate();
            EntityRoot.AddChild(n);
            return n;
        }

        public void CreatePoint(string AssetName, int lineId, Vector2 position)
        {
            var entity = GD.Load<PackedScene>(AssetUtility.GetPointAsset(AssetName));
            Point n = (Point)entity.Instantiate();
            n.Position = position;
            n.lineId = lineId;
            lines[lineId].AddChild(n);
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

        public void CreateLine(int lineId)
        {
            var entity = GD.Load<PackedScene>(AssetUtility.GetPointAsset("line"));
            Line n = (Line)entity.Instantiate();
            n.lineId = lineId;
            LineRoot.AddChild(n);
            n.Name = lineId.ToString();
            lines.Add(lineId, n);
        }

        public Line GetLine(int lineId)
        {
            return (Line)lines[lineId];
        }

        public void DelLine(int lineId)
        {
            lines[lineId].QueueFree();
            lines.Remove(lineId);
        }

        public void ClearLinePoints(int lineId)
        {
            for (int i = 2; i < lines[lineId].GetChildren().Count; i++)
            {
                lines[lineId].GetChild(i).QueueFree();
            }
            //line.ClearPoints();
        }

        public void AddLinePoint(Vector2 position, int lineId)
        {
            (lines[lineId] as Line).AddPoint(position);
        }

        public void CreateFace(string AssetName, Vector2[] points)
        {
            var entity = GD.Load<PackedScene>(AssetUtility.GetFaceAsset(AssetName));
            Face n = (Face)entity.Instantiate();
            n.points = points;
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
