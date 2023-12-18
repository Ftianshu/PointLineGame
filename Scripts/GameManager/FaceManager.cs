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

        List<int>[] lineIds = new List<int>[3];

        Vector2[] lastPoint = new Vector2[3];

        public FaceManager()
        {
            lineIds[0] = new List<int>();
            lineIds[1] = new List<int>();
            lineIds[2] = new List<int>();
        }


        public void LinkLines(int lineId1, int lineId2, LinkPoint linkPoint)
        {
            Line line1 = GameEntry.Entity.GetLine(lineId1);
            if (lineId1 == lineId2)
            {
                GameEntry.Entity.CreateFace("face", GetFacePoints(line1, linkPoint.point1.GetIndex()));
                ClearFaceLines(line1.faceId);
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
                ClearFaceLines(line1.faceId);
                return;
            }
            //更新所有连接的line中的linkedLines
            List<int> newLink = line1.linkedLines.Union(line2.linkedLines).ToList();
            for (int i = 0; i < newLink.Count; i++)
            {
                GameEntry.Entity.GetLine(newLink[i]).linkedLines = newLink;
            }
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


        public void ClearFaceLines(int faceId)
        {
            foreach (int lineId in lineIds[faceId])
            {
                GameEntry.Entity.DelLine(lineId);
            }
            //清空列表
            lineIds[faceId].Clear();
        }

        // public int GenerateLineId()
        // {
        //     GameEntry.Entity.CreateLine(cout, new Color(255, 255, 255), 0);
        //     return cout++;
        // }
    }
}
