using System;

namespace src
{
    public class ClaimDto
    {
        public ClaimDto(int leftEdge, int topEdge, int width, int height)
        {
            LeftEdgePosition = leftEdge;
            TopEdgePosition = topEdge;
            Width = width;
            Height = height;
        }

        public int LeftEdgePosition { get; }
        public int TopEdgePosition { get; }
        public int Width { get; }
        public int Height { get; }
    }
}
