using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Survival
{

    public class FaceManager
    {
        int cout = 0;
        int faceWidth = 540;
        int faceHeight = 540;
        int faceCount = 3;

        bool[][][] faces = new bool[3][][];

        List<int>[] lineIds = new List<int>[3];

        Vector2 lastPoint;

        public FaceManager()
        {
            // 运行速度还是下面的快，下面跳转少，优化一下
            // for (int i = 0; i < faceCount; i++)
            // {
            //     faces[i] = new bool[faceWidth][];
            //     for (int j = 0; j < faceWidth; j++)
            //     {
            //         faces[i][j] = new bool[faceHeight];
            //     }
            // }
            faces[0] = new bool[faceWidth][];
            faces[1] = new bool[faceWidth][];
            faces[2] = new bool[faceWidth][];
            lineIds[0] = new List<int>();
            lineIds[1] = new List<int>();
            lineIds[2] = new List<int>();
            for (int i = 0; i < faceWidth; i++)
            {
                faces[0][i] = new bool[faceHeight];
                faces[1][i] = new bool[faceHeight];
                faces[2][i] = new bool[faceHeight];
            }

        }
        //FaceId 默认：0-玩家平面；1-敌人平面；2-函数平面

        public void AddPoint(Vector2 point, int faceId, int lineId)
        {
            // 如果有新的line加入，在list中记录
            if (!lineIds[faceId].Contains(lineId))
            {
                GameEntry.Entity.CreateLine(lineId);
                lineIds[faceId].Add(lineId);
                lastPoint = point;
            }

            // 距离过近不生成点
            if (lastPoint.DistanceTo(point) < 2)
            {
                return;
            }

            //添加点
            GameEntry.Entity.AddLinePoint(point, lineId);
            GameEntry.Entity.CreatePoint("points", lineId, point);
            lastPoint = point;
        }


        public void LinkLines(int lineId1, int lineId2, LinkPoint linkPoint)
        {
            Line line1 = GameEntry.Entity.GetLine(lineId1);
            if (lineId1 == lineId2)
            {
                //GD.Print("生成平面");
                GameEntry.Entity.CreateFace("face", GetFacePoints(line1, linkPoint.point1.GetIndex()));
                ClearFaceLines((int)FaceId.PlayerFace);
                return;
            }

            Line line2 = GameEntry.Entity.GetLine(lineId2);
            line1.linkPoints.Add(linkPoint);
            line2.linkPoints.Add(linkPoint);
            if (line1.linkedLines.Contains(lineId2))
            {
                // TODO: 生成平面
                GD.Print("生成平面");
                //GD.Print(closedShapePoints.Count);
                GameEntry.Entity.CreateFace("face", GetFacePoints(line1));
                // 清楚所有点、线
                //ClearFacePoints(faceId);
                ClearFaceLines((int)FaceId.PlayerFace);
                return;
            }
            line1.linkedLines = line1.linkedLines.Union(line2.linkedLines).ToList();
            line2.linkedLines = line2.linkedLines.Union(line1.linkedLines).ToList();
            // GD.Print(line1.linkedLines.Count);
        }

        private Vector2[] GetFacePoints(Line line, int start)
        {
            Vector2[] points = new Vector2[line.GetChildCount() - start - 3];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = line.GetChild<Node2D>(i + start).Position;
            }
            return points;
        }

        private Vector2[] GetFacePoints(Line line)
        {
            List<Vector2> points = new List<Vector2>();
            Line startLine = line;
            LinkPoint lastLinkPoint = null;
            do
            {
                List<LinkPoint> linkPoints = new List<LinkPoint>();
                //除了第一条线，其余的线的起始点都是上一条线的最后一个点
                if (lastLinkPoint != null)
                {
                    linkPoints.Add(lastLinkPoint);
                }
                //找到线段的两个端点
                for (int i = 0; i < line.linkPoints.Count; i++)
                {
                    if (line.linkPoints[i].point1.lineId != line.lineId)
                    {
                        if (line.linkedLines.Contains(line.linkPoints[i].point1.lineId))
                        {
                            if (line.linkPoints[i] != lastLinkPoint)
                                linkPoints.Add(line.linkPoints[i]);
                        }
                        continue;
                    }
                    if (line.linkedLines.Contains(line.linkPoints[i].point2.lineId))
                    {
                        if (line.linkPoints[i] != lastLinkPoint)
                            linkPoints.Add(line.linkPoints[i]);
                    }
                }

                //通过端点，找到线段上所有的点
                int start = linkPoints[0].point1.lineId == line.lineId ? linkPoints[0].point1.GetIndex() : linkPoints[0].point2.GetIndex();
                int end = linkPoints[1].point1.lineId == line.lineId ? linkPoints[1].point1.GetIndex() : linkPoints[1].point2.GetIndex();
                if (start < end)
                    for (int i = start; i < end; i++)
                    {
                        points.Add(line.GetChild<Node2D>(i).Position);
                    }
                else
                    for (int i = start; i > end; i--)
                    {
                        points.Add(line.GetChild<Node2D>(i).Position);
                    }

                lastLinkPoint = linkPoints[1];
                //根据连接点找到下一条线段
                line = linkPoints[1].point1.lineId == line.lineId ? GameEntry.Entity.GetLine(linkPoints[1].point2.lineId) : GameEntry.Entity.GetLine(linkPoints[1].point1.lineId);
            } while (line != startLine);

            return points.ToArray();
        }

        // private Vector2[] GetFacePoints(Line line)
        // {
        //     line.linkPoints
        // }

        // private Vector2[] GetFacePoint(Line line1, Line line2)
        // {

        // }

        private bool IsClosedShape(int x, int y, int faceId)
        {
            bool[][] map = new bool[faceWidth][];
            for (int i = 0; i < map.Length; i++)
            {
                map[i] = (bool[])faces[faceId][i].Clone();
            }

            return DFS(map, (x, y), faceWidth, faceHeight);
        }

        private bool DFS(bool[][] map, (int row, int col) start, int rows, int cols)
        {

            // 使用栈来模拟递归调用

            Stack<(int row, int col)> stack = new Stack<(int, int)>();
            Dictionary<(int row, int col), int> pointsCount = new Dictionary<(int row, int col), int>();
            stack.Push(start);
            int popCount = 0;

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                int row = current.row;
                int col = current.col;
                if (pointsCount.ContainsKey(current) && pointsCount[current]++ >= 1)
                {
                    return true;
                }

                if (row < 0 || row >= rows || col < 0 || col >= cols || !map[row][col])
                {
                    popCount++;
                    continue;
                }

                map[row][col] = false; // 标记已访问

                // 将相邻的点入栈
                stack.Push((row + 1, col));
                stack.Push((row - 1, col));
                stack.Push((row, col + 1));
                stack.Push((row, col - 1));

                pointsCount.Add(current, 0);
            }

            return false;
        }

        public void ClearFaceLines(int faceId)
        {
            foreach (int lineId in lineIds[faceId])
            {
                GameEntry.Entity.DelLine(lineId);
            }
            //清空列表
            lineIds[faceId].Clear();
        }

        public int GenerateLineId()
        {
            return cout++;
        }
    }
}
