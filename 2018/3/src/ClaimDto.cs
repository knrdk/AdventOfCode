using System;

namespace src
{
    public class ClaimDto
    {
        public ClaimDto(int id, int leftEdge, int topEdge, int width, int height)
        {
            Id = id;
            LeftEdgePosition = leftEdge;
            TopEdgePosition = topEdge;
            Width = width;
            Height = height;
        }

        public int Id { get; }
        public int LeftEdgePosition { get; }
        public int TopEdgePosition { get; }
        public int Width { get; }
        public int Height { get; }
    }
}
