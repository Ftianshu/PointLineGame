using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Survival
{

    public class FaceManager
    {
        public FaceManager()
        {

        }

        public void LinkLines(Line line1, Line line2, LinkPoint linkPoint)
        {
            line1.linkPoints.Add(linkPoint);
            line2.linkPoints.Add(linkPoint);
            if (line1.linkedLines.Contains(line2.lineId))
            {
                // TODO: 生成平面
                GD.Print("生成平面");
                GameEntry.Entity.CreateFace("face", GetFacePoints(line1));
                // 清楚所有点、线
                GameEntry.Entity.ResetFaceLine(line1.faceId);
                return;
            }
            //更新所有连接的line中的linkedLines
            List<int> newLink = line1.linkedLines.Union(line2.linkedLines).ToList();
            for (int i = 0; i < newLink.Count; i++)
            {
                GameEntry.Entity.GetLine(newLink[i]).linkedLines = newLink;
            }
        }

        public void LinkLines(int lineId1, int lineId2, LinkPoint linkPoint)
        {
            Line line1 = GameEntry.Entity.GetLine(lineId1);
            if (lineId1 == lineId2)
            {
                // GameEntry.Entity.CreateFace("face", GetFacePoints(line1, linkPoint.point1.GetIndex()));
                GameEntry.Entity.ResetFaceLine(line1.faceId);
                return;
            }
            // GD.Print("line1: " + lineId1 + "--" + "line2:  " + lineId2);
            Line line2 = GameEntry.Entity.GetLine(lineId2);
            line1.linkPoints.Add(linkPoint);
            line2.linkPoints.Add(linkPoint);
            if (line1.linkedLines.Contains(lineId2))
            {
                // TODO: 生成平面
                GD.Print("生成平面");
                GameEntry.Entity.CreateFace("face", GetFacePoints(line1));
                // 清楚所有点、线
                GameEntry.Entity.ResetFaceLine(line1.faceId);
                return;
            }
            //更新所有连接的line中的linkedLines
            List<int> newLink = line1.linkedLines.Union(line2.linkedLines).ToList();
            for (int i = 0; i < newLink.Count; i++)
            {
                GameEntry.Entity.GetLine(newLink[i]).linkedLines = newLink;
            }
        }

        public Vector2[] GetFacePoints(Line2D line, int start)
        {
            Vector2[] points = new Vector2[line.Points.Length - start - 1];
            GD.Print(points.Length + "---" + line.Points[points.Length - 1 + start]);
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = line.Points[i + start];
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
                LinkPoint[] linkPoints = new LinkPoint[2];
                //除了第一条线，其余的线的起始点都是上一条线的最后一个点
                if (lastLinkPoint != null)
                {
                    linkPoints[0] = lastLinkPoint;
                }
                //找到线段的两个端点
                for (int i = 0; i < line.linkPoints.Count; i++)
                {
                    if (line.linkPoints[i].lineId1 != line.lineId)
                    {
                        if (line.linkedLines.Contains(line.linkPoints[i].lineId1))
                        {
                            if (line.linkPoints[i] != lastLinkPoint)
                                linkPoints[1] = line.linkPoints[i];
                        }
                        continue;
                    }
                    if (line.linkedLines.Contains(line.linkPoints[i].lineId2))
                    {
                        if (line.linkPoints[i] != lastLinkPoint)
                            linkPoints[1] = line.linkPoints[i];
                    }
                }

                //通过端点，找到线段上所有的点
                int start = linkPoints[0].lineId1 == line.lineId ? line.GetPointIndex(linkPoints[0].point1) : line.GetPointIndex(linkPoints[0].point2);
                int end = linkPoints[1].lineId1 == line.lineId ? line.GetPointIndex(linkPoints[0].point1) : line.GetPointIndex(linkPoints[0].point2);
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
                // //根据连接点找到下一条线段
                line = linkPoints[1].lineId1 == line.lineId ? GameEntry.Entity.GetLine(linkPoints[1].lineId1) : GameEntry.Entity.GetLine(linkPoints[1].lineId2);
            } while (line != startLine);

            return points.ToArray();
        }

        // public int GenerateLineId()
        // {
        //     GameEntry.Entity.CreateLine(cout, new Color(255, 255, 255), 0);
        //     return cout++;
        // }
    }
}
