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
        private Coordinate coordinate;
        private int lineCout = 0;
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

        public Node CreateEntity(string AssetName, Vector2 Position = default)
        {
            var entity = GD.Load<PackedScene>(AssetUtility.GetEntityAsset(AssetName));
            Node n = entity.Instantiate();
            if (Position != default)
            {
                (n as Node2D).Position = Position;
            }
            EntityRoot.AddChild(n);
            return n;
        }

        public Node2D CreateEnemy(string AssetName, Vector2 position = default)
        {
            var entity = GD.Load<PackedScene>(AssetUtility.GetEnemyAsset(AssetName));
            Node2D n = entity.Instantiate() as Node2D;
            EnemyRoot.AddChild(n);

            n.Position = position;
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

        public void CreateEffect(string AssetName, Vector2 position = default)
        {
            var entity = GD.Load<PackedScene>(AssetUtility.GetEffectAsset(AssetName));
            Node2D n = (Node2D)entity.Instantiate();
            n.Position = position;
            EntityRoot.AddChild(n);
        }

        public int CreateLine(Color color, int faceId)
        {
            var entity = GD.Load<PackedScene>(AssetUtility.GetLineAsset("line"));
            Line n = (Line)entity.Instantiate();
            n.lineId = lineCout;
            n.faceId = faceId;
            n.GetNode<Line2D>("Line2D").DefaultColor = color;
            LineRoot.AddChild(n);
            n.Name = lineCout.ToString();
            lines.Add(lineCout, n);
            return lineCout++;
        }

        public int CreatePlayerLine()
        {
            var entity = GD.Load<PackedScene>(AssetUtility.GetLineAsset("PlayerLine"));
            Line n = (Line)entity.Instantiate();
            n.lineId = lineCout;
            n.faceId = 0;
            LineRoot.AddChild(n);
            n.Name = lineCout.ToString();
            lines.Add(lineCout, n);
            return lineCout++;
        }

        public int CreateEnemyLine()
        {
            var entity = GD.Load<PackedScene>(AssetUtility.GetLineAsset("EnemyLine"));
            Line n = (Line)entity.Instantiate();
            n.lineId = lineCout;
            n.faceId = 1;
            n.GetNode<Line2D>("Line2D").DefaultColor = new Color(255, 0, 255);
            LineRoot.AddChild(n);
            n.Name = lineCout.ToString();
            lines.Add(lineCout, n);
            return lineCout++;
        }

        public Line GetLine(int lineId)
        {
            return lines[lineId];
        }

        public void DelLine(int lineId)
        {
            lines[lineId].QueueFree();
            lines.Remove(lineId);
        }

        public void ResetLine(int lineId)
        {
            lines[lineId].ResetLine();
        }

        public void ResetFaceLine(int faceId)
        {
            foreach (Line line in lines.Values)
            {
                if (line.faceId == faceId)
                {
                    line.ResetLine();
                }
            }
        }

        public void ClearLinePoints(int lineId)
        {
            for (int i = 2; i < lines[lineId].GetChildren().Count; i++)
            {
                lines[lineId].GetChild(i).QueueFree();
            }
            //line.ClearPoints();
        }

        public void CreateCoordinate()
        {
            var entity = GD.Load<PackedScene>(AssetUtility.GetEntityAsset("Players/Coordinate"));
            coordinate = entity.Instantiate() as Coordinate;
            EntityRoot.AddChild(coordinate);
            coordinate.Scale = new Vector2(0.3f, 0.3f);
            coordinate.Position = new Vector2(275, -100);

            DrawCoordinateLine(GameEntry.Player.faceDamage, 0);
        }
        public void DrawCoordinateLine(float start, float slope)
        {
            coordinate.DrawLine(start, slope);
        }

        public void DrawCoordinatePoint(int x, int y)
        {
            coordinate.DrawPoint(x, y);
        }

        public void AddLinePoint(Vector2 position, int lineId)
        {
            lines[lineId].AddPoint(position);
        }

        public void CreateFace(string AssetName, Vector2[] points, FaceId faceId = FaceId.PlayerFace)
        {
            var entity = GD.Load<PackedScene>(AssetUtility.GetFaceAsset(AssetName));
            Face n = (Face)entity.Instantiate();
            n.faceId = (int)faceId;
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
