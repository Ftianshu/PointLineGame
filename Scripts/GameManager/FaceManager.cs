using System.Collections.Generic;
using Godot;

namespace Survival
{

    public class FaceManager
    {
        int cout = 0;
        int faceWidth = 540;
        int faceHeight = 540;
        int faceCount = 3;
        //Dictionary<int, bool[][]> faces = new Dictionary<int, bool[][]>();
        bool[][][] faces = new bool[3][][];

        List<int>[] lineIds = new List<int>[3];

        List<Vector2> closedShapePoints = new List<Vector2>();

        (int x, int y)[] lastPoint = new (int x, int y)[3];

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
        public void AddNewFace(int faceId)
        {
            //faces.Add(faceId, new bool[faceWidth][]);
        }

        // public int AddNewLine()
        // {
        //     int lineId = GenerateLineId();
        //     GameEntry.Entity.CreateLine(lineId);
        //     return lineId;
        // }

        public void AddPoint(Vector2 point, int faceId, int lineId)
        {
            int indexX = (int)point.X + 270;
            int indexY = (int)point.Y + 270;

            // 如果有新的line加入，在list中记录
            if (!lineIds[faceId].Contains(lineId))
            {
                GameEntry.Entity.CreateLine(lineId);
                lineIds[faceId].Add(lineId);
                lastPoint[faceId] = (indexX, indexY);
            }
            if (faces[faceId][indexX][indexY])
            {
                // 无需重复添加点
                if (IsClosedShape(indexX, indexY, faceId))
                {
                    // TODO: 生成平面
                    // GD.Print("生成平面");
                    // 清楚所有点、线
                    ClearFacePoints(faceId);
                    ClearFaceLines(faceId);
                }
                return;
            }

            //添加点
            GameEntry.Entity.AddLinePoint(point, lineId);

            //GD.Print("addPoint" + "  x:" + indexX + "  y:" + indexY);
            faces[faceId][indexX][indexY] = true;

            //不连续需要补点,保证线条的连续性
            if (Mathf.Abs(indexX - lastPoint[faceId].x) + Mathf.Abs(indexY - lastPoint[faceId].y) > 1)
            {
                if (indexX < lastPoint[faceId].x)
                {
                    for (int i = indexX + 1; i <= lastPoint[faceId].x; i++)
                    {
                        faces[faceId][i][indexY] = true;
                    }
                }
                else
                {
                    for (int i = lastPoint[faceId].x + 1; i <= indexX; i++)
                    {
                        faces[faceId][i][indexY] = true;
                    }
                }

                if (indexY < lastPoint[faceId].y)
                {
                    for (int i = indexY + 1; i < lastPoint[faceId].y; i++)
                    {
                        faces[faceId][lastPoint[faceId].x][i] = true;
                    }
                }
                else
                {
                    for (int i = lastPoint[faceId].y + 1; i < indexY; i++)
                    {
                        faces[faceId][lastPoint[faceId].x][i] = true;
                    }
                }
            }

            lastPoint[faceId] = (indexX, indexY);

            if (IsClosedShape(indexX, indexY, faceId))
            {
                // TODO: 生成平面
                // GD.Print("生成平面");
                GD.Print(closedShapePoints.Count);
                GameEntry.Entity.CreateFace("face", closedShapePoints.ToArray());
                // 清楚所有点、线
                ClearFacePoints(faceId);
                ClearFaceLines(faceId);
            }
        }

        public void DelPoint(Vector2 point, int faceId, int lineId)
        {
            int indexX = (int)point.X + 270;
            int indexY = (int)point.Y + 270;
            faces[faceId][indexX][indexY] = false;
        }

        public bool IsFormFace(int faceId)
        {
            return false;
        }

        private bool IsClosedShape(int x, int y, int faceId)
        {
            bool[][] map = new bool[faceWidth][];
            for (int i = 0; i < map.Length; i++)
            {
                map[i] = (bool[])faces[faceId][i].Clone();
            }
            closedShapePoints.Clear();

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
                // if (popCount >= 4)
                // {
                //     closedShapePoints.Clear();
                // }
                // popCount = 0;
                closedShapePoints.Add(new Vector2(row - 270, col - 270)); // 将点添加到列表

                // 将相邻的点入栈
                stack.Push((row + 1, col));
                stack.Push((row - 1, col));
                stack.Push((row, col + 1));
                stack.Push((row, col - 1));

                pointsCount.Add(current, 0);
            }

            return false;
        }

        public void ClearFacePoints(int faceId)
        {
            for (int i = 0; i < faceWidth; i++)
            {
                for (int j = 0; j < faceHeight; j++)
                {
                    faces[faceId][i][j] = false;
                }
            }
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
