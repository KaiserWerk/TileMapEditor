namespace TileMapEditor.Model
{
    public class MapField
    {
        public long Id { get; set; }
        public string ImageFilename { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
