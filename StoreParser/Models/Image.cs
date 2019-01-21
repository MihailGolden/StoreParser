using System;

namespace StoreParser.Models
{
    public class Image
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public byte[] Data { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string ContentType { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
